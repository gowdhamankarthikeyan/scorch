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
    [ActivityData("User Group")]
    public class userGroup
    {
        private String activeDirectoryDomain;
        private String activeDirectoryOrganizationalUnit;
        private String agentName;
        private String agentSite;
        private String agentTime;
        private String creationDate;
        private String name;
        private String networkOperatingSystem;
        private int resourceID;
        private int resourceType;
        private String sid;
        private String uniqueUsergroupName;
        private String usergroupName;
        private String windowsNTDomain;

        internal userGroup(IResultObject userGroup)
        {
            userGroup.Get();
            this.activeDirectoryDomain = nullStringHandler(userGroup,"ActiveDirectoryDomain");
            this.activeDirectoryOrganizationalUnit = convertStringArray(userGroup["ActiveDirectoryOrganizationalUnit"].StringArrayValue);
            this.agentName = convertStringArray(userGroup["AgentName"].StringArrayValue);
            this.agentSite = convertStringArray(userGroup["AgentSite"].StringArrayValue);
            this.agentTime = convertDateTimeArray(userGroup["AgentTime"].DateTimeArrayValue);
            this.name = nullDateTimeHandler(userGroup, "CreationDate");
            this.activeDirectoryDomain = nullStringHandler(userGroup, "Name");
            this.networkOperatingSystem = nullStringHandler(userGroup, "NetworkOperatingSystemName");
            this.resourceID = nullIntHandler(userGroup, "ResourceId");
            this.resourceType = nullIntHandler(userGroup, "ResourceType");
            this.sid = nullStringHandler(userGroup, "SID");
            this.uniqueUsergroupName = nullStringHandler(userGroup, "UniqueUsergroupName");
            this.usergroupName = nullStringHandler(userGroup, "UsergroupName");
            this.windowsNTDomain = nullStringHandler(userGroup, "WindowsNTDomain");
        }

        [ActivityOutput, ActivityFilter]
        public String ActiveDirectoryDomain
        {
            get { return activeDirectoryDomain; }
        }
        [ActivityOutput, ActivityFilter]
        public String ActiveDirectoryOrganizationalUnit
        {
            get { return activeDirectoryOrganizationalUnit; }
        }
        [ActivityOutput, ActivityFilter]
        public String AgentName
        {
            get { return agentName; }
        }
        [ActivityOutput, ActivityFilter]
        public String AgentSite
        {
            get { return agentSite; }
        }
        [ActivityOutput, ActivityFilter]
        public String AgentTime
        {
            get { return agentTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String CreationDate
        {
            get { return creationDate; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }
        [ActivityOutput, ActivityFilter]
        public String NetworkOperatingSystem
        {
            get { return networkOperatingSystem; }
        }
        [ActivityOutput, ActivityFilter]
        public int ResourceID
        {
            get { return resourceID; }
        }
        [ActivityOutput, ActivityFilter]
        public int ResourceType
        {
            get { return resourceType; }
        }
        [ActivityOutput, ActivityFilter]
        public String SID
        {
            get { return sid; }
        }
        [ActivityOutput, ActivityFilter]
        public String UniqueUsergroupName
        {
            get { return uniqueUsergroupName; }
        }
        [ActivityOutput, ActivityFilter]
        public String UsergroupName
        {
            get { return usergroupName; }
        }
        [ActivityOutput, ActivityFilter]
        public String WindowsNTDomain
        {
            get { return windowsNTDomain; }
        }

        private static String convertStringArray(String[] sArray)
        {
            String retString = String.Empty;
            if (sArray != null)
            {
                foreach (String str in sArray)
                {
                    if (retString.Equals(String.Empty))
                    {
                        retString = str;
                    }
                    else { retString = retString + "," + str; }
                }
            }
            return retString;
        }
        private static String convertIntArray(int[] iArray)
        {
            String retString = String.Empty;
            if (iArray != null)
            {
                foreach (int i in iArray)
                {
                    if (retString.Equals(String.Empty))
                    {
                        retString = i.ToString();
                    }
                    else { retString = retString + "," + i.ToString(); }
                }
            }
            return retString;
        }
        private static String convertDateTimeArray(DateTime[] dArray)
        {
            String retString = String.Empty;
            if (dArray != null)
            {
                foreach (DateTime str in dArray)
                {
                    if (retString.Equals(String.Empty))
                    {
                        retString = str.ToString();
                    }
                    else { retString = retString + "," + str.ToString(); }
                }
            }
            return retString;
        }
        private static int nullIntHandler(IResultObject obj, String variableName)
        {
            int retValue = -1;
            try { retValue = obj[variableName].IntegerValue; }
            catch { }
            return retValue;
        }
        private static String nullDateTimeHandler(IResultObject obj, String variableName)
        {
            String retValue = DateTime.MinValue.ToString();
            try { retValue = obj[variableName].DateTimeValue.ToString(); }
            catch { }
            return retValue;
        }
        private static String nullStringHandler(IResultObject obj, String variableName)
        {
            String retValue = String.Empty; ;
            try { retValue = obj[variableName].StringValue; }
            catch { }
            return retValue;
        }
        private static String convertIntToString(IResultObject obj, String variableName)
        {
            String retValue = String.Empty;
            try { retValue = obj[variableName].StringValue; }
            catch { }
            return retValue;
        }
    }
}

