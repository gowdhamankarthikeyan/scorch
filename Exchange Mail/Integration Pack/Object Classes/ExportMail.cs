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
    [Activity("Export Email")]
    public class ExportMail : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String emailID = String.Empty;
        private String SaveLocation = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;
        private int numberOfExportedEmails;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Email ID");
            designer.AddInput("Save Location").WithFolderBrowser();
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddCorellatedData(typeof(ExportedEmail));
            designer.AddOutput("Number of Exported Emails");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            emailID = request.Inputs["Email ID"].AsString();
            SaveLocation = request.Inputs["Save Location"].AsString();

            numberOfExportedEmails = 0;

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

            PropertySet propSet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.MimeContent, EmailMessageSchema.IsRead);
            EmailMessage message = EmailMessage.Bind(service, emailID,propSet);

            response.PublishRange(exportEmail(message));
            response.Publish("Number of Exported Emails", numberOfExportedEmails);            
        }
        private IEnumerable<ExportedEmail> exportEmail(EmailMessage message)
        {
            string ID = message.InternetMessageId.ToString();
            string filename = ID.Substring(ID.IndexOf('<') + 1, (ID.IndexOf('@') - 1) - ID.IndexOf('<'));
            
            File.WriteAllBytes(SaveLocation + "\\" + filename + ".eml", message.MimeContent.Content);
            numberOfExportedEmails++;

            yield return new ExportedEmail(filename + ".eml", SaveLocation, message.Id.ToString());            
        }
    }
}
