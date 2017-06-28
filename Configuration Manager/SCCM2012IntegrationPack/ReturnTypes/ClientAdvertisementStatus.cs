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
    [ActivityData("Client Advertisement Status")]
    public class ClientAdvertisementStatus
    {
        private String AdvertisementID;
        private int LastAcceptanceMessageID;
        private String LastAcceptanceMessageIDName;
        private int LastAcceptanceMessageIDSeverity;
        private int LastAcceptanceState;
        private String LastAcceptanceStateName;
        private String LastAcceptanceStatusTime;
        private String LastExecutionContext;
        private String LastExecutionResult;
        private int LastState;
        private String LastStateName;
        private int LastStatusMessageID;
        private String LastStatusMessageIDName;
        private int LastStatusMessageIDSeverity;
        private String LastStatusTime;
        private int ResourceID;


        internal ClientAdvertisementStatus(IResultObject obj)
        {
            this.AdvertisementID = nullStringHandler(obj, "AdvertisementID");
            this.LastAcceptanceMessageID = nullIntHandler(obj, "LastAcceptanceMessageID");
            this.LastAcceptanceMessageIDName = nullStringHandler(obj, "LastAcceptanceMessageIDName");
            this.LastAcceptanceMessageIDSeverity = nullIntHandler(obj, "LastAcceptanceMessageIDSeverity");
            this.LastAcceptanceState = nullIntHandler(obj, "LastAcceptanceState");
            this.LastAcceptanceStateName = nullStringHandler(obj, "LastAcceptanceStateName");
            this.LastAcceptanceStatusTime = nullDateTimeHandler(obj, "LastAcceptanceStatusTime");
            this.LastExecutionContext = nullStringHandler(obj, "LastExecutionContext");
            this.LastExecutionResult = nullStringHandler(obj, "LastExecutionResult");
            this.LastState = nullIntHandler(obj, "LastState");
            this.LastStateName = nullDateTimeHandler(obj, "LastStateName");
            this.LastStatusMessageID = nullIntHandler(obj, "LastStatusMessageID");
            this.LastStatusMessageIDName = nullDateTimeHandler(obj, "LastStatusMessageIDName");
            this.LastStatusMessageIDSeverity = nullIntHandler(obj, "LastStatusMessageIDSeverity");
            this.LastStatusTime = nullDateTimeHandler(obj, "LastStatusTime");
            this.ResourceID = nullIntHandler(obj, "ResourceID");
        }
        [ActivityOutput, ActivityFilter]
        public String advertisementID
        {
            get { return AdvertisementID; }
        }
        [ActivityOutput, ActivityFilter]
        public int lastAcceptanceMessageID
        {
            get { return LastAcceptanceMessageID; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastAcceptanceMessageIDName
        {
            get { return LastAcceptanceMessageIDName; }
        }
        [ActivityOutput, ActivityFilter]
        public int lastAcceptanceMessageIDSeverity
        {
            get { return LastAcceptanceMessageIDSeverity; }
        }
        [ActivityOutput, ActivityFilter]
        public int lastAcceptanceState
        {
            get { return LastAcceptanceState; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastAcceptanceStateName
        {
            get { return LastAcceptanceStateName; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastAcceptanceStatusTime
        {
            get { return LastAcceptanceStatusTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastExecutionContext
        {
            get { return LastExecutionContext; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastExecutionResult
        {
            get { return LastExecutionResult; }
        }
        [ActivityOutput, ActivityFilter]
        public int lastState
        {
            get { return LastState; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastStateName
        {
            get { return LastStateName; }
        }
        [ActivityOutput, ActivityFilter]
        public int lastStatusMessageID
        {
            get { return LastStatusMessageID; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastStatusMessageIDName
        {
            get { return LastStatusMessageIDName; }
        }
        [ActivityOutput, ActivityFilter]
        public int lastStatusMessageIDSeverity
        {
            get { return LastStatusMessageIDSeverity; }
        }
        [ActivityOutput, ActivityFilter]
        public String lastStatusTime
        {
            get { return LastStatusTime; }
        }
        [ActivityOutput, ActivityFilter]
        public int resourceID
        {
            get { return ResourceID; }
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
        private static String generateLocalizedInformation(IResultObject obj)
        {
            String retValue = String.Empty;

            try
            {
                List<IResultObject> resultList = obj.GetArrayItems("LocalizedInformation");

                foreach (IResultObject result in resultList)
                {
                    if (retValue.Equals(String.Empty))
                    {
                        retValue = "Description:" + Convert.ToString(result["Description"].StringValue) + "-DisplayName:" + Convert.ToString(result["DisplayName"].StringValue) + "-InformativeURL:" + Convert.ToString(result["InformativeURL"].StringValue) + "-LocaleID:" + Convert.ToString(result["LocaleID"].StringValue);
                    }
                    else
                    {
                        retValue = retValue + ",Description:" + Convert.ToString(result["Description"].StringValue) + "-DisplayName:" + Convert.ToString(result["DisplayName"].StringValue) + "-InformativeURL:" + Convert.ToString(result["InformativeURL"].StringValue) + "-LocaleID:" + Convert.ToString(result["LocaleID"].StringValue);
                    }
                }
            }
            catch { }

            return retValue;
        }
    }
}

