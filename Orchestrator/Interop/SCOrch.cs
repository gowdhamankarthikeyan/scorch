using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Net;
using System.Collections;
using OrchestratorInterop.SCOrchestrator;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Globalization;
using OrchestratorInterop.Data_Class;
using System.Web;
using System.Xml;
using System.Xml.Xsl;

namespace OrchestratorInterop
{
    public class SCOrch
    {
        private static int INTERNAL_PAGE_SIZE = 50;

        public int PAGE_SIZE
        {
            get { return INTERNAL_PAGE_SIZE; }
            set { INTERNAL_PAGE_SIZE = value; }
        }

        /// <summary>
        /// Starts a Job for the given Runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookPath">Path to Runbook</param>
        /// <param name="runbookParameters">Input Parameters [Parameter Name] [Parameter Value]</param>
        /// <returns>Job Id</returns>
        public static Job startRunbookJob(OrchestratorContext sco, String runbookPath, Dictionary<string, string> runbookParameters)
        {
            var runbookParameterIds = new Dictionary<string, string>();

            var runbook = (from rb in sco.Runbooks
                           where rb.Path == runbookPath
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookPath);
                throw new Exception(msg);
            }

            if (runbook.IsMonitor)
            {
                // Create a job

                var job = new Job();
                job.RunbookId = runbook.Id;
                sco.AddToJobs(job);

                var response = sco.SaveChanges();
                return job;
            }
            else
            {
                // Check the input parameters of the runbook

                var inParams = from p in sco.RunbookParameters
                               where (p.RunbookId == runbook.Id && p.Direction.Equals("In", StringComparison.OrdinalIgnoreCase))
                               select p;

                int pageNumber = inParams.Count() / INTERNAL_PAGE_SIZE;
                for (int i = 0; i <= pageNumber; i++)
                {
                    foreach (var p in inParams.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                    {
                        runbookParameterIds[p.Name] = p.Id.ToString("B");
                    }
                }

                // Create a job

                var job = new Job();
                job.RunbookId = runbook.Id;

                var sb = new StringBuilder();

                // Add all parameters

                sb.AppendLine("<Data>");

                foreach (var kv in runbookParameters)
                {

                    if (!runbookParameterIds.ContainsKey(kv.Key))
                    {
                        var msg = string.Format(CultureInfo.InvariantCulture,
                            "No Input Parameter on Runbook Found for Key: {0}",
                            kv.Key);
                        throw new Exception(msg);
                    }

                    string value = HttpUtility.HtmlEncode(kv.Value);
                    value = XmlConvert.EncodeName(value);

                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                        kv.Key,
                        runbookParameterIds[kv.Key],
                        value);

                    // Remove the key/value so we know it's used
                    runbookParameterIds.Remove(kv.Key);
                }

                // for all parameters that are defined in the runbook but we do not use, fill in empty value
                foreach (var kv in runbookParameterIds)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                        kv.Key,
                        kv.Value,
                        string.Empty);
                }

                sb.AppendLine("</Data>");

                job.Parameters = sb.ToString();

                sco.AddToJobs(job);
                bool done = false;
                while (!done)
                {
                    try
                    {
                        var response = sco.SaveChanges();
                        done = true;
                    }
                    catch { throw; }
                }
                return job;
            }
        }
        /// <summary>
        /// Starts a Job for the given Runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookGUID">GUID of the runbook to start</param>
        /// <param name="runbookParameters">Input Parameters [Parameter Name] [Parameter Value]</param>
        /// <returns>a Job</returns>
        public static Job startRunbookJob(OrchestratorContext sco, Guid runbookGUID, Dictionary<string, string> runbookParameters)
        {
            var runbookParameterIds = new Dictionary<string, string>();

            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookGUID
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookGUID);
                throw new Exception(msg);
            }

            // Check the input parameters of the runbook

            var inParams = from p in sco.RunbookParameters
                           where (p.RunbookId == runbook.Id && p.Direction.Equals("In", StringComparison.OrdinalIgnoreCase))
                           select p;
            int pageNumber = inParams.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (var p in inParams.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    runbookParameterIds[p.Name] = p.Id.ToString("B");
                }
            }
            // Create a job

            var job = new Job();
            job.RunbookId = runbook.Id;

            var sb = new StringBuilder();

            // Add all parameters

            sb.AppendLine("<Data>");

            foreach (var kv in runbookParameters)
            {
                if (!runbookParameterIds.ContainsKey(kv.Key))
                {
                    var msg = string.Format(CultureInfo.InvariantCulture,
                        "No Input Parameter on Runbook Found for Key: {0}",
                        kv.Key);

                    throw new Exception(msg);
                }

                string value = HttpUtility.HtmlEncode(kv.Value);
                value = XmlConvert.EncodeName(value);

                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                    kv.Key,
                    runbookParameterIds[kv.Key],
                    value);

                // Remove the key/value so we know it's used
                runbookParameterIds.Remove(kv.Key);
            }

            // for all parameters that are defined in the runbook but we do not use, fill in empty value
            foreach (var kv in runbookParameterIds)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                    kv.Key,
                    kv.Value,
                    string.Empty);
            }

            sb.AppendLine("</Data>");

            job.Parameters = sb.ToString();

            sco.AddToJobs(job);
            bool done = false;
            while (!done)
            {
                try
                {
                    var response = sco.SaveChanges();
                    done = true;
                }
                catch { }
            }
            return job;
        }
        /// <summary>
        /// Starts a Job for the given Runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookPath">Path to Runbook</param>
        /// <param name="runbookPath">Runbook Server Name</param>
        /// <param name="runbookParameters">Input Parameters [Parameter Name] [Parameter Value]</param>
        /// <returns>Job Id</returns>
        public static Job startRunbookJob(OrchestratorContext sco, String runbookPath, String RunbookServerName, Dictionary<string, string> runbookParameters)
        {
            var runbookParameterIds = new Dictionary<string, string>();

            var runbook = (from rb in sco.Runbooks
                           where rb.Path == runbookPath
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookPath);
                throw new Exception(msg);
            }

            // Check the input parameters of the runbook

            var inParams = from p in sco.RunbookParameters
                           where (p.RunbookId == runbook.Id && p.Direction.Equals("In", StringComparison.OrdinalIgnoreCase))
                           select p;
            int pageNumber = inParams.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (var p in inParams.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    runbookParameterIds[p.Name] = p.Id.ToString("B");
                }
            }

            // Lookup runbook server Id

            var RunbookServer = (from rs in sco.RunbookServers
                                 where rs.Name == RunbookServerName
                                 select rs).FirstOrDefault();

            // Create a job

            var job = new Job();
            job.RunbookId = runbook.Id;
            job.RunbookServers = RunbookServer.Name;

            var sb = new StringBuilder();

            // Add all parameters

            sb.AppendLine("<Data>");

            foreach (var kv in runbookParameters)
            {
                if (!runbookParameterIds.ContainsKey(kv.Key))
                {
                    var msg = string.Format(CultureInfo.InvariantCulture,
                        "No Input Parameter on Runbook Found for Key: {0}",
                        kv.Key);

                    throw new Exception(msg);
                }

                string value = HttpUtility.HtmlEncode(kv.Value);
                value = XmlConvert.EncodeName(value);

                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                    kv.Key,
                    runbookParameterIds[kv.Key],
                    value);

                // Remove the key/value so we know it's used
                runbookParameterIds.Remove(kv.Key);
            }

            // for all parameters that are defined in the runbook but we do not use, fill in empty value
            foreach (var kv in runbookParameterIds)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                    kv.Key,
                    kv.Value,
                    string.Empty);
            }

            sb.AppendLine("</Data>");

            job.Parameters = sb.ToString();

            sco.AddToJobs(job);

            var response = sco.SaveChanges();
            return job;
        }
        /// <summary>
        /// Starts a Job for the given Runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookPath">Path to Runbook</param>
        /// <param name="runbookPath">Runbook Server Name</param>
        /// <param name="runbookParameters">Input Parameters [Parameter Name] [Parameter Value]</param>
        /// <returns>Job Id</returns>
        public static Job startRunbookJob(OrchestratorContext sco, Guid runbookGUID, String RunbookServerName, Dictionary<string, string> runbookParameters)
        {
            var runbookParameterIds = new Dictionary<string, string>();

            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookGUID
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookGUID);
                throw new Exception(msg);
            }

            // Check the input parameters of the runbook

            var inParams = from p in sco.RunbookParameters
                           where (p.RunbookId == runbook.Id && p.Direction.Equals("In", StringComparison.OrdinalIgnoreCase))
                           select p;
            int pageNumber = inParams.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (var p in inParams.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    runbookParameterIds[p.Name] = p.Id.ToString("B");
                }
            }

            // Lookup runbook server Id

            var RunbookServer = (from rs in sco.RunbookServers
                                 where rs.Name == RunbookServerName
                                 select rs).FirstOrDefault();

            // Create a job

            var job = new Job();
            job.RunbookId = runbook.Id;
            job.RunbookServers = RunbookServer.Name;

            var sb = new StringBuilder();

            // Add all parameters

            sb.AppendLine("<Data>");

            foreach (var kv in runbookParameters)
            {
                if (!runbookParameterIds.ContainsKey(kv.Key))
                {
                    var msg = string.Format(CultureInfo.InvariantCulture,
                        "No Input Parameter on Runbook Found for Key: {0}",
                        kv.Key);

                    throw new Exception(msg);
                }

                string value = HttpUtility.HtmlEncode(kv.Value);
                value = XmlConvert.EncodeName(value);
             
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                    kv.Key,
                    runbookParameterIds[kv.Key],
                    value);

                // Remove the key/value so we know it's used
                runbookParameterIds.Remove(kv.Key);
            }

            // for all parameters that are defined in the runbook but we do not use, fill in empty value
            foreach (var kv in runbookParameterIds)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "<Parameter><Name>{0}</Name><ID>{1}</ID><Value>{2}</Value></Parameter>",
                    kv.Key,
                    kv.Value,
                    string.Empty);
            }

            sb.AppendLine("</Data>");

            job.Parameters = sb.ToString();

            sco.AddToJobs(job);

            var response = sco.SaveChanges();
            return job;
        }
        
        /// <summary>
        /// Gets the Details for a given Runbook Job
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="JobId">Id Of job to retrieve execution details of</param>
        /// <returns>A Job Instance containing the Job reference and all input and output parameters</returns>
        public static JobInstance getJobDetails(OrchestratorContext sco, Guid JobId)
        {
            var jobId = JobId;
            
            var job = (from j in sco.Jobs
                       where j.Id == jobId
                       select j).FirstOrDefault();

            if (job == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Job Not Found: ", jobId);
                throw new Exception(msg);
            }
            
            var jobOutput = new Dictionary<string, string>();
            var jobInput = new Dictionary<string, string>();
            int instanceCount = 0;

            if (job.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
            {
                // Get the return data
                var instances = sco.RunbookInstances.Where(ri => ri.JobId == jobId);
                var activeInstances = sco.RunbookInstances.Where(ri => ri.JobId == jobId && ri.Status == "InProgress");
                instanceCount = activeInstances.Count();

                // For the non-monitor runbook job, there should be only one instance.
                foreach (var instance in instances)
                {
                    if (!(instance.Status.Equals("Success", StringComparison.OrdinalIgnoreCase) || instance.Status.Equals("Warning", StringComparison.OrdinalIgnoreCase)))
                        continue;

                    var outParameters = sco.RunbookInstanceParameters.Where(
                        rip => rip.RunbookInstanceId == instance.Id && rip.Direction.Equals("Out", StringComparison.OrdinalIgnoreCase));

                    foreach (var parameter in outParameters)
                    {
                        jobOutput[parameter.Name] = parameter.Value;
                    }

                    var inParameters = sco.RunbookInstanceParameters.Where(
                        rip => rip.RunbookInstanceId == instance.Id && rip.Direction.Equals("In", StringComparison.OrdinalIgnoreCase));

                    foreach (var parameter in inParameters)
                    {
                        jobInput[parameter.Name] = parameter.Value;
                    }
                }
            }
            else
            {
                try
                {
                    // Get the return data
                    var instances = sco.RunbookInstances.Where(ri => ri.JobId == jobId);
                    var activeInstances = sco.RunbookInstances.Where(ri => ri.JobId == jobId && ri.Status == "InProgress");
                    instanceCount = activeInstances.Count();

                    foreach (var instance in instances)
                    {
                        var inParameters = sco.RunbookInstanceParameters.Where(
                                rip => rip.RunbookInstanceId == instance.Id && rip.Direction.Equals("In", StringComparison.OrdinalIgnoreCase));

                        foreach (var parameter in inParameters)
                        {
                            jobInput[parameter.Name] = parameter.Value;
                        }
                    }
                }
                catch { }
            }

            JobInstance jInstance = new JobInstance() { job = job, InputParameters = jobInput, OutputParameters = jobOutput, ActiveInstances = instanceCount };

            return jInstance;
        }
        /// <summary>
        /// Gets all folders in a Orchestrator environment
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <returns>Array containing all folders</returns>
        public static Folder[] getAllFolders(OrchestratorContext sco)
        {
            var folders = (from f in sco.Folders
                           select f);

            Folder[] allFolders = new Folder[folders.Count()];


            int pageNumber = folders.Count() / INTERNAL_PAGE_SIZE;
            int j = 0;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Folder f in folders.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    allFolders[j] = f;
                    j++;
                }
            }

            return allFolders;
        }
        /// <summary>
        /// Gets subfolders for a given folderpath in a Orchestrator environment
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="folderPath">Path to folder</param>
        /// <returns>Array containing all folders</returns>
        public static Folder[] getSubFolders(OrchestratorContext sco, string folderPath)
        {
            var baseFolderID = ((from fid in sco.Folders
                               where fid.Path == folderPath
                               select fid).FirstOrDefault()).Id;

            var folders = (from f in sco.Folders
                           where f.ParentId == baseFolderID                           
                           select f);

            Folder[] allFolders = new Folder[folders.Count()];

            int pageNumber = folders.Count() / INTERNAL_PAGE_SIZE;
            int j = 0;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Folder f in folders.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    allFolders[j] = f;
                    j++;
                }
            }

            return allFolders;
        }

        /// <summary>
        /// Gets subfolders for a given folderpath in a Orchestrator environment
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="folderPath">Path to folder</param>
        /// <returns>Array containing all folders</returns>
        public static Folder[] getSubFolders(OrchestratorContext sco, string folderPath, bool loadRunbooks)
        {
            var baseFolderID = ((from fid in sco.Folders
                                 where fid.Path == folderPath
                                 select fid).FirstOrDefault()).Id;

            var folders = (from f in sco.Folders
                           where f.ParentId == baseFolderID
                           select f);

            Folder[] allFolders = new Folder[folders.Count()];

            int pageNumber = folders.Count() / INTERNAL_PAGE_SIZE;
            int j = 0;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Folder f in folders.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (loadRunbooks)
                    {
                        var runbooks = (from rb in sco.Runbooks
                                        where rb.FolderId == f.Id
                                        select rb);
                        System.Collections.ObjectModel.Collection<Runbook> rbCol = new System.Collections.ObjectModel.Collection<Runbook>();
                        foreach (Runbook rb in runbooks) { rbCol.Add(rb); }
                        f.Runbooks = rbCol;
                    }
                    allFolders[j] = f;
                    j++;
                }
            }

            return allFolders;
        }
        /// <summary>
        /// Stops a given Job for a runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="JobId">Job Id reference to stop</param>
        /// <returns>Job Instance details of stopped job</returns>
        public static JobInstance stopRunbookJob(OrchestratorContext sco, Guid JobId)
        {
            var jobId = JobId;

            var job = (from j in sco.Jobs
                       where j.Id == jobId
                       select j).FirstOrDefault();

            if (job == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Job Not Found: ", jobId);
                throw new Exception(msg);
            }

            var jobOutput = new Dictionary<string, string>();
            var jobInput = new Dictionary<string, string>();

            if (job.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
            {
                // Get the return data
                var instances = sco.RunbookInstances.Where(ri => ri.JobId == jobId);

                // For the non-monitor runbook job, there should be only one instance.
                foreach (var instance in instances)
                {
                    if (!instance.Status.Equals("Success", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var outParameters = sco.RunbookInstanceParameters.Where(
                        rip => rip.RunbookInstanceId == instance.Id && rip.Direction.Equals("Out", StringComparison.OrdinalIgnoreCase));

                    foreach (var parameter in outParameters)
                    {
                        jobOutput[parameter.Name] = parameter.Value;
                    }

                    var inParameters = sco.RunbookInstanceParameters.Where(
                        rip => rip.RunbookInstanceId == instance.Id && rip.Direction.Equals("In", StringComparison.OrdinalIgnoreCase));

                    foreach (var parameter in inParameters)
                    {
                        jobInput[parameter.Name] = parameter.Value;
                    }
                }
            }
            else
            {
                // Get the return data
                var instances = sco.RunbookInstances.Where(ri => ri.JobId == jobId);

                foreach (var instance in instances)
                {
                    var inParameters = sco.RunbookInstanceParameters.Where(
                            rip => rip.RunbookInstanceId == instance.Id && rip.Direction.Equals("In", StringComparison.OrdinalIgnoreCase));

                    foreach (var parameter in inParameters)
                    {
                        jobInput[parameter.Name] = parameter.Value;
                    }
                }

                // Set <Status> to "Canceled" (this is what stops the Job)
                job.Status = "Canceled";
                sco.UpdateObject(job);
                sco.SaveChanges();
            }

            JobInstance jInstance = new JobInstance() { job = job, InputParameters = jobInput, OutputParameters = jobOutput };

            return jInstance;
        }
        /// <summary>
        /// Retrieves all the Job Instance details for a given runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookPath">Path To Runbook -- Format \ContainingFolder\Folder Name\RunbookName</param>
        /// <returns>Job Instance array of all logged runs</returns>
        public static JobInstance[] getRunbookJobInstances(OrchestratorContext sco, String runbookPath)
        {
            ArrayList jInstanceArrayList = new ArrayList();
            JobInstance[] jInstances;

            var runbook = (from rb in sco.Runbooks
                           where rb.Path == runbookPath
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookPath);
                throw new Exception(msg);
            }

            var jobs = (from jb in sco.Jobs
                        where jb.RunbookId == runbook.Id
                        select jb);

            if (jobs == null)
            {
                if (runbook == null)
                {
                    var msg = string.Format(CultureInfo.InvariantCulture, "Jobs not found for Runbook:  ", runbookPath);
                    throw new Exception(msg);
                }
            }

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    jInstanceArrayList.Add(getJobDetails(sco, j.Id));
                }
            }

            jInstances = new JobInstance[jInstanceArrayList.Count];
            jInstanceArrayList.CopyTo(jInstances);
            return jInstances;
        }

        /// <summary>
        /// Retrieves all the Job Instance details for a given runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookID">Runbook GUID</param>
        /// <returns>Job Instance array of all logged runs</returns>
        public static JobInstance[] getRunbookJobInstances(OrchestratorContext sco, Guid runbookID)
        {
            ArrayList jInstanceArrayList = new ArrayList();
            JobInstance[] jInstances;

            var runbook = (from rb in sco.Runbooks
                           where rb.Id  == runbookID
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookID);
                throw new Exception(msg);
            }

            var jobs = (from jb in sco.Jobs
                        where jb.RunbookId == runbook.Id
                        select jb);

            if (jobs == null)
            {
                if (runbook == null)
                {
                    var msg = string.Format(CultureInfo.InvariantCulture, "Jobs not found for Runbook:  ", runbookID);
                    throw new Exception(msg);
                }
            }

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber ; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    jInstanceArrayList.Add(getJobDetails(sco, j.Id));
                }
            }

            

            jInstances = new JobInstance[jInstanceArrayList.Count];
            jInstanceArrayList.CopyTo(jInstances);
            return jInstances;
        }
        /// <summary>
        /// Retrieves all the Job Instance details for a given runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookPath">Path To Runbook -- Format \ContainingFolder\Folder Name\RunbookName</param>
        /// <param name="Status">Status of the Runbook Job</param>
        /// <returns>Job Instance array of all logged runs</returns>
        public static JobInstance[] getRunbookJobInstances(OrchestratorContext sco, String runbookPath, String Status)
        {
            ArrayList jInstanceArrayList = new ArrayList();
            JobInstance[] jInstances;

            var runbook = (from rb in sco.Runbooks
                           where rb.Path == runbookPath
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookPath);
                throw new Exception(msg);
            }

            var jobs = (from jb in sco.Jobs
                        where jb.RunbookId == runbook.Id && jb.Status == Status
                        select jb);

            if (jobs == null)
            {
                if (runbook == null)
                {
                    var msg = string.Format(CultureInfo.InvariantCulture, "Jobs not found for Runbook:  ", runbookPath);
                    throw new Exception(msg);
                }
            }

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    {
                        jInstanceArrayList.Add(getJobDetails(sco, j.Id));
                    }
                }
            }

            jInstances = new JobInstance[jInstanceArrayList.Count];
            jInstanceArrayList.CopyTo(jInstances);
            return jInstances;
        }

        /// <summary>
        /// Retrieves all the Job Instance details for a given runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookID">Runbook GUID</param>
        /// <param name="Status">Status of the Runbook Job</param>
        /// <returns>Job Instance array of all logged runs</returns>
        public static JobInstance[] getRunbookJobInstances(OrchestratorContext sco, Guid runbookID, String Status)
        {
            ArrayList jInstanceArrayList = new ArrayList();
            JobInstance[] jInstances;

            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookID
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookID);
                throw new Exception(msg);
            }

            var jobs = (from jb in sco.Jobs
                        where jb.RunbookId == runbook.Id && jb.Status == Status
                        select jb);

            if (jobs == null)
            {
                if (runbook == null)
                {
                    var msg = string.Format(CultureInfo.InvariantCulture, "Jobs not found for Runbook:  ", runbookID);
                    throw new Exception(msg);
                }
            }

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    jInstanceArrayList.Add(getJobDetails(sco, j.Id));
                }
            }

            jInstances = new JobInstance[jInstanceArrayList.Count];
            jInstanceArrayList.CopyTo(jInstances);
            return jInstances;
        }
        /// <summary>
        /// Retrieves all the Job Instance details for a given runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookID">Runbook GUID</param>
        /// <param name="Status">Status of the Runbook Job</param>
        /// <returns>Job Instance array of all logged runs</returns>
        public static JobInstance[] getRunbookJobInstances(OrchestratorContext sco, Guid runbookID, String Status, bool withDetails)
        {
            ArrayList jInstanceArrayList = new ArrayList();
            JobInstance[] jInstances;

            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookID
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookID);
                throw new Exception(msg);
            }

            var jobs = (from jb in sco.Jobs
                        where jb.RunbookId == runbook.Id && jb.Status == Status
                        select jb);

            if (jobs == null)
            {
                if (runbook == null)
                {
                    var msg = string.Format(CultureInfo.InvariantCulture, "Jobs not found for Runbook:  ", runbookID);
                    throw new Exception(msg);
                }
            }

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (withDetails)
                    {
                        jInstanceArrayList.Add(getJobDetails(sco, j.Id));
                    }
                    else
                    {
                        jInstanceArrayList.Add(new JobInstance() { job = j });
                    }

                }
            }

            jInstances = new JobInstance[jInstanceArrayList.Count];
            jInstanceArrayList.CopyTo(jInstances);
            return jInstances;
        }
        /// <summary>
        /// Retrieves all the Job Instance details for a given runbook
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="runbookID">Runbook GUID</param>
        /// <returns>Job Instance array of all logged runs</returns>
        public static JobInstance[] getRunbookJobInstances(OrchestratorContext sco, Guid runbookID, bool withDetails)
        {
            ArrayList jInstanceArrayList = new ArrayList();
            JobInstance[] jInstances;

            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookID
                           select rb).FirstOrDefault();

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found:  ", runbookID);
                throw new Exception(msg);
            }

            var jobs = (from jb in sco.Jobs
                        where jb.RunbookId == runbook.Id
                        select jb);

            if (jobs == null)
            {
                if (runbook == null)
                {
                    var msg = string.Format(CultureInfo.InvariantCulture, "Jobs not found for Runbook:  ", runbookID);
                    throw new Exception(msg);
                }
            }

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (withDetails)
                    {
                        jInstanceArrayList.Add(getJobDetails(sco, j.Id));
                    }
                    else
                    {
                        jInstanceArrayList.Add(new JobInstance() { job = j });
                    }

                }
            }

            jInstances = new JobInstance[jInstanceArrayList.Count];
            jInstanceArrayList.CopyTo(jInstances);
            return jInstances;
        }
        /// <summary>
        /// Retrieves all Runbooks in a given Folder Path
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="FolderPath">Path to folder (includes Folder Name) -- Format \ContainingFolder\FolderName</param>
        /// <returns>Array of all Runbooks in the folder</returns>
        public static Runbook[] getAllRunbooksInFolder(OrchestratorContext sco, String FolderPath)
        {
            var folder = (from fol in sco.Folders
                           where fol.Path == FolderPath
                           select fol).FirstOrDefault();

            if (folder == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbooks Not Found in Folder:  ", FolderPath);
                throw new Exception(msg);
            }

            Guid folderId = folder.Id;

            var runbooks = (from rb in sco.Runbooks
                            where rb.FolderId == folderId
                            select rb);

            if (runbooks == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbooks Not Found in Folder:  ", FolderPath);
                throw new Exception(msg);
            }
            Runbook[] runbookArray = new Runbook[runbooks.Count()];
            int b = 0;
            int pageNumber = runbooks.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Runbook r in runbooks.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    var runbookParams = (from param in sco.RunbookParameters
                                         where param.RunbookId == r.Id
                                         select param);

                    System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                    foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                    r.Parameters = paramCol;

                    runbookArray[b] = r;
                    b++;
                }
            }

            return runbookArray;
        }

        /// <summary>
        /// Retrieves all Runbooks in a given Folder Path
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="FolderPath">Path to folder (includes Folder Name) -- Format \ContainingFolder\FolderName</param>
        /// <returns>Array of all Runbooks in the folder</returns>
        public static Runbook[] getAllRunbooksInFolder(OrchestratorContext sco, String FolderPath, bool loadParamData)
        {
            var folder = (from fol in sco.Folders
                          where fol.Path == FolderPath
                          select fol).FirstOrDefault();

            if (folder == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbooks Not Found in Folder:  ", FolderPath);
                throw new Exception(msg);
            }

            Guid folderId = folder.Id;

            var runbooks = (from rb in sco.Runbooks
                            where rb.FolderId == folderId
                            select rb);

            if (runbooks == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbooks Not Found in Folder:  ", FolderPath);
                throw new Exception(msg);
            }
            Runbook[] runbookArray = new Runbook[runbooks.Count()];
            int b = 0;
            int pageNumber = runbooks.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Runbook r in runbooks.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (loadParamData)
                    {
                        var runbookParams = (from param in sco.RunbookParameters
                                             where param.RunbookId == r.Id
                                             select param);

                        System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                        foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                        r.Parameters = paramCol;
                    }
                    runbookArray[b] = r;
                    b++;
                }
            }

            return runbookArray;
        }

        /// <summary>
        /// Retrieves all Runbooks in a given Folder Path
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="RunbookPath">Path to runbook</param>
        /// <returns>Runbook Object</returns>
        public static Runbook getRunbook(OrchestratorContext sco, String RunbookPath)
        {
            var runbook = (from rb in sco.Runbooks
                          where rb.Path == RunbookPath
                          select rb).FirstOrDefault();

            var runbookParams = (from param in sco.RunbookParameters
                                 where param.RunbookId == runbook.Id
                                 select param);

            System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
            foreach(RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

            runbook.Parameters = paramCol;

            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found at path:  ", RunbookPath);
                throw new Exception(msg);
            }

            return runbook;
        }
        /// <summary>
        /// Retrieves all Runbooks in a given Folder Path
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="RunbookPath">Path to runbook</param>
        /// <returns>Runbook Object</returns>
        public static Runbook getRunbook(OrchestratorContext sco, Guid runbookGUID)
        {
            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookGUID
                           select rb).FirstOrDefault();

            var runbookParams = (from param in sco.RunbookParameters
                                 where param.RunbookId == runbook.Id
                                 select param);

            System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
            foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

            runbook.Parameters = paramCol;
            
            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found with GUID:  ", runbookGUID);
                throw new Exception(msg);
            }

            return runbook;
        }
        /// Retrieves all Runbooks in a given Folder Path
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="RunbookPath">Path to runbook</param>
        /// <returns>Runbook Object</returns>
        public static Runbook getRunbook(OrchestratorContext sco, String RunbookPath, bool loadParameterData)
        {
            var runbook = (from rb in sco.Runbooks
                           where rb.Path == RunbookPath
                           select rb).FirstOrDefault();

            if (loadParameterData)
            {
                var runbookParams = (from param in sco.RunbookParameters
                                     where param.RunbookId == runbook.Id
                                     select param);

                System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                runbook.Parameters = paramCol;
            }
            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found at path:  ", RunbookPath);
                throw new Exception(msg);
            }

            return runbook;
        }
        /// <summary>
        /// Retrieves all Runbooks in a given Folder Path
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="RunbookPath">Path to runbook</param>
        /// <returns>Runbook Object</returns>
        public static Runbook getRunbook(OrchestratorContext sco, Guid runbookGUID, bool loadParameterData)
        {
            var runbook = (from rb in sco.Runbooks
                           where rb.Id == runbookGUID
                           select rb).FirstOrDefault();
            if (loadParameterData)
            {
                var runbookParams = (from param in sco.RunbookParameters
                                     where param.RunbookId == runbook.Id
                                     select param);

                System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                runbook.Parameters = paramCol;
            }
            if (runbook == null)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "Runbook Not Found with GUID:  ", runbookGUID);
                throw new Exception(msg);
            }

            return runbook;
        }
        /// <summary>
        /// Gets all jobs in status Running or Pending
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <returns>Array of Runbook Objects in Running or Pending Status</returns>
        public static Runbook[] getAllRunningRunbooks(OrchestratorContext sco)
        {
            Dictionary<Guid, Runbook> runbookDict = new Dictionary<Guid,Runbook>();
            var jobs = (from jb in sco.Jobs
                        where jb.Status == "Running" || jb.Status == "Pending"
                        select jb);
            
            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    var runbook = (from rb in sco.Runbooks
                                   where rb.Id == j.RunbookId
                                   select rb).FirstOrDefault();

                    var runbookParams = (from param in sco.RunbookParameters
                                         where param.RunbookId == runbook.Id
                                         select param);

                    System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                    foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                    runbook.Parameters = paramCol;

                    if (!runbookDict.ContainsKey(runbook.Id)) { runbookDict.Add(runbook.Id, runbook); }
                }
            }

            Runbook[] allRunbooks = new Runbook[runbookDict.Count];
            runbookDict.Values.CopyTo(allRunbooks,0);
            return allRunbooks;
        }
        /// <summary>
        /// Gets all Monitor Runbooks
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="loadParams">load input and output parameters or not</param>
        /// <returns>Array of Runbook Objects in Running or Pending Status</returns>
        public static Runbook[] getMonitorRunbook(OrchestratorContext sco, bool loadParams)
        {
            Dictionary<Guid, Runbook> runbookDict = new Dictionary<Guid, Runbook>();
            var runbooks = (from rb in sco.Runbooks
                            where rb.IsMonitor == true
                            select rb);

            int pageNumber = runbooks.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Runbook r in runbooks.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (loadParams)
                    {
                        var runbookParams = (from param in sco.RunbookParameters
                                             where param.RunbookId == r.Id
                                             select param);

                        System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                        foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                        r.Parameters = paramCol;
                    }
                    if (!runbookDict.ContainsKey(r.Id)) { runbookDict.Add(r.Id, r); }
                }
            }
            
            Runbook[] allRunbooks = new Runbook[runbookDict.Count];
            runbookDict.Values.CopyTo(allRunbooks, 0);
            return allRunbooks;
        }
        /// <summary>
        /// Gets Monitor Runbooks
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="folderPath">Path to the folder contianing the monitor</param>
        /// <param name="loadParams">load input and output parameters or not</param>
        /// <returns>Array of Runbook Objects in Running or Pending Status</returns>
        public static Runbook[] getMonitorRunbook(OrchestratorContext sco, string folderPath, bool loadParams)
        {
            Dictionary<Guid, Runbook> runbookDict = new Dictionary<Guid, Runbook>();
            Guid folID = ((from f in sco.Folders
                          where f.Path == folderPath
                          select f).FirstOrDefault()).Id;

            var runbooks = (from rb in sco.Runbooks
                            where rb.FolderId == folID
                            && rb.IsMonitor == true
                            select rb);

            int pageNumber = runbooks.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Runbook r in runbooks.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (loadParams)
                    {
                        var runbookParams = (from param in sco.RunbookParameters
                                             where param.RunbookId == r.Id
                                             select param);

                        System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                        foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                        r.Parameters = paramCol;
                    }
                    if (!runbookDict.ContainsKey(r.Id)) { runbookDict.Add(r.Id, r); }
                }
            }

            Runbook[] allRunbooks = new Runbook[runbookDict.Count];
            runbookDict.Values.CopyTo(allRunbooks, 0);
            return allRunbooks;
        }
        /// <summary>
        /// Gets Monitor Runbooks
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="folderID">GUID to the folder contianing the monitor</param>
        /// <param name="loadParams">load input and output parameters or not</param>
        /// <returns>Array of Runbook Objects in Running or Pending Status</returns>
        public static Runbook[] getMonitorRunbook(OrchestratorContext sco, Guid folderID, bool loadParams)
        {
            Dictionary<Guid, Runbook> runbookDict = new Dictionary<Guid, Runbook>();
            var runbooks = (from rb in sco.Runbooks
                            where rb.FolderId == folderID
                            && rb.IsMonitor == true
                            select rb);

            int pageNumber = runbooks.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Runbook r in runbooks.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (loadParams)
                    {
                        var runbookParams = (from param in sco.RunbookParameters
                                             where param.RunbookId == r.Id
                                             select param);

                        System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                        foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                        r.Parameters = paramCol;
                    }
                    if (!runbookDict.ContainsKey(r.Id)) { runbookDict.Add(r.Id, r); }
                }
            }

            Runbook[] allRunbooks = new Runbook[runbookDict.Count];
            runbookDict.Values.CopyTo(allRunbooks, 0);
            return allRunbooks;
        }
        /// <summary>
        /// Gets Monitor Runbooks
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="folder">the folder object to search in</param>
        /// <param name="loadParams">load input and output parameters or not</param>
        /// <returns>Array of Runbook Objects in Running or Pending Status</returns>
        public static Runbook[] getMonitorRunbook(OrchestratorContext sco, Folder folder, bool loadParams)
        {
            Guid fID = folder.Id;
            Dictionary<Guid, Runbook> runbookDict = new Dictionary<Guid, Runbook>();
            var runbooks = (from rb in sco.Runbooks
                            where rb.IsMonitor == true
                            && rb.Folder.Id == fID
                            select rb);

            int pageNumber = runbooks.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Runbook r in runbooks.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (loadParams)
                    {
                        var runbookParams = (from param in sco.RunbookParameters
                                             where param.RunbookId == r.Id
                                             select param);

                        System.Collections.ObjectModel.Collection<RunbookParameter> paramCol = new System.Collections.ObjectModel.Collection<RunbookParameter>();
                        foreach (RunbookParameter rp in runbookParams) { paramCol.Add(rp); }

                        r.Parameters = paramCol;
                    }
                    if (!runbookDict.ContainsKey(r.Id)) { runbookDict.Add(r.Id, r); }
                }
            }

            Runbook[] allRunbooks = new Runbook[runbookDict.Count];
            runbookDict.Values.CopyTo(allRunbooks, 0);
            return allRunbooks;
        }
        /// <summary>
        /// Gets all Jobs with given status
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="Status">Status to get Job array for</param>
        /// <returns>Job Instance array containing all Jobs in given status</returns>
        public static JobInstance[] getAllJobInstancesWithStatus(OrchestratorContext sco, String Status)
        {
            Dictionary<Guid, JobInstance> jobInstanceDict = new Dictionary<Guid, JobInstance>();
            var jobs = (from jb in sco.Jobs
                        where jb.Status == Status
                        select jb);
            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    JobInstance instance = getJobDetails(sco, j.Id);
                    if (!jobInstanceDict.ContainsKey(instance.job.Id)) { jobInstanceDict.Add(instance.job.Id, instance); }
                }
            }

            JobInstance[] allJobInstances = new JobInstance[jobInstanceDict.Count];
            jobInstanceDict.Values.CopyTo(allJobInstances, 0);
            return allJobInstances;
        }
        /// <summary>
        /// Gets all Jobs with given status
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="Status">Status to get Job array for</param>
        /// <param name="withDetails">Return input and output paramaters or not</param>
        /// <returns>Job Instance array containing all Jobs in given status</returns>
        public static JobInstance[] getAllJobs(OrchestratorContext sco, String Status, bool withDetails)
        {
            Dictionary<Guid, JobInstance> jobInstanceDict = new Dictionary<Guid, JobInstance>();
            var jobs = (from jb in sco.Jobs
                        where jb.Status == Status
                        select jb);
            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    JobInstance instance = new JobInstance() { job = j };
                    if (withDetails)
                    {
                        instance = getJobDetails(sco, j.Id);
                    }
                    if (!jobInstanceDict.ContainsKey(instance.job.Id)) { jobInstanceDict.Add(instance.job.Id, instance); }
                }
            }

            JobInstance[] allJobInstances = new JobInstance[jobInstanceDict.Count];
            jobInstanceDict.Values.CopyTo(allJobInstances, 0);
            return allJobInstances;
        }
        /// <summary>
        /// Gets all Jobs with given status
        /// </summary>
        /// <param name="sco">Orchestrator Environment Reference</param>
        /// <param name="Status">Status to get Job array for</param>
        /// <param name="withDetails">Return input and output paramaters or not</param>
        /// <returns>Job Instance array containing all Jobs in given status</returns>
        public static JobInstance[] getAllJobs(OrchestratorContext sco, bool withDetails)
        {
            Dictionary<Guid, JobInstance> jobInstanceDict = new Dictionary<Guid, JobInstance>();
            var jobs = (from jb in sco.Jobs
                        select jb);
            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    JobInstance instance = new JobInstance() { job = j };
                    if (withDetails)
                    {
                        instance = getJobDetails(sco, j.Id);
                    }
                    if (!jobInstanceDict.ContainsKey(instance.job.Id)) { jobInstanceDict.Add(instance.job.Id, instance); }
                }
            }

            JobInstance[] allJobInstances = new JobInstance[jobInstanceDict.Count];
            jobInstanceDict.Values.CopyTo(allJobInstances, 0);
            return allJobInstances;
        }
        public static JobInstance[] getAllJobInstancesOnRunbookServer(OrchestratorContext sco, String RunbookServerName)
        {
            Dictionary<Guid, JobInstance> jobInstanceDict = new Dictionary<Guid, JobInstance>();
            var jobs = (from jb in sco.Jobs
                        where jb.RunbookServer.Name == RunbookServerName
                        select jb);

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    JobInstance instance = getJobDetails(sco, j.Id);
                    if (!jobInstanceDict.ContainsKey(instance.job.Id)) { jobInstanceDict.Add(instance.job.Id, instance); }
                }
            }

            JobInstance[] allJobInstances = new JobInstance[jobInstanceDict.Count];
            jobInstanceDict.Values.CopyTo(allJobInstances, 0);
            return allJobInstances;
        }
        public static JobInstance[] getAllJobInstancesOnRunbookServer(OrchestratorContext sco, RunbookServer runbookServer, bool withDetails)
        {
            Dictionary<Guid, JobInstance> jobInstanceDict = new Dictionary<Guid, JobInstance>();
            var jobs = (from jb in sco.Jobs
                        where jb.RunbookServerId == runbookServer.Id
                        select jb);

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    JobInstance instance = new JobInstance() { job = j };
                    if (withDetails) { instance = getJobDetails(sco, j.Id); }
                    if (!jobInstanceDict.ContainsKey(instance.job.Id)) { jobInstanceDict.Add(instance.job.Id, instance); }
                }
            }

            JobInstance[] allJobInstances = new JobInstance[jobInstanceDict.Count];
            jobInstanceDict.Values.CopyTo(allJobInstances, 0);
            return allJobInstances;
        }
        public static JobInstance[] getAllJobInstancesOnRunbookServer(OrchestratorContext sco, RunbookServer runbookServer, string jobStatus, bool withDetails)
        {
            Dictionary<Guid, JobInstance> jobInstanceDict = new Dictionary<Guid, JobInstance>();
            var jobs = (from jb in sco.Jobs
                        where jb.RunbookServerId == runbookServer.Id && jb.Status == jobStatus
                        select jb);

            int pageNumber = jobs.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Job j in jobs.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    JobInstance instance = new JobInstance() { job = j };
                    if (withDetails) { instance = getJobDetails(sco, j.Id); }
                    if (!jobInstanceDict.ContainsKey(instance.job.Id)) { jobInstanceDict.Add(instance.job.Id, instance); }
                }
            }

            JobInstance[] allJobInstances = new JobInstance[jobInstanceDict.Count];
            jobInstanceDict.Values.CopyTo(allJobInstances, 0);
            return allJobInstances;
        }
        /// <summary>
        /// Gets all Runbook Servers for an environment
        /// </summary>
        /// <param name="sco">Orchestrator Environment Pointer</param>
        /// <param name="loadJobs">load jobs or not</param>
        /// <returns>Array of runbook servers</returns>
        public static RunbookServerInst[] getAllRunbookServer(OrchestratorContext sco, bool loadJobs)
        {
            Dictionary<Guid, RunbookServerInst> runbookServerInstanceDict = new Dictionary<Guid, RunbookServerInst>();
            var runbookServer = (from rs in sco.RunbookServers
                        select rs);

            int pageNumber = runbookServer.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (RunbookServer r in runbookServer.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (!runbookServerInstanceDict.ContainsKey(r.Id))
                    {
                        if (loadJobs)
                        {
                            var jobs = (from jb in sco.Jobs
                                        where jb.RunbookServerId == r.Id
                                        && (jb.Status == "Running" || jb.Status == "Pending")
                                        select jb);

                            int jobCount = jobs.Count();
                            JobInstance[] jobArray = new JobInstance[jobCount];
                            int a = 0;

                            foreach (Job j in jobs)
                            {
                                jobArray[a] = new JobInstance() { job = j };
                                a++;
                            }

                            runbookServerInstanceDict.Add(r.Id, new RunbookServerInst() { server = r, countOfJobs = jobs.Count(), jobs = jobArray });
                        }
                        else
                        {
                            runbookServerInstanceDict.Add(r.Id, new RunbookServerInst() { server = r });
                        }
                    }
                }
            }

            RunbookServerInst[] AllRunbookServer = new RunbookServerInst[runbookServerInstanceDict.Count];
            runbookServerInstanceDict.Values.CopyTo(AllRunbookServer, 0);
            return AllRunbookServer;
        }

        /// <summary>
        /// Returns a runbook server
        /// </summary>
        /// <param name="sco"></param>
        /// <param name="runbookServerName">name of the runbook server to return</param>
        /// <param name="loadJobs">load jobs or not</param>
        /// <returns></returns>
        public static RunbookServerInst getRunbookServer(OrchestratorContext sco, string runbookServerName, bool loadJobs)
        {
            var runbookServer = (from rs in sco.RunbookServers
                                 where rs.Name == runbookServerName
                                 select rs).FirstOrDefault();

            if (loadJobs)
            {
                var jobs = (from jb in sco.Jobs
                            where jb.RunbookServerId == runbookServer.Id
                            && (jb.Status == "Running" || jb.Status == "Pending")
                            select jb);

                int jobCount = jobs.Count();
                JobInstance[] jobArray = new JobInstance[jobCount];
                int a = 0;

                foreach (Job j in jobs)
                {
                    jobArray[a] = new JobInstance() { job = j };
                    a++;
                }

                return new RunbookServerInst() { server = runbookServer, countOfJobs = jobs.Count(), jobs = jobArray };
            }
            else
            {
                return new RunbookServerInst() { server = runbookServer };
            }
        }

        /// <summary>
        /// Returns a runbook server
        /// </summary>
        /// <param name="sco"></param>
        /// <param name="runbookServerID">GUID of the runbook server to return</param>
        /// <param name="loadJobs">load Jobs or not</param>
        /// <returns></returns>
        public static RunbookServerInst getRunbookServer(OrchestratorContext sco, Guid runbookServerID, bool loadJobs)
        {
            var runbookServer = (from rs in sco.RunbookServers
                                 where rs.Id == runbookServerID
                                 select rs).FirstOrDefault();

            if (loadJobs)
            {
                var jobs = (from jb in sco.Jobs
                            where jb.RunbookServerId == runbookServer.Id
                            && (jb.Status == "Running" || jb.Status == "Pending")
                            select jb);

                int jobCount = jobs.Count();
                JobInstance[] jobArray = new JobInstance[jobCount];
                int a = 0;

                foreach (Job j in jobs)
                {
                    jobArray[a] = new JobInstance() { job = j };
                    a++;
                }

                return new RunbookServerInst() { server = runbookServer, countOfJobs = jobs.Count(), jobs = jobArray };
            }
            else { return new RunbookServerInst() { server = runbookServer }; }
        }
 
        public static Event[] getOrchestartorEvents(OrchestratorContext sco, DateTime minDate, DateTime maxDate)
        {
            Dictionary<Guid, Event> eventDictionary = new Dictionary<Guid, Event>();
            var eventCollection = (from ev in sco.Events
                                   where(ev.CreationTime > minDate && ev.CreationTime < maxDate)
                                   select ev);
            
            int pageNumber = eventCollection.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Event e in eventCollection.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (!eventDictionary.ContainsKey(e.Id))
                    {
                        eventDictionary.Add(e.Id, e);
                    }
                }
            }

            Event[] eventArray = new Event[eventDictionary.Count];
            eventDictionary.Values.CopyTo(eventArray, 0);
            return eventArray;
        }
        public static Event[] getOrchestartorEvents(OrchestratorContext sco, String summary)
        {
            Dictionary<Guid, Event> eventDictionary = new Dictionary<Guid, Event>();
            var eventCollection = (from ev in sco.Events
                                   where (ev.Summary.Equals(summary))
                                   select ev);

            int pageNumber = eventCollection.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Event e in eventCollection.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (!eventDictionary.ContainsKey(e.Id))
                    {
                        eventDictionary.Add(e.Id, e);
                    }
                }
            }

            Event[] eventArray = new Event[eventDictionary.Count];
            eventDictionary.Values.CopyTo(eventArray, 0);
            return eventArray;
        }
        /// <summary>
        /// Gets all Orchestrator Events based on filters
        /// </summary>
        /// <param name="sco">A connection to an Orchestrator Environment</param>
        /// <param name="minDate">The minimum age of an alert to retrieve</param>
        /// <param name="summaryArgument">The Summary Name Argument</param>
        /// <param name="summaryCriteria">How to use the Summary Name (Equals or Contains)</param>
        /// <returns></returns>
        public static Event[] getOrchestartorEvents(OrchestratorContext sco, DateTime minDate, DateTime maxDate, String summaryArgument, String summaryCriteria)
        {
            Dictionary<Guid, Event> eventDictionary = new Dictionary<Guid, Event>();
            IQueryable<Event> eventCollection;

            switch (summaryCriteria.ToUpper())
            {
                case "EQUALS":
                    eventCollection = (from ev in sco.Events
                                          where (ev.CreationTime > minDate && ev.CreationTime < maxDate && ev.Summary.Equals(summaryArgument))
                                           select ev);
                    break;
                case "CONTAINS":
                    eventCollection = (from ev in sco.Events
                                       where (ev.CreationTime > minDate && ev.CreationTime < maxDate && ev.Summary.Contains(summaryArgument))
                                       select ev);
                    break;
                default:
                    eventCollection = (from ev in sco.Events
                                       where (ev.CreationTime > minDate && ev.CreationTime < maxDate && ev.Summary.Equals(summaryArgument))
                                       select ev);
                    break;
            }
                

            int pageNumber = eventCollection.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Event e in eventCollection.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    if (!eventDictionary.ContainsKey(e.Id))
                    {
                        eventDictionary.Add(e.Id, e);
                    }
                }
            }

            Event[] eventArray = new Event[eventDictionary.Count];
            eventDictionary.Values.CopyTo(eventArray, 0);
            return eventArray;
        }
        public static String[] getOrchestartorEventSummaries(OrchestratorContext sco)
        {
            ArrayList eventSummaries = new ArrayList();
            var eventCollection = (from ev in sco.Events
                                   select ev);

            int pageNumber = eventCollection.Count() / INTERNAL_PAGE_SIZE;
            for (int i = 0; i <= pageNumber; i++)
            {
                foreach (Event e in eventCollection.Skip(INTERNAL_PAGE_SIZE * i).Take(INTERNAL_PAGE_SIZE))
                {
                    String summaryString = e.Summary;
                    if (!eventSummaries.Contains(summaryString))
                    {
                        eventSummaries.Add(summaryString);
                    }
                }
            }

            String[] summaryList = new String[eventSummaries.Count];
            eventSummaries.CopyTo(summaryList, 0);
            return summaryList;
        }
    }
}
