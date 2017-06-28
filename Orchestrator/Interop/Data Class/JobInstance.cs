using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchestratorInterop.SCOrchestrator;

namespace OrchestratorInterop.Data_Class
{
    public class JobInstance
    {
        public Job job { get; set; }
        public Dictionary<string, string> InputParameters { get; set; }
        public Dictionary<string, string> OutputParameters { get; set; }
        public int ActiveInstances { get; set; }
    }
}
