using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Set Hard Disk")]
    class SetHardDisk : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String VMName = String.Empty;
        private String CapacityKB = String.Empty;
        private String Persistence = String.Empty;
        private String Datastore = String.Empty;
        private String StorageFormat = String.Empty;
        private String HostUser = String.Empty;
        private String HostPassword = String.Empty;
        private String GuestUser = String.Empty;
        private String GuestPassword = String.Empty;
        private String ToolsWaitSecs = String.Empty;
        private String HelperVM = String.Empty;
        private String Partition = String.Empty;
        private String Inflate = String.Empty;

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

            string[] storageFormatOptions = new string[2];
            storageFormatOptions[0] = "Thin";
            storageFormatOptions[1] = "Thick";

            string[] inflateOptions = new string[2];
            inflateOptions[0] = "true";
            inflateOptions[1] = "false";

            designer.AddInput("Virtual Machine Name");
            designer.AddInput("CapacityKB");
            designer.AddInput("Persistence").NotRequired().WithListBrowser(persistenceOptions);
            designer.AddInput("Datastore").NotRequired();
            designer.AddInput("StorageFormat").NotRequired().WithListBrowser(storageFormatOptions);
            designer.AddInput("HostUser").NotRequired();
            designer.AddInput("HostPassword").NotRequired().PasswordProtect();
            designer.AddInput("GuestUser").NotRequired();
            designer.AddInput("GuestPassword").NotRequired().PasswordProtect();
            designer.AddInput("ToolsWaitSecs").NotRequired();
            designer.AddInput("HelperVM").NotRequired();
            designer.AddInput("Inflate").NotRequired().WithListBrowser(inflateOptions);
            
            designer.AddCorellatedData(typeof(harddisk));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VMName = request.Inputs["Virtual Machine Name"].AsString();
            CapacityKB = request.Inputs["CapacityKB"].AsString();
            Persistence = request.Inputs["Persistence"].AsString();
            Datastore = request.Inputs["Datastore"].AsString();
            StorageFormat = request.Inputs["StorageFormat"].AsString();
            HostUser = request.Inputs["HostUser"].AsString();
            HostPassword = request.Inputs["HostPassword"].AsString();
            GuestUser = request.Inputs["GuestUser"].AsString();
            GuestPassword = request.Inputs["GuestPassword"].AsString();
            ToolsWaitSecs = request.Inputs["ToolsWaitSecs"].AsString();
            HelperVM = request.Inputs["HelperVM"].AsString();
            Inflate = request.Inputs["Inflate"].AsString();

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

            String preCommand = "Get-HardDisk";

            if (!(VMName == String.Empty)) { preCommand += " -VM \"" + VMName + "\""; }

            String command = "Set-HardDisk";

            if (!(CapacityKB == String.Empty)) { command += " -CapacityKB \"" + CapacityKB + "\""; }
            if (!(Persistence == String.Empty)) { command += " -Persistence \"" + Persistence + "\""; }
            if (!(Datastore == String.Empty)) { command += " -Datastore \"" + Datastore + "\""; }
            if (!(StorageFormat == String.Empty)) { command += " -StorageFormat \"" + StorageFormat + "\""; }
            if (!(HostUser == String.Empty)) { command += " -HostUser \"" + HostUser + "\""; }
            if (!(HostPassword == String.Empty)) { command += " -HostPassword \"" + HostPassword + "\""; }
            if (!(GuestUser == String.Empty)) { command += " -GuestUser \"" + GuestUser + "\""; }
            if (!(GuestPassword == String.Empty)) { command += " -GuestPassword \"" + GuestPassword + "\""; }
            if (!(ToolsWaitSecs == String.Empty)) { command += " -ToolsWaitSecs \"" + ToolsWaitSecs + "\""; }
            if (!(HelperVM == String.Empty)) { command += " -HelperVM \"" + HelperVM + "\""; }
            if (!(Partition == String.Empty)) { command += " -Partition \"" + Partition + "\""; }
            if (Inflate.Equals("true")) { command += " -Inflate"; }

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

