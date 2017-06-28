using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Remove Network Adapter")]
    class RemoveNetworkAdapter : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String NetworkAdapter = String.Empty;
        private String VM = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Network Adapter Name");
            designer.AddInput("VM Name");

            designer.AddCorellatedData(typeof(networkAdapter));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            NetworkAdapter = request.Inputs["Network Adapter Name"].AsString();
            VM = request.Inputs["VM Name"].AsString();

            response.WithFiltering().PublishRange(runCommand());
        }

        private IEnumerable<networkAdapter> runCommand()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String preCommand = "Get-NetworkAdapter";

            if (!(VM == String.Empty)) { preCommand += " -VM \"" + VM + "\" | ? {$_.Name -eq \"" + NetworkAdapter + "\"}"; }

            String command = "Remove-NetworkAdapter";

            Script += preCommand + " | " + command + " -Confirm:$False";

            pipeline.Commands.AddScript(Script);

            Collection<PSObject> results = new Collection<PSObject>();

            try
            {
                results = pipeline.Invoke();
            }

            catch (Exception ex)
            {
                throw ex;
            }

            foreach (PSObject obj in results)
            {
                if (obj.BaseObject.GetType().ToString().Contains("NetworkAdapterImpl"))
                {
                    String Mac_Address = obj.Members["MacAddress"].Value.ToString();
                    String WakeOnLanEnabled = obj.Members["WakeOnLanEnabled"].Value.ToString();
                    String Network_Name = obj.Members["NetworkName"].Value.ToString();
                    String ParentId = obj.Members["ParentId"].Value.ToString();
                    String Id = obj.Members["Id"].Value.ToString();
                    String Name = obj.Members["Name"].Value.ToString();

                    yield return new networkAdapter(Mac_Address, WakeOnLanEnabled, Network_Name, ParentId, Id, Name);
                }
            }
            runspace.Close();
        }
    }
}

