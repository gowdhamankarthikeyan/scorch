using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop.Data_Class;
using OrchestratorInterop.SCOrchestrator;
using OrchestratorInterop;
using OrchestratorIP.ConfigurationObjects;
using OrchestratorIP.ReturnTypes;
using System.Globalization;
using System.Net;
using System.Data.Services.Client;
namespace OrchestratorIP.Activities
{
    [Activity("Get All Runbook Servers")]
    public class GetAllRunbookServers : IActivity
    {
        int numberOfRunbookServer = 0;
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddCorellatedData(typeof(RunbookServerInstance));
            designer.AddOutput("Number Of Runbook Servers");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            RunbookServerInst[] RunbookServerArray = SCOrch.getAllRunbookServer(sco,true);

            response.WithFiltering().PublishRange(parseResults(RunbookServerArray));
            response.Publish("Number Of Runbook Servers", numberOfRunbookServer);
        }

        private IEnumerable<RunbookServerInstance> parseResults(RunbookServerInst[] RunbookServerArray)
        {
            foreach (RunbookServerInst RServer in RunbookServerArray)
            {
                numberOfRunbookServer = numberOfRunbookServer + 1;
                yield return new RunbookServerInstance(RServer);
            }
        }
    }
}