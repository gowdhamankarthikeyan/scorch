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
    [Activity("Create SCCM Updates Assignment")]
    public class CreateUpdatesAssignment : IActivity
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
            designer.AddInput("Assignment Name").WithDefaultValue("Assignment NAME");
            designer.AddInput("Apply To Sub Targets").WithDefaultValue(true).WithBooleanBrowser();
            designer.AddInput("Assigned CIs (CSV)").WithDefaultValue("1,2,3");
            designer.AddInput("Assignment Description").WithDefaultValue("Description");
            designer.AddInput("Assignment Action").WithDefaultValue(2);
            designer.AddInput("Desired Config Type").WithDefaultValue(1).WithListBrowser(new int[] { 0,1 });
            designer.AddInput("DP Locality").WithDefaultValue(80);
            designer.AddInput("Locale ID").WithDefaultValue(1033);
            designer.AddInput("Log Compliace To Windows Event Log").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("Notify User").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("Raise MOM Alert of Failure").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("Read Only").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("Send Detailed Non Compliance Status").WithDefaultValue(false).WithBooleanBrowser();
            designer.AddInput("Start Time").WithDefaultValue("5/5/2005 5:05PM");
            designer.AddInput("Suppress Reboot").WithDefaultValue(1).WithListBrowser(new int[] {0,1});
            designer.AddInput("Target Collection ID").WithDefaultValue("AAA00000");
            designer.AddInput("Use GMT Times").WithDefaultValue(true).WithBooleanBrowser();

            designer.AddCorellatedData(typeof(updatesAssignment));
            designer.AddOutput("Number of Updates Assignments");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            SCCMServer = settings.SCCMSERVER;
            userName = settings.UserName;
            password = settings.Password;

            String assignmentName = request.Inputs["Assignment Name"].AsString();
            bool applyToSubTargets = request.Inputs["Apply To Sub Targets"].AsBoolean();
            String assignedCIs = request.Inputs["Assigned CIs (CSV)"].AsString();
            String description = request.Inputs["Assignment Description"].AsString();
            int action = (int)request.Inputs["Assignment Action"].AsUInt32();
            int desiredConfigType = (int)request.Inputs["Desired Config Type"].AsUInt32();
            int dpLocality = (int)request.Inputs["DP Locality"].AsUInt32();
            int localeID = (int)request.Inputs["Locale ID"].AsUInt32();
            bool logComplianceToWinEvent = request.Inputs["Log Compliace To Windows Event Log"].AsBoolean();
            bool notifyUser = request.Inputs["Notify User"].AsBoolean();
            bool raiseMOMAlertOnFailure = request.Inputs["Raise MOM Alert of Failure"].AsBoolean();
            bool readOnly = request.Inputs["Read Only"].AsBoolean();
            bool sendDetailedNonComplianceStatus = request.Inputs["Send Detailed Non Compliance Status"].AsBoolean();
            DateTime startTime = Convert.ToDateTime(request.Inputs["Start Time"].AsString());
            int suppressReboot = (int)request.Inputs["Suppress Reboot"].AsUInt32();
            String targetCollectionID = request.Inputs["Target Collection ID"].AsString();
            bool useGMTTimes = request.Inputs["Use GMT Times"].AsBoolean();

            //Setup WQL Connection and WMI Management Scope
            WqlConnectionManager connection = CM2012Interop.connectSCCMServer(SCCMServer, userName, password);
            using(connection)
            {  
                IResultObject col = CM2012Interop.createSCCMUpdatesAssignment(connection, applyToSubTargets, assignedCIs, action, description, assignmentName, desiredConfigType, dpLocality, localeID, logComplianceToWinEvent, notifyUser, raiseMOMAlertOnFailure, readOnly, sendDetailedNonComplianceStatus, startTime, suppressReboot, targetCollectionID, useGMTTimes);

                if (col != null)
                {
                    response.WithFiltering().PublishRange(getObjects(col));

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

