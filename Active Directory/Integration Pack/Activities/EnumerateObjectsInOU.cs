using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;


namespace Active_Directory
{
    [Activity("Enumerate Objects in an OU")]
    public class EnumerateObjectsInOU : IActivity
    {
        private ConnectionCredentials credentials;
        private string OuDn = string.Empty;

        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Containing OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=Container,DC=Contoso,DC=Com");
            designer.AddCorellatedData(typeof(ADObject));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            OuDn = CapitalizeLDAPPath(request.Inputs["Containing OU LDAP Path"].AsString());

            response.WithFiltering().PublishRange(getChildObjects(OuDn));
        }

        private IEnumerable<ADObject> getChildObjects(string OuDn)
        {
        
            DirectoryEntry directoryObject = new DirectoryEntry(OuDn, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            foreach (DirectoryEntry child in directoryObject.Children)
            {
                string childPath = child.Path.ToString();
                child.Close();
                child.Dispose();
                yield return new ADObject(childPath);
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

