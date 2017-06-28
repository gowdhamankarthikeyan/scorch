using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare HardDisk")]
    public class harddisk
    {
        private String capacityKB = String.Empty;
        private String diskType = String.Empty;
        private String filename = String.Empty;
        private String isAttachedToVM = String.Empty;
        private String persistence = String.Empty;
        private String storageFormat = String.Empty;
        private String name = String.Empty;
        
        internal harddisk(PSObject obj)
        {
            this.diskType = "Not Set";
            this.filename = "Not Set";
            this.isAttachedToVM = "Not Set";
            this.name = "Not Set";
            this.persistence = "Not Set";
            this.storageFormat = "Not Set";

            try
            {
                if (obj.Members["capacityKB"].Value != null) { this.capacityKB = obj.Members["capacityKB"].Value.ToString(); }
                if (obj.Members["diskType"].Value != null) { this.diskType = obj.Members["diskType"].Value.ToString(); }
                if (obj.Members["filename"].Value != null) { this.filename = obj.Members["filename"].Value.ToString(); }
                if (obj.Members["persistence"].Value != null) { this.persistence = obj.Members["persistence"].Value.ToString(); }
                if (obj.Members["storageFormat"].Value != null) { this.storageFormat = obj.Members["storageFormat"].Value.ToString(); }
                if (obj.Members["name"].Value != null) { this.name = obj.Members["name"].Value.ToString(); }
            }
            catch {}
        }

        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }

        [ActivityOutput, ActivityFilter]
        public String Capacity_KB
        {
            get { return capacityKB; }
        }
        [ActivityOutput, ActivityFilter]
        public String disk_Type
        {
            get { return diskType; }
        }
        [ActivityOutput, ActivityFilter]
        public String Filename
        {
            get { return filename; }
        }
        [ActivityOutput, ActivityFilter]
        public String is_Attached_To_VM
        {
            get { return isAttachedToVM; }
        }
        [ActivityOutput, ActivityFilter]
        public String Persistence
        {
            get { return persistence; }
        }
        [ActivityOutput, ActivityFilter]
        public String storage_Format
        {
            get { return storageFormat; }
        }
    }
}

