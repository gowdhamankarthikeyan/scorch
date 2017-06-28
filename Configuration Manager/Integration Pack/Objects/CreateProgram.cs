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
    [Activity("Create SCCM Program")]
    public class CreateProgram : IActivity
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
            designer.AddInput("Existing Package ID").WithDefaultValue("ABC00000");
            designer.AddInput("New Program Name").WithDefaultValue("Program Name");
            designer.AddInput("New Program Comment").WithDefaultValue("Comments").NotRequired();
            designer.AddInput("New Program Command Line").WithDefaultValue("Comments");
            designer.AddInput("New Program Max Runtime").WithDefaultValue("3600");
            designer.AddInput("New Program Working Directory").NotRequired();

            designer.AddCorellatedData(typeof(program));
            designer.AddOutput("Number of Programs");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String pkgID = request.Inputs["Existing Package ID"].AsString();
            String prgName = request.Inputs["New Program Name"].AsString();
            String prgComment = request.Inputs["New Program Comment"].AsString();
            String prgCommandLine = request.Inputs["New Program Command Line"].AsString();
            int prgMaxRunTime = (int)request.Inputs["New Program Max Runtime"].AsUInt32();
            String prgWorkingDirectory = request.Inputs["New Program Working Directory"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CMInterop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = CMInterop.createSCCMProgram(connection, pkgID, prgName, prgComment, prgCommandLine, prgMaxRunTime, prgWorkingDirectory);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

                }
                response.Publish("Number of Programs", ObjCount);
            }
        }
        private IEnumerable<program> getObjects(IResultObject objList)
        {
            foreach (IResultObject obj in objList)
            {
                ObjCount++;
                yield return new program(obj);
            }
        }
    }
}

