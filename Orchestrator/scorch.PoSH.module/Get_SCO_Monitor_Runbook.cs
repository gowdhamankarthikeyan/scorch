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
    [Cmdlet(VerbsCommon.Get, "SCOMonitorRunbook", DefaultParameterSetName="all")]
    public class Get_SCO_Monitor_Runbook : PSCmdlet
    {
        #region Parameters

        [Parameter(
            Position = 0,
            ParameterSetName = "all",
            Mandatory = true
        )]
        [Parameter(
            Position = 0,
            ParameterSetName = "path",
            Mandatory = true
        )]
        [Parameter(
            Position = 0,
            ParameterSetName = "guid",
            Mandatory = true
        )]
        [Parameter(
            Position = 0,
            ParameterSetName = "folder",
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
            ParameterSetName="path",
            Mandatory = true
        )]
        public string folderPath
        {
            get;
            set;
        }

        [Parameter(
            Position = 1,
            ValueFromPipeline = true,
            ParameterSetName = "guid",
            Mandatory = true
        )]
        public Guid folderGUID
        {
            get;
            set;
        }
        [Parameter(
           Position = 1,
           ValueFromPipeline = true,
           ParameterSetName = "folder",
           Mandatory = true
       )]
        public Folder folder
        {
            get;
            set;
        }
        [Parameter(
            Position = 2,
            ValueFromPipeline = true,
            Mandatory = false
        )]
        public SwitchParameter loadParameterData
        {
            get;
            set;
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
            switch (ParameterSetName)
            {
                case "folder":
                    foreach (Runbook rb in SCOrch.getMonitorRunbook(sco, folder, loadParameterData.IsPresent)) { WriteObject(rb); }
                    break;
                case "guid":
                    foreach (Runbook rb in SCOrch.getMonitorRunbook(sco, folderGUID, loadParameterData.IsPresent)) { WriteObject(rb); }
                    break;
                case "path":
                    foreach (Runbook rb in SCOrch.getMonitorRunbook(sco, folderPath, loadParameterData.IsPresent)) { WriteObject(rb); }
                    break;
                case "all":
                    foreach (Runbook rb in SCOrch.getMonitorRunbook(sco, loadParameterData.IsPresent)) { WriteObject(rb); }
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
    }
}
