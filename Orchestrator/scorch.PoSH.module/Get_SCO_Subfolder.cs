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
    [Cmdlet(VerbsCommon.Get, "SCOSubfolder")]
    public class Get_SCO_Subfolder : Cmdlet
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
            ValueFromPipeline = true
        )]
        public string FolderPath
        {
            get;
            set;
        }

        [Parameter(
            Position = 2,
            Mandatory = false
        )]
        public SwitchParameter loadRunbooks
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
            foreach (Folder fol in SCOrch.getSubFolders(sco, FolderPath, loadRunbooks.IsPresent))
            {
                WriteObject(fol);
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
