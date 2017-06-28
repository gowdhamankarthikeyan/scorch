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
    [Activity("Delete Collection Variable")]
    public class DeleteCollectionVariable : IActivity
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
            designer.AddInput("Collection ID").WithDefaultValue("AAA00000");

            designer.AddOutput("Variable Name");
            designer.AddOutput("Collection ID");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String collectionID = request.Inputs["Collection ID"].AsString();
            String variableName = request.Inputs["Variable Name"].AsString();


            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {  
                CMInterop.deleteSCCMCollectionVariable(connection, variableName, collectionID);

                response.Publish("Variable Name", variableName);
                response.Publish("Collection ID", collectionID);
            }
        }
    }
}

