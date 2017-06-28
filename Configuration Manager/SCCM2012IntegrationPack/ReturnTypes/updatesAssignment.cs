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
    [ActivityData("Updates Assignment Data")]
    public class updatesAssignment
    {
        private bool applyToSubTargets;
        private String assignedCIs;
        private int assignmentAction;
        private String assignmentDescription;
        private int assignmentID;
        private String assignmentName;
        private String assignmentUniqueID;
        private bool containsExpiredUpdates;
        private String creationTime;
        private int desiredConfigType;
        private bool disableMomAlerts;
        private int dpLocality;
        private String enforcementDeadline;
        private String evaluationSchedule;
        private String expirationTime;
        private String lastModificationTime;
        private int legacyAdvertFlags;
        private bool legacyCollectInventory;
        private String legacyDependentProgram;
        private bool legacyDeploymentEnabled;
        private String legacyDeploymentSchedule;
        private int legacyDPLocality;
        private bool legacyForceReboot;
        private bool legacyInstallAllOnDeadline;
        private int legacyInstallAllowedWindow;
        private bool legacyPostponeInstall;
        private int legacyProgramFlags;
        private int legacyRebootCountdown;
        private bool legacySilentInstall;
        private int localeID;
        private bool logComplianceToWinEvent;
        private int nonComplianceCriticality;
        private bool notifyUser;
        private bool overrideServiceWindows;
        private bool raiseMomAlertsOnFailure;
        private bool readOnly;
        private bool rebootOutsideOfServiceWindows;
        private bool sendDetailedNonComplianceStatus;
        private String sourceSite;
        private String startTime;
        private int suppressReboot;
        private String targetCollectionID;
        private bool useGMTTimes;
        private bool wolEnabled;


        internal updatesAssignment(IResultObject obj)
        {
            obj.Get();

            this.applyToSubTargets = nullBoolHandler(obj, "ApplyToSubTargets");
            this.assignedCIs = convertIntArray(obj["AssignedCIs"].IntegerArrayValue);
            this.assignmentAction = nullIntHandler(obj, "AssignmentAction");
            this.assignmentDescription = nullStringHandler(obj, "AssignmentDescription");
            this.assignmentID = nullIntHandler(obj, "AssignmentID");
            this.assignmentName = nullStringHandler(obj, "AssignmentName");
            this.assignmentUniqueID = nullStringHandler(obj, "AssignmentUniqueID");
            this.containsExpiredUpdates = nullBoolHandler(obj, "ContainsExpiredUpdates");
            this.creationTime = nullDateTimeHandler(obj, "CreationTime");
            this.desiredConfigType = nullIntHandler(obj, "DesiredConfigType");
            this.disableMomAlerts = nullBoolHandler(obj, "DisableMomAlerts");
            this.dpLocality = nullIntHandler(obj, "DPLocality");
            this.enforcementDeadline = nullDateTimeHandler(obj, "EnforcementDeadline");
            this.evaluationSchedule = nullStringHandler(obj, "EvaluationSchedule");
            this.expirationTime = nullDateTimeHandler(obj, "ExpirationTime");
            this.lastModificationTime = nullDateTimeHandler(obj, "LastModificationTime");
            this.legacyAdvertFlags = nullIntHandler(obj, "LegacyAdvertFlags");
            this.legacyCollectInventory = nullBoolHandler(obj, "LegacyCollectInventory");
            this.legacyDependentProgram = nullStringHandler(obj, "LegacyDependentProgram");
            this.legacyDeploymentEnabled = nullBoolHandler(obj, "LegacyDeploymentEnabled");
            this.legacyDeploymentSchedule = nullStringHandler(obj, "LegacyDeploymentSchedule");
            this.legacyDPLocality = nullIntHandler(obj, "LegacyDPLocality");
            this.legacyForceReboot = nullBoolHandler(obj, "LegacyForceReboot");
            this.legacyInstallAllOnDeadline = nullBoolHandler(obj, "LegacyInstallAllOnDeadline");
            this.legacyInstallAllowedWindow = nullIntHandler(obj, "LegacyInstallAllowedWindow");
            this.legacyPostponeInstall = nullBoolHandler(obj, "LegacyPostponeInstall");
            this.legacyProgramFlags = nullIntHandler(obj, "LegacyProgramFlags");
            this.legacyRebootCountdown = nullIntHandler(obj, "LegacyRebootCountdown");
            this.legacySilentInstall = nullBoolHandler(obj, "LegacySilentInstall");
            this.localeID = nullIntHandler(obj, "LocaleID");
            this.logComplianceToWinEvent = nullBoolHandler(obj, "LogComplianceToWinEvent");
            this.nonComplianceCriticality = nullIntHandler(obj, "NonComplianceCriticality");
            this.notifyUser = nullBoolHandler(obj, "NotifyUser");
            this.overrideServiceWindows = nullBoolHandler(obj, "OverrideServiceWindows");
            this.raiseMomAlertsOnFailure = nullBoolHandler(obj, "RaiseMomAlertsOnFailure");
            this.readOnly = nullBoolHandler(obj, "ReadOnly");
            this.rebootOutsideOfServiceWindows = nullBoolHandler(obj, "RebootOutsideOfServiceWindows");
            this.sendDetailedNonComplianceStatus = nullBoolHandler(obj, "SendDetailedNonComplianceStatus");
            this.sourceSite = nullStringHandler(obj, "SourceSite");
            this.startTime = nullDateTimeHandler(obj, "StartTime");
            this.suppressReboot = nullIntHandler(obj, "SuppressReboot");
            this.targetCollectionID = nullStringHandler(obj, "TargetCollectionID");
            this.useGMTTimes = nullBoolHandler(obj, "UseGMTTimes");
            this.wolEnabled = nullBoolHandler(obj, "WoLEnabled");
            this.applyToSubTargets = nullBoolHandler(obj, "ApplyToSubargets");
        }
        [ActivityOutput, ActivityFilter]
        public bool ApplyToSubTargets
        {
            get { return applyToSubTargets; }
        }
        [ActivityOutput, ActivityFilter]
        public String AssignedCIs
        {
            get { return assignedCIs; }
        }
        [ActivityOutput, ActivityFilter]
        public int AssignmentAction
        {
            get { return assignmentAction; }
        }
        [ActivityOutput, ActivityFilter]
        public String AssignmentDescription
        {
            get { return assignmentDescription; }
        }
        [ActivityOutput, ActivityFilter]
        public int AssignmentID
        {
            get { return assignmentID; }
        }
        [ActivityOutput, ActivityFilter]
        public String AssignmentName
        {
            get { return assignmentName; }
        }
        [ActivityOutput, ActivityFilter]
        public String AssignmentUniqueID
        {
            get { return assignmentUniqueID; }
        }
        [ActivityOutput, ActivityFilter]
        public bool ContainsExpiredUpdates
        {
            get { return containsExpiredUpdates; }
        }
        [ActivityOutput, ActivityFilter]
        public String CreationTime
        {
            get { return creationTime; }
        }
        [ActivityOutput, ActivityFilter]
        public int DesiredConfigType
        {
            get { return desiredConfigType; }
        }
        [ActivityOutput, ActivityFilter]
        public bool DisableMomAlerts
        {
            get { return disableMomAlerts; }
        }
        [ActivityOutput, ActivityFilter]
        public int DPLocality
        {
            get { return dpLocality; }
        }
        [ActivityOutput, ActivityFilter]
        public String EnforcementDeadline
        {
            get { return enforcementDeadline; }
        }
        [ActivityOutput, ActivityFilter]
        public String EvaluationSchedule
        {
            get { return evaluationSchedule; }
        }
        [ActivityOutput, ActivityFilter]
        public String ExpirationTime
        {
            get { return expirationTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastModificationTime
        {
            get { return lastModificationTime; }
        }
        [ActivityOutput, ActivityFilter]
        public uint LegacyAdvertFlags
        {
            get { return (uint)legacyAdvertFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LegacyCollectInventory
        {
            get { return legacyCollectInventory; }
        }
        [ActivityOutput, ActivityFilter]
        public String LegacyDependentProgram
        {
            get { return legacyDependentProgram; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LegacyDeploymentEnabled
        {
            get { return legacyDeploymentEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String LegacyDeploymentSchedule
        {
            get { return legacyDeploymentSchedule; }
        }
        [ActivityOutput, ActivityFilter]
        public int LegacyDPLocality
        {
            get { return legacyDPLocality; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LegacyForceReboot
        {
            get { return legacyForceReboot; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LegacyInstallAllOnDeadline
        {
            get { return legacyInstallAllOnDeadline; }
        }
        [ActivityOutput, ActivityFilter]
        public int LegacyInstallAllowedWindow
        {
            get { return legacyInstallAllowedWindow; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LegacyPostponeInstall
        {
            get { return legacyPostponeInstall; }
        }
        [ActivityOutput, ActivityFilter]
        public uint LegacyProgramFlags
        {
            get { return (uint)legacyProgramFlags; }
        }
        [ActivityOutput, ActivityFilter]
        public int LegacyRebootCountdown
        {
            get { return legacyRebootCountdown; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LegacySilentInstall
        {
            get { return legacySilentInstall; }
        }
        [ActivityOutput, ActivityFilter]
        public int LocaleID
        {
            get { return localeID; }
        }
        [ActivityOutput, ActivityFilter]
        public bool LogComplianceToWinEvent
        {
            get { return logComplianceToWinEvent; }
        }
        [ActivityOutput, ActivityFilter]
        public int NonComplianceCriticality
        {
            get { return nonComplianceCriticality; }
        }
        [ActivityOutput, ActivityFilter]
        public bool NotifyUser
        {
            get { return notifyUser; }
        }
        [ActivityOutput, ActivityFilter]
        public bool OverrideServiceWindows
        {
            get { return overrideServiceWindows; }
        }
        [ActivityOutput, ActivityFilter]
        public bool RaiseMomAlertsOnFailure
        {
            get { return raiseMomAlertsOnFailure; }
        }
        [ActivityOutput, ActivityFilter]
        public bool ReadOnly
        {
            get { return readOnly; }
        }
        [ActivityOutput, ActivityFilter]
        public bool RebootOutsideOfServiceWindows
        {
            get { return rebootOutsideOfServiceWindows; }
        }
        [ActivityOutput, ActivityFilter]
        public bool SendDetailedNonComplianceStatus
        {
            get { return sendDetailedNonComplianceStatus; }
        }
        [ActivityOutput, ActivityFilter]
        public String SourceSite
        {
            get { return sourceSite; }
        }
        [ActivityOutput, ActivityFilter]
        public String StartTime
        {
            get { return startTime; }
        }
        [ActivityOutput, ActivityFilter]
        public int SuppressReboot
        {
            get { return suppressReboot; }
        }
        [ActivityOutput, ActivityFilter]
        public String TargetCollectionID
        {
            get { return targetCollectionID; }
        }
        [ActivityOutput, ActivityFilter]
        public bool UseGMTTimes
        {
            get { return useGMTTimes; }
        }
        [ActivityOutput, ActivityFilter]
        public bool WoLEnabled
        {
            get { return wolEnabled; }
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

