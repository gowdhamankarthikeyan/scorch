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
    [Activity("Get SCCM Parent Collection")]
    public class GetParentCollection : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Child Collection ID").WithDefaultValue("AAA00000");
            designer.AddCorellatedData(typeof(collection));
            designer.AddOutput("Number of Collections");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Child Collection ID"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;
                col = CM2012Interop.getSCCMParentCollection(connection, objID);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col, connection));
                }
                response.Publish("Number of Collections", ObjCount);
            }
        }
        private IEnumerable<collection> getObjects(IResultObject objList, WqlConnectionManager connection)
        {
            foreach (IResultObject obj in objList)
            {
                IResultObject tempObjCol = CM2012Interop.getSCCMCollection(connection, "CollectionID LIKE '" + obj["parentCollectionID"].StringValue + "'");
                foreach (IResultObject o in tempObjCol)
                {
                    ObjCount++;
                    yield return new collection(o);
                }
            }
        }
    }
}

