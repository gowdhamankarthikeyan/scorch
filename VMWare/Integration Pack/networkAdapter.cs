using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Network Adapter")]
    public class networkAdapter
    {
        private String MacAddress = String.Empty;
        private String WakeOnLanEnabled = String.Empty;
        private String NetworkName = String.Empty;
        private String ParentId = String.Empty;
        private String Id = String.Empty;
        private String Name = String.Empty;

        internal networkAdapter(String MacAddress, String WakeOnLanEnabled, String NetworkName, String ParentId, String Id, String Name)
        {
            this.MacAddress = MacAddress;
            this.WakeOnLanEnabled = WakeOnLanEnabled;
            this.NetworkName = NetworkName;
            this.ParentId = ParentId;
            this.Id = Id;
            this.Name = Name;
        }

        [ActivityOutput, ActivityFilter]
        public String Mac_Address
        {
            get { return MacAddress; }
        }
        [ActivityOutput, ActivityFilter]
        public String Wake_On_Lan_Enabled
        {
            get { return WakeOnLanEnabled; }
        }
        [ActivityOutput, ActivityFilter]
        public String Network_Name
        {
            get { return NetworkName; }
        }
        [ActivityOutput, ActivityFilter]
        public String Parent_Id
        {
            get { return ParentId; }
        }
        [ActivityOutput, ActivityFilter]
        public String id
        {
            get { return Id; }
        }
        [ActivityOutput, ActivityFilter]
        public String name
        {
            get { return Name; }
        }
    }
}

