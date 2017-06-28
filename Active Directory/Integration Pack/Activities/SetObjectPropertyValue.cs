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
    [Activity("Set Object Property Value")]
    class setObjectPropertyValue : IActivity
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
            designer.AddInput("Object LDAP Path").WithDefaultValue("LDAP://Contoso.com/CN=ObjectName,OU=Container,DC=Contoso,DC=Com");
            designer.AddInput("Property Name").WithDefaultValue("Property Name");
            designer.AddInput("Property Value").WithDefaultValue("New Property Value");
            designer.AddInput("Use Secure Connection").WithDefaultValue("False").WithListBrowser(new string[] { "True", "False" }).NotRequired();
            designer.AddCorellatedData(typeof(ObjectValues));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            bool useSecureConnection = false;
            if (request.Inputs.Contains("Use Secure Connection")) { useSecureConnection = Convert.ToBoolean(request.Inputs["Use Secure Connection"].AsString()); }

            string LDAPPath = CapitalizeLDAPPath(request.Inputs["Object LDAP Path"].AsString());
            string propertyName = request.Inputs["Property Name"].AsString();
            string propertyValue = request.Inputs["Property Value"].AsString();

            response.WithFiltering().PublishRange(setPropertyValue(LDAPPath, propertyName, propertyValue, useSecureConnection));            
        }

        private IEnumerable<ObjectValues> setPropertyValue(string LDAPPath, string propertyName, string propertyValue, bool secureConnection)
        {
            DirectoryEntry objRootDSE;
            if (secureConnection) { objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password, AuthenticationTypes.Secure | AuthenticationTypes.Sealing | AuthenticationTypes.Signing); }
            else { objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password); }
            
            setSinglePropertyValue(objRootDSE, propertyName, propertyValue);

            objRootDSE = new DirectoryEntry(LDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            //Multi-Value Property
            if (objRootDSE.Properties[propertyName].Count > 1)
            {
                PropertyValueCollection ValueCollection = objRootDSE.Properties[propertyName];
                IEnumerator en = ValueCollection.GetEnumerator();

                while (en.MoveNext())
                {
                    if (en.Current != null)
                    {
                        yield return new ObjectValues(LDAPPath, propertyName, en.Current.ToString());
                    }
                }
            }
            //Single Value Property
            else
            {
                propertyValue = getSinglePropertyValue(objRootDSE, propertyName);
                yield return new ObjectValues(LDAPPath, propertyName, propertyValue);
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
                    byte[] array=new byte[arr.Length];
                    for(int i=0; i<arr.Length; i++) array[i]=Convert.ToByte(arr[i],16);
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

        private static IADsLargeInteger GetLargeIntegerFromDateTime(DateTime dateTimeValue)
        {
            //
            // Convert DateTime value to utc file time
            //
            Int64 int64Value = dateTimeValue.ToFileTimeUtc();
        
            //
            // convert to large integer
            //
            IADsLargeInteger largeIntValue = (IADsLargeInteger) new LargeInteger();
            largeIntValue.HighPart = (int) (int64Value >> 32);
            largeIntValue.LowPart = (int) (int64Value & 0xFFFFFFFF);
        
            return largeIntValue;
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
}

