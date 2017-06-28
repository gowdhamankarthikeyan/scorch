using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventLogInterop
{
    public class EventLogInterop
    {
        private String logName;
        private String userName;
        private String password;

        public EventLogInterop(String logName, String userName, String password)
        {
            this.logName = logName;
            this.userName = userName;
            this.password = password;
        }

        
    }
}
