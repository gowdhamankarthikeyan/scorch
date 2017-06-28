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
    [Activity("Move Item To Console Folder")]
    public class MoveItemToFolder : IActivity
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
            String[] ItemTypeChoice = new String[] {    "SMS_Package",
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

            designer.AddInput("Item ID").WithDefaultValue("GMS00001");
            designer.AddInput("Item Type").WithDefaultValue("SMS_Package").WithListBrowser(ItemTypeChoice);
            designer.AddInput("Source Container ID").WithDefaultValue(1);
            designer.AddInput("Destination Container ID").WithDefaultValue(2);
            designer.AddOutput("Number of Object Container Items");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;
            
            String itemObjectID = request.Inputs["Item ID"].AsString();
            String objectType = request.Inputs["Item Type"].AsString();
            int sourceContainerID = request.Inputs["Source Container ID"].AsInt32();
            int destinationContainerID = request.Inputs["Destination Container ID"].AsInt32();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                switch (objectType)
                {
                    case "SMS_Package":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_Package, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_Advertisement":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_Advertisement, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_Query":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_Query, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_Report":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_Report, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_MeteredProductRule":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_MeteredProductRule, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_ConfigurationItem":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_ConfigurationItem, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_OperatingSystemInstallPackage":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_OperatingSystemInstallPackage, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_StateMigration":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_StateMigration, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_ImagePackage":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_ImagePackage, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_BootImagePackage":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_BootImagePackage, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_TaskSequencePackage":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_TaskSequencePackage, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_DeviceSettingPackage":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_DeviceSettingPackage, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_DriverPackage":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_DriverPackage, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_Driver":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_Driver, sourceContainerID, destinationContainerID);
                        break;
                    case "SMS_SoftwareUpdate":
                        CMInterop.moveConsoleFolderItem(connection, itemObjectID, CMInterop.folderType.SMS_SoftwareUpdate, sourceContainerID, destinationContainerID);
                        break;
                    default:
                        break;
                }

                ObjCount++;

                response.Publish("Number of Object Container Items", ObjCount);
            }
        }
    }
}

