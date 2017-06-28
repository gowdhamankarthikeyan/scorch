using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using System.Diagnostics;

namespace EventLog
{
    [OpalisObject("Poll Event Log")]
    public class PollEventLog
    {
        private ConnectionCredentials credentials;

        [OpalisConfiguration]
        public ConnectionCredentials Credentials
        {
            get { return credentials; }
            set { credentials = value; }
        }

        public void Design(IOpalisDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddInput("Log Name");
            designer.AddInput("Event ID");
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {

        }
    }
}
