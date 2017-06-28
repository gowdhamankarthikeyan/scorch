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
    [Activity("Set SCCM Package Source Path")]
    public class SetSCCMPackageSourcePath : IActivity
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
            designer.AddInput("Existing Package ID").WithDefaultValue("ABC00000");
            designer.AddInput("New Package Source Path").WithDefaultValue("\\\\PATH\\TO\\SOURCE_FILES");
            designer.AddCorellatedData(typeof(package));
            designer.AddOutput("Number of Packages");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String pkgID = request.Inputs["Existing Package ID"].AsString();
            String pkgSourceFilePath = request.Inputs["New Package Source Path"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {
                IResultObject col = CMInterop.configureSCCMPackageSourcePath(connection, pkgID, pkgSourceFilePath);

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

