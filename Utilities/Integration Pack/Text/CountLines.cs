using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Text
{
    [Activity("Count Lines",ShowFilters = false)]
    public class CountLines : IActivity
    {
        private bool ignoreBlankLines = true;
        public void Design(IActivityDesigner designer)
        {
            bool[] Options = new bool[2];
            Options[0] = true;
            Options[1] = false;

            designer.AddInput("Input String");
            designer.AddInput("Ignore Blank Lines").WithDefaultValue(true).WithListBrowser(Options);
            designer.AddOutput("Number of Lines").AsNumber();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String inputString = request.Inputs["Input String"].AsString();
            ignoreBlankLines = request.Inputs["Ignore Blank Lines"].AsBoolean();
            String[] outputStringArray = inputString.Split('\n');
            if (ignoreBlankLines)
            {
                int count = 0;
                foreach (string line in outputStringArray)
                {
                    if (!line.Equals(string.Empty))
                    {
                        count++;
                    }
                }
                response.Publish("Number of Lines", count);
            }
            else
            {
                response.Publish("Number of Lines", outputStringArray.Length);
            }
        }
    }
}

