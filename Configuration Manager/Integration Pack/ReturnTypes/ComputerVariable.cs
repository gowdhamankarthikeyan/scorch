using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCMInterop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;


namespace SCCMExtension
{
    [ActivityData("Computer Variable")]
    public class ComputerVariable
    {
        private String VariableName = String.Empty;
        private String MachineName = String.Empty;
        private String VariableValue = String.Empty;

        internal ComputerVariable(String VariableName, String MachineName, String VariableValue)
        {
            this.VariableName = VariableName;
            this.MachineName = MachineName;
            this.VariableValue = VariableValue;
        }

        [ActivityOutput, ActivityFilter]
        public String Variable_Name
        {
            get { return VariableName; }
        }

        [ActivityOutput, ActivityFilter]
        public String Machine_Name
        {
            get { return MachineName; }
        }

        [ActivityOutput, ActivityFilter]
        public String Variable_Value
        {
            get { return VariableValue; }
        }
    }
}

