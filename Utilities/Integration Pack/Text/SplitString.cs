using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Text
{
    [Activity]
    public class SplitString
    {
        private string text;
        private string separators;
        private StringSplitOptions options;
        private int index;

        [ActivityInput]
        public string Text
        {
            set { text = value; }
        }

        [ActivityInput]
        public string Separators
        {
            set { separators = value; }
        }

        [ActivityInput]
        public int Index
        {
            set { index = value; }
        }

        [ActivityInput("Remove Empty Entries", Default = true)]
        public bool RemoveEmpty
        {
            set { options = value ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None; }
        }

        [ActivityOutput, ActivityFilter]
        public string Item
        {
            get 
            {
                string[] retValue = text.Split(separators.ToCharArray(), options);
                return retValue[index];
            }
        }
    }
}

