using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration;
using Microsoft.SystemCenter.Orchestrator.Integration.Administration.OIS_Export;

namespace Orchestrator.Administration.IntegrationPack
{
    [Activity("Export Runbook Folder")]
    public class ExportRunbookFolder : IActivity
    {
        [ActivityConfiguration]
        public COMInterfaceConnectionCredentials Credentials
        {
            get;
            set;
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(ResourceStrings.runbookPath).WithDefaultValue(ResourceStrings.runbookPathDefaultValue);
            designer.AddInput(ResourceStrings.savePath).WithDefaultValue(ResourceStrings.savePathDefaultValue).WithFolderBrowser();

            designer.AddInput(ResourceStrings.loadExportData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.overwriteExistingExport).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();

            designer.AddInput(ResourceStrings.loadGlobalComputerGroupData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.loadGlobalConfigurationData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.loadGlobalCounter).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.loadGlobalScheduleData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.loadGlobalVariableData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();

            designer.AddInput(ResourceStrings.cleanGlobalComputerGroupData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.cleanGlobalConfigurationData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.cleanGlobalCounter).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.cleanGlobalScheduleData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.cleanGlobalVariableData).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.logCommonData).WithListBrowser(ResourceStrings.trueFalseDoNotModify).WithDefaultValue(ResourceStrings.doNotModify).NotRequired();
            designer.AddInput(ResourceStrings.logSpecificData).WithListBrowser(ResourceStrings.trueFalseDoNotModify).WithDefaultValue(ResourceStrings.doNotModify).NotRequired();

            designer.AddInput(ResourceStrings.applyLinkBestPractices).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();
            designer.AddInput(ResourceStrings.updateMaxParallelRequests).WithListBrowser(ResourceStrings.trueFalse).WithDefaultValue(ResourceStrings.t).NotRequired();

            designer.AddOutput(ResourceStrings.exportFilePath);
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string username = Credentials.UserDomain + "\\" + Credentials.UserName;

            COMInterop scorchInterop = new COMInterop(username, Credentials.Password);
            ExportFile RunbookExport = new ExportFile();

            string runbookPath = String.Empty;
            string savePath = String.Empty;

            if (request.Inputs.Contains(ResourceStrings.runbookPath)) { runbookPath = request.Inputs[ResourceStrings.runbookPath].AsString(); }
            if (!runbookPath.ToLower().StartsWith(ResourceStrings.policies.ToLower())) { runbookPath = ResourceStrings.policies + runbookPath; } 
            if (request.Inputs.Contains(ResourceStrings.savePath)) { savePath = request.Inputs[ResourceStrings.savePath].AsString(); }

            bool loadExportData = true;
            bool overwriteExistingExport = true;

            if (request.Inputs.Contains(ResourceStrings.loadExportData)) { loadExportData = Convert.ToBoolean(request.Inputs[ResourceStrings.loadExportData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.overwriteExistingExport)) { overwriteExistingExport = Convert.ToBoolean(request.Inputs[ResourceStrings.overwriteExistingExport].AsString()); }

            bool loadGlobalComputerGroupData = true;
            bool loadGlobalConfigurationData = true;
            bool loadGlobalCounter = true;
            bool loadGlobalScheduleData = true;
            bool loadGlobalVariableData = true;
            bool applyLinkBestPractices = false;
            bool updateMaxParallelRequests = false;

            if (request.Inputs.Contains(ResourceStrings.loadGlobalComputerGroupData)) { loadGlobalComputerGroupData = Convert.ToBoolean(request.Inputs[ResourceStrings.loadGlobalComputerGroupData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.loadGlobalConfigurationData)) { loadGlobalConfigurationData = Convert.ToBoolean(request.Inputs[ResourceStrings.loadGlobalConfigurationData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.loadGlobalCounter)) { loadGlobalCounter = Convert.ToBoolean(request.Inputs[ResourceStrings.loadGlobalCounter].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.loadGlobalScheduleData)) { loadGlobalScheduleData = Convert.ToBoolean(request.Inputs[ResourceStrings.loadGlobalScheduleData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.loadGlobalVariableData)) { loadGlobalVariableData = Convert.ToBoolean(request.Inputs[ResourceStrings.loadGlobalVariableData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.applyLinkBestPractices)) { applyLinkBestPractices = Convert.ToBoolean(request.Inputs[ResourceStrings.applyLinkBestPractices].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.updateMaxParallelRequests)) { updateMaxParallelRequests = Convert.ToBoolean(request.Inputs[ResourceStrings.updateMaxParallelRequests].AsString()); }

            bool cleanGlobalComputerGroupData = true;
            bool cleanGlobalConfigurationData = true;
            bool cleanGlobalCounter = true;
            bool cleanGlobalScheduleData = true;
            bool cleanGlobalVariableData = true;
            
            if (request.Inputs.Contains(ResourceStrings.cleanGlobalComputerGroupData)) { cleanGlobalComputerGroupData = Convert.ToBoolean(request.Inputs[ResourceStrings.cleanGlobalComputerGroupData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.cleanGlobalConfigurationData)) { cleanGlobalConfigurationData = Convert.ToBoolean(request.Inputs[ResourceStrings.cleanGlobalConfigurationData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.cleanGlobalCounter)) { cleanGlobalCounter = Convert.ToBoolean(request.Inputs[ResourceStrings.cleanGlobalCounter].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.cleanGlobalScheduleData)) { cleanGlobalScheduleData = Convert.ToBoolean(request.Inputs[ResourceStrings.cleanGlobalScheduleData].AsString()); }
            if (request.Inputs.Contains(ResourceStrings.cleanGlobalVariableData)) { cleanGlobalVariableData = Convert.ToBoolean(request.Inputs[ResourceStrings.cleanGlobalVariableData].AsString()); }

            if (loadExportData) { RunbookExport.LoadExportFromFolder(runbookPath, scorchInterop); }

            if (loadGlobalComputerGroupData) { try { RunbookExport.LoadComputerGroups(scorchInterop); } catch { /* No Computer Groups defined in environment */ } }
            if (loadGlobalConfigurationData) { try { RunbookExport.LoadConfigurations(scorchInterop); } catch { /* No Global Configurations defined in environment */ } }
            if (loadGlobalCounter) { try { RunbookExport.LoadCounters(scorchInterop); } catch { /* No Counters defined in environment */ } }
            if (loadGlobalScheduleData) { try { RunbookExport.LoadSchedules(scorchInterop);} catch { /* No Schedules defined in environment */ } }
            if (loadGlobalVariableData) { try { RunbookExport.LoadVariables(scorchInterop); } catch { /* No Variables defined in environment */ } }

            if (cleanGlobalComputerGroupData) { RunbookExport.cleanGlobalComputerGroupsNode(); }
            if (cleanGlobalConfigurationData) { RunbookExport.cleanGlobalConfigurations(); }
            if (cleanGlobalCounter) { RunbookExport.cleanGlobalCountersNode(); }
            if (cleanGlobalScheduleData) { RunbookExport.cleanGlobalSchedulesNode(); }
            if (cleanGlobalVariableData) { RunbookExport.cleanGlobalVariablesNode(); }
            if (applyLinkBestPractices) { RunbookExport.modifyExportLinkApplyBestPractices(); }
            if (updateMaxParallelRequests) { RunbookExport.modifyExportSetMaxParallelRequestSettingNameBased(); }

            string logObjectSpecific = ResourceStrings.doNotModify;
            switch (logObjectSpecific)
            {
                case ResourceStrings.doNotModify:
                    break;
                case ResourceStrings.t:
                    RunbookExport.modifyObjectSpecificLogging(ResourceStrings.on);
                    break;
                case ResourceStrings.f:
                    RunbookExport.modifyObjectSpecificLogging(ResourceStrings.off);
                    break;
                default:
                    break;
            }
            
            string logCommonData = ResourceStrings.doNotModify;
            switch (logCommonData)
            {
                case ResourceStrings.doNotModify:
                    break;
                case ResourceStrings.t:
                    RunbookExport.modifyGenericLogging(ResourceStrings.on);
                    break;
                case ResourceStrings.f:
                    RunbookExport.modifyGenericLogging(ResourceStrings.off);
                    break;
                default:
                    break;
            }
            DirectoryInfo di = new DirectoryInfo(savePath.Substring(0, savePath.LastIndexOf('\\')));
            if (!di.Exists) { di.Create(); }

            if (overwriteExistingExport) { RunbookExport.OISExport.Save(savePath); response.Publish(ResourceStrings.exportFilePath, savePath); }
            else
            {
                if (System.IO.File.Exists(savePath)) { throw new Exception("Export File already exists, run with Overwrite Existing Export True to overwrite"); }
                else { RunbookExport.OISExport.Save(savePath); response.Publish(ResourceStrings.exportFilePath, savePath); }
            }
        }
    }
}

