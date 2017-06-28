using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using SCCMInterop;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;

namespace SCCMExtension
{
    [Activity("Delete SCCM Computer")]
    public class DeleteComputer : IActivity
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
            designer.AddInput("Filter Query").WithDefaultValue("ResourceID='375'");

            designer.AddCorellatedData(typeof(system));
            designer.AddOutput("Number of Systems");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String filter = request.Inputs["Filter Query"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {  
                IResultObject col = CMInterop.getSCCMObject(connection, "SMS_R_System", filter);

                List<system> systemCollection = new List<system>();
                foreach (IResultObject obj in col)
                {
                    ObjCount++;
                    systemCollection.Add(new system(obj));
                }

                if (systemCollection != null)
                {
                    response.WithFiltering().PublishRange(getObjects(systemCollection));
                }
                response.Publish("Number of Systems", ObjCount);

                CMInterop.removeSCCMObject(connection, filter, "SMS_R_System");
            }
        }
        private IEnumerable<system> getObjects(List<system> systemCollection)
        {
            foreach(system obj in systemCollection)
            {
                yield return obj;
            }
        }
    }
}

