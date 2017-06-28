using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Move Virtual Machine")]
    class MoveVM : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String VM = String.Empty;
        private String Folder = String.Empty;
        private String Host = String.Empty;
        private String ResourcePool = String.Empty;
        private String Datastore = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("VM");
            designer.AddInput("Folder").NotRequired();
            designer.AddInput("Host").NotRequired();
            designer.AddInput("Resource Pool").NotRequired();
            designer.AddInput("Datastore").NotRequired();
            designer.AddCorellatedData(typeof(vm));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VM = request.Inputs["VM"].AsString();
            Folder = request.Inputs["Folder"].AsString();
            Host = request.Inputs["Host"].AsString();
            ResourcePool = request.Inputs["Resource Pool"].AsString();
            Datastore = request.Inputs["Datastore"].AsString();

            response.WithFiltering().PublishRange(runCommand());
        }

        private IEnumerable<vm> runCommand()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "Move-VM";
            String preCommand = "";
            if (!(VM == String.Empty)) { command += " -VM \"" + VM + "\""; }
            if (!(Folder == String.Empty)) 
            {
                preCommand += "$Folder = Get-Folder \"" + Folder + "\"\n";
                command += " -Destination $Folder"; 
            }
            if (!(Host == String.Empty))
            {
                preCommand += "$Host = Get-VMHost \"" + Host + "\"\n";
                command += " -Destination $Host";
            }
            if (!(ResourcePool == String.Empty))
            {
                preCommand += "$Folder = Get-ResourcePool \"" + ResourcePool + "\"\n";
                command += " -Destination $ResourcePool";
            }
            if (!(Datastore == String.Empty)) { command += " -Datastore \"" + Datastore + "\""; }

            Script += command + " -confirm:$False";

            pipeline.Commands.AddScript(Script);

            Collection<PSObject> results = new Collection<PSObject>();

            try
            {
                results = pipeline.Invoke();
            }

            catch (Exception ex)
            {
                results.Add(new PSObject((object)ex.Message));
            }

            foreach (PSObject obj in results)
            {
                if (obj.BaseObject.GetType().ToString().Contains("VMware.VimAutomation.ViCore.Impl.V1.Inventory.VirtualMachineImpl"))
                {
                    String PowerState = obj.Members["PowerState"].Value.ToString();
                    String VMVersion = obj.Members["Version"].Value.ToString();
                    String NumCpu = obj.Members["NumCpu"].Value.ToString();
                    String MemoryMB = obj.Members["MemoryMB"].Value.ToString();
                    String HostId = obj.Members["HostId"].Value.ToString();
                    String FolderId = obj.Members["FolderId"].Value.ToString();
                    String ResourcePoolId = obj.Members["ResourcePoolId"].Value.ToString();
                    String UsedSpaceGB = obj.Members["UsedSpaceGB"].Value.ToString();
                    String ProvisionedSpaceGB = obj.Members["ProvisionedSpaceGB"].Value.ToString();
                    String id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    String Description = String.Empty;
                    String Notes = String.Empty;
                    try { Description = obj.Members["Description"].Value.ToString(); }
                    catch { }
                    try { Notes = obj.Members["Notes"].Value.ToString(); }
                    catch { }

                    yield return new vm(PowerState, VMVersion, Description, Notes, NumCpu, MemoryMB, HostId, FolderId, ResourcePoolId, UsedSpaceGB, ProvisionedSpaceGB, id, name);
                }
            }
            runspace.Close();
        }
    }
}

