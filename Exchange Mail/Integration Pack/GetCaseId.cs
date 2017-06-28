using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;
using System.IO;

namespace ExchangeMail
{
    [Activity("Get Case ID")]
    public class GetCaseId : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String emailID = String.Empty;
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
            designer.AddInput("Email ID");
            designer.AddCorellatedData(typeof(Case));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            emailID = request.Inputs["Email ID"].AsString();
            

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

            if (serviceURL.Equals("Autodiscover")) { service.AutodiscoverUrl(AccountUrl); }
            else { service.Url = new Uri(serviceURL); }
            PropertySet propSet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.MimeContent, EmailMessageSchema.IsRead);
            EmailMessage message = EmailMessage.Bind(service, emailID, propSet);

            getAttachments(message);

            if (message.HasAttachments)
            {
                response.Publish("Email Exported", message.Id.ToString());
                response.WithFiltering().PublishRange(getAttachments(message));
            }
            else
            {
                response.Publish("Email Exported", message.Id.ToString());
                response.WithFiltering().PublishRange(getAttachments(message));
            }


        }
        private IEnumerable<Case> getAttachments(EmailMessage message)
        {
            
            string subject = message.Subject.ToString();
            string output = subject.Substring(subject.IndexOf('[') + 1, (subject.IndexOf(']') - 1) - subject.IndexOf('['));

            yield return new Case(output);


        }

    }
}
