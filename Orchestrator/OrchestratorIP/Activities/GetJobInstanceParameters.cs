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
    [Activity("Get Job Instance Parameters")]
    public class GetJobInstanceParameters : IActivity
    {
        private int inputParamNumber = 0;
        private int outputParamNumber = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Job Id");

            designer.AddCorellatedData(typeof(JobParameterArray));
            designer.AddOutput("Number of Input Parameters");
            designer.AddOutput("Number of Output Parameters");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String JobId = request.Inputs["Job Id"].AsString();

            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            JobInstance jobInstance = SCOrch.getJobDetails(sco, new Guid(JobId));

            response.WithFiltering().PublishRange(parseResults(jobInstance));
            response.Publish("Number of Input Parameters", inputParamNumber);
            response.Publish("Number of Output Parameters", outputParamNumber);
        }

        private IEnumerable<JobParameterArray> parseResults(JobInstance job)
        {
            foreach (String inputParamName in job.InputParameters.Keys)
            {
                inputParamNumber = inputParamNumber + 1;
                yield return new JobParameterArray("In", job.InputParameters[inputParamName], inputParamName);
            }
            foreach (String OutputParamName in job.OutputParameters.Keys)
            {
                outputParamNumber = outputParamNumber + 1;
                yield return new JobParameterArray("Out", job.OutputParameters[OutputParamName], OutputParamName);
            }
        }
    }
}
