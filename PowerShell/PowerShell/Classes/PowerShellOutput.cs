using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace SCORCHDev.PowerShell.Classes
{ 
    [ActivityData("PowerShell Output")]
    public class PowerShellOutput
    {
        public PowerShellOutput(PSPropertyInfo PSInfo)
        {
            try { this.propertyName = PSInfo.Name; }
            catch { this.propertyName = "Error"; }

            try { this.propertyValue = PSInfo.Value.ToString(); }
            catch { this.propertyValue = "Error"; }
        }

        [ActivityOutput, ActivityFilter]
        public string propertyName { get; set; }
        [ActivityOutput, ActivityFilter]
        public string propertyValue { get; set; }
    }
}

