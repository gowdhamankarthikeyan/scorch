using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace LocalComputerSecurity
{
    [Activity("Remove Local User")]
    public class RemoveLocalUser : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("User Name");

            designer.AddInput("Alternate Connection Username").NotRequired().WithDefaultValue("UserID");
            designer.AddInput("Alternate Connection User Domain").NotRequired().WithDefaultValue("userDomain");
            designer.AddInput("Alternate Connection Password").NotRequired().PasswordProtect();

            designer.AddOutput("User Name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String userName = String.Empty;
            String computerName = String.Empty;
            userName = request.Inputs["User Name"].AsString();
            computerName = request.Inputs["Computer Name"].AsString();

            String aUserName = String.Empty;
            String aUserDomain = String.Empty;
            String aUserPassword = String.Empty;

            if (request.Inputs.Contains("Alternate Connection Username")) { aUserName = request.Inputs["Alternate Connection Username"].AsString(); }
            if (request.Inputs.Contains("Alternate Connection User Domain")) { aUserDomain = request.Inputs["Alternate Connection User Domain"].AsString(); }
            if (request.Inputs.Contains("Alternate Connection Password")) { aUserPassword = request.Inputs["Alternate Connection Password"].AsString(); }

            DirectoryEntry machine;
            if (aUserName.Equals(String.Empty) || aUserDomain.Equals(String.Empty) || aUserPassword.Equals(String.Empty))
            {
                machine = new DirectoryEntry("WinNT://" + computerName);
            }
            else
            {
                machine = new DirectoryEntry("WinNT://" + computerName, string.Format("{0}@{1}", aUserName, aUserDomain), aUserPassword);
            }

            DirectoryEntries entries = machine.Children;
            DirectoryEntry user = entries.Find(userName);
            entries.Remove(user);
            machine.Close();
            
            response.Publish("User Name", userName);
        }
    }
}

