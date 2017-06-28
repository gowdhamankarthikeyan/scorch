using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [Activity("Set Mailbox Out Of Office")]
    public class SetUserOof : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String emailAddress = String.Empty;
        private String userName = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;
        private String password = String.Empty;
        
        private String userEmailAddr = String.Empty;
        private String State = String.Empty;
        private DateTime StartTime = DateTime.Now;
        private DateTime EndTime = DateTime.Now;
        private String ExternalAudience = String.Empty;
        private String InternalReply = String.Empty;
        private String ExternalReply = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            OofSettings newOofSettings = new OofSettings();

            designer.AddInput("Mailbox Email Address").WithDefaultValue("User.Name@contoso.com");
            designer.AddInput("External Audiance").WithListBrowser(new string[] {"All","Known","None"}).WithDefaultValue("All").NotRequired();
            designer.AddInput("Start Time").WithDateTimeBrowser().WithDefaultValue(DateTime.Now.ToString()).NotRequired();
            designer.AddInput("End Time").WithDateTimeBrowser().WithDefaultValue(DateTime.Now.AddDays(1).ToString()).NotRequired();
            designer.AddInput("External Reply").WithDefaultValue("I'm currently out of office. Please contact my manager for critical issues. Thanks!").NotRequired();
            designer.AddInput("Internal Reply").WithDefaultValue("I am currently out of the office but will reply to emails when I return. Thanks!").NotRequired();
            designer.AddInput("State").WithListBrowser(new string[] { "Disabled", "Enabled", "Scheduled" }).WithDefaultValue("Enabled").NotRequired();
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

            if(request.Inputs.Contains("External Audiance"))
            {
                ExternalAudience = request.Inputs["External Audiance"].AsString();
                switch (ExternalAudience)
                {
                    case "All":
                        userOOFSettings.ExternalAudience = OofExternalAudience.All;
                        break;
                    case "Known":
                        userOOFSettings.ExternalAudience = OofExternalAudience.Known;
                        break;
                    case "None":
                        userOOFSettings.ExternalAudience = OofExternalAudience.None;
                        break;
                    default:
                        response.LogWarningMessage("State must be one of the following: Disabled, Enabled, Scheduled");
                        break;
                }
            }
            if (request.Inputs.Contains("Start Time"))
            {
                try
                {
                    StartTime = Convert.ToDateTime(request.Inputs["Start Time"].AsString());
                    userOOFSettings.Duration.StartTime = StartTime;
                }
                catch
                {
                    response.LogWarningMessage("Start Time must be a valid Date Time: Entered Value was " + request.Inputs["Start Time"].AsString());
                }
            }
            if (request.Inputs.Contains("End Time"))
            {
                try
                {
                    EndTime = Convert.ToDateTime(request.Inputs["End Time"].AsString());
                    userOOFSettings.Duration.EndTime = EndTime;
                }
                catch
                {
                    response.LogWarningMessage("End Time must be a valid Date Time: Entered Value was " + request.Inputs["End Time"].AsString());
                }
            }
            if (request.Inputs.Contains("External Reply"))
            {
                ExternalReply = request.Inputs["External Reply"].AsString();
                userOOFSettings.ExternalReply = ExternalReply;
            }
            if (request.Inputs.Contains("Internal Reply"))
            {
                InternalReply = request.Inputs["Internal Reply"].AsString();
                userOOFSettings.InternalReply = InternalReply;
            }
            if (request.Inputs.Contains("State"))
            {
                State = request.Inputs["State"].AsString();
                switch (State)
                {
                    case "Disabled":
                        userOOFSettings.State = OofState.Disabled;
                        break;
                    case "Enabled":
                        userOOFSettings.State = OofState.Enabled;
                        break;
                    case "Scheduled":
                        userOOFSettings.State = OofState.Scheduled;
                        break;
                    default:
                        response.LogWarningMessage("State must be one of the following: Disabled, Enabled, Scheduled");
                        break;
                }
            }

            service.SetUserOofSettings(userEmailAddr, userOOFSettings);
            userOOFSettings = service.GetUserOofSettings(userEmailAddr);
            response.WithFiltering().PublishRange(getOOF(userOOFSettings));
        }

        private IEnumerable<OutOfOffice> getOOF(OofSettings userSettings)
        {
            yield return new OutOfOffice(userSettings, userEmailAddr);
        }
    }
}