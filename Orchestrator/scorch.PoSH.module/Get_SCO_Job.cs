using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Net;
using System.Data.Services.Client;
using System.Management.Automation;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;
using OrchestratorInterop.SCOrchestrator;
using System.Threading;

namespace scorch.PoSH.module
{
    [Cmdlet(VerbsCommon.Get, "SCOJob")]
    public class Get_SCO_Job : PSCmdlet
    {
        #region Parameters

        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string webserverURL
        {
            get;
            set;
        }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = "SingleJob",
            ValueFromPipeline = true
        )]
        public JobInstance job
        {
            get;
            set;
        }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = "AllJobs"
        )]
        public SwitchParameter allJobs
        {
            get;
            set;
        }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = "Runbook",
            ValueFromPipeline = true
        )]
        public Runbook runbook
        {
            get;
            set;
        }
        
        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = "RunbookServer",
            ValueFromPipeline = true
        )]
        public RunbookServer runbookServer
        {
            get;
            set;
        }

        private string _jobStatus = string.Empty;
        [Parameter(
            Position = 2,
            Mandatory = false,
            ParameterSetName = "AllJobs"
        )]
        [Parameter(
            Position = 2,
            Mandatory = false,
            ParameterSetName = "Runbook"
        )]
        [Parameter(
            Position = 2,
            Mandatory = false,
            ParameterSetName = "RunbookServer"
        )]
        public string jobStatus
        {
            get { return _jobStatus; }
            set { _jobStatus = value; }
        }

        [Parameter(
            Position = 2,
            ParameterSetName = "SingleJob",
            Mandatory = false
        )]
        [Parameter(
            Position = 3,
            ParameterSetName = "AllJobs",
            Mandatory = false
        )]
        [Parameter(
            Position = 3,
            Mandatory = false,
            ParameterSetName = "Runbook"
        )]
        [Parameter(
            Position = 3,
            Mandatory = false,
            ParameterSetName = "RunbookServer"
        )]
        public SwitchParameter LoadJobDetails
        {
            get;
            set;
        }
        NetworkCredential _alternateCredentials = null;
        [Parameter(
            Position = 3,
            ParameterSetName = "SingleJob",
            ValueFromPipeline = true,
            Mandatory = false
        )]
        [Parameter(
            Position = 4,
            ParameterSetName = "AllJobs",
            ValueFromPipeline = true,
            Mandatory = false
        )]
        [Parameter(
            Position = 4,
            Mandatory = false,
            ParameterSetName = "Runbook",
            ValueFromPipeline = true
        )]
        [Parameter(
            Position = 4,
            Mandatory = false,
            ParameterSetName = "RunbookServer",
            ValueFromPipeline = true
        )]
        public NetworkCredential alternateCredentials
        {
            get { return _alternateCredentials; }
            set { _alternateCredentials = value; }
        }
        #endregion

        private OrchestratorContext sco;

        protected override void BeginProcessing()
        {
            sco = setupOrchestratorConnection();
        }

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "SingleJob":
                    WriteObject(SCOrch.getJobDetails(sco, job.job.Id));
                    break;
                case "Runbook":
                    if (string.IsNullOrEmpty(jobStatus)) { foreach (JobInstance ji in SCOrch.getRunbookJobInstances(sco, runbook.Id, LoadJobDetails.IsPresent)) { WriteObject(ji); } }
                    else { foreach (JobInstance ji in SCOrch.getRunbookJobInstances(sco, runbook.Id, jobStatus, LoadJobDetails.IsPresent)) { WriteObject(ji); } }
                    break;
                case "AllJobs":
                    if (string.IsNullOrEmpty(jobStatus)) { foreach (JobInstance ji in SCOrch.getAllJobs(sco, LoadJobDetails.IsPresent)) { WriteObject(ji); } }
                    else { foreach (JobInstance ji in SCOrch.getAllJobs(sco, jobStatus, LoadJobDetails.IsPresent)) { WriteObject(ji); } }
                    break;
                case "RunbookServer":
                    if (string.IsNullOrEmpty(jobStatus)) { foreach (JobInstance ji in SCOrch.getAllJobInstancesOnRunbookServer(sco, runbookServer, LoadJobDetails.IsPresent)) { WriteObject(ji); } }
                    else { foreach (JobInstance ji in SCOrch.getAllJobInstancesOnRunbookServer(sco, runbookServer, jobStatus, LoadJobDetails.IsPresent)) { WriteObject(ji); } }
                    break;
                default:
                    break;
            }
        }

        private OrchestratorContext setupOrchestratorConnection()
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(webserverURL));

            if (alternateCredentials == null) { sco.Credentials = CredentialCache.DefaultCredentials; }
            else { sco.Credentials = alternateCredentials; }
            sco.MergeOption = MergeOption.OverwriteChanges;

            return sco;
        }

        private void pollForJobCompletion(OrchestratorContext sco, Guid jobID)
        {
            while (!SCOrch.getJobDetails(sco, jobID).job.Status.Equals("Completed"))
            {
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
            }
            JobInstance j = SCOrch.getJobDetails(sco, jobID);

            WriteObject(j);
        }
    }
}
