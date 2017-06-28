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
    [ActivityData("Collection Data")]
    public class collection
    {
        private String collectionID;
        private String collectionRules;
        private int collectionVariablesCount;
        private String comment;
        private int currentStatus;
        private String lastChangeTime;
        private String lastMemberChangeTime;
        private String lastRefreshTime;
        private String memberClassName;
        private String name;
        private bool ownedByThisSite;
        private int powerConfigsCount;
        private String refreshSchedule;
        private int refreshType;
        private bool replicateToSubSites;
        private int serviceWindowsCount;

        internal collection(IResultObject obj)
        {
            obj.Get();

            this.collectionID = nullStringHandler(obj, "CollectionID");
            this.collectionRules = generateCollectionRulesString(obj);
            this.collectionVariablesCount = nullIntHandler(obj, "CollectionVariablesCount");
            this.comment = nullStringHandler(obj, "Comment");
            this.currentStatus = nullIntHandler(obj, "CurrentStatus");
            this.lastChangeTime = nullDateTimeHandler(obj, "LastChangeTime");
            this.lastMemberChangeTime = nullDateTimeHandler(obj, "LastMemberChangeTime");
            this.lastRefreshTime = nullDateTimeHandler(obj, "LastRefreshTime");
            this.memberClassName = nullStringHandler(obj, "MemberClassName");
            this.name = nullStringHandler(obj, "Name");
            this.ownedByThisSite = obj["OwnedByThisSite"].BooleanValue;
            this.powerConfigsCount = nullIntHandler(obj, "PowerConfigsCount");
            this.refreshSchedule = generateCollectionRefreshString(obj);
            this.refreshType = nullIntHandler(obj, "RefreshType");
            this.replicateToSubSites = obj["ReplicateToSubSites"].BooleanValue;
            this.serviceWindowsCount = nullIntHandler(obj, "ServiceWindowsCount");
        }

        [ActivityOutput, ActivityFilter]
        public String CollectionID
        {
            get { return collectionID; }
        }
        [ActivityOutput, ActivityFilter]
        public String CollectionRules
        {
            get { return collectionRules; }
        }
        [ActivityOutput, ActivityFilter]
        public int CollectionVariablesCount
        {
            get { return collectionVariablesCount; }
        }
        [ActivityOutput, ActivityFilter]
        public String Comment
        {
            get { return comment; }
        }
        [ActivityOutput, ActivityFilter]
        public int CurrentStatus
        {
            get { return currentStatus; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastChangeTime
        {
            get { return lastChangeTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastMemberChangeTime
        {
            get { return lastMemberChangeTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastRefreshTime
        {
            get { return lastRefreshTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String MemberClassName
        {
            get { return memberClassName; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }
        [ActivityOutput, ActivityFilter]
        public bool OwnedByThisSite
        {
            get { return ownedByThisSite; }
        }
        [ActivityOutput, ActivityFilter]
        public int PowerConfigsCount
        {
            get { return powerConfigsCount; }
        }
        [ActivityOutput, ActivityFilter]
        public String RefreshSchedule
        {
            get { return refreshSchedule; }
        }
        [ActivityOutput, ActivityFilter]
        public int RefreshType
        {
            get { return refreshType; }
        }
        [ActivityOutput, ActivityFilter]
        public bool ReplicateToSubSites
        {
            get { return replicateToSubSites; }
        }
        [ActivityOutput, ActivityFilter]
        public int ServiceWindowsCount
        {
            get { return serviceWindowsCount; }
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
        private static String convertIntToString(IResultObject obj, String variableName)
        {
            String retValue = String.Empty;
            try { retValue = obj[variableName].StringValue; }
            catch { }
            return retValue;
        }
        private static String generateCollectionRulesString(IResultObject obj)
        {
            String retValue = String.Empty;
            
            List<IResultObject> arrayItems = obj.GetArrayItems("CollectionRules");

            try
            {
                if (arrayItems != null)
                {
                    foreach (IResultObject CollectionRules in arrayItems)
                    {
                        if (retValue.Equals(String.Empty))
                        {
                            retValue = CollectionRules["RuleName"].StringValue;
                        }
                        else
                        {
                            retValue = retValue + "," + CollectionRules["RuleName"].StringValue;
                        }
                    }
                }
            }
            catch { }
            return retValue;
        }
        private static String generateCollectionRefreshString(IResultObject obj)
        {
            String retValue = String.Empty;
            
            List<IResultObject> arrayItems = obj.GetArrayItems("RefreshSchedule");

            try
            {
                if (arrayItems != null)
                {
                    foreach (IResultObject Schedule in arrayItems)
                    {
                        if (retValue.Equals(String.Empty))
                        {
                            retValue = "DayDuration:" + Schedule["DayDuration"].IntegerValue.ToString() + "-DaySpan:" + Schedule["DaySpan"].IntegerValue.ToString() + "-HourDuration:" + Schedule["HourDuration"].IntegerValue.ToString() + "-HourSpan:" + Schedule["HourSpan"].IntegerValue.ToString() + "-IsGMT:" + Schedule["IsGMT"].BooleanValue.ToString() + "-MinuteDuration:" + Schedule["MinuteDuration"].IntegerValue.ToString() + "-MinuteSpan:" + Schedule["MinuteSpan"].IntegerValue.ToString() + "-StartTime:" + Schedule["StartTime"].DateTimeValue.ToString();
                        }
                        else
                        {
                            retValue = retValue + ",DayDuration:" + Schedule["DayDuration"].IntegerValue.ToString() + "-DaySpan:" + Schedule["DaySpan"].IntegerValue.ToString() + "-HourDuration:" + Schedule["HourDuration"].IntegerValue.ToString() + "-HourSpan:" + Schedule["HourSpan"].IntegerValue.ToString() + "-IsGMT:" + Schedule["IsGMT"].BooleanValue.ToString() + "-MinuteDuration:" + Schedule["MinuteDuration"].IntegerValue.ToString() + "-MinuteSpan:" + Schedule["MinuteSpan"].IntegerValue.ToString() + "-StartTime:" + Schedule["StartTime"].DateTimeValue.ToString();
                        }
                    }
                }
            }
            catch { }
            return retValue;
        }
    }
}

