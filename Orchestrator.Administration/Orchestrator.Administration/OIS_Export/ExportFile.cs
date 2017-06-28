using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.OIS_Export
{
    public class ExportFile
    {
        private XmlDocument _OISExport = new XmlDocument();
        private XmlDocument finalDocument = new XmlDocument();

        public XmlDocument OISExport
        {
          get { return _OISExport; }            
        }

        public ExportFile()
        {
            _OISExport.LoadXml(OISExportString.emptyXMLStructure);
        }
        public ExportFile(FileInfo exportPath)
        {
            if (exportPath.Exists)
            {
                _OISExport.Load(exportPath.FullName);
            }
            else
            {
                throw new Exception("Export File not found");
            }
        }
        #region linkModification
        public void modifyExportLinkApplyBestPractices()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNodeList runbookRootFolders = finalDocument.SelectNodes(StringValues.policiesFolderPath);

            if (runbookRootFolders != null)
            {
                foreach (XmlNode folderNode in runbookRootFolders)
                {
                    recurseLinkBestPractices(folderNode);
                }
            }

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }

        public void modifyExportLinkApplyColoringByXML(XmlNode rootNode)
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNodeList runbookRootFolders = finalDocument.SelectNodes(StringValues.policiesFolderPath);

            if (runbookRootFolders != null)
            {
                foreach (XmlNode folderNode in runbookRootFolders)
                {
                    recurseLinkBestPractices(folderNode);
                }
            }

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        #endregion
        #region Name Modifications
        public void modifyExportUpdateNameByIDRef(string guid, string newName)
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            //Look for Folder Node
            XmlNode refIDNode = finalDocument.SelectSingleNode(String.Format("//Folder[UniqueID=\"{0}\"]", guid));
            if (refIDNode != null) { refIDNode["Name"].InnerText = newName; }
            else
            {
                //Look for Policy Node
                refIDNode = finalDocument.SelectSingleNode(String.Format("//Policy[UniqueID=\"{0}\"]", guid));
                if (refIDNode != null) { refIDNode["Name"].InnerText = newName; }
                else
                {
                    //Look for Policy Node
                    refIDNode = finalDocument.SelectSingleNode(String.Format("//Object[UniqueID=\"{0}\"]", guid));
                    if (refIDNode != null) { refIDNode["Name"].InnerText = newName; }
                }
            }
            
            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void modifyExportChangeGlobalQIKConfigurationName(string oldName, string newName, string guid)
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNode runbookRootFolders = finalDocument.SelectSingleNode(StringValues.policiesFolderPath);

            // Get reference to global configurations
            XmlNode globalConfigNode = finalDocument.SelectSingleNode(StringValues.configurationNodePath);

            // Update Global Config Nodes
            if(globalConfigNode != null)
            {
                // Find oldConfig
                XmlNode gcEntryNode = globalConfigNode.SelectSingleNode(String.Format("Entry[ID=\"{0}\"]", guid));
                if (gcEntryNode != null)
                {
                    XmlNode gcEntryDataNode = gcEntryNode["Data"];
                    gcEntryDataNode.InnerXml = gcEntryDataNode.InnerText;

                    XmlNode gcEntryDataConfigNode = gcEntryDataNode.SelectSingleNode("Object/Configurations");
                    gcEntryDataConfigNode.InnerXml = gcEntryDataConfigNode.InnerText;

                    XmlNode nameNode = gcEntryDataConfigNode.SelectSingleNode(string.Format("Configurations/Configuration[Name=\"{0}\"]", oldName));
                    nameNode["Name"].InnerText = newName;

                    gcEntryDataConfigNode.InnerText = gcEntryDataConfigNode.InnerXml;

                    gcEntryDataNode.InnerText = gcEntryDataNode.InnerXml;
                }
            }

            // Get Reference to QIK pointers
            ArrayList referenceGUIDList = new ArrayList();
            foreach (XmlNode node in globalConfigNode.SelectNodes("Entry"))
            {
                if (node["Data"].InnerText.Contains(guid))
                {
                    referenceGUIDList.Add(node["ID"].InnerText);
                }
            }

            // Get Reference to all oldConfig in Runbooks
            if (runbookRootFolders != null)
            {
                foreach (string refGUID in referenceGUIDList)
                {
                    XmlNodeList allObjectRefNodes = runbookRootFolders.SelectNodes(String.Format("//Object[ObjectType=\"{0}\"]", refGUID));
                    //XmlNodeList allObjectRefNodes = runbookRootFolders.SelectNodes(String.Format("//Object[Configuration=\"{0}\"]", oldName));
                    foreach (XmlNode objectRefNode in allObjectRefNodes)
                    {
                        if (objectRefNode["Configuration"].InnerText.Equals(oldName)) { objectRefNode["Configuration"].InnerText = newName; }
                    }
                }
            }
            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void modifyExportChangeGlobalNativeConfigurationName(string oldName, string newName, string guid)
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNode runbookRootFolders = finalDocument.SelectSingleNode(StringValues.policiesFolderPath);

            // Get reference to global configurations
            XmlNode globalConfigNode = finalDocument.SelectSingleNode(StringValues.configurationNodePath);

            // Update Global Config Nodes
            if (globalConfigNode != null)
            {
                // Find oldConfig
                XmlNode gcEntryNode = globalConfigNode.SelectSingleNode(String.Format("Entry[ID=\"{0}\"]", guid));
                if (gcEntryNode != null)
                {
                    XmlNode gcEntryDataNode = gcEntryNode["Data"];
                    gcEntryDataNode.InnerXml = gcEntryDataNode.InnerText;

                    XmlNode gcEntryDataInner = gcEntryDataNode.FirstChild;
                    gcEntryDataInner.InnerXml = gcEntryDataInner.InnerText;

                    XmlNode nameNode = gcEntryDataInner.SelectSingleNode(string.Format("ItemRoot/Entry[Name=\"{0}\"]", oldName));
                    nameNode["Name"].InnerText = newName;

                    gcEntryDataInner.InnerText = gcEntryDataInner.InnerXml;
                    gcEntryDataNode.InnerText = gcEntryDataNode.InnerXml;
                }
            }

            // Get Reference to all oldConfig in Runbooks -- DANGEROUS
            if (runbookRootFolders != null)
            {

                XmlNodeList allObjectRefNodes = runbookRootFolders.SelectNodes(String.Format("//Object[Connection=\"{0}\"]", oldName));
                foreach (XmlNode objectRefNode in allObjectRefNodes)
                {
                    objectRefNode["Connection"].InnerText = newName;
                }
            }
            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        #endregion
        #region MaxParralelRequest Settings
        public void modifyExportSetMaxParallelRequestSettingNameBased()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNodeList runbookRootFolders = finalDocument.SelectNodes(StringValues.policiesFolderPath);

            if (runbookRootFolders != null)
            {
                foreach (XmlNode folderNode in runbookRootFolders)
                {
                    /*
                    string folderDescription = folderNode["Description"].InnerText;

                    string folderMaxParallelRequests = string.Empty;
                    try { folderMaxParallelRequests = folderDescription.Substring(folderDescription.IndexOf("MaxParallelRequests:")).Split(' ')[0].Split(':')[1]; }
                    catch { }

                    recurseMaxParallelRequestsDescription(folderNode, folderMaxParallelRequests);
                     */
                    recurseMaxParallelRequestsName(folderNode, string.Empty);
                }
            }

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void modifyExportSetMaxParallelRequestSettingDescriptionBased()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNodeList runbookRootFolders = finalDocument.SelectNodes(StringValues.policiesFolderPath);

            if (runbookRootFolders != null)
            {
                foreach (XmlNode folderNode in runbookRootFolders)
                {
                    string folderDescription = folderNode["Description"].InnerText;

                    string folderMaxParallelRequests = string.Empty;
                    try { folderMaxParallelRequests = folderDescription.Substring(folderDescription.IndexOf("MaxParallelRequests:")).Split(' ')[0].Split(':')[1]; }
                    catch { }

                    recurseMaxParallelRequestsDescription(folderNode, folderMaxParallelRequests);
                }
            }

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        #endregion
        #region Logging Configuration
        public void modifyObjectSpecificLogging(String ObjectSpecificLogging)
        {
            switch (ObjectSpecificLogging.ToLower())
            {
                case "on":
                    modifyRunbookLogging(StringValues.SpecificData, true);
                    break;
                case "off":
                    modifyRunbookLogging(StringValues.SpecificData, false);
                    break;
                default:
                    break;
            }
        }
        public void modifyGenericLogging(String GenericObjectLogging)
        {
            switch (GenericObjectLogging.ToLower())
            {
                case "on":
                    modifyRunbookLogging(StringValues.CommonData, true);
                    break;
                case "off":
                    modifyRunbookLogging(StringValues.CommonData, false);
                    break;
                default:
                    break;
            }
        }
        public void modifyRunbookLogging(string LoggingType, bool Enable)
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            // Get reference to all runbook folders
            XmlNodeList runbookRootFolders = finalDocument.SelectNodes(StringValues.policiesFolderPath);

            if (runbookRootFolders != null)
            {
                foreach (XmlNode folderNode in runbookRootFolders)
                {
                    recurseLogging(folderNode, LoggingType, Enable);
                }
            }

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        static void recurseLogging(XmlNode folder, string LoggingType, bool Enable)
        {
            // Process runbooks inside the folder
            XmlNodeList runbookNodes = folder.SelectNodes("Policy");
            if (runbookNodes != null)
            {
                // Runbooks found, process
                foreach (XmlNode runbookNode in runbookNodes)
                {
                    if (Enable)
                    {
                        runbookNode[LoggingType].Attributes["datatype"].Value = "bool";
                        runbookNode[LoggingType].InnerText = "TRUE";
                    }
                    else
                    {
                        runbookNode[LoggingType].Attributes["datatype"].Value = "bool";
                        runbookNode[LoggingType].InnerText = "FALSE";
                    }
                }
            }
            // Process Sub Folders

            XmlNodeList folderList = folder.SelectNodes("Folder");
            foreach (XmlNode subFolder in folderList)
            {
                recurseLogging(subFolder, LoggingType, Enable);
            }
        }
        private void recurseLinkBestPractices(XmlNode folder)
        {
            // Process runbooks inside the folder
            XmlNodeList runbookNodes = folder.SelectNodes("Policy");
            if (runbookNodes != null)
            {
                // Runbooks found, process
                foreach (XmlNode runbookNode in runbookNodes)
                {
                    // Look for Link Objects
                    XmlNodeList objectNodes = runbookNode.SelectNodes("Object[ObjectTypeName=\"Link\"]");
                    foreach (XmlNode objectNode in objectNodes)
                    {
                        bool nodeColorModified = false;

                        string sourceObjectID = objectNode["SourceObject"].InnerText;
                        XmlNode linkName = objectNode.SelectSingleNode("Name");
                        XmlNode color = objectNode.SelectSingleNode("Color");
                        XmlNode width = objectNode.SelectSingleNode("Width");
                        XmlNode delay = objectNode.SelectSingleNode("WaitDelay");

                        //Check if previous Object was Flattened
                        XmlNode sourceFlattenNode = finalDocument.SelectSingleNode(String.Format("//Object[UniqueID=\"{0}\"]/Flatten", sourceObjectID));
                        if (sourceFlattenNode != null)
                        {
                            if (sourceFlattenNode.InnerText != null)
                            {
                                if (sourceFlattenNode.InnerText.Equals("TRUE"))
                                {
                                    if (linkName.InnerText.Equals("Link")) { linkName.InnerText = "Flattened"; }
                                    else if (!linkName.InnerText.Contains("Flattened")) { linkName.InnerText = String.Format("Flattened:{0}", linkName.InnerText); }
                                }
                            }
                        }

                        // Check to see if there is are link Triggers defined
                        XmlNodeList LinkTriggers = objectNode.SelectNodes("TRIGGERS/Entry");
                        if (LinkTriggers != null)
                        {
                            XmlNode linkTriggerNode = LinkTriggers[0];
                            switch (LinkTriggers.Count)
                            {
                                case 0:
                                    //This means it is a normal 'success of previous object' link
                                    nodeColorModified = setLinkToSuccessSettings(nodeColorModified, linkName, color);
                                    break;
                                case 1:
                                    // check if link is based on previous object status
                                    string linkTriggerNodeCondition = linkTriggerNode["Condition"].InnerText;
                                    if (linkTriggerNodeCondition.Equals(string.Empty))
                                    {
                                        switch (linkTriggerNode["Value"].InnerText)
                                        {
                                            case ("success#warning#failed"):
                                                nodeColorModified = setLinkToAlwaysSettings(nodeColorModified, linkName, color, width);
                                                break;
                                            case ("success"):
                                                nodeColorModified = setLinkToSuccessSettings(nodeColorModified, linkName, color);
                                                break;
                                            case ("warning#failed"):
                                            case ("failed"):
                                                nodeColorModified = setLinkToFailureSettings(nodeColorModified, linkName, color);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        // Set it to a custom string
                                        String[] dataStrArray = linkTriggerNode["Data"].InnerText.Split('.');

                                        if (dataStrArray.Length > 1)
                                        {
                                            StringBuilder dataStringBuilder = new StringBuilder();
                                            for (int i = 1; i < dataStrArray.Length; i++)
                                            {
                                                dataStringBuilder.Append(dataStrArray[i] + ".");
                                            }
                                            String dataString = dataStringBuilder.ToString().Substring(0, dataStringBuilder.ToString().Length - 1);
                                            String valueString = linkTriggerNode["Value"].InnerText;

                                            String NameString = String.Format("{0} {1} {2}", dataString, linkTriggerNodeCondition, valueString);
                                            nodeColorModified = setLinkToCustomString(nodeColorModified, linkName, color, NameString);
                                        }
                                        else
                                        {
                                            nodeColorModified = setLinkToCustomCriteriaSettings(nodeColorModified, linkName, color);
                                        }
                                    }
                                    break;
                                default:
                                    // more than 1 link trigger defined, set custom criteria
                                    nodeColorModified = setLinkToCustomCriteriaSettings(nodeColorModified, linkName, color);
                                    break;
                            }
                        }
                         //Check Node Delay
                         if (!objectNode["WaitDelay"].InnerText.Equals("0"))
                         {
                             setLinkDelaySettings(nodeColorModified, linkName, color, delay);
                        }
                    }
                }
            }
            // Process Sub Folders

            XmlNodeList folderList = folder.SelectNodes("Folder");
            foreach (XmlNode subFolder in folderList)
            {
                recurseLinkBestPractices(subFolder);
            }
        }
        private void recurseLinkByXML(XmlNode folder, XmlNode ruleNode)
        {
            // Process runbooks inside the folder
            XmlNodeList runbookNodes = folder.SelectNodes("Policy");
            if (runbookNodes != null)
            {
                // Runbooks found, process
                foreach (XmlNode runbookNode in runbookNodes)
                {
                    // Look for Link Objects
                    XmlNodeList objectNodes = runbookNode.SelectNodes("Object[ObjectTypeName=\"Link\"]");
                    foreach (XmlNode objectNode in objectNodes)
                    {
                        string sourceObjectID = objectNode["SourceObject"].InnerText;
                        XmlNode sourceNode = finalDocument.SelectSingleNode(String.Format("//{0}", sourceObjectID));

                        string targetObjectID = objectNode["TargetObject"].InnerText;
                        XmlNode targetNode = finalDocument.SelectSingleNode(String.Format("//{0}", targetObjectID));

                        XmlNode linkName = objectNode.SelectSingleNode("Name");
                        XmlNode color = objectNode.SelectSingleNode("Color");

                        // Check to see if there is are link Triggers defined
                        XmlNodeList LinkTriggers = objectNode.SelectNodes("TRIGGERS/Entry");
                        switch (LinkTriggers.Count)
                        {
                            case 0:
                                //This means it is a normal 'success of previous object' link
                                break;
                            case 1:
                            default:
                                XmlNode linkTriggerNode = LinkTriggers[0];
                                
                                // check if link is based on previous object status
                                string linkTriggerNodeCondition = linkTriggerNode["Condition"].InnerText;
                                if (linkTriggerNodeCondition.Equals(string.Empty))
                                {
                                    
                                }
                                break;
                        }
                    }
                }
            }
            // Process Sub Folders

            XmlNodeList folderList = folder.SelectNodes("Folder");
            foreach (XmlNode subFolder in folderList)
            {
                recurseLinkByXML(subFolder, ruleNode);
            }
        }
        private void recurseMaxParallelRequestsName(XmlNode folder, string parentMaxParallelRequests)
        {
            string folderName = folder["Name"].InnerText;

            string folderMaxParallelRequests = parseMaxFolderParallelRequests(parentMaxParallelRequests, folderName);

            // Process runbooks inside the folder
            XmlNodeList runbookNodes = folder.SelectNodes("Policy");
            if (runbookNodes != null)
            {
                // Runbooks found, process
                foreach (XmlNode runbookNode in runbookNodes)
                {
                    string currentParrallelRequests = runbookNode["MaxParallelRequests"].InnerText;
                    string runbookName = runbookNode["Name"].InnerText;

                    if (runbookName.ToUpper().Replace(" ","").Contains("-MP:"))
                    {
                        string runbookMaxParallelRequests = string.Empty;
                        try 
                        { 
                            runbookMaxParallelRequests = runbookName.ToUpper().Replace(" ", "");
                            runbookMaxParallelRequests = runbookMaxParallelRequests.Substring(runbookMaxParallelRequests.IndexOf("-MP:")).Split(':')[1];
                        }
                        catch { }
                        if (!runbookMaxParallelRequests.Equals(string.Empty))
                        {
                            try
                            {
                                Convert.ToInt32(runbookMaxParallelRequests);
                                runbookNode["MaxParallelRequests"].InnerText = runbookMaxParallelRequests;
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(folderMaxParallelRequests);
                            runbookNode["MaxParallelRequests"].InnerText = folderMaxParallelRequests;
                        }
                        catch { }
                    }
                }
            }
            // Process Sub Folders

            XmlNodeList folderList = folder.SelectNodes("Folder");
            foreach (XmlNode subFolder in folderList)
            {
                recurseMaxParallelRequestsName(subFolder, folderMaxParallelRequests);
            }
        }

        private static string parseMaxFolderParallelRequests(string parentMaxParallelRequests, string folderName)
        {
            string folderMaxParallelRequests = string.Empty;
            try
            {
                folderMaxParallelRequests = folderName.ToUpper().Replace(" ", "");
                if (folderMaxParallelRequests.Contains("-MP:"))
                {
                    folderMaxParallelRequests = folderMaxParallelRequests.Substring(folderMaxParallelRequests.IndexOf("-MP:"));
                    folderMaxParallelRequests = folderMaxParallelRequests.Split(':')[1];
                }
                else
                {
                    folderMaxParallelRequests = parentMaxParallelRequests;
                }
            }
            catch { }

            if (folderMaxParallelRequests.Equals(string.Empty))
            {
                folderMaxParallelRequests = parentMaxParallelRequests;
            }
            return folderMaxParallelRequests;
        }
        private void recurseMaxParallelRequestsDescription(XmlNode folder, string parentMaxParallelRequests)
        {
            string folderDescription = folder["Description"].InnerText;
            
            string folderMaxParallelRequests = string.Empty;
            try { folderMaxParallelRequests = folderDescription.Substring(folderDescription.IndexOf("MaxParallelRequests:")).Split(' ')[0].Split(':')[1]; }
            catch { }
            
            if (folderMaxParallelRequests.Equals(string.Empty))
            {
                folderMaxParallelRequests = parentMaxParallelRequests;
            }

            // Process runbooks inside the folder
            XmlNodeList runbookNodes = folder.SelectNodes("Policy");
            if (runbookNodes != null)
            {
                // Runbooks found, process
                foreach (XmlNode runbookNode in runbookNodes)
                {
                    string currentParrallelRequests = runbookNode["MaxParallelRequests"].InnerText;
                    string runbookDescription = runbookNode["Description"].InnerText;

                    if (runbookDescription.Contains("MaxParallelRequests"))
                    {
                        string runbookMaxParallelRequests = string.Empty;
                        try { runbookMaxParallelRequests = runbookDescription.Substring(runbookDescription.IndexOf("MaxParallelRequests:")).Split(' ')[0].Split(':')[1]; }
                        catch { }
                        if (!runbookMaxParallelRequests.Equals(string.Empty))
                        {
                            try
                            {
                                Convert.ToInt32(runbookMaxParallelRequests);
                                runbookNode["MaxParallelRequests"].InnerText = runbookMaxParallelRequests;
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        try
                        {
                            Convert.ToInt32(folderMaxParallelRequests);
                            runbookNode["MaxParallelRequests"].InnerText = folderMaxParallelRequests;
                        }
                        catch { }
                    }
                }
            }
            // Process Sub Folders

            XmlNodeList folderList = folder.SelectNodes("Folder");
            foreach (XmlNode subFolder in folderList)
            {
                recurseMaxParallelRequestsDescription(subFolder, folderMaxParallelRequests);
            }
        }

        private static void setLinkDelaySettings(bool nodeColorModified, XmlNode linkName, XmlNode Color, XmlNode Delay)
        {
            if (nodeColorModified)
            {
                // Color not initially set
                if (!Color.InnerText.Equals("0"))
                {
                    nodeColorModified = true;
                    Color.InnerText = "16711680";
                }
                else
                {
                    nodeColorModified = false;
                }
            }
            if (linkName.InnerText.Equals("Link")) { linkName.InnerText = "Delayed"; }
            else if (!linkName.InnerText.Contains("Delayed")) { linkName.InnerText = String.Format("{0}:Delayed by {1}", linkName.InnerText, Delay.InnerText); }
        }

        private static bool setLinkToAlwaysSettings(bool nodeColorModified, XmlNode linkName, XmlNode Color, XmlNode width)
        {
            // Color not initially set
            if (Color.InnerText.Equals("0") || Color.InnerText.Equals("255") || Color.InnerText.Equals("33023") || Color.InnerText.Equals("18468")
                || Color.InnerText.Equals("32768") || Color.InnerText.Equals("16711680") || Color.InnerText.Equals("16711680"))
            {
                nodeColorModified = true;
                Color.InnerText = "18468";
                width.InnerText = "2";
            }
            else
            {
                nodeColorModified = false;
            }
            if (linkName.InnerText.Equals("Link")) { linkName.InnerText = ":Always:"; }
            else
            {
                if (linkName.InnerText.Contains(":Success")) { linkName.InnerText = linkName.InnerText.Replace(":Success", ":Always"); }
                else if (linkName.InnerText.Contains(":Failure")) { linkName.InnerText = linkName.InnerText.Replace(":Failure", ":Always"); }
                else if (linkName.InnerText.Contains(":CustomCriteria")) { linkName.InnerText = linkName.InnerText.Replace(":CustomCriteria", ":Always"); }
                else if (linkName.InnerText.Contains(":Always")) { }
                else { linkName.InnerText = String.Format("{0}:Always", linkName.InnerText); }
            }
            return nodeColorModified;
        }

        private static bool setLinkToSuccessSettings(bool nodeColorModified, XmlNode linkName, XmlNode Color)
        {
            // Color not initially set
            if (Color.InnerText.Equals("0") || Color.InnerText.Equals("255") || Color.InnerText.Equals("33023") || Color.InnerText.Equals("18468")
                || Color.InnerText.Equals("32768") || Color.InnerText.Equals("16711680") || Color.InnerText.Equals("16711680"))
            {
                nodeColorModified = true;
                Color.InnerText = "32768";
            }
            else
            {
                nodeColorModified = false;
            }
            if (linkName.InnerText.Equals("Link")) { linkName.InnerText = ":Success:"; }
            else
            {
                if (linkName.InnerText.Contains(":Always")) { linkName.InnerText = linkName.InnerText.Replace(":Always:", ":Success:"); }
                else if (linkName.InnerText.Contains(":Failure")) { linkName.InnerText = linkName.InnerText.Replace(":Failure:", ":Success:"); }
                else if (linkName.InnerText.Contains(":CustomCriteria")) { linkName.InnerText = linkName.InnerText.Replace(":CustomCriteria:", ":Success:"); }
                else if (linkName.InnerText.Contains(":Success")) { }
                else { linkName.InnerText = String.Format("{0}:Success", linkName.InnerText); }
            }
            return nodeColorModified;
        }

        private static bool setLinkToFailureSettings(bool nodeColorModified, XmlNode linkName, XmlNode Color)
        {
            // Color not initially set
            if (Color.InnerText.Equals("0") || Color.InnerText.Equals("255") || Color.InnerText.Equals("33023") || Color.InnerText.Equals("18468")
                || Color.InnerText.Equals("32768") || Color.InnerText.Equals("16711680") || Color.InnerText.Equals("16711680"))
            {
                nodeColorModified = true;
                Color.InnerText = "255";
            }
            else
            {
                nodeColorModified = false;
            }
            if (linkName.InnerText.Equals("Link")) { linkName.InnerText = ":Failure:"; }
            else
            {
                if (linkName.InnerText.Contains(":Always:")) { linkName.InnerText = linkName.InnerText.Replace(":Always:", ":Failure:"); }
                else if (linkName.InnerText.Contains(":Success:")) { linkName.InnerText = linkName.InnerText.Replace(":Success:", ":Failure:"); }
                else if (linkName.InnerText.Contains(":CustomCriteria:")) { linkName.InnerText = linkName.InnerText.Replace(":CustomCriteria:", ":Failure:"); }
                else if (linkName.InnerText.Contains(":Failure:")) { }
                else { linkName.InnerText = String.Format("{0}:Failure:", linkName.InnerText); }
            }
            return nodeColorModified;
        }
        
        private static bool setLinkToCustomCriteriaSettings(bool nodeColorModified, XmlNode linkName, XmlNode Color)
        {
            // Color not initially set or set by us
            if (Color.InnerText.Equals("0") || Color.InnerText.Equals("255") || Color.InnerText.Equals("33023") || Color.InnerText.Equals("18468")
                || Color.InnerText.Equals("32768") || Color.InnerText.Equals("16711680") || Color.InnerText.Equals("16711680"))
            {
                nodeColorModified = true;
                Color.InnerText = "33023";
            }
            else
            {
                nodeColorModified = false;
            }

            if (linkName.InnerText.Equals("Link")) { linkName.InnerText = ":CustomCriteria:"; }
            else
            {
                if (linkName.InnerText.Contains(":Always:")) { linkName.InnerText = linkName.InnerText.Replace("Always", ":CustomCriteria:"); }
                else if (linkName.InnerText.Contains(":Success:")) { linkName.InnerText = linkName.InnerText.Replace("Success", ":CustomCriteria:"); }
                else if (linkName.InnerText.Contains(":Failure:")) { linkName.InnerText = linkName.InnerText.Replace("Failure", ":CustomCriteria:"); }
                else if (linkName.InnerText.Contains(":CustomCriteria:")) { }
                else { linkName.InnerText = String.Format("{0}:CustomCriteria:", linkName.InnerText); }
            }
            return nodeColorModified;
        }
        
        private static bool setLinkToCustomString(bool nodeColorModified, XmlNode linkName, XmlNode Color, String NameString)
        {
            // Color not initially set
            if (Color.InnerText.Equals("0") || Color.InnerText.Equals("255") || Color.InnerText.Equals("33023") || Color.InnerText.Equals("18468")
                || Color.InnerText.Equals("32768") || Color.InnerText.Equals("16711680") || Color.InnerText.Equals("16711680"))
            {
                nodeColorModified = true;
                Color.InnerText = "33023";
            }
            else
            {
                nodeColorModified = false;
            }
            
            if (linkName.InnerText.Equals("Link")) { linkName.InnerText = String.Format(":{0}:", NameString); }
            else
            {
                if (linkName.InnerText.Contains(":Always")) { linkName.InnerText = linkName.InnerText.Replace("Always", String.Format(":{0}:", NameString)); }
                else if (linkName.InnerText.Contains(":Success")) { linkName.InnerText = linkName.InnerText.Replace("Success", String.Format(":{0}:", NameString)); }
                else if (linkName.InnerText.Contains(":Failure")) { linkName.InnerText = linkName.InnerText.Replace("Failure", String.Format(":{0}:", NameString)); }
                else if (linkName.InnerText.Contains(":CustomCriteria")) { linkName.InnerText = linkName.InnerText.Replace("CustomCriteria", String.Format(":{0}:", NameString)); }
                else { linkName.InnerText = String.Format("{0}:{1}:", linkName.InnerText, NameString); }
            }
            return nodeColorModified;
        }
        #endregion
        #region public node clean functions
        public void cleanGlobalConfigurations()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);
            setupFinalConfigNode();
            XmlNode rootConfigNode = _OISExport.SelectSingleNode(StringValues.configurationNodePath);

            cleanGlobalConfigurations(rootConfigNode);
            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void cleanGlobalCountersNode()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            setupFinalDocNode(StringValues.countersNodePath);
            processGlobalSettingsNode(StringValues.countersNodePath, finalDocument.SelectSingleNode(StringValues.countersNodePath), globalType.Counter);

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void cleanGlobalVariablesNode()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            setupFinalDocNode(StringValues.variablesNodePath);
            processGlobalSettingsNode(StringValues.variablesNodePath, finalDocument.SelectSingleNode(StringValues.variablesNodePath), globalType.Variable);

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void cleanGlobalSchedulesNode()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            setupFinalDocNode(StringValues.schedulesNodePath);
            processGlobalSettingsNode(StringValues.schedulesNodePath, finalDocument.SelectSingleNode(StringValues.schedulesNodePath), globalType.Schedule);

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        public void cleanGlobalComputerGroupsNode()
        {
            finalDocument = (XmlDocument)_OISExport.CloneNode(true);

            setupFinalDocNode(StringValues.computerGroupsNodePath);
            processGlobalSettingsNode(StringValues.computerGroupsNodePath, finalDocument.SelectSingleNode(StringValues.computerGroupsNodePath), globalType.ComputerGroup);

            _OISExport = (XmlDocument)finalDocument.CloneNode(true);
        }
        #endregion
        #region private methods
        private void importFile(bool runbooks, bool variables, bool schedules, bool counters, bool computerGroups, bool globalConfigs, bool overwriteExisting, string dbServer, string dbName, COMInterop scorchInterop, Dictionary<Guid, Guid> guidMap)
        {
            // Process Counters
            if (counters)
            {
                XmlNode inputCountersNode = _OISExport.SelectSingleNode(StringValues.countersNodePath);
                if (inputCountersNode != null)
                {
                    //scorchInterop.ImportSCOGlobalSettingsNode(ResourceFolderRoot.Counters, ResourceFolderRoot.Counters, inputCountersNode, overwriteExisting, dbServer, dbName);
                }
            }
            // Process Variables
            if (variables)
            {
                XmlNode variablesNode = _OISExport.SelectSingleNode(StringValues.variablesNodePath);
                if (variablesNode != null)
                {
                    //scorchInterop.ImportSCOGlobalSettingsNode(ResourceFolderRoot.Variables, ResourceFolderRoot.Variables, variablesNode, overwriteExisting, dbServer, dbName);
                }
            }

            // Process Schedules
            if (schedules)
            {
                XmlNode schedulesNode = _OISExport.SelectSingleNode(StringValues.schedulesNodePath);
                if (schedulesNode != null)
                {
                    //scorchInterop.ImportSCOGlobalSettingsNode(ResourceFolderRoot.Schedules, ResourceFolderRoot.Schedules, schedulesNode, overwriteExisting, dbServer, dbName);
                }
            }

            // Process Computer Groups
            if (computerGroups)
            {
                XmlNode computerGroupsNode = _OISExport.SelectSingleNode(StringValues.computerGroupsNodePath);
                if (computerGroupsNode != null)
                {
                    //scorchInterop.ImportSCOGlobalSettingsNode(ResourceFolderRoot.Computers, ResourceFolderRoot.Computers, computerGroupsNode, overwriteExisting, dbServer, dbName);
                }
            }

            // Process Global Configurations
            if (globalConfigs)
            {

            }

            // Process Runbooks
            if (runbooks)
            {
                XmlNode runbooksNode = _OISExport.SelectSingleNode(StringValues.policiesFolderPath);
                if (runbooksNode != null)
                {
                    //scorchInterop.(ResourceFolderRoot.Counters, ResourceFolderRoot.Counters, inputCountersNode, overwriteExisting, dbServer, dbName);
                }
            }
        }
        private Dictionary<Guid, Guid> processNodeGuids(bool runbooks, bool variables, bool schedules, bool counters, bool computerGroups, bool globalConfigs, string dbServer, string dbName, COMInterop scorchInterop, ref Dictionary<Guid, Guid> guidMap)
        {
            // Process Counters
            if (counters)
            {
                XmlNode inputCountersNode = _OISExport.SelectSingleNode(StringValues.countersNodePath);
                if (inputCountersNode != null)
                {
                    parseNode(objectTypes.Counter, inputCountersNode, ref guidMap, scorchInterop, dbName, dbServer);
                }
            }
            // Process Variables
            if (variables)
            {
                XmlNode variablesNode = _OISExport.SelectSingleNode(StringValues.variablesNodePath);
                if (variablesNode != null)
                {
                    parseNode(objectTypes.Variable, variablesNode, ref guidMap, scorchInterop, dbName, dbServer);
                }
            }

            // Process Schedules
            if (schedules)
            {
                XmlNode schedulesNode = _OISExport.SelectSingleNode(StringValues.schedulesNodePath);
                if (schedulesNode != null)
                {
                    parseNode(objectTypes.Schedule, schedulesNode, ref guidMap, scorchInterop, dbName, dbServer);
                }
            }

            // Process Computer Groups
            if (computerGroups)
            {
                XmlNode computerGroupsNode = _OISExport.SelectSingleNode(StringValues.computerGroupsNodePath);
                if (computerGroupsNode != null)
                {
                    parseNode(objectTypes.ComputerGroups, computerGroupsNode, ref guidMap, scorchInterop, dbName, dbServer);
                }
            }

            // Process Global Configurations
            if (globalConfigs)
            {

            }

            // Process Runbooks
            if (runbooks)
            {
                XmlNode runbooksNode = _OISExport.SelectSingleNode(StringValues.policiesFolderPath);
                if (runbooksNode != null)
                {
                    parseNode(objectTypes.Runbook, runbooksNode, ref guidMap, scorchInterop, dbName, dbServer);
                }
            }
            return guidMap;
        }
        private void parseNode(objectTypes objectType, XmlNode currentNode, ref Dictionary<Guid, Guid> guidMap, COMInterop scorchInterop, string dbName, string dbServer)
        {
            // Select current object in folder and parse

            switch (objectType)
            {
                default:
                case objectTypes.Resource:
                case objectTypes.Activity:
                    break;
                case objectTypes.Counter:
                case objectTypes.Schedule:
                case objectTypes.Variable:
                case objectTypes.ComputerGroups:
                    XmlNodeList objectList = currentNode.SelectNodes("Objects/Object");
                    string parentID = currentNode.SelectSingleNode("UniqueID").InnerText.ToString();
                    foreach (XmlNode objectNode in objectList)
                    {
                        string objectID = objectNode["UniqueID"].InnerText.ToString();
                        string objectName = objectNode["Name"].InnerText.ToString();
                        foundStai foundStatus = scorchInterop.CompareSCOObjects(new Guid(parentID), new Guid(objectID), objectName, objectType, dbName, dbServer);
                        switch (foundStatus)
                        {
                            case foundStai.foundGuid:
                                guidMap.Add(new Guid(objectID), Guid.NewGuid());
                                break;
                            case foundStai.foundName:
                            case foundStai.notFound:
                            default:
                                break;
                        }
                    }
                    break;
                case objectTypes.Runbook:
                    XmlNodeList policyList = currentNode.SelectNodes("Policy");
                    foreach (XmlNode policyNode in policyList)
                    {
                        string policyID = policyNode["UniqueID"].InnerText.ToString();
                        string policyName = policyNode["Name"].InnerText.ToString();
                        string policyParentID = policyNode["ParentID"].InnerText.ToString();
                        foundStai foundStatus = scorchInterop.CompareSCOObjects(new Guid(policyParentID), new Guid(policyID), policyName, objectType, dbName, dbServer);
                        switch (foundStatus)
                        {
                            case foundStai.foundGuid:
                                guidMap.Add(new Guid(policyID), Guid.NewGuid());
                                break;
                            case foundStai.foundName:
                            case foundStai.notFound:
                            default:
                                break;
                        }
                    }
                    break;
            }
            // Select Child Folders and recurse
            XmlNodeList subFolders = currentNode.SelectNodes("Folder");
            foreach (XmlNode folder in subFolders)
            {
                string folderID = folder["UniqueID"].InnerText.ToString();
                deletionStati deletionStati = scorchInterop.GetSCOObjectExistence(new Guid(folderID), objectTypes.Folder, dbName, dbServer);
                switch (deletionStati)
                {
                    case deletionStati.Active:
                        guidMap.Add(new Guid(folderID), Guid.NewGuid());
                        break;
                    default:
                        break;
                }
                parseNode(objectType, folder, ref guidMap, scorchInterop, dbName, dbServer);
            }
        }
        private string ConvertToSQLGuid(string guid)
        {
            if (CheckGUID(guid))
            {
                if (guid.Contains("{"))
                {
                    guid = guid.Remove('{');
                    guid = guid.Remove('}');
                }
            }
            else
            {
                throw new Exception(String.Format("{0} is an invalid GUID", guid));
            }
            return guid;
        }
        private string ConvertToGuid(string guid)
        {
            if (!CheckGUID(guid))
            {
                throw new Exception(String.Format("{0} is an invalid GUID", guid));
            }
            else
            {
                if (!guid.Contains("{"))
                {
                    guid = String.Format("{{0}}", guid);
                }
            }
            return guid;
        }
        private bool CheckGUID(string guid)
        {
            string pattern = @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";
            
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(guid);

            return m.Success;            
        }
        private void processGlobalSettingsNode(String globalSettingsPath, XmlNode finalNode, globalType global)
        {
            XmlNode rootGlobalSettingsNode = _OISExport.SelectSingleNode(globalSettingsPath);

            processBaseObjects(rootGlobalSettingsNode, finalNode, global);
            processBaseFolders(rootGlobalSettingsNode, finalNode, global);
        }
        private void processBaseFolders(XmlNode rootNode, XmlNode finalNode, globalType global)
        {
            // Process base Folders
            XmlNodeList baseFolderList = rootNode.SelectNodes("Folder");
            foreach (XmlNode baseFolder in baseFolderList)
            {
                XmlNode tempBaseFolder = processGenericFolderNode(baseFolder, global);

                if (tempBaseFolder != null)
                {
                    // Check if child node has ANY objects nodes under it, if so add it
                    if (tempBaseFolder.SelectNodes("//Object").Count > 0)
                    {
                        XmlNode importPointerNode = finalNode.OwnerDocument.ImportNode(tempBaseFolder, true);
                        finalNode.AppendChild(importPointerNode);
                    }
                }
            }
        }
        private XmlNode processGenericFolderNode(XmlNode folderNode, globalType global)
        {
            XmlNode returnNode = folderNode.CloneNode(true);

            processGenericObjects(returnNode, global);

            return processGenericFolder(returnNode, global);
        }
        private XmlNode processGenericFolder(XmlNode returnNode, globalType global)
        {
            // Process child folders
            XmlNodeList folderNodes = returnNode.SelectNodes("Folder");
            if (folderNodes != null)
            {
                for (int i = folderNodes.Count - 1; i >= 0; i--)
                {
                    returnNode.RemoveChild(folderNodes[i]);
                    XmlNode possibleAddNode = processGenericFolderNode(folderNodes[i], global);

                    // Check if child node has ANY objects nodes under it, if so add it
                    if (possibleAddNode.SelectNodes("//Object").Count > 0)
                    {
                        returnNode.AppendChild(possibleAddNode);
                    }
                }
            }

            return returnNode;
        }
        private void processGenericObjects(XmlNode returnNode, globalType global)
        {
            // Process Generic Objects in folder
            XmlNode objectNodeRoot = returnNode["Objects"];
            if (objectNodeRoot != null)
            {
                XmlNodeList objectNodeList = objectNodeRoot.ChildNodes;
                for (int i = objectNodeList.Count - 1; i >= 0; i--)
                {
                    // Check each folder object for reference, if not referenced remove
                    String objectID = objectNodeList[i]["UniqueID"].InnerText;
                    int runbookRefCount = countRunbookGuidReferences(objectID, global);
                    if (runbookRefCount == 0)
                    {
                        objectNodeRoot.RemoveChild(objectNodeList[i]);
                    }
                    else
                    {
                    }
                }
            }
        }
        private void processBaseObjects(XmlNode rootNode, XmlNode finalNode, globalType global)
        {
            // Process base Objects
            XmlNode rootObjectsNode = rootNode["Objects"];
            if (rootObjectsNode != null)
            {
                foreach (XmlNode rootObjectNode in rootObjectsNode.ChildNodes)
                {
                    String counterID = rootObjectNode["UniqueID"].InnerText;

                    int refNumber = countRunbookGuidReferences(counterID, global);
                    if (refNumber > 0)
                    {
                        // If base counter is referenced add it to final xml
                        XmlNode importPointerNode = finalNode.OwnerDocument.ImportNode(rootObjectNode, true);
                        finalNode["Objects"].AppendChild(importPointerNode);
                    }
                    else
                    {
                    }
                }
            }
        }
        private void setupFinalConfigNode()
        {
            XmlNode finalConfigNode = finalDocument.SelectSingleNode(StringValues.configurationNodePath);
            if (finalConfigNode != null)
            {
                finalConfigNode.RemoveAll();
            }
        }
        private void setupFinalDocNode(String nodePath)
        {
            XmlNode targetNode = finalDocument.SelectSingleNode(nodePath);
            if (targetNode != null)
            {
                XmlNode objectNode = targetNode["Objects"];
                if (objectNode != null)
                {
                    objectNode.RemoveAll();
                }

                XmlNodeList subFolders = targetNode.SelectNodes("Folder");
                for (int i = subFolders.Count - 1; i >= 0; i--)
                {
                    targetNode.RemoveChild(subFolders[i]);
                }
            }
        }
        private void cleanGlobalConfigurations(XmlNode rootConfigNode)
        {
            Dictionary<string, XmlNode> QIKPointerNodes = new Dictionary<string, XmlNode>();
            Dictionary<string, XmlNode> SavedQIKPointerNodes = new Dictionary<string, XmlNode>();
            Dictionary<string, XmlNode> ConfigurationNodes = new Dictionary<string, XmlNode>();
            Dictionary<string, XmlNode> QIKConfigurationNodes = new Dictionary<string, XmlNode>();
            if (rootConfigNode != null)
            {
                // Load all entries into a dictionary for easier reference searching
                foreach (XmlNode configNode in rootConfigNode.SelectNodes("Entry"))
                {
                    if (configNode["Data"].InnerText.Contains("QIKObject"))
                    {
                        QIKPointerNodes.Add(configNode["ID"].InnerText, configNode.CloneNode(true));
                    }
                    else
                    {
                        ConfigurationNodes.Add(configNode["ID"].InnerText, configNode.CloneNode(true));
                    }
                }

                // Process all QIK Pointer Nodes
                foreach (string QIKPointerNodeID in QIKPointerNodes.Keys)
                {
                    XmlNode QIKPointerNode = QIKPointerNodes[QIKPointerNodeID];

                    XmlNode QIKPointerDataNode = QIKPointerNode["Data"];
                    // Find Reference Configuration Node, add to QIK Based References and Remove from Generic
                    QIKPointerDataNode.InnerXml = QIKPointerDataNode.InnerText;
                    
                    // Search for the referenced ConfigurationID
                    String QIKreferenceConfigID = QIKPointerNode.SelectSingleNode("//ConfigurationId").InnerText;
                    QIKPointerDataNode.InnerText = QIKPointerDataNode.InnerXml;

                    if (ConfigurationNodes.ContainsKey(QIKreferenceConfigID))
                    {
                        if (!QIKConfigurationNodes.ContainsKey(QIKreferenceConfigID)) { QIKConfigurationNodes.Add(QIKreferenceConfigID, ConfigurationNodes[QIKreferenceConfigID]); }
                        ConfigurationNodes.Remove(QIKreferenceConfigID);
                    }

                    // Count number of references in export runbooks
                    int numberOfReferences = countRunbookGuidReferences(QIKPointerNodeID, globalType.GlobalConfig);

                    // If this node is referenced save it and the reference configuration
                    if (numberOfReferences > 0)
                    {
                        saveConfigNode(QIKPointerNode);
                        if (!SavedQIKPointerNodes.ContainsKey(QIKPointerNodeID)) { SavedQIKPointerNodes.Add(QIKPointerNodeID, QIKPointerNode); }
                    }
                }

                // Save all referenced QIK Configuration Data
                foreach (string QIKreferenceConfigID in QIKConfigurationNodes.Keys)
                {
                    ArrayList referencedNames = new ArrayList();
                    XmlNode configurationNode = QIKConfigurationNodes[QIKreferenceConfigID];
                    XmlNode refDataNode = configurationNode.SelectSingleNode("Data");

                    bool save = false;
                    // Find each pointer config that references this and was saved
                    foreach (XmlNode QIKPointerNode in SavedQIKPointerNodes.Values)
                    {
                        if (QIKPointerNode.InnerXml.Contains(QIKreferenceConfigID))
                        {
                            // Find the Name Reference
                            XmlNodeList objectRefNodeList = _OISExport.SelectNodes(string.Format("//Object[ObjectType=\"{0}\"]", QIKPointerNode["ID"].InnerText));
                            foreach (XmlNode objectRefNode in objectRefNodeList)
                            {
                                if (!referencedNames.Contains(objectRefNode["Configuration"].InnerText)) { referencedNames.Add(objectRefNode["Configuration"].InnerText); }
                            }
                            save = true;
                        }
                    }

                    if (save)
                    {
                        // Get the Config Object's Data and convert to XML
                        refDataNode.InnerXml = refDataNode.InnerText;

                        // Get the Configurations node and convert to XML
                        XmlNode refConfigDataNode = refDataNode.SelectSingleNode("//Configurations");
                        if (refConfigDataNode != null)
                        {
                            refConfigDataNode.InnerXml = refConfigDataNode.InnerText;

                            // Get all individual Configurations
                            XmlNodeList configurationNameList = refConfigDataNode.SelectNodes("//Configuration/Name");

                            // Loop through all referenced Names and Remove all non-referenced instances
                            for (int i = configurationNameList.Count - 1; i >= 0; i--)
                            {
                                XmlNode pNode = configurationNameList[i].ParentNode;
                                if (!referencedNames.Contains(configurationNameList[i].InnerText))
                                {
                                    refConfigDataNode.FirstChild.RemoveChild(pNode);
                                }
                            }
                            // Rebuild node for import
                            refConfigDataNode.InnerText = refConfigDataNode.InnerXml;
                        }
                        refDataNode.InnerText = refDataNode.InnerXml;
                        saveConfigNode(configurationNode);
                    }
                }

                // Process Native References
                // Native References not supported! Done 'Best Effort'
                
                foreach (XmlNode nativeConfigurationNode in ConfigurationNodes.Values)
                {
                    XmlNode nativeConfigurationDataNode = nativeConfigurationNode["Data"];
                    nativeConfigurationDataNode.InnerXml = nativeConfigurationDataNode.InnerText;

                    XmlNode nativeConfigurationDataNodeConfigNode = nativeConfigurationDataNode["Object"].FirstChild;
                    nativeConfigurationDataNodeConfigNode.InnerXml = nativeConfigurationDataNodeConfigNode.InnerText;
                    bool save = false;

                    if (nativeConfigurationDataNodeConfigNode != null)
                    {
                        XmlNode nativeConfigurationDataNodeConfigNodeFistChild = nativeConfigurationDataNodeConfigNode.FirstChild;
                        if (nativeConfigurationDataNodeConfigNodeFistChild != null)
                        {
                            if (nativeConfigurationDataNodeConfigNode.FirstChild.Name.Equals("ItemRoot"))
                            {
                                List<XmlNode> saveNodes = new List<XmlNode>();
                                XmlNodeList entryList = nativeConfigurationDataNodeConfigNode.FirstChild.ChildNodes;
                                foreach (XmlNode entry in entryList)
                                {
                                    string name;
                                    XmlNode nameNode = entry.SelectSingleNode("Name");
                                    if (nameNode != null)
                                    {
                                        name = entry["Name"].InnerText;
                                    }
                                    else
                                    {
                                        StringBuilder nameBuilder = new StringBuilder();
                                        bool first = true;
                                        foreach (XmlNode child in entry.ChildNodes)
                                        {
                                            if (!child.InnerText.Contains("\\`d.T.~De/"))
                                            {
                                                if (first) { nameBuilder.Append(child.InnerText); first = false; }
                                                else { nameBuilder.Append(String.Format(":{0}", child.InnerText)); }
                                            }
                                        }

                                        name = nameBuilder.ToString();
                                    }
                                    XmlNodeList referenceNodeList = _OISExport.SelectNodes(String.Format("//Object[Connection=\"{0}\"]", name));
                                    if (referenceNodeList.Count > 0)
                                    {
                                        saveNodes.Add(entry);
                                    }
                                }
                                if (saveNodes.Count > 0)
                                {
                                    save = true;
                                    XmlNode firstChildRef = nativeConfigurationDataNodeConfigNode.FirstChild;
                                    firstChildRef.RemoveAll();
                                    foreach (XmlNode saveNode in saveNodes)
                                    {
                                        firstChildRef.AppendChild(saveNode);
                                    }
                                }
                            }
                        }
                    }
                    nativeConfigurationDataNodeConfigNode.InnerText = nativeConfigurationDataNodeConfigNode.InnerXml;
                    nativeConfigurationDataNode.InnerText = nativeConfigurationDataNode.InnerXml;
                    if (save) { saveConfigNode(nativeConfigurationNode); }
                }
            }
        }
        private int countRunbookGuidReferences(string testGUID, globalType global)
        {
            int refCount = 0;
            XmlNodeList tempList = null;
            switch (global)
            {
                case globalType.GlobalConfig:
                    tempList = _OISExport.SelectNodes(string.Format("//Policy/Object[ObjectType=\"{0}\"]", testGUID));
                    if (tempList != null) { refCount = tempList.Count; }
                    break;
                case globalType.Schedule:
                    tempList = _OISExport.SelectNodes(string.Format("//Policy/Object[ScheduleTemplateID=\"{0}\"]", testGUID));
                    if (tempList != null) { refCount = tempList.Count; }
                    break;
                case globalType.Counter:
                    tempList = _OISExport.SelectNodes(string.Format("//Policy/Object[CounterID=\"{0}\"]", testGUID));
                    if (tempList != null) { refCount = tempList.Count; }
                    break;
                case globalType.ComputerGroup:
                    tempList = _OISExport.SelectNodes("//Policy");
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (tempList[i].InnerText.Contains(String.Format("\\`d.T.~Cg/{0}\\`d.T.~Cg/", testGUID)))
                        {
                            refCount++;
                            break;
                        }
                    }
                    break;
                case globalType.Variable:
                    tempList = _OISExport.SelectNodes("//Policy");
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (tempList[i].InnerText.Contains(String.Format("\\`d.T.~Vb/{0}\\`d.T.~Vb/", testGUID)))
                        {
                            refCount++;
                            break;
                        }
                    }
                    break;
            }
            return refCount;
        }
        private void saveConfigNode(XmlNode configNode)
        {
            XmlNode finalConfigNode = finalDocument.SelectSingleNode(StringValues.configurationNodePath);
            // Add pointer node to final config node base
            XmlNode importPointerNode = finalConfigNode.OwnerDocument.ImportNode(configNode, true);
            finalConfigNode.AppendChild(importPointerNode);
        }
        private bool checkNewConfig(String referenceConfigID)
        {
            XmlNode finalConfigNode = finalDocument.SelectSingleNode(StringValues.configurationNodePath);
            // Search through the final config node and ensure this reference config is not already added
            bool notContained = true;
            XmlNodeList configIDNodes = finalConfigNode.SelectNodes("//ID");
            {
                foreach (XmlNode idNode in configIDNodes)
                {
                    if (idNode.InnerText.Equals(referenceConfigID))
                    {
                        notContained = false;
                        break;
                    }
                }
            }
            return notContained;
        }
        private void LoadFolderPolicyExports(COMInterop scorchInterop, string folderID, XmlNode currentPolicyNode)
        {
            XmlDocument policyDocument = scorchInterop.GetSCOFolderContents(folderID);
            XmlNodeList policyNodes = policyDocument.SelectNodes("FolderContents/Policy");
            foreach (XmlNode policyNode in policyNodes)
            {
                currentPolicyNode.AppendChild(_OISExport.ImportNode(policyNode, true));
            }

            XmlDocument subFolderDoc = scorchInterop.GetSCOSubFolders(folderID);
            XmlNodeList folderNodes = subFolderDoc.SelectNodes("Folders/Folder");
            for (int i = 0; i < folderNodes.Count; i++)
            {
                XmlNode folder = folderNodes[i];
            }
            foreach (XmlNode folder in folderNodes)
            {
                cleanFolderNode(folder);

                XmlNode UniqueID = folder["UniqueID"];

                XmlNode importNode = _OISExport.ImportNode(folder, true);
                currentPolicyNode.AppendChild(importNode);

                LoadFolderPolicyExports(scorchInterop, UniqueID.InnerText, importNode);
            }
        }
        private void cleanFolderNode(XmlNode folder)
        {
            XmlNode parentID = folder["ParentID"];
            XmlNode TimeCreated = folder["TimeCreated"];
            XmlNode CreatedBy = folder["CreatedBy"];
            XmlNode LastModified = folder["LastModified"];
            XmlNode LastModifiedBy = folder["LastModifiedBy"];
            XmlNode Disabled = folder["Disabled"];

            if (parentID != null) { folder.RemoveChild(parentID); }
            if (TimeCreated != null) { folder.RemoveChild(TimeCreated); }
            if (CreatedBy != null) { folder.RemoveChild(CreatedBy); }
            if (LastModified != null) { folder.RemoveChild(LastModified); }
            if (LastModifiedBy != null) { folder.RemoveChild(LastModifiedBy); }
            if (Disabled != null) { folder.RemoveChild(Disabled); }
        }
        private XmlNode LoadBaseFolderInfo(COMInterop scorchInterop, string targetFolderGUID)
        {
            XmlDocument baseDoc = scorchInterop.GetSCOSubFolders(ResourceFolderRoot.Runbooks);

            foreach (XmlNode idNode in baseDoc.SelectNodes("Folders/Folder/UniqueID"))
            {
                if (idNode.InnerText.Equals(targetFolderGUID))
                {
                    XmlNode retNode = idNode.ParentNode;
                    cleanFolderNode(retNode);
                    return retNode;
                }
            }

            
            foreach (XmlNode subFolder in baseDoc.SelectNodes("Folders/Folder"))
            {
                XmlNode folderNode = LoadBaseFolderInfo(scorchInterop, targetFolderGUID, subFolder["UniqueID"].InnerText);
                if (folderNode != null)
                {
                    XmlNode retNode = folderNode;
                    cleanFolderNode(retNode);
                    return retNode;
                }
            }

            return null;
        }
        private XmlNode LoadBaseFolderInfo(COMInterop scorchInterop, string targetFolderGUID, string baseFolderGUID)
        {
            XmlDocument baseDoc = scorchInterop.GetSCOSubFolders(baseFolderGUID);

            foreach (XmlNode idNode in baseDoc.SelectNodes("Folders/Folder/UniqueID"))
            {
                if (idNode.InnerText.Equals(targetFolderGUID))
                {
                    return idNode.ParentNode;
                }
            }


            foreach (XmlNode subFolder in baseDoc.SelectNodes("Folders/Folder"))
            {
                XmlNode folderNode = LoadBaseFolderInfo(scorchInterop, targetFolderGUID, baseFolderGUID);
                if (folderNode != null)
                {
                    return folderNode;
                }
            }

            return null;
        }
        #endregion
        #region export functions
        public void LoadComputerGroups(COMInterop scorchInterop)
        {
            XmlDocument computerDocument = scorchInterop.GetSCOResourceInFolder(ResourceFolderRoot.Computers, ResourceType.Computer);
            if (computerDocument.SelectNodes("//UniqueID").Count > 1)
            {
                XmlNode resourceNode = _OISExport.SelectSingleNode("ExportData/GlobalSettings/ComputerGroups");
                resourceNode.RemoveAll();
                XmlNode importNode = _OISExport.ImportNode(computerDocument.SelectSingleNode("Folder"), true);
                resourceNode.AppendChild(importNode);
            }
        }
        public void LoadCounters(COMInterop scorchInterop)
        {
            XmlDocument countersDocument = scorchInterop.GetSCOResourceInFolder(ResourceFolderRoot.Counters, ResourceType.Counter);
            if (countersDocument.SelectNodes("//UniqueID").Count > 1)
            {
                XmlNode resourceNode = _OISExport.SelectSingleNode("ExportData/GlobalSettings/Counters");
                resourceNode.RemoveAll();
                XmlNode importNode = _OISExport.ImportNode(countersDocument.SelectSingleNode("Folder"), true);
                resourceNode.AppendChild(importNode);
            }
        }
        public void LoadSchedules(COMInterop scorchInterop)
        {
            XmlDocument schedulesDocument = scorchInterop.GetSCOResourceInFolder(ResourceFolderRoot.Schedules, ResourceType.Schedule);
            if (schedulesDocument.SelectNodes("//UniqueID").Count > 1)
            {
                XmlNode resourceNode = _OISExport.SelectSingleNode("ExportData/GlobalSettings/Schedules");
                resourceNode.RemoveAll();
                XmlNode importNode = _OISExport.ImportNode(schedulesDocument.SelectSingleNode("Folder"), true);
                resourceNode.AppendChild(importNode);
            }
        }
        public void LoadVariables(COMInterop scorchInterop)
        {
            XmlDocument variablesDocument = scorchInterop.GetSCOResourceInFolder(ResourceFolderRoot.Variables, ResourceType.Variable);
            if (variablesDocument.SelectNodes("//UniqueID").Count > 1)
            {
                XmlNode resourceNode = _OISExport.SelectSingleNode("ExportData/GlobalSettings/Variables");
                resourceNode.RemoveAll();
                XmlNode importNode = _OISExport.ImportNode(variablesDocument.SelectSingleNode("Folder"), true);
                resourceNode.AppendChild(importNode);
            }
        }
        public void LoadConfigurations(COMInterop scorchInterop)
        {
            string[] configurationIDArray = scorchInterop.GetSCOConfigurationIDs();
            foreach (string configID in configurationIDArray)
            {
                XmlDocument configDocument = scorchInterop.GetSCOConfigurationValue(configID);
                XmlNode importNode = _OISExport.ImportNode(configDocument.FirstChild, true);
                _OISExport.SelectSingleNode("ExportData/GlobalConfigurations").AppendChild(importNode);
            }
        }
        public void LoadExportFromFolder(string folderPath, COMInterop scorchInterop)
        {
            XmlDocument folderDocument = scorchInterop.GetSCOFolderByPath(folderPath);

            string folderID = folderDocument.SelectSingleNode("Folder/UniqueID").InnerText;
            XmlNode baseFolderNode = _OISExport.ImportNode(folderDocument.SelectSingleNode("Folder"), true);
            cleanFolderNode(baseFolderNode);

            _OISExport.SelectSingleNode("ExportData/Policies").AppendChild(baseFolderNode); 
            LoadFolderPolicyExports(scorchInterop, folderID, baseFolderNode);
        }
        #endregion
        #region import
        public void ImportSCORunbook(string parentFolderID, bool overwriteExisting,
                                               bool runbooks, bool variables, bool schedules, bool counters, bool computerGroups, bool globalConfigs,
                                               bool overwriteGlobalConfigs, string dbServer, string dbName, COMInterop scorchInterop)
        {
            Dictionary<Guid, Guid> guidMap = new Dictionary<Guid, Guid>();

            // Verify Import File / Fix GUIDs
            guidMap = processNodeGuids(runbooks, variables, schedules, counters, computerGroups, globalConfigs, dbServer, dbName, scorchInterop, ref guidMap);

            string innerXML = _OISExport.InnerXml;

            // Guid Replace
            foreach (Guid exportFileGuid in guidMap.Keys)
            {
                innerXML.Replace(exportFileGuid.ToString(), guidMap[exportFileGuid].ToString());
            }
            
            // Import
            importFile(runbooks, variables, schedules, counters, computerGroups, globalConfigs, overwriteExisting, dbServer, dbName, scorchInterop, guidMap);
        }

        #endregion
    }
}
