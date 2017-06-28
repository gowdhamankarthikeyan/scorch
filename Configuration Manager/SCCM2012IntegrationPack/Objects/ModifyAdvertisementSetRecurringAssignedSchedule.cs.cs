using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCM2012Interop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;


namespace SCCM2012IntegrationPack
{
    [Activity("Modify SCCM Advertisement: Set Recurring Assignment Schedule")]
    public class ModifyAdvertisementSetRecurringAssignedSchedule : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            int[] hourDuration = new int[24];
            for (int i = 0; i < 24; i++)
            {
                hourDuration[i] = i;
            }

            int[] minuteDuration = new int[60];
            for (int i = 0; i < 60; i++)
            {
                minuteDuration[i] = i;
            }

            designer.AddInput("Advertisement ID").WithDefaultValue("ABC00000");

            designer.AddInput("Start Time").WithDefaultValue("5/5/2005 5:00PM").WithDateTimeBrowser();
            
            designer.AddInput("Day Duration").WithDefaultValue(0);
            designer.AddInput("Hour Duration").WithDefaultValue(0).WithListBrowser(hourDuration);
            designer.AddInput("Minute Duration").WithDefaultValue(0).WithListBrowser(minuteDuration);
            
            designer.AddInput("Day Span").WithDefaultValue(0);
            designer.AddInput("Hour Span").WithDefaultValue(0).WithListBrowser(hourDuration);
            designer.AddInput("Minute Span").WithDefaultValue(0).WithListBrowser(minuteDuration);
            
            designer.AddInput("Is GMT").WithDefaultValue(true).WithBooleanBrowser();

            designer.AddCorellatedData(typeof(advertisement));
            designer.AddOutput("Number of Advertisements");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Advertisement ID"].AsString();

            DateTime startTime = request.Inputs["Start Time"].AsDateTime();

            int dayDuration = request.Inputs["Day Duration"].AsInt32();
            int hourDuration = request.Inputs["Hour Duration"].AsInt32();
            int minuteDuration = request.Inputs["Minute Duration"].AsInt32();

            int daySpan = request.Inputs["Day Span"].AsInt32();
            int hourSpan = request.Inputs["Hour Span"].AsInt32();
            int minuteSpan = request.Inputs["Minute Span"].AsInt32();

            bool isGMT = request.Inputs["Is GMT"].AsBoolean();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                CM2012Interop.modifySCCMAdvertisementAddAssignmentScheduleRecurring(connection, objID, isGMT, daySpan, hourSpan, minuteSpan, dayDuration, hourDuration, minuteDuration, startTime);

                IResultObject col = CM2012Interop.getSCCMAdvertisement(connection, "AdvertisementID LIKE '" + objID + "'");
                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));
                }
                response.Publish("Number of Advertisements", ObjCount);
            }
        }
        private IEnumerable<advertisement> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new advertisement(obj);
            }
        }
    }
}

