using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;
namespace ExchangeMail
{
    [Activity("Get Appointment", ShowFilters = false)]
    public class GetAppointment : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;

        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;

        private String AppointmentID = String.Empty;
        private String StartDate = String.Empty;
        private String EndDate = String.Empty;
        private String Calendar = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Appointment Id").NotRequired();
            designer.AddInput("Start Date").NotRequired();
            designer.AddInput("End Date").NotRequired();
            designer.AddInput("Calendar Name").NotRequired();
            designer.AddInput("Alternate Mailbox").NotRequired();

            designer.AddCorellatedData(typeof(appointment));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            exchangeVersion = settings.ExchangeVersion;
            serviceURL = settings.ServiceUrl;

            AppointmentID = request.Inputs["Appointment Id"].AsString();
            StartDate = request.Inputs["Start Date"].AsString();
            EndDate = request.Inputs["End Date"].AsString();
            Calendar = request.Inputs["Calendar Name"].AsString();

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

            if (serviceURL.Equals("Autodiscover")) { service.AutodiscoverUrl(AccountUrl); }
            else { service.Url = new Uri(serviceURL); }

            if (!alternateMailbox.Equals(String.Empty)) { service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, alternateMailbox); }

            response.WithFiltering().PublishRange(getAppointment(service));
        }

        private IEnumerable<appointment> getAppointment(ExchangeService service)
        {
            if (AppointmentID.Equals(String.Empty))
            {
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


                FindItemsResults<Appointment> appResults = service.FindAppointments(fid, new CalendarView(Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate)));

                foreach (Appointment app in appResults)
                {
                    String OptionalAttendees = String.Empty;
                    foreach (Attendee person in app.OptionalAttendees)
                    {
                        if (OptionalAttendees.Equals(String.Empty)) { OptionalAttendees = person.ToString(); }
                        else { OptionalAttendees += "," + person.ToString(); }
                    }

                    String RequiredAttendees = String.Empty;
                    foreach (Attendee person in app.OptionalAttendees)
                    {
                        if (RequiredAttendees.Equals(String.Empty)) { RequiredAttendees = person.ToString(); }
                        else { RequiredAttendees += "," + person.ToString(); }
                    }

                    String Subject = String.Empty;
                    String Body = String.Empty;
                    String Start = String.Empty;
                    String End = String.Empty;
                    String location = String.Empty;
                    String Id = String.Empty;
                    String ParentFolderId = String.Empty;

                    try { Subject = app.Subject.ToString(); }
                    catch { }
                    try { Body = app.Body.ToString(); }
                    catch { }
                    try { Start = app.Start.ToString(); }
                    catch { }
                    try { End = app.End.ToString(); }
                    catch { }
                    try { location = app.Location; }
                    catch { }
                    try { Id = app.Id.ToString(); }
                    catch { }
                    try { ParentFolderId = app.ParentFolderId.ToString(); }
                    catch { }
                    yield return new appointment(Subject, Body, Start, End, location, RequiredAttendees, OptionalAttendees, Id, ParentFolderId);
                }
            }
            else
            {
                Appointment app = Appointment.Bind(service, AppointmentID);

                String OptionalAttendees = String.Empty;
                foreach (Attendee person in app.OptionalAttendees)
                {
                    if (OptionalAttendees.Equals(String.Empty)) { OptionalAttendees = person.ToString(); }
                    else { OptionalAttendees += "," + person.ToString(); }
                }

                String RequiredAttendees = String.Empty;
                foreach (Attendee person in app.OptionalAttendees)
                {
                    if (RequiredAttendees.Equals(String.Empty)) { RequiredAttendees = person.ToString(); }
                    else { RequiredAttendees += "," + person.ToString(); }
                }
                String Subject = String.Empty;
                String Body = String.Empty;
                String Start = String.Empty;
                String End = String.Empty;
                String location = String.Empty;
                String Id = String.Empty;
                String ParentFolderId = String.Empty;

                try { Subject = app.Subject.ToString(); }
                catch { }
                try { Body = app.Body.ToString(); }
                catch { }
                try { Start = app.Start.ToString(); }
                catch { }
                try { End = app.End.ToString(); }
                catch { }
                try { location = app.Location; }
                catch { }
                try { Id = app.Id.ToString(); }
                catch { }
                try { ParentFolderId = app.ParentFolderId.ToString(); }
                catch { }
                yield return new appointment(Subject, Body, Start, End, location, RequiredAttendees, OptionalAttendees, Id, ParentFolderId);
            }
        }
    }
}

