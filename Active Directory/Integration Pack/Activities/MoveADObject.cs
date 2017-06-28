using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Move AD Object")]
    class MoveADObject : IActivity
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
            designer.AddInput("Source Object LDAP Path").WithDefaultValue("LDAP://Contoso.com/CN=ObjectName,OU=Container,DC=Contoso,DC=Com");
            designer.AddInput("Destination Container OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=DestinationContainer,DC=Contoso,DC=Com");
            designer.AddOutput("Object LDAP Path");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String originalLDAPPath = CapitalizeLDAPPath(request.Inputs["Source Object LDAP Path"].AsString());
            String ContainerOULDAPPath = CapitalizeLDAPPath(request.Inputs["Destination Container OU LDAP Path"].AsString());

            DirectoryEntry sourceObject = new DirectoryEntry(originalLDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry destinationLocation = new DirectoryEntry(ContainerOULDAPPath, credentials.UserName + "@" + credentials.Domain, credentials.Password);

            sourceObject.MoveTo(destinationLocation, sourceObject.Name);

            String finalPath = sourceObject.Path;

            sourceObject.Close();
            destinationLocation.Close();

            response.Publish("Object LDAP Path", finalPath);
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

