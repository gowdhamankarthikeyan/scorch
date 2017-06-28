using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;

namespace OrchestratorIP.ReturnTypes
{
    [ActivityData("Job Parameter Information")]
    public class JobParameterArray
    {
        internal JobParameterArray(String Direction, String Value, String Name)
        {
            this.Direction = Direction;
            this.Value = Value;
            this.Name = Name;
        }
        [ActivityOutput, ActivityFilter]
        public String Direction
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Value
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
    }
}
