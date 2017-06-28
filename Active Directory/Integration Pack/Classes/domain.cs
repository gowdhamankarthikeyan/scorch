using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
namespace Active_Directory
{
    [ActivityData("Domain")]
    public class domain
    {
        private String domainName = String.Empty;

        internal domain(String domainName)
        {
            this.domainName = domainName;
        }

        [ActivityOutput, ActivityFilter]
        public String DomainName
        {
            get { return domainName; }
        }
    }
}

