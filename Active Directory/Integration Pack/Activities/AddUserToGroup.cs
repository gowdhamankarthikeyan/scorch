using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Add User to Group")]
    class AddUserToGroup : IActivity
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
            designer.AddInput("User LDAP Path").WithDefaultValue("LDAP://Contoso.com/cn=UserName,ou=Container,DC=Contoso,DC=Com");
            designer.AddInput("Group LDAP Path").WithDefaultValue("LDAP://Contoso.com/cn=GroupName,ou=Container,DC=Contoso,DC=Com");
            designer.AddCorellatedData(typeof(ADObject));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String userLDAPPath = CapitalizeLDAPPath(request.Inputs["User LDAP Path"].AsString());
            String groupLDAPPath = CapitalizeLDAPPath(request.Inputs["Group LDAP Path"].AsString());

            try
            {
                DirectoryEntry dirEntry = new DirectoryEntry(groupLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
                DirectoryEntry userEntry = new DirectoryEntry(userLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
                
                dirEntry.Properties["member"].Add(userEntry.Properties["DistinguishedName"].Value.ToString());

                dirEntry.CommitChanges();
                dirEntry.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            response.WithFiltering().PublishRange(getGroupMembers(groupLDAPPath));
        }

        private IEnumerable<ADObject> getGroupMembers(String groupLDAPPath)
        {
            DirectoryEntry groupEntry = new DirectoryEntry(groupLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            
            DirectoryEntry DomainRoot = groupEntry; 
            do { DomainRoot = DomainRoot.Parent; } 
            while (DomainRoot.SchemaClassName != "domainDNS");

            DirectorySearcher mySearcher = new DirectorySearcher(DomainRoot);
            mySearcher.PageSize = 1000;
            mySearcher.Filter = "(memberOf=" + groupEntry.Properties["DistinguishedName"].Value.ToString() + ")";
            SearchResultCollection resultCollection = mySearcher.FindAll();

            foreach (SearchResult result in resultCollection)
            {
                DirectoryEntry directoryObject = result.GetDirectoryEntry();
                String ldapPath = directoryObject.Path;

                yield return new ADObject(ldapPath);
                directoryObject.Close();
            }
            groupEntry.Close();
            groupEntry.Dispose();
            mySearcher.Dispose();
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

