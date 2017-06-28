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
    [Activity("Associate SCCM Computer Record")]
    public class AssociateComputerRecord : IActivity
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
            designer.AddInput("Reference Computer Resource ID");
            designer.AddInput("Destination Computer Resource ID");

            designer.AddCorellatedData(typeof(system));
            designer.AddOutput("Number of Systems");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            int referenceID = (int)request.Inputs["Reference Computer Resource ID"].AsUInt32();
            int destinationID = (int)request.Inputs["Destination Computer Resource ID"].AsUInt32();


            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {             
                CM2012Interop.associateComputer(connection, referenceID, destinationID);

                IResultObject col = null;
                col = CM2012Interop.getSCCMComputer(connection, Convert.ToString(destinationID), "", "");

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Systems", ObjCount);
            }
        }
        private IEnumerable<system> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new system(obj);
            }
        }
    }
}

