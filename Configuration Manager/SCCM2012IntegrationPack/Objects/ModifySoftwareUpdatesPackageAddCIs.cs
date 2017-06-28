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
    [Activity("Modify SCCM Software Updates Package: Add CIs")]
    public class ModifySoftwareUpdatesPackageAddCIs : IActivity
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
            designer.AddInput("Software Updates Package ID").WithDefaultValue("ABC00000");
            designer.AddInput("Content ID List (CSV)").WithDefaultValue("1,2,3");
            designer.AddInput("Content Source Path List (CSV)").WithDefaultValue("\\\\PATH\\To\\ContentID1,\\\\PATH\\To\\ContentID2,\\\\PATH\\To\\ContentID3");
            designer.AddInput("Refresh DPs").WithBooleanBrowser().WithDefaultValue(false);
            designer.AddCorellatedData(typeof(package));
            designer.AddOutput("Number of Software Updates Packages");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Collection ID"].AsString();
            String contentIDList = request.Inputs["Content ID List (CSV)"].AsString();
            String contentSourcePathList = request.Inputs["Content Source Path List (CSV)"].AsString();
            bool refreshDPs = request.Inputs["Refresh DPs"].AsBoolean();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                CM2012Interop.modifySCCMSoftwareUpdatesPackageAddCIs(connection, objID, contentIDList, contentSourcePathList, refreshDPs);

                IResultObject col = null;
                col = CM2012Interop.getSCCMSoftwareUpdatesPackage(connection, "PackageID LIKE '" + objID + "'");

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Collections", ObjCount);
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

