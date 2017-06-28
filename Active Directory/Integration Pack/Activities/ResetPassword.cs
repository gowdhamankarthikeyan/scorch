using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Collections;

namespace Active_Directory
{
    [Activity("Reset Password", ShowFilters = false)]
    class ResetPassword : IActivity
    {
        private string domainName = string.Empty;
        private string objectName = string.Empty;
        private string objClass = string.Empty;
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
            designer.AddInput("New User Password").PasswordProtect();
            designer.AddInput("User Must Change Password").WithListBrowser(new string[] { "Yes", "No" }).NotRequired().WithDefaultValue("Yes");
            designer.AddOutput("User LDAP Path").AsString();
            designer.AddOutput("User Must Change Password").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string userDn = CapitalizeLDAPPath(request.Inputs["User LDAP Path"].AsString());
            string password = request.Inputs["New User Password"].AsString();
            string changePassword = "Yes";

            if (request.Inputs.Contains("User Must Change Password")) { changePassword = request.Inputs["User Must Change Password"].AsString(); }
            if (DirectoryEntry.Exists(userDn))
            {
                try
                {
                    DirectoryEntry uEntry = new DirectoryEntry(userDn);
                    uEntry.Invoke("SetPassword", new object[] { password });
                    uEntry.Properties["LockOutTime"].Value = 0; //unlock account
                    switch (changePassword)
                    {
                        case "Yes":
                                uEntry.Properties["pwdLastSet"].Value = -1; //set password to must change
                            break;
                        case "No":
                                uEntry.Properties["pwdLastSet"].Value = 0; //set password to must change
                            break;
                        default:
                                uEntry.Properties["pwdLastSet"].Value = -1; //set password to must change
                            break;
                    }
                    
                    uEntry.CommitChanges();
                    uEntry.Close();

                    response.Publish("User LDAP Path", userDn);
                    response.Publish("User Must Change Password", changePassword);
                }
                catch (System.DirectoryServices.DirectoryServicesCOMException E)
                {
                    throw E;
                }
            }
            else
            {
                throw new Exception("User with DN " + userDn + " does not exist");
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

