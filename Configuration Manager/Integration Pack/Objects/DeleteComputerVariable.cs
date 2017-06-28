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


namespace SCCMExtension
{
    [Activity("Delete Computer Variable")]
    public class DeleteComputerVariable : IActivity
    {
        private ConnectionCredentials settings;
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
            designer.AddInput("Machine Name");

            designer.AddOutput("Variable Name");
            designer.AddOutput("Machine Name");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String machineName = request.Inputs["Machine Name"].AsString();
            String variableName = request.Inputs["Variable Name"].AsString();


            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {  
                //Get Computer Object
                IResultObject computerObj = null;
                IResultObject col = CMInterop.getSCCMComputer(connection, "", machineName, "");
                foreach (IResultObject c in col)
                {
                    computerObj = c;
                    break;
                }

                if (computerObj != null)
                {
                    CMInterop.deleteSCCMComputerVariable(connection, variableName, computerObj["ResourceID"].StringValue);

                    response.Publish("Variable Name", variableName);
                    response.Publish("Machine Name", machineName);
                }
                else
                {
                    response.LogErrorMessage("Could not find machine " + machineName);
                }
            }
        }
    }
}

