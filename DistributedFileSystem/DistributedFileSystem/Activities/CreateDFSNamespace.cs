using System;
using System.Runtime.InteropServices;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace DistributedFileSystem.Activites
{
    [Activity("Create DFS Namespace", Description = "Creates a new domain-based Distributed File System (DFS) namespace. If the namespace already exists, the function adds the specified root target to it.")]
    public class CreateDFSNamespace : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(ResourceStrings.ServerName);
            designer.AddInput(ResourceStrings.RootShare);
            designer.AddInput(ResourceStrings.Comment).NotRequired();

            designer.AddOutput(ResourceStrings.NERR_Success);
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string comment = string.Empty;

            string ServerName = request.Inputs[ResourceStrings.ServerName].AsString();
            string RootShare = request.Inputs[ResourceStrings.RootShare].AsString();

            if (request.Inputs.Contains(ResourceStrings.Comment)) { comment = request.Inputs[ResourceStrings.Comment].AsString(); }

            int result = 0;
            result = NetDfsAddFtRoot(ServerName, RootShare, RootShare, comment, 0);

            response.Publish(ResourceStrings.NERR_Success, result);
        }

        [DllImport("Netapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int NetDfsAddFtRoot([MarshalAs(UnmanagedType.LPWStr)] string ServerName, [MarshalAs(UnmanagedType.LPWStr)] string RootShare, [MarshalAs(UnmanagedType.LPWStr)] string FtDfsName, [MarshalAs(UnmanagedType.LPWStr)] string Comment, uint Flags);
    }
}
