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
    [Activity("Get SCCM Usergroup")]
    public class GetUserGroup : IActivity
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
            String[] argTypes = new String[4] { "ResourceID", "GroupName", "DomainName", "Query" };
            designer.AddInput("Argument Type").WithListBrowser(argTypes).WithDefaultValue("GroupName");
            designer.AddInput("Argument String").WithDefaultValue("Group Name");
            designer.AddCorellatedData(typeof(userGroup));
            designer.AddOutput("Number of Usergroups");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String argType = request.Inputs["Argument Type"].AsString();
            String argString = request.Inputs["Argument String"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = null;

                //Get Computer Collection
                switch (argType)
                {
                    case "ResourceID":
                        col = CMInterop.getSCCMUserGroup(connection, argString, "", "");
                        break;
                    case "GroupName":
                        col = CMInterop.getSCCMUserGroup(connection, "", argString, "");
                        break;
                    case "DomainName":
                        col = CMInterop.getSCCMUserGroup(connection, "", "", argString);
                        break;
                    case "Query":
                        col = CMInterop.getSCCMObject(connection, "SMS_R_Usergroup", argString);
                        break;
                }

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Usergroups", ObjCount);
            }
        }
        private IEnumerable<userGroup> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new userGroup(obj);
            }
        }
    }
}

