/*
 * Copyright (c) 2011 Ryan Andorfer
 * Copyright (c) Microsoft under Microsoft Public License (Ms-PL)
 *
 * Microsoft System Center Configuration Manager Interoperability functions
 *
 * $HeadURL$
 *
 * $Id$
 *
 * $LastChangedDate$
 *
 * $LastChangedRevision$
 *
 * Code by Ryan Andorfer and other contributors
 * http://www.codeplex.com/site/users/view/randorfer
 * 
 * http://scorch.codeplex.com/SourceControl/list/changesets
 * 
 * see also:
 * http://opalissccmextension.codeplex.com/
 * http://opalissccmextension.codeplex.com/SourceControl/changeset/view/14517#125728
 * http://opalis.wordpress.com/2011/05/17/sccm-extension-integration-pack-testers-wanted/
 * http://www.java2s.com/Open-Source/CSharp/Workflow/OpalisSCCMExtension/OpalisSCCMExtension/SCCMInterop.cs.htm
 * 
 *   Doxygen comments by Wolfgang Fahl
 */
#pragma warning disable 0168
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;

namespace SCCM2012Interop
{
    public class CM2012Interop
    {
        #region Resource Functions
        public static IResultObject getSCCMComputer(WqlConnectionManager connection, String ResourceID, String NetbiosName, String DomainName)
        {
            String smsQueryFilter = String.Empty;

            if (!ResourceID.Equals(String.Empty)) { smsQueryFilter = "ResourceID LIKE '" + ResourceID + "'"; }
            if (!NetbiosName.Equals(String.Empty))
            {
                if (smsQueryFilter.Equals(String.Empty)) { smsQueryFilter = "NetbiosName LIKE '" + NetbiosName + "'"; }
                else
                {
                    smsQueryFilter += " AND NetbiosName LIKE '" + NetbiosName + "'";
                }
            }
            if (!DomainName.Equals(String.Empty))
            {
                if (smsQueryFilter.Equals(String.Empty)) { smsQueryFilter = "DomainName LIKE '" + DomainName + "'"; }
                else { smsQueryFilter += " AND DomainName LIKE '" + DomainName + "'"; }
            }

            return getSCCMObject(connection, "SMS_R_System", smsQueryFilter);
        }
        public static IResultObject getSCCMUser(WqlConnectionManager connection, String ResourceID, String UserName, String DomainName)
        {
            String smsQueryFilter = String.Empty;

            if (!ResourceID.Equals(String.Empty)) { smsQueryFilter = "ResourceID LIKE '" + ResourceID + "'"; }
            if (!UserName.Equals(String.Empty))
            {
                if (smsQueryFilter.Equals(String.Empty)) { smsQueryFilter = "UserName LIKE '" + UserName + "'"; }
                else
                {
                    smsQueryFilter += " AND UserName LIKE '" + UserName + "'";
                }
            }
            if (!DomainName.Equals(String.Empty))
            {
                if (smsQueryFilter.Equals(String.Empty)) { smsQueryFilter = "ResourceID LIKE '" + ResourceID + "'"; }
                else { smsQueryFilter += " AND DomainName LIKE '" + DomainName + "'"; }
            }

            return getSCCMObject(connection, "SMS_R_User", smsQueryFilter);
        }
        public static IResultObject getSCCMUserGroup(WqlConnectionManager connection, String ResourceID, String groupName, String DomainName)
        {
            String smsQueryFilter = String.Empty;

            if (!ResourceID.Equals(String.Empty)) { smsQueryFilter = "ResourceID LIKE '" + ResourceID + "'"; }
            if (!groupName.Equals(String.Empty))
            {
                if (smsQueryFilter.Equals(String.Empty)) { smsQueryFilter = "groupName LIKE '" + groupName + "'"; }
                else
                {
                    smsQueryFilter += " AND groupName LIKE '" + groupName + "'";
                }
            }
            if (!DomainName.Equals(String.Empty))
            {
                if (smsQueryFilter.Equals(String.Empty)) { smsQueryFilter = "ResourceID LIKE '" + ResourceID + "'"; }
                else { smsQueryFilter += " AND DomainName LIKE '" + DomainName + "'"; }
            }

            return getSCCMObject(connection, "SMS_R_UserGroup", smsQueryFilter);
        }
        #endregion

        #region Utility Functions
        public static IResultObject getSCCMObject(WqlConnectionManager connection, String objectClass, String filter)
        {
            String query;

            if (filter == String.Empty)
            {
                query = "Select * from " + objectClass;
            }
            else
            {
                query = "Select * from " + objectClass + " WHERE " + filter;
            }

            return connection.QueryProcessor.ExecuteQuery(query);
        }
        public static WqlConnectionManager connectSCCMServer(String serverName, String UserName, String Password)
        {
            WqlConnectionManager connection = new WqlConnectionManager();
            connection.Connect(serverName, UserName, Password);
            return connection;
        }
        public static String[] getSCCMObjectPropertyNames(WqlConnectionManager connection, String objectClass)
        {
            String[] retArray = null;

            IResultObject obj = connection.CreateInstance(objectClass);
            retArray = obj.PropertyNames;

            return retArray;
        }
        #endregion

        #region Package Functions
        public static IResultObject getSCCMPackage(WqlConnectionManager connection, String filter)
        {
            return getSCCMObject(connection, "SMS_Package", filter);
        }
        public static IResultObject createSCCMPackage(WqlConnectionManager connection, String newPackageName, String newPackageDescription, int newPackageSourceFlag, String newPackageSourcePath)
        {
            try
            {
                // Create new package object.
                IResultObject newPackage = connection.CreateInstance("SMS_Package");

                // Populate new package properties.
                newPackage["Name"].StringValue = newPackageName;
                newPackage["Description"].StringValue = newPackageDescription;
                newPackage["PkgSourceFlag"].IntegerValue = newPackageSourceFlag;
                newPackage["PkgSourcePath"].StringValue = newPackageSourcePath;

                // Save new package and new package properties.
                newPackage.Put();
                newPackage.Get();
                return newPackage;
            }

            catch (SmsException ex)
            {
                throw new Exception("Failed to create package. Error: " + ex.Message);
            }
        }
        public static IResultObject configureSCCMPackageDescription(WqlConnectionManager connection, String existingPackageID, String newPackageDescription)
        {
            try
            {
                // Get specific package instance to modify.
                IResultObject packageToConfigure = connection.GetInstance(@"SMS_Package.PackageID='" + existingPackageID + "'");

                // Replace the existing package property with the new value (in this case the package description).
                packageToConfigure["Description"].StringValue = newPackageDescription;

                // Save package and modified package properties.
                packageToConfigure.Put();
                packageToConfigure.Get();
                return packageToConfigure;
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject configureSCCMPackageSourcePath(WqlConnectionManager connection, String existingPackageID, String newPackageSourcePath)
        {
            try
            {
                // Get specific package instance to modify.
                IResultObject packageToConfigure = connection.GetInstance(@"SMS_Package.PackageID='" + existingPackageID + "'");

                // Replace the existing package property with the new value (in this case the package description).
                packageToConfigure["PkgSourcePath"].StringValue = newPackageSourcePath;

                // Save package and modified package properties.
                packageToConfigure.Put();
                packageToConfigure.Get();

                return packageToConfigure;
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void deleteSCCMPackage(WqlConnectionManager connection, String existingPackageID)
        {
            try
            {
                // Get the specified package instance (passed in as existingPackageID).
                IResultObject packageToDelete = connection.GetInstance(@"SMS_Package.PackageID='" + existingPackageID + "'");

                // Delete the package instance.
                packageToDelete.Delete();
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void refreshSCCMPackageAtDistributionPoint(WqlConnectionManager connection, String existingPackageID, String siteCode, String serverName, String NALPathQuery)
        {
            try
            {
                // This query selects System Resource
                string query = "SELECT * FROM SMS_SystemResourceList WHERE RoleName='SMS Distribution Point' AND SiteCode='" + siteCode + "' AND ServerName='" + serverName + "' AND NALPAth Like '" + NALPathQuery + "'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    string dpQuery = "SELECT * FROM SMS_DistributionPoint WHERE PackageID='" + existingPackageID + "'";
                    IResultObject listOfDistributionPoints = connection.QueryProcessor.ExecuteQuery(dpQuery);
                    foreach (IResultObject dp in listOfDistributionPoints)
                    {
                        if (dp["ServerNALPath"].StringValue.Equals(resource["NalPath"].StringValue))
                        {
                            //Set Refresh Flag
                            dp["RefreshNow"].BooleanValue = true;
                            dp.Put();
                            break;
                        }
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void refreshSCCMPackageAtDistributionPointGroup(WqlConnectionManager connection, String existingPackageID, String distributionPointGroupName)
        {
            try
            {
                // This query selects a group of distribution points based on sGroupName
                string query = "SELECT * FROM SMS_DistributionPointGroup WHERE sGroupName LIKE '" + distributionPointGroupName + "'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    foreach (String NALPath in resource["arrNALPath"].StringArrayValue)
                    {
                        string dpQuery = "SELECT * FROM SMS_DistributionPoint WHERE PackageID='" + existingPackageID + "'";
                        IResultObject listOfDistributionPoints = connection.QueryProcessor.ExecuteQuery(dpQuery);
                        foreach (IResultObject dp in listOfDistributionPoints)
                        {
                            if (dp["ServerNALPath"].StringValue.Equals(NALPath))
                            {
                                //Set Refresh Flag
                                dp["RefreshNow"].BooleanValue = true;
                                dp.Put();
                            }
                        }
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void refreshSCCMPackageAtAllDistributionPoints(WqlConnectionManager connection, String existingPackageID)
        {
            try
            {
                // This query selects all SMS Distribution Point Servers
                string query = "SELECT * FROM SMS_SystemResourceList WHERE RoleName='SMS Distribution Point'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    string dpQuery = "SELECT * FROM SMS_DistributionPoint WHERE PackageID='" + existingPackageID + "'";
                    IResultObject listOfDistributionPoints = connection.QueryProcessor.ExecuteQuery(dpQuery);
                    foreach (IResultObject dp in listOfDistributionPoints)
                    {
                        if (dp["ServerNALPath"].StringValue.Equals(resource["NalPath"].StringValue))
                        {
                            //Set Refresh Flag
                            dp["RefreshNow"].BooleanValue = true;
                            dp.Put();
                        }
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void assignSCCMPackageToAllDistributionPoints(WqlConnectionManager connection, String existingPackageID)
        {
            try
            {
                // Create the distribution point object (this is not an actual distribution point).
                IResultObject distributionPoint = connection.CreateInstance("SMS_DistributionPoint");

                // Associate the package with the new distribution point object. 
                distributionPoint["PackageID"].StringValue = existingPackageID;

                // This query selects all distribution points
                string query = "SELECT * FROM SMS_SystemResourceList WHERE RoleName='SMS Distribution Point'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    distributionPoint["ServerNALPath"].StringValue = resource["NALPath"].StringValue;
                    distributionPoint["SiteCode"].StringValue = resource["SiteCode"].StringValue;

                    // Save the distribution point object and properties.
                    distributionPoint.Put();
                    distributionPoint.Get();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void assignSCCMPackageToDistributionPointGroup(WqlConnectionManager connection, String existingPackageID, String distributionPointGroupName)
        {
            try
            {
                // Create the distribution point object (this is not an actual distribution point).
                IResultObject distributionPoint = connection.CreateInstance("SMS_DistributionPoint");

                // Associate the package with the new distribution point object. 
                distributionPoint["PackageID"].StringValue = existingPackageID;

                // This query selects a group of distribution points based on sGroupName
                string query = "SELECT * FROM SMS_DistributionPointGroup WHERE sGroupName LIKE '" + distributionPointGroupName + "'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    foreach (String NALPath in resource["arrNALPath"].StringArrayValue)
                    {
                        String SysResourceQuery = "SELECT * FROM SMS_SystemResourceList WHERE RoleName='SMS Distribution Point'";
                        IResultObject listOfSystemResources = connection.QueryProcessor.ExecuteQuery(SysResourceQuery);

                        foreach (IResultObject sysResource in listOfSystemResources)
                        {
                            if (sysResource["NALPath"].StringValue.Equals(NALPath))
                            {
                                distributionPoint["ServerNALPath"].StringValue = sysResource["NALPath"].StringValue;
                                distributionPoint["SiteCode"].StringValue = sysResource["SiteCode"].StringValue;

                                // Save the distribution point object and properties.
                                distributionPoint.Put();
                                distributionPoint.Get();
                            }
                        }
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void assignSCCMPackageToDistributionPoint(WqlConnectionManager connection, String existingPackageID, String siteCode, String serverName, String NALPathQuery)
        {
            try
            {
                // Create the distribution point object (this is not an actual distribution point).
                IResultObject distributionPoint = connection.CreateInstance("SMS_DistributionPoint");

                // Associate the package with the new distribution point object. 
                distributionPoint["PackageID"].StringValue = existingPackageID;

                // This query selects a single distribution point based on the provided siteCode and serverName.
                string query = "SELECT * FROM SMS_SystemResourceList WHERE RoleName='SMS Distribution Point' AND SiteCode='" + siteCode + "' AND ServerName='" + serverName + "' AND NALPAth Like '" + NALPathQuery + "'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    distributionPoint["ServerNALPath"].StringValue = resource["NALPath"].StringValue;
                    distributionPoint["SiteCode"].StringValue = resource["SiteCode"].StringValue;
                }

                // Save the distribution point object and properties.
                distributionPoint.Put();
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject getSCCMPackageDistributionPoints(WqlConnectionManager connection, String existingPackageID)
        {
            return getSCCMObject(connection, "SMS_DistributionPoint", "PackageID='" + existingPackageID + "'");
        }
        public static void removeSCCMPackageFromDistributionPoint(WqlConnectionManager connection, String existingPackageID, String siteCode, String serverName, String NALPathQuery)
        {
            try
            {
                // This query selects the specified package from a single distribution point based on the provided siteCode and serverName.
                string query = "SELECT * FROM SMS_SystemResourceList WHERE RoleName='SMS Distribution Point' AND SiteCode='" + siteCode + "' AND ServerName='" + serverName + "' AND PackageID='" + existingPackageID + "' AND NALPAth Like '" + NALPathQuery + "'";
                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    string dpQuery = "SELECT * FROM SMS_DistributionPoint WHERE PackageID='" + existingPackageID + "' AND ServerNALPath=\"" + resource["NalPath"].StringValue + "\"";
                    IResultObject listOfDistributionPions = connection.QueryProcessor.ExecuteQuery(dpQuery);
                    foreach (IResultObject dp in listOfDistributionPions)
                    {
                        //Delete Package DP
                        dp.Delete();
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void removeSCCMPackageFromDistributionPointGroup(WqlConnectionManager connection, String existingPackageID, String distributionPointGroupName)
        {
            try
            {
                // This query selects a group of distribution points based on sGroupName
                string query = "SELECT * FROM SMS_DistributionPointGroup WHERE sGroupName LIKE '" + distributionPointGroupName + "'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    foreach (String NALPath in resource["arrNALPath"].StringArrayValue)
                    {
                        String DPResourceQuery = "SELECT * FROM SMS_DistributionPoint WHERE PackageID='" + existingPackageID + "'";
                        IResultObject listOfDPs = connection.QueryProcessor.ExecuteQuery(DPResourceQuery);

                        foreach (IResultObject DP in listOfDPs)
                        {
                            if (DP["ServerNALPath"].StringValue.Equals(NALPath))
                            {

                                //Delete the DP
                                DP.Delete();
                            }
                        }
                        /*
                        String DPResourceQuery = "SELECT * FROM SMS_DistributionPoint WHERE ServerNALPath=\"" + NALPath + "\" AND PackageID='" + existingPackageID + "'";
                        IResultObject listOfDPs = connection.QueryProcessor.ExecuteQuery(DPResourceQuery);

                        foreach (IResultObject DP in listOfDPs)
                        {
                            //Delete the DP
                            DP.Delete();
                        }
                         */
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void removeSCCMPackageFromAllDistributionPoints(WqlConnectionManager connection, String existingPackageID)
        {
            try
            {
                // This query selects all distribution points
                string query = "SELECT * FROM SMS_DistributionPoint WHERE PackageID='" + existingPackageID + "'";

                // 
                IResultObject listOfResources = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject resource in listOfResources)
                {
                    //deletes the package DP
                    resource.Delete();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject modifySCCMPackage(WqlConnectionManager connection, String existingPackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_Package.PackageID='" + existingPackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static void refreshSCCMPackageSource(WqlConnectionManager connection, String existingPackageID)
        {
            try
            {
                // Get specific package instance to modify.
                IResultObject package = connection.GetInstance(@"SMS_Package.PackageID='" + existingPackageID + "'");

                // Request Package Refresh
                package.ExecuteMethod("RefreshPkgSource", null);
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        #endregion

        #region Advertisement Functions
        public static IResultObject getSCCMAdvertisement(WqlConnectionManager connection, String filter)
        {
            return getSCCMObject(connection, "SMS_Advertisement", filter);
        }
        public static List<IResultObject> getSCCMAdvertisementStatus(WqlConnectionManager connection, String SystemArgType, String SystemArgValue, String AdvID, bool ReturnAll)
        {
            String query = "SELECT * FROM SMS_R_SYSTEM as sys " +
                           "INNER JOIN SMS_ClientAdvertisementStatus as adv on sys.ResourceId = adv.ResourceId " +
                           "WHERE adv.AdvertisementId LIKE '" + AdvID + "' ";

            switch (SystemArgType)
            {
                case "NetbiosName":
                    query += "AND sys.NetbiosName LIKE '" + SystemArgValue + "'";
                    break;
                case "ResourceId":
                    query += "AND sys.ResourceId LIKE '" + SystemArgValue + "'";
                    break;
                default:
                    query += "AND sys.NetbiosName LIKE '" + SystemArgValue + "'";
                    break;
            }

            IResultObject objCollection = connection.QueryProcessor.ExecuteQuery(query);

            if (ReturnAll)
            {
                List<IResultObject> oCollection = new List<IResultObject>();
                foreach (IResultObject obj in objCollection)
                {
                    IResultObject advProperties = null;
                    foreach (IResultObject o in obj.GenericsArray)
                    {
                        if (o.ObjectClass.Equals("SMS_ClientAdvertisementStatus"))
                        {
                            advProperties = o;
                        }
                    }
                    if (advProperties != null)
                    {
                        oCollection.Add(advProperties);
                    }
                }
                return oCollection;
            }
            else
            {
                List<IResultObject> objToReturn = new List<IResultObject>();
                DateTime newestRecordDate = DateTime.MinValue;
                foreach (IResultObject obj in objCollection)
                {
                    IResultObject advProperties = null;
                    foreach (IResultObject o in obj.GenericsArray)
                    {
                        if (o.ObjectClass.Equals("SMS_ClientAdvertisementStatus"))
                        {
                            advProperties = o;
                            break;
                        }
                    }

                    if (objToReturn == null && advProperties != null)
                    {
                        objToReturn.Clear();
                        objToReturn.Add(advProperties);

                        DateTime InitialDate = DateTime.MinValue;
                        if (advProperties.PropertyList["LastStatusTime"] != String.Empty)
                        {
                            if (advProperties.PropertyList["LastAcceptanceStatusTime"] != String.Empty)
                            {
                                if (advProperties["LastStatusTime"].DateTimeValue > advProperties["LastAcceptanceStatusTime"].DateTimeValue)
                                {
                                    InitialDate = advProperties["LastStatusTime"].DateTimeValue;
                                }
                                else
                                {
                                    InitialDate = advProperties["LastAcceptanceStatusTime"].DateTimeValue;
                                }
                            }
                            else
                            {
                                InitialDate = advProperties["LastStatusTime"].DateTimeValue;
                            }
                        }
                        else
                        {
                            if (advProperties.PropertyList["LastAcceptanceStatusTime"] != String.Empty)
                            {
                                InitialDate = advProperties["LastAcceptanceStatusTime"].DateTimeValue;
                            }
                        }
                        newestRecordDate = InitialDate;
                    }
                    else
                    {
                        if (advProperties != null)
                        {
                            DateTime CompareDate = DateTime.MinValue;

                            if (advProperties.PropertyList["LastStatusTime"] != String.Empty)
                            {
                                if (advProperties.PropertyList["LastAcceptanceStatusTime"] != String.Empty)
                                {
                                    if (advProperties["LastStatusTime"].DateTimeValue > advProperties["LastAcceptanceStatusTime"].DateTimeValue)
                                    {
                                        CompareDate = advProperties["LastStatusTime"].DateTimeValue;
                                    }
                                    else
                                    {
                                        CompareDate = advProperties["LastAcceptanceStatusTime"].DateTimeValue;
                                    }
                                }
                                else
                                {
                                    CompareDate = advProperties["LastStatusTime"].DateTimeValue;
                                }
                            }
                            else
                            {
                                if (advProperties.PropertyList["LastAcceptanceStatusTime"] != String.Empty)
                                {
                                    CompareDate = advProperties["LastAcceptanceStatusTime"].DateTimeValue;
                                }
                            }

                            if (CompareDate > newestRecordDate)
                            {
                                objToReturn.Clear();
                                objToReturn.Add(advProperties);
                                newestRecordDate = CompareDate;
                            }
                        }
                    }
                }
                return objToReturn;
            }
        }
        public static IResultObject modifySCCMAdvertisement(WqlConnectionManager connection, String existingAdvertisementID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_Advertisement.AdvertisementID='" + existingAdvertisementID + "'");

            objToModify.Get();

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject createSCCMAdvertisement(WqlConnectionManager connection, string existingCollectionID, string existingPackageID, string existingProgramName, string newAdvertisementName, string newAdvertisementComment, int newAdvertisementFlags, string newAdvertisementStartOfferDateTime, bool newAdvertisementStartOfferEnabled)
        {
            try
            {
                // Create new advertisement instance.
                IResultObject newAdvertisement = connection.CreateInstance("SMS_Advertisement");

                // Populate new advertisement values.
                newAdvertisement["CollectionID"].StringValue = existingCollectionID;
                newAdvertisement["PackageID"].StringValue = existingPackageID;
                newAdvertisement["ProgramName"].StringValue = existingProgramName;
                newAdvertisement["AdvertisementName"].StringValue = newAdvertisementName;
                newAdvertisement["Comment"].StringValue = newAdvertisementComment;
                newAdvertisement["AdvertFlags"].IntegerValue = newAdvertisementFlags;
                newAdvertisement["PresentTime"].StringValue = newAdvertisementStartOfferDateTime;
                newAdvertisement["PresentTimeEnabled"].BooleanValue = newAdvertisementStartOfferEnabled;

                // Save the new advertisment and properties.
                newAdvertisement.Put();
                newAdvertisement.Get();

                return newAdvertisement;
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void deleteSCCMAdvertisement(WqlConnectionManager connection, String advertisementIDToDelete)
        {
            try
            {
                // Get the specific collection instance to delete.
                IResultObject advertisementToDelete = connection.GetInstance(@"SMS_Advertisement.AdvertisementID='" + advertisementIDToDelete + "'");

                // Delete the collection.
                advertisementToDelete.Delete();
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        /// <summary>
        /// enables scheduling of events that occur at regular intervals, such as every ten days, as opposed to designated dates and times
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="existingAdvertisementID"></param>
        /// <param name="isGMT"></param>
        /// <param name="daySpan"></param>
        /// <param name="hourSpan"></param>
        /// <param name="minuteSpan"></param>
        /// <param name="dayDuration"></param>
        /// <param name="hourDuration"></param>
        /// <param name="minuteDuration"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static IResultObject modifySCCMAdvertisementAddAssignmentScheduleRecurring(WqlConnectionManager connection, String existingAdvertisementID, bool isGMT, int daySpan, int hourSpan, int minuteSpan, int dayDuration, int hourDuration, int minuteDuration, DateTime startTime)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_Advertisement.AdvertisementID='" + existingAdvertisementID + "'");

            objToModify.Get();

            List<IResultObject> tokenList = new List<IResultObject>();
            IResultObject scheduleToken = connection.CreateEmbeddedObjectInstance("SMS_ST_RecurInterval");

            scheduleToken["DaySpan"].IntegerValue = daySpan;
            scheduleToken["HourSpan"].IntegerValue = hourSpan;
            scheduleToken["MinuteSpan"].IntegerValue = minuteSpan;

            scheduleToken["DayDuration"].IntegerValue = dayDuration;
            scheduleToken["HourDuration"].IntegerValue = hourDuration;
            scheduleToken["MinuteDuration"].IntegerValue = minuteDuration;

            scheduleToken["StartTime"].DateTimeValue = startTime;
            scheduleToken["IsGMT"].BooleanValue = isGMT;

            tokenList = objToModify.GetArrayItems("AssignedSchedule");
            tokenList.Add(scheduleToken);

            objToModify["AssignedScheduleEnabled"].BooleanValue = true;
            objToModify["AssignedScheduleIsGMT"].BooleanValue = isGMT;
            objToModify.SetArrayItems("AssignedSchedule", tokenList);
            objToModify.Put();
            objToModify.Get();

            return objToModify;
        }
        /// <summary>
        /// used for non-recurring event scheduling by designating a date and time
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="existingAdvertisementID"></param>
        /// <param name="isGMT"></param>
        /// <param name="dayDuration"></param>
        /// <param name="hourDuration"></param>
        /// <param name="minuteDuration"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public static IResultObject modifySCCMAdvertisementAddAssignmentScheduleNonReccuring(WqlConnectionManager connection, String existingAdvertisementID, bool isGMT, int dayDuration, int hourDuration, int minuteDuration, DateTime startTime)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_Advertisement.AdvertisementID='" + existingAdvertisementID + "'");

            objToModify.Get();

            List<IResultObject> tokenList = new List<IResultObject>();
            IResultObject scheduleToken = connection.CreateEmbeddedObjectInstance("SMS_ST_NonRecurring");

            scheduleToken["DayDuration"].IntegerValue = dayDuration;
            scheduleToken["HourDuration"].IntegerValue = hourDuration;
            scheduleToken["MinuteDuration"].IntegerValue = minuteDuration;
            scheduleToken["StartTime"].DateTimeValue = startTime;

            tokenList = objToModify.GetArrayItems("AssignedSchedule");
            tokenList.Add(scheduleToken);

            objToModify["AssignedScheduleEnabled"].BooleanValue = true;
            objToModify["AssignedScheduleIsGMT"].BooleanValue = isGMT;
            objToModify.SetArrayItems("AssignedSchedule", tokenList);
            objToModify.Put();
            objToModify.Get();

            return objToModify;
        }
        #endregion

        #region Program Functions
        public static IResultObject getSCCMProgram(WqlConnectionManager connection, String filter)
        {
            return getSCCMObject(connection, "SMS_Program", filter);
        }
        public static IResultObject createSCCMProgram(WqlConnectionManager connection, string existingPackageID, string newProgramName, string newProgramComment, string newProgramCommandLine, int newMaxRunTime, string workingDirectory)
        {
            try
            {
                // Create an instance of SMS_Program.
                IResultObject newProgram = connection.CreateInstance("SMS_Program");

                // Populate basic program values.
                newProgram["PackageID"].StringValue = existingPackageID;
                newProgram["ProgramName"].StringValue = newProgramName;
                newProgram["Comment"].StringValue = newProgramComment;
                newProgram["CommandLine"].StringValue = newProgramCommandLine;
                newProgram["Duration"].IntegerValue = newMaxRunTime;
                newProgram["WorkingDirectory"].StringValue = workingDirectory;

                // Save the new program instance and values.
                newProgram.Put();
                newProgram.Get();

                return newProgram;
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void deleteSCCMProgram(WqlConnectionManager connection, String existingPackageID, string existingProgramName)
        {
            try
            {
                String query = "SELECT * FROM SMS_Program WHERE PackageID='" + existingPackageID + "' AND ProgramName='" + existingProgramName + "'";
                // Get the specific program instance to delete.
                IResultObject programToDelete = connection.QueryProcessor.ExecuteQuery(query);
                foreach (IResultObject prg in programToDelete)
                {
                    // Delete the Program.
                    prg.Delete();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject modifySCCMProgramSupportedPlatforms(WqlConnectionManager connection, String existingPackageID, String existingProgramNameToModify, String newMaxVersion, String newMinVersion, String newName, String newPlatform)
        {
            try
            {
                // Define a constant with the hexadecimal value for RUN_ON_SPECIFIED_PLATFORMS. 
                const Int32 RUN_ON_SPECIFIED_PLATFORMS = 0x08000000;

                // Build query to get the programs for the package. 
                String query = "SELECT * FROM SMS_Program WHERE PackageID='" + existingPackageID + "'";

                // Load the specific program to change (programname is a key value and must be unique).
                IResultObject programsForPackage = connection.QueryProcessor.ExecuteQuery(query);

                // The query returns a collection that needs to be enumerated.
                foreach (IResultObject program in programsForPackage)
                {
                    // If a match for the program name is found, make the change(s).
                    if (program["ProgramName"].StringValue == existingProgramNameToModify)
                    {
                        // Get all properties, specifically the lazy properties, for the program object.
                        program.Get();

                        // Check whether RUN_ON_SPECIFIED_PLATFORMS is already set.
                        Int32 checkPlatformValue = (program["ProgramFlags"].IntegerValue ^ RUN_ON_SPECIFIED_PLATFORMS);

                        if (checkPlatformValue == 0)
                        {
                            // RUN_ON_SPECIFIED_PLATFORMS is not set. Setting RUN_ON_SPECIFIED_PLATFORMS value.
                            program["ProgramFlags"].IntegerValue = program["ProgramFlags"].IntegerValue ^ RUN_ON_SPECIFIED_PLATFORMS;
                        }

                        // Create a new array list to hold the supported platform window objects.
                        List<IResultObject> tempSupportedPlatformsArray = new List<IResultObject>();

                        // Create and populate a temporary SMS_OS_Details object with the new OS values.
                        IResultObject tempSupportedPlatformsObject = connection.CreateEmbeddedObjectInstance("SMS_OS_Details");

                        // Populate temporary SMS_OS_Details object with the new supported platforms values.
                        tempSupportedPlatformsObject["MaxVersion"].StringValue = newMaxVersion;
                        tempSupportedPlatformsObject["MinVersion"].StringValue = newMinVersion;
                        tempSupportedPlatformsObject["Name"].StringValue = newName;
                        tempSupportedPlatformsObject["Platform"].StringValue = newPlatform;

                        // Populate the local array list with the existing supported platform objects (type SMS_OS_Details).
                        tempSupportedPlatformsArray = program.GetArrayItems("SupportedOperatingSystems");

                        // Add the newly created service window object to the local array list.
                        tempSupportedPlatformsArray.Add(tempSupportedPlatformsObject);

                        // Replace the existing service window objects from the target collection with the temporary array that includes the new service window.
                        program.SetArrayItems("SupportedOperatingSystems", tempSupportedPlatformsArray);

                        // Save the new values in the collection settings instance associated with the Collection ID.
                        program.Put();
                        program.Get();
                        return program;
                    }
                }
                return null;
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject removeAllSCCMProgramSupportedPlatforms(WqlConnectionManager connection, String existingPackageID, String existingProgramNameToModify)
        {
            try
            {
                // Define a constant with the hexadecimal value for RUN_ON_SPECIFIED_PLATFORMS. 
                const Int32 RUN_ON_SPECIFIED_PLATFORMS = 0x08000000;

                // Build query to get the programs for the package. 
                String query = "SELECT * FROM SMS_Program WHERE PackageID='" + existingPackageID + "'";

                // Load the specific program to change (programname is a key value and must be unique).
                IResultObject programsForPackage = connection.QueryProcessor.ExecuteQuery(query);

                // The query returns a collection that needs to be enumerated.
                foreach (IResultObject program in programsForPackage)
                {
                    // If a match for the program name is found, make the change(s).
                    if (program["ProgramName"].StringValue == existingProgramNameToModify)
                    {
                        // Get all properties, specifically the lazy properties, for the program object.
                        program.Get();

                        // Check whether RUN_ON_SPECIFIED_PLATFORMS is already set.
                        Int32 checkPlatformValue = (program["ProgramFlags"].IntegerValue ^ RUN_ON_SPECIFIED_PLATFORMS);

                        if (checkPlatformValue == 0)
                        {
                            // RUN_ON_SPECIFIED_PLATFORMS is not set. Setting RUN_ON_SPECIFIED_PLATFORMS value.
                            program["ProgramFlags"].IntegerValue = program["ProgramFlags"].IntegerValue ^ RUN_ON_SPECIFIED_PLATFORMS;
                        }

                        // Create a new array list to hold the supported platform window objects.
                        List<IResultObject> tempSupportedPlatformsArray = new List<IResultObject>();

                        // Populate the local array list with the existing supported platform objects (type SMS_OS_Details).
                        tempSupportedPlatformsArray = program.GetArrayItems("SupportedOperatingSystems");

                        // Remove all entries
                        tempSupportedPlatformsArray.Clear();

                        // Replace the array with the blank array
                        program.SetArrayItems("SupportedOperatingSystems", tempSupportedPlatformsArray);

                        // Save the new values in the program settings instance associated with the program ID.
                        program.Put();
                        program.Get();
                        return program;
                    }
                }
                return null;
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject modifySCCMProgram(WqlConnectionManager connection, String existingPackageID, String existingProgramName, String objType, String objName, String objValue)
        {
            String query = "SELECT * FROM SMS_Program WHERE PackageID='" + existingPackageID + "' AND ProgramName='" + existingProgramName + "'";

            // Load the specific program to change (programname is a key value and must be unique).
            IResultObject objToModifyCol = connection.QueryProcessor.ExecuteQuery(query);

            foreach (IResultObject objToModify in objToModifyCol)
            {
                switch (objType)
                {
                    case "StringValue":
                        objToModify[objName].StringValue = Convert.ToString(objValue);
                        objToModify.Put();
                        break;
                    case "DateTimeValue":
                        objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                        objToModify.Put();
                        break;
                    case "IntegerValue":
                        objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                        objToModify.Put();
                        break;
                    case "BooleanValue":
                        objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                        objToModify.Put();
                        break;
                    default:
                        throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
                }
            }

            return objToModifyCol;
        }
        #endregion

        #region Collection Functions
        public static IResultObject getSCCMCollection(WqlConnectionManager connection, String filter)
        {
            return getSCCMObject(connection, "SMS_Collection", filter);
        }
        public static IResultObject getSCCMCollectionMembers(WqlConnectionManager connection, String CollectionID)
        {
            return getSCCMObject(connection, "SMS_FullCollectionMembership", "CollectionID = '" + CollectionID + "'");
        }
        public static IResultObject getSCCMSubCollections(WqlConnectionManager connection, String CollectionID)
        {
            return getSCCMObject(connection, "SMS_CollectToSubCollect", "parentCollectionID = '" + CollectionID + "'");
        }
        public static IResultObject getSCCMParentCollection(WqlConnectionManager connection, String CollectionID)
        {
            return getSCCMObject(connection, "SMS_CollectToSubCollect", "subCollectionID = '" + CollectionID + "'");
        }
        public static IResultObject createSCCMCollection(WqlConnectionManager connection, String Name, String Comment, int refreshMinutes, int refreshHours, int refreshDays, String ParentCollectionID)
        {
            try
            {
                // Create a new SMS_Collection object.
                IResultObject Collection = connection.CreateInstance("SMS_Collection");

                if (ParentCollectionID.Equals(String.Empty)) { ParentCollectionID = "COLLROOT"; }

                // Populate new collection properties.
                Collection["Name"].StringValue = Name;
                Collection["Comment"].StringValue = Comment;
                Collection["OwnedByThisSite"].BooleanValue = true;

                // Save the new collection object and properties.  
                // In this case, it seems necessary to 'get' the object again to access the properties
                Collection.Put();
                Collection.Get();

                // Create new SMS_CollectToSubCollect instance to define collection parent/child relationship.
                IResultObject newSubCollectToSubCollect = connection.CreateInstance("SMS_CollectToSubCollect");

                // Define parent relationship (in this case, off the main collection node). 
                newSubCollectToSubCollect["parentCollectionID"].StringValue = ParentCollectionID;
                newSubCollectToSubCollect["subCollectionID"].StringValue = Collection["CollectionID"].StringValue;

                newSubCollectToSubCollect.Put();

                //Add SMS_ST_RecurInterval
                if (refreshDays > 0 || refreshHours > 0 || refreshMinutes > 0)
                {
                    IResultObject recurInterval = connection.CreateEmbeddedObjectInstance("SMS_ST_RecurInterval");

                    if (refreshDays > 0)
                    {
                        recurInterval["DaySpan"].IntegerValue = refreshDays;
                    }
                    if (refreshHours > 0)
                    {
                        recurInterval["HourSpan"].IntegerValue = refreshHours;
                    }
                    if (refreshMinutes > 0)
                    {
                        recurInterval["MinuteSpan"].IntegerValue = refreshMinutes;
                    }
                    List<IResultObject> refreshSchedules = Collection.GetArrayItems("RefreshSchedule");
                    refreshSchedules.Add(recurInterval);
                    Collection.SetArrayItems("RefreshSchedule", refreshSchedules);
                    Collection["RefreshType"].IntegerValue = 2;
                    Collection.Put();
                    Collection.Get();
                }

                return Collection;
            }
            catch
            {
                throw;
            }
        }
        public static void addDirectSCCMCollectionMemberMachine(WqlConnectionManager connection, String CollectionID, int ResourceID)
        {
            try
            {
                // Get the specific collection instance to remove a population rule from.
                IResultObject collectionToModify = connection.GetInstance(@"SMS_Collection.CollectionID='" + CollectionID + "'");
                collectionToModify.Get();
                // Get the specific UserGroup object
                IResultObject system = connection.GetInstance(@"SMS_R_System.ResourceID='" + ResourceID + "'");

                List<IResultObject> collectionRules = collectionToModify.GetArrayItems("CollectionRules");

                bool found = false;

                foreach (IResultObject collectionRule in collectionRules)
                {
                    if (collectionRule["RuleName"].StringValue.Equals(system["Name"].StringValue))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    IResultObject tempCollectionRule = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect");

                    tempCollectionRule["RuleName"].StringValue = system["Name"].StringValue;
                    tempCollectionRule["ResourceClassName"].StringValue = "SMS_R_System";
                    tempCollectionRule["ResourceID"].IntegerValue = system["ResourceID"].IntegerValue;

                    collectionRules.Add(tempCollectionRule);

                    collectionToModify.SetArrayItems("CollectionRules", collectionRules);
                    collectionToModify.Put();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void addDirectSCCMCollectionMemberUser(WqlConnectionManager connection, String CollectionID, int ResourceID)
        {
            try
            {
                // Get the specific collection instance to remove a population rule from.
                IResultObject collectionToModify = connection.GetInstance(@"SMS_Collection.CollectionID='" + CollectionID + "'");
                collectionToModify.Get();
                // Get the specific UserGroup object
                IResultObject user = connection.GetInstance(@"SMS_R_User.ResourceID='" + ResourceID + "'");

                List<IResultObject> collectionRules = collectionToModify.GetArrayItems("CollectionRules");

                bool found = false;

                foreach (IResultObject collectionRule in collectionRules)
                {
                    if (collectionRule["RuleName"].StringValue.Equals(user["UniqueUserName"].StringValue))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    IResultObject tempCollectionRule = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect");

                    tempCollectionRule["RuleName"].StringValue = user["UniqueUserName"].StringValue;
                    tempCollectionRule["ResourceClassName"].StringValue = "SMS_R_User";
                    tempCollectionRule["ResourceID"].IntegerValue = user["ResourceID"].IntegerValue;

                    collectionRules.Add(tempCollectionRule);

                    collectionToModify.SetArrayItems("CollectionRules", collectionRules);
                    collectionToModify.Put();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void addDirectSCCMCollectionMemberUserGroup(WqlConnectionManager connection, String CollectionID, int ResourceID)
        {
            try
            {
                // Get the specific collection instance to remove a population rule from.
                IResultObject collectionToModify = connection.GetInstance(@"SMS_Collection.CollectionID='" + CollectionID + "'");
                collectionToModify.Get();
                // Get the specific UserGroup object
                IResultObject userGroup = connection.GetInstance(@"SMS_R_UserGroup.ResourceID='" + ResourceID + "'");

                List<IResultObject> collectionRules = collectionToModify.GetArrayItems("CollectionRules");

                bool found = false;

                foreach (IResultObject collectionRule in collectionRules)
                {
                    if (collectionRule["RuleName"].StringValue.Equals(userGroup["UniqueUsergroupName"].StringValue))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    IResultObject tempCollectionRule = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect");

                    tempCollectionRule["RuleName"].StringValue = userGroup["UniqueUsergroupName"].StringValue;
                    tempCollectionRule["ResourceClassName"].StringValue = "SMS_R_UserGroup";
                    tempCollectionRule["ResourceID"].IntegerValue = userGroup["ResourceID"].IntegerValue;

                    collectionRules.Add(tempCollectionRule);

                    collectionToModify.SetArrayItems("CollectionRules", collectionRules);
                    collectionToModify.Put();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void addSCCMCollectionRule(WqlConnectionManager connection, String CollectionID, String RuleName, String WQLQuery, String LimitToCollectionID)
        {
            try
            {
                // Get the specific collection instance to remove a population rule from.
                IResultObject collectionToModify = connection.GetInstance(@"SMS_Collection.CollectionID='" + CollectionID + "'");
                collectionToModify.Get();
                List<IResultObject> collectionRules = collectionToModify.GetArrayItems("CollectionRules");

                bool found = false;

                foreach (IResultObject collectionRule in collectionRules)
                {
                    if (collectionRule["RuleName"].StringValue.Equals(RuleName))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    IResultObject tempCollectionRule = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleQuery");
                    tempCollectionRule["RuleName"].StringValue = RuleName;
                    tempCollectionRule["QueryExpression"].StringValue = WQLQuery;

                    if (!LimitToCollectionID.Equals(String.Empty)) { tempCollectionRule["LimitToCollectionID"].StringValue = LimitToCollectionID; }

                    collectionRules.Add(tempCollectionRule);

                    collectionToModify.SetArrayItems("CollectionRules", collectionRules);
                    collectionToModify.Put();
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void deleteSCCMCollection(WqlConnectionManager connection, String collectionIDToDelete)
        {
            //  Note:  On delete, the provider cleans up the SMS_CollectionSettings and SMS_CollectToSubCollect objects.

            try
            {
                // Get the specific collection instance to delete.
                IResultObject collectionToDelete = connection.GetInstance(@"SMS_Collection.CollectionID='" + collectionIDToDelete + "'");

                // Delete the collection.
                collectionToDelete.Delete();
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void refreshSCCMCollection(WqlConnectionManager connection, String collectionIDToRefresh)
        {
            try
            {
                // Get the specific collection instance to refresh.
                IResultObject collectionToRefresh = connection.GetInstance(@"SMS_Collection.CollectionID='" + collectionIDToRefresh + "'");

                // refresh the collection.
                collectionToRefresh.ExecuteMethod("RequestRefresh", null);
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void removeSCCMCollectionMember(WqlConnectionManager connection, String collectionID, String RuleName)
        {
            try
            {
                // Get the specific collection instance to remove a population rule from.
                IResultObject collectionToModify = connection.GetInstance(@"SMS_Collection.CollectionID='" + collectionID + "'");
                List<IResultObject> collectionRules = collectionToModify.GetArrayItems("CollectionRules");

                foreach (IResultObject collectionRule in collectionRules)
                {
                    if (collectionRule["RuleName"].StringValue.Equals(RuleName))
                    {
                        collectionRules.Remove(collectionRule);

                        collectionToModify.SetArrayItems("CollectionRules", collectionRules);
                        collectionToModify.Put();

                        break;
                    }
                }
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject modifySCCMCollection(WqlConnectionManager connection, String collectionID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_Collection.CollectionID='" + collectionID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static void CreateMaintenanceWindow(WqlConnectionManager connection, string targetCollectionID, string newMaintenanceWindowName, string newMaintenanceWindowDescription, string newMaintenanceWindowServiceWindowSchedules, bool newMaintenanceWindowIsEnabled, int newMaintenanceWindowServiceWindowType)
        {
            try
            {
                // Create an object to hold the collection settings instance (used to check whether a collection settings instance exists). 
                IResultObject collectionSettingsInstance = null;

                // Get the collection settings instance for the targetCollectionID.
                IResultObject allCollectionSettings = connection.QueryProcessor.ExecuteQuery("Select * from SMS_CollectionSettings where CollectionID='" + targetCollectionID + "'");

                // Enumerate the allCollectionSettings collection (there should be just one item) and save the instance.
                foreach (IResultObject collectionSetting in allCollectionSettings)
                {
                    collectionSettingsInstance = collectionSetting;
                }

                // If a collection settings instance doesn't exist for the target collection, create one.
                if (collectionSettingsInstance == null)
                {
                    collectionSettingsInstance = connection.CreateInstance("SMS_CollectionSettings");
                    collectionSettingsInstance["CollectionID"].StringValue = targetCollectionID;
                    collectionSettingsInstance.Put();
                    collectionSettingsInstance.Get();
                }

                // Create a new array list to hold the service window object.
                List<IResultObject> tempServiceWindowArray = new List<IResultObject>();

                // Create and populate a temporary SMS_ServiceWindow object with the new maintenance window values.
                IResultObject tempServiceWindowObject = connection.CreateEmbeddedObjectInstance("SMS_ServiceWindow");

                // Populate temporary SMS_ServiceWindow object with the new maintenance window values.
                tempServiceWindowObject["Name"].StringValue = newMaintenanceWindowName;
                tempServiceWindowObject["Description"].StringValue = newMaintenanceWindowDescription;
                tempServiceWindowObject["ServiceWindowSchedules"].StringValue = newMaintenanceWindowServiceWindowSchedules;
                tempServiceWindowObject["IsEnabled"].BooleanValue = newMaintenanceWindowIsEnabled;
                tempServiceWindowObject["ServiceWindowType"].IntegerValue = newMaintenanceWindowServiceWindowType;

                // Populate the local array list with the existing service window objects (from the target collection).
                tempServiceWindowArray = collectionSettingsInstance.GetArrayItems("ServiceWindows");

                // Add the newly created service window object to the local array list.
                tempServiceWindowArray.Add(tempServiceWindowObject);

                // Replace the existing service window objects from the target collection with the temporary array that includes the new service window.
                collectionSettingsInstance.SetArrayItems("ServiceWindows", tempServiceWindowArray);

                // Save the new values in the collection settings instance associated with the target collection.
                collectionSettingsInstance.Put();
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public void DeleteMaintenanceWindowfromCollection(WqlConnectionManager connection, string targetCollectionID, string serviceWindowID)
        {
            try
            {
                // Create a new array list to hold the service window objects.
                List<IResultObject> tempMaintenanceWindowArray = new List<IResultObject>();

                // Establish connection to collection settings instance associated with the target collection ID.
                IResultObject collectionSettings = connection.GetInstance(@"SMS_CollectionSettings.CollectionID='" + targetCollectionID + "'");

                // Populate the array list with the existing service window objects (from the target collection).
                tempMaintenanceWindowArray = collectionSettings.GetArrayItems("ServiceWindows");

                // Enumerate through the array list to access each maintenance window object.
                foreach (IResultObject maintenanceWindow in tempMaintenanceWindowArray)
                {
                    // If the maintenance window ID matches the one passed in to the function, delete the maintenance window.
                    if (maintenanceWindow["ServiceWindowID"].StringValue == serviceWindowID)
                    {
                        tempMaintenanceWindowArray.Remove(maintenanceWindow);
                        Console.WriteLine("Deleted:");
                        Console.WriteLine("Maintenance Window Name: " + maintenanceWindow["Name"].StringValue);
                        Console.WriteLine("Maintenance Windows Service Window ID: " + maintenanceWindow["ServiceWindowID"].StringValue);
                        break;
                    }
                }

                // Replace the existing service window objects from the target collection with the temporary array that includes the new maintenance window.
                collectionSettings.SetArrayItems("ServiceWindows", tempMaintenanceWindowArray);

                // Save the new values in the collection settings instance associated with the Collection ID.
                collectionSettings.Put();
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static List<IResultObject> GetMaintenanceWindowsAndPropertiesForASpecificCollection(WqlConnectionManager connection, string targetCollectionID)
        {
            try
            {
                // Create an object to hold the collection settings instance (used to check whether a collection settings instance exists). 
                IResultObject collectionSettingsInstance = null;

                // Get the collection settings instance for the targetCollectionID.
                IResultObject allCollectionSettings = connection.QueryProcessor.ExecuteQuery("Select * from SMS_CollectionSettings where CollectionID='" + targetCollectionID + "'");

                // Enumerate the allCollectionSettings collection (there should be just one item) and save the instance.
                foreach (IResultObject collectionSetting in allCollectionSettings)
                {
                    collectionSettingsInstance = collectionSetting;
                }

                // If a collection settings instance, output message that there are no maintenance windows.
                if (collectionSettingsInstance == null)
                {
                    throw new Exception("There are no maintenance windows for collection: " + targetCollectionID);
                }
                else
                {
                    // Create a new array list to hold the service window objects.
                    List<IResultObject> maintenanceWindowArray = new List<IResultObject>();

                    // Establish connection to collection settings instance associated with the Collection ID.
                    IResultObject collectionSettings = connection.GetInstance(@"SMS_CollectionSettings.CollectionID='" + targetCollectionID + "'");

                    // Populate the array list with the existing service window objects (from the target collection).
                    maintenanceWindowArray = collectionSettings.GetArrayItems("ServiceWindows");

                    return maintenanceWindowArray;
                }
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        #endregion

        #region OS Deployment Functions
        public static IResultObject getSCCMDriver(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_Driver", Filter);
        }
        public static IResultObject modifySCCMDriver(WqlConnectionManager connection, String CI_ID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_Driver.CI_ID='" + CI_ID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject updateSCCMDriverSourcePath(WqlConnectionManager connection, String currentPath, String newPath)
        {
            IResultObject driverCollection = getSCCMDriver(connection, "ContentSourcePath LIKE '" + currentPath + "'");
            foreach (IResultObject driver in driverCollection)
            {
                driver["ContentSourcePath"].StringValue = newPath;
                driver.Put();
            }

            return getSCCMDriver(connection, "ContentSourcePath LIKE '" + newPath + "'");
        }
        public static IResultObject getSCCMDriverPackage(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_DriverPackage", Filter);
        }
        public static IResultObject modifySCCMDriverPackage(WqlConnectionManager connection, String PackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_DriverPackage.PackageID='" + PackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject updateSCCMDriverPackagePath(WqlConnectionManager connection, String currentPath, String newPath)
        {
            IResultObject driverPackageCollection = getSCCMDriverPackage(connection, "PkgSourcePath LIKE '" + currentPath + "'");
            foreach (IResultObject driverPackage in driverPackageCollection)
            {
                driverPackage["PkgSourcePath"].StringValue = newPath;
                driverPackage.Put();
            }

            return getSCCMDriverPackage(connection, "PkgSourcePath LIKE '" + newPath + "'");
        }
        public static IResultObject getSCCMTaskSequencePackage(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_TaskSequencePackage", Filter);
        }
        public static IResultObject modifySCCMTaskSequencePackage(WqlConnectionManager connection, String PackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_TaskSequencePackage.PackageID='" + PackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject getSCCMImagePackage(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_ImagePackage", Filter);
        }
        public static IResultObject modifySCCMImagePackage(WqlConnectionManager connection, String PackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_ImagePackage.PackageID='" + PackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject getSCCMOperatingSystemInstallPackage(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_OperatingSystemInstallPackage", Filter);
        }
        public static IResultObject modifySCCMOperatingSystemInstallPackage(WqlConnectionManager connection, String PackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_OperatingSystemInstallPackage.PackageID='" + PackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject getSCCMBootImagePackage(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_BootImagePackage", Filter);
        }
        public static IResultObject modifySCCMBootImagePackage(WqlConnectionManager connection, String PackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_BootImagePackage.PackageID='" + PackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static void removeSCCMObject(WqlConnectionManager connection, String filter, String objClass)
        {
            IResultObject objCol = getSCCMObject(connection, objClass, filter);
            foreach (IResultObject objToBeDeleted in objCol)
            {
                try
                {
                    objToBeDeleted.Delete();
                }
                catch { throw; }
            }
        }

        /**
         * add a new computer (SMS_R_System) via the given connection with the given NetBiosName
         * either the smBiosGuid or the macAddress or both must be set
         * the smBiosGuid / macAddress should be unique otherwise no new computer will be added
         * 
         * see http://msdn.microsoft.com/en-us/library/cc143628.aspx for the original Microsoft code which 
         * is used "as is" in this function
         * 
         * @param connection - the WqlConnectionManager to use for accessing SMS
         * @param netBiosName - the string to be used as the computer's name
         * @param smBiosGuid - a string representing the smBIOSGuid (may be null if macAddress is given)
         * You can obtain the smBiosGuid: of a system with the following commands:
         * <code>wmic
         * wmic:root\cli>csproduct get uuid
         * </code>
         * an example result would be
         * <code>UUID
         * 73794D56-552C-15A3-0268-DBED4A94509B</code>
         * 
         * for more details on the smBiosGuid issue see eg.
         * http://myitforum.com/myitforumwp/2011/10/21/osdtask-sequencepxe-duplicate-smbios-guids-system-uuids-in-sccm-2007/
         *
         * @param macAddress - the MAC Address to be used to uniquely identify the system - preferably this should be the MAC Address of
         * the Network Interface Card the system is going to use for initial boot (e.g. via PXE)
         * @return the resourceID which has been assigned to the new computer - if a non unique input is given the resourceID of an 
         * existing computer is returned instead
         */
        public static int addNewComputer(WqlConnectionManager connection, String netBiosName, String smBiosGuid, String macAddress)
        {
            try
            {
                if (smBiosGuid == null && macAddress == null)
                {
                    throw new ArgumentNullException("smBiosGuid or macAddress must be defined");
                }

                // Reformat macAddress to : separator.
                if (String.IsNullOrEmpty(macAddress) == false)
                {
                    macAddress = macAddress.Replace("-", ":");
                }

                // Create the computer.
                Dictionary<String, object> inParams = new Dictionary<String, object>();
                inParams.Add("NetbiosName", netBiosName);
                inParams.Add("SMBIOSGUID", smBiosGuid);
                inParams.Add("MACAddress", macAddress);
                inParams.Add("OverwriteExistingRecord", false);

                IResultObject outParams = connection.ExecuteMethod(
                    "SMS_Site",
                    "ImportMachineEntry",
                    inParams);

                // Add to All System collection.
                IResultObject collection = connection.GetInstance("SMS_Collection.collectionId='SMS00001'");
                IResultObject collectionRule = connection.CreateEmbeddedObjectInstance("SMS_CollectionRuleDirect");
                collectionRule["ResourceClassName"].StringValue = "SMS_R_System";
                collectionRule["ResourceID"].IntegerValue = outParams["ResourceID"].IntegerValue;

                Dictionary<String, object> inParams2 = new Dictionary<String, object>();
                inParams2.Add("collectionRule", collectionRule);

                collection.ExecuteMethod("AddMembershipRule", inParams2);

                return outParams["ResourceID"].IntegerValue;
            }
            catch (SmsException e)
            {
                throw;
            }

        }
        public static void associateComputer(WqlConnectionManager connection, int referenceComputerResourceId, int destinationComputerResourceId)
        {
            try
            {
                // Set up the reference and destination computer in parameters.
                Dictionary<String, object> inParams = new Dictionary<String, object>();
                inParams.Add("SourceClientResourceID", referenceComputerResourceId);
                inParams.Add("RestoreClientResourceID", destinationComputerResourceId);

                // Create the computer association.
                connection.ExecuteMethod("SMS_StateMigration", "AddAssociation", inParams);
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static void createSCCMCollectionVariable(WqlConnectionManager connection, String name, String value, bool mask, String collectionId, int precedence)
        {
            try
            {
                IResultObject collectionSettings = null;

                // Get the collection settings. Create it if necessary.

                IResultObject collectionSettingsQuery = connection.QueryProcessor.ExecuteQuery(
                           "Select * from SMS_CollectionSettings where CollectionID='" + collectionId + "'");

                foreach (IResultObject setting in collectionSettingsQuery)
                {
                    setting.Get();
                    collectionSettings = setting;
                }

                if (collectionSettings == null)
                {
                    collectionSettings = connection.CreateInstance("SMS_CollectionSettings");
                    collectionSettings["CollectionID"].StringValue = collectionId;
                    collectionSettings.Put();
                    collectionSettings.Get();
                }

                // Create the collection variable.
                List<IResultObject> collectionVariables = collectionSettings.GetArrayItems("CollectionVariables");
                IResultObject collectionVariable = connection.CreateEmbeddedObjectInstance("SMS_CollectionVariable");
                collectionVariable["Name"].StringValue = name;
                collectionVariable["Value"].StringValue = value;
                collectionVariable["IsMasked"].BooleanValue = mask;

                // Add the collection variable to the collection settings.
                collectionVariables.Add(collectionVariable);
                collectionSettings.SetArrayItems("CollectionVariables", collectionVariables);

                // Set the collection variable precedence.
                collectionSettings["CollectionVariablePrecedence"].IntegerValue = precedence;

                collectionSettings.Put();
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static void createSCCMComputerVariable(WqlConnectionManager connection, String siteCode, String name, String value, bool mask, int computerId)
        {
            try
            {
                // Get the computer settings.
                IResultObject computerSettings = null;

                IResultObject computerSettingsQuery = connection.QueryProcessor.ExecuteQuery(
                    "Select * from SMS_MachineSettings where ResourceId = '" + computerId + "'");

                foreach (IResultObject settings in computerSettingsQuery)
                {
                    settings.Get();
                    computerSettings = settings;
                }

                if (computerSettings == null) // It doesn't exist, so create it.
                {
                    computerSettings = connection.CreateInstance(@"SMS_MachineSettings");
                    computerSettings["ResourceID"].IntegerValue = computerId;
                    computerSettings["SourceSite"].StringValue = siteCode;
                    computerSettings["LocaleID"].IntegerValue = 1033;
                    computerSettings.Put();
                    computerSettings.Get();
                }

                // Create the computer variable.
                List<IResultObject> computerVariables = computerSettings.GetArrayItems("MachineVariables");
                IResultObject computerVariable = connection.CreateEmbeddedObjectInstance("SMS_MachineVariable");
                computerVariable["Name"].StringValue = name;
                computerVariable["Value"].StringValue = value;
                computerVariable["IsMasked"].BooleanValue = mask;

                // Add the computer variable to the computer settings.
                computerVariables.Add(computerVariable);
                computerSettings.SetArrayItems("MachineVariables", computerVariables);

                computerSettings.Put();
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static void deleteSCCMCollectionVariable(WqlConnectionManager connection, String name, String collectionId)
        {
            try
            {
                IResultObject collectionSettings = null;

                // Get the collection settings. Create it if necessary.

                IResultObject collectionSettingsQuery = connection.QueryProcessor.ExecuteQuery(
                           "Select * from SMS_CollectionSettings where CollectionID='" + collectionId + "'");

                foreach (IResultObject setting in collectionSettingsQuery)
                {
                    setting.Get();
                    collectionSettings = setting;
                }

                if (collectionSettings == null)
                {
                    collectionSettings = connection.CreateInstance("SMS_CollectionSettings");
                    collectionSettings["CollectionID"].StringValue = collectionId;
                    collectionSettings.Put();
                    collectionSettings.Get();
                }

                // Get the current collection variables.
                List<IResultObject> collectionVariables = collectionSettings.GetArrayItems("CollectionVariables");
                List<IResultObject> filteredVariableList = new List<IResultObject>();

                foreach (IResultObject variable in collectionVariables)
                {
                    if (variable.PropertyList.ContainsKey("Name"))
                    {
                        if (!variable["Name"].StringValue.Equals(name))
                        {
                            filteredVariableList.Add(variable);
                        }
                    }
                }

                // Set the collection variable collection to the filtered settings list.
                collectionSettings.SetArrayItems("CollectionVariables", filteredVariableList);

                collectionSettings.Put();
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static void deleteSCCMComputerVariable(WqlConnectionManager connection, String name, String computerID)
        {
            try
            {
                IResultObject collectionSettings = null;

                // Get the collection settings. Create it if necessary.

                IResultObject collectionSettingsQuery = connection.QueryProcessor.ExecuteQuery(
                            "Select * from SMS_MachineSettings where ResourceId LIKE '" + computerID + "'");

                foreach (IResultObject setting in collectionSettingsQuery)
                {
                    setting.Get();
                    collectionSettings = setting;
                }

                if (collectionSettings == null)
                {
                    collectionSettings = connection.CreateInstance("MachineVariables");
                    collectionSettings["CollectionID"].StringValue = computerID;
                    collectionSettings.Put();
                    collectionSettings.Get();
                }

                // Get the current machine variables.
                List<IResultObject> collectionVariables = collectionSettings.GetArrayItems("MachineVariables");
                List<IResultObject> filteredVariableList = new List<IResultObject>();

                foreach (IResultObject variable in collectionVariables)
                {
                    if (variable.PropertyList.ContainsKey("Name"))
                    {
                        if (!variable["Name"].StringValue.Equals(name))
                        {
                            filteredVariableList.Add(variable);
                        }
                    }
                }

                // Set the collection variable collection to the filtered settings list.
                collectionSettings.SetArrayItems("MachineVariables", filteredVariableList);

                collectionSettings.Put();
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static List<IResultObject> getSCCMCollectionVariables(WqlConnectionManager connection, String collectionId)
        {
            try
            {
                IResultObject collectionSettings = null;

                // Get the collection settings. Create it if necessary.

                IResultObject collectionSettingsQuery = connection.QueryProcessor.ExecuteQuery(
                           "Select * from SMS_CollectionSettings where CollectionID LIKE '" + collectionId + "'");

                foreach (IResultObject setting in collectionSettingsQuery)
                {
                    collectionSettings = setting;
                }

                if (collectionSettings == null)
                {
                    return null;
                }
                else
                {
                    collectionSettings.Get();
                    return collectionSettings.GetArrayItems("CollectionVariables");
                }
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static List<IResultObject> getSCCMComputerVariables(WqlConnectionManager connection, String computerID)
        {
            try
            {
                IResultObject computerSettings = null;

                // Get the collection settings. Create it if necessary.

                IResultObject computerSettingsQuery = connection.QueryProcessor.ExecuteQuery(
                           "Select * from SMS_MachineSettings where ResourceId LIKE '" + computerID + "'");

                foreach (IResultObject setting in computerSettingsQuery)
                {
                    setting.Get();
                    computerSettings = setting;
                }

                if (computerSettings == null)
                {
                    return null;
                }
                else
                {
                    return computerSettings.GetArrayItems("MachineVariables");
                }
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        #endregion

        #region Software Update Functions
        public static IResultObject createSCCMSoftwareUpdatesPackage(WqlConnectionManager connection, String newPackageName, String newPackageDescription, int newPackageSourceFlag, String newPackageSourcePath)
        {
            IResultObject retValue = null;
            try
            {
                // Create the new SUM package object.
                IResultObject newSUMDeploymentPackage = connection.CreateInstance("SMS_SoftwareUpdatesPackage");

                // Populate the new SUM package properties.
                newSUMDeploymentPackage["Name"].StringValue = newPackageName;
                newSUMDeploymentPackage["Description"].StringValue = newPackageDescription;
                newSUMDeploymentPackage["PkgSourceFlag"].IntegerValue = newPackageSourceFlag;
                newSUMDeploymentPackage["PkgSourcePath"].StringValue = newPackageSourcePath;

                // Save the new SUM package and new package properties.
                newSUMDeploymentPackage.Put();
                newSUMDeploymentPackage.Get();

                retValue = newSUMDeploymentPackage;
            }

            catch (SmsException ex)
            {
                throw;
            }
            return retValue;
        }
        public static IResultObject getSCCMSoftwareUpdatesPackage(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_SoftwareUpdatesPackage", Filter);
        }
        public static IResultObject modifySCCMSoftwareUpdatesPackage(WqlConnectionManager connection, String PackageID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_BootImagePackage.PackageID='" + PackageID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject createSCCMAuthorizationList(WqlConnectionManager connection, String displayName, String description, String informativeURL, String localeID, String newUpdates)
        {
            IResultObject retValue = null;
            try
            {
                List<IResultObject> newDescriptionInfo = new List<IResultObject>();
                IResultObject SMSCILocalizedProperties = connection.CreateEmbeddedObjectInstance("SMS_CI_LocalizedProperties");

                //Populate the initial array values (this could be a loop to added more localized info).

                SMSCILocalizedProperties["Description"].StringValue = description;
                SMSCILocalizedProperties["DisplayName"].StringValue = displayName;
                SMSCILocalizedProperties["InformativeURL"].StringValue = informativeURL;
                SMSCILocalizedProperties["LocaleID"].StringValue = localeID;

                //Add the 'embedded properties' to newDescriptionInfo.

                newDescriptionInfo.Add(SMSCILocalizedProperties);

                //Create the array of CI_IDs.

                String[] temp = newUpdates.Split(',');
                int[] newCI_ID = new int[temp.Length];

                for (int i = 0; i < temp.Length; i++)
                {
                    newCI_ID[i] = Convert.ToInt32(temp[i]);
                }

                // Create the new SMS_AuthorizationList object.
                IResultObject newUpdateList = connection.CreateInstance("SMS_AuthorizationList");

                // Populate the new SMS_AuthorizationList object properties.
                // Updates is an int32 array that maps to the CI_ID in SMS_SoftwareUpdate.
                newUpdateList["Updates"].IntegerArrayValue = newCI_ID;
                // Pass embedded properties (LocalizedInformation) here.
                newUpdateList.SetArrayItems("LocalizedInformation", newDescriptionInfo);

                // Save changes.
                newUpdateList.Put();
                newUpdateList.Get();

                retValue = newUpdateList;
            }

            catch (SmsException ex)
            {
                throw;
            }
            return retValue;
        }
        public static IResultObject getSCCMAuthorizationList(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_AuthorizationList", Filter);
        }
        public static IResultObject modifySCCMAuthorizationList(WqlConnectionManager connection, String CI_ID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_AuthorizationList.CI_ID='" + CI_ID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static void modifySCCMSoftwareUpdatesPackageAddCIs(WqlConnectionManager connection, string existingSoftwareUpdatesPackageID, string contentIDList, string contentSourcePathList, bool refreshDPs)
        {
            try
            {
                string[] tempArrayContentIds = contentIDList.Split(',');
                int[] newArrayContentIds = new int[tempArrayContentIds.Length];

                for (int i = 0; i < tempArrayContentIds.Length; i++)
                {
                    newArrayContentIds[i] = Convert.ToInt32(tempArrayContentIds[i]);
                }

                string[] newArrayContentSourcePath = contentSourcePathList.Split(',');

                //Load the update content parameters into an object to pass to the method.
                Dictionary<string, object> addUpdateContentParameters = new Dictionary<string, object>();
                addUpdateContentParameters.Add("ContentIds", newArrayContentIds);
                addUpdateContentParameters.Add("ContentSourcePath", newArrayContentSourcePath);
                addUpdateContentParameters.Add("bRefreshDPs", refreshDPs);

                // Get the specific SUM Deployment Package to change.
                IResultObject existingSUMDeploymentPackage = connection.GetInstance(@"SMS_SoftwareUpdatesPackage.PackageID='" + existingSoftwareUpdatesPackageID + "'");

                // Add updates to the existing SUM Deployment Package using the AddUpdateContent method.
                // Note: The method will throw an exception, if the method is not able to add the content.
                existingSUMDeploymentPackage.ExecuteMethod("AddUpdateContent", addUpdateContentParameters);
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void modifySCCMSoftwareUpdatesPackageRemoveCIs(WqlConnectionManager connection, string existingSoftwareUpdatesPackageID, string contentIDList, bool refreshDPs)
        {
            try
            {
                string[] tempArrayContentIds = contentIDList.Split(',');
                int[] newArrayContentIds = new int[tempArrayContentIds.Length];

                for (int i = 0; i < tempArrayContentIds.Length; i++)
                {
                    newArrayContentIds[i] = Convert.ToInt32(tempArrayContentIds[i]);
                }

                //Load the update content parameters into an object to pass to the method.
                Dictionary<string, object> removeContentParameters = new Dictionary<string, object>();
                removeContentParameters.Add("ContentIds", newArrayContentIds);
                removeContentParameters.Add("bRefreshDPs", refreshDPs);

                // Get the specific SUM Deployment Package to change.
                IResultObject existingSUMDeploymentPackage = connection.GetInstance(@"SMS_SoftwareUpdatesPackage.PackageID='" + existingSoftwareUpdatesPackageID + "'");

                // Remove updates from the existing SUM Deployment Package using the RemoveContent method.
                // Note: The method will throw an exception, if the method is not able to add the content.
                IResultObject result = existingSUMDeploymentPackage.ExecuteMethod("RemoveContent", removeContentParameters);
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject createSCCMUpdatesAssignment(WqlConnectionManager connection, bool newApplyToSubTargets, String ArrayAssignedCIs, int newAssignmentAction, string newAssignmentDescription, string newAssignmentName, int newDesiredConfigType, int newDPLocality, int newLocaleID, bool newLogComplianceToWinEvent, bool newNotifyUser, bool newRaiseMomAlertsOnFailure, bool newReadOnly, bool newSendDetailedNonComplianceStatus, DateTime newStartTime, int newSuppressReboot, string newTargetCollectionID, bool newUseGMTTimes)
        {
            try
            {
                // Create the deployment object.
                IResultObject newSUMUpdatesAssignment = connection.CreateInstance("SMS_UpdatesAssignment");

                String[] tArray = ArrayAssignedCIs.Split(',');
                int[] newArrayAssignedCIs = new int[tArray.Length];

                for (int i = 0; i < tArray.Length; i++)
                {
                    newArrayAssignedCIs[i] = Convert.ToInt32(tArray[i]);
                }

                // Populate new deployment properties.
                // Note: newTemplateName must be unique.

                newSUMUpdatesAssignment["ApplyToSubTargets"].BooleanValue = newApplyToSubTargets;
                newSUMUpdatesAssignment["AssignedCIs"].IntegerArrayValue = newArrayAssignedCIs;
                newSUMUpdatesAssignment["AssignmentAction"].IntegerValue = newAssignmentAction;
                newSUMUpdatesAssignment["AssignmentDescription"].StringValue = newAssignmentDescription;
                newSUMUpdatesAssignment["AssignmentName"].StringValue = newAssignmentName;
                newSUMUpdatesAssignment["DesiredConfigType"].IntegerValue = newDesiredConfigType;
                newSUMUpdatesAssignment["DPLocality"].IntegerValue = newDPLocality;
                newSUMUpdatesAssignment["LocaleID"].IntegerValue = newLocaleID;
                newSUMUpdatesAssignment["LogComplianceToWinEvent"].BooleanValue = newLogComplianceToWinEvent;
                newSUMUpdatesAssignment["NotifyUser"].BooleanValue = newNotifyUser;
                newSUMUpdatesAssignment["RaiseMomAlertsOnFailure"].BooleanValue = newRaiseMomAlertsOnFailure;
                newSUMUpdatesAssignment["ReadOnly"].BooleanValue = newReadOnly;
                newSUMUpdatesAssignment["NotifyUser"].BooleanValue = newNotifyUser;
                newSUMUpdatesAssignment["SendDetailedNonComplianceStatus"].BooleanValue = newSendDetailedNonComplianceStatus;
                newSUMUpdatesAssignment["StartTime"].DateTimeValue = newStartTime;
                newSUMUpdatesAssignment["SuppressReboot"].IntegerValue = newSuppressReboot;
                newSUMUpdatesAssignment["TargetCollectionID"].StringValue = newTargetCollectionID;
                newSUMUpdatesAssignment["UseGMTTimes"].BooleanValue = newUseGMTTimes;

                // Save new deployment and new deployment properties.
                newSUMUpdatesAssignment.Put();
                newSUMUpdatesAssignment.Get();

                return newSUMUpdatesAssignment;
            }

            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject getSCCMUpdatesAssignment(WqlConnectionManager connection, String Filter)
        {
            return getSCCMObject(connection, "SMS_UpdatesAssignment", Filter);
        }
        public static IResultObject modifySCCMUpdatesAssignment(WqlConnectionManager connection, String AssignmentID, String objType, String objName, String objValue)
        {
            IResultObject objToModify = connection.GetInstance(@"SMS_UpdatesAssignment.AssignmentID='" + AssignmentID + "'");

            switch (objType)
            {
                case "StringValue":
                    objToModify[objName].StringValue = Convert.ToString(objValue);
                    objToModify.Put();
                    break;
                case "DateTimeValue":
                    objToModify[objName].DateTimeValue = Convert.ToDateTime(objValue);
                    objToModify.Put();
                    break;
                case "IntegerValue":
                    objToModify[objName].IntegerValue = Convert.ToInt32(objValue);
                    objToModify.Put();
                    break;
                case "BooleanValue":
                    objToModify[objName].BooleanValue = Convert.ToBoolean(objValue);
                    objToModify.Put();
                    break;
                default:
                    throw new Exception("Invalid Object Type " + objType + ".  Must be StringValue, DateTimeValue, IntegerValue or BooleanValue");
            }

            return objToModify;
        }
        public static IResultObject modifySCCMUpdatesAssignmentAddCIs(WqlConnectionManager connection, string AssignmentID, string IDListToAdd)
        {
            try
            {
                // Get the specific Updates Assignment Package to change.
                IResultObject ua = connection.GetInstance(@"SMS_UpdatesAssignment.AssignmentID='" + AssignmentID + "'");
                ua.Get();

                string[] tempArrayContentIds = IDListToAdd.Split(',');
                int[] assignedCIs = ua["AssignedCIs"].IntegerArrayValue;

                ArrayList mergedList = new ArrayList();

                //Add Current Contents of UA to Merge List
                foreach (int i in assignedCIs)
                {
                    if (!mergedList.Contains(i))
                    {
                        mergedList.Add(i);
                    }
                }

                //Add New Entries to Merge List
                foreach (String i in tempArrayContentIds)
                {
                    if (!mergedList.Contains(Convert.ToInt32(i)))
                    {
                        mergedList.Add(Convert.ToInt32(i));
                    }
                }

                //Convert Merged List to int[]
                int[] finalCIArray = new int[mergedList.Count];
                for (int i = 0; i < mergedList.Count; i++)
                {
                    finalCIArray[i] = Convert.ToInt32(mergedList[i]);
                }

                ua["AssignedCIs"].IntegerArrayValue = finalCIArray;

                ua.Put();
                ua.Get();

                return ua;
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static IResultObject modifySCCMUpdatesAssignmentRemoveCIs(WqlConnectionManager connection, string AssignmentID, string IDListToRemove)
        {
            try
            {
                // Get the specific Updates Assignment Package to change.
                IResultObject ua = connection.GetInstance(@"SMS_UpdatesAssignment.AssignmentID='" + AssignmentID + "'");
                ua.Get();

                string[] tempArrayContentIds = IDListToRemove.Split(',');
                int[] assignedCIs = ua["AssignedCIs"].IntegerArrayValue;

                ArrayList mergedList = new ArrayList();

                //Add Current Contents of UA to Merge List
                foreach (int i in assignedCIs)
                {
                    if (!mergedList.Contains(i))
                    {
                        mergedList.Add(i);
                    }
                }

                //Add New Entries to Merge List
                foreach (String i in tempArrayContentIds)
                {
                    if (mergedList.Contains(Convert.ToInt32(i)))
                    {
                        mergedList.Remove(Convert.ToInt32(i));
                    }
                }

                //Convert Merged List to int[]
                int[] finalCIArray = new int[mergedList.Count];
                for (int i = 0; i < mergedList.Count; i++)
                {
                    finalCIArray[i] = Convert.ToInt32(mergedList[i]);
                }

                ua["AssignedCIs"].IntegerArrayValue = finalCIArray;

                ua.Put();
                ua.Get();

                return ua;
            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        public static void synchronizeSCCMSoftwareUpdatePoint(WqlConnectionManager connection, String siteCode, String SUPServerName)
        {
            // Calculate the current timestamp (number of seconds from 1/1/1970 to current time UTC).
            DateTime baseTimeValue = new DateTime(1970, 1, 1);
            DateTime currentDateTime = DateTime.UtcNow;
            TimeSpan calculatedTimeStamp = currentDateTime.Subtract(baseTimeValue);
            int currentTimestamp = Convert.ToInt32(calculatedTimeStamp.TotalSeconds);

            try
            {
                // Connect to SMS_WSUS_SYNC_MANAGER section of the site control file.
                IResultObject siteDefinition = connection.GetInstance(@"SMS_SCI_Component.FileType=2,ItemType='Component',SiteCode='" + siteCode + "',ItemName='SMS_WSUS_SYNC_MANAGER|" + SUPServerName + "'");
                foreach (KeyValuePair<string, IResultObject> kvp in siteDefinition.EmbeddedProperties)
                {
                    // Temp copy of the embedded properties.
                    Dictionary<string, IResultObject> embeddedProperties = siteDefinition.EmbeddedProperties;

                    // Setting: Sync Now.
                    if (kvp.Value.PropertyList["PropertyName"] == "Sync Now")
                    {
                        // Change Sync Now value to current timestamp which will initiate WSUS sync.
                        embeddedProperties["Sync Now"]["Value"].StringValue = currentTimestamp.ToString();
                    }

                    // Store the settings that have changed.
                    siteDefinition.EmbeddedProperties = embeddedProperties;
                }

                // Save the settings.
                siteDefinition.Put();

            }
            catch (SmsException ex)
            {
                throw;
            }
        }
        #endregion

        #region Console Functions
        public enum folderType : int
        {
            SMS_Package = 2,
            SMS_Advertisement = 3,
            SMS_Query = 7,
            SMS_Report = 8,
            SMS_MeteredProductRule = 9,
            SMS_ConfigurationItem = 11,
            SMS_OperatingSystemInstallPackage = 14,
            SMS_StateMigration = 17,
            SMS_ImagePackage = 18,
            SMS_BootImagePackage = 19,
            SMS_TaskSequencePackage = 20,
            SMS_DeviceSettingPackage = 21,
            SMS_DriverPackage = 23,
            SMS_Driver = 25,
            SMS_SoftwareUpdate = 1011
        }
        public static IResultObject createConsoleFolder(WqlConnectionManager connection, string name, folderType objectType, Int32 parentNodeID)
        {
            try
            {
                IResultObject folder = connection.CreateInstance("SMS_ObjectContainerNode");
                folder["Name"].StringValue = name;
                folder["ObjectType"].IntegerValue = (int)objectType;
                folder["ParentContainerNodeID"].IntegerValue = parentNodeID;

                folder.Put();

                return folder;
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static IResultObject createSearchFolder(WqlConnectionManager connection, string name, folderType objectType, Int32 parentNodeID, string searchXML)
        {
            try
            {
                IResultObject folder = connection.CreateInstance("SMS_ObjectContainerNode");

                folder["Name"].StringValue = name;
                folder["ObjectType"].IntegerValue = (int)objectType;
                folder["ParentContainerNodeID"].IntegerValue = parentNodeID;
                folder["SearchFolder"].BooleanValue = true;
                folder["SearchString"].StringValue = searchXML;
                folder["FolderFlags"].IntegerValue = 1;

                folder.Put();
                folder.Get();
                return folder;
            }

            catch (SmsException eX)
            {
                throw;
            }

        }
        public static void moveConsoleFolder(WqlConnectionManager connection, Int32 sourceContainerID, Int32 destinationContainerID)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                Int32[] sourceFolders = new Int32[1];//we're only moving one folder hence an array size of 1.

                sourceFolders[0] = sourceContainerID;
                parameters.Add("ContainerNodeIDs", sourceFolders);
                parameters.Add("TargetContainerNodeID", destinationContainerID);
                connection.ExecuteMethod("SMS_ObjectContainerNode", "MoveFolders", parameters);
            }
            catch (SmsException e)
            {
                Console.WriteLine("Failed to move folder. Error: " + e.Message);
                throw;
            }
        }
        public static void deleteConsoleFolder(WqlConnectionManager connection, Int32 nodeID)
        {
            try
            {
                IResultObject folder = connection.GetInstance(@"SMS_ObjectContainerNode.ContainerNodeID=" + nodeID + "");
                folder.Delete();
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        public static void moveConsoleFolderItem(WqlConnectionManager connection, string itemObjectID, folderType objectType, Int32 sourceContainerID, Int32 destinationContainerID)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string[] sourceItems = new string[1];
                // Only one item is being moved, the array size is 1.

                sourceItems[0] = itemObjectID;
                parameters.Add("InstanceKeys", sourceItems);
                parameters.Add("ContainerNodeID", sourceContainerID);
                parameters.Add("TargetContainerNodeID", destinationContainerID);
                parameters.Add("ObjectType", (int)objectType);
                connection.ExecuteMethod("SMS_ObjectContainerItem", "MoveMembers", parameters);
            }
            catch (SmsException e)
            {
                throw;
            }
        }
        #endregion
    }
}
