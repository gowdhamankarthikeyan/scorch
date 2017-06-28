using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Find Folder")]
    public class FindFolder : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;

        private String SearchField = String.Empty;
        private String SearchString = String.Empty;

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
            string[] searchFieldOptions = new string[2];
            searchFieldOptions[0] = "Folder Name";
            searchFieldOptions[1] = "Folder Id";

            designer.AddInput("Search Field").WithListBrowser(searchFieldOptions).WithDefaultValue("Folder Name");
            designer.AddInput("Search String");
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddCorellatedData(typeof(InternalFolder));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            SearchField = request.Inputs["Search Field"].AsString();
            SearchString = request.Inputs["Search String"].AsString();

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

            SearchFilter folderFilter;
            FindFoldersResults results;
            FolderView folderView = new FolderView(int.MaxValue);
            folderView.PropertySet = new PropertySet(BasePropertySet.IdOnly);
            folderView.PropertySet.Add(FolderSchema.DisplayName);
            folderView.Traversal = FolderTraversal.Deep;

            switch(SearchField)
            {
                case "Folder Name":
                    folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, SearchString);
                    results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, folderView);
                    response.WithFiltering().PublishRange(getFolderMatches(results, service));
                    break;
                case "Folder Id":
                    Folder folder = Folder.Bind(service, SearchString);
                    response.WithFiltering().PublishRange(singleMatch(folder.DisplayName.ToString(), folder.Id.ToString(), folder.ParentFolderId.ToString()));
                    break;
                default:
                    folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, SearchString);
                    results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, folderView);
                    response.WithFiltering().PublishRange(getFolderMatches(results, service));
                    break;
            }
        }
        private IEnumerable<InternalFolder> singleMatch(String FolderName, String FolderId, String ParentFolderId)
        {
            yield return new InternalFolder(FolderName, FolderId, ParentFolderId);
        }

        private IEnumerable<InternalFolder> getFolderMatches(FindFoldersResults results,ExchangeService service)
        {
            foreach (Folder tempFolder in results)
            {
                Folder folder = Folder.Bind(service, tempFolder.Id);
                String tempDisplayName = String.Empty;
                String tempID = String.Empty;
                String tempParentId = String.Empty;

                try
                {
                    tempDisplayName = folder.DisplayName.ToString();
                }
                catch
                {
                    tempDisplayName = SearchString;
                }

                tempID = folder.Id.ToString();

                try
                {
                    tempParentId = folder.ParentFolderId.ToString();
                }
                catch
                {
                    tempParentId = tempID;
                }

                yield return new InternalFolder(tempDisplayName, tempID, tempParentId);
            }
        }

    }
}

