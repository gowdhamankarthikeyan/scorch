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
    [Activity("Get All Running Runbooks")]
    public class GetAllRunningRunbooks : IActivity
    {
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddCorellatedData(typeof(RunbookInst));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            Runbook[] runbookArray = SCOrch.getAllRunningRunbooks(sco);

            response.WithFiltering().PublishRange(parseResults(runbookArray));
        }

        private IEnumerable<RunbookInst> parseResults(Runbook[] runbookArray)
        {
            foreach (Runbook runbook in runbookArray)
            {
                yield return new RunbookInst(runbook);
            }
        }
    }
}
