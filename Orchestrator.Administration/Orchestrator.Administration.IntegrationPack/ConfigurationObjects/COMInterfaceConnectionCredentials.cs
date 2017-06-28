using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Orchestrator.Administration.IntegrationPack
{
    [ActivityData("Orchestrator COM Connection")]
    public class COMInterfaceConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;

        [ActivityInput(Default="UserName")]
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
        [ActivityInput(Default="Contoso.com")]
        public String UserDomain
        {
            get { return domain; }
            set { domain = value; }
        }
    }
}
