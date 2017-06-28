using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;


namespace Active_Directory
{
    [ActivityData("AD Connection Credentials")]
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;

        [ActivityInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        [ActivityInput]
        public String Domain
        {
            get { return domain; }
            set { domain = value; }
        }
    }
}

