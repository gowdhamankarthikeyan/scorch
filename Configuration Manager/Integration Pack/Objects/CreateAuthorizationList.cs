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
    [Activity("Create SCCM Authorization List")]
    public class CreateAuthorizationList : IActivity
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
            designer.AddInput("Display Name").WithDefaultValue("Authorization List Display Name");
            designer.AddInput("Description").WithDefaultValue("4 CI_IDs - 9,34,53,72");
            designer.AddInput("Informative URL").WithDefaultValue("Test URL");
            designer.AddInput("LocaleID").WithDefaultValue("1033");
            designer.AddInput("Updates List (CSV)").WithDefaultValue("9,34,53,72");
            designer.AddCorellatedData(typeof(authorizationList));
            designer.AddOutput("Number of Authorization Lists");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String DisplayName = request.Inputs["Display Name"].AsString();
            String Description = request.Inputs["Description"].AsString();
            String InformativeURL = request.Inputs["Informative URL"].AsString();
            String LocaleID = request.Inputs["LocaleID"].AsString();
            String UpdatesList = request.Inputs["Updates List (CSV)"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);

            using(connection)
            {
                try
                {
                    IResultObject col = CMInterop.createSCCMAuthorizationList(connection, DisplayName, Description, InformativeURL, LocaleID, UpdatesList);
                    if (col != null)
                    {
                        response.WithFiltering().PublishRange(getObjects(col));

                    }
                }
                catch (Exception ex)
                {
                    response.LogErrorMessage(ex.Message);
                }

                response.Publish("Number of Packages", ObjCount);
            }
        }
        private IEnumerable<package> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new package(obj);
            }
        }
    }
}

