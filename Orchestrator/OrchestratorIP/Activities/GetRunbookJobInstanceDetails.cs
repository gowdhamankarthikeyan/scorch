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
    [Activity("Get Runbook Job Instance Details")]
    public class GetRunbookJobInstanceDetails : IActivity
    {
        private int numberOfJobs = 0;
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Runbook Identifier").WithDefaultValue(@"\ParentFolder\ContainingFolder\Runbook Name");
            designer.AddInput("Identifier Type").WithDefaultValue(@"Runbook Path").WithListBrowser(new string[] { "Runbook Path", "Runbook GUID" });
            designer.AddInput("Job Status").NotRequired().WithDefaultValue("Running").WithListBrowser(new string[] { "Running", "Pending", "Completed", "Canceled" });
            designer.AddCorellatedData(typeof(JobDetails));
            designer.AddOutput("Number of Jobs");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String RunbookIdentifier = request.Inputs["Runbook Identifier"].AsString();
            String IdentifierType = request.Inputs["Identifier Type"].AsString();
            String jobStatus = String.Empty;
            if(request.Inputs.Contains("Job Status")) { jobStatus = request.Inputs["Job Status"].AsString(); }
            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            JobInstance[] jobInstance = new JobInstance[0];

            switch (IdentifierType)
            {
                case "Runbook Path":
                    if (jobStatus.Equals(String.Empty)) { jobInstance = SCOrch.getRunbookJobInstances(sco, RunbookIdentifier); }
                    else { jobInstance = SCOrch.getRunbookJobInstances(sco, RunbookIdentifier, jobStatus); }
                    break;
                case "Runbook GUID":
                    Guid runbookGUID = new Guid(RunbookIdentifier);
                    if (jobStatus.Equals(String.Empty)) { jobInstance = SCOrch.getRunbookJobInstances(sco, runbookGUID); }
                    else { jobInstance = SCOrch.getRunbookJobInstances(sco, runbookGUID, jobStatus); }
                    break;
                default:
                    response.LogErrorMessage("Must choose Runbook Path or Runbook GUID as an Identifier Type");
                    break;
            }
            response.WithFiltering().PublishRange(parseResults(jobInstance));
            response.Publish("Number of Jobs", numberOfJobs);
        }

        private IEnumerable<JobDetails> parseResults(JobInstance[] jobArray)
        {
            foreach (JobInstance job in jobArray)
            {
                numberOfJobs = numberOfJobs + 1;
                yield return new JobDetails(job);
            }
        }
    }
}
