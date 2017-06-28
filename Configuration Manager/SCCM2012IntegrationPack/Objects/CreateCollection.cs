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
    [Activity("Create SCCM Collection")]
    public class CreateCollection : IActivity
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
            designer.AddInput("New Collection Name").WithDefaultValue("Collection NAME");
            designer.AddInput("New Collection Comment").WithDefaultValue("Comments").NotRequired();
            designer.AddInput("New Collection Refresh Minutes").WithDefaultValue("1").NotRequired();
            designer.AddInput("New Collection Refresh Hours").WithDefaultValue("1").NotRequired();
            designer.AddInput("New Collection Refresh Days").WithDefaultValue("1").NotRequired();
            designer.AddInput("New Collection Parent CollectionID").WithDefaultValue("COLLROOT").NotRequired();
            
            designer.AddCorellatedData(typeof(collection));
            designer.AddOutput("Number of Collections");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;
            
            String colComment = String.Empty;
            int colRefreshMin = 0;
            int colRefreshHours = 0;
            int colRefreshDays = 0;
            String parentColID = String.Empty;

            String colName = request.Inputs["New Collection Name"].AsString();
            if (request.Inputs.Contains("New Collection Comment")) { colComment = request.Inputs["New Collection Comment"].AsString(); }
            if (request.Inputs.Contains("New Collection Refresh Minutes")) { colRefreshMin = (int)request.Inputs["New Collection Refresh Minutes"].AsUInt32(); }
            if (request.Inputs.Contains("New Collection Refresh Hours")) { colRefreshHours = (int)request.Inputs["New Collection Refresh Hours"].AsUInt32(); }
            if (request.Inputs.Contains("New Collection Refresh Days")) { colRefreshDays = (int)request.Inputs["New Collection Refresh Days"].AsUInt32(); }
            if (request.Inputs.Contains("New Collection Parent CollectionID")) { parentColID = request.Inputs["New Collection Parent CollectionID"].AsString(); }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {  
                IResultObject col = CM2012Interop.createSCCMCollection(connection, colName, colComment, colRefreshMin, colRefreshHours, colRefreshDays, parentColID);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Collections", ObjCount);
            }
        }
        private IEnumerable<collection> getObjects(IResultObject obj)
        {
            ObjCount++;
            yield return new collection(obj);
        }
    }
}

