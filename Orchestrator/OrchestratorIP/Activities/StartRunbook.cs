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
    [Activity("Start Runbook")]
    public class StartRunbook : IActivity
    {
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            string[] RunbookServerNames = new string[0];
            
            try
            {
                OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

                sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
                sco.MergeOption = MergeOption.OverwriteChanges;

                RunbookServerInst[] allRunbookServers = SCOrch.getAllRunbookServer(sco, false);

                RunbookServerNames = new string[allRunbookServers.Count()];
            
                int b = 0;
                foreach (RunbookServerInst r in allRunbookServers)
                {
                    try
                    {
                        RunbookServerNames[b] = r.server.Name;
                        b++;
                    }
                    catch { }
                }

            }
            catch { }

            designer.AddInput("Runbook Identifier").WithDefaultValue(@"\ParentFolder\ContainingFolder\Runbook Name");
            designer.AddInput("Identifier Type").WithDefaultValue(@"Runbook Path").WithListBrowser(new string[] { "Runbook Path", "Runbook GUID" });
            designer.AddInput("Wait for Exit").WithDefaultValue("True").WithListBrowser(new string[] { "True", "False" });
            if (RunbookServerNames.Length > 0)
            {
                designer.AddInput("Runbook Server Name").WithDefaultValue(RunbookServerNames[0]).WithListBrowser(RunbookServerNames).NotRequired();
            }
            else
            {
                designer.AddInput("Runbook Server Name").NotRequired();
            }
            designer.AddInput("Alternate User Name").NotRequired();
            designer.AddInput("Alternate User Domain").NotRequired();
            
            designer.AddInput("Password").NotRequired();


            for (int i = 0; i < Credentials.MaxInputParameters; i++)
            {
                if (i < 10)
                {
                    designer.AddInput("0" + i.ToString() + "_ParameterName").NotRequired(); 
                    designer.AddInput("0" + i.ToString() + "_ParameterValue").NotRequired();
                }
                else
                {
                    designer.AddInput(i.ToString() + "_ParameterName").NotRequired();
                    designer.AddInput(i.ToString() + "_ParameterValue").NotRequired();
                }
            }

            designer.AddCorellatedData(typeof(JobDetails));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String RunbookIdentifier = request.Inputs["Runbook Identifier"].AsString();
            String IdentifierType = request.Inputs["Identifier Type"].AsString();
            String WaitForExit = request.Inputs["Wait For Exit"].AsString();

            String RunbookServerName = String.Empty;

            String altUserName = String.Empty;
            String altUserDomain = String.Empty;
            String altPassword = String.Empty;

            if (request.Inputs.Contains("Alternate User Name") && request.Inputs.Contains("Alternate User Domain") && request.Inputs.Contains("Password"))
            {
                altPassword = request.Inputs["Password"].AsString();
                altUserDomain = request.Inputs["Alternate User Domain"].AsString();
                altUserName = request.Inputs["Alternate User Name"].AsString();
            }

            if (request.Inputs.Contains("Runbook Server Name"))
            {
                RunbookServerName = request.Inputs["Runbook Server Name"].AsString();
            }

            Dictionary<string, string> ParameterList = new Dictionary<string, string>();

            for (int i = 0; i < Credentials.MaxInputParameters; i++)
            {
                if (i < 10)
                {
                    if(request.Inputs.Contains("0" + i.ToString() + "_ParameterName") && request.Inputs.Contains("0" + i.ToString() + "_ParameterValue"))
                    {
                        string paramName = request.Inputs["0" + i.ToString() + "_ParameterName"].AsString();
                        string paramValue = request.Inputs["0" + i.ToString() + "_ParameterValue"].AsString();

                        ParameterList.Add(paramName, paramValue);
                    }
                }
                else
                {
                    if (request.Inputs.Contains(i.ToString() + "_ParameterName") && request.Inputs.Contains(i.ToString() + "_ParameterValue"))
                    {
                        string paramName = request.Inputs["0" + i.ToString() + "_ParameterName"].AsString();
                        string paramValue = request.Inputs["0" + i.ToString() + "_ParameterValue"].AsString();

                        ParameterList.Add(paramName, paramValue);
                    }
                }
            }

            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            if(altUserName.Equals(String.Empty))
            {
                sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            }
            else
            {
                sco.Credentials = new NetworkCredential(altUserName, altPassword, altUserDomain);
            }
            sco.MergeOption = MergeOption.OverwriteChanges;

            Guid jobInstanceGUID = Guid.NewGuid();
            if (RunbookServerName.Equals(String.Empty))
            {
                switch(IdentifierType.ToUpper())
                {
                    case "RUNBOOK PATH":
                        jobInstanceGUID = ((Job)SCOrch.startRunbookJob(sco, RunbookIdentifier, ParameterList)).Id;
                        break;
                    case "RUNBOOK GUID":
                        Guid runbookGUID = new Guid(RunbookIdentifier);
                        jobInstanceGUID =((Job) SCOrch.startRunbookJob(sco, runbookGUID, ParameterList)).Id;
                        break;
                    default:
                        jobInstanceGUID = ((Job)SCOrch.startRunbookJob(sco, RunbookIdentifier, ParameterList)).Id;
                        break;
                }
                
            }
            else
            {
                switch (IdentifierType.ToUpper())
                {
                    case "RUNBOOK PATH":
                        jobInstanceGUID = ((Job)SCOrch.startRunbookJob(sco, RunbookIdentifier, RunbookServerName, ParameterList)).Id;
                        break;
                    case "RUNBOOK GUID":
                        Guid runbookGUID = new Guid(RunbookIdentifier);
                        jobInstanceGUID = ((Job)SCOrch.startRunbookJob(sco, runbookGUID, RunbookServerName, ParameterList)).Id;
                        break;
                    default:
                        jobInstanceGUID = ((Job)SCOrch.startRunbookJob(sco, RunbookIdentifier, RunbookServerName, ParameterList)).Id;
                        break;
                }
            }

            if(WaitForExit.Equals("True"))
            {
                JobInstance ji = SCOrch.getJobDetails(sco, jobInstanceGUID);
                while(!(ji.job.Status.Equals("Completed") || ji.job.Status.Equals("Canceled") || ji.job.Status.Equals("Failed")))
                {
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                    ji = SCOrch.getJobDetails(sco, jobInstanceGUID);
                }
            }

            JobInstance jobInstance = SCOrch.getJobDetails(sco, jobInstanceGUID);

            response.WithFiltering().PublishRange(parseResults(jobInstance));
        }

        private IEnumerable<JobDetails> parseResults(JobInstance job)
        {
            for (int i = 0; i < 1; i++)
            {
                yield return new JobDetails(job);
            }
        }
    }
}
