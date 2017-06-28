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
    [ActivityData("Image Package Data")]
    public class imagePackage
    {
        private int actionInProgress;
        private String alternateContentProviders;
        private String description;
        private int extendedDataSize;
        private int forcedDisconnectDelay;
        private bool forcedDisconnectEnabled;
        private int forcedDisconnectNumRetries;
        private int iconSize;
        private bool ignoreAddressSchedule;
        private String imageDiskLayout;
        private String imageProperty;
        private String isvData;
        private int isvDataSize;
        private String language;
        private String lastRefreshTime;
        private String manufacturer;
        private String mifFilename;
        private String mifName;
        private String mifPublisher;
        private String mifVersion;
        private String name;
        private String packageID;
        private int packageType;
        private int pkgFlags;
        private int pkgSourceFlag;
        private String pkgSourcePath;
        private String preferredAddressType;
        private int priority;
        private String refreshSchedule;
        private bool refreshPkgSourceFlag;
        private String shareName;
        private int shareType;
        private String sourceDate;
        private String sourceSite;
        private int sourceVersion;
        private String storedPkgPath;
        private int storedPkgVersion;
        private String version;

        internal imagePackage(IResultObject obj)
        {
            obj.Get();
            this.actionInProgress = nullIntHandler(obj, "ActionInProgress");
            this.alternateContentProviders = obj["AlternateContentProviders"].StringValue;
            this.description = obj["Description"].StringValue;
            this.extendedDataSize = nullIntHandler(obj, "ExtendedDataSize");
            this.forcedDisconnectDelay = nullIntHandler(obj, "ForcedDisconnectDelay");
            this.forcedDisconnectEnabled = obj["ForcedDisconnectEnabled"].BooleanValue;
            this.forcedDisconnectNumRetries = nullIntHandler(obj, "ForcedDisconnectNumRetries");
            this.iconSize = nullIntHandler(obj, "IconSize");
            this.ignoreAddressSchedule = obj["IgnoreAddressSchedule"].BooleanValue;
            this.imageDiskLayout = nullStringHandler(obj, "ImageDiskLayout");
            this.imageProperty = nullStringHandler(obj, "ImageProperty");
            this.isvData = convertIntArray(obj["ISVData"].IntegerArrayValue);
            this.isvDataSize = nullIntHandler(obj, "ISVDataSize");
            this.language = obj["Language"].StringValue;
            this.lastRefreshTime = nullDateTimeHandler(obj, "LastRefreshTime");
            this.manufacturer = obj["Manufacturer"].StringValue;
            this.mifFilename = obj["MIFFileName"].StringValue;
            this.mifName = obj["MIFName"].StringValue;
            this.mifPublisher = obj["MIFPublisher"].StringValue;
            this.mifVersion = obj["MifVersion"].StringValue;
            this.name = obj["Name"].StringValue;
            this.packageID = obj["PackageID"].StringValue;
            this.packageType = nullIntHandler(obj, "PackageType");
            this.pkgFlags = nullIntHandler(obj, "PkgFlags");
            this.pkgSourceFlag = nullIntHandler(obj, "PkgSourceFlag");
            this.pkgSourcePath = obj["PkgSourcePath"].StringValue;
            this.preferredAddressType = obj["PreferredAddressType"].StringValue;
            this.priority = nullIntHandler(obj, "Priority");
            this.refreshSchedule = generateRefreshSchedule(obj);
            this.refreshPkgSourceFlag = obj["RefreshPkgSourceFlag"].BooleanValue;
            this.shareName = obj["ShareName"].StringValue;
            this.shareType = nullIntHandler(obj, "ShareType");
            this.sourceDate = nullDateTimeHandler(obj, "SourceDate");
            this.sourceSite = obj["SourceSite"].StringValue;
            this.sourceVersion = nullIntHandler(obj, "SourceVersion");
            this.storedPkgPath = obj["StoredPkgPath"].StringValue;
            this.storedPkgVersion = nullIntHandler(obj, "StoredPkgVersion");
            this.version = obj["Version"].StringValue;
        }
        [ActivityOutput, ActivityFilter]
        public int ActionInProgress
        {
            get { return actionInProgress; }
        }
        [ActivityOutput, ActivityFilter]
        public String AlternateContentProviders
        {
            get { return alternateContentProviders; }
        }
        [ActivityOutput, ActivityFilter]
        public String Description
        {
            get { return description; }
        }
        [ActivityOutput, ActivityFilter]
        public int ExtendedDataSize
        {
            get { return extendedDataSize; }
        }
        [ActivityOutput, ActivityFilter]
        public int ForcedDisconnectDelay
        {
            get { return forcedDisconnectDelay; }
        }
        [ActivityOutput, ActivityFilter]
        public bool ForcedDisconnectEnabled
        {
            get { return forcedDisconnectEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public int ForcedDisconnectNumRetries
        {
            get { return forcedDisconnectNumRetries; }
        }
        [ActivityOutput, ActivityFilter]
        public int IconSize
        {
            get { return iconSize; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IgnoreAddressSchedule
        {
            get { return ignoreAddressSchedule; }
        }
        [ActivityOutput, ActivityFilter]
        public String ISCData
        {
            get { return isvData; }
        }
        [ActivityOutput, ActivityFilter]
        public int ISVDataSize
        {
            get { return isvDataSize; }
        }
        [ActivityOutput, ActivityFilter]
        public String Language
        {
            get { return language; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastRefreshTime
        {
            get { return lastRefreshTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String Manufacturer
        {
            get { return manufacturer; }
        }
        [ActivityOutput, ActivityFilter]
        public String MIFFilename
        {
            get { return mifFilename; }
        }
        [ActivityOutput, ActivityFilter]
        public String MIFName
        {
            get { return mifName; }
        }
        [ActivityOutput, ActivityFilter]
        public String MIFPublisher
        {
            get { return mifPublisher; }
        }
        [ActivityOutput, ActivityFilter]
        public String MIFVersion
        {
            get { return mifVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }
        [ActivityOutput, ActivityFilter]
        public String PackageID
        {
            get { return packageID; }
        }
        [ActivityOutput, ActivityFilter]
        public int PackageType
        {
            get { return packageType; }
        }
        [ActivityOutput, ActivityFilter]
        public uint PkgFlags
        {
            get { return (uint)pkgFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public uint PkgSourceFlag
        {
            get { return (uint)pkgSourceFlag; }
        }
        [ActivityOutput, ActivityFilter]
        public String PkgSourcePath
        {
            get { return pkgSourcePath; }
        }
        [ActivityOutput, ActivityFilter]
        public String PreferredAddressType
        {
            get { return preferredAddressType; }
        }
        [ActivityOutput, ActivityFilter]
        public int Priority
        {
            get { return priority; }
        }
        [ActivityOutput, ActivityFilter]
        public String RefreshSchedule
        {
            get { return refreshSchedule; }
        }
        [ActivityOutput, ActivityFilter]
        public bool RefreshPkgSourceFlag
        {
            get { return refreshPkgSourceFlag; }
        }
        [ActivityOutput, ActivityFilter]
        public String ShareName
        {
            get { return shareName; }
        }
        [ActivityOutput, ActivityFilter]
        public int ShareType
        {
            get { return shareType; }
        }
        [ActivityOutput, ActivityFilter]
        public String SourceDate
        {
            get { return sourceDate; }
        }
        [ActivityOutput, ActivityFilter]
        public String SourceSite
        {
            get { return sourceSite; }
        }
        [ActivityOutput, ActivityFilter]
        public int SourceVersion
        {
            get { return sourceVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String StoredPkgPath
        {
            get { return storedPkgPath; }
        }
        [ActivityOutput, ActivityFilter]
        public int StoredPkgVersion
        {
            get { return storedPkgVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String Version
        {
            get { return version; }
        }
        private static String nullStringHandler(IResultObject obj, String propertyName)
        {
            String retValue = String.Empty;

            try { retValue = obj[propertyName].StringValue; }
            catch { }

            return retValue;
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
        private static String generateRefreshSchedule(IResultObject obj)
        {
            String retValue = String.Empty;

            List<IResultObject> arrayItems = obj.GetArrayItems("RefreshSchedule");
            try
            {
                if (arrayItems != null)
                {
                    foreach (IResultObject schedule in arrayItems)
                    {
                        if (retValue.Equals(String.Empty))
                        {
                            retValue = "DayDuration:" + Convert.ToString(schedule["DayDuration"].IntegerValue) + "-HourDuration:" + Convert.ToString(schedule["HourDuration"].IntegerValue) + "-IsGMT:" + Convert.ToString(schedule["IsGMT"].BooleanValue) + "-MinuteDuration:" + Convert.ToString(schedule["MinuteDuration"].IntegerValue) + "-StartTime:" + Convert.ToString(schedule["StartTime"].DateTimeValue);
                        }
                        else
                        {
                            retValue = retValue + ",DayDuration:" + Convert.ToString(schedule["DayDuration"].IntegerValue) + "-HourDuration:" + Convert.ToString(schedule["HourDuration"].IntegerValue) + "-IsGMT:" + Convert.ToString(schedule["IsGMT"].BooleanValue) + "-MinuteDuration:" + Convert.ToString(schedule["MinuteDuration"].IntegerValue) + "-StartTime:" + Convert.ToString(schedule["StartTime"].DateTimeValue);
                        }
                    }
                }
            }
            catch { }

            return retValue;
        }
    }
}

