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
    [Cmdlet(VerbsCommon.Get, "SCOEvent")]
    public class Get_SCO_Event : Cmdlet
    {
        #region Parameters

        [Parameter(
            Position = 0,
            ParameterSetName = "webserverURL",
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
            Mandatory = true
        )]
        public DateTime minDate
        {
            get;
            set;
        }

        private DateTime _maxDate = DateTime.Now;
        [Parameter(
           Position = 2,
           Mandatory = false
       )]
        public DateTime maxDate
        {
            get { return _maxDate; }
            set { _maxDate = value; }
        }

        NetworkCredential _alternateCredentials = null;
        [Parameter(
            Position = 3,
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
            Event[] events = SCOrch.getOrchestartorEvents(sco, minDate, maxDate);
            foreach (Event e in events)
            {
                WriteObject(e);
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
    }
}
