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
    [Activity("Set Collection Variable")]
    public class SetCollectionVariable : IActivity
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
            designer.AddInput("Variable Value");
            designer.AddInput("CollectionID").WithDefaultValue("AAA00003");
            designer.AddInput("Precedence");
            designer.AddOutput("Variable Name");
            designer.AddOutput("Variable Value");
            designer.AddOutput("CollectionID");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String CollectionID = request.Inputs["CollectionID"].AsString();
            String variableName = request.Inputs["Variable Name"].AsString();
            String variableValue = request.Inputs["Variable Value"].AsString();
            int precedence = (int)request.Inputs["Precedence"].AsUInt32();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                CMInterop.createSCCMCollectionVariable(connection, variableName, variableValue, false, CollectionID, precedence);

                response.Publish("Variable Name", variableName);
                response.Publish("Variable Value", variableValue);
                response.Publish("CollectionID", CollectionID);
            }
        }
    }
}

