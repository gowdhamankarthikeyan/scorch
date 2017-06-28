using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceManagementAutomation.Interop.ServiceManagementAutomationWS;

namespace ServiceManagementAutomation.Interop
{
    public class SMAInterop
    {
        private static int INTERNAL_PAGE_SIZE = 50;

        public int PAGE_SIZE
        {
            get { return INTERNAL_PAGE_SIZE; }
            set { INTERNAL_PAGE_SIZE = value; }
        }

        /// <summary>
        /// Starts a Job for the given Runbook, Does not wait for job to complete
        /// </summary>
        /// <param name="sma">Orchestrator Environment Reference</param>
        /// <param name="runbookName">Path to Runbook</param>
        /// <param name="runbookParameters">Input Parameters [Parameter Name] [Parameter Value]</param>
        /// <returns>Job</returns>
        public static Job startRunbookJob(OrchestratorApi sma, String runbookName, List<NameValuePair> runbookParameters)
        {
            return initiateRunbookJob(sma, runbookName, runbookParameters);
        }
        /// <summary>
        /// Starts a Job for the given Runbook, Waits up to timeout for job to complete
        /// </summary>
        /// <param name="sma">Orchestrator Environment Reference</param>
        /// <param name="runbookName">Path to Runbook</param>
        /// <param name="runbookParameters">Input Parameters [Parameter Name] [Parameter Value]</param>
        /// <param name="timeOut">Timespan to wait for job to complete before timing out</param>
        /// <returns>Job</returns>
        public static Job startRunbookJob(OrchestratorApi sma, String runbookName, List<NameValuePair> runbookParameters, TimeSpan timeOut)
        {
            Job job = initiateRunbookJob(sma, runbookName, runbookParameters);
            var jobId = job.JobID;
            var jobStatus = job.JobStatus;
            DateTime startTime = DateTime.Now;

            while (jobStatus != "Completed" && jobStatus != "Failed")
            {
                // Wait 5 seconds between polling
                Thread.Sleep(new TimeSpan(0, 0, 0, 5));
                
                jobStatus = sma.Jobs.Where(j => j.JobID == jobId).Select(j => j.JobStatus).AsEnumerable().First();
                if(TimeSpan.Compare(DateTime.Now - startTime, timeOut) > 0) { break; }
            }

            return sma.Jobs.Where(j => j.JobID == jobId).AsEnumerable().First();
        }

        private static Job initiateRunbookJob(OrchestratorApi sma, String runbookName, List<NameValuePair> runbookParameters)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                delegate
                {
                    return true;
                });
            List<string> parameterNames = new List<string>();
            var runbook = (from rb in sma.Runbooks
                           where rb.RunbookName == runbookName
                           select rb).AsEnumerable().FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "{0}: {1}", refStrings.RunbookNotFound, runbookName);
                throw new Exception(msg);
            }

            OperationParameter operationParameters = new BodyOperationParameter(refStrings.JobParameterName, runbookParameters);
            string uri = string.Concat(sma.Runbooks, string.Format("(guid'{0}')/{1}", runbook.RunbookID, refStrings.StartRunbookActionName));
            Uri uriSMA = new Uri(uri, UriKind.Absolute);

            // Create the job
            var jobIdValue = sma.Execute<Guid>(uriSMA, refStrings.HttpPost, true, operationParameters) as QueryOperationResponse<Guid>;
            var jobId = jobIdValue.Single();

            var job = sma.Jobs.Where(j => j.JobID == jobId).AsEnumerable().FirstOrDefault();
            if (job == null) { throw new Exception(refStrings.JobNotStarted); }
            return job;
        }
    }
}
