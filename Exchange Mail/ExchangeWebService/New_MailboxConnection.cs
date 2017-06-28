using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.IO;

namespace ExchangeWebService
{
    [Cmdlet(VerbsCommon.New, "EWSMailboxConnection")]
    public class New_MailboxConnection : PSCmdlet
    {
        #region Parameters

        [Parameter(
           Position = 0,
           Mandatory = false
        )]
        public ExchangeVersion? exchangeVersion
        {
            get;
            set;
        }
        [Parameter(
           Position = 1,
           Mandatory = false
        )]
        public PSCredential Credential
        {
            get;
            set;
        }

        [Parameter(
           Position = 2,
           Mandatory = false,
           ValueFromPipeline = true
        )]
        public string webserviceURL
        {
            get { return _webserviceURL; }
            set { _webserviceURL = value; }
        }
        private string _webserviceURL = "Autodiscover";

        [Parameter(
           Position = 3,
           Mandatory = false,
           ValueFromPipeline = true
        )]
        public string alternateMailboxSMTPAddress
        {
            get { return _alternateMailboxSMTPAddress; }
            set { _alternateMailboxSMTPAddress = value; }
        }
        private string _alternateMailboxSMTPAddress = string.Empty;
        #endregion
        protected override void ProcessRecord()
        {
            ExchangeService service = new ExchangeService();
            if (exchangeVersion.HasValue) { service = new ExchangeService(exchangeVersion.Value); }
            

            service.Credentials = Credential.GetNetworkCredential();

            string username = Credential.GetNetworkCredential().UserName;
            string userDomain = Credential.GetNetworkCredential().Domain;

            String AccountUrl = String.Empty;

            if (username.Contains("@")) { if (username.ToLower().EndsWith(".com")) { AccountUrl = username; } else { AccountUrl = string.Format("{0}.com", username); } }
            else
            {
                if (!userDomain.ToLower().EndsWith(".com")) { userDomain += ".com"; }
                AccountUrl = string.Format("{0}@{1}", username, userDomain);
            }

            if (webserviceURL.Equals("Autodiscover")) { service.AutodiscoverUrl(AccountUrl, RedirectionUrlValidationCallback); }
            else { service.Url = new Uri(webserviceURL); }

            if (!alternateMailboxSMTPAddress.Equals(String.Empty)) { service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, alternateMailboxSMTPAddress); }

            WriteObject(service);
        }

        
            

        // Create the callback to validate the redirection URL.
        private static bool RedirectionUrlValidationCallback(String redirectionUrl)
        {
            // Perform validation.
            return true;
        }
    }
}
