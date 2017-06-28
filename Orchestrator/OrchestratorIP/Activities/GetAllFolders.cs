using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop.Data_Class;
using OrchestratorInterop.SCOrchestrator;
using OrchestratorInterop;
using OrchestratorIP.ConfigurationObjects;
using OrchestratorIP.ReturnTypes;
using System.Globalization;
using System.Net;
using System.Data.Services.Client;
namespace OrchestratorIP.Activities
{
    [Activity("Get All Folders")]
    public class GetAllFolders : IActivity
    {
        [ActivityConfiguration]
        public ConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddCorellatedData(typeof(FolderInst));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            OrchestratorContext sco = new OrchestratorContext(new Uri(Credentials.OrchestratorServiceURL));

            sco.Credentials = new NetworkCredential(Credentials.UserName, Credentials.Password, Credentials.Domain);
            sco.MergeOption = MergeOption.OverwriteChanges;

            Folder[] folderArray = SCOrch.getAllFolders(sco);

            response.WithFiltering().PublishRange(parseResults(folderArray));
        }

        private IEnumerable<FolderInst> parseResults(Folder[] folderArray)
        {
            foreach (Folder folder in folderArray)
            {
                yield return new FolderInst(folder);
            }
        }
    }
}
