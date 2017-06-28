using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("VMWare Host")]
    public class vmHost
    {
        private String ConnectionState = String.Empty;
        private String PowerState = String.Empty;
        private String isStandalone = String.Empty;
        private String Manufacturer = String.Empty;
        private String Model = String.Empty;
        private String NumCpu = String.Empty;
        private String MemoryTotalMB = String.Empty;
        private String MemoryUsageMB = String.Empty;
        private String ProcessorType = String.Empty;
        private String HyperthreadingActive = String.Empty;
        private String TimeZone = String.Empty;
        private String Version = String.Empty;
        private String Build = String.Empty;
        private String id = String.Empty;
        private String name = String.Empty;

        internal vmHost(String ConnectionState, String PowerState, String isStandalone, String Manufacturer, String Model, String NumCpu, String MemoryTotalMB, String MemoryUsageMB, String ProcessorType, String HyperthreadingActive, String TimeZone, String Version, String Build, String id, String name)
        {
            this.ConnectionState = ConnectionState;
            this.PowerState = PowerState;
            this.isStandalone = isStandalone;
            this.Manufacturer = Manufacturer;
            this.Model = Model;
            this.NumCpu = NumCpu;
            this.MemoryTotalMB = MemoryTotalMB;
            this.MemoryUsageMB = MemoryUsageMB;
            this.ProcessorType = ProcessorType;
            this.HyperthreadingActive = HyperthreadingActive;
            this.TimeZone = TimeZone;
            this.Version = Version;
            this.Build = Build;
            this.id = id;
            this.name = name;
        }

        [ActivityOutput, ActivityFilter]
        public String Connection_State
        {
            get { return ConnectionState; }
        }
        [ActivityOutput, ActivityFilter]
        public String Power_State
        {
            get { return PowerState; }
        }
        [ActivityOutput, ActivityFilter]
        public String is_Standalone
        {
            get { return isStandalone; }
        }
        [ActivityOutput, ActivityFilter]
        public String manufacturer
        {
            get { return Manufacturer; }
        }
        [ActivityOutput, ActivityFilter]
        public String model
        {
            get { return Model; }
        }
        [ActivityOutput, ActivityFilter]
        public String Num_Cpu
        {
            get { return NumCpu; }
        }
        [ActivityOutput, ActivityFilter]
        public String Memory_Total_MB
        {
            get { return MemoryTotalMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Memory_Usage_MB
        {
            get { return MemoryUsageMB; }
        }
        [ActivityOutput, ActivityFilter]
        public String Processor_Type
        {
            get { return ProcessorType; }
        }
        [ActivityOutput, ActivityFilter]
        public String Time_Zone
        {
            get { return TimeZone; }
        }
        [ActivityOutput, ActivityFilter]
        public String Hyperthreading_Active
        {
            get { return HyperthreadingActive; }
        }
        [ActivityOutput, ActivityFilter]
        public String version
        {
            get { return Version; }
        }
        [ActivityOutput, ActivityFilter]
        public String build
        {
            get { return Build; }
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get { return id; }
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get { return name; }
        }
    }

}

