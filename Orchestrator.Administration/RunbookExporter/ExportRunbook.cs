using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.OIS_Export;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.Infrastructure;
using System.Data.SqlClient;
using System.DirectoryServices;
namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration.RunbookExporter
{
    class ExportRunbook
    {
        static void Main(string[] args)
        {
                DirectoryEntry groupEntry = new DirectoryEntry("LDAP://CN=Domain Users,cn=Users,DC=Genmills,DC=Com", "M2IS254@genmills.com", "Dw9c8b5Ju0wB");

            DirectoryEntry DomainRoot = groupEntry;
            do { DomainRoot = DomainRoot.Parent; }
            while (DomainRoot.SchemaClassName != "domainDNS");
                DomainRoot.RefreshCache();
            DirectorySearcher mySearcher = new DirectorySearcher(DomainRoot);
            mySearcher.Filter = "(objectClass=*)";
                mySearcher.SearchScope = SearchScope.Subtree;
                
                mySearcher.PageSize = 1000;

            mySearcher.Filter = "(memberOf=" + groupEntry.Properties["DistinguishedName"].Value.ToString() + ")";
            SearchResultCollection resultCollection = mySearcher.FindAll();
            
            foreach (SearchResult result in resultCollection)
            {
                DirectoryEntry directoryObject = result.GetDirectoryEntry();
                String ldapPath = directoryObject.Path;

                Console.WriteLine(ldapPath);
                directoryObject.Close();
            }
            groupEntry.Close();
            groupEntry.Dispose();
            mySearcher.Dispose();
            /*
            Console.ReadLine();
  
            ExportFile importRunbook = new ExportFile(new FileInfo(args[0]));
            COMInterop scorchInterop = new COMInterop(username, password);
            importRunbook.ImportSCORunbook(Microsoft.SystemCenter.Orchestrator.Integration.Administration.ResourceFolderRoot.Runbooks, true, true, true, true, true, true, true, true, serverName, database, scorchInterop);
            /*
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", serverName, database);
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<ActionServer> asList = Infrastructure.Infrastructure.GetRunbookServers(connection, database);
                List<RunbookDesigner> rbList = Infrastructure.Infrastructure.GetRunbookDesigners(connection, database);
            }

            Console.ReadLine();
            COMInterop scorchInterop = new COMInterop(args[0], args[1]);

            ExportFile allRunbooks = new ExportFile();
            
            allRunbooks.LoadExportFromFolder(args[2].ToString(), scorchInterop);
            allRunbooks.LoadComputerGroups(scorchInterop);
            allRunbooks.LoadCounters(scorchInterop);
            allRunbooks.LoadSchedules(scorchInterop);
            allRunbooks.LoadVariables(scorchInterop);
            allRunbooks.LoadConfigurations(scorchInterop);

            allRunbooks.cleanGlobalComputerGroupsNode();
            allRunbooks.cleanGlobalCountersNode();
            allRunbooks.cleanGlobalSchedulesNode();
            allRunbooks.cleanGlobalVariablesNode();
            allRunbooks.cleanGlobalConfigurations();

            allRunbooks.OISExport.Save("d:\\temp\\newoutput2.ois_export");
            /*/
            
        }
    }
}
