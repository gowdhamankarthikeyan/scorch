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
    [Activity("Get SCCM Computer")]
    public class GetSystem : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int SystemCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            String[] argTypes = new String[4] { "ResourceID", "NetbiosName", "DomainName", "Query" };
            designer.AddInput("Argument Type").WithListBrowser(argTypes).WithDefaultValue("NetbiosName");
            designer.AddInput("Argument String").WithDefaultValue("Computer Name");
            designer.AddCorellatedData(typeof(system));
            designer.AddOutput("Number of Systems");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String argType = request.Inputs["Argument Type"].AsString();
            String argString = request.Inputs["Argument String"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;

                //Get Computer Collection
                switch (argType)
                {
                    case "ResourceID":
                        col = CMInterop.getSCCMComputer(connection, argString, "", "");
                        break;
                    case "NetbiosName":
                        col = CMInterop.getSCCMComputer(connection, "", argString, "");
                        break;
                    case "DomainName":
                        col = CMInterop.getSCCMComputer(connection, "", "", argString);
                        break;
                    case "Query":
                        col = CMInterop.getSCCMObject(connection, "SMS_R_System", argString);
                        break;
                }

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getComputers(col));

                }
                response.Publish("Number of Systems", SystemCount);
            }
        }
        private IEnumerable<system> getComputers(IResultObject computerList)
        {
            foreach (IResultObject computer in computerList)
            {
                SystemCount++;
                yield return new system(computer);
            }
        }
    }
}

