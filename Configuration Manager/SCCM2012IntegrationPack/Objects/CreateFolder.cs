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
    [Activity("Create Folder")]
    public class CreateFolder : IActivity
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
            String[] FolderTypeChoice = new String[] {  "SMS_Package",
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

            String[] SearchFolderChoice = new String[] { "True", "False" };

            String exampleSearchXML = "<SearchFolderDescription xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" Name=\"SMS_Package\">" + "\n" +
                                      "    <SearchFolderDescriptionItems>" + "\n" +
                                      "        <SearchFolderDescriptionItem PropertyName=\"PackageID\">" + "\n" +
                                      "            <SearchStrings>" + "\n" +
                                      "                <String>JBS00004</strings>" + "\n" +
                                      "            </SearchStrings>" + "\n" +
                                      "        </SearchFolderDescriptionItem>" + "\n" +
                                      "    </SearchFolderDescriptionItems>" + "\n" +
                                      "</SearchFolderDescription>";

            designer.AddInput("Folder Name").WithDefaultValue("Folder Name");
            designer.AddInput("Folder Type").WithDefaultValue("SMS_Package").WithListBrowser(FolderTypeChoice);
            designer.AddInput("Search Folder").WithDefaultValue("False").WithListBrowser(SearchFolderChoice).NotRequired();
            designer.AddInput("Search Folder Query").WithDefaultValue(exampleSearchXML).NotRequired();
            designer.AddInput("Parent Container ID").WithDefaultValue(1);
            designer.AddCorellatedData(typeof(ObjectContainerNode));
            designer.AddOutput("Number of Object Container Nodes");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;      

            String folderName = request.Inputs["Folder Name"].AsString();
            String folderType = request.Inputs["Folder Type"].AsString();
            
            String searchFolder = String.Empty;
            String searchXML = String.Empty;

            if (request.Inputs.Contains("Search Folder")) { searchFolder = request.Inputs["Search Folder"].AsString(); }
            if (request.Inputs.Contains("Search Folder Query")) { searchXML = request.Inputs["Search Folder Query"].AsString(); }
            
            int parentContainerID = request.Inputs["Parent Container ID"].AsInt32();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {  
                IResultObject col = null;

                switch (folderType)
                {
                    case "SMS_Package":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_Package, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_Package, parentContainerID); }
                        break;
                    case "SMS_Advertisement":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_Advertisement, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_Advertisement, parentContainerID); }
                        break;
                    case "SMS_Query":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_Query, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_Query, parentContainerID); }
                        break;
                    case "SMS_Report":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_Report, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_Report, parentContainerID); }
                        break;
                    case "SMS_MeteredProductRule":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_MeteredProductRule, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_MeteredProductRule, parentContainerID); }
                        break;
                    case "SMS_ConfigurationItem":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_ConfigurationItem, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_ConfigurationItem, parentContainerID); }
                        break;
                    case "SMS_OperatingSystemInstallPackage":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_OperatingSystemInstallPackage, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_OperatingSystemInstallPackage, parentContainerID); }
                        break;
                    case "SMS_StateMigration":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_StateMigration, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_StateMigration, parentContainerID); }
                        break;
                    case "SMS_ImagePackage": if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_ImagePackage, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_ImagePackage, parentContainerID); }
                        break;
                    case "SMS_BootImagePackage":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_BootImagePackage, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_BootImagePackage, parentContainerID); }
                        break;
                    case "SMS_TaskSequencePackage":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_TaskSequencePackage, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_TaskSequencePackage, parentContainerID); }
                        break;
                    case "SMS_DeviceSettingPackage":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_DeviceSettingPackage, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_DeviceSettingPackage, parentContainerID); }
                        break;
                    case "SMS_DriverPackage":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_DriverPackage, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_DriverPackage, parentContainerID); }
                        break;
                    case "SMS_Driver":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_Driver, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_Driver, parentContainerID); }
                        break;
                    case "SMS_SoftwareUpdate":
                        if (searchFolder.Equals("True") && !(searchXML.Equals(String.Empty))) { col = CM2012Interop.createSearchFolder(connection, folderName, CM2012Interop.folderType.SMS_SoftwareUpdate, parentContainerID, searchXML); }
                        else { col = CM2012Interop.createConsoleFolder(connection, folderName, CM2012Interop.folderType.SMS_SoftwareUpdate, parentContainerID); }
                        break;
                    default:
                        break;
                }

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));
                }
                response.Publish("Number of Object Container Nodes", ObjCount);
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

