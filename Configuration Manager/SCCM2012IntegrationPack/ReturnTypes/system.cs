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
    [ActivityData("System Resource")]
    public class system
    {
        private int active;
        private String adDomainName;
        private String adSiteName;
        private String agentName;
        private String agentSite;
        private String agentTime;
        private int alwaysInternet;
        private String amtFullVersion;
        private int amtStatus;
        private int client;
        private int clientType;
        private String clientVersion;
        private String cpuType;
        private String creationDate;
        private int decommissioned;
        private String hardwareID;
        private int internetEnabled;
        private String ipAddresses;
        private String ipSubnets;
        private String ipv6Addresses;
        private String ipv6Prefixes;
        private String ipXAddresses;
        private int isClientAMT30Compatible;
        private String lastLogonTimestamp;
        private String lastLogonUserDomain;
        private String lastLogonUserName;
        private String macAddresses;
        private String managedBy;
        private String name;
        private String netbiosName;
        private int obsolete;
        private String operatingSystemNameandVersion;
        private String previousSMSUUID;
        private int primaryGroupID;
        private String pwdLastSet;
        private String resourceDomainOrWorkgroup;
        private int resourceId;
        private String resourceNames;
        private int resourceType;
        private String smBiosGUID;
        private String smsAssignedSites;
        private String smsInstalledSites;
        private String smsUniqueIdentifier;
        private String smsUUIDChangeDate;
        private String snmpCommunityName;
        private int suppressAutoProvision;
        private String systemContainerName;
        private String systemGroupName;
        private String systemOUName;
        private String systemRoles;
        private int unknown;
        private int userAccountControl;
        private String whenChanged;


        internal system(IResultObject system)
        {
            system.Get();
            this.active = nullIntHandler(system, "Active");
            this.adDomainName = nullStringHandler(system, "ADDomainName");
            this.adSiteName = nullStringHandler(system, "ADSiteName");
            this.agentName = convertStringArray(system["AgentName"].StringArrayValue);
            this.agentSite = convertStringArray(system["AgentSite"].StringArrayValue);
            this.agentTime = convertDateTimeArray(system["AgentTime"].DateTimeArrayValue);
            this.alwaysInternet = nullIntHandler(system, "AlwaysInternet");
            this.amtFullVersion = nullStringHandler(system, "AMTFullVersion");
            this.amtStatus = nullIntHandler(system, "AMTStatus");
            this.client = nullIntHandler(system, "Client");
            this.clientType = nullIntHandler(system, "ClientType");
            this.clientVersion = nullStringHandler(system, "ClientVersion");
            this.cpuType = nullStringHandler(system, "CPUType");
            this.creationDate = nullDateTimeHandler(system, "CreationDate");
            this.decommissioned = nullIntHandler(system, "Decommissioned");
            this.hardwareID = nullStringHandler(system, "HardwareID");
            this.internetEnabled = nullIntHandler(system, "InternetEnabled");
            this.ipAddresses = convertStringArray(system["IPAddresses"].StringArrayValue);
            this.ipSubnets = convertStringArray(system["IPSubnets"].StringArrayValue);
            this.ipv6Addresses = convertStringArray(system["IPv6Addresses"].StringArrayValue);
            this.ipv6Prefixes = convertStringArray(system["IPv6Prefixes"].StringArrayValue);
            try
            {
                this.ipXAddresses = convertStringArray(system["IPXAddresses"].StringArrayValue);
            }
            catch
            { }
            this.isClientAMT30Compatible = nullIntHandler(system, "IsClientAMT30Compatible");
            this.lastLogonTimestamp = convertIntToString(system, "lastLogonTimestamp");
            this.lastLogonUserDomain = nullStringHandler(system, "lastLogonUserDomain");
            this.lastLogonUserName = nullStringHandler(system, "lastLogonUserName");
            this.macAddresses = convertStringArray(system["MACAddresses"].StringArrayValue);
            this.managedBy = nullStringHandler(system, "managedBy");
            this.name = nullStringHandler(system, "Name");
            this.netbiosName = nullStringHandler(system, "NetbiosName");
            this.obsolete = nullIntHandler(system, "Obsolete");
            this.operatingSystemNameandVersion = nullStringHandler(system, "OperatingSystemNameandVersion");
            this.previousSMSUUID = nullStringHandler(system, "PreviousSMSUUID");
            this.primaryGroupID = nullIntHandler(system, "PrimaryGroupID");
            this.pwdLastSet = convertIntToString(system, "pwdLastSet");
            this.resourceDomainOrWorkgroup = nullStringHandler(system, "ResourceDomainORWorkgroup");
            this.resourceId = nullIntHandler(system, "ResourceId");
            this.resourceNames = convertStringArray(system["ResourceNames"].StringArrayValue);
            this.resourceType = nullIntHandler(system, "ResourceType");
            this.smBiosGUID = nullStringHandler(system, "SMBIOSGUID");
            this.smsAssignedSites = convertStringArray(system["SMSAssignedSites"].StringArrayValue);
            this.smsInstalledSites = convertStringArray(system["SMSInstalledSites"].StringArrayValue);
            this.smsUniqueIdentifier = nullStringHandler(system, "SMSUniqueIdentifier");
            this.smsUUIDChangeDate = nullDateTimeHandler(system, "SMSUUIDChangeDate");
            this.snmpCommunityName = nullStringHandler(system, "SNMPCommunityName");
            this.suppressAutoProvision = nullIntHandler(system, "SuppressAutoProvision");
            this.systemContainerName = convertStringArray(system["SystemContainerName"].StringArrayValue);
            this.systemGroupName = convertStringArray(system["SystemGroupName"].StringArrayValue);
            this.systemOUName = convertStringArray(system["SystemOUName"].StringArrayValue);
            this.systemRoles = convertStringArray(system["SystemRoles"].StringArrayValue);
            this.unknown = nullIntHandler(system, "Unknown");
            this.userAccountControl = nullIntHandler(system, "UserAccountControl");
            this.whenChanged = nullDateTimeHandler(system, "whenChanged");
        }

        [ActivityOutput, ActivityFilter]
        public int Active
        {
            get { return active; }
        }
        [ActivityOutput, ActivityFilter]
        public String ADDomainName
        {
            get { return adDomainName; }
        }
        [ActivityOutput, ActivityFilter]
        public String ADSiteName
        {
            get { return adSiteName; }
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
        public int AlwaysInternet
        {
            get { return alwaysInternet; }
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
        public int Client
        {
            get { return client; }
        }
        [ActivityOutput, ActivityFilter]
        public int ClientType
        {
            get { return clientType; }
        }
        [ActivityOutput, ActivityFilter]
        public String ClientVersion
        {
            get { return clientVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String CPUType
        {
            get { return cpuType; }
        }
        [ActivityOutput, ActivityFilter]
        public String CreationDate
        {
            get { return creationDate; }
        }
        [ActivityOutput, ActivityFilter]
        public int Decommissioned
        {
            get { return decommissioned; }
        }
        [ActivityOutput, ActivityFilter]
        public String HardwareID
        {
            get { return hardwareID; }
        }
        [ActivityOutput, ActivityFilter]
        public int InternetEnabled
        {
            get { return internetEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String IPAddresses
        {
            get { return ipAddresses; }
        }
        [ActivityOutput, ActivityFilter]
        public String IPSubnets
        {
            get { return ipSubnets; }
        }
        [ActivityOutput, ActivityFilter]
        public String IPv6Addresses
        {
            get { return ipv6Addresses; }
        }
        [ActivityOutput, ActivityFilter]
        public String IPv6Prefixes
        {
            get { return ipv6Prefixes; }
        }
        [ActivityOutput, ActivityFilter]
        public String IPXAddresses
        {
            get { return ipXAddresses; }
        }
        [ActivityOutput, ActivityFilter]
        public int IsClientAMT30Compatible
        {
            get { return isClientAMT30Compatible; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastLogonTimestamp
        {
            get { return lastLogonTimestamp; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastLogonUserDomain
        {
            get { return lastLogonUserDomain; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastLogonUserName
        {
            get { return lastLogonUserName; }
        }
        [ActivityOutput, ActivityFilter]
        public String MACAddresses
        {
            get { return macAddresses; }
        }
        [ActivityOutput, ActivityFilter]
        public String ManagedBy
        {
            get { return managedBy; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }
        [ActivityOutput, ActivityFilter]
        public String NetbiosName
        {
            get { return netbiosName; }
        }
        [ActivityOutput, ActivityFilter]
        public String OperatingSystemNameandVersion
        {
            get { return operatingSystemNameandVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public int PrimaryGroupID
        {
            get { return primaryGroupID; }
        }
        [ActivityOutput, ActivityFilter]
        public String PWDLastSet
        {
            get { return pwdLastSet; }
        }
        [ActivityOutput, ActivityFilter]
        public String ResourceDomainOrWorkgroup
        {
            get { return resourceDomainOrWorkgroup; }
        }
        [ActivityOutput, ActivityFilter]
        public int ResourceId
        {
            get { return resourceId; }
        }
        [ActivityOutput, ActivityFilter]
        public String ResourceNames
        {
            get { return resourceNames; }
        }
        [ActivityOutput, ActivityFilter]
        public String SMBiosGUID
        {
            get { return smBiosGUID; }
        }
        [ActivityOutput, ActivityFilter]
        public String SMSAssignedSites
        {
            get { return smsAssignedSites; }
        }
        [ActivityOutput, ActivityFilter]
        public String SMSInstalledSites
        {
            get { return smsInstalledSites; }
        }
        [ActivityOutput, ActivityFilter]
        public String SMSUniqueIdentifier
        {
            get { return smsUniqueIdentifier; }
        }
        [ActivityOutput, ActivityFilter]
        public String SMSUUIDChangeDate
        {
            get { return smsUUIDChangeDate; }
        }
        [ActivityOutput, ActivityFilter]
        public String SNMPCommunityName
        {
            get { return snmpCommunityName; }
        }
        [ActivityOutput, ActivityFilter]
        public int SuppressAutoProvision
        {
            get { return suppressAutoProvision; }
        }
        [ActivityOutput, ActivityFilter]
        public String SystemContainerName
        {
            get { return systemContainerName; }
        }
        [ActivityOutput, ActivityFilter]
        public String SystemGroupName
        {
            get { return systemGroupName; }
        }
        [ActivityOutput, ActivityFilter]
        public String SystemOUName
        {
            get { return systemOUName; }
        }
        [ActivityOutput, ActivityFilter]
        public String SystemRoles
        {
            get { return systemRoles; }
        }
        [ActivityOutput, ActivityFilter]
        public int Unknown
        {
            get { return unknown; }
        }
        [ActivityOutput, ActivityFilter]
        public int UserAccountControl
        {
            get { return userAccountControl; }
        }
        [ActivityOutput, ActivityFilter]
        public String WhenChanged
        {
            get { return whenChanged; }
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

