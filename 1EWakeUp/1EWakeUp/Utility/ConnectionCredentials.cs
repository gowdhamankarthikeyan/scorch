using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;

namespace _1EWakeUp.Utility
{
    [ActivityData("SCCM Connection Settings")]
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String SCCMServer = String.Empty;
        private String siteCode = String.Empty;
        [ActivityInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInput("SCCM Server")]
        public String SCCMSERVER
        {
            get { return SCCMServer; }
            set { SCCMServer = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}

