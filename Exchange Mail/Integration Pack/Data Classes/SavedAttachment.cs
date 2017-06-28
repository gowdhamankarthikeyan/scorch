using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [ActivityData("Attachment")]
    public class SavedAttachment
    {
        private String AttachmentName = String.Empty;
        private String AttachmentPath = String.Empty;

        internal SavedAttachment(String AttachmentName, String AttachmentPath)
        {
            this.AttachmentName = AttachmentName;
            this.AttachmentPath = AttachmentPath;
        }

        [ActivityOutput, ActivityFilter]
        public String attachmentName
        {
            get { return AttachmentName; }
        }

        [ActivityOutput, ActivityFilter]
        public String attachmentPath
        {
            get { return AttachmentPath; }
        }
    }
}

