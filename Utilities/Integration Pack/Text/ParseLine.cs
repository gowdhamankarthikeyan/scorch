using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Text
{
    [Activity("Select Individual Line",Description="0 indexed",ShowFilters=false)]
    public class ParseLine : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Input String");
            designer.AddInput("Line");
            designer.AddOutput("Output Line").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String inputString = request.Inputs["Input String"].AsString();
            int line = request.Inputs["Line"].AsInt32();
            String[] outputStringArray = inputString.Split('\n');
            response.Publish("Output Line", outputStringArray[line]);
        }
    }
}

