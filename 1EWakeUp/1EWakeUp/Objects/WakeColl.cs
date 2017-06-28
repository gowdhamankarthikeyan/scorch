using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using _1EWakeUp.Utility;

namespace _1EWakeUp.Objects
{
    [Activity("Send WOL to SCCM Collection")]
    public class WakeColl : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Collection Name").WithDefaultValue("computerName");

            designer.AddOutput("Collection Name");
            designer.AddOutput("Status Message");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String collectionName = null;

            collectionName = request.Inputs["Collection Name"].AsString();

            _1EWakeUpCollection(collectionName);

            response.Publish("Collection Name", collectionName);
        }

        private void _1EWakeUpCollection(String collectionName)
        {
            ManagementScope scope = new ManagementScope(String.Format(@"\\{0}\root\N1E\WakeUp", SCCMServer));
            scope.Options.Username = userName;
            scope.Options.Password = password;

            scope.Connect();

            //Setup WQL Connection and WMI Management Scope
            try// Get the client's SMS_Client class.
            {
                ManagementClass cls = new ManagementClass(scope.Path.Path, "WakeUp", null);

                // Set up Netbios Name input parameter for sInArg.
                ManagementBaseObject inParams = cls.GetMethodParameters("WakeColl");
                inParams["sInArg"] = collectionName;

                // Assign the Site code.
                ManagementBaseObject outMPParams = cls.InvokeMethod("WakeColl", inParams, null);
            }
            catch (ManagementException e)
            {
                throw new Exception("Failed to execute method", e);
            }
        }
    }
}

