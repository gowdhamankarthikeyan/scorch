using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [ActivityData("ExportedEmail")]
    public class ExportedEmail
    {
        private String EmailName = String.Empty;
        private String EmailPath = String.Empty;
        private String EmailID = String.Empty;

        internal ExportedEmail(String EmailName, String EmailPath, String EmailID)
        {
            this.EmailName = EmailName;
            this.EmailPath = EmailPath;
            this.EmailID = EmailID;
        }

        [ActivityOutput, ActivityFilter]
        public String emailName
        {
            get { return EmailName; }
        }

        [ActivityOutput, ActivityFilter]
        public String emailPath
        {
            get { return EmailPath; }
        }

        [ActivityOutput, ActivityFilter]
        public String emailID
        {
            get { return EmailID; }
        }
    }
}


