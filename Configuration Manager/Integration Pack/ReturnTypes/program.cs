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
    [ActivityData("Program Data")]
    public class program
    {
        private int actionInProgress;
        private String applicationHierarchy;
        private String commandLine;
        private String comment;
        private String dependentProgram;
        private String description;
        private int deviceFlags;
        private String diskSpaceReq;
        private String driveLetter;
        private int duration;
        private String extendedData;
        private int extendedDataSize;
        private String icon;
        private int iconSize;
        private String isvData;
        private int isvDataSize;
        private String msiFilePath;
        private String msiProductID;
        private String packageID;
        private int programFlags;
        private String programName;
        private String removalKey;
        private String requirements;
        private String supportedOperatingSystems;
        private String workingDirectory;

        internal program(IResultObject obj)
        {
            obj.Get();
            this.actionInProgress = nullIntHandler(obj, "ActionInProgress");
            this.applicationHierarchy = nullStringHandler(obj, "ApplicationHierarchy");
            this.commandLine = nullStringHandler(obj,"CommandLine");
            this.comment = nullStringHandler(obj,"Comment");
            this.dependentProgram = nullStringHandler(obj,"DependentProgram");
            this.description = nullStringHandler(obj,"Description");
            this.deviceFlags = nullIntHandler(obj, "DeviceFlags");
            this.diskSpaceReq = nullStringHandler(obj,"DiskSpaceReq");
            this.driveLetter = nullStringHandler(obj,"DriveLetter");
            this.duration = nullIntHandler(obj, "Duration");
            this.extendedData = convertIntArray(obj["ExtendedData"].IntegerArrayValue);
            this.extendedDataSize = nullIntHandler(obj, "ExtendedDataSize");
            this.icon = convertIntArray(obj["Icon"].IntegerArrayValue);
            this.iconSize = nullIntHandler(obj, "IconSize");
            this.isvData = convertIntArray(obj["ISVData"].IntegerArrayValue);
            this.isvDataSize = nullIntHandler(obj, "ISVDataSize");
            this.msiFilePath = nullStringHandler(obj,"MSIFilePath");
            this.msiProductID = nullStringHandler(obj,"MSIProductID");
            this.packageID = nullStringHandler(obj,"PackageID");
            this.programFlags = nullIntHandler(obj, "ProgramFlags");
            this.programName = nullStringHandler(obj,"ProgramName");
            this.removalKey = nullStringHandler(obj,"RemovalKey");
            this.requirements = nullStringHandler(obj,"Requiremens");
            this.supportedOperatingSystems = generateSupportedOperatingSystemList(obj);
            this.workingDirectory = nullStringHandler(obj,"WorkingDirectory");
        }

        [ActivityOutput, ActivityFilter]
        public int ActionInProgress
        {
            get { return actionInProgress; }
        }
        [ActivityOutput, ActivityFilter]
        public String ApplicationHierarchy
        {
            get { return applicationHierarchy; }
        }
        [ActivityOutput, ActivityFilter]
        public String CommandLine
        {
            get { return commandLine; }
        }
        [ActivityOutput, ActivityFilter]
        public String Comment
        {
            get { return comment; }
        }
        [ActivityOutput, ActivityFilter]
        public String DependentProgram
        {
            get { return dependentProgram; }
        }
        [ActivityOutput, ActivityFilter]
        public String Description
        {
            get { return description; }
        }
        [ActivityOutput, ActivityFilter]
        public uint DeviceFlags
        {
            get { return (uint)deviceFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public String DiskSpaceReq
        {
            get { return diskSpaceReq; }
        }
        [ActivityOutput, ActivityFilter]
        public String DriveLetter
        {
            get { return driveLetter; }
        }
        [ActivityOutput, ActivityFilter]
        public int Duration
        {
            get { return duration; }
        }
        [ActivityOutput, ActivityFilter]
        public String ExtendedData
        {
            get { return extendedData; }
        }
        [ActivityOutput, ActivityFilter]
        public int ExtendedDataSize
        {
            get { return extendedDataSize; }
        }
        [ActivityOutput, ActivityFilter]
        public String Icon
        {
            get { return icon; }
        }
        [ActivityOutput, ActivityFilter]
        public int IconSize
        {
            get { return iconSize; }
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
        public String MSIFilePath
        {
            get { return msiFilePath; }
        }
        [ActivityOutput, ActivityFilter]
        public String MSIProductID
        {
            get { return msiProductID; }
        }
        [ActivityOutput, ActivityFilter]
        public String PackageID
        {
            get { return packageID; }
        }
        [ActivityOutput, ActivityFilter]
        public uint ProgramFlags
        {
            get { return (uint)programFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public String ProgramName
        {
            get { return programName; }
        }
        [ActivityOutput, ActivityFilter]
        public String WorkingDirectory
        {
            get { return workingDirectory; }
        }
        [ActivityOutput, ActivityFilter]
        public String RemovalKey
        {
            get { return removalKey; }
        }
        [ActivityOutput, ActivityFilter]
        public String Requirements
        {
            get { return requirements; }
        }
        [ActivityOutput, ActivityFilter]
        public String SupportedOperatingSystems
        {
            get { return supportedOperatingSystems; }
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
        private static String generateSupportedOperatingSystemList(IResultObject obj)
        {
            String retValue = String.Empty;
            try
            {
                List<IResultObject> arrayItems = obj.GetArrayItems("SupportedOperatingSystems");

                if (arrayItems != null)
                {
                    foreach (IResultObject supportedOS in arrayItems)
                    {
                        if (retValue.Equals(String.Empty))
                        {
                            retValue = "Name:" + supportedOS["Name"].StringValue + "-Platform:" + supportedOS["Platform"].StringValue + "-MinVersion:" + supportedOS["MinVersion"].StringValue + "-MaxVersion:" + supportedOS["MaxVersion"].StringValue;
                        }
                        else
                        {
                            retValue = retValue + ",Name:" + supportedOS["Name"].StringValue + "-Platform:" + supportedOS["Platform"].StringValue + "-MinVersion:" + supportedOS["MinVersion"].StringValue + "-MaxVersion:" + supportedOS["MaxVersion"].StringValue;
                        }
                    }
                }
            }
            catch { }
            return retValue;
        }

    }
}

