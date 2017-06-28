using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Net;
using System.Data.Services.Client;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;
using OrchestratorInterop.SCOrchestrator;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace commandLine
{
    class Program
    {
        private static string TargetRunbookServer;
        private static string RunbookPath = @"\Testing\New Runbook";
        private static string webserverURL = "http://orchestrator.genmills.com/Orchestrator2012/Orchestrator.svc";
        private static bool WaitForExit = true;
        private static string RunbookGUID;
        private static NetworkCredential alternateCredentials;
        private static Dictionary<string, string> _InputParameters;
        private static bool runRunbook()
        {
            bool status = true;
            OrchestratorContext sco = setupOrchestratorConnection();
            OrchestratorInterop.SCOrchestrator.Job job;
            try
            {
                if (string.IsNullOrEmpty(TargetRunbookServer))
                {
                    if (!String.IsNullOrEmpty(RunbookPath)) { job = SCOrch.startRunbookJob(sco, RunbookPath, _InputParameters); }
                    else if (!String.IsNullOrEmpty(RunbookGUID)) { job = SCOrch.startRunbookJob(sco, new Guid(RunbookGUID), _InputParameters); }
                    else { throw new Exception("Must pass either -RunbookPath or -RunbookGUID"); }
                }
                else
                {
                    if (!String.IsNullOrEmpty(RunbookPath)) { job = SCOrch.startRunbookJob(sco, RunbookPath, TargetRunbookServer, _InputParameters); }
                    else if (!String.IsNullOrEmpty(RunbookGUID)) { job = SCOrch.startRunbookJob(sco, new Guid(RunbookGUID), TargetRunbookServer, _InputParameters); }
                    else { throw new Exception("Must pass either -RunbookPath or -RunbookGUID"); }
                }

                if (WaitForExit)
                {
                    pollForJobCompletion(sco, job.Id);
                }
                else
                {
                    JobInstance ji = new JobInstance();
                    ji.job = job;
                   // WriteObject(ji);
                }
            }
            catch
            {
                throw;
            }
            return status;
        }
        private static OrchestratorContext setupOrchestratorConnection()
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(webserverURL));

            if (alternateCredentials == null) { sco.Credentials = CredentialCache.DefaultCredentials; }
            else { sco.Credentials = alternateCredentials; }
            sco.MergeOption = MergeOption.OverwriteChanges;

            return sco;
        }

        private static void pollForJobCompletion(OrchestratorContext sco, Guid jobID)
        {
            while (!SCOrch.getJobDetails(sco, jobID).job.Status.Equals("Completed"))
            {
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
            }
            JobInstance j = SCOrch.getJobDetails(sco, jobID);

            //WriteObject(j);
        }

        private static Dictionary<string, string> HashtableToDictionary(Hashtable table)
        {
            Dictionary<string, string> retTable = new Dictionary<string, string>();
            foreach (DictionaryEntry pair in table)
            {
                retTable.Add(pair.Key.ToString(), pair.Value.ToString());
            }
            return retTable;
        }
        static void Main(string[] args)
        {
            
                }
            }
    /*
            if (internalCounter >= RetryCount)
            {
                throw new Exception(exceptionMessage);
            }*/
        }

