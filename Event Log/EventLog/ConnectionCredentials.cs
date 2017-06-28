using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;

namespace EventLog
{
    [OpalisData("Event Log Connection Credentials")]
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;

        [OpalisInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [OpalisInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        [OpalisInput]
        public String Domain
        {
            get { return domain; }
            set { domain = value; }
        }
    }
}
