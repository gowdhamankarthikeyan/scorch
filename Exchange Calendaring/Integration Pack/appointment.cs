using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeCalendaring
{
    [ActivityData("Appointment")]
    public class appointment
    {
        private String subject = String.Empty;
        private String body = String.Empty;
        private String id = String.Empty;
        private String StartTime = String.Empty;
        private String EndTime = String.Empty;
        private String Location = String.Empty;
        private String RequiredAttendees = String.Empty;
        private String OptionalAttendees = String.Empty;
        private String Calendar = String.Empty;

        internal appointment(String subject, String body, String StartTime, String EndTime, String Location, String RequiredAttendees, String OptionalAttendees, String id, String Calendar)
        {
            this.subject = subject;
            this.body = body;
            this.id = id;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Location = Location;
            this.RequiredAttendees = RequiredAttendees;
            this.OptionalAttendees = OptionalAttendees;
            this.Calendar = Calendar;
        }

        [ActivityOutput, ActivityFilter]
        public String Start_Time
        {
            get { return StartTime; }
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
        public String End_Time
        {
            get { return EndTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get { return id; }
        }
        [ActivityOutput, ActivityFilter]
        public String location
        {
            get { return Location; }
        }
        [ActivityOutput, ActivityFilter]
        public String Required_Attendees
        {
            get { return RequiredAttendees; }
        }
        [ActivityOutput, ActivityFilter]
        public String Optional_Attendees
        {
            get { return OptionalAttendees; }
        }
        [ActivityOutput, ActivityFilter]
        public String calendar
        {
            get { return Calendar; }
        }
    }
}

