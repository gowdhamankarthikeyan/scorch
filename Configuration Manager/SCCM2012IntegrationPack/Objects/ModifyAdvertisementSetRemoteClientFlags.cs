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
    [Activity("Modify SCCM Advertisement: Set Remote Client Flags")]
    public class ModifyAdvertisementSetRemoteClientFlags : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        private const uint BATTERY_POWER = 0x00000001;
        private const uint RUN_FROM_CD = 0x00000002;
        private const uint DOWNLOAD_FROM_CD = 0x00000004;
        private const uint RUN_FROM_LOCAL_DISPPOINT = 0x00000008;
        private const uint DOWNLOAD_FROM_LOCAL_DISPPOINT = 0x00000010;
        private const uint DONT_RUN_NO_LOCAL_DISPPOINT = 0x00000020;
        private const uint DOWNLOAD_FROM_REMOTE_DISPPOINT = 0x00000040;
        private const uint RUN_FROM_REMOTE_DISPPOINT = 0x00000080;
        private const uint DOWNLOAD_ON_DEMAND_FROM_LOCAL_DP = 0x00000100;
        private const uint DOWNLOAD_ON_DEMAND_FROM_REMOTE_DP = 0x00000200;
        private const uint BALLOON_REMINDERS_REQUIRED = 0x00000400;
        private const uint RERUN_ALWAYS = 0x00000800;
        private const uint RERUN_NEVER = 0x00001000;
        private const uint RERUN_IF_FAILED = 0x00002000;
        private const uint RERUN_IF_SUCCEEDED = 0x00004000;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(settings.SCCMSERVER, settings.UserName, settings.Password);

            designer.AddInput("Advertisement ID").WithDefaultValue("ABC00000");

            designer.AddInput("BATTERY_POWER").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN_FROM_CD").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DOWNLOAD_FROM_CD").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN_FROM_LOCAL_DISPPOINT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DOWNLOAD_FROM_LOCAL_DISPPOINT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DONT_RUN_NO_LOCAL_DISPPOINT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DOWNLOAD_FROM_REMOTE_DISPPOINT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN_FROM_REMOTE_DISPPOINT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DOWNLOAD_ON_DEMAND_FROM_LOCAL_DP").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DOWNLOAD_ON_DEMAND_FROM_REMOTE_DP").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("BALLOON_REMINDERS_REQUIRED").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RERUN_ALWAYS").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RERUN_NEVER").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RERUN_IF_FAILED").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RERUN_IF_SUCCEEDED").WithDefaultValue(false).WithBooleanBrowser();

            designer.AddCorellatedData(typeof(advertisement));
            designer.AddOutput("Number of Advertisements");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Advertisement ID"].AsString();

            uint flags = 0x00000000;

            if (request.Inputs["BATTERY_POWER"].AsBoolean()) { flags ^= BATTERY_POWER; }
            if (request.Inputs["RUN_FROM_CD"].AsBoolean()) { flags ^= RUN_FROM_CD; }
            if (request.Inputs["DOWNLOAD_FROM_CD"].AsBoolean()) { flags ^= DOWNLOAD_FROM_CD; }
            if (request.Inputs["RUN_FROM_LOCAL_DISPPOINT"].AsBoolean()) { flags ^= RUN_FROM_LOCAL_DISPPOINT; }
            if (request.Inputs["DOWNLOAD_FROM_LOCAL_DISPPOINT"].AsBoolean()) { flags ^= DOWNLOAD_FROM_LOCAL_DISPPOINT; }
            if (request.Inputs["DONT_RUN_NO_LOCAL_DISPPOINT"].AsBoolean()) { flags ^= DONT_RUN_NO_LOCAL_DISPPOINT; }
            if (request.Inputs["DOWNLOAD_FROM_REMOTE_DISPPOINT"].AsBoolean()) { flags ^= DOWNLOAD_FROM_REMOTE_DISPPOINT; }
            if (request.Inputs["RUN_FROM_REMOTE_DISPPOINT"].AsBoolean()) { flags ^= RUN_FROM_REMOTE_DISPPOINT; }
            if (request.Inputs["DOWNLOAD_ON_DEMAND_FROM_LOCAL_DP"].AsBoolean()) { flags ^= DOWNLOAD_ON_DEMAND_FROM_LOCAL_DP; }
            if (request.Inputs["DOWNLOAD_ON_DEMAND_FROM_REMOTE_DP"].AsBoolean()) { flags ^= DOWNLOAD_ON_DEMAND_FROM_REMOTE_DP; }
            if (request.Inputs["BALLOON_REMINDERS_REQUIRED"].AsBoolean()) { flags ^= BALLOON_REMINDERS_REQUIRED; }
            if (request.Inputs["RERUN_ALWAYS"].AsBoolean()) { flags ^= RERUN_ALWAYS; }
            if (request.Inputs["RERUN_NEVER"].AsBoolean()) { flags ^= RERUN_NEVER; }
            if (request.Inputs["RERUN_IF_FAILED"].AsBoolean()) { flags ^= RERUN_IF_FAILED; }
            if (request.Inputs["RERUN_IF_SUCCEEDED"].AsBoolean()) { flags ^= RERUN_IF_SUCCEEDED; }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                String flagsString = Convert.ToString((int)flags);

                CM2012Interop.modifySCCMAdvertisement(connection, objID, "IntegerValue", "RemoteClientFlags", flagsString);
                IResultObject col = CM2012Interop.getSCCMAdvertisement(connection, "AdvertisementID LIKE '" + objID + "'");
                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Advertisements", ObjCount);
            }
        }
        private IEnumerable<advertisement> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new advertisement(obj);
            }
        }
    }
}

