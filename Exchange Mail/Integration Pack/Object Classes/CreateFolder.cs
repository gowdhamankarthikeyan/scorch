using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Create Folder",ShowFilters = false)]
    public class CreateFolder : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String folderName = String.Empty;
        private String parentFolderName = String.Empty;
        private String parentFolderID = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Folder Name");
            designer.AddInput("Parent Folder Name").NotRequired();
            designer.AddInput("Parent Folder ID").NotRequired();
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddOutput("Folder Name").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            folderName = request.Inputs["Folder Name"].AsString();
            parentFolderName = request.Inputs["Parent Folder Name"].AsString();
            parentFolderID = request.Inputs["Parent Folder Id"].AsString();

            string alternateMailbox = string.Empty;
            if (request.Inputs.Contains("Alternate Mailbox")) { alternateMailbox = request.Inputs["Alternate Mailbox"].AsString(); }

            ExchangeService service = new ExchangeService();
            switch (exchangeVersion)
            {
                case "Exchange2007_SP1":
                    service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
                    break;
                case "Exchange2010":
                    service = new ExchangeService(ExchangeVersion.Exchange2010);
                    break;
                case "Exchange2010_SP1":
                    service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                    break;
                default:
                    service = new ExchangeService();
                    break;
            }

            service.Credentials = new WebCredentials(userName, password, domain);
            String AccountUrl = userName + "@" + domain;

            if (serviceURL.Equals("Autodiscover")) { service.AutodiscoverUrl(AccountUrl, (a) => { return true; }); }
            else { service.Url = new Uri(serviceURL); }

            if (!alternateMailbox.Equals(String.Empty)) { service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, alternateMailbox); }

            Folder folder = new Folder(service);

            FolderId folderID = new FolderId(WellKnownFolderName.Inbox);

            if (parentFolderName != String.Empty || parentFolderID != String.Empty)
            {
                if (parentFolderID != String.Empty)
                {
                    folderID = new FolderId(parentFolderID);
                }
                else
                {
                    SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, parentFolderName);
                    FolderView folderView = new FolderView(int.MaxValue);
                    folderView.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                    folderView.PropertySet.Add(FolderSchema.DisplayName);
                    folderView.Traversal = FolderTraversal.Deep;
                    FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, folderView);

                    foreach (Folder tempFolder in results)
                    {
                        folderID = tempFolder.Id;
                        break;
                    }
                }
            }

            folder.DisplayName = folderName;
            folder.Save(folderID);

            response.Publish("Folder Name", folderName);
        }
    }
}

