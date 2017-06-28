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
    [Activity("Get Folder")]
    public class GetFolder : IActivity
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
            String[] FolderTypes = new String[] { "SMS_Package",
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

            designer.AddInput("Folder Type").WithListBrowser(FolderTypes).WithDefaultValue("SMS_Package");
            designer.AddInput("Filter").WithDefaultValue("Name LIKE '%'");
            designer.AddCorellatedData(typeof(ObjectContainerNode));
            designer.AddOutput("Number of Folders");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String folderType = request.Inputs["Folder Type"].AsString();
            String filter = request.Inputs["Filter"].AsString();
            
            switch (folderType)
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
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;
                col = CMInterop.getSCCMObject(connection, "SMS_ObjectContainerNode", filter);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Folders", ObjCount);
            }
        }
        private IEnumerable<ObjectContainerNode> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new ObjectContainerNode(obj);
            }
        }
    }
}

