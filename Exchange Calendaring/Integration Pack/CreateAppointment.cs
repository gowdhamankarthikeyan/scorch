using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeCalendaring
{
    [Activity("Create Appointment", ShowFilters = false)]
    public class CreateAppointment : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;

        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;

        private String Subject = String.Empty;
        private String Body = String.Empty;
        private String Start = String.Empty;
        private String Time = String.Empty;
        private String Location = String.Empty;
        private String RequiredAttendees = String.Empty;
        private String OptionalAttendees = String.Empty;
        private String Calendar = String.Empty;
        private String serviceURL = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Appointment Subject");
            designer.AddInput("Calendar").NotRequired();
            designer.AddInput("Appointment Body");
            designer.AddInput("Appointment Start");
            designer.AddInput("Appointment Time (minutes)");
            designer.AddInput("Appointment Location");
            designer.AddInput("Required Attendees");
            designer.AddInput("Optional Attendees").NotRequired();
            designer.AddOutput("appointment ID");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            exchangeVersion = settings.ExchangeVersion;
            serviceURL = settings.ServiceUrl;

            Subject = request.Inputs["Appointment Subject"].AsString();
            Body = request.Inputs["Appointment Body"].AsString();
            Start = request.Inputs["Appointment Start"].AsString();
            Time = request.Inputs["Appointment Time (minutes)"].AsString();
            RequiredAttendees = request.Inputs["Required Attendees"].AsString();
            OptionalAttendees = request.Inputs["Optional Attendees"].AsString();

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

            Appointment app = new Appointment(service);

            FolderId fid = new FolderId(WellKnownFolderName.Calendar);

            if (!Calendar.Equals(String.Empty))
            {
                FindFoldersResults results = service.FindFolders(WellKnownFolderName.Root, new FolderView(int.MaxValue));

                foreach (Folder f in results)
                {
                    if (f.DisplayName.Equals(Calendar))
                    {
                        fid = f.Id;
                    }
                }
            }
            if (!RequiredAttendees.Equals(String.Empty))
            {
                foreach (String RequiredAttendee in RequiredAttendees.Split(','))
                {
                    app.RequiredAttendees.Add(new Attendee(RequiredAttendee));
                }
            }

            if (!OptionalAttendees.Equals(String.Empty))
            {
                foreach (String OptionalAttendee in OptionalAttendees.Split(','))
                {
                    app.OptionalAttendees.Add(new Attendee(OptionalAttendee));
                }
            }
            app.Subject = Subject;
            app.Body = Body;
            app.Start = Convert.ToDateTime(Start);
            app.End = app.Start.AddMinutes(Convert.ToDouble(Time));
            app.Location = Location;
            app.Save(fid, SendInvitationsMode.SendOnlyToAll);

            response.Publish("appointment ID", app.Id);
        }
    }
}

