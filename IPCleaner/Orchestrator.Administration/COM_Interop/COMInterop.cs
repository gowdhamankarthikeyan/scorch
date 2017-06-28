using System.Xml;
using OpalisManagementServiceLib;

namespace Microsoft.SystemCenter.Orchestrator.Integration.Administration
{
    public class COMInterop
    {
        private OpalisManager _scoManager = new OpalisManager();


        public int ConnectScoManagementService(string username, string password)
        {
            object handle;
            _scoManager.Connect(username, password, out handle);

            int connHandle = 0;
            if (int.TryParse(handle.ToString(), out connHandle))
            {
                return connHandle;
            }
            return 0;
        }

        public XmlDocument ExportRunbook(int handle, string runbookID)
        {
            XmlDocument xml = new XmlDocument();
            object outVar;
            _scoManager.LoadPolicy(handle, runbookID, out outVar);

            if (outVar != null)
            {
                xml.LoadXml(outVar.ToString());
            }
            return xml;
        }
    }
}
