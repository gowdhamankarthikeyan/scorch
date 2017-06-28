using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCMInterop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using System.Collections;

namespace SCCMExtension
{
    [Activity("Set Computer Variable")]
    public class SetComputerVariable : IActivity
    {
        private  ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Variable Name");
            designer.AddInput("Variable Value");
            designer.AddInput("Machine Name");
            designer.AddInput("Machine Site").NotRequired();
            designer.AddInput("Resource ID").NotRequired();

            designer.AddOutput("Variable Name");
            designer.AddOutput("Variable Value");
            designer.AddOutput("Machine Name");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;
            
            String machineName = request.Inputs["Machine Name"].AsString();
            String variableName = request.Inputs["Variable Name"].AsString();
            String variableValue = request.Inputs["Variable Value"].AsString();
            String machineSite = String.Empty;


            String resourceID = String.Empty;
            if (request.Inputs.Contains("Machine Site")) { machineSite = request.Inputs["Machine Site"].AsString(); }
            if (request.Inputs.Contains("Resource ID")) { resourceID = request.Inputs["Resource ID"].AsString(); }
            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {
                //Get Computer Object
                IResultObject computerObj = null;

                IResultObject col = null;
                if (resourceID.Equals(String.Empty)) { col = CMInterop.getSCCMComputer(connection, "", machineName, ""); }
                else { col = CMInterop.getSCCMComputer(connection, resourceID, "", ""); }

                foreach (IResultObject c in col)
                {
                    computerObj = c;
                    if (computerObj != null)
                    {
                        if (machineSite.Equals(String.Empty))
                        {
                            String SiteCode = determineComputerSiteCode(computerObj);
                            CMInterop.createSCCMComputerVariable(connection, SiteCode, variableName, variableValue, false, computerObj["ResourceID"].IntegerValue);
                        }
                        else
                        {
                            CMInterop.createSCCMComputerVariable(connection, machineSite, variableName, variableValue, false, computerObj["ResourceID"].IntegerValue);
                        }

                        response.Publish("Variable Name", variableName);
                        response.Publish("Variable Value", variableValue);
                        response.Publish("Machine Name", machineName);
                    }
                    else
                    {
                        response.LogErrorMessage("Could not find machine " + machineName);
                    }
                }
            }
        }
        private static String determineComputerSiteCode(IResultObject computerObj)
        {
            String SiteCode = "";
            Hashtable siteTable = new Hashtable();
            foreach (String sCode in computerObj["AgentSite"].StringArrayValue)
            {
                if (siteTable.ContainsKey(sCode))
                {
                    int temp = (int)siteTable[sCode];
                    temp++;
                    siteTable[sCode] = temp;
                }
                else
                {
                    siteTable.Add(sCode, 1);
                }
            }

            int highest = 0;
            foreach (String sCode in siteTable.Keys)
            {
                int temp = (int)siteTable[sCode];
                if (temp > highest)
                {
                    highest = temp;
                    SiteCode = sCode;
                }
            }
            return SiteCode;
        }
    }
}

