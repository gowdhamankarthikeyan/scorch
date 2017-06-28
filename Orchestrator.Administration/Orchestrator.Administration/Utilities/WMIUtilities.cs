using System.Text;
using System.Management;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class WMIUtilities
    {

        public static bool DeleteFileUsingWMI(string fileName, string computerName, ConnectionOptions connectionOptions)
        {
            StringBuilder log = new StringBuilder();

            string query = string.Format("Select * From CIM_DataFile Where Name = '{0}'", fileName);
            log.AppendLine("Querying CIM_DataFile for the filename");

            ManagementObjectCollection files = QueryWMI(query, computerName, connectionOptions);
            log.AppendLine(string.Format("Found [{0}] matches", files.Count));

            foreach (ManagementObject file in files)
            {
                var result = file.InvokeMethod("Delete", null);
                log.Append(string.Format("The Delete method result is {0}", result.ToString()));

            }
            return true;
        }

        public static ManagementObjectCollection QueryWMI(string query, string computerName, ConnectionOptions connectionOptions)
        {
            System.Management.ManagementScope oMs = null;

            if (connectionOptions == null)
            {
                oMs = new System.Management.ManagementScope(string.Format("\\{0}", computerName));
            }
            else
            {
                oMs = new System.Management.ManagementScope(string.Format("\\{0}", computerName), connectionOptions);
            }
            System.Management.ObjectQuery oQuery = new System.Management.ObjectQuery(query);

            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection result = oSearcher.Get();
            return result;
        }

    }
}
