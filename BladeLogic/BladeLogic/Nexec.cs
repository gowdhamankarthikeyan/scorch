using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Renci.SshNet;
using System.IO;
namespace BladeLogic
{
    [Activity("Nexec")]
    public class Nexec : IActivity
    {
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Host Name");
            designer.AddInput("Command");
            designer.AddInput("Key File Location").NotRequired().WithFileBrowser().WithDefaultValue(@"\\Path\To\KeyFile\KeyfileName");
            designer.AddInput("Key File Passphrase").NotRequired().WithDefaultValue("Key File Passphrase");

            designer.AddOutput("Command Output");
            designer.AddOutput("Command Extended Output");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String HostName = request.Inputs["Host Name"].AsString();
            String Command = request.Inputs["Command"].AsString();

            String keyFileLocation = String.Empty;
            String keyFilePassphrase = String.Empty;

            Dictionary<string, string> returnValues = new Dictionary<string, string>();

            if (request.Inputs.Contains("Key File Location")) { keyFileLocation = request.Inputs["Key File Location"].AsString(); }
            if (request.Inputs.Contains("Key File Passphrase")) { keyFilePassphrase = request.Inputs["Key File Passphrase"].AsString(); }

            if (keyFileLocation.Equals(String.Empty))
            {
                using (SshClient client = new SshClient(Credentials.serverName, Credentials.UserName, Credentials.Password))
                {
                    client.Connect();
                    returnValues = runCommand(client, HostName, Command);
                    client.Disconnect();
                }
            }
            else
            {
                if (keyFilePassphrase.Equals(String.Empty))
                {
                    using (var client = new SshClient(Credentials.serverName, Credentials.UserName, new PrivateKeyFile(keyFileLocation)))
                    {
                        client.Connect();
                        returnValues = runCommand(client, HostName, Command);
                        client.Disconnect();
                    }    
                }
                else
                {
                    using (var client = new SshClient(Credentials.serverName, Credentials.UserName, new PrivateKeyFile(keyFileLocation,keyFilePassphrase)))
                    {
                        client.Connect();
                        returnValues = runCommand(client, HostName, Command);
                        client.Disconnect();
                    }
                }
            }
            response.Publish("Command Output", returnValues["Command Output"]);
            response.Publish("Command Extended Output", returnValues["Command Extended Output"]);
        }

        private Dictionary<string,string> runCommand(SshClient client, string HostName, string Command)
        {
            Dictionary<string, string> returnValues = new Dictionary<string, string>();

            StringBuilder sb = new StringBuilder();
            sb.Append("/usr/nsh/bin/nexec ");
            sb.Append(HostName);
            sb.Append(" ");
            sb.Append(Command);

            SshCommand command = client.RunCommand(sb.ToString());
            String extendedData = new StreamReader(command.ExtendedOutputStream, Encoding.ASCII).ReadToEnd();

            returnValues.Add("Command Output", command.Result);
            returnValues.Add("Command Extended Output", extendedData);

            return returnValues;
        }
    }
}
