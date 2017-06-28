using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;


namespace SQL
{
    [ActivityData("SQL Database Connection")]
    public class ConnectionCredentials
    {
        [ActivityInput]
        public String DatabaseServer
        {

            get;
            set;
        }
        [ActivityInput]
        public String InitialCatalog
        {

            get;
            set;
        }
        [ActivityInput(Default = 50)]
        public int MaxParameterCount
        {
            get;
            set;
        }
    }
}
