using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace ExchangeMail
{
    [ActivityData("Exchange Mailbox Settings")]
    public class MailboxSettings
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String exchangeVersion = String.Empty;
        private String serviceUrl = "Autodiscover";

        [ActivityInput("User Name")]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInput("Exchange Version",Options="Exchange2007_SP1,Exchange2010,Exchange2010_SP1,Auto",Default="Auto")]
        public String ExchangeVersion
        {
            get { return exchangeVersion; }
            set { exchangeVersion = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        
        [ActivityInput]
        public String Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        [ActivityInput("ServiceUrl",Default="Autodiscover")]
        public String ServiceUrl
        {
            get { return serviceUrl; }
            set { serviceUrl = value; }
        }
    }
}

