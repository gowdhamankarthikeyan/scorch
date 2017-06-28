using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;
using _1EWakeUp.Utility;
using System.Net;
using System.Net.NetworkInformation;

namespace _1EWakeUp.Objects
{
    [Activity("Send WOL to SCCM Computer")]
    public class WakeMachine : IActivity
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
            designer.AddInput("Netbios Name").WithDefaultValue("computerName");
            designer.AddInput("Ping Timeout (Seconds)").NotRequired().WithDefaultValue("60");

            designer.AddOutput("Netbios Name");
            designer.AddOutput("Ping Result");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            //default timeout == 60 seconds
            int timeout = 60000;
            String netBiosName = null;


            if (request.Inputs.Contains("Ping Timeout (Seconds)")) { try { timeout = Convert.ToInt32(request.Inputs["Ping Timeout (Seconds)"].AsString()) * 1000; } catch { timeout = 60000; } } 
            netBiosName = request.Inputs["Netbios Name"].AsString();

            _1EWakeUp(netBiosName);

            PingReply reply;
            bool exit = false;
            do
            {
                reply = pingServer(response, 5000, netBiosName);
                timeout -= 5000;
                if (timeout <= 0)
                {
                    exit = true;
                }
                else
                {
                    if (reply != null)
                    {
                        if (reply.Status == IPStatus.Success)
                        {
                            exit = true;
                        }
                    }
                }
            }
            while (!exit);
            if (reply == null)
            {
                throw new Exception(String.Format("Machine {0} Failed to Ping", netBiosName));
            }
            else
            {
                if (reply.Status != IPStatus.Success)
                {
                    throw new Exception(String.Format("Machine {0} Failed to Ping", netBiosName));
                }
                else
                {
                    response.Publish("Ping Result", reply.Status);
                }
            }
           
            response.Publish("Netbios Name", netBiosName);
        }

        private PingReply pingServer(IActivityResponse response, int timeout, String netBiosName)
        {
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128, 
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                return pingSender.Send(netBiosName, timeout, buffer, options);
            }
            catch
            {
                return null;
            }
        }
        

        /// <summary>
        /// Create a WMI call to the 1E Wake Server
        /// </summary>
        /// <param name="netBiosName">netbios name of machine to wake up</param>
        private void _1EWakeUp(String netBiosName)
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
                ManagementBaseObject inParams = cls.GetMethodParameters("WakeName");
                inParams["sInArg"] = netBiosName;

                // Assign the Site code.
                ManagementBaseObject outMPParams = cls.InvokeMethod("WakeName", inParams, null);
            }
            catch (ManagementException e)
            {
                throw new Exception("Failed to execute method", e);
            }
        }
    }
}

