using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;


namespace Active_Directory
{
    [Activity("Disable Account", ShowFilters = false)]
    class DisableAccount : IActivity
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
            designer.AddOutput("Disabled Object LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string objectLDAPPath = CapitalizeLDAPPath(request.Inputs["Object LDAP Path"].AsString());

            DirectoryEntry obj = new DirectoryEntry(objectLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            int val = (int)obj.Properties["userAccountControl"].Value;
            obj.Properties["userAccountControl"].Value = val | 0x2;
            //ADS_UF_ACCOUNTDISABLE;

            obj.CommitChanges();
            obj.Close();

            response.Publish("Disabled Object LDAP Path", objectLDAPPath);
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

