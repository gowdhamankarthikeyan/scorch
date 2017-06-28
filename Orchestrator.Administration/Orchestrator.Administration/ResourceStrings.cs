using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class ResourceFolderRoot
    {
        public const string Runbooks   = "{00000000-0000-0000-0000-000000000000}";
        public const string Computers = "{00000000-0000-0000-0000-000000000001}";
        //                   ?????????  = "{00000000-0000-0000-0000-000000000002}";
        //                   ?????????  = "{00000000-0000-0000-0000-000000000003}";
        public const string Counters = "{00000000-0000-0000-0000-000000000004}";
        public const string Variables = "{00000000-0000-0000-0000-000000000005}";
        public const string Schedules = "{00000000-0000-0000-0000-000000000006}";
        public const string Satellites = "{00000000-0000-0000-0000-000000000007}";
    }
    public enum globalType
    {
        Variable,
        Schedule,
        Counter,
        ComputerGroup,
        GlobalConfig
    }
    public enum objectTypes
    {
        Activity,
        Counter,
        Folder,
        Resource,
        Runbook,
        Schedule,
        Variable,
        ComputerGroups
    }
    public enum foundStai
    {
        foundGuid,
        foundName,
        notFound
    }
    public enum deletionStati
    {
        NotFound,
        Deleted,
        Active
    }
    public class DataTypeDelimiters
    {
        public const string PublishedData = @"\`d.T.~Ed/";
        public const string Formatted     = @"\`~F/";
        public const string Encrypted     = @"\`d.T.~Ec/";
    }
    public class ResourceType
    {
        public static string Variable   = "{2E88BB5A-62F9-482E-84B0-4D963C987231}";
        public static string Counter    = "{0BABBCF6-C702-4F02-9BA6-BAB75983A06A}";
        public static string Schedule   = "{4386DA28-C311-4A2B-8C47-3C7BB9D66B51}";
        public static string Computer   = "{162204B6-7F54-4CB9-A678-B94A6510BD0C}";
        public static string Satellite  = "{155D5068-BFF4-4054-AD01-19403371FAD2}";
        /*
         * WAIT_OBJECTTYPE              =  '{B40FDFBD-6E5F-44F0-9AA6-6469B0A35710}"
         * CUSTOM_START_OBJECTTYPE      = '{6C576F3D-E927-417A-B145-5D3EFF9C995F}";
         * LINK_OBJECTTYPE              = '{7A65BD17-9532-4D07-A6DA-E0F89FA0203E}";
         * COUNTER_GET_OBJECTTYPE       = '{4E753C05-1A1F-4350-B572-09AE196AB593}";
         * COUNTER_SET_OBJECTTYPE       = '{D2259B53-4C86-4A58-B40B-7493FC182E02}";
         * COUNTER_MONITOR_OBJECTTYPE   = '{15A1CEB4-16D8-4AD7-A54B-8162A309439C}";
         * CHECK_SCHEDULE_OBJECTTYPE    = '{0B807C4B-41C3-4517-B24E-7D98F016AD1C}";
         * TRIGGER_POLICY_OBJECTTYPE    = '{9C1BF9B4-515A-4FD2-A753-87D235D8BA1F}";
         * NOTEOBJECT_GUID			    = '{AB1D2E56-3842-4184-A9AF-DFBB99115D26}";
         * JUNCTION_TYPE                = '{1C5F9236-92E0-4795-8CAA-1669B7643607}";
         */
    }

    public class StringValues
    {
        public const string policyRootString = "Policies";

        public static string configurationNodePath = @"ExportData/GlobalConfigurations";
        public static string countersNodePath = @"ExportData/GlobalSettings/Counters/Folder";
        public static string variablesNodePath = @"ExportData/GlobalSettings/Variables/Folder";
        public static string schedulesNodePath = @"ExportData/GlobalSettings/Schedules/Folder";
        public static string computerGroupsNodePath = @"ExportData/GlobalSettings/ComputerGroups/Folder";
        public static string policiesFolderPath = @"ExportData/Policies/Folder";
        public static string CommonData = "LogCommonData";
        public static string SpecificData = "LogSpecificData";
    }

    public class OISExportString
    {
        public static string emptyXMLStructure = String.Format(
                                                                "<ExportData>" +
                                                                "  <Policies>" +
                                                                "  </Policies>" +
                                                                "  <GlobalSettings>" +
                                                                "    <Counters>" +
                                                                "      <Folder>" +
                                                                "        <UniqueID>{0}</UniqueID>" +
                                                                "        <Objects />" +
                                                                "      </Folder>" +
                                                                "    </Counters>" +
                                                                "    <Variables>" +
                                                                "      <Folder>" +
                                                                "        <UniqueID>{1}</UniqueID>" +
                                                                "        <Objects />" +
                                                                "      </Folder>" +
                                                                "    </Variables>" +
                                                                "    <ComputerGroups>" +
                                                                "      <Folder>" +
                                                                "        <UniqueID>{2}</UniqueID>" +
                                                                "        <Objects />" +
                                                                "      </Folder>" +
                                                                "    </ComputerGroups>" +
                                                                "    <Schedules>" +
                                                                "      <Folder>" +
                                                                "        <UniqueID>{3}</UniqueID>" +
                                                                "        <Objects />" +
                                                                "      </Folder>" +
                                                                "    </Schedules>" +
                                                                "  </GlobalSettings>" +
                                                                "  <GlobalConfigurations>" +
                                                                "  </GlobalConfigurations>" +
                                                                "</ExportData>", 
                                                                ResourceFolderRoot.Counters, 
                                                                ResourceFolderRoot.Variables, 
                                                                ResourceFolderRoot.Computers, 
                                                                ResourceFolderRoot.Schedules
                                                               );
        public static string resourceFolderStructure = String.Format(
                                                                "<Folder>" +
                                                                "  <UniqueID></UniqueID>" +
                                                                "  <Objects>" +
                                                                "  </Objects>" +
                                                                "</Folder>"
                                                               );
        public static string globalConfigurationStructure= String.Format(
                                                                "<Entry>" +
                                                                "  <ID></ID>" +
                                                                "  <Data>" +
                                                                "  </Data>" +
                                                                "</Entry>"
                                                               );
    }
}
