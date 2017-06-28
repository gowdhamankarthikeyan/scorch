using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Create Computer Account", ShowFilters = false)]
    class CreateComputerAccount : IActivity
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
            designer.AddInput("Computer Account Name").WithDefaultValue("computerName");
            designer.AddInput("Computer Account Password").PasswordProtect().NotRequired();
            designer.AddInput("Destination OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=Container,DC=Contoso,DC=Com");
            designer.AddOutput("New Computer Account LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string AccountName = request.Inputs["Computer Account Name"].AsString();
            string password = request.Inputs["Computer Account Password"].AsString();
            string ParentOU = CapitalizeLDAPPath(request.Inputs["Destination OU LDAP Path"].AsString());

            string NewAccountLDAPPath = string.Empty;
            string connectionPrefix = ParentOU;
            DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry newComputerAccount = dirEntry.Children.Add("CN=" + AccountName, "computer");
            try
            {
                newComputerAccount.Properties["samAccountName"].Value = AccountName;
                newComputerAccount.Properties["netbootGUID"].Add(Guid.NewGuid().ToByteArray());
                newComputerAccount.Properties["userAccountControl"].Value = 0x00001000;

                if (password.Equals(string.Empty))
                {
                    newComputerAccount.Invoke("SetPassword", new object[] { RandomPassword.Generate(12) });
                    newComputerAccount.CommitChanges();
                }
                else
                {
                    newComputerAccount.Invoke("SetPassword", new object[] { password });
                    newComputerAccount.CommitChanges();
                }

                newComputerAccount.CommitChanges();
            }
            catch
            {
                throw;
            }

            dirEntry.Close();
            newComputerAccount.Close();

            string newComputerAccountPath = newComputerAccount.Path;
            response.Publish("New Computer Account LDAP Path", newComputerAccountPath);
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
