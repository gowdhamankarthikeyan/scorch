using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Utilities.SystemUtilities
{
    [Activity("Sleep")]
    public class Sleep : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Sleep Seconds");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            int sleepSeconds = request.Inputs["Sleep Seconds"].AsInt32();

            Thread.Sleep(TimeSpan.FromSeconds(sleepSeconds));

            response.Publish("Sleep Minutes", sleepSeconds);
        }
    }
}

