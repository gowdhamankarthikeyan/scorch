using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Virtual Machine")]
    public class vm
    {
        private String PowerState = String.Empty;
        private String VMVersion = String.Empty;
        private String Description = String.Empty;
        private String Notes = String.Empty;
        private String NumCpu = String.Empty;
        private String MemoryMB = String.Empty;
        private String HostId = String.Empty;
        private String FolderId = String.Empty;
        private String ResourcePoolId = String.Empty;
        private String UsedSpaceGB = String.Empty;
        private String ProvisionedSpaceGB = String.Empty;
        private String id = String.Empty;
        private String name = String.Empty;

        internal vm(String PowerState, String VMVersion, String Description, String Notes, String NumCpu, String MemoryMB, String HostId, String FolderId, String ResourcePoolId, String UsedSpaceGB, String ProvisionedSpaceGB, String id, String name)
        {
            this.PowerState = PowerState;
            this.VMVersion = VMVersion;
            this.Description = Description;
            this.Notes = Notes;
            this.NumCpu = NumCpu;
            this.MemoryMB = MemoryMB;
            this.HostId = HostId;
            this.FolderId = FolderId;
            this.ResourcePoolId = ResourcePoolId;
            this.UsedSpaceGB = UsedSpaceGB;
            this.ProvisionedSpaceGB = ProvisionedSpaceGB;
            this.id = id;
            this.name = name;
        }

        [ActivityOutput, ActivityFilter]
        public String Power_State
        {
            get { return PowerState; }
        }
        [ActivityOutput, ActivityFilter]
        public String VM_Version
        {
            get { return VMVersion; }
        }
        [ActivityOutput, ActivityFilter]
        public String description
        {
            get { return Description; }
        }
        [ActivityOutput, ActivityFilter]
        public String notes
        {
            get { return Notes; }
        }
        [ActivityOutput, ActivityFilter]
        public String Num_Cpu
        {
            get { return NumCpu; }
        }
        [ActivityOutput, ActivityFilter]
        public String Memory_MB
        {
            get { return MemoryMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Host_Id
        {
            get { return HostId; }
        }
        [ActivityOutput, ActivityFilter]
        public String Folder_Id
        {
            get { return FolderId; }
        }
        [ActivityOutput, ActivityFilter]
        public String Resource_Pool_Id
        {
            get { return ResourcePoolId; }
        }
        [ActivityOutput, ActivityFilter]
        public String Used_Space_GB
        {
            get { return UsedSpaceGB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Provisioned_Space_GB
        {
            get { return ProvisionedSpaceGB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get { return id; }
        }
    }
}

