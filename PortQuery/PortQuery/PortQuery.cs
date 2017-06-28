using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Net.Sockets;
using System.Net;
namespace PortQuery
{
    [Activity("Port Query")]
    public class PortQuery : IActivity
    {       
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Machine Name");
            designer.AddInput("Port Number");

            designer.AddOutput("Port Status");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string portStatus = "Open";

            string machineName = request.Inputs["Machine Name"].AsString();
            int portNumber = request.Inputs["Port Number"].AsInt32();

            TcpClient portChecker = new TcpClient();

            try
            {
                portChecker.Connect(machineName, portNumber);
            }
            catch
            {
                portStatus = "Closed";
            }
            finally
            {
                portChecker.Close();
            }

            response.Publish("Port Status", portStatus);
        }

    }
}
