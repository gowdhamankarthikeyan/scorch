using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Get Virtual Machine")]
    class GetVM : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String Datastore = String.Empty;
        private String Location = String.Empty;
        private String Name = String.Empty;
        private String Id = String.Empty;
        
        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Datastore").NotRequired();
            designer.AddInput("Location").NotRequired();
            designer.AddInput("Name").NotRequired();
            designer.AddInput("Id").NotRequired();
            designer.AddCorellatedData(typeof(vm));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            Datastore = request.Inputs["Datastore"].AsString();
            Location = request.Inputs["Location"].AsString();
            Name = request.Inputs["Name"].AsString();
            Id = request.Inputs["Id"].AsString();

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

            String command = "Get-VM";

            if (!(Datastore == String.Empty)) { command += " -Datastore " + Datastore; }
            if (!(Location == String.Empty)) { command += " -Location " + Location; }
            if (!(Name == String.Empty)) { command += " -Name " + Name; }
            if (!(Id == String.Empty)) { command += " -Id " + Id; }

            Script += command;

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

