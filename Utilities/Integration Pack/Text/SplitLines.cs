using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Text
{
    [ActivityData("Line")]
    public class Line
    {
        private String line = String.Empty;

        internal Line(String line)
        {
            this.line = line;
        }

        [ActivityOutput,ActivityFilter]
        public String OutputLine
        {
            get { return line; }
        }
    }

    [Activity("Split Lines",Description="Spawns new workflow for each line in passed text")]
    public class SplitLines : IActivity
    {
        private String inputString;

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Input String");
            designer.AddCorellatedData(typeof(Line));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            inputString = request.Inputs["Input String"].AsString();
            try
            {
                response.PublishRange(getLines());
            }
            catch (Exception ex)
            {
                response.ReportErrorEvent("Failed to parse line", ex.ToString());
            }
        }

        private IEnumerable<Line> getLines()
        {
            String[] inputStringArray = inputString.Split('\n');
            foreach (String outputString in inputStringArray)
            {
                yield return new Line(outputString);
            }     
        }
    }
}

