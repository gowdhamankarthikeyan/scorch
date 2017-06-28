using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchestratorInterop.SCOrchestrator;

namespace OrchestratorInterop.Data_Class
{
    public class RunbookServerInst
    {
        public RunbookServer server { get; set; }
        public int countOfJobs { get; set; }
        public JobInstance[] jobs { get; set; }
    }
}
