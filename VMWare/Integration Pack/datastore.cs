using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Datastore")]
    public class datastore
    {
        private String capacityMB = String.Empty;
        private String FreeSpaceMB = String.Empty;
        private String ParentFolderId = String.Empty;
        private String DatacenterId = String.Empty;
        private String id = String.Empty;
        private String name = String.Empty;

        internal datastore(String capacityMB, String FreeSpaceMB, String ParentFolderId, String DatacenterId, String id, String name)
        {
            this.capacityMB = capacityMB;
            this.FreeSpaceMB = FreeSpaceMB;
            this.ParentFolderId = ParentFolderId;
            this.DatacenterId = DatacenterId;
            this.id = id;
            this.name = name;
        }

        [ActivityOutput, ActivityFilter]
        public String Capacity_MB
        {
            get { return capacityMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Free_Space_MB
        {
            get { return FreeSpaceMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Parent_Folder_Id
        {
            get { return ParentFolderId; }
        }
        [ActivityOutput, ActivityFilter]
        public String Datacenter_ID
        {
            get { return DatacenterId; }
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

