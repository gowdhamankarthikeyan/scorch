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
    [Activity("Get Job Instance Details")]
    public class GetJobInstanceDetails : IActivity
    {
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Job Id");

            designer.AddCorellatedData(typeof(JobDetails));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String JobId = request.Inputs["Job Id"].AsString();

            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            JobInstance jobInstance = SCOrch.getJobDetails(sco, new Guid(JobId));

            response.WithFiltering().PublishRange(parseResults(jobInstance));
        }

        private IEnumerable<JobDetails> parseResults(JobInstance job)
        {
            for(int i = 0 ; i < 1 ; i++)
            {
                yield return new JobDetails(job);
            }
        }
    }
}
