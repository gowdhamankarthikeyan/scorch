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
    [ActivityData("Collection Member")]
    public class collectionMember
    {
        private String amtFullVersion;
        private int amtStatus;
        private int clientType;
        private String collectionID;
        private String domain;
        private bool isActive;
        private bool isAlwaysInternet;
        private int isApproved;
        private bool isAssigned;
        private bool isBlocked;
        private bool isClient;
        private bool isDecommissioned;
        private bool isDirect;
        private bool isInternetEnabled;
        private String name;
        private int resourceID;
        private int resourceType;
        private String siteCode;
        private String smsID;
        private bool suppressAutoProvision;

        internal collectionMember(IResultObject obj)
        {
            obj.Get();

            this.amtFullVersion = nullStringHandler(obj, "AMTFullVersion");
            this.amtStatus = nullIntHandler(obj, "AMTStatus");
            this.clientType = nullIntHandler(obj, "ClientType");
            this.collectionID = nullStringHandler(obj, "CollectionID");
            this.domain = nullStringHandler(obj, "Domain");
            this.isActive = nullBoolHandler(obj, "IsActive");
            this.isAlwaysInternet = nullBoolHandler(obj, "IsAlwaysInternet");
            this.isApproved = nullIntHandler(obj, "IsApproved");
            this.isBlocked = nullBoolHandler(obj, "IsBlocked");
            this.isClient = nullBoolHandler(obj, "IsClient");
            this.isDecommissioned = nullBoolHandler(obj, "IsDecommissioned");
            this.isDirect = nullBoolHandler(obj, "IsDirect");
            this.isInternetEnabled = nullBoolHandler(obj, "IsInternetEnabled");
            this.name = nullStringHandler(obj, "Name");
            this.resourceID = nullIntHandler(obj, "ResourceID");
            this.resourceType = nullIntHandler(obj, "ResourceType");
            this.siteCode = nullStringHandler(obj, "SiteCode");
            this.smsID = nullStringHandler(obj, "SMSID");
            this.suppressAutoProvision = nullBoolHandler(obj, "SuppressAutoProvision");
        }
        [ActivityOutput, ActivityFilter]
        public String AMTFullVersion
        {
            get { return amtFullVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public int AMTStatus
        {
            get { return amtStatus; }
        }
        [ActivityOutput, ActivityFilter]
        public int ClientType
        {
            get { return clientType; }
        }
        [ActivityOutput, ActivityFilter]
        public String CollectionID
        {
            get { return collectionID; }
        }
        [ActivityOutput, ActivityFilter]
        public String Domain
        {
            get { return domain; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsActive
        {
            get { return isActive; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsAlwaysInternet
        {
            get { return isAlwaysInternet; }
        }
        [ActivityOutput, ActivityFilter]
        public int IsApproved
        {
            get { return isApproved; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsAssigned
        {
            get { return isAssigned; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsBlocked
        {
            get { return isBlocked; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsClient
        {
            get { return isClient; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsDecommissioned
        {
            get { return isDecommissioned; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsDirect
        {
            get { return isDirect; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsInternetEnabled
        {
            get { return isInternetEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
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
        public String SiteCode
        {
            get { return siteCode; }
        }
        [ActivityOutput, ActivityFilter]
        public String SMSID
        {
            get { return smsID; }
        }
        [ActivityOutput, ActivityFilter]
        public bool SuppressAutoProvision
        {
            get { return suppressAutoProvision; }
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
            String retValue = String.Empty;
            try { retValue = obj[variableName].StringValue; }
            catch { }
            return retValue;
        }
        private static bool nullBoolHandler(IResultObject obj, String variableName)
        {
            bool retValue = false;
            try { retValue = obj[variableName].BooleanValue; }
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

