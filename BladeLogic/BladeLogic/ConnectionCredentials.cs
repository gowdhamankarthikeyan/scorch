using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;


namespace BladeLogic
{
    [ActivityData("Blade Logic Connection Creds")]
    public class ConnectionCredentials
    {
        [ActivityInput]
        public String UserName
        {
            get;
            set;
        }
        
        [ActivityInput(PasswordProtected = true)]
        public String Password
        {
            get;
            set;
        }
        [ActivityInput]
        public String serverName
        {
            get;
            set;
        }
    }
}
