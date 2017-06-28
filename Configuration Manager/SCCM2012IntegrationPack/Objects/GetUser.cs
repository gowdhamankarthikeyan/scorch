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
    [Activity("Get SCCM User")]
    public class GetUser : IActivity
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
            String[] argTypes = new String[4] { "ResourceID", "UserName", "DomainName", "Query" };
            designer.AddInput("Argument Type").WithListBrowser(argTypes).WithDefaultValue("UserName");
            designer.AddInput("Argument String").WithDefaultValue("User Name");
            designer.AddCorellatedData(typeof(user));
            designer.AddOutput("Number of Users");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String argType = request.Inputs["Argument Type"].AsString();
            String argString = request.Inputs["Argument String"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;

                //Get Computer Collection
                switch (argType)
                {
                    case "ResourceID":
                        col = CM2012Interop.getSCCMUser(connection, argString, "", "");
                        break;
                    case "UserName":
                        col = CM2012Interop.getSCCMUser(connection, "", argString, "");
                        break;
                    case "DomainName":
                        col = CM2012Interop.getSCCMUser(connection, "", "", argString);
                        break;
                    case "Query":
                        col = CM2012Interop.getSCCMObject(connection, "SMS_R_User", argString);
                        break;
                }

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Users", ObjCount);
            }
        }
        private IEnumerable<user> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new user(obj);
            }
        }
    }
}

