using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace LocalComputerSecurity
{
    [Activity("Remove User From Local Computer Group")]
    public class RemoveUserFromLocalGroup : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("Local Group Name");
            designer.AddInput("User Name");
            designer.AddInput("Domain").NotRequired();

            designer.AddInput("Alternate Connection Username").NotRequired().WithDefaultValue("UserID");
            designer.AddInput("Alternate Connection User Domain").NotRequired().WithDefaultValue("userDomain");
            designer.AddInput("Alternate Connection Password").NotRequired().PasswordProtect();

            designer.AddOutput("Group Name");
            designer.AddOutput("User Name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String computerName = request.Inputs["Computer Name"].AsString();
            String localGroupName = request.Inputs["Local Group Name"].AsString();
            String userName = request.Inputs["User Name"].AsString();
            String domain = request.Inputs["Domain"].AsString();

            String aUserName = String.Empty;
            String aUserDomain = String.Empty;
            String aUserPassword = String.Empty;

            if (request.Inputs.Contains("Alternate Connection Username")) { aUserName = request.Inputs["Alternate Connection Username"].AsString(); }
            if (request.Inputs.Contains("Alternate Connection User Domain")) { aUserDomain = request.Inputs["Alternate Connection User Domain"].AsString(); }
            if (request.Inputs.Contains("Alternate Connection Password")) { aUserPassword = request.Inputs["Alternate Connection Password"].AsString(); }

            try
            {
                DirectoryEntry AD;
                if (aUserName.Equals(String.Empty) || aUserDomain.Equals(String.Empty) || aUserPassword.Equals(String.Empty))
                {
                    AD = new DirectoryEntry("WinNT://" + computerName + ",computer");
                }
                else
                {
                    AD = new DirectoryEntry("WinNT://" + computerName + ",computer", string.Format("{0}@{1}", aUserName, aUserDomain), aUserPassword);
                }

                if (domain != null)
                {
                    DirectoryEntry Domain;
                    if (aUserName.Equals(String.Empty) || aUserDomain.Equals(String.Empty) || aUserPassword.Equals(String.Empty))
                    {
                        Domain = new DirectoryEntry("WinNT://" + domain);
                    }
                    else
                    {
                        Domain = new DirectoryEntry("WinNT://" + domain, string.Format("{0}@{1}", aUserName, aUserDomain), aUserPassword);
                    }
                    DirectoryEntry user = Domain.Children.Find(userName, "User");

                    DirectoryEntry grp = AD.Children.Find(localGroupName, "group");
                    if (grp != null)
                    {
                        grp.Invoke("Remove", new object[] { user.Path.ToString() });
                        response.Publish("Group Name", localGroupName);
                        response.Publish("User Name", user.Path.ToString());
                    }
                    else { response.ReportErrorEvent("Failed to find local group", ""); throw new Exception("Failed to find local group"); }
                }
                else
                {
                    DirectoryEntry user = AD.Children.Find(userName, "User");

                    DirectoryEntry grp = AD.Children.Find(localGroupName, "group");
                    if (grp != null)
                    {
                        grp.Invoke("Remove", new object[] { user.Path.ToString() });
                        response.Publish("Group Name", localGroupName);
                        response.Publish("User Name", user.Path.ToString());
                    }
                    else { response.ReportErrorEvent("Failed to find local group", ""); throw new Exception("Failed to find local group"); }
                }
            }
            catch (Exception ex)
            {
                response.ReportErrorEvent("Failed to add user to local group", ex.Message.ToString());
                throw ex;
            }
        }
    }
}

