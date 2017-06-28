using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Create Organizational Unit", ShowFilters = false)]
    class CreateOrganizationalUnit : IActivity
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
            designer.AddInput("New OU Name").WithDefaultValue("New Container Name");
            designer.AddInput("Parent OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=ParentContainer,DC=Contoso,DC=Com");
            designer.AddInput("New OU Description").WithDefaultValue("Description").NotRequired();
            designer.AddOutput("Organization Unit LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string ouName = request.Inputs["New OU Name"].AsString();
            string parentLDAPPath = CapitalizeLDAPPath(request.Inputs["Parent OU LDAP Path"].AsString());
            string ouDescription = string.Empty;
            if(request.Inputs.Contains("New OU Description")) { ouDescription = request.Inputs["New OU Description"].AsString(); }

            string connectionPrefix = parentLDAPPath;
            DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry newOU = dirEntry.Children.Add("OU=" + ouName, "OrganizationalUnit");
            try
            {
                if (!ouDescription.Equals(string.Empty))
                { 
                    newOU.Properties["description"].Value = ouDescription;
                }
                newOU.CommitChanges();
            }
            catch (Exception e) { response.ReportErrorEvent("Error setting description", e.Message.ToString()); }

            string ouLDAPPath = newOU.Path;
            response.Publish("Organization Unit LDAP Path", ouLDAPPath);
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

