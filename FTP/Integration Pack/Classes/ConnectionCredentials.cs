using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace SCORCHDev_FTP
{
    [ActivityData("FTP Connection Settings")]
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;

        [ActivityInput(Default="User@contoso.com")]
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
    }
}
