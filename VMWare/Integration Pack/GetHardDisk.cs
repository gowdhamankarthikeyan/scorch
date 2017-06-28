using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Get Hard Disk")]
    class GetHardDisk : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String Path = String.Empty;
        private String DiskType = String.Empty;
        private String Datastore = String.Empty;
        private String DatastorePath = String.Empty;
        private String VM = String.Empty;
        private String Template = String.Empty;
        private String Snapshot = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            string[] diskTypeOptions = new string[4];
            diskTypeOptions[0] = "rawVirtual";
            diskTypeOptions[1] = "rawPhysical";
            diskTypeOptions[2] = "flat";
            diskTypeOptions[3] = "unknown";

            designer.AddInput("Path").NotRequired();
            designer.AddInput("DiskType").NotRequired().WithListBrowser(diskTypeOptions);
            designer.AddInput("Datastore").NotRequired();
            designer.AddInput("DatastorePath").NotRequired();
            designer.AddInput("VM").NotRequired();
            designer.AddInput("Template").NotRequired();
            designer.AddInput("Snapshot").NotRequired();
           
            designer.AddCorellatedData(typeof(harddisk));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            Path = request.Inputs["Path"].AsString();
            DiskType = request.Inputs["DiskType"].AsString();
            Datastore = request.Inputs["Datastore"].AsString();
            DatastorePath = request.Inputs["DatastorePath"].AsString();
            VM = request.Inputs["VM"].AsString();
            Template = request.Inputs["Template"].AsString();
            Datastore = request.Inputs["Datastore"].AsString();
            Snapshot = request.Inputs["Snapshot"].AsString();

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

            String command = "Get-HardDisk";

            if (!(Path == String.Empty)) { command += " -Path \"" + Path + "\""; }
            if (!(DiskType == String.Empty)) { command += " -DiskType \"" + DiskType + "\""; }
            if (!(Datastore == String.Empty)) { command += " -Datastore \"" + Datastore + "\""; }
            if (!(DatastorePath == String.Empty)) { command += " -DatastorePath \"" + DatastorePath + "\""; }
            if (!(VM == String.Empty)) { command += " -VM \"" + VM + "\""; }
            if (!(Template == String.Empty)) { command += " -Template \"" + Template + "\""; }
            if (!(Snapshot == String.Empty)) { command += " -Snapshot \"" + Snapshot + "\""; }

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
                if (obj.BaseObject.GetType().ToString().Contains("DiskImpl"))
                {
                    yield return new harddisk(obj);
                }
            }
            runspace.Close();
        }
    }
}

