using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Date
{
    [Activity("Get Current Date Time",ShowFilters = false)]
    public class GetCurrentDateTime : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            int Default = 0;
            designer.AddInput("OffsetDays").WithDefaultValue(Default);
            designer.AddInput("OffsetHours").WithDefaultValue(Default);
            designer.AddInput("OffsetMinutes").WithDefaultValue(Default);
            designer.AddInput("OffsetSeconds").WithDefaultValue(Default);

            designer.AddOutput("Calculated DateTime").AsDateTime();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            double offsetDays = request.Inputs["OffsetDays"].AsDouble();
            double offsetHours = request.Inputs["OffsetHours"].AsDouble();
            double offsetMinutes = request.Inputs["OffsetMinutes"].AsDouble();
            double offsetSeconds = request.Inputs["OffsetSeconds"].AsDouble();

            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(offsetDays);
            dateTime = dateTime.AddHours(offsetHours);
            dateTime = dateTime.AddMinutes(offsetMinutes);
            dateTime = dateTime.AddSeconds(offsetSeconds);

            response.Publish("Calculated DateTime", dateTime);
        }
    }
}

