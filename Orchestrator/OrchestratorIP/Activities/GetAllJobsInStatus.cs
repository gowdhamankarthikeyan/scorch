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
    [Activity("Get All Jobs In Status")]
    public class GetAllJobsInStatus : IActivity
    {
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {

            designer.AddInput("Job Stats").WithDefaultValue(@"Running").WithListBrowser(new string[] { "Running", "Pending", "Completed", "Canceled"});
            
            designer.AddCorellatedData(typeof(JobDetails));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String JobStatus = request.Inputs["Job Status"].AsString();

            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            JobInstance[] jobInstance = SCOrch.getAllJobInstancesWithStatus(sco, JobStatus);

            response.WithFiltering().PublishRange(parseResults(jobInstance));
        }

        private IEnumerable<JobDetails> parseResults(JobInstance[] jobArray)
        {
            foreach (JobInstance job in jobArray)
            {
                yield return new JobDetails(job);
            }
        }
    }
}
