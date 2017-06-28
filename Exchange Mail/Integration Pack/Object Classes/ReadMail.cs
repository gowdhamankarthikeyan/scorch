using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Read Mail From Folder")]
    public class ReadMail : IActivity
    {
        private MailboxSettings settings;
        private String maxNumberOfEmails = "100";
        private String exchangeVersion = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String folderName = String.Empty;
        private FolderId folderID = new FolderId(WellKnownFolderName.Inbox);
        private String serviceURL = String.Empty;
        private String bodyFormat = "HTML Format";
        private String readMailFilter = "Unread Only";

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            string[] bodyFormat = { "Plain Text", "HTML Format" };
            string[] readFilter = { "All Email", "Unread Only", "Read Only" };

            designer.AddInput("Folder Name").WithDefaultValue("Inbox");
            designer.AddInput("Max Number of Emails").NotRequired().WithDefaultValue("100");
            designer.AddInput("Body Format").WithListBrowser(bodyFormat).WithDefaultValue("HTML Format");
            designer.AddInput("Read Mail Filter").WithListBrowser(readFilter).WithDefaultValue("Unread Only");
            designer.AddInput("Alternate Mailbox").NotRequired();

            designer.AddOutput("Number of Emails").AsString();
            designer.AddCorellatedData(typeof(Email));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            folderName = request.Inputs["Folder Name"].AsString();
            maxNumberOfEmails = request.Inputs["Max Number of Emails"].AsString();
            if (request.Inputs.Contains("Body Format")) { bodyFormat = request.Inputs["Body Format"].AsString(); }
            if (request.Inputs.Contains("Read Mail Filter")) { readMailFilter = request.Inputs["Read Mail Filter"].AsString(); }

            string alternateMailbox = string.Empty;
            if (request.Inputs.Contains("Alternate Mailbox")) { alternateMailbox = request.Inputs["Alternate Mailbox"].AsString(); }

            try
            {
                Convert.ToInt32(maxNumberOfEmails);
            }
            catch
            {
                maxNumberOfEmails = "100";
            }

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

            SearchFilter.SearchFilterCollection FolderfilterCollection = new SearchFilter.SearchFilterCollection(LogicalOperator.And);
            SearchFilter.SearchFilterCollection EmailfilterCollection = new SearchFilter.SearchFilterCollection(LogicalOperator.Or);

            FolderfilterCollection.Add(new SearchFilter.IsEqualTo(FolderSchema.DisplayName, folderName));

            switch (readMailFilter)
            {
                case "All Email":
                    EmailfilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
                    EmailfilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                    break;
                case "Unread Only":
                    EmailfilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                    break;
                case "Read Only":
                    EmailfilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
                    break;
                default:
                    EmailfilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                    break;
            }
            
            FolderView view = new FolderView(int.MaxValue);
            view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
            view.PropertySet.Add(FolderSchema.DisplayName);
            view.Traversal = FolderTraversal.Deep;
            FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot,FolderfilterCollection, view);

            foreach (Folder folder in results)
            {
                folderID = folder.Id;
                break;
            }

            FindItemsResults<Item> findResults = service.FindItems(folderID, EmailfilterCollection, new ItemView(Convert.ToInt32(maxNumberOfEmails)));

            response.Publish("Number of Emails", findResults.Items.Count.ToString());
            response.WithFiltering().PublishRange(getMail(findResults, service));
        }

        private IEnumerable<Email> getMail(FindItemsResults<Item> findResults, ExchangeService service)
        {
            PropertySet propSet = new PropertySet(BasePropertySet.FirstClassProperties);
            if(bodyFormat.Equals("HTML Format"))
            {
                propSet.RequestedBodyType = BodyType.HTML;
            }
            else
            {
                propSet.RequestedBodyType = BodyType.Text;
            }
               
            foreach (Item result in findResults)
            {
                if (result.GetType().ToString().Equals("Microsoft.Exchange.WebServices.Data.EmailMessage"))
                {
                    EmailMessage message = EmailMessage.Bind(service, result.Id, propSet);
                    Email retMail = new Email(message);

                    message.IsRead = true;
                    message.Update(ConflictResolutionMode.AutoResolve);

                    yield return retMail;                
                }
            }
        }
    }
}