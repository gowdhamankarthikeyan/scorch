using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Get Resource Pool")]
    class GetResourcePool : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String virtualCenter = String.Empty;
        private String VM = String.Empty;
        private String Location = String.Empty;
        private String Name = String.Empty;
        private String Id = String.Empty;
        private String port = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("VM").NotRequired();
            designer.AddInput("Location").NotRequired();
            designer.AddInput("Name").NotRequired();
            designer.AddInput("Id").NotRequired();
            designer.AddCorellatedData(typeof(ResourcePool));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VM = request.Inputs["VM"].AsString();
            Location = request.Inputs["Location"].AsString();
            Name = request.Inputs["Name"].AsString();
            Id = request.Inputs["Id"].AsString();            

            response.WithFiltering().PublishRange(getResourcePool());
        }

        private IEnumerable<ResourcePool> getResourcePool()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();


            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "Get-ResourcePool";

            if (!(VM == String.Empty)) { command += " -VM " + VM; }
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
                if (obj.BaseObject.GetType().ToString().Contains("VMware.VimAutomation.ViCore.Impl.V1.Inventory.ResourcePoolImpl"))
                {
                    String NumCpuShares = obj.Members["NumCpuShares"].Value.ToString();
                    String CpuReservationMHz = obj.Members["CpuReservationMHz"].Value.ToString();
                    String CpuExpandableReservation = obj.Members["CpuExpandableReservation"].Value.ToString();
                    String CpuLimitMHz = obj.Members["CpuLimitMHz"].Value.ToString();
                    String NumMemShares = obj.Members["NumMemShares"].Value.ToString();
                    String MemReservationMB = obj.Members["MemReservationMB"].Value.ToString();
                    String MemExpandableReservation = obj.Members["MemExpandableReservation"].Value.ToString();
                    String MemLimitMB = obj.Members["MemLimitMB"].Value.ToString();
                    String id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    yield return new ResourcePool(NumCpuShares,CpuReservationMHz,CpuExpandableReservation,CpuLimitMHz,NumMemShares,MemReservationMB,MemExpandableReservation,MemLimitMB,id,name);
                }
            }

            runspace.Close();
        }
    }
}

