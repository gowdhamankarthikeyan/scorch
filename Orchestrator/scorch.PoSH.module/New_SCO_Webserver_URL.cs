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
    [Cmdlet(VerbsCommon.New, "SCOWebserverURL")]
    public class New_SCO_Webserver_URL : Cmdlet
    {
        #region Parameters

        [Parameter(
            Position = 0,
            Mandatory = true
        )]
        public string ServerName
        {
            get;
            set;
        }

        string _port = "81";
        [Parameter(
            Position = 1,
            Mandatory = false
        )]
        public string port
        {
            get { return _port; }
            set { _port = value; }
        }

        bool _useSSL = false;
        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public bool useSSL
        {
            get { return _useSSL; }
            set { _useSSL = value; }
        }
        
        #endregion

        protected override void ProcessRecord()
        {
            if (useSSL) { WriteObject(string.Format(@"https://{0}:{1}/Orchestrator2012/Orchestrator.svc", ServerName, port)); }
            else { WriteObject(string.Format(@"http://{0}:{1}/Orchestrator2012/Orchestrator.svc", ServerName, port)); }
        }
    }
}
