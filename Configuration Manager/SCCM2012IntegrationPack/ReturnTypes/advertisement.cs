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
    [ActivityData("Advertisement Data")]
    public class advertisement
    {
        private int actionInProgress;
        private int advertFlags;
        private String advertisementID;
        private String advertisementName;
        private String assignedSchedule;
        private bool assignedScheduleEnabled;
        private bool assignedScheduleIsGMT;
        private int assignmentID;
        private String collectionID;
        private int deviceFlags;
        private String expirationTime;
        private bool expirationTimeEnabled;
        private bool expirationTimeIsGMT;
        private String hierarchyPath;
        private bool includeSubCollection;
        private String isvData;
        private int isvDataSize;
        private int mandatoryCountdown;
        private String packageID;
        private String presentTime;
        private bool presentTimeEnabled;
        private bool presentTimeIsGMT;
        private int priority;
        private String programName;
        private int remoteClientFlags;
        private String sourceSite;
        private int timeFlags;
        
        internal advertisement(IResultObject obj)
        {
            obj.Get();

            this.actionInProgress = nullIntHandler(obj, "ActionInProgress");
            this.advertFlags = nullIntHandler(obj, "AdvertFlags");
            this.advertisementID = obj["AdvertisementID"].StringValue;
            this.advertisementName = obj["AdvertisementName"].StringValue;
            this.assignedSchedule = generateAssignedSchedule(obj);
            this.assignedScheduleEnabled = nullBoolHandler(obj, "AssignedScheduleEnabled");
            this.assignedScheduleIsGMT = nullBoolHandler(obj, "AssignedScheduleIsGMT");
            this.assignmentID = nullIntHandler(obj, "AssignmentID");
            this.collectionID = obj["CollectionID"].StringValue;
            this.deviceFlags = nullIntHandler(obj, "DeviceFlags");
            this.expirationTime = nullDateTimeHandler(obj, "ExpirationTime");
            this.expirationTimeEnabled = obj["ExpirationTimeEnabled"].BooleanValue;
            this.expirationTimeIsGMT = obj["ExpirationTimeIsGMT"].BooleanValue;
            this.hierarchyPath = obj["HierarchyPath"].StringValue;
            this.includeSubCollection = obj["IncludeSubCollection"].BooleanValue;
            this.isvData = convertIntArray(obj["ISVData"].IntegerArrayValue);
            this.isvDataSize = nullIntHandler(obj, "ISVDataSize");
            this.mandatoryCountdown = nullIntHandler(obj, "MandatoryCountdown");
            this.packageID = obj["PackageID"].StringValue;
            this.presentTime = nullDateTimeHandler(obj, "PresentTime");
            this.presentTimeEnabled = obj["PresentTimeEnabled"].BooleanValue;
            this.presentTimeIsGMT = obj["PresentTimeIsGMT"].BooleanValue;
            this.priority = nullIntHandler(obj, "Priority");
            this.programName = obj["programName"].StringValue;
            this.remoteClientFlags = nullIntHandler(obj, "RemoteClientFlags");
            this.sourceSite = obj["SourceSite"].StringValue;
            this.timeFlags = nullIntHandler(obj, "TimeFlags");
        }
        [ActivityOutput, ActivityFilter]
        public int ActionInProgress
        {
            get { return actionInProgress; }
        }
        [ActivityOutput, ActivityFilter]
        public uint AdvertFlags
        {
            get { return (uint)advertFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public String AdvertisementID
        {
            get { return advertisementID; }
        }
        [ActivityOutput, ActivityFilter]
        public String AdvertisementName
        {
            get { return advertisementName; }
        }

        [ActivityOutput, ActivityFilter]
        public String AssignedSchedule
        {
            get { return assignedSchedule; }
        }

        [ActivityOutput, ActivityFilter]
        public bool AssignedScheduleEnabled
        {
            get { return assignedScheduleEnabled; }
        }

        [ActivityOutput, ActivityFilter]
        public bool AssignedScheduleIsGMT
        {
            get { return assignedScheduleIsGMT; }
        }
        [ActivityOutput, ActivityFilter]
        public int AssignmentID
        {
            get { return assignmentID; }
        }
        [ActivityOutput, ActivityFilter]
        public String CollectionID
        {
            get { return collectionID; }
        }
        [ActivityOutput, ActivityFilter]
        public uint DeviceFlags
        {
            get { return (uint)deviceFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public String ExpirationTime
        {
            get { return expirationTime; }
        }
        [ActivityOutput, ActivityFilter]
        public bool ExpirationTimeEnabled
        {
            get { return expirationTimeEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public bool ExpirationTimeIsGMT
        {
            get { return expirationTimeIsGMT; }
        }
        [ActivityOutput, ActivityFilter]
        public String HierarchyPath
        {
            get { return hierarchyPath; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IncludeSubCollection
        {
            get { return includeSubCollection; }
        }
        [ActivityOutput, ActivityFilter]
        public String ISVData
        {
            get { return isvData; }
        }
        [ActivityOutput, ActivityFilter]
        public int ISVDataSize
        {
            get { return isvDataSize; }
        }
        [ActivityOutput, ActivityFilter]
        public int MandatoryCountdown
        {
            get { return mandatoryCountdown; }
        }
        [ActivityOutput, ActivityFilter]
        public String PackageID
        {
            get { return packageID; }
        }
        [ActivityOutput, ActivityFilter]
        public String PresentTime
        {
            get { return presentTime; }
        }
        [ActivityOutput, ActivityFilter]
        public bool PresentTimeEnabled
        {
            get { return presentTimeEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public bool PresentTimeIsGMT
        {
            get { return presentTimeIsGMT; }
        }
        [ActivityOutput, ActivityFilter]
        public int Priority
        {
            get { return priority; }
        }
        [ActivityOutput, ActivityFilter]
        public String ProgramName
        {
            get { return programName; }
        }
        [ActivityOutput, ActivityFilter]
        public uint RemoteClientFlags
        {
            get { return (uint)remoteClientFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public String SourceSite
        {
            get { return sourceSite; }
        }
        [ActivityOutput, ActivityFilter]
        public uint TimeFlags
        {
            get { return (uint)timeFlags; }
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
        private static String generateAssignedSchedule(IResultObject obj)
        {
            String retValue = String.Empty;
            List<IResultObject> arrayItems = obj.GetArrayItems("AssignedSchedule");

            if (arrayItems != null)
            {
                foreach (IResultObject Schedule in arrayItems)
                {
                    int daySpan = 0;
                    int hourSpan = 0;
                    int minuteSpan = 0;
                    bool IsGMT = false;
                    DateTime startTime = DateTime.UtcNow;
                    try { if (Schedule["DaySpan"].IntegerValue != null) { daySpan = Schedule["DaySpan"].IntegerValue; } }
                    catch { }
                    try { if (Schedule["HourSpan"].IntegerValue != null) { hourSpan = Schedule["HourSpan"].IntegerValue; } }
                    catch { }
                    try { if (Schedule["MinuteSpan"].IntegerValue != null) { minuteSpan = Schedule["MinuteSpan"].IntegerValue; } }
                    catch { }
                    try { if (Schedule["IsGMT"].BooleanValue != null) { IsGMT = Schedule["IsGMT"].BooleanValue; } }
                    catch { }
                    try { if (Schedule["StartTime"].DateTimeValue != null) { startTime = Schedule["StartTime"].DateTimeValue; } }
                    catch { }

                    if (retValue.Equals(String.Empty))
                    {
                        retValue = "DaySpan:" + daySpan.ToString() + "-HourSpan:" + hourSpan.ToString() + "-IsGMT:" + IsGMT.ToString() + "-MinuteSpan:" + minuteSpan.ToString() + "-StartTime:" + startTime.ToString();
                    }
                    else
                    {
                        retValue = retValue + ",DaySpan:" + daySpan.ToString() + "-HourSpan:" + hourSpan.ToString() + "-IsGMT:" + IsGMT.ToString() + "-MinuteSpan:" + minuteSpan.ToString() + "-StartTime:" + startTime.ToString();
                    }
                }
            }
            return retValue;
        }
    }
}

