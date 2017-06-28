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
    [Activity("Modify SCCM Image Package: Set PKG Flags")]
    public class ModifyImagePackageSetPKGFlags : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        private const uint DO_NOT_DOWNLOAD = 0x01000000;
        private const uint PERSIST_IN_CACHE = 0x02000000;
        private const uint USE_BINARY_DELTA_REP = 0x04000000;
        private const uint NO_PACKAGE = 0x10000000;
        private const uint USE_SPECIAL_MIF = 0x20000000;
        private const uint DISTRIBUTE_ON_DEMAND = 0x40000000;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Package ID").WithDefaultValue("ABC00000");

            designer.AddInput("DO_NOT_DOWNLOAD").WithDefaultValue(true).WithBooleanBrowser();

            designer.AddInput("PERSIST_IN_CACHE").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("USE_BINARY_DELTA_REP").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("NO_PACKAGE").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("USE_SPECIAL_MIF").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DISTRIBUTE_ON_DEMAND").WithDefaultValue(false).WithBooleanBrowser();

            designer.AddCorellatedData(typeof(imagePackage));
            designer.AddOutput("Number of Packages");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Package ID"].AsString();

            uint flags = 0x00000000;

            if (request.Inputs["DO_NOT_DOWNLOAD"].AsBoolean()) { flags ^= DO_NOT_DOWNLOAD; }
            if (request.Inputs["PERSIST_IN_CACHE"].AsBoolean()) { flags ^= PERSIST_IN_CACHE; }
            if (request.Inputs["USE_BINARY_DELTA_REP"].AsBoolean()) { flags ^= USE_BINARY_DELTA_REP; }
            if (request.Inputs["NO_PACKAGE"].AsBoolean()) { flags ^= NO_PACKAGE; }
            if (request.Inputs["USE_SPECIAL_MIF"].AsBoolean()) { flags ^= USE_SPECIAL_MIF; }
            if (request.Inputs["DISTRIBUTE_ON_DEMAND"].AsBoolean()) { flags ^= DISTRIBUTE_ON_DEMAND; }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                String flagsString = Convert.ToString((int)flags);

                CMInterop.modifySCCMImagePackage(connection, objID, "IntegerValue", "PkgFlags", flagsString);
                IResultObject col = CMInterop.getSCCMImagePackage(connection, "PackageID LIKE '" + objID + "'");
                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Packages", ObjCount);
            }
        }
        private IEnumerable<imagePackage> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new imagePackage(obj);
            }
        }
    }
}

