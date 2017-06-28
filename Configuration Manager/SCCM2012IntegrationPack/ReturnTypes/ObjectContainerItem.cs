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
    [ActivityData("Object Container Item")]
    public class ObjectContainerItem
    {
        private int ContainerNodeID;
        private String InstanceKey;
        private String MemberGUID;
        private int MemberID;
        private int ObjectType;
        private String FriendlyObjectType;
        private String SourceSite;
        

        internal ObjectContainerItem(IResultObject obj)
        {
            obj.Get();

            this.ContainerNodeID = nullIntHandler(obj, "ContainerNodeID");
            this.InstanceKey = nullStringHandler(obj, "InstanceKey");
            this.MemberGUID = nullStringHandler(obj, "MemberGUID");
            this.MemberID = nullIntHandler(obj, "MemberID");
            this.ObjectType = nullIntHandler(obj, "ObjectType");
            this.FriendlyObjectType = convertObjectTypeToFriendly(this.ObjectType);
            this.SourceSite = nullStringHandler(obj, "SourceSite");
        }

        [ActivityOutput, ActivityFilter]
        public int containerNodeID
        {
            get { return ContainerNodeID; }
        }
        [ActivityOutput, ActivityFilter]
        public String instanceKey
        {
            get { return InstanceKey; }
        }
        [ActivityOutput, ActivityFilter]
        public String memberGUID
        {
            get { return MemberGUID; }
        }
        [ActivityOutput, ActivityFilter]
        public int memberID
        {
            get { return MemberID; }
        }
        [ActivityOutput, ActivityFilter]
        public int objectType
        {
            get { return ObjectType; }
        }
        [ActivityOutput, ActivityFilter]
        public String friendlyObjectType
        {
            get { return FriendlyObjectType; }
        }
        [ActivityOutput, ActivityFilter]
        public String sourceSite
        {
            get { return SourceSite; }
        }

        private static String convertObjectTypeToFriendly(int objectType)
        {
            String retString = String.Empty;

            switch (objectType)
            {
                case 2:
                    retString = "SMS_Package";
                    break;
                case 3:
                    retString = "SMS_Advertisement";
                    break;
                case 7:
                    retString = "SMS_Query";
                    break;
                case 8:
                    retString = "SMS_Report";
                    break;
                case 9:
                    retString = "SMS_MeteredProductRule";
                    break;
                case 11:
                    retString = "SMS_ConfigurationItem";
                    break;
                case 14:
                    retString = "SMS_OperatingSystemInstallPackage";
                    break;
                case 17:
                    retString = "SMS_StateMigration";
                    break;
                case 18:
                    retString = "SMS_ImagePackage";
                    break;
                case 19:
                    retString = "SMS_BootImagePackage";
                    break;
                case 20:
                    retString = "SMS_TaskSequencePackage";
                    break;
                case 21:
                    retString = "SMS_DeviceSettingPackage";
                    break;
                case 23:
                    retString = "SMS_DriverPackage";
                    break;
                case 25:
                    retString = "SMS_Driver";
                    break;
                case 1011:
                    retString = "SMS_SoftwareUpdate";
                    break;
                default:
                    retString = "Unknown";
                    break;
            }
            return retString;
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
    }
}

