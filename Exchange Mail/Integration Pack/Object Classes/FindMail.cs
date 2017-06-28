using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Find Mail")]
    public class FindMail : IActivity
    {
        private MailboxSettings settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String SearchField = String.Empty;
        private String SearchString = String.Empty;
        private String SearchAlgorithm = String.Empty;
        private String folderName = String.Empty;
        private String exchangeVersion = String.Empty;
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
            searchFieldOptions[0] = "Subject";
            searchFieldOptions[1] = "From";

            string[] searchAlgorithmOptions = new string[3];
            searchAlgorithmOptions[0] = "Equals";
            searchAlgorithmOptions[1] = "Contains String";
            searchAlgorithmOptions[2] = "Does Not Equal";

            designer.AddInput("Search Field").WithListBrowser(searchFieldOptions).WithDefaultValue("Subject");
            designer.AddInput("Search Algorithm").WithListBrowser(searchAlgorithmOptions).WithDefaultValue("Equals");
            designer.AddInput("Search String");
            designer.AddInput("Folder Name").NotRequired();
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddOutput("Number of Results").AsString();
            designer.AddCorellatedData(typeof(Email));
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
            SearchAlgorithm = request.Inputs["Search Algorithm"].AsString();
            folderName = request.Inputs["Folder Name"].AsString();

            string alternateMailbox = string.Empty;
            if (request.Inputs.Contains("Alternate Mailbox")) { alternateMailbox = request.Inputs["Alternate Mailbox"].AsString(); }

            FolderId folderID = new FolderId(WellKnownFolderName.Inbox);

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

            if (!folderName.Equals(String.Empty))
            {
                SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, folderName);
                FolderView folderView = new FolderView(int.MaxValue);
                folderView.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                folderView.PropertySet.Add(FolderSchema.DisplayName);
                folderView.Traversal = FolderTraversal.Deep;
                FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, folderView);

                foreach (Folder folder in results)
                {
                    folderID = folder.Id;
                    break;
                }
            }

            ItemView emailView = new ItemView(int.MaxValue);
            SearchFilter EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, SearchString);

            switch (SearchField)
            {
                case "Subject":
                    switch (SearchAlgorithm)
                    {
                        case "Equals":
                            EmailFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.Subject, SearchString);
                            break;
                        case "Contains String":
                            EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, SearchString);
                            break;
                        case "Does Not Equal":
                            EmailFilter = new SearchFilter.IsNotEqualTo(EmailMessageSchema.Subject, SearchString);
                            break;
                    }
                    break;
                case "From":
                    switch (SearchAlgorithm)
                    {
                        case "Equals":
                            EmailFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.From, SearchString);
                            break;
                        case "Contains String":
                            EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.From, SearchString);
                            break;
                        case "Does Not Equal":
                            EmailFilter = new SearchFilter.IsNotEqualTo(EmailMessageSchema.From, SearchString);
                            break;
                    }
                    break;
                default:
                    switch (SearchAlgorithm)
                    {
                        case "Equals":
                            EmailFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.Subject, SearchString);
                            break;
                        case "Contains String":
                            EmailFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, SearchString);
                            break;
                        case "Does Not Equal":
                            EmailFilter = new SearchFilter.IsNotEqualTo(EmailMessageSchema.Subject, SearchString);
                            break;
                    }
                    break;
            }

            FindItemsResults<Item> findResults = service.FindItems(folderID, EmailFilter, emailView);
            response.Publish("Number of Results", findResults.Items.Count.ToString());
            response.WithFiltering().PublishRange(getMail(findResults, service));
        }


        private IEnumerable<Email> getMail(FindItemsResults<Item> findResults,ExchangeService service)
        {
            foreach (Item result in findResults)
            {
                if (result.GetType().ToString().Equals("Microsoft.Exchange.WebServices.Data.EmailMessage"))
                {
                    EmailMessage message = EmailMessage.Bind(service, result.Id);
                    yield return new Email(message);
                }
            }
        }
    }
}

