using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;
using System.Security.Principal;
using ActiveDs;

namespace Active_Directory
{
    [Activity("Get Object Property Values")]
    class getObjectPropertyValues : IActivity
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
            designer.AddInput(getObjectPropValuesRefStrings.ldapPath).WithDefaultValue("LDAP://Contoso.com/CN=ObjectName,OU=Container,DC=Contoso,DC=Com");
            designer.AddInput(getObjectPropValuesRefStrings.useSecureConnection).WithDefaultValue("False").WithListBrowser(getObjectPropValuesRefStrings.tOrF).NotRequired();
            designer.AddInput(getObjectPropValuesRefStrings.childItemSelection).WithDefaultValue("False").WithListBrowser(getObjectPropValuesRefStrings.tOrF).NotRequired();
            designer.AddCorellatedData(typeof(ObjectValues));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            bool useSecureConnection = false;
            bool includeChildItems = false;
            if (request.Inputs.Contains(getObjectPropValuesRefStrings.useSecureConnection)) { useSecureConnection = Convert.ToBoolean(request.Inputs[getObjectPropValuesRefStrings.useSecureConnection].AsString()); }
            if (request.Inputs.Contains(getObjectPropValuesRefStrings.childItemSelection)) { includeChildItems = Convert.ToBoolean(request.Inputs[getObjectPropValuesRefStrings.childItemSelection].AsString()); }

            ArrayList propertyList = new ArrayList();
            if (request.Filters.Count > 0)
            {
                foreach (IFilterCriteria fCriteria in request.Filters)
                {
                    try
                    {
                        if (fCriteria.Relation.Equals(Relation.EqualTo) && fCriteria.Name.Equals("Property_Name"))
                        {
                            propertyList.Add(fCriteria.Value.AsString());
                        }
                    }
                    catch { }
                }
            }
            string LDAPPath = CapitalizeLDAPPath(request.Inputs[getObjectPropValuesRefStrings.ldapPath].AsString());
            if (propertyList.Count > 0)
            {
                response.WithFiltering().PublishRange(getValues(LDAPPath, propertyList, useSecureConnection, includeChildItems));
            }
            else
            {
                response.WithFiltering().PublishRange(getValues(LDAPPath, useSecureConnection, includeChildItems));
            }
        }

        private IEnumerable<ObjectValues> getValues(string LDAPPath, bool secureConnection, bool includeChildItems)
        {
            DirectoryEntry objRootDSE;
            if (secureConnection) { objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password, AuthenticationTypes.Secure | AuthenticationTypes.Sealing | AuthenticationTypes.Signing); }
            else { objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password); }

            foreach (string strAttrName in objRootDSE.Properties.PropertyNames)
            {
                //Multi-Value Property
                if (objRootDSE.Properties[strAttrName].Count > 1)
                {
                    PropertyValueCollection ValueCollection = objRootDSE.Properties[strAttrName];
                    IEnumerator en = ValueCollection.GetEnumerator();

                    while (en.MoveNext())
                    {
                        if (en.Current != null)
                        {
                            yield return new ObjectValues(LDAPPath, strAttrName, en.Current.ToString());
                        }
                    }
                }
                //Single Value Property
                else
                {
                    string propertyValue = getSinglePropertyValue(objRootDSE, strAttrName);
                    yield return new ObjectValues(LDAPPath, strAttrName, propertyValue);
                }
            }
            if (includeChildItems)
            {
                var children = objRootDSE.Children;
                foreach (DirectoryEntry child in children) using (child)
                    {
                        foreach (string strAttrName in child.Properties.PropertyNames)
                        {
                            //Multi-Value Property
                            if (child.Properties[strAttrName].Count > 1)
                            {
                                PropertyValueCollection ValueCollection = child.Properties[strAttrName];
                                IEnumerator en = ValueCollection.GetEnumerator();

                                while (en.MoveNext())
                                {
                                    if (en.Current != null)
                                    {
                                        yield return new ObjectValues(child.Path, strAttrName, en.Current.ToString());
                                    }
                                }
                            }
                            //Single Value Property
                            else
                            {
                                string propertyValue = getSinglePropertyValue(child, strAttrName);
                                yield return new ObjectValues(child.Path, strAttrName, propertyValue);
                            }
                        }
                    }
            }
        }

        private IEnumerable<ObjectValues> getValues(string LDAPPath, ArrayList propertyList, bool secureConnection, bool includeChildItems)
        {
            DirectoryEntry objRootDSE;
            if (secureConnection) { objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password, AuthenticationTypes.Secure | AuthenticationTypes.Sealing | AuthenticationTypes.Signing); }
            else { objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password); }

            AccessControlEntry objAce = new AccessControlEntry();
            

            foreach (string strAttrName in propertyList)
            {
                if (objRootDSE.Properties.Contains(strAttrName))
                {
                    //Multi-Value Property
                    if (objRootDSE.Properties[strAttrName].Count > 1)
                    {
                        PropertyValueCollection ValueCollection = objRootDSE.Properties[strAttrName];
                        IEnumerator en = ValueCollection.GetEnumerator();

                        while (en.MoveNext())
                        {
                            if (en.Current != null)
                            {
                                yield return new ObjectValues(LDAPPath, strAttrName, en.Current.ToString());
                            }
                        }
                    }
                    //Single Value Property
                    else
                    {
                        string propertyValue = getSinglePropertyValue(objRootDSE, strAttrName);
                        yield return new ObjectValues(LDAPPath, strAttrName, propertyValue);
                    }
                }
            }
            if (includeChildItems)
            {
                var children = objRootDSE.Children;
                foreach (DirectoryEntry child in children) using (child)
                {
                    foreach (string strAttrName in propertyList)
                    {
                        if (child.Properties.Contains(strAttrName))
                        {
                            //Multi-Value Property
                            if (child.Properties[strAttrName].Count > 1)
                            {
                                PropertyValueCollection ValueCollection = child.Properties[strAttrName];
                                IEnumerator en = ValueCollection.GetEnumerator();

                                while (en.MoveNext())
                                {
                                    if (en.Current != null)
                                    {
                                        yield return new ObjectValues(child.Path, strAttrName, en.Current.ToString());
                                    }
                                }
                            }
                            //Single Value Property
                            else
                            {
                                string propertyValue = getSinglePropertyValue(child, strAttrName);
                                yield return new ObjectValues(child.Path, strAttrName, propertyValue);
                            }
                        }
                    }
                }
            }
        }

        private static string getSinglePropertyValue(DirectoryEntry objRootDSE, string strAttrName)
        {
            string propertyValue = objRootDSE.Properties[strAttrName].Value.ToString();
            try
            {
                switch (strAttrName)
                {
                    case "msFVE-VolumeGuid":
                        propertyValue = new Guid((byte[])objRootDSE.Properties[strAttrName].Value).ToString();
                        break;
                    case "msFVE-RecoveryGuid":
                        propertyValue = new Guid((byte[])objRootDSE.Properties[strAttrName].Value).ToString();
                        break;
                    case "msFVE-KeyPackage":
                        propertyValue = BitConverter.ToString((byte[])objRootDSE.Properties[strAttrName].Value);
                        break;
                    case "uSNCreated":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "uSNChanged":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "objectGUID":
                        propertyValue = objRootDSE.Guid.ToString();
                        break;
                    case "badPasswordTime":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "lastLogoff":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "lastLogon":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "pwdLastSet":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "objectSid":
                        propertyValue = SIDtoString((byte[])objRootDSE.Properties[strAttrName].Value);
                        break;
                    case "accountExpires":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "lockoutTime":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "lastLogonTimestamp":
                        propertyValue = ConvertToTimeString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "msExchMailboxSecurityDescriptor":
                        propertyValue = securityDescriptorToString(objRootDSE, strAttrName, propertyValue);
                        break;
                    case "nTSecurityDescriptor":
                        propertyValue = securityDescriptorToString(objRootDSE, strAttrName, propertyValue);
                        break;
                }
            }
            catch { }
            return propertyValue;
        }

        private static string securityDescriptorToString(DirectoryEntry objRootDSE, string strAttrName, string propertyValue)
        {
            SecurityDescriptor sd = (SecurityDescriptor)objRootDSE.Properties[strAttrName].Value;
            AccessControlList acl = (AccessControlList)sd.DiscretionaryAcl;
            String m_Trustee = String.Empty;
            String m_AccessMask = String.Empty;
            String m_AceType = String.Empty;

            foreach (AccessControlEntry ace in (IEnumerable)acl)
            {
                m_Trustee = m_Trustee + "," + ace.Trustee;
                m_AccessMask = m_AccessMask + "," + ace.AccessMask.ToString();
                m_AceType = m_AceType + "," + ace.AceType.ToString();

            }
            propertyValue = "Trustee: " + m_Trustee + " " + "AccessMask: " + m_AccessMask + "AcessType: " + m_AceType;
            return propertyValue;
        }

        private static string ConvertToTimeString(DirectoryEntry objRootDSE, string strAttrName, string propertyValue)
        {
            IADsLargeInteger IADstimeObj = (IADsLargeInteger)objRootDSE.Properties[strAttrName].Value;
            long timeObj = GetLongFromLargeInteger(IADstimeObj);
            propertyValue = DateTime.FromFileTime(timeObj).ToString();
            return propertyValue;
        }

        private static long GetLongFromLargeInteger(IADsLargeInteger Li)
        {
            long retval = Li.HighPart;
            retval <<= 32;
            retval |= (uint)Li.LowPart;
            return retval;
        }

        private static string SIDtoString(byte[] sidBinary)
        {
            SecurityIdentifier sid = new SecurityIdentifier(sidBinary, 0);
            return sid.ToString();
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

    }
    internal class getObjectPropValuesRefStrings
    {
        public static string ldapPath = "Object LDAP Path";
        public static string useSecureConnection = "Use Secure Connection";
        public static string childItemSelection = "Include Child Items";
        public static string[] tOrF = { "True", "False" };
    }
}

