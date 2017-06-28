using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;

namespace VMWareIntegrationPack
{
    [Activity("Invoke VM Script")]
    class InvokeVMScript : IActivity
    {
        private ConnectionSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String virtualCenter = String.Empty;
        private String port = String.Empty;
        private String domain = String.Empty;

        private String ScriptText = String.Empty;
        private String VM = String.Empty;
        private String HostUser = String.Empty;
        private String HostPassword = String.Empty;
        private String GuestUser = String.Empty;
        private String GuestPassword = String.Empty;
        private String ToolsWaitSecs = String.Empty;
        private String ScriptType = String.Empty;

        [ActivityConfiguration]
        public ConnectionSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            string[] ScriptTypeOptions = new string[3];
            ScriptTypeOptions[0] = "PowerShell";
            ScriptTypeOptions[1] = "Bat";
            ScriptTypeOptions[2] = "Bash";

            designer.AddInput("ScriptText");
            designer.AddInput("VM");
            designer.AddInput("HostUser");
            designer.AddInput("HostPassword").PasswordProtect();
            designer.AddInput("GuestUser");
            designer.AddInput("GuestPassword").PasswordProtect();
            designer.AddInput("ToolsWaitSecs").NotRequired().WithDefaultValue("20");
            designer.AddInput("ScriptType").WithListBrowser(ScriptTypeOptions).WithDefaultValue("PowerShell");
            designer.AddOutput("ScriptResult");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            port = settings.Port;
            virtualCenter = settings.VirtualCenter;

            ScriptText = request.Inputs["ScriptText"].AsString();
            VM = request.Inputs["VM"].AsString();
            VM = request.Inputs["HostUser"].AsString();
            VM = request.Inputs["HostPassword"].AsString();
            VM = request.Inputs["GuestUser"].AsString();
            VM = request.Inputs["GuestPassword"].AsString();
            VM = request.Inputs["ToolsWaitSecs"].AsString();
            VM = request.Inputs["ScriptType"].AsString();

            PSSnapInException warning = new PSSnapInException();
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out warning);
            runspace.Open();


            Pipeline pipeline = runspace.CreatePipeline();

            String Script = "Connect-VIServer -Server " + virtualCenter + " -Port " + port + " -User " + domain + "\\" + userName + " -Password " + password + "\n";

            String command = "Invoke-VMScript";

            if (!(ScriptText == String.Empty)) { command += " -ScriptText '" + ScriptText + "'"; }
            if (!(VM == String.Empty)) { command += " -VM " + VM; }
            if (!(HostUser == String.Empty)) { command += " -HostUser " + HostUser; }
            if (!(HostPassword == String.Empty)) { command += " -HostPassword " + HostPassword; }
            if (!(GuestUser == String.Empty)) { command += " -GuestUser " + GuestUser; }
            if (!(ToolsWaitSecs == String.Empty)) { command += " -ToolsWaitSecs " + ToolsWaitSecs; }
            if (!(ScriptType == String.Empty)) { command += " -ScriptType " + ScriptType; }
            
            Script += command + " -confirm:$False";

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

            response.Publish("ScriptResult", results.ToString());
        }
    }
}

