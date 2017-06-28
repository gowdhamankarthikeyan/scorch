using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Get Users Group Membership")]
    public class GetUserGroupMemberships : IActivity
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
            designer.AddInput("User LDAP Path").WithDefaultValue("LDAP://Contoso.com/CN=UserName,OU=Container,DC=Contoso,DC=Com");
            designer.AddCorellatedData(typeof(ADObject));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String userLDAPPath = CapitalizeLDAPPath(request.Inputs["User LDAP Path"].AsString());
            response.WithFiltering().PublishRange(getGroups(userLDAPPath));            
        }

        private IEnumerable<ADObject> getGroups(String userLDAPPath)
        {
            /*
            DirectoryEntry ent = new DirectoryEntry(userLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);

            DirectoryEntry DomainRoot = ent;
            do { DomainRoot = DomainRoot.Parent; }
            while (DomainRoot.SchemaClassName != "domainDNS");

            DirectorySearcher mySearcher = new DirectorySearcher(DomainRoot);
            mySearcher.Filter = "(memberOf=" + ent.Properties["DistinguishedName"].Value.ToString() + ")";
            SearchResultCollection resultCollection = mySearcher.FindAll();

            foreach (SearchResult result in resultCollection)
            {
                DirectoryEntry directoryObject = result.GetDirectoryEntry();
                String ldapPath = directoryObject.Path;

                yield return new ADObject(ldapPath);
                directoryObject.Close();
            }
            ent.Close();
            ent.Dispose();
            mySearcher.Dispose();
             */
            DirectoryEntry objRootDSE = new DirectoryEntry(userLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry DomainRoot = objRootDSE;
            do { DomainRoot = DomainRoot.Parent; }
            while (DomainRoot.SchemaClassName != "domainDNS");

            string LDAPPrefix = DomainRoot.Parent.Path.Replace("DC=com", "");

            PropertyValueCollection groups = objRootDSE.Properties["memberOf"];

            foreach (string groupName in groups)
            {
                yield return new ADObject(String.Format("{0}{1}", LDAPPrefix, groupName));
            }
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

