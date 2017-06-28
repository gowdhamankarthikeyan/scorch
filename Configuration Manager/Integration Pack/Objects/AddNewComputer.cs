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
    [Activity("Add New SCCM Computer")]
    public class AddNewComputer : IActivity
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
            WqlConnectionManager connection = CMInterop.connectSCCMServer(settings.SCCMSERVER, settings.UserName, settings.Password);

            designer.AddInput("Netbios Name").WithDefaultValue("computerName");
            designer.AddInput("SM Bios GUID").NotRequired();
            designer.AddInput("MAC Address").NotRequired();

            designer.AddCorellatedData(typeof(system));
            designer.AddOutput("Number of Systems");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String netBiosName = null;
            String macAddress = null;
            String smBiosGuid = null;

            netBiosName = request.Inputs["Netbios Name"].AsString();

            if (request.Inputs.Contains("SM Bios GUID")) { smBiosGuid = request.Inputs["SM Bios GUID"].AsString(); }
            if (request.Inputs.Contains("MAC Address")) { macAddress = request.Inputs["MAC Address"].AsString(); }
            
            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {
                int resourceID = CMInterop.addNewComputer(connection, netBiosName, smBiosGuid, macAddress);

                IResultObject col = null;
                col = CMInterop.getSCCMComputer(connection, Convert.ToString(resourceID), "", "");

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

