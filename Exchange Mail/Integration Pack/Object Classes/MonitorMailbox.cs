using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;
using System.Threading;

namespace ExchangeMail
{
    [Activity("Monitor Mailbox"), ActivityMonitor(Interval=60)]
    public class MonitorMailbox : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String folderName = String.Empty;
        private int MonitorInterval = 60;
        private FolderId folderID = new FolderId(WellKnownFolderName.Inbox);
        private String serviceURL = String.Empty;
        private String bodyFormat = "HTML Format";
        private String readMailFilter = "Unread Only";
        private Boolean complete = false;
        private String subjectFilter = String.Empty;
        private String subjectFilterType = String.Empty;
        private String fromAddressFilter = String.Empty;
        private String fromAddressFilterType = String.Empty;
        
        private int recievedTimeOffset = 0;
        private string recievedTimeOffsetFilterType = string.Empty;

        string alternateMailbox = String.Empty; 
        
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
            designer.AddInput("Monitor Interval [Seconds]").WithDefaultValue(60);
            designer.AddInput("Body Format").WithListBrowser(bodyFormat).WithDefaultValue("HTML Format");
            designer.AddInput("Read Mail Filter").WithListBrowser(readFilter).WithDefaultValue("Unread Only");
            designer.AddInput("Subject Line").NotRequired();
            designer.AddInput("Subject Line Match Type").NotRequired().WithListBrowser(new string[] { "Contains", "Is Equal To" });
            designer.AddInput("From Address").NotRequired();
            designer.AddInput("From Address Match Type").NotRequired().WithListBrowser(new string[] { "Contains", "Is Equal To" });
            designer.AddInput("Recieved Time Offset (Seconds)").NotRequired().WithDefaultValue("86400");
            designer.AddInput("Recieved Time Filter Type").NotRequired().WithListBrowser(new string[] { "Older than", "Newer than" }).WithDefaultValue("Older than");
            designer.AddInput("Alternate Mailbox").NotRequired();

            designer.AddCorellatedData(typeof(Email));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;
            
            if (request.Inputs.Contains("Alternate Mailbox")) { alternateMailbox = request.Inputs["Alternate Mailbox"].AsString(); }
            if (request.Inputs.Contains("Subject Line")) { subjectFilter = request.Inputs["Subject Line"].AsString(); }
            if (request.Inputs.Contains("Subject Line Match Type")) { subjectFilterType = request.Inputs["Subject Line Match Type"].AsString(); }
            if (request.Inputs.Contains("From Address")) { fromAddressFilter = request.Inputs["From Address"].AsString(); }
            if (request.Inputs.Contains("From Address Match Type")) { fromAddressFilterType = request.Inputs["From Address Match Type"].AsString(); }
            if (request.Inputs.Contains("Recieved Time Offset (Seconds)")) { recievedTimeOffset = Convert.ToInt32(request.Inputs["Recieved Time Offset (Seconds)"].AsString()); }
            if (request.Inputs.Contains("Recieved Time Filter Type")) { recievedTimeOffsetFilterType = request.Inputs["Recieved Time Filter Type"].AsString(); }

            folderName = request.Inputs["Folder Name"].AsString();
            MonitorInterval = request.Inputs["Monitor Interval [Seconds]"].AsInt32();
            bodyFormat = request.Inputs["Body Format"].AsString();
            readMailFilter = request.Inputs["Read Mail Filter"].AsString();

            ExchangeService service = SetupExchangeConnection();
            FindFolder(service);
            PropertySet propSet = setupPropertySet();
            complete = false;

            while (!complete)
            {
                try
                {
                    SearchFilter.SearchFilterCollection filterCollection = setupFilterCollection();
                    FindItemsResults<Item> findResults = service.FindItems(folderID, filterCollection, new ItemView(Int32.MaxValue));
                    
                    if (findResults.TotalCount > 0)
                    {
                        response.WithFiltering().PublishRange(processMail(findResults.Items, service, propSet));
                        complete = true;
                        break;
                    }
                }
                catch (Exception e) { response.ReportWarningEvent("Exchange Monitor Problem", String.Format("{0}\n{1}\n{2}", e.Message, e.Source, e.StackTrace)); service = SetupExchangeConnection(); }
                // Delay looping by Monitor Interval
                Thread.Sleep(TimeSpan.FromSeconds(Convert.ToDouble(MonitorInterval)));
            }
        }

        private PropertySet setupPropertySet()
        {
            PropertySet propSet = new PropertySet(BasePropertySet.FirstClassProperties);

            if (bodyFormat.Equals("HTML Format")) { propSet.RequestedBodyType = BodyType.HTML; }
            else { propSet.RequestedBodyType = BodyType.Text; }

            return propSet;
        }

        private void FindFolder(ExchangeService service)
        {
            if (!folderName.Equals(String.Empty)) 
            {
                SearchFilter filter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, folderName);
                FolderView view = new FolderView(int.MaxValue);
                view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                view.PropertySet.Add(FolderSchema.DisplayName);
                view.Traversal = FolderTraversal.Deep;
                FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, filter, view);

                foreach (Folder folder in results)
                {
                    folderID = folder.Id;
                    break;
                }
            }
        }

        private SearchFilter.SearchFilterCollection setupFilterCollection()
        {
            SearchFilter.SearchFilterCollection filterCollection = new SearchFilter.SearchFilterCollection(LogicalOperator.And);
            
            switch (readMailFilter)
            {
                case "All Email":
                    break;
                case "Unread Only":
                    filterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                    break;
                case "Read Only":
                    filterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
                    break;
                default:
                    filterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
                    break;
            }

            switch (subjectFilterType)
            {
                case "Contains":
                    filterCollection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, subjectFilter));
                    break;
                case "Is Equal To":
                    filterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.Subject, subjectFilter));
                    break;
                default:
                    break;
            }

            switch (fromAddressFilterType)
            {

                case "Contains":
                    filterCollection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.From, fromAddressFilter));
                    break;
                case "Is Equal To":
                    filterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.From, fromAddressFilter));
                    break;
                default:
                    break;
            }

            switch (recievedTimeOffsetFilterType.ToUpper())
            {
                case "OLDER THAN":
                    filterCollection.Add(new SearchFilter.IsLessThan(EmailMessageSchema.DateTimeReceived, DateTime.Now.AddSeconds(recievedTimeOffset * -1)));
                    break;
                case "NEWER THAN":
                    filterCollection.Add(new SearchFilter.IsGreaterThan(EmailMessageSchema.DateTimeReceived, DateTime.Now.AddSeconds(recievedTimeOffset * -1)));
                    break;
                default:
                    break;
            }
            return filterCollection;
        }
        
        private ExchangeService SetupExchangeConnection()
        {
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
            return service;
        }

        private IEnumerable<Email> processMail(System.Collections.ObjectModel.Collection<Item> itemCollection, ExchangeService service, PropertySet propSet)
        {
            foreach (Item result in itemCollection)
            {
                if(result is EmailMessage)
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