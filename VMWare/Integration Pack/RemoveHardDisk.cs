using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Remove Hard Disk")]
    class RemoveHardDisk : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String VMName = String.Empty;
        private String DiskName = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("VMName");
            designer.AddInput("Disk Name");
            designer.AddCorellatedData(typeof(harddisk));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VMName = request.Inputs["VMName"].AsString();
            DiskName = request.Inputs["Disk Name"].AsString();
            
            response.WithFiltering().PublishRange(runCommand());
        }

        private IEnumerable<harddisk> runCommand()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();

            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";
            String preCommand = "Get-HardDisk -VM + \"" + VMName + "\" | ? {$_.Name -eq \"" + DiskName + "\"}";

            String command = "Remove-HardDisk";

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
                if (obj.BaseObject.GetType().ToString().Contains("DiskImpl"))
                {
                    yield return new harddisk(obj);
                }
            }
            runspace.Close();
        }
    }
}

