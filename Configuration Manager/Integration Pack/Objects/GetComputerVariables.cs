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
    [Activity("Get Computer Variable(s)")]
    public class GetComputerVariables : IActivity
    {
        private  ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private String VariableName = String.Empty;
        private String MachineName = String.Empty;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Variable Name").NotRequired();
            designer.AddInput("Machine Name");
            designer.AddInput("Resource ID").NotRequired();
            designer.AddCorellatedData(typeof(ComputerVariable));
            designer.AddOutput("Number of Machine Variables");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String varType = request.Inputs["Variable Name"].AsString();
            MachineName = request.Inputs["Machine Name"].AsString();
            
            String resourceID = String.Empty;
            if (request.Inputs.Contains("Resource ID")) { resourceID = request.Inputs["Resource ID"].AsString(); }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                //Get Computer Object
                IResultObject computerObj = null;
                IResultObject col = null;
                if (resourceID.Equals(String.Empty)) { col = CMInterop.getSCCMComputer(connection, "", MachineName, ""); }
                else { col = CMInterop.getSCCMComputer(connection, resourceID, "", ""); }

                foreach (IResultObject c in col)
                {
                    computerObj = c;
                    if (computerObj != null)
                    {
                        //Get Variables from Comoputer Object
                        List<IResultObject> variableCollection = CMInterop.getSCCMComputerVariables(connection, computerObj["ResourceID"].IntegerValue.ToString());

                        if (variableCollection != null)
                        {
                            response.WithFiltering().PublishRange(getComputerVariables(variableCollection));
                            response.Publish("Number of Machine Variables", variableCollection.Count);
                        }
                        response.Publish("Number of Machine Variables", 0);
                    }
                    else
                    {
                        response.LogErrorMessage("Could Not Find Computer " + MachineName);
                    }
                }
            }
        }
        private IEnumerable<ComputerVariable> getComputerVariables(List<IResultObject> computerVariables)
        {
            foreach(IResultObject variable in computerVariables)
            {
                if (VariableName.Equals(String.Empty))
                {
                    yield return new ComputerVariable((String)variable.PropertyList["Name"], MachineName, variable.PropertyList["Value"].ToString());
                }
                else
                {
                    if(VariableName.Equals((String)variable.PropertyList["Name"]))
                    {
                        yield return new ComputerVariable((String)variable.PropertyList["Name"], MachineName, variable.PropertyList["Value"].ToString());
                    }
                }
            }
        }
    }
}

