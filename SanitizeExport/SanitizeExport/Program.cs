using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security;
using System.IO;
using System.Reflection;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.OIS_Export;

namespace SanitizeExport
{
    class Program
    {
        #region Gobals
        private static XmlDocument finalDocument = new XmlDocument();
        private static XmlDocument inputXML = new XmlDocument();

        private static XmlNode finalConfigNode;
        private static XmlNode finalCountersNode;
        private static XmlNode finalVariablesNode;
        private static XmlNode finalSchedulesNode;
        private static XmlNode finalComputerGroupsNode;

        private static string configurationNodePath = @"//GlobalConfigurations";
        private static string countersNodePath = @"//GlobalSettings/Counters/Folder";
        private static string variablesNodePath = @"//GlobalSettings/Variables/Folder";
        private static string schedulesNodePath = @"//GlobalSettings/Schedules/Folder";
        private static string computerGroupsNodePath = @"//GlobalSettings/ComputerGroups/Folder";
        private static string policiesFolderPath = @"//Policies/Folder";
        private static string CommonData = "LogCommonData";
        private static string SpecificData = "LogSpecificData";

        private static bool verbose = false;
        #endregion 

        static void Main(string[] args)
        {
            FileInfo inputFile = null;
            FileInfo outputFile = null;
            
            bool sanitizeGlobals = true;
            bool applyLinkBestPractices = true;
            bool updateMaxParallel = true;

            String ObjectSpecificLogging = String.Empty;
            String GenericObjectLogging = String.Empty;

            if (parseInputs(args, ref inputFile, ref outputFile, ref sanitizeGlobals, ref ObjectSpecificLogging, ref GenericObjectLogging, ref applyLinkBestPractices, ref updateMaxParallel))
            {
                ExportFile export = new ExportFile(inputFile);

                if (sanitizeGlobals)
                {
                    export.cleanGlobalConfigurations();
                    export.cleanGlobalComputerGroupsNode();
                    export.cleanGlobalCountersNode();
                    export.cleanGlobalSchedulesNode();
                    export.cleanGlobalVariablesNode();
                }

                if (applyLinkBestPractices) { export.modifyExportLinkApplyBestPractices(); }
                if (updateMaxParallel) { export.modifyExportSetMaxParallelRequestSettingNameBased(); }
                              
                export.modifyGenericLogging(GenericObjectLogging);
                export.modifyObjectSpecificLogging(ObjectSpecificLogging);

                export.OISExport.Save(outputFile.FullName);
            }
        }

        #region Input Parameter Parsing and Validation
        private static bool parseInputs(string[] args, ref FileInfo inputFile, ref FileInfo outputFile, ref bool sanitizeGlobals, ref String ObjectSpecificLogging, ref String GenericObjectLogging, ref bool applyLinkBestPractices, ref bool updateMaxParallel)
        {
            bool force = false;

            if (args.Count() == 0 || args.Contains("-help") || args.Contains("/?"))
            {
                printHelp();
                return false;
            }
            else
            {
                for (int i = 0; i < args.Count(); i++)
                {
                    parseInput(args, ref inputFile, ref outputFile, ref force, ref sanitizeGlobals, ref ObjectSpecificLogging, ref GenericObjectLogging, ref i, ref applyLinkBestPractices, ref updateMaxParallel);
                }

                return verifyMandatoryParameters(inputFile, outputFile, force);
            }
        }
        private static void printHelp()
        {
            Console.WriteLine("SanitizeExport.exe -ExportFilePath <String> -SanitizedExportFilePath <String> [-ObjectSpecificLogging (On|Off)] [-GenericObjectLogging (On|Off)] [-DoNotSanitizeGlobals] [-Force]\n\n");
            Console.WriteLine("-ExportFilePath <String> :  Path to the ois_export file to sanitize");
            Console.WriteLine("-SanitizedExportFilePath <String> : Path to save the sanitized export file to");
            Console.WriteLine("-ObjectSpecificLogging (On|Off) : Turns On or Off object specific logging for all runbooks in export file");
            Console.WriteLine("-GenericObjectLogging (On|Off) : Turns On or Off generic object logging for all runbooks in export file");
            Console.WriteLine("-ApplyLinkBestPractices (True|False) : Applys link best practice naming and coloring");
            Console.WriteLine("-UpdateMaxParallel (True|False) : Updates the max concurrent settings for Runbooks based on folder names");
            Console.WriteLine("-DoNotSanitizeGlobals : If set no manipulations of Globals (configurations, variables, counters etc) will be done");
            Console.WriteLine("-Force : If set any file at SanitizedExportFilePath will be overwritten");
            Console.WriteLine("-Verbose : Prints detailed information on what the tool is doing");
            Console.WriteLine("-Help : Prints this Help Page");
        }
        private static void parseInput(string[] args, ref FileInfo inputFile, ref FileInfo outputFile, ref bool force, ref bool sanitizeGlobals, ref String ObjectSpecificLogging, ref String GenericObjectLogging, ref int i, ref bool applyLinkBestPractices, ref bool updateMaxParallel)
        {
            switch (args[i].ToLower())
            {
                case "-applylinkbestpractices":
                case "-a":
                    i++;
                    applyLinkBestPractices = Convert.ToBoolean(args[i]);
                    break;
                case "-updatemaxparallel":
                case "-u":
                    i++;
                    updateMaxParallel = Convert.ToBoolean(args[i]);
                    break;
                case "-exportfilepath":
                    i++;
                    inputFile = new FileInfo(args[i]);
                    break;
                case "-e":
                    i++;
                    inputFile = new FileInfo(args[i]);
                    break;
                case "-sanitizedexportfilepath":
                    i++;
                    outputFile = new FileInfo(args[i]);
                    break;
                case "-s":
                    i++;
                    outputFile = new FileInfo(args[i]);
                    break;
                case "-objectspecificlogging":
                    i++;
                    ObjectSpecificLogging = args[i];
                    break;
                case "-o":
                    i++;
                    ObjectSpecificLogging = args[i];
                    break;
                case "-genericobjectlogging":
                    i++;
                    GenericObjectLogging = args[i];
                    break;
                case "-g":
                    i++;
                    GenericObjectLogging = args[i];
                    break;
                case "-donotsanitizeglobals":
                    sanitizeGlobals = false;
                    break;
                case "-d":
                    sanitizeGlobals = false;
                    break;
                case "-force":
                    force = true;
                    break;
                case "-f":
                    force = true;
                    break;
                case "-verbose":
                    verbose = true;
                    break;
                case "-v":
                    verbose = true;
                    break;
                default:
                    break;
            }
        }
        private static bool verifyMandatoryParameters(FileInfo inputFile, FileInfo outputFile, bool force)
        {
            if (inputFile == null)
            {
                Console.WriteLine("Please specifiy an export file to sanitize using -ExportFilePath \"\\\\path\\to\\exportfile.ois_export\"");
                return false;
            }
            else
            {
                if(!inputFile.Exists)
                {
                    return false;
                }
            }
            if (outputFile == null)
            {
                Console.WriteLine("Please specifiy a path to store the output file using -SanitizedExportFilePath \"\\\\path\\to\\exportfile.ois_export\"");
                return false;
            }
            else
            {
                if (outputFile.Exists)
                {
                    if (force)
                    {
                        outputFile.Delete();
                    }
                    else
                    {
                        Console.WriteLine("Output file path already exsits, please remove before running or specify -Force\n" + outputFile.FullName);
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    
    }
}
