using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Registry
{
    [Activity("Set Registry Key String Value")]
    public class ModifyRegistryKeyValueString : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Key Path:  HKLM:\\");
            designer.AddInput("Value Name");
            designer.AddInput("New Value");
            designer.AddInput("Registry Hive").NotRequired().WithDefaultValue("64 Bit").WithListBrowser(new string[] { "64 Bit", "32 Bit" }); designer.AddInput("Server Name").WithComputerBrowser();
            designer.AddOutput("Results").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String results = "";
            String KeyPath = request.Inputs["Key Path:  HKLM:\\"].AsString();
            String ServerName = request.Inputs["Server Name"].AsString();
            String ValueName = request.Inputs["Value Name"].AsString();
            String NewValue = request.Inputs["New Value"].AsString();
            string registryHive = "default";
            if (request.Inputs.Contains("Registry Hive")) { registryHive = request.Inputs["Registry Hive"].AsString(); }
            
            try
            {
                RegistryKey rk;
                switch (registryHive.ToLower())
                {
                    case "32 Bit":
                        //rk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName, RegistryView.Registry32);
                        rk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName);
                        break;
                    case "64 Bit":
                        //rk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName, RegistryView.Registry64);
                        rk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName);
                        break;
                    default:
                        rk = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName);
                        break;
                }
                
                RegistryKey sk = rk.CreateSubKey(KeyPath);
                sk.SetValue(ValueName, NewValue);
                results = sk.GetValue(ValueName).ToString();
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

