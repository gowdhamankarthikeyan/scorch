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
    [Activity("Modify Program's Supported Platform")]
    public class ModifyProgramSupportedPlatform : IActivity
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
            designer.AddInput("Existing Program Name").WithDefaultValue("Program Name");
            designer.AddInput("New Max Version").WithDefaultValue("5.10.9999.9999");
            designer.AddInput("New Min Version").WithDefaultValue("5.10.0000.0");
            designer.AddInput("New Name").WithDefaultValue("Win NT");
            designer.AddInput("New Platform").WithDefaultValue("I386");
            
            designer.AddCorellatedData(typeof(program));
            designer.AddOutput("Number of Programs");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String pkgID = request.Inputs["Existing Package ID"].AsString();
            String prgName = request.Inputs["Existing Program Name"].AsString();
            String prgOSMaxVersion = request.Inputs["New Max Version"].AsString();
            String prgOSMinVersion = request.Inputs["New Min Version"].AsString();
            String prgOSName = request.Inputs["New Name"].AsString();
            String prgOSPlatform = request.Inputs["New Platform"].AsString();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                CM2012Interop.modifySCCMProgramSupportedPlatforms(connection, pkgID, prgName, prgOSMaxVersion, prgOSMinVersion, prgOSName, prgOSPlatform);

                IResultObject col = CM2012Interop.getSCCMProgram(connection, "PackageID LIKE '" + pkgID + "' AND ProgramName LIKE '" + prgName + "'");

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

