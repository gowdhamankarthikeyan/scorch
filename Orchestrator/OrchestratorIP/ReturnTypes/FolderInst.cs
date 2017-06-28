using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using OrchestratorInterop;
using OrchestratorInterop.Data_Class;


namespace OrchestratorIP.ReturnTypes
{
    [ActivityData("Folder Instance")]
    public class FolderInst
    {
        internal FolderInst(OrchestratorInterop.SCOrchestrator.Folder folderInst)
        {
            try
            {
                if (folderInst != null)
                {
                    try { CreatedBy = folderInst.CreatedBy; }
                    catch { CreatedBy = string.Empty; }
                    try { CreationTime = folderInst.CreationTime; }
                    catch { CreationTime = DateTime.MinValue; }
                    try { ParentId = folderInst.ParentId.ToString(); }
                    catch { ParentId = string.Empty; }
                    try { Id = folderInst.Id.ToString(); }
                    catch { Id = string.Empty; }
                    try { LastModifiedBy = folderInst.LastModifiedBy; }
                    catch { LastModifiedBy = string.Empty; }
                    try { LastModifiedTime = folderInst.LastModifiedTime; }
                    catch { LastModifiedTime = DateTime.MinValue; }
                    try { Name = folderInst.Name; }
                    catch { Name = String.Empty; }
                    try { Path = folderInst.Path; }
                    catch { Path = string.Empty; }
                }
            }
            catch { }
        }

        [ActivityOutput, ActivityFilter]
        public String CreatedBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime CreationTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String ParentId
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Id
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String LastModifiedBy
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public DateTime LastModifiedTime
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Name
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String Path
        {
            get;
            set;
        }
    }
}