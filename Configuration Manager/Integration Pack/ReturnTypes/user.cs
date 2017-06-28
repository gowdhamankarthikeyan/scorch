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
    [ActivityData("User Group")]
    public class user
    {
        private String agentName;
        private String agentSite;
        private String agentTime;
        private String creationDate;
        private String distinguishedName;
        private String fullUserName;
        private String lastLogonTimestamp;
        private String name;
        private String networkOperatingSystem;
        private int primaryGroupID;
        private int resourceId;
        private int resourceType;
        private String uniqueUserName;
        private int userAccountControl;
        private String userContainerName;
        private String userGroupName;
        private String userName;
        private String userOUName;
        private String windowsNTDomain;

        internal user(IResultObject user)
        {
            user.Get();
            this.agentName = convertStringArray(user["AgentName"].StringArrayValue);
            this.agentSite = convertStringArray(user["AgentSite"].StringArrayValue);
            this.agentTime = convertDateTimeArray(user["AgentTime"].DateTimeArrayValue);
            this.creationDate = nullDateTimeHandler(user, "CreationDate");
            this.distinguishedName = nullStringHandler(user, "distinguishedName");
            this.fullUserName = nullStringHandler(user, "FullUserName");
            this.lastLogonTimestamp = convertIntToString(user, "lastLogonTimestamp");
            this.name = nullStringHandler(user, "Name");
            this.networkOperatingSystem = nullStringHandler(user, "NetworkOperatingSystemName");
            this.primaryGroupID = nullIntHandler(user, "PrimaryGroupID");
            this.resourceId = nullIntHandler(user, "ResourceId");
            this.resourceType = nullIntHandler(user, "ResourceType");
            this.uniqueUserName = nullStringHandler(user, "UniqueUserName");
            this.userAccountControl = nullIntHandler(user, "UserAccountControl");
            this.userContainerName = convertStringArray(user["UserContainerName"].StringArrayValue);
            this.userGroupName = convertStringArray(user["UserGroupName"].StringArrayValue);
            this.userName = nullStringHandler(user, "UserName");
            this.userOUName = convertStringArray(user["UserOUName"].StringArrayValue);
            this.windowsNTDomain = nullStringHandler(user, "WindowsNTDomain");
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
        public String DistinguishedName
        {
            get { return distinguishedName; }
        }
        [ActivityOutput, ActivityFilter]
        public String FullUserName
        {
            get { return fullUserName; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastLogonTimestamp
        {
            get { return lastLogonTimestamp; }
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
        public int PrimaryGroupID
        {
            get { return primaryGroupID; }
        }

        [ActivityOutput, ActivityFilter]
        public int ResourceId
        {
            get { return resourceId; }
        }
        [ActivityOutput, ActivityFilter]
        public int ResourceType
        {
            get { return resourceType; }
        }
        [ActivityOutput, ActivityFilter]
        public String UniqueUserName
        {
            get { return uniqueUserName; }
        }
        [ActivityOutput, ActivityFilter]
        public int UserAccountControl
        {
            get { return userAccountControl; }
        }
        [ActivityOutput, ActivityFilter]
        public String UserContainerName
        {
            get { return userContainerName; }
        }
        [ActivityOutput, ActivityFilter]
        public String UserGroupName
        {
            get { return userGroupName; }
        }
        [ActivityOutput, ActivityFilter]
        public String UserName
        {
            get { return userName; }
        }
        [ActivityOutput, ActivityFilter]
        public String UserOUName
        {
            get { return userOUName; }
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
        private static String convertIntToString(IResultObject obj, String variableName)
        {
            String retValue = String.Empty;
            try { retValue = obj[variableName].StringValue; }
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
    }
}

