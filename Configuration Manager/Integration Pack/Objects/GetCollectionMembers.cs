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
    [Activity("Get Collection Members")]
    public class GetCollectionMembers : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private String VariableName = String.Empty;
        private String MachineName = String.Empty;

        private int ObjCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Collection ID").WithDefaultValue("AAA00000");

            designer.AddCorellatedData(typeof(collectionMember));
            designer.AddOutput("Number of Members");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Collection ID"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                //Get Object Collection
                IResultObject col = CMInterop.getSCCMCollectionMembers(connection, objID);
                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));
                }
                response.Publish("Number of Members", ObjCount);
            }
        }
        private IEnumerable<collectionMember> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new collectionMember(obj);
            }
        }
    }
}

