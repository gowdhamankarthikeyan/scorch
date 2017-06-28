using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Empty Folder",ShowFilters=false)]
    public class EmptyFolder : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;

        private String FolderID = String.Empty;
        private String FolderName = String.Empty;

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
            string[] exchangeVersionOptions = new string[2];

            designer.AddInput("Folder Name");
            designer.AddInput("Folder ID").NotRequired();
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

            FolderName = request.Inputs["Folder Name"].AsString();
            FolderID = request.Inputs["Folder ID"].AsString();
            exchangeVersion = request.Inputs["Exchange Version"].AsString();

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

            FolderId fID = new FolderId(WellKnownFolderName.Inbox);

            if (FolderID != String.Empty)
            {
                fID = new FolderId(FolderID);

                SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.Id, FolderID);
                FolderView folderView = new FolderView(int.MaxValue);
                folderView.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                folderView.PropertySet.Add(FolderSchema.DisplayName);
                folderView.Traversal = FolderTraversal.Deep;
                FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, folderView);

                foreach (Folder tempFolder in results)
                {
                    if(exchangeVersion.Equals("Exchange2010_SP1"))
                    {
                        tempFolder.Empty(DeleteMode.HardDelete, true);
                    }
                    else
                    {
                        
                            FolderView fView = new FolderView(int.MaxValue);
                            FindFoldersResults SubResults = tempFolder.FindFolders(fView);
                            foreach (Folder subFolder in SubResults)
                            {
                                subFolder.Delete(DeleteMode.HardDelete);
                            }

                            ItemView iView = new ItemView(int.MaxValue);
                            FindItemsResults<Item> subEmails = tempFolder.FindItems(iView);
                            foreach (Item subMessage in subEmails)
                            {
                                subMessage.Delete(DeleteMode.HardDelete);
                            }
                    }
                }
            }
            else
            {
                SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, FolderName);
                FolderView folderView = new FolderView(int.MaxValue);
                folderView.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                folderView.PropertySet.Add(FolderSchema.DisplayName);
                folderView.Traversal = FolderTraversal.Deep;

                FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, folderView);

                foreach (Folder tempFolder in results)
                {
                    if (exchangeVersion.Equals("Exchange2010_SP1"))
                    {
                        tempFolder.Empty(DeleteMode.HardDelete, true);
                    }
                    else
                    {

                        FolderView fView = new FolderView(int.MaxValue);
                        FindFoldersResults SubResults = tempFolder.FindFolders(fView);
                        foreach (Folder subFolder in SubResults)
                        {
                            subFolder.Delete(DeleteMode.HardDelete);
                        }

                        ItemView iView = new ItemView(int.MaxValue);
                        FindItemsResults<Item> subEmails = tempFolder.FindItems(iView);
                        foreach (Item subMessage in subEmails)
                        {
                            subMessage.Delete(DeleteMode.HardDelete);
                        }
                    }
                }
            }

            response.Publish("Folder Name", FolderName);
        }
    }
}

