using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace OrchestratorIP.ConfigurationObjects
{
    [ActivityData("Orchestrator Webservice Connection Credentials")]
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String orchestratorServiceURL = String.Empty;
        private int maxInputParameters = 100;

        [ActivityInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        [ActivityInput(Default=50)]
        public int MaxInputParameters
        {
            get { return maxInputParameters; }
            set { maxInputParameters = value; }
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
        public String OrchestratorServiceURL
        {
            get { return orchestratorServiceURL; }
            set { orchestratorServiceURL = value; }
        }
    }
}
