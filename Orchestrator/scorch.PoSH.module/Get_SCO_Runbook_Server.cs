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
    [Cmdlet(VerbsCommon.Get, "SCORunbookServer")]
    public class Get_SCO_Runbook_Server : Cmdlet
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
            Mandatory = false
        )]
        public string runbookServerName
        {
            get;
            set;
        }
        public Guid _runbookServerGUID = Guid.Empty;
        [Parameter(
            Position = 2,
            ValueFromPipeline = true,
            Mandatory = false
        )]
        public Guid runbookServerGUID
        {
            get { return _runbookServerGUID; }
            set { _runbookServerGUID = value; }
        }

        [Parameter(
            Position = 3,
            Mandatory = false
        )]
        public SwitchParameter loadJobs
        {
            get;
            set;
        }
        NetworkCredential _alternateCredentials = null;
        [Parameter(
            Position = 4,
            ValueFromPipeline = true,
            Mandatory = false
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
            if (string.IsNullOrEmpty(runbookServerName) && runbookServerGUID.Equals(Guid.Empty))
            {
                RunbookServerInst[] RunbookServers = SCOrch.getAllRunbookServer(sco, loadJobs.IsPresent);
                foreach (RunbookServerInst rs in RunbookServers)
                {
                    WriteObject(rs);
                }
            }
            else if (!string.IsNullOrEmpty(runbookServerName)) { WriteObject(SCOrch.getRunbookServer(sco, runbookServerName, loadJobs)); }
            else { WriteObject(SCOrch.getRunbookServer(sco, runbookServerGUID, loadJobs.IsPresent)); }
        }

        private OrchestratorContext setupOrchestratorConnection()
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(webserverURL));

            if (alternateCredentials == null) { sco.Credentials = CredentialCache.DefaultCredentials; }
            else { sco.Credentials = alternateCredentials; }
            sco.MergeOption = MergeOption.OverwriteChanges;

            return sco;
        }
    }
}
