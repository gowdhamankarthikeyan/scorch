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
    [ActivityData("Distribution Point")]
    public class DistributionPoint
    {
        private bool _BitsEnabled = false;
        private bool _IsFileStreaming = false;
        private bool _IsPeerDP = false;
        private bool _IsProtected = false;
        private String _LastRefreshTime = String.Empty;
        private String _PackageID = String.Empty;
        private String _ResourceType = String.Empty;
        private String _ServerNALPath = String.Empty;
        private String _SiteCode = String.Empty;
        private String _SiteName = String.Empty;
        private String _SourceSite = String.Empty;
        private String _Status = String.Empty;
        
        internal DistributionPoint(IResultObject obj)
        {
            obj.Get();


            this._BitsEnabled = nullBoolHandler(obj, "BitsEnabled");
            this._IsFileStreaming = nullBoolHandler(obj, "IsFileStreaming");
            this._IsPeerDP = nullBoolHandler(obj, "IsPeerDP");
            this._IsProtected = nullBoolHandler(obj, "IsProtected");
            this._LastRefreshTime = nullDateTimeHandler(obj, "LastRefreshTime");
            this._PackageID = obj["PackageID"].StringValue;
            this._ResourceType = obj["ResourceType"].StringValue;
            this._ServerNALPath = obj["ServerNALPath"].StringValue;
            this._SiteCode = obj["SiteCode"].StringValue;
            this._SiteName = obj["SiteName"].StringValue;
            this._SourceSite = obj["SourceSite"].StringValue;
            this._Status = obj["Status"].StringValue;
        }

        [ActivityOutput, ActivityFilter]
        public bool BitsEnabled
        {
            get { return _BitsEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsFileStreaming
        {
            get { return _IsFileStreaming; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsPeerDP
        {
            get { return IsPeerDP; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsProtected
        {
            get { return _IsProtected; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastRefreshTime
        {
            get { return _LastRefreshTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String PackageID
        {
            get { return _PackageID; }
        }
        [ActivityOutput, ActivityFilter]
        public String ResourceType
        {
            get { return _ResourceType; }
        }
        [ActivityOutput, ActivityFilter]
        public String ServerNALPath
        {
            get { return _ServerNALPath; }
        }
        [ActivityOutput, ActivityFilter]
        public String SiteCode
        {
            get { return _SiteCode; }
        }
        [ActivityOutput, ActivityFilter]
        public String SiteName
        {
            get { return _SiteName; }
        }
        [ActivityOutput, ActivityFilter]
        public String SourceSite
        {
            get { return _SourceSite; }
        }
        [ActivityOutput, ActivityFilter]
        public String Status
        {
            get { return _Status; }
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
        private static bool nullBoolHandler(IResultObject obj, String variableName)
        {
            bool retValue = false;
            try { retValue = obj[variableName].BooleanValue; }
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
    }
}

