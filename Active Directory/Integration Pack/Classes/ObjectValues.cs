using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
namespace Active_Directory
{
    [ActivityData("Object Value")]
    public class ObjectValues
    {
        private String objectName;
        private String propertyName;
        private String propertyValue;

        internal ObjectValues(String objectName, String propertyName, String propertyValue)
        {
            try
            {
                this.objectName = objectName;
                this.propertyName = propertyName;
                this.propertyValue = propertyValue;
            }
            catch { }
        }

        [ActivityOutput, ActivityFilter]
        public String Object_Name
        {
            get { return objectName; }
        }

        [ActivityOutput, ActivityFilter]
        public String Property_Name
        {
            get { return propertyName; }
        }

        [ActivityOutput, ActivityFilter]
        public String Property_Value
        {
            get { return propertyValue; }
        }
    }
}

