using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Get Datastore")]
    class GetDatastore : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String VMHost = String.Empty;
        private String VM = String.Empty;
        private String Entity = String.Empty;                
        private String Name = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("VMHost").NotRequired();
            designer.AddInput("VM").NotRequired();
            designer.AddInput("Entity").NotRequired();
            designer.AddInput("Name").NotRequired();
            designer.AddCorellatedData(typeof(datastore));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VMHost = request.Inputs["VMHost"].AsString();
            VM = request.Inputs["VM"].AsString();
            Entity = request.Inputs["Entity"].AsString();
            Name = request.Inputs["Name"].AsString();

            response.WithFiltering().PublishRange(runCommand());
        }

        private IEnumerable<datastore> runCommand()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();


            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "Get-Datastore";

            if (!(VMHost == String.Empty)) { command += " -VMHost " + VMHost; }
            if (!(VM == String.Empty)) { command += " -VM " + VM; }
            if (!(Entity == String.Empty)) { command += " -Entity " + Entity; }
            if (!(Name == String.Empty)) { command += " -Name " + Name; }

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
                if (obj.BaseObject.GetType().ToString().Contains("DatastoreImpl"))
                {
                    String id = obj.Members["id"].Value.ToString();
                    String name = obj.Members["name"].Value.ToString();
                    String capacityMB = obj.Members["capacityMB"].Value.ToString();
                    String FreeSpaceMB = obj.Members["FreeSpaceMB"].Value.ToString();
                    String ParentFolderId = obj.Members["ParentFolderId"].Value.ToString();
                    String DatacenterId = obj.Members["DatacenterId"].Value.ToString();

                    yield return new datastore(capacityMB, FreeSpaceMB, ParentFolderId, DatacenterId, id, name);
                }
            }

            runspace.Close();
        }
    }
}

