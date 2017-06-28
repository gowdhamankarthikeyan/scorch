using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Snapshot")]
    public class snapshot
    {
        private String Created = String.Empty;
        private String Quiesced = String.Empty;
        private String PowerState = String.Empty;
        private String VMId = String.Empty;
        private String SizeMB = String.Empty;
        private String IsCurrent = String.Empty;
        private String IsReplaySupported = String.Empty;
        private String Id = String.Empty;
        private String Name = String.Empty;
        private String Description = String.Empty;
        internal snapshot(String Created, String Quiesced, String PowerState, String VMId, String SizeMB, String IsCurrent, String IsReplaySupported, String Id, String Name, String Description)
        {
            this.Created = Created;
            this.Quiesced = Quiesced;
            this.PowerState = PowerState;
            this.VMId = VMId;
            this.SizeMB = SizeMB;
            this.IsCurrent = IsCurrent;
            this.IsReplaySupported = IsReplaySupported;
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }

        [ActivityOutput, ActivityFilter]
        public String created
        {
            get { return Created; }
        }
        [ActivityOutput, ActivityFilter]
        public String quiesced
        {
            get { return Quiesced; }
        }
        [ActivityOutput, ActivityFilter]
        public String Power_State
        {
            get { return PowerState; }
        }
        [ActivityOutput, ActivityFilter]
        public String VM_Id
        {
            get { return VMId; }
        }
        [ActivityOutput, ActivityFilter]
        public String Size_MB
        {
            get { return SizeMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Is_Current
        {
            get { return IsCurrent; }
        }
        [ActivityOutput, ActivityFilter]
        public String Is_Replay_Supported
        {
            get { return IsReplaySupported; }
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
        [ActivityOutput, ActivityFilter]
        public String description
        {
            get { return Description; }
        }
    }
}

