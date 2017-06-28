using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Suspend Virtual Machine")]
    class SuspendVM : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String VM = String.Empty;
        private String GuestCommand = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            string[] guestOption = new string[2];
            guestOption[0] = "true";
            guestOption[1] = "false";
            designer.AddInput("VM");
            designer.AddInput("Guest Suspend").WithListBrowser(guestOption);

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
            GuestCommand = request.Inputs["Guest Suspend"].AsString();
            response.WithFiltering().PublishRange(startVM());
        }

        private IEnumerable<vm> startVM()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "Suspend-VM";
            if (GuestCommand.Equals("true")) { command = "Suspend-VMGuest"; }

            if (!(VM == String.Empty)) { command += " -VM " + VM; }

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

