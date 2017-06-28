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
    [Activity("Modify SCCM Program: Set Program Flags")]
    public class ModifyProgramSetProgramFlags : IActivity
    {
        private ConnectionCredentials settings;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String SCCMServer = String.Empty;

        private int ObjCount = 0;

        private const uint AUTHORIZED_DYNAMIC_INSTALL = 0x00000001;
        private const uint USECUSTOMPROGRESSMSG = 0x00000002;
        private const uint DEFAULT_PROGRAM = 0x00000010;
        private const uint DISABLEMOMALERTONRUNNING = 0x00000020;
        private const uint MOMALERTONFAIL = 0x00000040;
        private const uint RUN_DEPENDANT_ALWAYS = 0x00000080;
        private const uint WINDOWS_CE = 0x00000100;
        private const uint NOT_USED = 0x00000200;
        private const uint COUNTDOWN = 0x00000400;
        private const uint FORCERERUN = 0x00000800;
        private const uint DISABLED = 0x00001000;
        private const uint UNATTENDED = 0x00002000;
        private const uint USERCONTEXT = 0x00004000;
        private const uint ADMINRIGHTS = 0x00008000;
        private const uint EVERYUSER = 0x00010000;
        private const uint NOUSERLOGGEDIN = 0x00020000;
        private const uint OKTOQUIT = 0x00040000;
        private const uint OKTOREBOOT = 0x00080000;
        private const uint USEUNCPATH = 0x00100000;
        private const uint PERSISTCONNECTION = 0x00200000;
        private const uint RUNMINIMIZED = 0x00400000;
        private const uint RUNMAXIMIZED = 0x00800000;
        private const uint HIDEWINDOW = 0x01000000;
        private const uint OKTOLOGOFF = 0x02000000;
        private const uint RUNACCOUNT = 0x04000000;
        private const uint ANY_PLATFORM = 0x08000000;
        private const uint STILL_RUNNING = 0x10000000;
        private const uint SUPPORT_UNINSTALL = 0x20000000;
        private const uint Platform_Not_Supported = 0x40000000;
        private const uint SHOW_IN_ARP = 0x80000000;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Package ID").WithDefaultValue("ABC00000");
            designer.AddInput("Program Name").WithDefaultValue("PROGRAM NAME");

            designer.AddInput("UNATTENDED").WithDefaultValue(true).WithBooleanBrowser();
            designer.AddInput("USER CONTEXT").WithDefaultValue(true).WithBooleanBrowser();
            designer.AddInput("EVERY USER").WithDefaultValue(true).WithBooleanBrowser();
            designer.AddInput("USE UNC PATH").WithDefaultValue(true).WithBooleanBrowser();

            designer.AddInput("Authorized Dynamic Install").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("USE CUSTOM PROGRESS MSG").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DEFAULT PROGRAM").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DISABLE MOM ALERT ON RUNNING").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("MOM ALERT ON FAIL").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN DEPENDANT ALWAYS").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("WINDOWS CE").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("COUNTDOWN").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("FORCE RERUN").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("DISABLED").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ADMIN RIGHTS").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("USE CUSTOM PROGRESS MSG").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("NO USER LOGGED IN").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("OK TO QUIT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("OK TO REBOOT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("PERSIST CONNECTION").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN MINIMIZED").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN MAXIMIZED").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("HIDE WINDOW").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("OK TO LOGOFF").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("RUN ACCOUNT").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("ANY PLATFORM").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("STILL RUNNING").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("SUPPORT UNINSTALL").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("Platform Not Supported").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("SHOW IN ARP").WithDefaultValue(false).WithBooleanBrowser();
            
            designer.AddCorellatedData(typeof(program));
            designer.AddOutput("Number of Programs");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String objID = request.Inputs["Package ID"].AsString();
            String prgName = request.Inputs["Program Name"].AsString();

            uint programFlags = 0x00000000;

            if (request.Inputs["UNATTENDED"].AsBoolean()) { programFlags ^= UNATTENDED; }
            if (request.Inputs["USER CONTEXT"].AsBoolean()) { programFlags ^= USERCONTEXT; }
            if (request.Inputs["EVERY USER"].AsBoolean()) { programFlags ^= EVERYUSER; }
            if (request.Inputs["USE UNC PATH"].AsBoolean()) { programFlags ^= USEUNCPATH; }
            if (request.Inputs["Authorized Dynamic Install"].AsBoolean()) { programFlags ^= AUTHORIZED_DYNAMIC_INSTALL; }
            if (request.Inputs["USE CUSTOM PROGRESS MSG"].AsBoolean()) { programFlags ^= USECUSTOMPROGRESSMSG; }
            if (request.Inputs["DEFAULT PROGRAM"].AsBoolean()) { programFlags ^= DEFAULT_PROGRAM; }
            if (request.Inputs["DISABLE MOM ALERT ON RUNNING"].AsBoolean()) { programFlags ^= DISABLEMOMALERTONRUNNING; }
            if (request.Inputs["MOM ALERT ON FAIL"].AsBoolean()) { programFlags ^= MOMALERTONFAIL; }
            if (request.Inputs["RUN DEPENDANT ALWAYS"].AsBoolean()) { programFlags ^= RUN_DEPENDANT_ALWAYS; }
            if (request.Inputs["WINDOWS CE"].AsBoolean()) { programFlags ^= WINDOWS_CE; }
            if (request.Inputs["COUNTDOWN"].AsBoolean()) { programFlags ^= COUNTDOWN; }
            if (request.Inputs["FORCE RERUN"].AsBoolean()) { programFlags ^= FORCERERUN; }
            if (request.Inputs["DISABLED"].AsBoolean()) { programFlags ^= DISABLED; }
            if (request.Inputs["ADMIN RIGHTS"].AsBoolean()) { programFlags ^= ADMINRIGHTS; }
            if (request.Inputs["USE CUSTOM PROGRESS MSG"].AsBoolean()) { programFlags ^= USECUSTOMPROGRESSMSG; }
            if (request.Inputs["NO USER LOGGED IN"].AsBoolean()) { programFlags ^= NOUSERLOGGEDIN; }
            if (request.Inputs["OK TO QUIT"].AsBoolean()) { programFlags ^= OKTOQUIT; }
            if (request.Inputs["OK TO REBOOT"].AsBoolean()) { programFlags ^= OKTOREBOOT; }
            if (request.Inputs["PERSIST CONNECTION"].AsBoolean()) { programFlags ^= PERSISTCONNECTION; }
            if (request.Inputs["RUN MINIMIZED"].AsBoolean()) { programFlags ^= RUNMINIMIZED; }
            if (request.Inputs["RUN MAXIMIZED"].AsBoolean()) { programFlags ^= RUNMAXIMIZED; }
            if (request.Inputs["HIDE WINDOW"].AsBoolean()) { programFlags ^= HIDEWINDOW; }
            if (request.Inputs["OK TO LOGOFF"].AsBoolean()) { programFlags ^= OKTOLOGOFF; }
            if (request.Inputs["RUN ACCOUNT"].AsBoolean()) { programFlags ^= RUNACCOUNT; }
            if (request.Inputs["ANY PLATFORM"].AsBoolean()) { programFlags ^= ANY_PLATFORM; }
            if (request.Inputs["STILL RUNNING"].AsBoolean()) { programFlags ^= STILL_RUNNING; }
            if (request.Inputs["SUPPORT UNINSTALL"].AsBoolean()) { programFlags ^= SUPPORT_UNINSTALL; }
            if (request.Inputs["Platform Not Supported"].AsBoolean()) { programFlags ^= Platform_Not_Supported; }
            if (request.Inputs["SHOW IN ARP"].AsBoolean()) { programFlags ^= SHOW_IN_ARP; }

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                String prgFlags = Convert.ToString((int)programFlags);

                CM2012Interop.modifySCCMProgram(connection, objID, prgName, "IntegerValue", "ProgramFlags", prgFlags);
                IResultObject col = CM2012Interop.getSCCMProgram(connection, "PackageID LIKE '" + objID + "' AND ProgramName LIKE '" + prgName + "'");
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

