using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("New Snapshot")]
    class NewSnapshot : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String VM = String.Empty;
        private String Name = String.Empty;
        private String Description = String.Empty;
        private String Memory = String.Empty;
        private String Quiesce = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("VM");
            designer.AddInput("Name");
            designer.AddInput("Quiesce").NotRequired();
            designer.AddInput("Memory").NotRequired();
            designer.AddCorellatedData(typeof(snapshot));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VM = request.Inputs["VM"].AsString();
            Name = request.Inputs["Name"].AsString();
            Quiesce = request.Inputs["Quiesce"].AsString();
            Memory = request.Inputs["Memory"].AsString();

            response.WithFiltering().PublishRange(runCommand());
        }

        private IEnumerable<snapshot> runCommand()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "New-Snapshot";

            if (!(VM == String.Empty)) { command += " -VM \"" + VM + "\""; }
            if (!(Name == String.Empty)) { command += " -Name \"" + Name + "\""; }
            if (Memory.Equals("true")) { command += " -Memory:$True"; }
            if (Quiesce.Equals("true")) { command += " -Quiesce:$True"; }

            Script += command + " -Confirm:$False";

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
                if (obj.BaseObject.GetType().ToString().Contains("VMware.VimAutomation.ViCore.Impl.V1.VM.SnapshotImpl"))
                {
                    String Created = obj.Members["Created"].Value.ToString();
                    String Quiesced = obj.Members["Quiesced"].Value.ToString();
                    String PowerState = obj.Members["PowerState"].Value.ToString();
                    String VMId = obj.Members["VMId"].Value.ToString();
                    String SizeMB = obj.Members["SizeMB"].Value.ToString();
                    String IsCurrent = obj.Members["IsCurrent"].Value.ToString();
                    String IsReplaySupported = obj.Members["IsReplaySupported"].Value.ToString();
                    String Id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    String Snapshot_Description = String.Empty;
                    try { Snapshot_Description = obj.Members["Description"].Value.ToString(); }
                    catch { }

                    yield return new snapshot(Created, Quiesced, PowerState, VMId, SizeMB, IsCurrent, IsReplaySupported, Id, name, Snapshot_Description);
                }
            }
            runspace.Close();
        }
    }
}

