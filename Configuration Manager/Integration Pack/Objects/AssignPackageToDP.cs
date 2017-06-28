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
    [Activity("Assign SCCM Package To Distribution Point")]
    public class AssignPackageToDP : IActivity
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
            String[] dpSelectMethod = new String[] { "Single Distribution Point", "Distribution Point Group", "All Distribution Points" };
            designer.AddInput("Existing Package ID").WithDefaultValue("ABC00000");
            designer.AddInput("Distribution Point Selection Method").WithListBrowser(dpSelectMethod).WithDefaultValue("All Distribution Points");
            designer.AddInput("Selection Criteria").NotRequired();
            designer.AddInput("NALPath Query (Single DP)").NotRequired().WithDefaultValue("%");
            designer.AddOutput("Package ID");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String pkgID = request.Inputs["Existing Package ID"].AsString();
            String selectionMethod = request.Inputs["Distribution Point Selection Method"].AsString();
            String criteria = request.Inputs["Selection Criteria"].AsString();
            String NALPathQuery = request.Inputs["NALPath Query (Single DP)"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {
                switch (selectionMethod)
                {
                    case "Single Distribution Point":
                        if (criteria.Equals(String.Empty) || NALPathQuery.Equals(String.Empty))
                        {
                            response.LogErrorMessage("Must use optional property 'Selection Criteria' with Single DP Selection.  Criteria should be DP Name to refresh Package at.  Must also provide a NALPathQuery");
                        }
                        else
                        {
                            IResultObject obj = CMInterop.getSCCMComputer(connection, "", criteria, "");
                            foreach (IResultObject comp in obj)
                            {
                                system DP = new system(comp);

                                bool success = false;

                                foreach (String siteCode in DP.AgentSite.Split(','))
                                {
                                    try
                                    {
                                        CMInterop.assignSCCMPackageToDistributionPoint(connection, pkgID, siteCode, criteria, NALPathQuery);
                                        success = true;
                                        break;
                                    }
                                    catch { }
                                }

                                if (!success) { throw new Exception("Failed to assign package to DP"); }
                            }
                        }
                        break;
                    case "Distribution Point Group":
                        if (criteria.Equals(String.Empty))
                        {
                            response.LogErrorMessage("Must use optional property 'Selection Criteria' with DP Group Selection.  Criteria should be DP Group Name to Assign Package to");
                        }
                        else
                        {
                            CMInterop.assignSCCMPackageToDistributionPointGroup(connection, pkgID, criteria);
                        }
                        break;
                    case "All Distribution Points":
                        CMInterop.assignSCCMPackageToAllDistributionPoints(connection, pkgID);
                        break;
                }
                response.Publish("Package ID", pkgID);
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

