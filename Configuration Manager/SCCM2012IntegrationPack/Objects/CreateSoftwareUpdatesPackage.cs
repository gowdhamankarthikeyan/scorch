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
    [Activity("Create SCCM Software Updates Package")]
    public class CreateSoftwareUpdatesPackage : IActivity
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
            designer.AddInput("New Package Name").WithDefaultValue("PACKAGE NAME");
            designer.AddInput("New Package Description").WithDefaultValue("Description");
            designer.AddInput("New Package Source Flag").WithDefaultValue("1");
            designer.AddInput("New Package Source Path").WithDefaultValue("\\\\PATH\\To\\Package_Source");
            designer.AddCorellatedData(typeof(package));
            designer.AddOutput("Number of Packages");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String pkgName = request.Inputs["New Package Name"].AsString();
            String pkgDescription = request.Inputs["New Package Description"].AsString();
            int pkgSourceFlag = (int)request.Inputs["New Package Source Flag"].AsUInt32();
            String pkgSourcePath = request.Inputs["New Package Source Path"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {  
                IResultObject col = CM2012Interop.createSCCMSoftwareUpdatesPackage(connection, pkgName, pkgDescription, pkgSourceFlag, pkgSourcePath);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Packages", ObjCount);
            }
        }
        private IEnumerable<package> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new package(obj);
            }
        }
    }
}

