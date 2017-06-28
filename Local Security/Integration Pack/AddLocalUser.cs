using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace LocalComputerSecurity
{
    [Activity("Add Local User")]
    public class AddLocalUser : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("User Name");
            designer.AddInput("User Password").PasswordProtect();
            designer.AddInput("User Full Name").NotRequired();

            designer.AddInput("Alternate Connection Username").NotRequired().WithDefaultValue("UserID");
            designer.AddInput("Alternate Connection User Domain").NotRequired().WithDefaultValue("userDomain");
            designer.AddInput("Alternate Connection Password").NotRequired().PasswordProtect();

            designer.AddOutput("User Name");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String userName = String.Empty;
            String password = String.Empty;
            String userFullName = String.Empty;
            String computerName = String.Empty;
            userName = request.Inputs["User Name"].AsString();
            password = request.Inputs["User Password"].AsString();
            userFullName = request.Inputs["User Full Name"].AsString();
            computerName = request.Inputs["Computer Name"].AsString();

            String aUserName = String.Empty;
            String aUserDomain = String.Empty;
            String aUserPassword = String.Empty;

            if (request.Inputs.Contains("Alternate Connection Username")) { aUserName = request.Inputs["Alternate Connection Username"].AsString(); }
            if (request.Inputs.Contains("Alternate Connection User Domain")) { aUserDomain = request.Inputs["Alternate Connection User Domain"].AsString(); }
            if (request.Inputs.Contains("Alternate Connection Password")) { aUserPassword = request.Inputs["Alternate Connection Password"].AsString(); }

            DirectoryEntry hostMachineDirectory;
            if (aUserName.Equals(String.Empty) || aUserDomain.Equals(String.Empty) || aUserPassword.Equals(String.Empty))
            {
                hostMachineDirectory = new DirectoryEntry("WinNT://" + computerName);
            }
            else
            {
                hostMachineDirectory = new DirectoryEntry("WinNT://" + computerName, string.Format("{0}@{1}", aUserName, aUserDomain), aUserPassword);
            }
            DirectoryEntries entries = hostMachineDirectory.Children;
            bool userExists = false; 
            foreach (DirectoryEntry each in entries)
            { 
                userExists = each.Name.Equals(userName,StringComparison.CurrentCultureIgnoreCase); 
                if (userExists) 
                    break; 
            } 
     
            if (false == userExists) 
            {
                DirectoryEntry obUser = entries.Add(userName, "User"); 
        
                if(userFullName == String.Empty) { obUser.Properties["FullName"].Add(userFullName); }
                else { obUser.Properties["FullName"].Add("Local user"); }
                
                obUser.Invoke("SetPassword", password); 
                obUser.Invoke("Put", new object[] {"UserFlags", 0x10000}); 
                obUser.CommitChanges(); 
            }
            response.Publish("User Name", userName);
        }
    }
}

