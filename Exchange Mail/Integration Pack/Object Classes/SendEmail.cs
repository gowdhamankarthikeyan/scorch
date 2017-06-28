using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Send Email",ShowFilters=false)]
    public class SendEmail : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;

        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;

        private String ToRecipients = String.Empty;
        private String ccRecipients = String.Empty;
        private String bccRecipients = String.Empty;
        private String Subject = String.Empty;
        private String Body = String.Empty;
        private String Attachements = String.Empty;
        private String ImportanceLevel = String.Empty;
        private String bodyFormat = String.Empty;

        private bool requestReadReceipt;
        private bool requestDeliveryReceipt;
        private bool requestResponse;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            string[] importanceOptions = new string[3];
            importanceOptions[0] = "High";
            importanceOptions[1] = "Low";
            importanceOptions[2] = "Normal";

            string[] bodyFormat = { "Plain Text", "HTML Format" };
            string[] trueFalse = { "True", "False" };

            designer.AddInput("Subject");
            designer.AddInput("Recipients");
            designer.AddInput("Body");
            designer.AddInput("Importance Level").WithListBrowser(importanceOptions).WithDefaultValue("Normal");

            designer.AddInput("CC Recipients").NotRequired().WithDefaultValue("user@contoso.com;user2@contoso.com");
            designer.AddInput("BCC Recipients").NotRequired().WithDefaultValue("user@contoso.com;user2@contoso.com");
            designer.AddInput("Attachment File Locations").NotRequired().WithFileBrowser();
            designer.AddInput("Request Read Receipt").NotRequired().WithDefaultValue("False").WithListBrowser(trueFalse);
            designer.AddInput("Request Response").NotRequired().WithDefaultValue("False").WithListBrowser(trueFalse);
            designer.AddInput("Request Delivery Receipt").NotRequired().WithDefaultValue("False").WithListBrowser(trueFalse);
            designer.AddInput("Body Format").NotRequired().WithDefaultValue("HTML Format").WithListBrowser(bodyFormat);

            designer.AddInput("Alternate Mailbox").NotRequired();

            designer.AddOutput("EmailSubject").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            bodyFormat = "HTML Format";
            requestReadReceipt = false;

            ToRecipients = request.Inputs["Recipients"].AsString();
            Subject = request.Inputs["Subject"].AsString();
            Body = request.Inputs["Body"].AsString();
            ImportanceLevel = request.Inputs["Importance Level"].AsString();

            string alternateMailbox = string.Empty;
            if (request.Inputs.Contains("Alternate Mailbox")) { alternateMailbox = request.Inputs["Alternate Mailbox"].AsString(); }

            if (request.Inputs.Contains("Attachment File Locations")) { Attachements = request.Inputs["Attachment File Locations"].AsString(); }
            if (request.Inputs.Contains("Request Read Receipt")) { requestReadReceipt = Convert.ToBoolean(request.Inputs["Request Read Receipt"].AsString()); }
            if (request.Inputs.Contains("Request Response")) { requestResponse = Convert.ToBoolean(request.Inputs["Request Response"].AsString()); }
            if (request.Inputs.Contains("Request Delivery Receipt")) { requestDeliveryReceipt = Convert.ToBoolean(request.Inputs["Request Delivery Receipt"].AsString()); }
            if (request.Inputs.Contains("Body Format")) { bodyFormat = request.Inputs["Body Format"].AsString(); }
            if (request.Inputs.Contains("CC Recipients")) { ccRecipients = request.Inputs["CC Recipients"].AsString(); }
            if (request.Inputs.Contains("BCC Recipients")) { bccRecipients = request.Inputs["BCC Recipients"].AsString(); }

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

            if (!alternateMailbox.Equals(String.Empty)) { service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, alternateMailbox); }

            EmailMessage message = new EmailMessage(service);


            // Add Recipients
            foreach(String recipient in ToRecipients.Split(';'))
            {
                message.ToRecipients.Add(recipient);
            }

            // Add CC Recipients
            if (!ccRecipients.Equals(String.Empty))
            {
                foreach (String recipient in ccRecipients.Split(';'))
                {
                    message.CcRecipients.Add(recipient);
                }    
            }
            // Add BCC Recipients
            if (!bccRecipients.Equals(String.Empty))
            {
                foreach (String recipient in bccRecipients.Split(';'))
                {
                    message.BccRecipients.Add(recipient);
                }
            }

            // Set Subject
            message.Subject = Subject;

            // Set Body
            if (bodyFormat.Equals("HTML Format")) { message.Body = new MessageBody(BodyType.HTML, Body); }
            else { message.Body = new MessageBody(BodyType.Text, Body); }
            message.Body = Body;

            // Attach Attachments
            if (Attachements != String.Empty) 
            {
                foreach (String AttachmentPath in Attachements.Split(';'))
                {
                    message.Attachments.AddFileAttachment(AttachmentPath);
                }
            }

            // Set Importance Level
            switch (ImportanceLevel)
            {
                case "High":
                    message.Importance = Importance.High;
                    break;
                case "Low":
                    message.Importance = Importance.Low;
                    break;
                case "Normal":
                    message.Importance = Importance.Normal;
                    break;
                default:
                    message.Importance = Importance.Normal;
                    break;
            }

            if (requestReadReceipt) { message.IsReadReceiptRequested = true; }
            else { message.IsReadReceiptRequested = false; }

            if (requestResponse) { message.IsResponseRequested = true; }
            else { message.IsResponseRequested = false; }
            
            if (requestDeliveryReceipt) { message.IsDeliveryReceiptRequested = true; }
            else { message.IsDeliveryReceiptRequested = false; }

            message.Send();

            response.Publish("EmailSubject", message.Subject.ToString());
        }
    }
}

