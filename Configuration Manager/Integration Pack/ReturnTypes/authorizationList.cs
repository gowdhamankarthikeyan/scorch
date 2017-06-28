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
    [ActivityData("Authorization List")]
    public class authorizationList
    {
        private String applicabilityCondition;
        private String categoryInstance_UniqueIDs;
        private int ci_ID;
        private String ci_UniqueID;
        private int ciType_ID;
        private int ciVersion;
        private String createdBy;
        private String dateCreated;
        private String dateLastModified;
        private String effectiveDate;
        private int eulaAccepted;
        private bool eulaExists;
        private String eulaSignoffDate;
        private String eulaSignoffUser;
        private bool isBundle;
        private bool isDigest;
        private bool isEnabled;
        private bool isHidden;
        private bool isQuarantined;
        private bool isSuperseded;
        private bool isUserDefined;
        private String lastModifiedBy;
        private String localizedCategoryInstanceNames;
        private String localizedDescription;
        private String localizedDisplayName;
        private String localizedInformation;
        private String localizedInformativeURL;
        private int localizedPropertyLocaleID;
        private String modelName;
        private int permittedUsers;
        private int sdmPackageVersion;
        private String sdmPackageXML;
        private String sourceSite;
        private String updates;

        internal authorizationList(IResultObject obj)
        {
            obj.Get();
            this.applicabilityCondition = nullStringHandler(obj, "ApplicabilityCondition");
            this.categoryInstance_UniqueIDs = convertStringArray(obj["CategoryInstance_UniqueIDs"].StringArrayValue);
            this.ci_ID = nullIntHandler(obj, "CI_ID");
            this.ci_UniqueID = nullStringHandler(obj, "CI_UniqueID");
            this.ciType_ID = nullIntHandler(obj, "CIType_ID");
            this.ciVersion = nullIntHandler(obj, "CIVersion");
            this.createdBy = nullStringHandler(obj, "CreatedBy");
            this.dateCreated = nullDateTimeHandler(obj, "DateCreated");
            this.dateLastModified = nullDateTimeHandler(obj, "DateLastModified");
            this.effectiveDate = nullDateTimeHandler(obj, "EffectiveDate");
            this.eulaAccepted = nullIntHandler(obj, "EulaAccepted");
            this.eulaExists = nullBoolHandler(obj, "EulaExists");
            this.eulaSignoffDate = nullDateTimeHandler(obj, "EulaSignoffDate");
            this.eulaSignoffUser = nullStringHandler(obj, "EulaSignoffUser");
            this.isBundle = nullBoolHandler(obj, "IsBundle");
            this.isDigest = nullBoolHandler(obj, "IsDigest");
            this.isEnabled = nullBoolHandler(obj, "IsEnabled");
            this.isHidden = nullBoolHandler(obj, "IsHidden");
            this.isQuarantined = nullBoolHandler(obj, "IsQuarantined");
            this.isSuperseded = nullBoolHandler(obj, "IsSuperseded");
            this.isUserDefined = nullBoolHandler(obj, "IsUserDefined");
            this.lastModifiedBy = nullStringHandler(obj, "LastModifiedBy");
            this.localizedCategoryInstanceNames = convertStringArray(obj["LocalizedCategoryInstanceNames"].StringArrayValue);
            this.localizedDescription = nullStringHandler(obj, "LocalizedDescription");
            this.localizedDisplayName = nullStringHandler(obj, "LocalizedDisplayName");
            this.localizedInformation = generateLocalizedInformation(obj);
            this.localizedInformativeURL = nullStringHandler(obj, "LocalizedInformativeURL");
            this.localizedPropertyLocaleID = nullIntHandler(obj, "LocalizedPropertyLocaleID");
            this.modelName = nullStringHandler(obj, "ModelName");
            this.permittedUsers = nullIntHandler(obj, "PermittedUsers");
            this.sdmPackageVersion = nullIntHandler(obj, "SDMPackageVersion");
            this.sdmPackageXML = nullStringHandler(obj, "SDMPackageXML");
            this.sourceSite = nullStringHandler(obj, "SourceSite");
            this.updates = convertIntArray(obj["Updates"].IntegerArrayValue);
        }
        [ActivityOutput, ActivityFilter]
        public String ApplicabilityCondition
        {
            get { return applicabilityCondition; }
        }
        [ActivityOutput, ActivityFilter]
        public String CategoryInstance_UniqueIDs
        {
            get { return categoryInstance_UniqueIDs; }
        }
        [ActivityOutput, ActivityFilter]
        public int CI_ID
        {
            get { return ci_ID; }
        }
        [ActivityOutput, ActivityFilter]
        public String CI_UniqueID
        {
            get { return ci_UniqueID; }
        }
        [ActivityOutput, ActivityFilter]
        public int CIType_ID
        {
            get { return ciType_ID; }
        }
        [ActivityOutput, ActivityFilter]
        public int CIVersion
        {
            get { return ciVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String CreatedBy
        {
            get { return createdBy; }
        }
        [ActivityOutput, ActivityFilter]
        public String DateCreated
        {
            get { return dateCreated; }
        }
        [ActivityOutput, ActivityFilter]
        public String DateLastModified
        {
            get { return dateLastModified; }
        }
        [ActivityOutput, ActivityFilter]
        public String EffectiveDate
        {
            get { return effectiveDate; }
        }
        [ActivityOutput, ActivityFilter]
        public int EulaAccepted
        {
            get { return eulaAccepted; }
        }
        [ActivityOutput, ActivityFilter]
        public bool EulaExists
        {
            get { return eulaExists; }
        }
        [ActivityOutput, ActivityFilter]
        public String EulaSignoffDate
        {
            get { return eulaSignoffDate; }
        }
        [ActivityOutput, ActivityFilter]
        public String EulaSignoffUser
        {
            get { return eulaSignoffUser; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsBundle
        {
            get { return isBundle; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsDigest
        {
            get { return isDigest; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsEnabled
        {
            get { return isEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsHidden
        {
            get { return isHidden; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsQuarantined
        {
            get { return isQuarantined; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsSuperseded
        {
            get { return isSuperseded; }
        }
        [ActivityOutput, ActivityFilter]
        public bool IsUserDefined
        {
            get { return isUserDefined; }
        }
        [ActivityOutput, ActivityFilter]
        public String LastModifiedBy
        {
            get { return lastModifiedBy; }
        }
        [ActivityOutput, ActivityFilter]
        public String LocalizedCategoryInstanceNames
        {
            get { return localizedCategoryInstanceNames; }
        }
        [ActivityOutput, ActivityFilter]
        public String LocalizedDescription
        {
            get { return localizedDescription; }
        }
        [ActivityOutput, ActivityFilter]
        public String LocalizedDisplayName
        {
            get { return localizedDisplayName; }
        }
        [ActivityOutput, ActivityFilter]
        public String LocalizedInformation
        {
            get { return localizedInformation; }
        }
        [ActivityOutput, ActivityFilter]
        public String LocalizedInformativeURL
        {
            get { return localizedInformativeURL; }
        }
        [ActivityOutput, ActivityFilter]
        public int LocalizedPropertyLocaleID
        {
            get { return localizedPropertyLocaleID; }
        }
        [ActivityOutput, ActivityFilter]
        public String ModelName
        {
            get { return modelName; }
        }
        [ActivityOutput, ActivityFilter]
        public int PermittedUsers
        {
            get { return permittedUsers; }
        }
        [ActivityOutput, ActivityFilter]
        public int SDMPackageVersion
        {
            get { return sdmPackageVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String SDMPackageXML
        {
            get { return sdmPackageXML; }
        }
        [ActivityOutput, ActivityFilter]
        public String SourceSite
        {
            get { return sourceSite; }
        }
        [ActivityOutput, ActivityFilter]
        public String Updates
        {
            get { return updates; }
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
                        retValue = retValue +  ",Description:" + Convert.ToString(result["Description"].StringValue) + "-DisplayName:" + Convert.ToString(result["DisplayName"].StringValue) + "-InformativeURL:" + Convert.ToString(result["InformativeURL"].StringValue) + "-LocaleID:" + Convert.ToString(result["LocaleID"].StringValue);
                    }
                }
            }
            catch { }

            return retValue;
        }
    }
}

