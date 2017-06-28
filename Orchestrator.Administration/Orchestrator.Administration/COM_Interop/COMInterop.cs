using System;
using System.Xml;
using OpalisManagementServiceLib;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class COMInterop
    {
        #region constructors
        private OpalisManager _scoManager = new OpalisManager();
        private int _handle;
        public int Handle
        {
            get { return _handle; }
            //set { _handle = value; }
        }
        /// <summary>
        /// Creates a new COMInterop instance
        /// </summary>
        /// <param name="username">username to authenticate to Orchestrator with. Format "Domain\UserID"</param>
        /// <param name="password">password to authenticate to Orchestrator with</param>
        public COMInterop(string username, string password)
        {
            object handle;
            _scoManager.Connect(username, password, out handle);

            int connHandle = 0;
            if (int.TryParse(handle.ToString(), out connHandle))
            {
                _handle = connHandle;
            }
            else
            {
                throw new Exception("Failed to connect");
            }
        }
        /// <summary>
        /// Connection to an existing COMInterop instance
        /// </summary>
        /// <param name="handle">handle of existing connection</param>
        public COMInterop(int handle)
        {
            int connHandle = 0;
            if (int.TryParse(handle.ToString(), out connHandle))
            {
                _handle = connHandle;
            }
            else
            {
                throw new Exception("Failed to connect");
            }
        }
        #endregion
        #region events
        public XmlDocument GetSCOEvents()
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetEvents(out outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        public XmlDocument GetSCOEventDetails(string eventID)
        {
            XmlDocument xml = new XmlDocument();

            XmlDocument events = GetSCOEvents();
            XmlNode eventNode = events.SelectSingleNode(String.Format("//Event/UniqueID[. = {0}]", eventID));
            if (eventNode != null)
            {
                string eventText = eventNode.InnerText;
                try
                {
                    getEDetails(xml, eventText);
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                throw new Exception(string.Format("Event ID {0} not found", eventID));
            }
            return xml;
        }

        private void getEDetails(XmlDocument xml, string eventText)
        {
            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetEventDetails(eventText, out outVar);

            if (outVar != null)
            {
                xml.LoadXml(outVar.ToString());
            }
        }
        #endregion
        #region infrastructure info
        /// <summary>
        /// Gets a list of existing Orchestrator Runbook Servers connected to this Management Server
        /// </summary>
        /// <param name="ManagementServer">The name of the management server (default is blank)</param>
        /// <returns></returns>
        public XmlDocument GetSCORunbookServers(string ManagementServer)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetActionServers(Handle, ManagementServer, out outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        public XmlDocument GetSCOClientConnections()
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetClientConnections(Handle, out outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        public string GetSCOVersion()
        {
            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetVersionInformation(out outVar);
                return (string)outVar;
            }
            catch
            {
                throw;
            }
        }
        public string GetSCOClientCOnnectSignal()
        {
            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.ClientConnectSignal(ref outVar);
                return (string)outVar;
            }
            catch
            {
                throw;
            }
        }
        public void PromoteRunbookServerToPrimary(string runbookServerID)
        {
            // SQL Query.  Dump data in AS table, find current runbook server, make #0, decrement all others
            // Create tmp table and swap for unit query
        }
        #endregion
        #region Generic Object Functions
        public void RemoveSCOObject(Guid objectID, bool deleteFromDB, string dbName, string dbServer, objectTypes objectType)
        {
            if (deleteFromDB)
            {
                _deleteObjectFromDB(objectID, dbName, dbServer, objectType);
            }
            else
            {
                int flags = 0;
                switch (objectType)
                {
                    case objectTypes.Activity:
                        //_scoManager.DeleteResource(_handle, objectID.ToString(), flags);
                        break;
                    case objectTypes.Counter:
                        //_scoManager.DeleteObject(_handle, objectID.ToString(), flags);
                        break;
                    case objectTypes.Folder:
                        _scoManager.DeleteFolder(Handle, objectID.ToString(), flags);
                        break;
                    case objectTypes.Resource:
                        //_scoManager.DeleteResource(_handle, objectID.ToString(), flags);
                        break;
                    case objectTypes.Runbook:
                        _scoManager.DeletePolicy(_handle, objectID.ToString(), flags);
                        break;
                    case objectTypes.Schedule:
                        //_scoManager.DeleteResource(_handle, objectID.ToString(), flags);
                        break;
                    case objectTypes.Variable:
                        //_scoManager.DeleteResource(_handle, objectID.ToString(), flags);
                        break;
                    default:
                        break;
                }
                
            }
        }
        public string CreateSCOObjectUniqueName(Guid parentID, String objectName, objectTypes objectType, string dbName, string dbServer)
        {
            try
            {
                string baseName = objectName;
                bool exists = false;
                int count = 0;

                switch (objectType)
                {
                    case objectTypes.Activity:
                        _generateUniqueObjObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                    case objectTypes.Counter:
                        _generateUniqueObjObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                    case objectTypes.Folder:
                        _generateUniqueFolderObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                    case objectTypes.Resource:
                        _generateUniqueObjObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                    case objectTypes.Runbook:
                        _generateUniquePolicyObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                    case objectTypes.Schedule:
                        _generateUniqueObjObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                    case objectTypes.Variable:
                        _generateUniqueObjObjectName(ref objectName, dbName, dbServer, parentID, baseName, ref exists, ref count);
                        break;
                }               

            }
            catch
            {
                throw;
            }

            return objectName;
        }
        public deletionStati GetSCOObjectExistence(Guid objectID, objectTypes objectType, string dbName, string dbServer)
        {
            try
            {
                deletionStati delStati = deletionStati.NotFound;

                switch (objectType)
                {
                    case objectTypes.Activity:
                        delStati = _checkObjectDeletionStatus(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Counter:
                        delStati = _checkObjectDeletionStatus(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Folder:
                        delStati = _checkFolderDeletionStatus(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Resource:
                        delStati = _checkObjectDeletionStatus(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Runbook:
                        delStati = _checkPolicyDeletionStatus(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Schedule:
                        delStati = _checkObjectDeletionStatus(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Variable:
                        delStati = _checkObjectDeletionStatus(objectID, dbName, dbServer);
                        break;
                }

                return delStati;
            }
            catch
            {
                throw;
            }
        }
        public void UndeleteSCOObject(Guid objectID, objectTypes objectType, string dbName, string dbServer)
        {
            try
            {
                switch (objectType)
                {
                    case objectTypes.Activity:
                        _undeleteObject(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Counter:
                        _undeleteObject(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Folder:
                        _undeleteFolder(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Resource:
                        _undeleteObject(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Runbook:
                        _undeletePolicy(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Schedule:
                        _undeleteObject(objectID, dbName, dbServer);
                        break;
                    case objectTypes.Variable:
                        _undeleteObject(objectID, dbName, dbServer);
                        break;
                }
            }
            catch
            {
                throw;
            }
        }
        public foundStai CompareSCOObjects(Guid parentID, Guid objectID, string objectName, objectTypes objectType, string dbName, string dbServer)
        {
            try
            {
                foundStai exists = foundStai.notFound;
                switch (objectType)
                {
                    case objectTypes.Activity:
                        exists = _checkObjectExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                    case objectTypes.Counter:
                        exists = _checkObjectExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                    case objectTypes.Folder:
                        exists = _checkFolderExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                    case objectTypes.Resource:
                        exists = _checkObjectExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                    case objectTypes.Runbook:
                        exists = _checkPolicyExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                    case objectTypes.Schedule:
                        exists = _checkObjectExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                    case objectTypes.Variable:
                        exists = _checkObjectExistance(parentID, objectID, objectName, dbName, dbServer);
                        break;
                }
                return exists;
            }
            catch
            {
                throw;
            }
        }
        private void importNode(Guid parentID, XmlDocument objectXML, objectTypes objectType, bool overwriteExisting, string dbServer, string dbName)
        {
            Guid objectID = new Guid(objectXML[Enum.GetName(typeof(objectTypes), objectType)]["UniqueID"].InnerText.ToString());
            String objectName = objectXML[Enum.GetName(typeof(objectTypes), objectType)]["Name"].InnerText.ToString();

            // Test to see if the is active, deleted or doesn't exist
            switch (GetSCOObjectExistence(objectID, objectType, dbName, dbServer))
            {
                case deletionStati.Active:
                    // GUID of object Found and is Active
                    // Check if the object is active at the import destination
                    switch (CompareSCOObjects(parentID, objectID, objectName, objectType, dbName, dbServer))
                    {
                        case foundStai.foundGuid:
                            // Object is active at the current location
                            if (overwriteExisting)
                            {
                                // Overwrite the exsiting object
                                RemoveSCOObject(objectID, true, dbName, dbServer, objectType);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Create a new Name and GUID for the object
                                objectName = CreateSCOObjectUniqueName(parentID, objectName, objectType, dbName, dbServer);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                        case foundStai.foundName:
                            // Object is active at the current location
                            if (overwriteExisting)
                            {
                                // Overwrite the exsiting object
                                RemoveSCOObject(objectID, true, dbName, dbServer, objectType);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Create a new Name and GUID for the object
                                objectName = CreateSCOObjectUniqueName(parentID, objectName, objectType, dbName, dbServer);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                        case foundStai.notFound:
                            // Object is active at different location -- Change GUID and import
                            objectID = Guid.NewGuid();
                            _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            break;
                    }
                    break;
                case deletionStati.Deleted:
                    // GUID of folder object in Deleted Status
                    // Check to see if the object is deleted at the import destination
                    switch (CompareSCOObjects(parentID, objectID, objectName, objectType, dbName, dbServer))
                    {
                        case foundStai.foundGuid:
                            // Object is deleted at the current location so undelete it so we can modify
                            // UndeleteSCOObject(objectID, objectType, dbName, dbServer);
                            if (overwriteExisting)
                            {
                                // Overwrite the existing deleted object
                                RemoveSCOFolder(objectID, true, dbName, dbServer);

                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Generate a new Name and GUID for the object
                                objectName = CreateSCOObjectUniqueName(parentID, objectName, objectType, dbName, dbServer);
                                objectID = Guid.NewGuid();

                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                        case foundStai.foundName:
                            // Object is deleted at the current location so undelete it so we can modify
                            // UndeleteSCOObject(objectID, objectType, dbName, dbServer);
                            if (overwriteExisting)
                            {
                                // Overwrite the existing deleted object
                                RemoveSCOFolder(objectID, true, dbName, dbServer);

                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Generate a new Name and GUID for the object
                                objectName = CreateSCOObjectUniqueName(parentID, objectName, objectType, dbName, dbServer);
                                objectID = Guid.NewGuid();

                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                        case foundStai.notFound:
                            // Object is deleted at a different location
                            if (overwriteExisting)
                            {
                                // Delete Folder to retain guid -- Object is deleted at the current location so undelete it so we can modify
                                UndeleteSCOObject(objectID, objectType, dbName, dbServer);
                                RemoveSCOFolder(objectID, true, dbName, dbServer);

                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Not overwriting existing so generate a new GUID and import
                                objectID = Guid.NewGuid();

                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                    }
                    break;
                case deletionStati.NotFound:
                    // GUID of object Not Found
                    // Check to see if there is a object of the same name at the location
                    switch (CompareSCOObjects(parentID, objectID, objectName, objectType, dbName, dbServer))
                    {
                        case foundStai.foundGuid:
                            // There is an object with the same name (but different GUID) at this location
                            if (overwriteExisting)
                            {
                                // Delete the existing object and import the object
                                RemoveSCOFolder(objectID, true, dbName, dbServer);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Cannot overwrite the existing object so create a new, unique name
                                objectName = CreateSCOObjectUniqueName(parentID, objectName, objectType, dbName, dbServer);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                        case foundStai.foundName:
                            // There is an object with the same name (but different GUID) at this location
                            if (overwriteExisting)
                            {
                                // Delete the existing object and import the object
                                RemoveSCOFolder(objectID, true, dbName, dbServer);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            else
                            {
                                // Cannot overwrite the existing object so create a new, unique name
                                objectName = CreateSCOObjectUniqueName(parentID, objectName, objectType, dbName, dbServer);
                                _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            }
                            break;
                        case foundStai.notFound:
                            // There is no object at this location with this name, safe to import!
                            _addSCOObject(parentID, objectXML, objectID, objectName, objectType);
                            break;
                    }
                    break;
            }
        }
        #endregion
        #region Activity / Global Resources
        public XmlDocument GetSCOActivity(Guid objectID)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.LoadObject(Handle, objectID.ToString(), out outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        public XmlDocument GetSCOActivityTypes()
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetObjectTypes(Handle, out outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        /// <summary>
        /// Updates a given object with new XML
        /// </summary>
        /// <param name="objectID">GUID of the object to be updated</param>
        /// <param name="ObjectXML">An XML-formatted String containing the <OBJECT> node to be imported.</param>
        /// <returns></returns>
        public XmlDocument SetSCOActivity(Guid objectID, string ObjectXML)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(ObjectXML);
            try
            {
                _scoManager.ModifyObject(Handle, objectID.ToString(), Guid.NewGuid().ToString(), ObjectXML);
            }
            catch
            {
                throw;
            }

            return xml;
        }
               
        #endregion
        #region Import
        public void ImportSCOGlobalSettingsNode(string rootNodeID, string parentID, XmlNode objectNode, bool overwriteExisting, string dbServer, string dbName)
        {
            string parentName = string.Empty;
            string parentFolderID = string.Empty;

            if (parentID != string.Empty)
            {
                parentFolderID = (new Guid(parentID)).ToString();
            }
            else
            {
                parentFolderID = objectNode["UniqueID"].InnerText;
            }

            if (objectNode["UniqueID"].InnerText.Equals(rootNodeID))
            {
                // root node, don't try adding
            }
            else
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(objectNode.OuterXml);
                AddSCOFolder(new Guid(parentID), xml, overwriteExisting, dbServer, dbName);
            }

            // Process sub folders
            XmlNodeList folderNodes = objectNode.SelectNodes("Folder");
            if (folderNodes.Count > 0)
            {
                foreach (XmlNode folderNode in folderNodes)
                {
                    ImportSCOGlobalSettingsNode(rootNodeID, parentID, folderNode, overwriteExisting, dbServer, dbName);
                }
            }

            // Process objects in this folder
            XmlNodeList objectNodes = objectNode.SelectNodes("Objects/Object");
            if (objectNodes.Count > 0)
            {
                foreach (XmlNode o in objectNodes)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(o.OuterXml);
                    importNode(new Guid(parentID), xml, objectTypes.Resource, overwriteExisting, dbServer, dbName);
                }
            }
        }
        #endregion
        #region Runbooks
        /// <summary>
        /// Returns an individual Runbook's XML representation
        /// </summary>
        /// <param name="runbookID">The ID of the runbook to retrieve the XML of</param>
        /// <returns>XML Representation of the Runbook</returns>
        public XmlDocument GetIndividualRunbookXML(string runbookID)
        {
            XmlDocument xml = new XmlDocument();

            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.LoadPolicy(_handle, runbookID, out outVar);

            if (outVar != null)
            {
                xml.LoadXml(outVar.ToString());
            }
            return xml;
        }
        public string GetSCORunbookIDFromPath(string path)
        {
            object odetails = new object();
            object outvar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetPolicyIDFromPath(path, out outvar);

            return outvar.ToString();
        }
        public string GetSCORunbookPathFromID(string runbookID)
        {
            object odetails = new object();
            object outvar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetPolicyPathFromID(runbookID, out outvar);

            return outvar.ToString();
        }
        public string GetSCORunbookActivities(string runbookID)
        {
            object odetails = new object();
            object outvar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetPolicyObjectList(Handle, runbookID, out outvar);

            return outvar.ToString();
        }
        public int GetSCORunbookPublishState(string runbookID)
        {
            object odetails = new object();
            int outvar = 0;
            _scoManager.GetPolicyPublishState(Handle, runbookID, out outvar);

            // create an enum mapping

            return outvar;
        }
        #endregion
        #region Folders
        /// <summary>
        /// Returns the folder path of a folder given the Folder's ID
        /// </summary>
        /// <param name="folderID">The GUID of the folder</param>
        /// <returns>The string path of the folder</returns>
        public string GetSCOFolderPathFromID(string folderID)
        {
            try
            {
                if (folderID.Equals(ResourceFolderRoot.Runbooks))
                {
                    return StringValues.policyRootString;
                }
                else
                {
                    object odetails = new object();
                    object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                    _scoManager.GetFolderPathFromID(folderID, out outVar);
                    return Convert.ToString(outVar);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Returns a SCO Folder given a Path
        /// </summary>
        /// <param name="folderPath">Path to the folder of which to return the XML representation.  Form of Policies\FolderName\SubFolderName</param>
        /// <returns>An XML represenation of the folder and its contents</returns>
        public XmlDocument GetSCOFolderByPath(string folderPath)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetFolders(_handle, ResourceFolderRoot.Runbooks, out outVar);
                xml.LoadXml(outVar.ToString());

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                    if (folderPath.Equals("Policies"))
                    {
                        xml = _GetSCOFolderByPath(folderPath, xml);
                    }
                    else
                    {
                        xml = _GetSCOFolderByPath(folderPath.Substring(folderPath.IndexOf("\\") + 1), xml);
                    }
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        /// <summary>
        /// Returns all immediate subfolders for a given Folder ID
        /// </summary>
        /// <param name="folderID">ID To retrieve sub folders for</param>
        /// <returns>XML Representaion of child folders</returns>
        public XmlDocument GetSCOSubFolders(string folderID)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object odetails = new object();
                object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                _scoManager.GetFolders(_handle, folderID, out outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        /// <summary>
        /// Returns the folder contents of a given folder
        /// </summary>
        /// <param name="folderID">Folder ID of the folder</param>
        /// <returns>XML Representation of a folders contents</returns>
        public XmlDocument GetSCOFolderContents(string folderID)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                object outVar = new object();

                _scoManager.GetFolderContents(_handle, folderID, ref outVar);

                if (outVar != null)
                {
                    xml.LoadXml(outVar.ToString());
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        public string GetSCOFolderIDFromPath(string folderPath, string dbServer, string dbName)
        {
            if (folderPath.StartsWith("Runbooks")) { folderPath.Remove(0, 8); }
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<_Microsoft_SystemCenter_Orchestrator___Folders_> folderTable = db.GetTable<_Microsoft_SystemCenter_Orchestrator___Folders_>();

                // Query for Policies with name
                var folder =
                    (from fol in folderTable
                    where fol.Path == folderPath
                    select fol).First();

                return folder.Id.ToString();
            }
        }
        public void RemoveSCOFolder(Guid folderID, bool deleteFromDB, string dbName, string dbServer)
        {
            RemoveSCOObject(folderID, deleteFromDB, dbName, dbServer, objectTypes.Folder);
        }
        public void RemoveSCOFolder(string folderPath, bool deleteFromDB, string dbName, string dbServer)
        {
            Guid folderID = new Guid(GetSCOFolderIDFromPath(folderPath, dbServer, dbName));
            RemoveSCOObject(folderID, deleteFromDB, dbName, dbServer, objectTypes.Folder);
        }
        public void RestoreSCOFolder(Guid folderID, string dbServer, string dbName)
        {
            UndeleteSCOObject(folderID, objectTypes.Folder, dbServer, dbName);
        }
        public void RestoreSCOFolder(string folderPath, string dbServer, string dbName)
        {
            Guid folderID = new Guid(GetSCOFolderIDFromPath(folderPath, dbServer, dbName));
            UndeleteSCOObject(folderID, objectTypes.Folder, dbServer, dbName);
        }
        public XmlDocument AddSCOFolder(Guid parentID, XmlDocument folderXML, bool overwriteExisting, string dbServer, string dbName)
        {
            importNode(parentID, folderXML, objectTypes.Folder, overwriteExisting, dbServer, dbName);

            return folderXML;
        }
        public XmlDocument AddSCOFolder(Guid parentID, string folderName, string folderDescription, bool overwriteExisting, string dbServer, string dbName)
        {
            XmlDocument folderXML = new XmlDocument();
            folderXML.Load(string.Format("<Folder>" +
                                            "<UniqueID datatype=\"string\">{0}</UniqueID>" +
                                            "<Name datatype=\"string\">{1}</Name>" +
                                            "<Description datatype=\"string\">{2}</Description>" +
                                            "<ParentID datatype=\"string\">{3}</ParentID>" +
                                            "<TimeCreated datatype=\"date\"></TimeCreated>" +
                                            "<CreatedBy datatype=\"string\"></CreatedBy>" +
                                            "<LastModified datatype=\"date\"></LastModified>" +
                                            "<LastModifiedBy datatype=\"null\"></LastModifiedBy>" +
                                            "<Disabled datatype=\"bool\">FALSE</Disabled>" +
                                         "</Folder>", Guid.NewGuid().ToString(), folderName, folderDescription));
            
            folderXML = AddSCOFolder(parentID, folderXML, overwriteExisting, dbServer, dbName);
            return folderXML;
        }
        #endregion
        #region Resource Operations
        /// <summary>
        /// Returns all resources of a given type (variable, computer group, schedule etc)
        /// </summary>
        /// <param name="baseFolderID">The base folder ID for the given resource type</param>
        /// <param name="resourceID">The resource type id to be returned</param>
        /// <returns>XML Representation of all resources of the given type</returns>
        public XmlDocument GetSCOResourceInFolder(string baseFolderID, string resourceID)
        {
            //Create a XML document representing this folder
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(OISExportString.resourceFolderStructure);
            xml.SelectSingleNode("Folder/UniqueID").InnerText = baseFolderID;

            // Load Resource Information for the resource type based and the folder passed
            XmlDocument objectDocument = new XmlDocument();
            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetResources(_handle, baseFolderID, resourceID, out outVar);

            // If data is found Load it into the XML document representing this folder
            if (outVar != null)
            {
                //Load data into a temporary xmlDocument for searching
                objectDocument.LoadXml(outVar.ToString());

                // Iterate through each object in the folder and add it to the XML representation of the folder
                foreach (XmlNode objectNode in objectDocument.SelectNodes("Objects/Object"))
                {
                    XmlNode importNode = xml.ImportNode(objectNode, true);
                    xml.SelectSingleNode("Folder/Objects").AppendChild(importNode);
                }
            }

            // Get all of the subfolders from this base folder
            XmlDocument resourceFolders = GetSCOSubFolders(baseFolderID);

            // Iterate through all of the base folders
            foreach (XmlNode folderNode in resourceFolders.SelectNodes("Folders/Folder"))
            {
                // Clean the child folder node
                _cleanFolderNode(folderNode);
                XmlDocument folderDocument = _GetSCOResourceInFolderPrivate(folderNode, resourceID);
                XmlNode importNode = xml.ImportNode(folderDocument.SelectSingleNode("Folder"), true);

                xml.SelectSingleNode("Folder").AppendChild(importNode);
            }
            return xml;
        }

        /// <summary>
        /// Returns the XML representation of a given Resource ID
        /// </summary>
        /// <param name="resourceID">ID Of resource to be retrieved</param>
        /// <returns>XML Representation of the resource</returns>
        public XmlDocument GetIndividualSCOResource(string resourceID)
        {
            XmlDocument xml = new XmlDocument();

            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.LoadResource(_handle, resourceID, out outVar);

            if (outVar != null)
            {
                xml.LoadXml(outVar.ToString());
            }
            return xml;
        }

        public XmlDocument UpdateSCOResource(Guid resourceID, string objectXML)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(objectXML);

            _scoManager.ModifyResource(_handle, resourceID.ToString(), objectXML);

            return xml;
        }

        public XmlDocument NewSCOResource(Guid parentID, ResourceFolderRoot rootFolder, string objectXML)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(objectXML);
            XmlNode objectType = xml.SelectSingleNode("//Object/ObjectType");
            object evdetails = new System.Runtime.InteropServices.VariantWrapper(xml.OuterXml);
            _scoManager.LoadResource(_handle, parentID.ToString(), out evdetails);

            if (evdetails != null)
            {
                xml.LoadXml(evdetails.ToString());
            }
            return xml;          
        }

        public XmlDocument SetSCOVariableValue(string resourceID, string value)
        {
            XmlDocument variableExport = GetIndividualSCOResource(resourceID);

            variableExport.InnerText = value;
            string varXML = variableExport.OuterXml;

            _scoManager.ModifyResource(_handle, resourceID, varXML);

            return variableExport;
        }

        #endregion
        #region Global Configuration Operations
        /// <summary>
        /// Returns a list of all configuration IDs
        /// </summary>
        /// <returns>A list of all configuration IDs</returns>
        public string[] GetSCOConfigurationIDs()
        {
            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetConfigurationIds(_handle, out outVar);

            if (outVar != null)
            {
                return (string[])outVar;
            }

            return null;
        }
        /// <summary>
        /// Returns a XML represenation of the Configuration ID passed
        /// </summary>
        /// <param name="configurationID">Configuration ID to be searched on {0D2586FB-2936-4F21-BE7F-0FCF6ED809C7}</param>
        /// <returns>XML Representation of the doc
        /// <Object GUID="{0D2586FB-2936-4F21-BE7F-0FCF6ED809C7}">
        ///   <Any>True</Any>
        /// </Object>
        /// </returns>
        public XmlDocument GetSCOConfigurationValue(string configurationID)
        {
            XmlDocument xml = new XmlDocument();

            XmlDocument configurationData = new XmlDocument();
            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
            _scoManager.GetConfigurationValues(_handle, configurationID, out outVar);

            if (outVar != null)
            {
                configurationData.LoadXml(outVar.ToString());
            }

            XmlNode objectNode = configurationData.SelectSingleNode("Object");
            XmlAttributeCollection objectNodeAttributes = objectNode.Attributes;

            string uniqueID = objectNodeAttributes["GUID"].InnerXml;

            xml.LoadXml(OISExportString.globalConfigurationStructure);

            XmlNode IDNode = xml.SelectSingleNode("Entry/ID");
            IDNode.InnerText = uniqueID;

            XmlNode DataNode = xml.SelectSingleNode("Entry/Data");
            DataNode.InnerText = objectNode.OuterXml;

            return xml;
        }
        public XmlDocument SetSCOConfigurationValue(string configurationID, string configurationXML)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(configurationXML);

            configurationID = (new Guid(configurationID)).ToString();
            _scoManager.SetConfigurationValues(Handle, configurationID, configurationXML);

            return xml;
        }

        #endregion
        #region private Methods
        private Guid _checkXMLNode(ref XmlNode xNode, bool overwriteExisting, string dbServer, string dbName, objectTypes objectType)
        {
            string name = xNode.SelectSingleNode("Name").InnerText.ToString();
            Guid id = new Guid(xNode.SelectSingleNode("UniqueID").InnerText.ToString());
            Guid parentID = new Guid(xNode.ParentNode.SelectSingleNode("UniqueID").InnerText.ToString());
            
            switch(_checkObjectDeletionStatus(id, dbName, dbServer))
            {
                case deletionStati.Active:
                    // Object is active in the destination system, we may need to change GUIDs / Names
                    switch (CompareSCOObjects(parentID, id, name, objectType, dbName, dbServer))
                    {
                        // Object Exists (Guid Active) but not in this folder -- must generate a new GUID
                        case foundStai.foundGuid:
                            id = Guid.NewGuid();
                            xNode.SelectSingleNode("UniqueID").InnerText = id.ToString();
                            break;
                        // Object Exists (Guid Active) in this folder
                        case foundStai.foundName:
                            // If we are overwritng existing entries we don't have to do anything we will just overwrite
                            if (overwriteExisting)
                            {
                            }
                            // If we are not overwriting existing then we need to generate a new GUID AND Name
                            else
                            {
                                id = Guid.NewGuid();
                                xNode.SelectSingleNode("UniqueNode").InnerText = id.ToString();

                                _generateUniqueName(ref name, dbName, dbServer, id, objectType);
                                xNode.SelectSingleNode("Name").InnerText = name;
                            }
                            break;
                        case foundStai.notFound:
                            // object doesn't exist -- shouldn't be possible given that it is active...
                            break;
                    }
                    break;
                case deletionStati.Deleted:
                    // Object is deleted in the destination system, we will hard delete it during import so we can ignore
                    break;
                case deletionStati.NotFound:
                    // No need to modify any GUIDs for this object
                    break;
            }
            return id;
        }
        private static void _deleteObjectFromDB(Guid objectID, string dbName, string dbServer, objectTypes objectType)
        {
            switch (objectType)
            {
                case objectTypes.Activity:
                    _hardDeleteObjectFromDB(objectID, dbName, dbServer);
                    break;
                case objectTypes.Counter:
                    _hardDeleteObjectFromDB(objectID, dbName, dbServer);
                    break;
                case objectTypes.Folder:
                    _hardDeleteFolderFromDB(objectID, dbName, dbServer);
                    break;
                case objectTypes.Resource:
                    _hardDeleteObjectFromDB(objectID, dbName, dbServer);
                    break;
                case objectTypes.Runbook:
                    _hardDeletePolicyFromDB(objectID, dbName, dbServer);
                    break;
                case objectTypes.Schedule:
                    _hardDeleteObjectFromDB(objectID, dbName, dbServer);
                    break;
                case objectTypes.Variable:
                    _hardDeleteObjectFromDB(objectID, dbName, dbServer);
                    break;
                default:
                    break;
            }
        }
        private static void _hardDeleteObjectFromDB(Guid objectID, string dbServer, string dbName)
        {
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<OBJECTS> objectsTable = db.GetTable<OBJECTS>();

                // Query for Policies with name
                var objRef =
                    (from obj in objectsTable
                     where obj.UniqueID == objectID
                     select obj).First();

                objectsTable.DeleteOnSubmit(objRef);
                db.SubmitChanges();
            }
        }
        private static void _hardDeleteFolderFromDB(Guid objectID, string dbServer, string dbName)
        {
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<FOLDERS> objectsTable = db.GetTable<FOLDERS>();

                // Query for Policies with name
                var objRef =
                    (from obj in objectsTable
                     where obj.UniqueID == objectID
                     select obj).First();

                objectsTable.DeleteOnSubmit(objRef);
                db.SubmitChanges();
            }
        }
        private static void _hardDeletePolicyFromDB(Guid objectID, string dbServer, string dbName)
        {
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<POLICIES> objectsTable = db.GetTable<POLICIES>();

                // Query for Policies with name
                var objRef =
                    (from obj in objectsTable
                     where obj.UniqueID == objectID
                     select obj).First();

                objectsTable.DeleteOnSubmit(objRef);
                db.SubmitChanges();
            }
        }
        private void _addSCOObject(Guid parentID, XmlDocument objectXML, Guid objectID, String objectName, objectTypes objectType)
        {
            objectXML[Enum.GetName(typeof(objectTypes), objectType)]["Name"].InnerText = objectName;
            objectXML[Enum.GetName(typeof(objectTypes), objectType)]["UniqueID"].InnerText = objectID.ToString();
            Object obj = new System.Runtime.InteropServices.VariantWrapper(objectXML.InnerXml);

            switch (objectType)
            {
                case objectTypes.Folder:
                    _scoManager.AddFolder(_handle, parentID.ToString(), ref obj);
                    break;
                case objectTypes.Activity:
                    _scoManager.AddResource(_handle, parentID.ToString(), ref obj);
                    break;
                case objectTypes.Counter:
                    _scoManager.AddResource(_handle, parentID.ToString(), ref obj);
                    break;
                case objectTypes.Resource:
                    _scoManager.AddResource(_handle, parentID.ToString(), ref obj);
                    break;
                case objectTypes.Runbook:
                    _scoManager.AddPolicy(_handle, parentID.ToString(), ref obj);
                    break;
                case objectTypes.Schedule:
                    _scoManager.AddResource(_handle, parentID.ToString(), ref obj);
                    break;
                case objectTypes.Variable:
                    _scoManager.AddResource(_handle, parentID.ToString(), ref obj);
                    break;
                default:
                    break;
            }
        }
        private static void _generateUniqueName(ref String objName, string dbName, string dbServer, Guid objectID, objectTypes objectType)
        {
            bool exists = true;
            int count = 0;
            switch (objectType)
            {
                case objectTypes.Runbook:
                    exists = _testPolicyNameExist(objName, dbName, dbServer, objectID);
                    if (exists) { _generateUniquePolicyObjectName(ref objName, dbName, dbServer, objectID, objName, ref exists, ref count); }
                    break;
                case objectTypes.Folder:
                    exists = _testFolderNameExist(objName, dbName, dbServer, objectID);
                    if (exists) { _generateUniqueFolderObjectName(ref objName, dbName, dbServer, objectID, objName, ref exists, ref count); }
                    break;
                default:
                    exists = _testObjectNameExist(objName, dbName, dbServer, objectID);
                    if (exists) { _generateUniqueObjObjectName(ref objName, dbName, dbServer, objectID, objName, ref exists, ref count); }
                    break;
            }
        }
        private static void _generateUniquePolicyObjectName(ref String objectName, string dbName, string dbServer, Guid pID, string baseName, ref bool exists, ref int count)
        {
            exists = _testPolicyNameExist(objectName, dbName, dbServer, pID);

            while (exists)
            {
                count++;
                exists = false;
                objectName = string.Format("{0} ({1})", baseName, count);
                exists = _testPolicyNameExist(objectName, dbName, dbServer, pID);
            }
        }
        private static void _generateUniqueObjObjectName(ref String objectName, string dbName, string dbServer, Guid pID, string baseName, ref bool exists, ref int count)
        {
            exists = _testObjectNameExist(objectName, dbName, dbServer, pID);

            while (exists)
            {
                count++;
                exists = false;
                objectName = string.Format("{0} ({1})", baseName, count);
                exists = _testObjectNameExist(objectName, dbName, dbServer, pID);
            }
        }
        private static void _generateUniqueFolderObjectName(ref String objectName, string dbName, string dbServer, Guid pID, string baseName, ref bool exists, ref int count)
        {
            exists = _testFolderNameExist(objectName, dbName, dbServer, pID);

            while (exists)
            {
                count++;
                exists = false;
                objectName = string.Format("{0} ({1})", baseName, count);
                exists = _testFolderNameExist(objectName, dbName, dbServer, pID);
            }
        }
        private static bool _testPolicyNameExist(String objectName, string dbName, string dbServer, Guid pID)
        {
            bool exists = false;
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<POLICIES> polTable = db.GetTable<POLICIES>();

                // Query for Policies with name
                var query =
                    from pol in polTable
                    where pol.ParentID == pID && pol.Name == objectName
                    select pol;

                if (query.Count() > 0) { exists = true; }
            }
            return exists;
        }
        private static bool _testObjectNameExist(String objectName, string dbName, string dbServer, Guid pID)
        {
            bool exists = false;
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<OBJECTS> objTable = db.GetTable<OBJECTS>();

                // Query for Policies with name
                var query =
                    from obj in objTable
                    where obj.ParentID == pID && obj.Name == objectName
                    select obj;

                if (query.Count() > 0) { exists = true; }
            }
            return exists;
        }
        private static bool _testFolderNameExist(String objectName, string dbName, string dbServer, Guid pID)
        {
            bool exists = false;
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<FOLDERS> folderTable = db.GetTable<FOLDERS>();

                // Query for Policies with name
                var query =
                    from obj in folderTable
                    where obj.ParentID == pID && obj.Name == objectName
                    select obj;

                if (query.Count() > 0) { exists = true; }
            }
            return exists;
        }
        private static deletionStati _checkObjectDeletionStatus(Guid objectID, string dbName, string dbServer)
        {
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<OBJECTS> objTable = db.GetTable<OBJECTS>();

                // Query for Policies with id
                var query =
                    from obj in objTable
                    where obj.UniqueID == objectID
                    select obj;

                if (query.Count() > 0) { if (query.First().Deleted) { return deletionStati.Deleted; } else { return deletionStati.Active; } }
                else { return deletionStati.NotFound; }
            }
        }
        private static deletionStati _checkPolicyDeletionStatus(Guid objectID, string dbName, string dbServer)
        {
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<POLICIES> objTable = db.GetTable<POLICIES>();

                // Query for Policies with id
                var query =
                    from obj in objTable
                    where obj.UniqueID == objectID
                    select obj;

                if (query.Count() > 0) { if (query.First().Deleted) { return deletionStati.Deleted; } else { return deletionStati.Active; } }
                else { return deletionStati.NotFound; }
            }
        }
        private static deletionStati _checkFolderDeletionStatus(Guid objectID, string dbName, string dbServer)
        {
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<FOLDERS> objTable = db.GetTable<FOLDERS>();

                // Query for Policies with id
                var query =
                    from obj in objTable
                    where obj.UniqueID == objectID
                    select obj;

                if (query.Count() > 0) { if (query.First().Deleted) { return deletionStati.Deleted; } else { return deletionStati.Active; } }
                else { return deletionStati.NotFound; }
            }
        }
        private static foundStai _checkFolderExistance(Guid parentID, Guid objectID, string objectName, string dbName, string dbServer)
        {
            foundStai exists;
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<FOLDERS> objTable = db.GetTable<FOLDERS>();
                // No match found - try searching on name instead of GUID
                var nameQuery =
                    from obj in objTable
                    where obj.Name == objectName && obj.ParentID == parentID
                    select obj;

                if (nameQuery.Count() > 0)
                {
                    // Macth found by name
                    exists = foundStai.foundName;
                }
                
                else
                {
                    // Query for Policies with id
                    var guidQUery =
                         from obj in objTable
                         where obj.UniqueID == objectID && obj.ParentID == parentID
                         select obj;


                    if (guidQUery.Count() > 0)
                    {
                        // Match Exists in given Path
                        exists = foundStai.foundGuid;
                    }
                
                    else
                    {
                        // No Match Found
                        exists = foundStai.notFound;
                    }
                }
            }
            return exists;
        }
        private static foundStai _checkPolicyExistance(Guid parentID, Guid objectID, string objectName, string dbName, string dbServer)
        {
            foundStai exists;
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<POLICIES> objTable = db.GetTable<POLICIES>();
                

                // Query for Policies with id
                var guidQUery =
                        from obj in objTable
                        where obj.UniqueID == objectID && obj.ParentID == parentID
                        select obj;


                if (guidQUery.Count() > 0)
                {
                    // Match Exists in given Path
                    exists = foundStai.foundGuid;
                }

                else
                {

                    // No match found - try searching on name instead of GUID
                    var nameQuery =
                        from obj in objTable
                        where obj.Name == objectName && obj.ParentID == parentID
                        select obj;

                    if (nameQuery.Count() > 0)
                    {
                        // Macth found by name
                        exists = foundStai.foundName;
                    }   
                    
                    else
                    {
                        // No Match Found
                        exists = foundStai.notFound;
                    }
                }
            }
            return exists;
        }
        private static foundStai _checkObjectExistance(Guid parentID, Guid objectID, string objectName, string dbName, string dbServer)
        {
            foundStai exists;
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<OBJECTS> objTable = db.GetTable<OBJECTS>();
              
                // Query for Policies with id
                var guidQUery =
                        from obj in objTable
                        where obj.UniqueID == objectID && obj.ParentID == parentID
                        select obj;


                if (guidQUery.Count() > 0)
                {
                    // Match Exists in given Path
                    exists = foundStai.foundGuid;
                }
                else
                {
                    // No match found - try searching on name instead of GUID
                    var nameQuery =
                        from obj in objTable
                        where obj.Name == objectName && obj.ParentID == parentID
                        select obj;

                    if (nameQuery.Count() > 0)
                    {
                        // Macth found by name
                        exists = foundStai.foundName;
                    }

                    else
                    {
                        // No Match Found
                        exists = foundStai.notFound;
                    }
                }
            }
            return exists;
        }
        private static void _undeleteObject(Guid objectID, string dbName, string dbServer)
        {
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<OBJECTS> objTable = db.GetTable<OBJECTS>();

                // Query for Policies with id
                var objRef =
                    (from obj in objTable
                     where obj.UniqueID == objectID
                     select obj).First();

                objRef.Deleted = false;
                db.SubmitChanges();
            }
        }
        private static void _undeletePolicy(Guid objectID, string dbName, string dbServer)
        {
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<POLICIES> objTable = db.GetTable<POLICIES>();

                // Query for Policies with id
                var objRef =
                    (from obj in objTable
                     where obj.UniqueID == objectID
                     select obj).First();

                objRef.Deleted = false;
                db.SubmitChanges();
            }
        }
        private static void _undeleteFolder(Guid objectID, string dbName, string dbServer)
        {
            // Setup connection to DB
            SqlConnectionStringBuilder connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = dbServer;
            connStringBuilder.InitialCatalog = dbName;
            connStringBuilder.IntegratedSecurity = true;
            OrchestratorDB db = new OrchestratorDB(connStringBuilder.ConnectionString);

            using (db)
            {
                // Get a typed table to run queries
                Table<POLICIES> objTable = db.GetTable<POLICIES>();
                
                // Query for Policies with id
                var objRef =
                    (from obj in objTable
                     where obj.UniqueID == objectID
                     select obj).First();

                objRef.Deleted = false;
                db.SubmitChanges();
            }
        }
        private void _cleanFolderNode(XmlNode folder)
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
        /// <summary>
        /// Recursively retrieves all resources of a given type
        /// </summary>
        /// <param name="folderNode">The base folder for the given resource type</param>
        /// <param name="resourceID">The ID for the resource to be retrieved</param>
        /// <returns>XML Representation of the resource type for an environment</returns>
        private XmlDocument _GetSCOResourceInFolderPrivate(XmlNode folderNode, string resourceID)
        {
            //Create a XML document representing this folder
            XmlDocument folderXml = new XmlDocument();
            folderXml.LoadXml(folderNode.OuterXml);

            // Load Resource Information for the resource type based and the folder passed
            XmlDocument objectDocument = new XmlDocument();
            object odetails = new object();
            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);

            // Find this folder's unique ID
            string uniqueFolderID = folderXml.SelectSingleNode("Folder/UniqueID").InnerText;
            _scoManager.GetResources(_handle, uniqueFolderID, resourceID, out outVar);

            // If data is found Load it into the XML document representing this folder
            if (outVar != null)
            {
                //Load data into a temporary xmlDocument for searching
                objectDocument.LoadXml(outVar.ToString());
                XmlNode importNode = folderXml.ImportNode(objectDocument.SelectSingleNode("Objects"), true);
                folderXml.SelectSingleNode("Folder").AppendChild(importNode);
            }

            // Get all of the subfolders from this base folder
            XmlDocument resourceFolders = GetSCOSubFolders(uniqueFolderID);

            // Iterate through all of the base folders
            foreach (XmlNode fNode in resourceFolders.SelectNodes("Folders/Folder"))
            {
                _cleanFolderNode(fNode);
                XmlDocument folderDocument = _GetSCOResourceInFolderPrivate(fNode, resourceID);
                XmlNode importNode = folderXml.ImportNode(folderDocument.SelectSingleNode("Folder"), true);

                folderXml.SelectSingleNode("Folder").AppendChild(importNode);
            }
            return folderXml;
        }
        /// <summary>
        /// Called by GetSCOFolderPath recursviely to find subfolder contents
        /// </summary>
        /// <param name="folderPath">Folder Path of which to return contents</param>
        /// <param name="baseDoc">The base document to add this node to</param>
        /// <returns>An XML representation of the folder and sub folders / policies</returns>
        private XmlDocument _GetSCOFolderByPath(string folderPath, XmlDocument baseDoc)
        {
            XmlDocument xml = new XmlDocument();

            try
            {
                if (folderPath.Contains("\\"))
                {
                    string pFolder = folderPath.Split('\\')[0];
                    folderPath = folderPath.Substring(folderPath.IndexOf('\\') + 1);

                    foreach (XmlNode folderNode in baseDoc.SelectNodes("Folders/Folder/Name"))
                    {
                        if (folderNode.InnerText.Equals(pFolder))
                        {
                            object odetails = new object();
                            object outVar = new System.Runtime.InteropServices.VariantWrapper(odetails);
                            _scoManager.GetFolders(_handle, folderNode.ParentNode["UniqueID"].InnerText, out outVar);

                            if (outVar != null)
                            {
                                xml.LoadXml(outVar.ToString());
                            }

                            xml = _GetSCOFolderByPath(folderPath, xml);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (XmlNode folderNode in baseDoc.SelectNodes("Folders/Folder/Name"))
                    {
                        if (folderNode.InnerText.Equals(folderPath))
                        {
                            xml.LoadXml(folderNode.ParentNode.OuterXml);
                            return xml;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return xml;
        }
        #endregion
    }
}

