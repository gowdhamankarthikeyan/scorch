using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Get User Out Of Office")]
    public class GetUserOof : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String emailAddress = String.Empty;
        private String userName = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;
        private String password = String.Empty;
        private String userEmailAddr = String.Empty;
        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Mailbox Email Address").WithDefaultValue("User.Name@contoso.com");
            designer.AddCorellatedData(typeof(OutOfOffice));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            userEmailAddr = request.Inputs["Mailbox Email Address"].AsString();

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

            if (serviceURL.Equals("Autodiscover")) { service.AutodiscoverUrl(userEmailAddr, (a) => { return true; }); }
            else { service.Url = new Uri(serviceURL); }
            
            OofSettings userOOFSettings = service.GetUserOofSettings(userEmailAddr);
            response.WithFiltering().PublishRange(getOOF(userOOFSettings));   
        }

        private IEnumerable<OutOfOffice> getOOF(OofSettings userSettings)
        {
            yield return new OutOfOffice(userSettings, userEmailAddr);
        }
    }
}