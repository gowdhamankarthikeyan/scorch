using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ParseOrchestratorExport;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.OIS_Export;
using System.IO;
using System.Collections;
using System.Linq;

namespace ParseOrchestratorExport
{
    public partial class Form1 : Form
    {
        private XmlDocument dom;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "OIS Export Files|*.ois_export";
            openFileDialog1.Title = "Select OIS Export File";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(string.Empty))
            {
                FileInfo fi = new FileInfo(textBox1.Text);
                if (fi.Exists)
                {
                    this.Cursor = Cursors.WaitCursor;
                    button2.Enabled = false;

                    LoadDom();

                    reloadTree();

                    this.Cursor = Cursors.Default;
                    button2.Enabled = true;

                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                    button7.Visible = true;
                    button8.Visible = true;
                    button9.Visible = true;

                    btnModifyName.Visible = true;

                    btnSanitizeExport.Visible = true;
                }
                else
                {
                    MessageBox.Show(String.Format("Cannot find file: {0}", textBox1.Text));
                }
            }
            else
            {
                button1_Click(sender, e);
            }
        }

        private void reloadTree()
        {
            // Create Base Folder Structure
            treeViewFoldersAndPolicies.Nodes.Clear();
            TreeNode treeRoot = treeViewFoldersAndPolicies.Nodes.Add("rootnode", "Root", 0);

            // Declare Initial Roots
            TreeNode RunbookNode;
            TreeNode VariablesTreeNode;
            TreeNode SchedulesTreeNode;
            TreeNode CountersTreeNode;
            TreeNode ComputerGroupsTreeNode;
            TreeNode GlobalConfigurationsNode;
            TreeNode ASCUsersTreeNode;

            // Setup initial Roots
            setupTreeRoots(treeRoot, out RunbookNode, out VariablesTreeNode, out SchedulesTreeNode, out CountersTreeNode, out ComputerGroupsTreeNode, out GlobalConfigurationsNode, out ASCUsersTreeNode);

            // Load Data into Roots
            LoadTreeRootData(RunbookNode, VariablesTreeNode, SchedulesTreeNode, CountersTreeNode, ComputerGroupsTreeNode, GlobalConfigurationsNode, ASCUsersTreeNode);
        }

        private void LoadTreeRootData(TreeNode RunbookNode, TreeNode VariablesTreeNode, TreeNode SchedulesTreeNode, TreeNode CountersTreeNode, TreeNode ComputerGroupsTreeNode, TreeNode GlobalConfigurationsNode, TreeNode ASCUsersTreeNode)
        {
            // Load Folder Section
            AddNewFolder(dom.GetElementsByTagName("Policies").Item(0), RunbookNode);

            // Load Global Settings Section
            try { AddNewGlobalSettingsFolder(dom.GetElementsByTagName("Variables").Item(0).SelectSingleNode("./Folder"), VariablesTreeNode, "variab"); }
            catch { }
            try { AddNewGlobalSettingsFolder(dom.GetElementsByTagName("Schedules").Item(0).SelectSingleNode("./Folder"), SchedulesTreeNode, "schedu"); }
            catch { }
            try { AddNewGlobalSettingsFolder(dom.GetElementsByTagName("Counters").Item(0).SelectSingleNode("./Folder"), CountersTreeNode, "counte"); }
            catch { }
            try { AddNewGlobalSettingsFolder(dom.GetElementsByTagName("ComputerGroups").Item(0).SelectSingleNode("./Folder"), ComputerGroupsTreeNode, "compgr"); }
            catch { }

            // Load Global Configurations Section
            AddNewGlobalConfigurationFolder(dom.GetElementsByTagName("GlobalConfigurations").Item(0), GlobalConfigurationsNode);

            // Load Alternate Users
            AddNewAscUser(dom.GetElementsByTagName("Policies").Item(0), ASCUsersTreeNode);
        }

        private void setupTreeRoots(TreeNode treeRoot, out TreeNode RunbookNode, out TreeNode VariablesTreeNode, out TreeNode SchedulesTreeNode, out TreeNode CountersTreeNode, out TreeNode ComputerGroupsTreeNode, out TreeNode GlobalConfigurationsNode, out TreeNode ASCUsersTreeNode)
        {

            // Setup Root Runbook Node
            RunbookNode = treeRoot.Nodes.Add(String.Format("folder_{0}", ResourceFolderRoot.Runbooks), "Runbooks", 0);

            // Setup Global Settings Root
            XmlNodeList variablesNodeList = dom.GetElementsByTagName("Variables");
            XmlNodeList SchedulesNodeList = dom.GetElementsByTagName("Schedules");
            XmlNodeList CountersNodeList = dom.GetElementsByTagName("Counters");
            XmlNodeList ComputerGroupsNodeList = dom.GetElementsByTagName("ComputerGroups");
            TreeNode GlobalSettingsTreeNode = treeRoot.Nodes.Add("_GlobalSettings", "Global Settings", 0);

            if (variablesNodeList.Count > 0) 
            { 
                XmlNode variablesNode = variablesNodeList.Item(0).SelectSingleNode("./Folder");
                VariablesTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", variablesNode.SelectSingleNode("./UniqueID/text()").Value), "Variables", 0); 
            }
            else { VariablesTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", ResourceFolderRoot.Variables), "Variables", 0); }
            if (SchedulesNodeList.Count > 0) 
            { 
                XmlNode SchedulesNode = SchedulesNodeList.Item(0).SelectSingleNode("./Folder");
                SchedulesTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", SchedulesNode.SelectSingleNode("./UniqueID/text()").Value), "Schedules", 0); 
            }
            else { SchedulesTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", ResourceFolderRoot.Schedules), "Schedules", 0); }
            if (CountersNodeList.Count > 0) 
            { 
                XmlNode CountersNode = CountersNodeList.Item(0).SelectSingleNode("./Folder");
                CountersTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", CountersNode.SelectSingleNode("./UniqueID/text()").Value), "Counters", 0); 
            }
            else { CountersTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", ResourceFolderRoot.Counters), "Counters", 0); }
            if (ComputerGroupsNodeList.Count > 0) 
            {
                XmlNode ComputerGroupsNode = ComputerGroupsNodeList.Item(0).SelectSingleNode("./Folder");            
                ComputerGroupsTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", ComputerGroupsNode.SelectSingleNode("./UniqueID/text()").Value), "Computer Groups", 0); 
            }
            else { ComputerGroupsTreeNode = GlobalSettingsTreeNode.Nodes.Add(String.Format("folder_{0}", ResourceFolderRoot.Computers), "Computer Groups", 0); }
            // Setup Global Configurations Root
            GlobalConfigurationsNode = treeRoot.Nodes.Add("_GlobalConfigurations", "Global Configurations", 0);

            // Setup ASC Users Node
            ASCUsersTreeNode = treeRoot.Nodes.Add("_ASCUsers", "Alternate Runbook Users", 0);
        }

        private void LoadDom()
        {
            try
            {
                dom = new XmlDocument();
                dom.Load(textBox1.Text);
            }

            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Called Recursively on Folders to add folders and policies contained in the folders to a tree node
        /// </summary>
        /// <param name="parentX">Parent folder node reference</param>
        /// <param name="parentT">Parent tree node reference</param>
        private void AddNewFolder(XmlNode parentX, TreeNode parentT)
        {
            if (parentX != null)
            {
                TreeNode addedNode;

                // Get all Child Folders
                XmlNodeList folderList = parentX.SelectNodes("./Folder");

                // Get all Child Policies
                XmlNodeList policyList = parentX.SelectNodes("./Policy");

                try
                {
                    // Iterate through the Folder List
                    foreach (XmlNode folderNode in folderList)
                    {
                        addedNode = parentT.Nodes.Add("folder_" + folderNode.SelectSingleNode("./UniqueID/text()").Value, folderNode.SelectSingleNode("./Name/text()").Value, 1, 1);
                        AddNewFolder(folderNode, addedNode);
                    }

                    // Iterate through the Policy List
                    foreach (XmlNode policyNode in policyList)
                    {
                        parentT.Nodes.Add("policy_" + policyNode.SelectSingleNode("./UniqueID/text()").Value, policyNode.SelectSingleNode("./Name/text()").Value, 2, 2);
                    }
                }

                catch (XmlException xmlEx)
                {
                    //MessageBox.Show(xmlEx.Message);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Called Recursively on Global Settings Folders to add folders and policies contained in the folders to a tree node
        /// </summary>
        /// <param name="parentX">Parent folder node reference</param>
        /// <param name="parentT">Parent tree node reference</param>
        private void AddNewGlobalSettingsFolder(XmlNode parentX, TreeNode parentT, string type)
        {
            if (parentX != null)
            {
                TreeNode addedNode;

                // Get all Child Folders
                XmlNodeList folderList = parentX.SelectNodes("./Folder");

                // Get all objects
                XmlNodeList objectList = parentX.SelectNodes("./Objects/Object");

                try
                {
                    // Iterate through the Folder List
                    foreach (XmlNode folderNode in folderList)
                    {
                        addedNode = parentT.Nodes.Add(String.Format("{0}_{1}", "folder", folderNode.SelectSingleNode("./UniqueID/text()").Value), folderNode.SelectSingleNode("./Name/text()").Value, 1, 1);
                        AddNewGlobalSettingsFolder(folderNode, addedNode, type);
                    }

                    // Iterate through the Folder List
                    foreach (XmlNode objectNode in objectList)
                    {
                        addedNode = parentT.Nodes.Add(String.Format("{0}_{1}", type, objectNode.SelectSingleNode("./UniqueID/text()").Value), objectNode.SelectSingleNode("./Name/text()").Value, 2, 2);
                    }
                }

                catch (XmlException xmlEx)
                {
                    //MessageBox.Show(xmlEx.Message);
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Called to Process Global Configrations
        /// </summary>
        /// <param name="parentX">Parent Global Config Node Reference</param>
        /// <param name="parentT">Parent tree node reference</param>
        private void AddNewGlobalConfigurationFolder(XmlNode parentX, TreeNode parentT)
        {
            if (parentX != null)
            {
                // Get all Child Entries
                XmlNodeList entryList = parentX.SelectNodes("./Entry");

                // Iterate through the Folder List
                foreach (XmlNode entry in entryList)
                {
                    try
                    {
                        string globalConfigID = entry.SelectSingleNode("./ID/text()").Value;
                        parseGlobalConfigDataSection(entry.SelectSingleNode("./Data"), parentT, globalConfigID);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Called to Process Alternate Users
        /// </summary>
        /// <param name="parentX">Parent Global Config Node Reference</param>
        /// <param name="parentT">Parent tree node reference</param>
        private void AddNewAscUser(XmlNode parentX, TreeNode parentT)
        {
            if (parentX != null)
            {
                Dictionary<String, String> userList = new Dictionary<String, String>();

                // Get all Child Entries
                XmlNodeList objectList = parentX.SelectNodes("//Object[ASC_ThisAccount='TRUE']");
                XmlNodeList PolicyList = parentX.SelectNodes("//Policy[ASC_ThisAccount='TRUE']");


                try
                {
                    // Iterate through the Folder List
                    foreach (XmlNode entry in objectList)
                    {
                        if (!userList.ContainsKey(entry["ASC_Username"].InnerText))
                        {
                            userList.Add(entry["ASC_Username"].InnerText, String.Format("{0}\\{1}", entry["UniqueID"].InnerText, entry["Name"].InnerText));
                        }
                    }
                    foreach (XmlNode entry in PolicyList)
                    {
                        if (!userList.ContainsKey(entry["ASC_Username"].InnerText))
                        {
                            userList.Add(entry["ASC_Username"].InnerText, String.Format("{0}\\{1}", entry["UniqueID"].InnerText, entry["Name"].InnerText));
                        }
                    }

                    // Add users to node
                    foreach (string user in userList.Keys)
                    {
                        TreeNode addedNode = parentT.Nodes.Add(String.Format("{0}_{1}", "config", userList[user]), user, 2, 2);
                    }

                }

                catch (XmlException xmlEx)
                {
                    //MessageBox.Show(xmlEx.Message);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        private void parseGlobalConfigDataSection(XmlNode parentX, TreeNode parentT, string ID)
        {
            if (parentX != null)
            {
                string innerText = String.Empty;
                try
                {
                    innerText = parentX.InnerText;
                    parentX.InnerXml = parentX.InnerText;

                    XmlNode configurationsNode = parentX.SelectSingleNode("./Object/Configurations");
                    if (configurationsNode != null)
                    {
                        string ConfigTypeName = parentX.SelectSingleNode(".//Name/text()").Value;
                        TreeNode addedNode = parentT.Nodes.Add(String.Format("{0}_{1}", "config", ID), ConfigTypeName, 1, 1);
                        parseGlobalConfigConfigurationsSection(configurationsNode, addedNode, ID);
                    }
                    else
                    {
                        XmlNode childNode = parentX["Object"].FirstChild;
                        if (childNode != null)
                        {
                            childNode.InnerXml = childNode.InnerText;

                            XmlNode itemRootNode = childNode.SelectSingleNode("ItemRoot");
                            if (itemRootNode != null)
                            {
                                string ConfigTypeName = string.Format("NativeConfig:{0}", ID);
                                TreeNode addedNode = parentT.Nodes.Add(String.Format("{0}_{1}", "native", ID), ConfigTypeName, 1, 1);
                                foreach (XmlNode entryNode in itemRootNode.ChildNodes)
                                {
                                    string name;
                                    XmlNode nameNode = entryNode.SelectSingleNode("Name");
                                    if (nameNode != null)
                                    {
                                        name = entryNode["Name"].InnerText;
                                    }
                                    else
                                    {
                                        StringBuilder nameBuilder = new StringBuilder();
                                        bool first = true;
                                        foreach (XmlNode child in entryNode.ChildNodes)
                                        {
                                            if (!child.InnerText.Contains("\\`d.T.~De/"))
                                            {
                                                if (first) { nameBuilder.Append(child.InnerText); first = false; }
                                                else { nameBuilder.Append(String.Format(":{0}", child.InnerText)); }
                                            }
                                        }

                                        name = nameBuilder.ToString();
                                    }

                                    TreeNode cNode = addedNode.Nodes.Add(String.Format(@"{0}_{1}\{0}\{2}", "native", ID, name), name, 2, 2);
                                }
                            }

                            childNode.InnerText = childNode.InnerXml;
                        }
                    }
                }
                finally
                {
                    parentX.InnerText = innerText;
                }
            }
        }

        private void parseGlobalConfigConfigurationsSection(XmlNode parentX, TreeNode parentT, string ID)
        {
            if (parentX != null)
            {
                string innerText = String.Empty;
                try
                {
                    innerText = parentX.InnerText;
                    parentX.InnerXml = parentX.InnerText;

                    XmlNodeList ConfigNames = parentX.SelectNodes(".//Name");
                    foreach (XmlNode configName in ConfigNames)
                    {
                        TreeNode addedNode = parentT.Nodes.Add(String.Format(@"{0}_{1}\{2}", "config", ID, configName.InnerText), configName.InnerText, 2, 2);
                    }
                }
                finally
                {
                    parentX.InnerText = innerText;
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode;
            string type, id;

            boxProperties.Text = "";

            selectedNode = e.Node;
            type = selectedNode.Name.Substring(0, 6);
            id = selectedNode.Name.Substring(7);

            boxProperties.Text = "Unique ID: \t" + id;

            switch (type)
            {
                case "folder":
                    GetFolderObjects(id);
                    break;
                case "policy":
                    GetPolicyObjects(id);
                    break;
                case "variab":
                    GetVariableObjects(id);
                    break;
                case "schedu":
                    GetScheduleObjects(id);
                    break;
                case "counte":
                    GetScheduleObjects(id);
                    break;
                case "compgr":
                    GetComputerGroupObjects(id);
                    break;
                case "config":
                    GetConfigObjects(id);
                    break;
                default:
                    break;
            }
        }

        private void GetConfigObjects(string configID)
        {
            try
            {
                string[] configIDArray = configID.Split('\\');
                if (configIDArray.Length > 0)
                {
                    XmlNode ConfigCategoryRoot = dom.SelectSingleNode(String.Format("//Entry[ID='{0}']", configIDArray[0]));

                    if (configIDArray.Length == 2)
                    {
                        parseConfigEntryDataBlock(ConfigCategoryRoot.SelectSingleNode("./Data"), configIDArray[1]);
                    }
                    else
                    {
                        setupDefaultListViewObjects();
                    }
                }
            }


            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void parseConfigEntryDataBlock(XmlNode dataNode, string configPointer)
        {
            string innerTextDataNode = String.Empty;
            try
            {
                // Unpack inner datanode XML
                innerTextDataNode = dataNode.InnerText;
                dataNode.InnerXml = dataNode.InnerText;

                XmlNode configurationsNode = dataNode.SelectSingleNode("./Object/Configurations");
                if (configurationsNode != null)
                {
                    string innerTextConfigurationNode = String.Empty;
                    try
                    {
                        // Unpack inner configurations XML
                        innerTextConfigurationNode = dataNode.InnerText;
                        configurationsNode.InnerXml = configurationsNode.InnerText;

                        // Find our config Node
                        XmlNode targetConfigNode = configurationsNode.SelectSingleNode(String.Format("./Configurations/Configuration[Name='{0}']", configPointer));

                        // Get its properties section
                        XmlNode propertiesNode = targetConfigNode.SelectSingleNode("./Properties");
                        string innerTextProperties = string.Empty;
                        try
                        {
                            // Unpack inner configurations XML
                            innerTextProperties = propertiesNode.InnerText;
                            propertiesNode.InnerXml = propertiesNode.InnerText;

                            XmlNodeList propertyNameNodeList = propertiesNode.SelectNodes("./ItemRoot/Entry/PropertyName/text()");
                            string[] nameList = new string[propertyNameNodeList.Count];

                            for (int i = 0; i < propertyNameNodeList.Count; i++)
                            {
                                nameList[i] = propertyNameNodeList[i].Value;
                            }

                            setupGlobalConfigurationsListViewObjects(nameList, propertiesNode);
                        }
                        finally
                        {
                            // RePack inner datanode xml
                            propertiesNode.InnerText = innerTextProperties;
                        }

                    }
                    finally
                    {
                        // RePack inner datanode xml
                        configurationsNode.InnerText = innerTextConfigurationNode;
                    }
                }
            }
            finally
            {
                // RePack inner datanode xml
                dataNode.InnerText = innerTextDataNode;
            }
        }

        private void GetPolicyObjects(string policyID)
        {
            XmlNode policy;
            XmlNodeList policyObjects;
            ListViewItem cItem;

            setupDefaultListViewObjects();

            try
            {
                policy = dom.SelectSingleNode("//Policy[UniqueID='" + policyID + "']");
                policyObjects = policy.SelectNodes("./Object[not(ObjectTypeName = 'Link')]");
                for (int i = 0; i < policyObjects.Count; i++)
                {
                    cItem = new ListViewItem(policyObjects[i].SelectSingleNode("./Name/text()").Value);
                    cItem.SubItems.Add(policyObjects[i].SelectSingleNode("./ObjectTypeName/text()").Value);
                    cItem.SubItems.Add(policyObjects[i].SelectSingleNode("./UniqueID/text()").Value);
                    listViewObjects.Items.Add(cItem);
                }
            }

            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void GetFolderObjects(string folderID)
        {
            XmlNode folder;
            XmlNodeList folderObjects;
            ListViewItem cItem;

            foreach (ListViewItem itm in listViewObjects.Items)
                listViewObjects.Items.Remove(itm);

            try
            {
                folder = dom.SelectSingleNode("//Folder[UniqueID='" + folderID + "']");
                folderObjects = folder.SelectNodes("./Objects/Object[not(ObjectTypeName = 'Link')]");

                if (folderObjects.Count > 0)
                {
                    switch (folderObjects[0].SelectSingleNode("./ObjectTypeName/text()").Value)
                    {
                        default:
                            setupDefaultListViewObjects();
                            break;
                        case "Variable":
                            setupVariableListViewObjects();
                            break;
                    }
                }
                for (int i = 0; i < folderObjects.Count; i++)
                {
                    cItem = new ListViewItem(folderObjects[i].SelectSingleNode("./Name/text()").Value);

                    switch (folderObjects[0].SelectSingleNode("./ObjectTypeName/text()").Value)
                    {
                        default:
                            cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./ObjectTypeName/text()").Value);
                            break;
                        case "Variable":
                            cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./Value/text()").Value);
                            break;
                    }

                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./UniqueID/text()").Value);
                    listViewObjects.Items.Add(cItem);
                }
            }

            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void GetVariableObjects(string objectID)
        {
            XmlNode obj;
            ListViewItem cItem;

            setupVariableListViewObjects();


            try
            {
                obj = dom.SelectSingleNode(String.Format("//Object[UniqueID='{0}']", objectID));

                cItem = new ListViewItem(obj.SelectSingleNode("./Name/text()").Value);
                cItem.SubItems.Add(obj.SelectSingleNode("./Value/text()").Value);
                cItem.SubItems.Add(obj.SelectSingleNode("./UniqueID/text()").Value);
                listViewObjects.Items.Add(cItem);

            }

            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void GetScheduleObjects(string folderID)
        {
            XmlNode folder;
            XmlNodeList folderObjects;
            ListViewItem cItem;

            setupDefaultListViewObjects();

            try
            {
                folder = dom.SelectSingleNode("//Folder[UniqueID='" + folderID + "']");
                folderObjects = folder.SelectNodes("./Objects/Object[not(ObjectTypeName = 'Link')]");
                for (int i = 0; i < folderObjects.Count; i++)
                {
                    cItem = new ListViewItem(folderObjects[i].SelectSingleNode("./Name/text()").Value);
                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./ObjectTypeName/text()").Value);
                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./UniqueID/text()").Value);
                    listViewObjects.Items.Add(cItem);
                }
            }

            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void GetCounterObjects(string folderID)
        {
            XmlNode folder;
            XmlNodeList folderObjects;
            ListViewItem cItem;

            setupDefaultListViewObjects();

            try
            {
                folder = dom.SelectSingleNode("//Folder[UniqueID='" + folderID + "']");
                folderObjects = folder.SelectNodes("./Objects/Object[not(ObjectTypeName = 'Link')]");
                for (int i = 0; i < folderObjects.Count; i++)
                {
                    cItem = new ListViewItem(folderObjects[i].SelectSingleNode("./Name/text()").Value);
                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./ObjectTypeName/text()").Value);
                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./UniqueID/text()").Value);
                    listViewObjects.Items.Add(cItem);
                }
            }

            catch (XmlException xmlEx)
            {
                // MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void GetComputerGroupObjects(string folderID)
        {
            XmlNode folder;
            XmlNodeList folderObjects;
            ListViewItem cItem;

            setupDefaultListViewObjects();

            try
            {
                folder = dom.SelectSingleNode("//Folder[UniqueID='" + folderID + "']");
                folderObjects = folder.SelectNodes("./Objects/Object[not(ObjectTypeName = 'Link')]");
                for (int i = 0; i < folderObjects.Count; i++)
                {
                    cItem = new ListViewItem(folderObjects[i].SelectSingleNode("./Name/text()").Value);
                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./ObjectTypeName/text()").Value);
                    cItem.SubItems.Add(folderObjects[i].SelectSingleNode("./UniqueID/text()").Value);
                    listViewObjects.Items.Add(cItem);
                }
            }

            catch (XmlException xmlEx)
            {
                //MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void setupDefaultListViewObjects()
        {
            btnModifyName.Visible = true;
            foreach (ListViewItem itm in listViewObjects.Items)
                listViewObjects.Items.Remove(itm);

            for (int i = listViewObjects.Columns.Count - 1; i >= 0; i--)
            {
                listViewObjects.Columns.Remove(listViewObjects.Columns[i]);
            }

            ColumnHeader cHeader1 = new ColumnHeader();
            cHeader1.Name = "cHeader1";
            cHeader1.Text = "Name";

            ColumnHeader cHeader2 = new ColumnHeader();
            cHeader2.Name = "cHeader1";
            cHeader2.Text = "Type Name";

            ColumnHeader cHeader3 = new ColumnHeader();
            cHeader3.Name = "cHeader1";
            cHeader3.Text = "Unique ID";

            listViewObjects.Columns.Add(cHeader1);
            listViewObjects.Columns.Add(cHeader2);
            listViewObjects.Columns.Add(cHeader3);
        }

        private void setupVariableListViewObjects()
        {
            btnModifyName.Visible = true;
            foreach (ListViewItem itm in listViewObjects.Items)
                listViewObjects.Items.Remove(itm);

            for (int i = listViewObjects.Columns.Count - 1; i >= 0; i--)
            {
                listViewObjects.Columns.Remove(listViewObjects.Columns[i]);
            }

            ColumnHeader cHeader1 = new ColumnHeader();
            cHeader1.Name = "cHeader1";
            cHeader1.Text = "Name";

            ColumnHeader cHeader2 = new ColumnHeader();
            cHeader2.Name = "cHeader1";
            cHeader2.Text = "Variable Value";

            ColumnHeader cHeader3 = new ColumnHeader();
            cHeader3.Name = "cHeader1";
            cHeader3.Text = "Unique ID";

            listViewObjects.Columns.Add(cHeader1);
            listViewObjects.Columns.Add(cHeader2);
            listViewObjects.Columns.Add(cHeader3);
        }

        private void setupGlobalConfigurationsListViewObjects(string[] columnText, XmlNode propertiesNode)
        {
            btnModifyName.Visible = true;
            foreach (ListViewItem itm in listViewObjects.Items)
                listViewObjects.Items.Remove(itm);

            for (int i = listViewObjects.Columns.Count - 1; i >= 0; i--)
            {
                listViewObjects.Columns.Remove(listViewObjects.Columns[i]);
            }

            ListViewItem cItem = null;
            foreach (string cText in columnText)
            {
                ColumnHeader nHeader = new ColumnHeader();
                nHeader.Name = cText;
                nHeader.Text = cText;

                listViewObjects.Columns.Add(nHeader);
                if (cItem == null) { cItem = new ListViewItem(propertiesNode.SelectSingleNode(string.Format("./ItemRoot/Entry[PropertyName='{0}']/PropertyValue/text()", cText)).Value); }
                else { cItem.SubItems.Add(propertiesNode.SelectSingleNode(string.Format("./ItemRoot/Entry[PropertyName='{0}']/PropertyValue/text()", cText)).Value); }
            }
            listViewObjects.Items.Add(cItem);
        }

        private void btnModifyName_Click(object sender, EventArgs e)
        {
            string name = string.Empty;
            string[] nArray = boxProperties.Text.Split('\t','\\');
            
            bool noError = true;
            try 
            { 
                new Guid( nArray[1]);
                switch (nArray[1])
                {
                    case ResourceFolderRoot.Computers:
                        noError = false;
                        break;
                    case ResourceFolderRoot.Counters:
                        noError = false;
                        break;
                    case ResourceFolderRoot.Runbooks:
                        noError = false;
                        break;
                    case ResourceFolderRoot.Satellites:
                        noError = false;
                        break;
                    case ResourceFolderRoot.Schedules:
                        noError = false;
                        break;
                    case ResourceFolderRoot.Variables:
                        noError = false;
                        break;
                }
            }
            catch { noError = false; }

            if (noError)
            {
                switch (nArray.Length)
                {
                    case 4:
                        // handle 'native' global config renames
                        if (!nArray[2].Equals("native")) { break; }
                        name = nArray[3];
                        break;
                    case 3:
                        name = nArray[2];
                        break;
                    case 2:
                        //Look for Folder Node
                        XmlNode refNode = dom.SelectSingleNode(String.Format("//Folder[UniqueID=\"{0}\"]", nArray[1]));
                        if (refNode != null) { name = refNode["Name"].InnerText; break; }

                        //Look for Policy Node
                        refNode = dom.SelectSingleNode(String.Format("//Policy[UniqueID=\"{0}\"]", nArray[1]));
                        if (refNode != null) { name = refNode["Name"].InnerText; break; }

                        //Look for Object Node
                        refNode = dom.SelectSingleNode(String.Format("//Object[UniqueID=\"{0}\"]", nArray[1]));
                        if (refNode != null) { name = refNode["Name"].InnerText; break; }
                        noError = false;
                        MessageBox.Show("Not Valid for this object");
                        break;
                    default:
                        MessageBox.Show("Not Valid for this object");
                        noError = false;
                        break;
                }
                if (noError)
                {
                    ModifyName mName = new ModifyName(name);
                    mName.FormClosed += mName_FormClosed;
                    mName.Show();
                }
            }
            else
            {
                MessageBox.Show("Not Valid for this object");
            }
        }

        void mName_FormClosed(object sender, FormClosedEventArgs e)
        {
            ModifyName mName = (ModifyName)sender;
            if (!mName.cancelled)
            {
                string name = mName.returnName;
                string properties = boxProperties.Text;


                string[] pArray = properties.Split(new char[] { '\\', '\t' });

                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));

                bool noError = true;
                try { new Guid(pArray[1]); }
                catch { noError = false; MessageBox.Show("Not Valid for this object"); }

                if (noError)
                {
                    switch (pArray.Length)
                    {
                        case 2:
                            export.modifyExportUpdateNameByIDRef(pArray[1], name);
                            export.OISExport.Save(textBox1.Text);
                            LoadDom();
                            reloadTree();
                            break;
                        case 3:
                            export.modifyExportChangeGlobalQIKConfigurationName(pArray[2], name, pArray[1]);
                            export.OISExport.Save(textBox1.Text);
                            LoadDom();
                            reloadTree();
                            break;
                        case 4:
                            //native global configuration
                            export.modifyExportChangeGlobalNativeConfigurationName(pArray[3], name, pArray[1]);
                            export.OISExport.Save(textBox1.Text);
                            LoadDom();
                            reloadTree();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                LoadDom();
                reloadTree();
            }
        }


        void updateGlobalConfigurationReferences(string id, string oldName, string newName)
        {
            // Find reference objects and update
            XmlNodeList objectNodes = dom.SelectNodes(String.Format("//Object[Configuration='{0}']", oldName));
            foreach (XmlNode objNode in objectNodes)
            {
                // Find Object Type and see if it references the correct connection type
                XmlNode gcObjectTypeNode = dom.SelectSingleNode(String.Format("//GlobalConfigurations/Entry[ID='{0}']", objNode.SelectSingleNode("ObjectType").InnerText));
                XmlNode gcObjectTypeDataNode = gcObjectTypeNode.SelectSingleNode("Data");
                gcObjectTypeDataNode.InnerXml = gcObjectTypeDataNode.InnerText;
                string configTypeID = gcObjectTypeDataNode.SelectSingleNode(".//ConfigurationID").InnerText;
                if (configTypeID.Equals(id))
                {
                    XmlNode configNode = objNode.SelectSingleNode("Configuration");
                    configNode.InnerText = newName;
                }
                gcObjectTypeDataNode.InnerText = gcObjectTypeDataNode.InnerXml;

            }


            // Find GC and update
            XmlNode gcNode = dom.SelectSingleNode(String.Format("//Entry[ID='{0}']", id));
            XmlNode gcDataNode = gcNode.SelectSingleNode("Data");
            gcDataNode.InnerXml = gcDataNode.InnerText;
            XmlNode gcDataConfigurationsNode = gcDataNode.SelectSingleNode("Object/Configurations");
            gcDataConfigurationsNode.InnerXml = gcDataConfigurationsNode.InnerText;
            XmlNode targetConfigNode = gcDataConfigurationsNode.SelectSingleNode(String.Format("Configurations/Configuration[Name='{0}']", oldName));
            targetConfigNode["Name"].InnerText = newName;
            gcDataConfigurationsNode.InnerText = gcDataConfigurationsNode.InnerXml;
            gcDataNode.InnerText = gcDataNode.InnerXml;

            reloadTree();
        }

        private void btnSanitizeExport_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will remove all un-refferenced global settings and configurations, are you sure?", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.cleanGlobalComputerGroupsNode();
                export.cleanGlobalConfigurations();
                export.cleanGlobalCountersNode();
                export.cleanGlobalSchedulesNode();
                export.cleanGlobalVariablesNode();
                export.OISExport.Save(textBox1.Text);
                LoadDom();
                reloadTree();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This this will turn on Object Specific Logging for all Runbooks, are you sure?", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.modifyObjectSpecificLogging("on");
                export.OISExport.Save(textBox1.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This this will turn off Object Specific Logging for all Runbooks, are you sure?", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.modifyObjectSpecificLogging("off");
                export.OISExport.Save(textBox1.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This this will turn on Generic Logging for all Runbooks, are you sure?", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.modifyGenericLogging("on");
                export.OISExport.Save(textBox1.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This this will turn off Generic Logging for all Runbooks, are you sure?", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.modifyGenericLogging("off");
                export.OISExport.Save(textBox1.Text);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This this will apply the best practices for link labeling and coloring.", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.modifyExportLinkApplyBestPractices();
                export.OISExport.Save(textBox1.Text);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This this will modify the Max Parallel Request settings for Runbooks\n" +
                                                  "The modification is based on Folder and Runbook Name.\n" +
                                                  "End a Runbook or Folder Name with \n" +
                                                  "\t- MP:NUMBER\n" +
                                                  "This will set all child Runbooks Parallel Request value to NUMBER", "Continue?", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ExportFile export = new ExportFile(new FileInfo(textBox1.Text));
                export.modifyExportSetMaxParallelRequestSettingNameBased();
                export.OISExport.Save(textBox1.Text);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<string> badTriggerPolicyObjectIDS = checkTriggerPolicy();
            List<string> badDoubleLinkSourceObjectIDs = checkDoubleLink();

            if (badDoubleLinkSourceObjectIDs.Count == 0 && badTriggerPolicyObjectIDS.Count == 0)
            {
                MessageBox.Show("Export Passed Validation Checks");
            }
            else
            {
                if (badDoubleLinkSourceObjectIDs.Count > 0)
                {
                    StringBuilder badPolicies = new StringBuilder();
                    foreach (string objectID in badDoubleLinkSourceObjectIDs)
                    {
                        XmlNode objectNode = dom.SelectSingleNode(String.Format("//Object[UniqueID=\"{0}\"]", objectID));
                        badPolicies.Append("------------------------------------------------------------------------------------\n");
                        badPolicies.Append(string.Format("{0}\\{1}\n", generatePolicyPath(objectNode["ParentID"].InnerText), objectNode["Name"].InnerText));
                        badPolicies.Append("------------------------------------------------------------------------------------\n");
                    }
                    MessageBox.Show(String.Format("Export Failed Double Link Check\n{0}", badPolicies.ToString()));
                }
                if (badTriggerPolicyObjectIDS.Count > 0)
                {
                    StringBuilder badTriggerPolicies = new StringBuilder();
                    foreach (string objectID in badTriggerPolicyObjectIDS)
                    {
                        XmlNode objectNode = dom.SelectSingleNode(String.Format("//Object[UniqueID=\"{0}\"]", objectID));
                        badTriggerPolicies.Append("------------------------------------------------------------------------------------\n");
                        badTriggerPolicies.Append(string.Format("{0}\\{1}\n", generatePolicyPath(objectNode["ParentID"].InnerText), objectNode["Name"].InnerText));
                        badTriggerPolicies.Append("------------------------------------------------------------------------------------\n");
                    }
                    MessageBox.Show(String.Format("Export Failed Incorrectly Matched Trigger Policy Check\n{0}", badTriggerPolicies.ToString()));
                }
            }
        }
        private string generatePolicyPath(string policyID)
        {
            XmlNode policyNode = dom.SelectSingleNode(String.Format("//Policy[UniqueID=\"{0}\"]", policyID));

            StringBuilder policyPath = new StringBuilder(String.Format("\\{0}",policyNode["Name"].InnerText));

            generatePolicyPath(ref policyPath, policyNode.ParentNode);

            return policyPath.ToString();
        }

        private void generatePolicyPath(ref StringBuilder policyPath, XmlNode folderNode)
        {
            XmlNode folderName = folderNode.SelectSingleNode("Name");
            if (folderName != null)
            {
                policyPath.Insert(0, String.Format("\\{0}", folderName.InnerText));
                generatePolicyPath(ref policyPath, folderNode.ParentNode);
            }
        }
        private List<string> checkTriggerPolicy()
        {
            List<string> returnList = new List<string>();

            XmlNodeList triggerPolicyList = dom.SelectNodes("//Object[ObjectTypeName=\"Trigger Policy\"]");
            foreach (XmlNode triggerPolicy in triggerPolicyList)
            {
                string targetPolicyID = triggerPolicy["PolicyObjectID"].InnerText;

                XmlNode targetPolicyCustomStart = dom.SelectSingleNode(String.Format("//Policy[UniqueID=\"{0}\"]/Object[ObjectTypeName=\"Custom Start\"]", targetPolicyID));
                if (targetPolicyCustomStart != null)
                {
                    XmlNodeList targetPolicyCustomStartParameters = targetPolicyCustomStart.SelectNodes("CUSTOM_START_PARAMETERS/Entry");
                    XmlNode triggerPolicyDefinedParameters = triggerPolicy.SelectSingleNode("TRIGGER_POLICY_PARAMETERS");

                    foreach (XmlNode targetPolicyParam in targetPolicyCustomStartParameters)
                    {
                        string parameterName = targetPolicyParam["Value"].InnerText;

                        XmlNode triggerPolicyParamMatch = triggerPolicyDefinedParameters.SelectSingleNode(String.Format("Entry[ParameterName=\"{0}\"]", parameterName));
                        if (triggerPolicyParamMatch == null)
                        {
                            returnList.Add(triggerPolicy["UniqueID"].InnerText);
                            break;
                        }
                    }
                }
            }
            return returnList;
        }
        private List<string> checkDoubleLink()
        {
            List<string> returnList = new List<string>();
            ArrayList sourceTargetMap = new ArrayList();
            XmlNodeList allLinks = dom.SelectNodes("//Object[ObjectTypeName=\"Link\"]");
            foreach (XmlNode linkNode in allLinks)
            {
                string sourceObjectID = linkNode["SourceObject"].InnerText;
                string targetObjectID = linkNode["TargetObject"].InnerText;

                StringBuilder sb = new StringBuilder(sourceObjectID);
                sb.Append("-");
                sb.Append(targetObjectID);

                if (!sourceTargetMap.Contains(sb.ToString()))
                {
                    sourceTargetMap.Add(sb.ToString());
                }
                else
                {
                    returnList.Add(sourceObjectID);
                }
            }
            return returnList;
        }
    }
}
