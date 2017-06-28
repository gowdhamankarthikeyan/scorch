using OrchestratorInterop;
using OrchestratorInterop.Data_Class;
using OrchestratorInterop.SCOrchestrator;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace scorchLauncher
{
    class Program
    {
        private static string CONFIG_FILE_PATH = string.Empty;
        private static string WEBSERVICE_URL = string.Empty;
        private static string RUNBOOK_GUID = string.Empty;
        private static string RUNBOOK_PATH = string.Empty;
        private static string username = string.Empty;
        private static string domain = string.Empty;
        private static string password = string.Empty;
        private static bool useAltCreds = false;
        private static Dictionary<string,string> INPUT_RUNBOOK_PARAMETERS = new Dictionary<string,string>();
        private static bool waitForExit = false;
        
        // Try for up to 5 Minutes
        private static int tryCount = 30;
        private static TimeSpan delay = new TimeSpan(0, 0, 10);

        static void Main(string[] args)
        {
            int internalCounter = 0;
            bool finished = false;
            String exceptionMessage = String.Empty;

            if (parseInputs(args))
            {
                while (internalCounter < tryCount && !finished)
                {
                    try
                    {
                        finished = runRunbook();
                    }
                    catch(Exception e)
                    {
                        exceptionMessage = String.Format("Summary: {0}\nDetails: {1}", e.Message, e.InnerException);
                        if (exceptionMessage.Contains("Summary: No Input Parameter on Runbook Found for Key:"))
                        {
                            internalCounter = tryCount;
                            break;
                        }
                        internalCounter++;
                        Thread.Sleep(delay);
                    }
                }

                if (internalCounter >= tryCount)
                {
                    throw new Exception(exceptionMessage);
                }
            }
        }

        private static void ReadConfigFile(string path)
        {
            XDocument xdoc = null;
            try
            {

                xdoc = XDocument.Load(path);
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine("Error: Invalid configuration file path " + path);
                System.Environment.Exit(-200);
            }
            catch (XmlException ex)
            {
                Console.Error.WriteLine("Error: Configuration file is not valid xml.");
                System.Environment.Exit(-201);
            }
            XElement root = xdoc.Root;

            var urlNode = root.Descendants("webServerUrl");
            var pathNode = root.Descendants("rubookPathPrefix");
            var retryCountNode = root.Descendants("retryCount");
            var retryDelayNode = root.Descendants("retryDelay");

            if (urlNode.Count() != 1 || pathNode.Count() != 1 || retryCountNode.Count() != 1 || retryDelayNode.Count() != 1)
            {
                Console.Error.WriteLine("Error: Invalid configuration file.  Must contain elements: <webServerUrl>,<runbookPathPrefix>,<retryCount>, <retryDelay>");
                System.Environment.Exit(-202);
            }

            WEBSERVICE_URL = (string)(urlNode.First());
            RUNBOOK_PATH = (string)(pathNode.First()) + "\\" + RUNBOOK_PATH;
            tryCount = (int)(retryCountNode.First());
            delay = new TimeSpan(0,0,(int)(retryDelayNode.First()));


        }

        private static bool runRunbook()
        {
            bool status = true;
            OrchestratorContext sco = setupOrchestratorConnection();
            string jobID;
            try
            {
                if (!String.IsNullOrEmpty(RUNBOOK_PATH)) { jobID = Convert.ToString(((Job)SCOrch.startRunbookJob(sco, RUNBOOK_PATH, INPUT_RUNBOOK_PARAMETERS)).Id); }
                else if (!String.IsNullOrEmpty(RUNBOOK_PATH)) { jobID = Convert.ToString(((Job)SCOrch.startRunbookJob(sco, new Guid(RUNBOOK_GUID), INPUT_RUNBOOK_PARAMETERS)).Id); }
                else { Console.WriteLine("Must pass either -RunbookPath or -RunbookGUID"); return false; }
                
                if (waitForExit)
                {
                    pollForJobCompletion(sco, new Guid(jobID));
                }
            }
            // If webservice isn't available attempt 1 try again
            catch
            {
                throw;
            }
            return status;
        }

        private static OrchestratorContext setupOrchestratorConnection()
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(WEBSERVICE_URL));

            if (useAltCreds)
            {
                NetworkCredential credentials = new NetworkCredential();
                credentials.UserName = username;
                credentials.Domain = domain;
                credentials.Password = password;
                sco.Credentials = credentials;
            }
            else { sco.Credentials = CredentialCache.DefaultCredentials; }
            sco.MergeOption = MergeOption.OverwriteChanges;
            return sco;
        }

        private static void pollForJobCompletion(OrchestratorContext sco, Guid jobID)
        {
            try
            {
                while (!SCOrch.getJobDetails(sco, jobID).job.Status.Equals("Completed"))
                {
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                }
                JobInstance j = SCOrch.getJobDetails(sco, jobID);
                Console.WriteLine("<OutputParameters>");
                foreach (string key in j.OutputParameters.Keys)
                {
                    Console.WriteLine(string.Format("\t<{0}>{1}</{0}>", key, j.OutputParameters[key]));
                }
                Console.WriteLine("</OutputParameters>");
            }
            // Allow for 1 webservice Error
            catch
            {
                sco = setupOrchestratorConnection();
                while (!SCOrch.getJobDetails(sco, jobID).job.Status.Equals("Completed"))
                {
                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                }
                JobInstance j = SCOrch.getJobDetails(sco, jobID);
                Console.WriteLine("<OutputParameters>");
                foreach (string key in j.OutputParameters.Keys)
                {
                    Console.WriteLine(string.Format("\t<{0}>{1}</{0}>", key, j.OutputParameters[key]));
                }
                Console.WriteLine("</OutputParameters>");
            }
        }
        private static bool parseInputs(string[] args)
        {
            if (args.Count() == 0 || args.Contains("-help") || args.Contains("/?"))
            {
                printHelp();
                return false;
            }
            else if (args.Contains("-config") && 
                        (args.Contains("-webServerUrl") || args.Contains("-w") || 
                         args.Contains("-retrycount") || args.Contains("-rc") ||
                         args.Contains("-retrydelay") || args.Contains("-rd")))
            {
                
                Console.Error.WriteLine("Error: The -config flag is incompatible with flags -webServerUrl, -retryCount, or -retryDelay.");
                Console.Error.WriteLine("Set those parameters using the configuration file instead.");
                return false;
            }
            else
            {
                for (int i = 0; i < args.Count(); i++)
                {
                    parseInput(args, ref i);
                }
                if (!String.IsNullOrEmpty(CONFIG_FILE_PATH))
                {
                    ReadConfigFile(CONFIG_FILE_PATH);
                }
                return verifyMandatoryParameters();
            }
        }
        private static void printHelp()
        {
            Console.WriteLine("scorchLauncher.exe -RunbookPath <String> -WebServerURL <String> [-Username <String> -Domain <String> -Password <String>] [-InputParameters <String>|<String>~<String>|<String>] [-WaitForExit (True|False)]\n\n");
            
            Console.WriteLine("-RunbookPath <String> :  Path to runbook in the form of \"\\RootFolder\\Containing Folder\\Runbook Name\"");
            Console.WriteLine("-WebServerURL <String> : Path to the webserver in the form of http://webservername:81/Orchestrator2012/Orchestrator.svc");
            Console.WriteLine("-InputParameters <String>|<String>~<String>|<String> : a list of input parameter key|value pairs seperated by ~");
            Console.WriteLine("-WaitForExit (True|False) : tells the program to wait for the completion of the runbook or not.  Must be set to True for accessing output parameters");
            Console.WriteLine("-UserName : The user name to connect to the web service with");
            Console.WriteLine("-Domain : Domain of the user to connect to the web service with");
            Console.WriteLine("-Password : Password of the user to connect to the web service with");
            Console.WriteLine("-RetryDelay : Delay between retry attempts in seconds (Default 10)");
            Console.WriteLine("-RetryCount : Number of Attempts to retry the web service call (Default 30)");
            Console.WriteLine("-Config : Supply the location of a configuration file to determine the WebServerUrl and prefix for the RunbookPath");
            Console.WriteLine("-Help : Prints this Help Page");
        }
        private static void parseInput(string[] args, ref int i)
        {
            switch (args[i].ToLower())
            {
                case "runbookguid":
                case "rg":
                    i++;
                    RUNBOOK_GUID = args[i];
                    break;
                case "-retrycount":
                case "-rc":
                    i++;
                    tryCount = Convert.ToInt32(args[i]);
                    break;
                case "-retrydelay":
                case "-rd":
                    i++;
                    delay = new TimeSpan(0, 0, Convert.ToInt32(args[i]));
                    break;
                case "-runbookpath":
                    i++;
                    RUNBOOK_PATH = args[i];
                    break;
                case "-r":
                    i++;
                    RUNBOOK_PATH = args[i];
                    break;
                case "-webserverurl":
                    i++;
                    WEBSERVICE_URL = args[i];
                    break;
                case "-w":
                    i++;
                    WEBSERVICE_URL = args[i];
                    break;
                case "-username":
                    i++;
                    username = args[i];
                    useAltCreds = true;
                    break;
                case "-u":
                    i++;
                    username = args[i];
                    useAltCreds = true;
                    break;
                case "-domain":
                    i++;
                    domain = args[i];
                    useAltCreds = true;
                    break;
                case "-d":
                    i++;
                    domain = args[i];
                    useAltCreds = true;
                    break;
                case "-password":
                    i++;
                    password = args[i];
                    useAltCreds = true;
                    break;
                case "-p":
                    i++;
                    password = args[i];
                    useAltCreds = true;
                    break;
                case "-inputparameters":
                    i++;
                    ParseInputParameters(args, i);
                    break;
                case "-i":
                    i++;
                    ParseInputParameters(args, i);
                    break;
                case "-waitforexit":
                    i++;
                    waitForExit = Convert.ToBoolean(args[i]);
                    break;
                case "-config":
                    i++;
                    CONFIG_FILE_PATH = args[i];
                    break;
                default:
                    break;
            }
        }

        private static void ParseInputParameters(string[] args, int i)
        {
            bool keyFinished = false;
            bool pairFinished = false;
            StringBuilder key = new StringBuilder();
            StringBuilder value = new StringBuilder();
         
            for (int j = 0; j < args[i].Length; j++)
            {
                if (!keyFinished)
                {
                    if (args[i][j].Equals('|'))
                    {
                        keyFinished = true;
                    }
                    else
                    {
                        if (args[i][j].Equals('\\'))
                        {
                            if (args[i][j + 1].Equals('|') || args[i][j + 1].Equals('~') || args[i][j + 1].Equals('\\'))
                            {
                                j = j + 1;
                            }
                            key.Append(args[i][j]);
                        }
                        else
                        {
                            key.Append(args[i][j]);
                        }
                    }
                }
                else
                {
                    if (args[i][j].Equals('~'))
                    {
                        pairFinished = true;
                    }
                    else
                    {
                        if (args[i][j].Equals('\\'))
                        {
                            if (args[i][j + 1].Equals('|') || args[i][j + 1].Equals('~') || args[i][j + 1].Equals('\\'))
                            {
                                j = j + 1;
                            }
                            value.Append(args[i][j]);
                        }
                        else
                        {
                            value.Append(args[i][j]);
                        }
                    }
                }
                if (pairFinished || j+1 == args[i].Length)
                {
                    pairFinished = false;
                    keyFinished = false;

                    INPUT_RUNBOOK_PARAMETERS.Add(key.ToString(), value.ToString());

                    key.Remove(0, key.Length);
                    value.Remove(0, value.Length);
                }
            }
        }
        private static bool verifyMandatoryParameters()
        {
            if (WEBSERVICE_URL.Equals(string.Empty))
            {
                Console.WriteLine("Please specify a Webservice URL in the form of http://webservername:81/Orchestrator2012/Orchestrator.svc");
                return false;
            }
            if (RUNBOOK_PATH.Equals(string.Empty))
            {
                Console.WriteLine("Please specify the path to the runbook to initiate in the form of \\Root Folder\\ContainingFolder\\RunbookName");
                return false;
            }
            if (useAltCreds && (username.Equals(string.Empty) || domain.Equals(string.Empty) || password.Equals(string.Empty)))
            {
                Console.WriteLine(string.Format("If alternate credentials are used all fields related must be passed " +
                                                "(Username, Domain, Password), passed values were\n" +
                                                "Username:\t{0}\n" +
                                                "Domain:\t{1}\n" + 
                                                "Password:\t{2}"
                                                ,username,domain,password));
                return false;
            }
            return true;
        }
    }
}
