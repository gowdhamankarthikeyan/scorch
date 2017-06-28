using System;
using System.Runtime.InteropServices;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace DistributedFileSystem.Activites
{
    [Activity("Create DFS Link", Description="Creates a new Distributed File System (DFS) link or adds targets to an existing link in a DFS namespace")]
    public class CreateDFSLink : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(ResourceStrings.DfsEntryPath).WithDefaultValue(ResourceStrings.DefaultValueDfsEntryPath).WithListBrowser(ResourceStrings.ValueListDfsEntryPath);
            designer.AddInput(ResourceStrings.ServerName);
            designer.AddInput(ResourceStrings.PathName).WithDefaultValue(ResourceStrings.DefaultValuePathName);
            designer.AddInput(ResourceStrings.Comment).NotRequired();
            designer.AddInput(ResourceStrings.DFS_ADD_VOLUME).WithDefaultValue(ResourceStrings.DefaultValue_DFS_ADD_VOLUME).WithListBrowser(ResourceStrings.ValueList_DFS_ADD_VOLUME);

            designer.AddOutput(ResourceStrings.DfsEntryPath);
            designer.AddOutput(ResourceStrings.NERR_Success);
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string comment = string.Empty;

            string DfsEntryPath = request.Inputs[ResourceStrings.DfsEntryPath].AsString();
            string ServerName = request.Inputs[ResourceStrings.ServerName].AsString();
            string PathName = request.Inputs[ResourceStrings.DfsEntryPath].AsString();            

            bool DFS_ADD_VOLUME = Convert.ToBoolean(request.Inputs[ResourceStrings.DFS_ADD_VOLUME].AsString());

            if (request.Inputs.Contains(ResourceStrings.Comment)) { comment = request.Inputs[ResourceStrings.Comment].AsString(); }

            int result = 0;
            if (DFS_ADD_VOLUME) { result = NetDfsAdd(DfsEntryPath, ServerName, PathName, comment, ResourceStrings.Value_DFS_ADD_VOLUME); }
            else { result = NetDfsAdd(DfsEntryPath, ServerName, PathName, comment, ResourceStrings.Value_NO_DFS_ADD_VOLUME); }

            response.Publish(ResourceStrings.NERR_Success, result);
            response.Publish(ResourceStrings.DfsEntryPath, DfsEntryPath);
        }

        [DllImport("Netapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int NetDfsAdd([MarshalAs(UnmanagedType.LPWStr)] string DfsEntryPath, [MarshalAs(UnmanagedType.LPWStr)] string ServerName, [MarshalAs(UnmanagedType.LPWStr)] string PathName, [MarshalAs(UnmanagedType.LPWStr)] string Comment, uint Flags);
    }
}
