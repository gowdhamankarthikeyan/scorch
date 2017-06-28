using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Get Cluster")]
    class GetCluster : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String virtualCenter = String.Empty;
        private String VM = String.Empty;
        private String VMHost = String.Empty;
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
            designer.AddInput("VMHost").NotRequired();
            designer.AddInput("Location").NotRequired();
            designer.AddInput("Name").NotRequired();
            designer.AddInput("Id").NotRequired();
            designer.AddInput("Server").NotRequired();
            designer.AddCorellatedData(typeof(cluster));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            VM = request.Inputs["VM"].AsString();
            VMHost = request.Inputs["VMHost"].AsString();
            Location = request.Inputs["Location"].AsString();
            Name = request.Inputs["Name"].AsString();
            Id = request.Inputs["Id"].AsString();

            response.WithFiltering().PublishRange(getCluster());
        }

        private IEnumerable<cluster> getCluster()
        {
            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();


            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "Get-Cluster";

            if (!(VM == String.Empty)) { command += " -VM " + VM; }
            if (!(VMHost == String.Empty)) { command += " -VMHost " + VMHost; }
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
                if(obj.BaseObject.GetType().ToString().Contains("ClusterImpl"))
                {
                    String HAEnabled = obj.Members["HAEnabled"].Value.ToString();
                    String HAAdmissionControlEnabled = obj.Members["HAAdmissionControlEnabled"].Value.ToString();
                    String HAFailoverLevel = obj.Members["HAFailoverLevel"].Value.ToString();
                    String HARestartPriority = obj.Members["HARestartPriority"].Value.ToString();
                    String HAIsolationResponse = obj.Members["HAIsolationResponse"].Value.ToString();
                    String VMSwapFilePolicy = obj.Members["VMSwapFilePolicy"].Value.ToString();
                    String DrsEnabled = obj.Members["DrsEnabled"].Value.ToString();
                    String DrsMode = obj.Members["DrsMode"].Value.ToString();
                    String DrsAutomationLevel = obj.Members["DrsAutomationLevel"].Value.ToString();
                    String id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    yield return new cluster(HAEnabled,HAAdmissionControlEnabled,HAFailoverLevel,HARestartPriority,HAIsolationResponse,VMSwapFilePolicy,DrsEnabled,DrsMode,DrsAutomationLevel,id,name);
                }
            }

            runspace.Close();
        }        
    }
}

