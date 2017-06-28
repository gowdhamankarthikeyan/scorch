using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCM2012Interop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;

namespace SCCM2012IntegrationPack
{
    [Activity("Get Collection Variable(s)")]
    public class GetCollectionVariables : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private String VariableName = String.Empty;
        private String MachineName = String.Empty;

        int objCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Collection ID");
            designer.AddCorellatedData(typeof(collectionVaraible));
            designer.AddOutput("Number of Collection Variables");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String colId = request.Inputs["Collection ID"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                List<IResultObject> results = CM2012Interop.getSCCMCollectionVariables(connection, colId);
                response.WithFiltering().PublishRange(getObjectProperties(results));
                response.Publish("Number of Collection Variables", objCount);
            }
        }
        private IEnumerable<collectionVaraible> getObjectProperties(List<IResultObject> colVariableCollection)
        {
            if (colVariableCollection != null)
            {
                foreach (IResultObject variable in colVariableCollection)
                {
                    objCount++;
                    yield return new collectionVaraible(variable);
                }
            }
        }
    }
}

