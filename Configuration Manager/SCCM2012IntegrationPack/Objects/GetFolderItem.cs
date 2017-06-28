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
    [Activity("Get Folder Item")]
    public class GetFolderItem : IActivity
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
            String[] FolderItemTypes = new String[] { "SMS_Package",
                                                      "SMS_Advertisement",
                                                      "SMS_Query",
                                                      "SMS_Report",
                                                      "SMS_MeteredProductRule",
                                                      "SMS_ConfigurationItem",
                                                      "SMS_OperatingSystemInstallPackage",
                                                      "SMS_StateMigration",
                                                      "SMS_ImagePackage",
                                                      "SMS_BootImagePackage",
                                                      "SMS_TaskSequencePackage",
                                                      "SMS_DeviceSettingPackage",
                                                      "SMS_DriverPackage",
                                                      "SMS_Driver",
                                                      "SMS_SoftwareUpdate" };

            designer.AddInput("Folder Item Type").WithListBrowser(FolderItemTypes).WithDefaultValue("SMS_Package");
            designer.AddInput("Filter").WithDefaultValue("ContainerNodeID LIKE '%'");
            designer.AddCorellatedData(typeof(ObjectContainerItem));
            designer.AddOutput("Number of Folder Items");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objectType = request.Inputs["Folder Item Type"].AsString();
            String filter = request.Inputs["Filter"].AsString();

            switch (objectType)
            {
                case "SMS_Package":
                    filter += " AND ObjectType = 2";
                    break;
                case "SMS_Advertisement":
                    filter += " AND ObjectType = 3";
                    break;
                case "SMS_Query":
                    filter += " AND ObjectType = 7";
                    break;
                case "SMS_Report":
                    filter += " AND ObjectType = 8";
                    break;
                case "SMS_MeteredProductRule":
                    filter += " AND ObjectType = 9";
                    break;
                case "SMS_ConfigurationItem":
                    filter += " AND ObjectType = 11";
                    break;
                case "SMS_OperatingSystemInstallPackage":
                    filter += " AND ObjectType = 14";
                    break;
                case "SMS_StateMigration":
                    filter += " AND ObjectType = 17";
                    break;
                case "SMS_ImagePackage":
                    filter += " AND ObjectType = 18";
                    break;
                case "SMS_BootImagePackage":
                    filter += " AND ObjectType = 19";
                    break;
                case "SMS_TaskSequencePackage":
                    filter += " AND ObjectType = 20";
                    break;
                case "SMS_DeviceSettingPackage":
                    filter += " AND ObjectType = 21";
                    break;
                case "SMS_DriverPackage":
                    filter += " AND ObjectType = 23";
                    break;
                case "SMS_Driver":
                    filter += " AND ObjectType = 25";
                    break;
                case "SMS_SoftwareUpdate":
                    filter += " AND ObjectType = 1011";
                    break;
                default:
                    break;
            }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;
                col = CM2012Interop.getSCCMObject(connection, "SMS_ObjectContainerItem", filter);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Folder Items", ObjCount);
            }
        }
        private IEnumerable<ObjectContainerItem> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new ObjectContainerItem(obj);
            }
        }
    }
}

