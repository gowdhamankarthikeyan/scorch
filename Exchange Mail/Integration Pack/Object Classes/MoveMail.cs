using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Move Mail",ShowFilters = false)]
    public class MoveMail : IActivity
    {
        private MailboxSettings settings;
        private String emailID = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String exchangeVersion = String.Empty;
        private String destinationFolder = String.Empty;
        private String serviceURL = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Email ID");
            designer.AddInput("Destination Folder");
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddOutput("Email ID");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            emailID = request.Inputs["Email ID"].AsString();
            destinationFolder = request.Inputs["Destination Folder"].AsString();

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

            EmailMessage message = EmailMessage.Bind(service, emailID);
            if (!destinationFolder.Equals(String.Empty))
            {
                SearchFilter filter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, destinationFolder);
                FolderView view = new FolderView(int.MaxValue);
                view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                view.PropertySet.Add(FolderSchema.DisplayName);
                view.Traversal = FolderTraversal.Deep;
                FindFoldersResults results = service.FindFolders(WellKnownFolderName.MsgFolderRoot, filter, view);

                foreach (Folder folder in results)
                {
                    message = (EmailMessage)message.Move(folder.Id);
                    if (message != null)
                    {
                        response.Publish("Email ID", message.Id);
                    }
                    else
                    {
                        response.Publish("Email ID", "Email moved between two mailboxes or between a mailbox and a public folder");
                    }
                    break;
                }
            }
        }
    }
}

