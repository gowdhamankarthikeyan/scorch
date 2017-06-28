using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Management;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class Utilities
    {
        public static bool IsLocalComputer(string computerName)
        {
            if (string.Compare(computerName, "Localhost", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return true;
            }

            if (string.Compare(computerName, "127.0.0.1") == 0)
            {
                return true;
            }

            if (string.Compare(computerName, Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return true;
            }

            if (string.Compare(computerName, System.Net.Dns.GetHostName(), StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return true;
            }



            return false;
        }

        public static bool FindStringInFile(string fileName, string regex)
        {
            StreamReader stream = new StreamReader(fileName);
            string contents = stream.ReadToEnd();
            return Regex.IsMatch(contents, regex, RegexOptions.IgnoreCase);
        }

        public static string UninstallMsi(string productCode, string computerName, ConnectionOptions connectionOptions)
        {
            //uses WMI to uninstall the application, then deletes the MSI from the Management Server path

            StringBuilder log = new StringBuilder();

            string selectQuery = string.Format("Select * FROM Win32_Product Where IdentifyingNumber = '{0}'", GuidUtilities.AddBracesToGuid(productCode));

            //ConnectionOptions oConn = new ConnectionOptions();
            //oConn.Username = "JohnDoe";
            //oConn.Password = "JohnsPass";

            ManagementObjectCollection products = WMIUtilities.QueryWMI(selectQuery, computerName, connectionOptions);

            log.AppendLine(string.Format("Found [{0}] matches", products.Count));
            foreach (ManagementObject product in products)
            {
                log.Append(string.Format("Uninstalling '{0}'", product.Properties["Name"].ToString()));

                var result = product.InvokeMethod("Uninstall", null);
                log.Append(string.Format("The Uninstall method result is {0}", result.ToString()));

            }
            return log.ToString();
        }
    }
}
