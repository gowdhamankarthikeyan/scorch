using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;


namespace OrchestratorIP.ReturnTypes
{
    [ActivityData("Event Instance")]
    public class EventInst
    {
        internal EventInst(OrchestratorInterop.SCOrchestrator.Event eventInst)
        {
            try { CreationTime = eventInst.CreationTime; }
            catch { CreationTime = DateTime.MinValue; }
            try { Description = eventInst.Description; }
            catch { Description = String.Empty; }
            try { Id = eventInst.Id.ToString(); }
            catch { Id = String.Empty; }
            try { SourceId = eventInst.SourceId.ToString(); }
            catch { SourceId = String.Empty; }
            try { SourceName = eventInst.SourceName; }
            catch { SourceName = String.Empty; }
            try { Summary = eventInst.Summary; }
            catch { Summary = String.Empty; }
            try { Type = eventInst.Type; }
            catch { Type = String.Empty; }
        }

        [ActivityOutput]
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
        public String Id
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String SourceId
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String SourceName
        {
            get;
            set;
        }
        [ActivityOutput]
        public String Summary
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Type
        {
            get;
            set;
        }
    }
}