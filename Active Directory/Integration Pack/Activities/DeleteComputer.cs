using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Delete AD Computer", ShowFilters = false)]
    class DeleteComputer : IActivity
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
            designer.AddInput("Computer LDAP Path").WithDefaultValue("LDAP://Contoso.com/CN=UserName,OU=Container,DC=Contoso,DC=Com");
            designer.AddOutput("Deleted Computer LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string ComputerLDAPPath = CapitalizeLDAPPath(request.Inputs["Computer LDAP Path"].AsString());

            DeleteADObject(ComputerLDAPPath);

            response.Publish("Deleted User LDAP Path", ComputerLDAPPath);
        }

        private void DeleteADObject(string ObjectLDAPPath)
        {
            DirectoryEntry dirObject = new DirectoryEntry(ObjectLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry objParent = new DirectoryEntry(dirObject.Parent.Path, credentials.UserName + "@" + credentials.Domain, credentials.Password);

            objParent.Children.Remove(dirObject);
            objParent.CommitChanges();
            objParent.Close();
            dirObject.Dispose();
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

