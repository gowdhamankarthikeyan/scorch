using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;

namespace OrchestratorIP
{
    [ActivityData("Runbook Instance")]
    class RunbookInst
    {
        internal RunbookInst(OrchestratorInterop.SCOrchestrator.Runbook runbook)
        {
            CheckedOutBy = runbook.CheckedOutBy;
            try { CheckedOutTime = Convert.ToDateTime(runbook.CheckedOutTime); }
            catch { CheckedOutTime = DateTime.MinValue; }
            CreatedBy = runbook.CreatedBy;
            CreationTime = runbook.CreationTime;
            Description = runbook.Description;
            FolderId = runbook.FolderId.ToString();
            Id = runbook.Id.ToString();
            IsMonitor = runbook.IsMonitor;
            LastModifiedBy = runbook.LastModifiedBy;
            LastModifiedTime = runbook.LastModifiedTime;
            Name = runbook.Name;
            Path = runbook.Path;
        }
        [ActivityOutput, ActivityFilter]
        public String CheckedOutBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime CheckedOutTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String CreatedBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime CreationTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Description
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String FolderId
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public Boolean IsMonitor
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String LastModifiedBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime LastModifiedTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Path
        {
            get;
            set;
        }
    }
}
