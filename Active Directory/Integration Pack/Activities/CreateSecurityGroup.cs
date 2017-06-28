using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Principal;
using System.Collections;
using ActiveDs;

namespace Active_Directory
{
    [Activity("Create Security Group",ShowFilters=false)]
    class CreateSecurityGroup : IActivity
    {
        private ConnectionCredentials credentials;

        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Group Container OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=ContainerOU,DC=Contoso,DC=Com");
            designer.AddInput("Group Name").WithDefaultValue("GroupName");
            designer.AddInput("Managed By User LDAP Path").NotRequired();
            designer.AddInput("Set Manager Can Update Membership List").WithDefaultValue("True").NotRequired().WithListBrowser(new string[] { "True", "False" });
            designer.AddOutput("Group LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string containingOU = CapitalizeLDAPPath(request.Inputs["Group Container OU LDAP Path"].AsString());
            string groupName = request.Inputs["Group Name"].AsString();

            string managerLDAPPath = string.Empty;
            bool managerUpdateMembershipList = false;

            if (request.Inputs.Contains("Managed By User LDAP Path")) { managerLDAPPath = request.Inputs["Managed By User LDAP Path"].AsString(); }
            if (request.Inputs.Contains("Set Manager Can Update Membership List")) { managerUpdateMembershipList = Convert.ToBoolean(request.Inputs["Set Manager Can Update Membership List"].AsString()); }

            try
            {
                

                if (managerLDAPPath.Equals(string.Empty))
                {
                    DirectoryEntry group = CreateGroup(containingOU, groupName);
                    response.Publish("Group DN", group.Path);
                    group.Close();
                }
                else
                {
                    DirectoryEntry group = CreateGroup(containingOU, groupName);

                    setManagedBy(managerLDAPPath, managerUpdateMembershipList, group);
                    
                    response.Publish("Group DN", group.Path);
                    group.Close();
                }

                
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                throw E;
            }
        }

        private void setManagedBy(string managerLDAPPath, bool managerUpdateMembershipList, DirectoryEntry group)
        {
            DirectoryEntry managedBy = new DirectoryEntry(managerLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            string managedBymanagerDistinguishedName = managedBy.Properties["distinguishedName"].Value.ToString();
            string userPrincipalName = managedBy.Properties["userPrincipalName"].Value.ToString();
            string managedBysAMAccountName = userPrincipalName.Split('@')[0];
            string managedByDomainName = userPrincipalName.Split('@')[1].Replace(".com", "");

            setSinglePropertyValue(group, "managedBy", managedBymanagerDistinguishedName);

            if (managerUpdateMembershipList)
            {
                IADsSecurityDescriptor sd = (IADsSecurityDescriptor)group.Properties["ntSecurityDescriptor"].Value;
                IADsAccessControlList dacl = (IADsAccessControlList)sd.DiscretionaryAcl;

                IADsAccessControlEntry ace = new AccessControlEntry();

                ace.Trustee = string.Format("{0}\\{1}", managedByDomainName, managedBysAMAccountName);
                ace.AccessMask = (int)ADS_RIGHTS_ENUM.ADS_RIGHT_DS_WRITE_PROP;
                ace.AceFlags = (int)ADS_ACEFLAG_ENUM.ADS_ACEFLAG_NO_PROPAGATE_INHERIT_ACE;
                ace.AceType = (int)ADS_ACETYPE_ENUM.ADS_ACETYPE_ACCESS_ALLOWED_OBJECT;
                ace.Flags = (int)ADS_FLAGTYPE_ENUM.ADS_FLAG_OBJECT_TYPE_PRESENT;
                ace.ObjectType = "{BF9679C0-0DE6-11D0-A285-00AA003049E2}";

                dacl.AddAce(ace);

                sd.DiscretionaryAcl = dacl;

                ((IADsGroup)group.NativeObject).Put("ntSecurityDescriptor", sd);
                ((IADsGroup)group.NativeObject).SetInfo();
            }
        }

        private DirectoryEntry CreateGroup(string containingOU, string groupName)
        {
            DirectoryEntry entry = new DirectoryEntry(containingOU, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry group = entry.Children.Add("CN=" + groupName, "group");
            group.Properties["sAmAccountName"].Value = groupName;
            group.CommitChanges();
            return group;
        }

        private string CapitalizeLDAPPath(string value)
        {
            if (value == null)
                throw new Exception("Must enter valid LDAP Path");
            if (!value.StartsWith("LDAP", true, System.Globalization.CultureInfo.CurrentCulture))
                throw new Exception("Must enter valid LDAP Path");

            StringBuilder result = new StringBuilder(value);
            for (int i = 0; i < 4; i++)
            {
                result[i] = char.ToUpper(result[i]);
            }
            return result.ToString();
        }
        private static IADsLargeInteger GetLargeIntegerFromDateTime(DateTime dateTimeValue)
        {
            //
            // Convert DateTime value to utc file time
            //
            Int64 int64Value = dateTimeValue.ToFileTimeUtc();

            //
            // convert to large integer
            //
            IADsLargeInteger largeIntValue = (IADsLargeInteger)new LargeInteger();
            largeIntValue.HighPart = (int)(int64Value >> 32);
            largeIntValue.LowPart = (int)(int64Value & 0xFFFFFFFF);

            return largeIntValue;
        }

        private static void setSinglePropertyValue(DirectoryEntry objRootDSE, string strAttrName, string propertyValue)
        {
            switch (strAttrName)
            {
                case "msFVE-VolumeGuid":
                    objRootDSE.Properties[strAttrName].Value = new Guid(propertyValue).ToByteArray();
                    break;
                case "msFVE-RecoveryGuid":
                    objRootDSE.Properties[strAttrName].Value = new Guid(propertyValue).ToByteArray();
                    break;
                case "msFVE-KeyPackage":
                    String[] arr = propertyValue.Split('-');
                    byte[] array = new byte[arr.Length];
                    for (int i = 0; i < arr.Length; i++) array[i] = Convert.ToByte(arr[i], 16);
                    objRootDSE.Properties[strAttrName].Value = array;
                    break;
                case "uSNCreated":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "uSNChanged":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "objectGUID":
                    objRootDSE.Properties[strAttrName].Value = new Guid(propertyValue);
                    break;
                case "badPasswordTime":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "lastLogoff":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "lastLogon":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "pwdLastSet":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "objectSid":
                    objRootDSE.Properties[strAttrName].Value = new SecurityIdentifier(propertyValue);
                    break;
                case "accountExpires":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "lockoutTime":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "lastLogonTimestamp":
                    objRootDSE.Properties[strAttrName].Value = GetLargeIntegerFromDateTime(Convert.ToDateTime(propertyValue));
                    break;
                case "msExchMailboxSecurityDescriptor":
                    throw new Exception("Setting Security Descriptors is Not Supported");
                case "nTSecurityDescriptor":
                    throw new Exception("Setting Security Descriptors is Not Supported");
                default:
                    objRootDSE.Properties[strAttrName].Value = propertyValue;
                    break;
            }
            objRootDSE.CommitChanges();
            objRootDSE.Close();
        }
    }
}

