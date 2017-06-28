using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Remove User From Group", ShowFilters = false)]
    class RemoveUserFromGroup : IActivity
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
            designer.AddInput("Group LDAP Path").WithDefaultValue("LDAP://Contoso.com/CN=GroupName,OU=Container,DC=Contoso,DC=Com");
            designer.AddInput("User LDAP Path").WithDefaultValue("LDAP://Contoso.com/CN=GroupName,OU=Container,DC=Contoso,DC=Com");
            designer.AddOutput("Removed User LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string GroupLDAPPath = CapitalizeLDAPPath(request.Inputs["Group LDAP Path"].AsString());
            string UserLDAPPath = CapitalizeLDAPPath(request.Inputs["User LDAP Path"].AsString());

            DirectoryEntry dirEntry = new DirectoryEntry(GroupLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry userEntry = new DirectoryEntry(UserLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);

            String userDN = userEntry.Properties["DistinguishedName"].Value.ToString();

            dirEntry.Properties["member"].Remove(userDN);
            dirEntry.CommitChanges();
            dirEntry.Close();

            response.Publish("Removed User LDAP Path", UserLDAPPath);
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

