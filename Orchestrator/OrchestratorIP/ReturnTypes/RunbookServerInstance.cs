using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;


namespace OrchestratorIP.ReturnTypes
{
    [ActivityData("Runbook Server Instance")]
    public class RunbookServerInstance
    {
        internal RunbookServerInstance(RunbookServerInst rs)
        {
            this.Id = String.Empty;
            this.Name = String.Empty;
            this.NumberOfJobs = 0;

            try
            {
                this.Id = rs.server.Id.ToString();
            }
            catch { }
            try
            {
                this.Name = rs.server.Name;
            }
            catch { }
            try
            {
                this.NumberOfJobs = rs.countOfJobs;
            }
            catch { }
        }

        [ActivityOutput, ActivityFilter]
        public String Id
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
        public int NumberOfJobs
        {
            get;
            set;
        }
    }
}