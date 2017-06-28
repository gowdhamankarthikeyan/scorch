using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [ActivityData("Email")]
    public class Email
    {
        private String subject = String.Empty;
        private String body = String.Empty;
        private String from = String.Empty;
        private String id = String.Empty;
        private Boolean Attachements = false;
        private DateTime recievedDate = new DateTime();
        private Boolean isRead = false;

        internal Email(String subject, String body, String from, String id, DateTime recievedDate, Boolean attachments, Boolean isRead)
        {
            this.subject = subject;
            this.body = body;
            this.from = from;
            this.id = id;
            this.recievedDate = recievedDate;
            this.Attachements = attachments;
            this.isRead = isRead;
        }

        internal Email(String subject, String body, String from, String id, DateTime recievedDate, Boolean attachments)
        {
            this.subject = subject;
            this.body = body;
            this.from = from;
            this.id = id;
            this.recievedDate = recievedDate;
            this.Attachements = attachments;
        }

        internal Email(String subject, String body, String from, String id, DateTime recievedDate)
        {
            this.subject = subject;
            this.body = body;
            this.from = from;
            this.id = id;
            this.recievedDate = recievedDate;
        }

        internal Email(String subject, String body, String from, String id)
        {
            this.subject = subject;
            this.body = body;
            this.from = from;
            this.id = id;
        }
        internal Email(String subject, String body, String from)
        {
            this.subject = subject;
            this.body = body;
            this.from = from;
        }
        internal Email(String subject, String from)
        {
            this.subject = subject;
            this.from = from;
        }
        internal Email(EmailMessage message)
        {
            try { this.body = message.Body.ToString(); }
            catch { }
            try { this.from = message.From.Address.ToString(); }
            catch { }
            try { this.subject = message.Subject.ToString(); }
            catch { }
            try { this.id = message.Id.ToString(); }
            catch { }
            try { this.recievedDate = message.DateTimeReceived; }
            catch { }
            try { this.Attachements = message.HasAttachments; }
            catch { }
            try { this.isRead = message.IsRead; }
            catch { }
        }
        [ActivityOutput, ActivityFilter]
        public Boolean attachments
        {
            get { return Attachements; }
        }

        [ActivityOutput, ActivityFilter]
        public Boolean IsRead
        {
            get { return isRead; }
        }

        [ActivityOutput, ActivityFilter]
        public String Subject
        {
            get { return subject; }
        }

        [ActivityOutput, ActivityFilter]
        public String Body
        {
            get { return body; }
        }

        [ActivityOutput, ActivityFilter]
        public String From
        {
            get { return from; }
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get { return id; }
        }
        [ActivityOutput, ActivityFilter]
        public DateTime ReceivedDate
        {
            get { return recievedDate; }
        }
    }
}

