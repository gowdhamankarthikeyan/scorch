using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using System.Xml;
using System.Management;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.IntegrationPack;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class IPUtilities
    {

        private static readonly string _scoCommonFilesFolder = GetScoCommonFilesDirectory();
        private static readonly string _managementServerFolder = Path.Combine(_scoCommonFilesFolder, @"Management Server");
        private static readonly string _extensionsPath = Path.Combine(_scoCommonFilesFolder, @"Extensions");
        private static readonly string _stringsPath = Path.Combine(_scoCommonFilesFolder, @"Strings");
        private static readonly string _ipBasePath = Path.Combine(_scoCommonFilesFolder, @"Support\Integration Toolkit");
        private static readonly string _packsPath = Path.Combine(_managementServerFolder, @"Components\Packs");
        private static readonly string _msiPath = Path.Combine(_managementServerFolder, @"Components\Objects");
        private static readonly string _ipsRegistryKey = @"SOFTWARE\Wow6432Node\Microsoft\SystemCenter2012\Orchestrator\IPs";
        private static SqlConnection _scoConnection;

        #region Public Properties

        public static string ExtensionsPath
        {
            get { return _extensionsPath; }
        }

        public static string StringsPath
        {
            get { return _stringsPath; }
        }

        public static string IpBasePath
        {
            get { return _ipBasePath; }
        }

        public static string PacksPath
        {
            get { return _packsPath; }
        }

        public static string MsiPath
        {
            get { return _msiPath; }
        }

        #endregion

        

        #region Get File / Directory Paths

        private static string GetScoCommonFilesDirectory()
        {
            return GetScoCommonFilesDirectory(null);
        }

        private static string GetScoCommonFilesDirectory(string managementServerName)
        {
            RegistryKey key;
            if ((string.IsNullOrEmpty(managementServerName) || (Utilities.IsLocalComputer(managementServerName))))
            {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, managementServerName, RegistryView.Registry64);
            }
            RegistryKey commonFilesKey = key.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\SystemCenter2012\Orchestrator\Standard Activities");
            string dir = commonFilesKey.GetValue("InstallLocation").ToString();

            return dir;
        }

        private static string GetScoManagementServerDirectory()
        {
            return GetScoManagementServerDirectory(null);
        }

        private static string GetScoManagementServerDirectory(string managementServerName)
        {
            RegistryKey key;
            if ((string.IsNullOrEmpty(managementServerName) || (Utilities.IsLocalComputer(managementServerName))))
            {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, managementServerName, RegistryView.Registry64);
            } 
            RegistryKey mgmtServerKey = key.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\SystemCenter2012\Orchestrator\Management Server");
            string dir = mgmtServerKey.GetValue("InstallLocation").ToString();

            return dir;
        }

        public static string GetObjectsXmlFilePath(string packID)
        {
            return GetObjectsXmlFilePath(packID, null);
        }
   
        public static string GetObjectsXmlFilePath(string packID, string managementServerName)
        {
            string objectsFile = string.Empty;

            if ((string.IsNullOrEmpty(managementServerName) || (Utilities.IsLocalComputer(managementServerName))))
            {
                objectsFile = Path.Combine(_scoCommonFilesFolder, _extensionsPath, packID + "Objects.Xml");
            }
            else
            {
                objectsFile = Path.Combine(_scoCommonFilesFolder, _extensionsPath, packID + "Objects.Xml"); 
            }

            if (File.Exists(objectsFile))
            {
                return objectsFile;
            }

            string filePath = Path.Combine(_scoCommonFilesFolder, _extensionsPath);
            foreach (string xmlFile in Directory.EnumerateFiles(filePath, "*.XML"))
            {
                if (Utilities.FindStringInFile(xmlFile, string.Format("{0}", packID)))
                {
                    return xmlFile;
                }
            }
            
            return null;
        }

        public static string GetOIPFilePath(string packID)
        {
            return GetOIPFilePath(packID, null);
        }

        public static string GetOIPFilePath(string packID, string managementServerName)
        {
            string oipFile = Path.Combine(_scoCommonFilesFolder, _packsPath, GuidUtilities.AddBracesToGuid(packID) + ".oip");
            if ((string.IsNullOrEmpty(managementServerName) || (Utilities.IsLocalComputer(managementServerName))))
            {
                if (File.Exists(oipFile))
                {
                    return oipFile;
                }
            }
            else
            {
                if (File.Exists(oipFile))
                {
                    return oipFile;
                }
            } 
            return null;
        }

        public static string GetMSIFilePath(string fileName)
        {
            return GetMSIFilePath(fileName, null);
        }

        public static string GetMSIFilePath(string fileName, string managementServerName)
        {
            string msiFile = Path.Combine(_scoCommonFilesFolder, _msiPath, fileName);
            if (File.Exists(msiFile))
            {
                return msiFile;
            }
            return null;
        }

        #endregion

 
        #region IP Registration / Deployment 


        public static string GetProductCodeForIP(string packID, SqlConnection scoConnection)
        {
            string productCode = string.Empty;
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select ProductID from [Orchestrator].[dbo].[CAPS] Where UniqueID = '{0}'", packID), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                productCode = myReader[0].ToString();
                continue;
            }
            myReader.Close();
            return productCode;
        }

        public static bool IpIsRegistered(string packID)
        {
            // An IP is "Registered" when it's in the database and the OIP file exists in the Common Files path. 
            // When you unregister an IP, all it does is delete the file.
            string filename = GetOIPFilePath(packID);
            if (!string.IsNullOrEmpty(filename))
            {
                return true;
            }
            return false;
        }

        public static bool IpIsDeployed(string productCode, string computerName)
        {
            // check this key: HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\SystemCenter2012\Orchestrator\IPs
            // and enumerate the subkeys to find the one matching the programcode (this key is set when msi is installed)

            RegistryKey key;

            if ((string.IsNullOrEmpty(computerName) || (Utilities.IsLocalComputer(computerName))))
            {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName, RegistryView.Registry64);
            }
            RegistryKey ipKey = key.OpenSubKey(_ipsRegistryKey);

            String[] ValueNames = ipKey.GetValueNames();
            foreach (string name in ValueNames)
            {
                string value = ipKey.GetValue(name).ToString();
                if (string.Compare(value, GuidUtilities.AddBracesToGuid(productCode), true) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetRegisteredIPVersion(string packID, SqlConnection scoConnection)
        {
            string version = string.Empty;
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select Version from [Orchestrator].[dbo].[CAPS] Where UniqueID = '{0}'", packID), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                version = Decimal.Round(Decimal.Parse(myReader[0].ToString()), 1).ToString();
                continue;
            }
            myReader.Close();
            return version;
        }

        public static string GetDeployedIpVersion(string productCode, string computerName)
        {
            string version = string.Empty;

            string uninstallKey = GuidUtilities.AddBracesToGuid(productCode);

            RegistryKey key;

            if ((string.IsNullOrEmpty(computerName) || (Utilities.IsLocalComputer(computerName))))
            {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }
            else
            {
                key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName, RegistryView.Registry64);
            }
            string baseKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\";
            RegistryKey ipKey = key.OpenSubKey(baseKey + uninstallKey);
            if (null == ipKey)
            {
                return string.Empty;
            }
            object versionObj = ipKey.GetValue("DisplayVersion");
            if (null != versionObj)
            {
                return versionObj.ToString();
            }
            return string.Empty;
        }

        public static bool UnregisterIP(string packID, string computerName, ConnectionOptions connectionOptions)
        {
            string oipFile = IPUtilities.GetOIPFilePath(packID);
            return WMIUtilities.DeleteFileUsingWMI(oipFile, computerName, connectionOptions);
        }


        #endregion


        #region IP Removal

        public static int RemoveCapDataForIP(string packID, SqlConnection scoConnection)
        {
            packID = GuidUtilities.RemoveBracesFromGuid(packID);
            string queryString = string.Format("DELETE FROM [Orchestrator].[dbo].[CAPS] Where UniqueID = '{0}'", packID);
            return SQLUtilities.RunDeleteQuery(queryString, scoConnection);
        }

        public static int RemoveConfigurationDataForIP(string packID, SqlConnection scoConnection)
        {
            packID = GuidUtilities.RemoveBracesFromGuid(packID);
            string queryString = string.Format("DELETE FROM [Orchestrator].[dbo].[CONFIGURATION] Where AND DataValue like '%{0}%' ", packID);
            return SQLUtilities.RunDeleteQuery(queryString, scoConnection);
        }

        public static int RemoveActivityType(string typeID, SqlConnection scoConnection, bool replaceWithUnknown, bool deletedOnly)
        {

            //TODO: Also need to remove QIKOBJECT instances
            int numAffected = 0;
            string whereClause = string.Empty;
            if (deletedOnly)
            {
                whereClause = string.Format("WHERE obj.ObjectType = '{0}' AND obj.Deleted = 1)", typeID);
            }
            else
            {
                whereClause = string.Format("WHERE obj.ObjectType = '{0}')", typeID);
            }

            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);
            if (replaceWithUnknown)
            {
                sb.Append("UPDATE [Orchestrator].[dbo].[OBJECTS] obj");
                sb.Append("SET obj.ObjectType = 'DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD', set obj.Enabled = 0");
                sb.Append(whereClause);
            }
            else
            {
                sb.Append("DELETE FROM [Orchestrator].[dbo].[OBJECTS]");
                sb.Append(whereClause);
            }
            numAffected += SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
            return numAffected;
        }

        public static int RemoveLoopsForActivityType(string typeID, SqlConnection scoConnection)
        {
            return RemoveLoopsForActivityType(typeID, scoConnection, false);
        }

        public static int RemoveLoopsForActivityType(string typeID, SqlConnection scoConnection, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);

            sb.Append("DELETE FROM [Orchestrator].[dbo].[OBJECTLOOPING]");
            sb.Append("WHERE UniqueID IN");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));

            return SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
        }

        public static int RemoveAuditDataForActivityType(string typeID, SqlConnection scoConnection)
        {
            return RemoveAuditDataForActivityType(typeID, scoConnection, false);
        }

        public static int RemoveAuditDataForActivityType(string typeID, SqlConnection scoConnection, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);

            sb.Append("DELETE FROM [Orchestrator].[dbo].[OBJECT_AUDIT]");
            sb.Append("WHERE ObjectID IN");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));


            return SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
        }

        public static int RemoveLinksForActivityType(string typeID, SqlConnection scoConnection, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE FROM [Orchestrator].[dbo].[LINKS]");
            sb.Append("WHERE SourceObject IN");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));
            sb.Append("OR TargetObject IN");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));

            int numAffected = SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
            return numAffected;
        }

        private static string GetSelectObjectTypeSubQueryString(string typeID, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);

            sb.Append("(SELECT obj.UniqueID FROM [Orchestrator].[dbo].[OBJECTS] obj");

            if (deletedOnly)
            {
                sb.Append(string.Format("WHERE obj.ObjectType = '{0}' AND obj.Deleted = 1)", typeID));
            }
            else
            {
                sb.Append(string.Format("WHERE obj.ObjectType = '{0}')", typeID));
            }
            return sb.ToString();
        }

        public static int RemoveLinkTriggersForActivityType(string typeID, SqlConnection scoConnection, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);

            sb.Append("DELETE FROM [Orchestrator].[dbo].[TRIGGERS]");
            sb.Append("WHERE ParentID IN");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));

            return SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
        }

        public static int RemoveInstancesOfActivityType(string typeID, SqlConnection scoConnection)
        {
            return RemoveInstancesOfActivityType(typeID, scoConnection, false);
        }

        public static int RemoveInstancesOfActivityType(string typeID, SqlConnection scoConnection, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);

            sb.Append(" DELETE FROM [Orchestrator].[dbo].[OBJECTINSTANCES]");
            sb.Append(" WHERE ObjectID IN");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));


            return SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
        }

        public static int RemoveJobDataForActivityType(string typeID, SqlConnection scoConnection)
        {
            return RemoveJobDataForActivityType(typeID, scoConnection, false);
        }

        public static int RemoveJobDataForActivityType(string typeID, SqlConnection scoConnection, bool deletedOnly)
        {
            StringBuilder sb = new StringBuilder();
            typeID = GuidUtilities.RemoveBracesFromGuid(typeID);

            sb.Append(" DELETE FROM [Orchestrator].[dbo].[OBJECTINSTANCEDATA] ");
            sb.Append(" WHERE ObjectInstanceID IN ");
            sb.Append(" (SELECT UniqueID FROM [Orchestrator].[dbo].[OBJECTINSTANCES] ");
            sb.Append("  WHERE ObjectID IN ");
            sb.Append(GetSelectObjectTypeSubQueryString(typeID, deletedOnly));
            sb.Append(string.Format(")", typeID));
            return SQLUtilities.RunDeleteQuery(sb.ToString(), scoConnection);
        }

 

        #endregion


        #region IP Information



        public static Dictionary<string, string> GetAllIpInfo(SqlConnection connection)
        {

            Dictionary<string, string> ipList = new Dictionary<string, string>();

            //get a list of all IPs from the CAPS table
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from [Orchestrator].[dbo].[CAPS]", connection);
            myReader = myCommand.ExecuteReader();
            //read the list
            while (myReader.Read())
            {
                string ipName = myReader["Name"].ToString();
                string packID = myReader["UniqueID"].ToString();
                if (!ipList.ContainsKey(ipName))
                {
                    ipList.Add(ipName, packID);
                }
            }
            myReader.Close();

            return ipList;
        }

        public static int GetNumberOfActivitiesInIP(string packID, SqlConnection scoConnection, IPType ipType)
        {
            if (ipType == IPType.Toolkit) 
            {
                return GetNumberOfActivitiesInToolkitIP(packID, scoConnection);
            }
            else if ((ipType == IPType.Native) || (ipType == IPType.Unknown))
            {
                return GetNumberOfActivitiesInNativeIP(packID);
            }
            return 0;
        }

        public static int GetNumberOfActivitiesInToolkitIP(string packID, SqlConnection scoConnection)
        {
            int activities = 0;
            StringBuilder queryString = new StringBuilder();

            queryString.Append("select COUNT(*) from [Orchestrator].[dbo].[CONFIGURATION] ");
            queryString.Append(string.Format("Where DataName = 'QIKOBJECT' AND DataValue like '%{0}%'", packID));

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(queryString.ToString(), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                activities = Int32.Parse(myReader[0].ToString());
            }
            myReader.Close();
            return activities;
        }

        public static int GetNumberOfActivitiesInNativeIP(string packID)
        {
            int activities = 0;
            string objectsFile = GetObjectsXmlFilePath(packID);
            if (string.IsNullOrEmpty(objectsFile))
            {
                return 0;
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(objectsFile);
            XmlNodeList activitiesNodes = xml.SelectNodes("//Object/ObjectType");
            activities = (activitiesNodes == null) ? 0 : activitiesNodes.Count;

            return activities;
        }

        public static Dictionary<string, string> GetActivitesInIP(string packID)
        {
            string objectsXmlFile = GetObjectsXmlFilePath(packID);
            if (string.IsNullOrEmpty(objectsXmlFile))
            {
                return null;
            }

            XmlDocument xml = new XmlDocument();
            xml.Load(objectsXmlFile);
            Dictionary<string, string> activities = new Dictionary<string, string>();

            XmlNodeList activityNodes = xml.SelectNodes("//Objects/Object");
            foreach (XmlNode activityNode in activityNodes)
            {
                string activityID = activityNode.SelectSingleNode("./ObjectType").InnerText;
                string activityName = activityNode.SelectSingleNode("./Name").InnerText;
                activities.Add(activityName, activityID);
            }

            return activities;

        }

        public static IPType GetIPType(string packID, SqlConnection scoConnection)
        {
            // The first part queries the CONFIGURATION table to see if anything exists for this IP. This query only
            // works for IPs that use am ActivityConfiguration type (the options menu). If not found, the second part
            // does a double-check to see if an objects XML file exists for the IP. Since it's possible for an IP to
            // use an Objects XML file that is not named <GUID>Objects.Xml (but is the default behavior for IPs created
            // using the wizard), the query is used first.
            IPType ipType = IPType.Unknown;

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(string.Format("select * from [Orchestrator].[dbo].[CONFIGURATION] Where TypeGUID = '{0}'", packID), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                ipType = IPType.Toolkit;
                continue;
            }
            myReader.Close();

            if (ipType == IPType.Unknown)
            {
                string objectsFile = GetObjectsXmlFilePath(packID);
                if (!string.IsNullOrEmpty(objectsFile))
                {
                    ipType = (Utilities.FindStringInFile(objectsFile, "QIKObjects")) ? IPType.Toolkit : IPType.Native;
                }
            }
            return ipType;
        }

 

        #endregion


        #region Activities In Runbooks

        public static string BuildActivityTypesListForQuery(string packID)
        {
            StringBuilder queryString = new StringBuilder();

            Dictionary<string, string> activitiesList = new Dictionary<string, string>();
            activitiesList = GetActivitesInIP(packID);
            if (activitiesList != null)
            {
                int i = 0;
                queryString.Append(" (");
                foreach (string activityID in activitiesList.Values)
                {
                    if (i > 0) { queryString.Append(", "); }
                    queryString.Append(string.Format("'{0}'", GuidUtilities.RemoveBracesFromGuid(activityID)));
                    i++;
                }
                queryString.Append(")");
                return queryString.ToString();
            }
            else
            {
                Dictionary<string, string> aList = new Dictionary<string, string>();
                aList = GetActivitesInIP(packID);
                int i = 0;
                queryString.Append(" (");
                foreach (string activityID in aList.Values)
                {
                    if (i > 0) { queryString.Append(", "); }
                    queryString.Append(string.Format("'{0}'", GuidUtilities.RemoveBracesFromGuid(activityID)));
                    i++;
                }
                queryString.Append(")");
                return queryString.ToString();
            }

        }

        public static int GetActivityInstancesCountForIP(string packID, SqlConnection scoConnection, IPType ipType)
        {
            return GetActivityInstancesCountForIP(packID, scoConnection, ipType, false);
        }

        public static int GetActivityInstancesCountForIP(string packID, SqlConnection scoConnection, IPType ipType, bool deletedOnly)
        {
            int instanceCount = 0;
            StringBuilder queryString = new StringBuilder();

            queryString.Append("Select COUNT(*) FROM [Orchestrator].[dbo].[OBJECTS] WHERE ObjectType IN ");
            if (ipType == IPType.Toolkit)
            {
                queryString.Append(" (Select TypeGUID from [Orchestrator].[dbo].[CONFIGURATION] ");
                queryString.Append(string.Format("Where DataName = 'QIKOBJECT' AND DataValue like '%{0}%')", packID));
            }
            else if ((ipType == IPType.Native) || (ipType == IPType.Unknown))
            {
                queryString.Append(BuildActivityTypesListForQuery(packID));
            }
            if (deletedOnly)
            {
                queryString.Append(" AND Deleted = 1");
            }

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(queryString.ToString(), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                instanceCount = Int32.Parse(myReader[0].ToString());
            }
            myReader.Close();
            return instanceCount;
        }

        public static List<string> GetRunbookNamesWhereActivityTypeIsUsed(string typeID, SqlConnection scoConnection)
        {
            List<string> runbookList = new List<string>();
            StringBuilder queryString = new StringBuilder();

            queryString.Append("Select DISTINCT Name FROM [Orchestrator].[dbo].[POLICIES] WHERE UniqueID IN ");
            queryString.Append("(Select ParentID from [Orchestrator].[dbo].[OBJECTS] ");
            queryString.Append(string.Format("Where ObjectType = '{0}')", GuidUtilities.RemoveBracesFromGuid(typeID)));

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(queryString.ToString(), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                //insert into list
                runbookList.Add(myReader[0].ToString());
            }
            myReader.Close();
            return runbookList;
        }



        #endregion


        #region Activities in Jobs

        public static int GetActivityInstanceDataCountForIP(string packID, SqlConnection scoConnection, IPType ipType)
        {
            return GetActivityInstanceDataCountForIP(packID, scoConnection, ipType, false);
        }

        public static int GetActivityInstanceDataCountForIP(string packID, SqlConnection scoConnection, IPType ipType, bool deletedOnly)
        {
            int instanceCount = 0;
            StringBuilder queryString = new StringBuilder();


            queryString.Append("Select COUNT(*) FROM [Orchestrator].[dbo].[OBJECTINSTANCEDATA] oid ");
            queryString.Append("JOIN [Orchestrator].[dbo].[OBJECTINSTANCES] oi on oid.ObjectInstanceID = oi.InstanceID ");
            queryString.Append("JOIN [Orchestrator].[dbo].[OBJECTS] obj on oi.ObjectID = obj.UniqueID "); 
            if (ipType == IPType.Toolkit) 
            {
                queryString.Append("JOIN [Orchestrator].[dbo].[CONFIGURATION] cfg on obj.ObjectType = cfg.TypeGUID ");
                queryString.Append("Where cfg.DataName = 'QIKOBJECT' ");
                queryString.Append(string.Format("AND DataValue like '%{0}%' ", packID));
            }
            else if ((ipType == IPType.Native) || (ipType == IPType.Unknown))
            {
                queryString.Append("Where obj.ObjectType ");
                queryString.Append(BuildActivityTypesListForQuery(packID));
            }
            if (deletedOnly)
            {
                queryString.Append(" AND obj.Deleted = 1");
            }

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(queryString.ToString(), scoConnection);
            myReader = myCommand.ExecuteReader();

            //read the list
            while (myReader.Read())
            {
                instanceCount = Int32.Parse(myReader[0].ToString());
            }
            myReader.Close();
            return instanceCount;
        }

        #endregion


    }
}
