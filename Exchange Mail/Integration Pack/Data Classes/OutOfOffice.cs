using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [ActivityData("OutOfOffice")]
    public class OutOfOffice
    {
        private DateTime StartTime = new DateTime();
        private DateTime EndTime = new DateTime();
        private String State = String.Empty;
        private String ExternalAudience = String.Empty;
        private String InternalReply = String.Empty;
        private String ExternalReply = String.Empty;
        private String UserEmail = String.Empty;
        private Boolean AllowExternalOof = false;


        internal OutOfOffice(OofSettings outofOfficeSetting, String emailAddress)
        {
            try { this.StartTime = outofOfficeSetting.Duration.StartTime; }
            catch { }

            try { this.EndTime = outofOfficeSetting.Duration.EndTime; }
            catch { }
            try { this.State = outofOfficeSetting.State.ToString(); }
            catch { }
            try { this.ExternalAudience = outofOfficeSetting.ExternalAudience.ToString(); }
            catch { }
            try { this.InternalReply = outofOfficeSetting.InternalReply.Message.ToString(); }
            catch { }
            try { this.ExternalReply = outofOfficeSetting.ExternalReply.Message.ToString(); }
            catch { }
            try { this.AllowExternalOof = Convert.ToBoolean(outofOfficeSetting.AllowExternalOof); }
            catch { }
            try { this.UserEmail = emailAddress; }
            catch { }
        }
        [ActivityOutput, ActivityFilter]
        public DateTime startTime
        {
            get { return StartTime; }
        }
        [ActivityOutput, ActivityFilter]
        public DateTime endTime
        {
            get { return EndTime; }
        }
        [ActivityOutput, ActivityFilter]
        public String state
        {
            get { return State; }
        }
        [ActivityOutput, ActivityFilter]
        public String externalAudience
        {
            get { return ExternalAudience; }
        }
        [ActivityOutput, ActivityFilter]
        public String internalReply
        {
            get { return InternalReply; }
        }
        [ActivityOutput, ActivityFilter]
        public String externalReply
        {
            get { return ExternalReply; }
        }
        [ActivityOutput, ActivityFilter]
        public String userEmailAddr
        {
            get { return UserEmail; }
        }
        [ActivityOutput, ActivityFilter]
        public Boolean allowExternalOof
        {
            get { return AllowExternalOof; }
        }
    }
}

