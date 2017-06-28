using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Active_Directory
{
    [ActivityData("Object")]
    public class ADObject
    {
        private String objectLDAPPath = String.Empty;

        internal ADObject(String objectLDAPPath)
        {
            this.objectLDAPPath = objectLDAPPath;
        }

        [ActivityOutput, ActivityFilter]
        public String Object_LDAP_Path
        {
            get { return objectLDAPPath; }
        }
    }
}

