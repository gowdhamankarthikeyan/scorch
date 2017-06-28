using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Registry
{
    [Activity("Delete Registry Key Value",ShowFilters = false)]
    public class DeleteRegistryKeyValue : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Key Path:  HKLM:\\");
            designer.AddInput("Value Name");
            designer.AddInput("Server Name").WithComputerBrowser();
            designer.AddOutput("Results").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String results = "Failure";
            String KeyPath = request.Inputs["Key Path:  HKLM:\\"].AsString();
            String ServerName = request.Inputs["Server Name"].AsString();
            String ValueName = request.Inputs["Value Name"].AsString();

            try
            {
                RegistryKey rk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName);
                rk = rk.OpenSubKey(KeyPath);
                rk.DeleteValue(ValueName);
                results = "Success";
            }
            catch (Exception ex)
            {
                response.LogErrorMessage(ex.Message.ToString());
                throw ex;
            }

            response.Publish("Results", results);
        }
    }
}

