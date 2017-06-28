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
    [Activity("Get All Monitor Runbooks")]
    public class GetAllMonitorRunbooks: IActivity
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
            String JobStatus = request.Inputs["Job Status"].AsString();

            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            Runbook[] RunbookArray = SCOrch.getMonitorRunbook(sco, false);

            response.WithFiltering().PublishRange(parseResults(RunbookArray));
        }

        private IEnumerable<RunbookInst> parseResults(Runbook[] rArray)
        {
            foreach (Runbook r in rArray)
            {
                yield return new RunbookInst(r);
            }
        }
    }
}
