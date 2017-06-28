using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Resource Pool")]
    public class ResourcePool
    {
        private String NumCpuShares = String.Empty;
        private String CpuReservationMHz = String.Empty;
        private String CpuExpandableReservation = String.Empty;
        private String CpuLimitMHz = String.Empty;
        private String NumMemShares = String.Empty;
        private String MemReservationMB = String.Empty;
        private String MemExpandableReservation = String.Empty;
        private String MemLimitMB = String.Empty;
        private String Id = String.Empty;
        private String Name = String.Empty;

        internal ResourcePool(String NumCpuShares, String CpuReservationMHz, String CpuExpandableReservation, String CpuLimitMHz, String NumMemShares, String MemReservationMB, String MemExpandableReservation, String MemLimitMB, String Id, String Name)
        {
            this.NumCpuShares = NumCpuShares;
            this.CpuReservationMHz = CpuReservationMHz;
            this.CpuExpandableReservation = CpuExpandableReservation;
            this.CpuLimitMHz = CpuLimitMHz;
            this.NumMemShares = NumMemShares;
            this.MemReservationMB = MemReservationMB;
            this.MemExpandableReservation = MemExpandableReservation;
            this.MemLimitMB = MemLimitMB;
            this.Id = Id;
            this.Name = Name;
        }

        [ActivityOutput, ActivityFilter]
        public String Num_Cpu_Shares
        {
            get { return NumCpuShares; }
        }
        [ActivityOutput, ActivityFilter]
        public String Cpu_Reservation_MHz
        {
            get { return CpuReservationMHz; }
        }
        [ActivityOutput, ActivityFilter]
        public String Cpu_Expandable_Reservation
        {
            get { return CpuExpandableReservation; }
        }
        [ActivityOutput, ActivityFilter]
        public String Cpu_Limit_MHz
        {
            get { return CpuLimitMHz; }
        }
        [ActivityOutput, ActivityFilter]
        public String Num_Mem_Shares
        {
            get { return NumMemShares; }
        }
        [ActivityOutput, ActivityFilter]
        public String Mem_Reservation_MB
        {
            get { return MemReservationMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Mem_Expandable_Reservation
        {
            get { return MemExpandableReservation; }
        }
        [ActivityOutput, ActivityFilter]
        public String Mem_Limit_MB
        {
            get { return MemLimitMB; }
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

