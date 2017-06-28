using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Get VMWare Host")]
    class GetVMHost : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String Datastore = String.Empty;
        private String State = String.Empty;
        private String Location = String.Empty;
        private String Name = String.Empty;
        private String Id = String.Empty;        
        private String VM = String.Empty;
        private String ResourcePool = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Datastore").NotRequired();
            designer.AddInput("State").NotRequired();
            designer.AddInput("Location").NotRequired();
            designer.AddInput("Name").NotRequired();
            designer.AddInput("Id").NotRequired();
            designer.AddInput("VM").NotRequired();
            designer.AddInput("ResourcePool").NotRequired();
            designer.AddCorellatedData(typeof(vmHost));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            Datastore = request.Inputs["Datastore"].AsString();
            State = request.Inputs["State"].AsString();
            Location = request.Inputs["Location"].AsString();
            Name = request.Inputs["Name"].AsString();
            Id = request.Inputs["Id"].AsString();
            VM = request.Inputs["VM"].AsString();
            ResourcePool = request.Inputs["ResourcePool"].AsString();

            response.WithFiltering().PublishRange(getVMHost());
        }

        private IEnumerable<vmHost> getVMHost()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();


            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String getVMHost = "Get-VMHost";

            if (!(Datastore == String.Empty)) { getVMHost += " -Datastore " + Datastore; }
            if (!(State == String.Empty)) { getVMHost += " -State " + State; }
            if (!(Location == String.Empty)) { getVMHost += " -Location " + Location; }
            if (!(Name == String.Empty)) { getVMHost += " -Name " + Name; }
            if (!(Id == String.Empty)) { getVMHost += " -Id " + Id; }
            if (!(VM == String.Empty)) { getVMHost += " -VM " + VM; }
            if (!(ResourcePool == String.Empty)) { getVMHost += " -ResourcePool " + ResourcePool; }

            Script += getVMHost;

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
                if (obj.BaseObject.GetType().ToString().Contains("VMware.VimAutomation.ViCore.Impl.V1.Inventory.VMHostImpl"))
                {
                    String ConnectionState = obj.Members["ConnectionState"].Value.ToString();
                    String PowerState = obj.Members["PowerState"].Value.ToString();
                    String isStandalone = obj.Members["isStandalone"].Value.ToString();
                    String Manufacturer = obj.Members["Manufacturer"].Value.ToString();
                    String Model = obj.Members["Model"].Value.ToString();
                    String NumCpu = obj.Members["NumCpu"].Value.ToString();
                    String MemoryTotalMB = obj.Members["MemoryTotalMB"].Value.ToString();
                    String MemoryUsageMB = obj.Members["MemoryUsageMB"].Value.ToString();
                    String ProcessorType = obj.Members["ProcessorType"].Value.ToString();
                    String HyperthreadingActive = obj.Members["HyperthreadingActive"].Value.ToString();
                    String TimeZone = obj.Members["TimeZone"].Value.ToString();
                    String Version = obj.Members["Version"].Value.ToString();
                    String Build = obj.Members["Build"].Value.ToString();
                    String id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    yield return new vmHost(ConnectionState,PowerState,isStandalone,Manufacturer,Model,NumCpu,MemoryTotalMB,MemoryUsageMB,ProcessorType,HyperthreadingActive,TimeZone,Version,Build,id,name);
                }
            }

            runspace.Close();
        }
    }
}

