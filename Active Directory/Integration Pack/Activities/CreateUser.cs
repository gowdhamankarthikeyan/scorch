using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Create AD User", ShowFilters = false)]
    class CreateUser : IActivity
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
            designer.AddInput("New User Name").WithDefaultValue("username");
            designer.AddInput("New User Domain").WithDefaultValue("contoso.com");
            designer.AddInput("New User Password").PasswordProtect();
            designer.AddInput("Destination OU LDAP Path").WithDefaultValue("LDAP://Contoso.com/OU=Container,DC=Contoso,DC=Com");
            designer.AddOutput("New User LDAP Path").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string userName = request.Inputs["New User Name"].AsString();
            string password = request.Inputs["New User Password"].AsString();
            string userDomain = request.Inputs["New User Domain"].AsString();
            string ParentOU = CapitalizeLDAPPath(request.Inputs["Destination OU LDAP Path"].AsString());

            string NewUserLDAPPath = string.Empty;
            string connectionPrefix = ParentOU;
            DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix, credentials.UserName + "@" + credentials.Domain, credentials.Password);
            DirectoryEntry newUser = dirEntry.Children.Add("CN=" + userName, "user");
            try
            {
                newUser.Properties["samAccountName"].Value = userName;
                newUser.CommitChanges();
            }
            catch (Exception e) { response.ReportErrorEvent("samAccountName Setting Error", e.Message.ToString()); }

            try
            {
                newUser.Properties["userPrincipalName"].Value = userName + "@" + userDomain;
                newUser.CommitChanges();
            }
            catch (Exception e) { response.ReportErrorEvent("userPrincipalName Setting Error", e.Message.ToString()); }

            try
            {
                newUser.Invoke("SetPassword", new object[] { password });
                newUser.CommitChanges();
            }
            catch (Exception e) { response.ReportErrorEvent("Set Password Error", e.Message.ToString()); }

            try
            {
                int val = (int)newUser.Properties["userAccountControl"].Value;
                newUser.Properties["userAccountControl"].Value = val & ~0x2;
                //ADS_UF_NORMAL_ACCOUNT;

                newUser.CommitChanges();
            }
            catch (Exception e) { throw e; }

            dirEntry.Close();
            newUser.Close();

            NewUserLDAPPath = newUser.Path;
            response.Publish("New User LDAP Path",NewUserLDAPPath);
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

