using System;
using System.Management.Automation;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace VMWareIntegrationPack
{
    [ActivityData("Virtual Center Connection Settings")]
    public class ConnectionSettings
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String port = String.Empty;
        private String virtualCenter = String.Empty;
        
        [ActivityInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInput]
        public String Port
        {
            get { return port; }
            set { port = value; }
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

        [ActivityInput]
        public String VirtualCenter
        {
            get { return virtualCenter; }
            set { virtualCenter = value; }
        }
    }
}

