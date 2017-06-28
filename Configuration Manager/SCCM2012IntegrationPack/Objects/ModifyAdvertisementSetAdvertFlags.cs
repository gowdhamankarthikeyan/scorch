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
    [Activity("Modify SCCM Advertisement: Set Advert Flags")]
    public class ModifyAdvertisementSetAdvertFlags : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        private const uint IMMEDIATE = 0x00000020;
        private const uint ONSYSTEMSTARTUP = 0x00000100;
        private const uint ONUSERLOGON = 0x00000200;
        private const uint ONUSERLOGOFF = 0x00000400;
        private const uint WINDOWS_CE = 0x00008000;
        private const uint DONOT_FALLBACK = 0x00020000;
        private const uint ENABLE_TS_FROM_CD_AND_PXE = 0x00040000;
        private const uint OVERRIDE_SERVICE_WINDOWS = 0x00100000;
        private const uint REBOOT_OUTSIDE_OF_SERVICE_WINDOWS = 0x00200000;
        private const uint WAKE_ON_LAN_ENABLED = 0x00400000;
        private const uint SHOW_PROGRESS = 0x00800000;
        private const uint NO_DISPLAY = 0x02000000;
        private const uint ONSLOWNET = 0x04000000;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Advertisement ID").WithDefaultValue("ABC00000");

            designer.AddInput("IMMEDIATE").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ONSYSTEMSTARTUP").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ONUSERLOGON").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ONUSERLOGOFF").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("WINDOWS_CE").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DONOT_FALLBACK").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ENABLE_TS_FROM_CD_AND_PXE").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("OVERRIDE_SERVICE_WINDOWS").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("REBOOT_OUTSIDE_OF_SERVICE_WINDOWS").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("WAKE_ON_LAN_ENABLED").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("SHOW_PROGRESS").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("NO_DISPLAY").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ONSLOWNET").WithDefaultValue(false).WithBooleanBrowser();

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

            if (request.Inputs["IMMEDIATE"].AsBoolean()) { flags ^= IMMEDIATE; }
            if (request.Inputs["ONSYSTEMSTARTUP"].AsBoolean()) { flags ^= ONSYSTEMSTARTUP; }
            if (request.Inputs["ONUSERLOGON"].AsBoolean()) { flags ^= ONUSERLOGON; }
            if (request.Inputs["ONUSERLOGOFF"].AsBoolean()) { flags ^= ONUSERLOGOFF; }
            if (request.Inputs["WINDOWS_CE"].AsBoolean()) { flags ^= WINDOWS_CE; }
            if (request.Inputs["DONOT_FALLBACK"].AsBoolean()) { flags ^= DONOT_FALLBACK; }
            if (request.Inputs["ENABLE_TS_FROM_CD_AND_PXE"].AsBoolean()) { flags ^= ENABLE_TS_FROM_CD_AND_PXE; }
            if (request.Inputs["OVERRIDE_SERVICE_WINDOWS"].AsBoolean()) { flags ^= OVERRIDE_SERVICE_WINDOWS; }
            if (request.Inputs["REBOOT_OUTSIDE_OF_SERVICE_WINDOWS"].AsBoolean()) { flags ^= REBOOT_OUTSIDE_OF_SERVICE_WINDOWS; }
            if (request.Inputs["WAKE_ON_LAN_ENABLED"].AsBoolean()) { flags ^= WAKE_ON_LAN_ENABLED; }
            if (request.Inputs["SHOW_PROGRESS"].AsBoolean()) { flags ^= SHOW_PROGRESS; }
            if (request.Inputs["NO_DISPLAY"].AsBoolean()) { flags ^= NO_DISPLAY; }
            if (request.Inputs["ONSLOWNET"].AsBoolean()) { flags ^= ONSLOWNET; }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                String flagsString = Convert.ToString((int)flags);

                CM2012Interop.modifySCCMAdvertisement(connection, objID, "IntegerValue", "AdvertFlags", flagsString);
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

