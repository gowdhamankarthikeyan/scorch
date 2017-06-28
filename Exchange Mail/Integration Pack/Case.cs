using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [ActivityData("Case")]
    public class Case
    {
        private String CaseID = String.Empty;


        internal Case(String CaseID)
        {
            this.CaseID = CaseID;

        }

        [ActivityOutput, ActivityFilter]
        public String caseID
        {
            get { return CaseID; }
        }

    }
}

