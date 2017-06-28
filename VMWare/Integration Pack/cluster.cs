using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Cluster")]
    public class cluster
    {
        private String HAEnabled = String.Empty;
        private String HAAdmissionControlEnabled = String.Empty;
        private String HAFailoverLevel = String.Empty;
        private String HARestartPriority = String.Empty;
        private String HAIsolationResponse = String.Empty;
        private String VMSwapFilePolicy = String.Empty;
        private String DrsEnabled = String.Empty;
        private String DrsMode = String.Empty;
        private String DrsAutomationLevel = String.Empty;
        private String Id = String.Empty;
        private String Name = String.Empty;

        internal cluster(String HAEnabled, String HAAdmissionControlEnabled, String HAFailoverLevel, String HARestartPriority, String HAIsolationResponse, String VMSwapFilePolicy, String DrsEnabled, String DrsMode, String DrsAutomationLevel, String Id, String Name)
        {
            this.HAEnabled = HAEnabled;
            this.HAAdmissionControlEnabled = HAAdmissionControlEnabled;
            this.HAFailoverLevel = HAFailoverLevel;
            this.HARestartPriority = HARestartPriority;
            this.HAIsolationResponse = HAIsolationResponse;
            this.VMSwapFilePolicy = VMSwapFilePolicy;
            this.DrsEnabled = DrsEnabled;
            this.DrsMode = DrsMode;
            this.DrsAutomationLevel = DrsAutomationLevel;
            this.Id = Id;
            this.Name = Name;
        }

        [ActivityOutput, ActivityFilter]
        public String HA_Enabled
        {
            get { return HAEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String HA_AdmissionControlEnabled
        {
            get { return HAAdmissionControlEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String HA_FailoverLevel
        {
            get { return HAFailoverLevel; }
        }
        [ActivityOutput, ActivityFilter]
        public String HA_RestartPriority
        {
            get { return HARestartPriority; }
        }
        [ActivityOutput, ActivityFilter]
        public String HA_IsolationResponse
        {
            get { return HAIsolationResponse; }
        }
        [ActivityOutput, ActivityFilter]
        public String VM_SwapFilePolicy
        {
            get { return VMSwapFilePolicy; }
        }
        [ActivityOutput, ActivityFilter]
        public String DRSEnabled
        {
            get { return DrsEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String DRSMode
        {
            get { return DrsMode; }
        }
        [ActivityOutput, ActivityFilter]
        public String DRSAutomationLevel
        {
            get { return DrsAutomationLevel; }
        }
        [ActivityOutput, ActivityFilter]
        public String name
        {
            get { return Name; }
        }
        [ActivityOutput, ActivityFilter]
        public String id
        {
            get { return Id; }
        }
    }
}

