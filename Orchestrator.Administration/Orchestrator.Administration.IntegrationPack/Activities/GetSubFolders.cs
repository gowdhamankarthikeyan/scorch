using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.OIS_Export;

namespace Orchestrator.Administration.IntegrationPack.Activities
{
    [Activity("Get Subfolders")]
    public class GetSubFolders : IActivity
    {
        int numberOfFolders = 0;
        [ActivityConfiguration]
        public COMInterfaceConnectionCredentials Credentials
        {
            get;
            set;
        }
        
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(ResourceStrings.targetFolderPath).WithDefaultValue(ResourceStrings.targetFolderPathDefaultValue);

            designer.AddOutput(ResourceStrings.numberOfFolders);
            designer.AddCorellatedData(typeof(SCOFolder));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string username = Credentials.UserDomain + "\\" + Credentials.UserName;

            string baseFolderPath = string.Empty;

            if (request.Inputs.Contains(ResourceStrings.targetFolderPath)) { baseFolderPath = request.Inputs[ResourceStrings.targetFolderPath].AsString(); }

            COMInterop scorchInterop = new COMInterop(username, Credentials.Password);
            XmlDocument baseFolder = scorchInterop.GetSCOFolderByPath(baseFolderPath);
            XmlDocument subFolders = scorchInterop.GetSCOSubFolders(baseFolder.SelectSingleNode("//UniqueID").InnerText);

            response.WithFiltering().PublishRange(parseResults(subFolders, baseFolderPath));
            response.Publish(ResourceStrings.numberOfFolders, numberOfFolders);
        }

        private IEnumerable<SCOFolder> parseResults(XmlDocument subFolders, string basePath)
        {
            foreach (XmlNode folderNode in subFolders.SelectNodes("//Folder"))
            {
                numberOfFolders++;
                yield return new SCOFolder(folderNode, basePath);
            }
        }
    }
}