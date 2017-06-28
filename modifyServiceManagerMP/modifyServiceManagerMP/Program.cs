using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace modifyServiceManagerMP
{
    public class mpItem { public string propertyQuery { get; set; } public string xPath { get; set; } }
    class Program
    {
        static string propertyPath = "$Context/Property[Type='CustomSystem_WorkItem_Activity_Library!System.WorkItem.Activity']/Status$";
        static mpItem[] runbookActivityTypeItemArray = new mpItem[] { new mpItem() { propertyQuery = "@TypeID='RunbookActivity!Microsoft.SystemCenter.Orchestrator.RunbookAutomationActivity.Projection'", xPath = "ManagementPack/Templates/ObjectTemplate" },
                                                                      new mpItem() { propertyQuery = "contains(@Path, 'CustomSystem_WorkItem_Activity_Library!System.WorkItem.Activity.ManualActivity')", xPath = "ManagementPack/Templates/ObjectTemplate/Object" },
                                                                      new mpItem() { propertyQuery = "contains(@Path, 'CustomMicrosoft_SystemCenter_Orchestrator!Microsoft.SystemCenter.Orchestrator.RunbookAutomationActivity')", xPath = "ManagementPack/Templates/ObjectTemplate/Object" } };
            
        static void Main(string[] args)
        {
            DirectoryInfo currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo modificationDirectory = new DirectoryInfo(string.Format("{0}\\{1}",currentDirectory.ToString(),"ModifiedMPs"));
            if(!modificationDirectory.Exists) { modificationDirectory.Create(); }

            IEnumerable<FileInfo> managementPacks = currentDirectory.EnumerateFiles("*.xml");

            foreach (FileInfo managementPack in managementPacks)
            {
                processManagementPack(modificationDirectory, managementPack);
            }
        }

        private static void processManagementPack(DirectoryInfo modificationDirectory, FileInfo managementPack)
        {
            XmlDocument propertyDocNode;
            XmlDocument exportFile;

            parseManagementPack(managementPack, out propertyDocNode, out exportFile);

            saveModifiedManagementPack(modificationDirectory, managementPack, exportFile);
        }

        private static void saveModifiedManagementPack(DirectoryInfo modificationDirectory, FileInfo managementPack, XmlDocument exportFile)
        {
            FileInfo saveFile = new FileInfo(string.Format("{0}\\{1}", modificationDirectory, managementPack.Name));
            if (saveFile.Exists) { saveFile.Delete(); }
            exportFile.Save(saveFile.FullName);
        }

        private static void parseManagementPack(FileInfo managementPack, out XmlDocument propertyDocNode, out XmlDocument exportFile)
        {
            propertyDocNode = new XmlDocument();
            propertyDocNode.LoadXml("<Property Path=\"$Context/Property[Type='CustomSystem_WorkItem_Activity_Library!System.WorkItem.Activity']/Status$\">$MPElement[Name='CustomSystem_WorkItem_Activity_Library!ActivityStatusEnum.Ready']$</Property>");
            exportFile = new XmlDocument();
            exportFile.Load(managementPack.ToString());

            foreach (mpItem runbookActivityTypeItem in runbookActivityTypeItemArray)
            {
                XmlNodeList objectTemplateList = exportFile.SelectNodes(String.Format("{0}[{1}]", runbookActivityTypeItem.xPath, runbookActivityTypeItem.propertyQuery));
                foreach (XmlNode objectTemplateNode in objectTemplateList)
                {
                    parseTemplateNode(propertyDocNode, exportFile, objectTemplateNode);
                }
            }
        }

        private static void parseTemplateNode(XmlDocument propertyDocNode, XmlDocument exportFile, XmlNode objectTemplateNode)
        {
            XmlNodeList pList = objectTemplateNode.SelectNodes("Property");

            bool found = false;
            foreach (XmlNode pNode in pList)
            {
                if (pNode.Attributes["Path"].Value.Equals(propertyPath))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                XmlNode refNode = exportFile.ImportNode(propertyDocNode.FirstChild, true);
                objectTemplateNode.PrependChild(refNode);
            }
        }
    }
}
