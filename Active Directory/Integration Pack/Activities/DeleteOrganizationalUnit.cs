using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Delete Organizational Unit", ShowFilters = false)]
    class DeleteOrganizationalUnit : IActivity
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
            designer.AddInput("OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=Container,DC=Contoso,DC=Com");
            designer.AddOutput("Deleted OU LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string ouLDAPPath = CapitalizeLDAPPath(request.Inputs["OU LDAP Path"].AsString());

            DirectoryEntry dirEntry = new DirectoryEntry(ouLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            
            dirEntry.DeleteTree();
            dirEntry.CommitChanges();
            dirEntry.Close();

            response.Publish("Deleted OU LDAP Path", ouLDAPPath);
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

