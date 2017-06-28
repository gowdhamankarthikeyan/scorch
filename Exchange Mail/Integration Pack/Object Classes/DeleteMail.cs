using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Delete Mail",ShowFilters=false)]
    public class DeleteMail : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String emailID = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String deleteModeChoice = String.Empty;
        private String serviceURL = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            string[] deleteModeOptions = new string[3];
            deleteModeOptions[0] = "Hard Delete";
            deleteModeOptions[1] = "Soft Delete";
            deleteModeOptions[2] = "Move To Deleted Items";

            designer.AddInput("Email ID");
            designer.AddInput("Delete Mode").WithListBrowser(deleteModeOptions).WithDefaultValue("Hard Delete");
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddOutput("Email ID").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            deleteModeChoice = request.Inputs["Delete Mode"].AsString();
            emailID = request.Inputs["Email ID"].AsString();

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

            switch (deleteModeChoice)
            {
                case "Soft Delete":
                    message.Delete(DeleteMode.SoftDelete);
                    break;
                case "Hard Delete":
                    message.Delete(DeleteMode.HardDelete);
                    break;
                case "Move To Deleted Items":
                    message.Delete(DeleteMode.MoveToDeletedItems);
                    break;
                default:
                    message.Delete(DeleteMode.HardDelete);
                    break;
            }

            response.Publish("Email ID", emailID);
        }
    }
}

