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
    [Activity("Get Package Distribution Points")]
    public class GetPackageDPs : IActivity
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
            designer.AddInput("Package ID").WithDefaultValue("SMS00000");
            designer.AddCorellatedData(typeof(DistributionPoint));
            designer.AddOutput("Number of Distribution Points");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Package ID"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;
                col = CM2012Interop.getSCCMPackageDistributionPoints(connection, objID);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col, connection));
                }
                response.Publish("Number of Collections", ObjCount);
            }
        }
        private IEnumerable<DistributionPoint> getObjects(IResultObject objList, WqlConnectionManager connection)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new DistributionPoint(obj);                
            }
        }
    }
}

