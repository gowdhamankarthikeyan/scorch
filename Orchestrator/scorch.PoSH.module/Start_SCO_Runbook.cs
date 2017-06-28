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
    [Cmdlet(VerbsLifecycle.Start, "SCORunbook")]
    public class Start_SCO_Runbook : PSCmdlet
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
            ValueFromPipeline = true,
            ParameterSetName = "RunbookPath",
            Mandatory = true
        )]
        public string RunbookPath
        {
            get;
            set;
        }
        
        [Parameter(
            Position = 1,
            ValueFromPipeline = true,
            ParameterSetName = "RunbookGuid",
            Mandatory = true
        )]
        public Guid RunbookGuid
        {
            get;
            set;
        }

        [Parameter(
            Position = 1,
            ValueFromPipeline = true,
            ParameterSetName = "Runbook",
            Mandatory = true
        )]
        public Runbook Runbook
        {
            get;
            set;
        }

        private Hashtable _InputParameters = new Hashtable();
        [Parameter(
            Position = 2,
            ValueFromPipeline = true,
            Mandatory = false
        )]
        public Hashtable InputParameters
        {
            get { return _InputParameters; }
            set { _InputParameters = value; }
        }

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public SwitchParameter WaitForExit
        {
            get;
            set;
        }

        private int _RetryCount = 1;
        [Parameter(
            Position = 4,
            Mandatory = false
        )]
        public int RetryCount
        {
            get { return _RetryCount; }
            set { _RetryCount = value; }
        }

        private int _RetryDelay = 10;
        [Parameter(
            Position = 5,
            Mandatory = false
        )]
        public int RetryDelay
        {
            get { return _RetryDelay; }
            set { _RetryDelay = value; }
        }

        private NetworkCredential _alternateCredentials = null;
        [Parameter(
            Position = 6,
            ValueFromPipeline = true,
            Mandatory = false
        )]
        public NetworkCredential alternateCredentials
        {
            get { return _alternateCredentials; }
            set { _alternateCredentials = value; }
        }

        private String _runbookServer = string.Empty;
        [Parameter(
            Position = 7,
            ValueFromPipeline = false,
            Mandatory = false
        )]
        public String RunbookServer
        {
            get { return _runbookServer; }
            set { _runbookServer = value; }
        }
        
        #endregion

        private OrchestratorContext sco;
        protected override void BeginProcessing()
        {
            sco = setupOrchestratorConnection();
        }

        protected override void ProcessRecord()
        {
            String exceptionMessage = String.Empty;
            bool finished = false;

            int internalCounter = 0;
            while (internalCounter < RetryCount && !finished)
            {
                sco = setupOrchestratorConnection();
                OrchestratorInterop.SCOrchestrator.Job job;
                try
                {
                    switch (ParameterSetName)
                    {
                        case "Runbook":
                            if (RunbookServer.Equals(String.Empty))
                            {
                                job = SCOrch.startRunbookJob(sco, Runbook.Id, HashtableToDictionary(_InputParameters));
                            }
                            else
                            {
                                job = SCOrch.startRunbookJob(sco, Runbook.Id, RunbookServer, HashtableToDictionary(_InputParameters));
                            }
                            break;
                        case "RunbookGuid":
                            if (RunbookServer.Equals(String.Empty))
                            {
                                job = SCOrch.startRunbookJob(sco, RunbookGuid, HashtableToDictionary(_InputParameters));
                            }
                            else
                            {
                                job = SCOrch.startRunbookJob(sco, RunbookGuid, RunbookServer, HashtableToDictionary(_InputParameters));
                            }
                            
                            break;
                        default:
                        case "RunbookPath":
                            if (RunbookServer.Equals(String.Empty))
                            {
                                job = SCOrch.startRunbookJob(sco, RunbookPath, HashtableToDictionary(_InputParameters));
                            }
                            else
                            {
                                job = SCOrch.startRunbookJob(sco, RunbookPath, RunbookServer, HashtableToDictionary(_InputParameters));
                            }
                            break;
                    }
                    
                    if (WaitForExit.IsPresent)
                    {
                        pollForJobCompletion(sco, job.Id);
                        WriteObject(SCOrch.getJobDetails(sco, job.Id));
                    }
                    else { WriteObject(new JobInstance() { job = job }); }

                    finished = true;
                }
                catch (Exception e)
                {
                    exceptionMessage = String.Format("Summary: {0}\nDetails: {1}", e.Message, e.InnerException);
                    if (exceptionMessage.Contains("Summary: No Input Parameter on Runbook Found for Key:"))
                    {
                        throw new Exception(exceptionMessage);
                    }
                    else if (exceptionMessage.Equals("Must pass either -RunbookPath or -RunbookGUID"))
                    {
                        throw new Exception(exceptionMessage);
                    }
                    else if (exceptionMessage.Contains("Runbook Not Found:  "))
                    {
                        throw new Exception(exceptionMessage);
                    }
                    else if (exceptionMessage.Contains("The requested operation requires Publish permissions on the Runbook"))
                    {
                        throw new Exception(exceptionMessage);
                    }

                    internalCounter++;
                    Thread.Sleep(new TimeSpan(0, 0, RetryDelay));
                }
            }

            if (internalCounter >= RetryCount) { throw new Exception(exceptionMessage); }
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
        }

        private Dictionary<string, string> HashtableToDictionary(Hashtable table)
        {
            Dictionary<string, string> retTable = new Dictionary<string, string>();
            foreach (DictionaryEntry pair in table)
            {
                retTable.Add(pair.Key.ToString(), pair.Value.ToString());
            }
            return retTable;
        }
    }
}
