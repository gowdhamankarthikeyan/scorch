using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchestrator.Administration.IntegrationPack
{
    class ResourceStrings
    {
        public static string runbookPath = @"Runbook Path";
        public static string runbookPathDefaultValue = @"Policies\FolderName\SubFolder";
        
        public static string savePath = @"Save Path";
        public static string savePathDefaultValue = @"C:\folder\Subfolder\export_name.ois_export";

        public static string exportFilePath = @"Export File Path";
        public static string loadExportData = "Load Export Runbook Data";
        
        public static string loadGlobalConfigurationData = "Load Global Configuration Data";
        public static string cleanGlobalConfigurationData = "Clean Global Configuration Data";

        public static string loadGlobalVariableData = "Load Global Variable Data";
        public static string cleanGlobalVariableData = "Clean Global Variable Data";

        public static string loadGlobalScheduleData = "Load Global Schedule Data";
        public static string cleanGlobalScheduleData = "Clean Global Schedule Data";

        public static string loadGlobalComputerGroupData = "Load Global Computer Group Data";
        public static string cleanGlobalComputerGroupData = "Clean Global Computer Group Data";
        
        public static string loadGlobalCounter = "Load Global Counter Data";
        public static string cleanGlobalCounter = "Clean Global Counter Data";
        
        public static string overwriteExistingExport = "Overwrite Existing Export File";
        
        public static string[] trueFalse = new string[2] { "True", "False" };
        public static string[] trueFalseDoNotModify = new string[3] { "True", "False", "Do Not Modify" };
        public const string t = "True";
        public const string f = "False";
        public const string doNotModify = "Do Not Modify";

        public static string on = "On";
        public static string off = "Off";

        public static string targetFolderPath = "Target Folder Path";
        public static string targetFolderPathDefaultValue = @"Policies\FolderName\SubFolder";

        public static string numberOfFolders = "Number of Folders";

        public static string applyLinkBestPractices = "Apply Link Best Practices";
        public static string updateMaxParallelRequests = "Update Max Parallel Requests";

        public static string logCommonData = "Turn on Common Logging Data";
        public static string logSpecificData = "Turn On Object Specific Logging Data";

        public static string policies = @"Policies\";
    }
}

