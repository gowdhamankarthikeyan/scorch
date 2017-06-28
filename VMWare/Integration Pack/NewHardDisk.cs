using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("New Hard Disk")]
    class NewHardDisk : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String Persistence = String.Empty;
        private String DiskType = String.Empty;
        private String CapacityKB = String.Empty;
        private String Split = String.Empty;
        private String ThinProvisioned = String.Empty;
        private String DeviceName = String.Empty;
        private String Datastore = String.Empty;
        private String VM = String.Empty;
        private String DiskPath = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            string[] persistenceOptions = new string[5];
            persistenceOptions[0] = "Persistent";
            persistenceOptions[1] = "NonPersistent";
            persistenceOptions[2] = "IndependentPersistent";
            persistenceOptions[3] = "IndependentNonPersistent";
            persistenceOptions[4] = "Undoable";

            string[] diskTypeOptions = new string[4];
            diskTypeOptions[0] = "rawVirtual";
            diskTypeOptions[1] = "rawPhysical";
            diskTypeOptions[2] = "flat";
            diskTypeOptions[3] = "unknown";

            string[] splitOptions = new string[2];
            splitOptions[0] = "true";
            splitOptions[1] = "false";

            string[] thinProvisionedOptions = new string[2];
            thinProvisionedOptions[0] = "true";
            thinProvisionedOptions[1] = "false";

            designer.AddInput("Persistence").NotRequired().WithListBrowser(persistenceOptions);
            designer.AddInput("DiskType").NotRequired().WithListBrowser(diskTypeOptions);
            designer.AddInput("CapacityKB");
            designer.AddInput("Split").NotRequired().WithListBrowser(splitOptions);
            designer.AddInput("ThinProvisioned").NotRequired().WithListBrowser(thinProvisionedOptions);
            designer.AddInput("DeviceName").NotRequired();
            designer.AddInput("Datastore").NotRequired();
            designer.AddInput("VM");
            designer.AddInput("DiskPath").NotRequired();
            designer.AddCorellatedData(typeof(harddisk));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            Persistence = request.Inputs["Persistence"].AsString();
            DiskType = request.Inputs["DiskType"].AsString();
            CapacityKB = request.Inputs["CapacityKB"].AsString();
            Split = request.Inputs["Split"].AsString();
            ThinProvisioned = request.Inputs["ThinProvisioned"].AsString();
            DeviceName = request.Inputs["DeviceName"].AsString();
            Datastore = request.Inputs["Datastore"].AsString();
            VM = request.Inputs["VM"].AsString();
            DiskPath = request.Inputs["DiskPath"].AsString();

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

            String command = "New-HardDisk";

            if (!(Persistence == String.Empty)) { command += " -Persistence \"" + Persistence + "\""; }
            if (!(DiskType == String.Empty)) { command += " -DiskType \"" + DiskType + "\""; }
            if (!(CapacityKB == String.Empty)) { command += " -CapacityKB \"" + CapacityKB + "\""; }
            if (Split.Equals("true")) { command += " -Split"; }
            if (ThinProvisioned.Equals("true")) { command += " -ThinProvisioned"; }
            if (!(DeviceName == String.Empty)) { command += " -DeviceName \"" + DeviceName + "\""; }
            if (!(Datastore == String.Empty)) { command += " -Datastore \"" + Datastore + "\""; }
            if (!(VM == String.Empty)) { command += " -VM \"" + VM + "\""; }
            if (!(DiskPath == String.Empty)) { command += " -DiskPath \"" + DiskPath + "\""; }

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
                if (obj.BaseObject.GetType().ToString().Contains("DiskImpl"))
                {
                    yield return new harddisk(obj);
                }
            }
            runspace.Close();
        }
    }
}

