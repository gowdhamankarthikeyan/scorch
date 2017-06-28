using System;

namespace DistributedFileSystem
{
    class ResourceStrings
    {
        /// <summary>
        /// Pointer to a string that specifies the Universal Naming Convention (UNC) path of a DFS link in a DFS namespace.
        /// 
        /// The string can be in one of two forms. The first form is as follows:
        /// 
        /// \\ServerName\DfsName\link_path
        /// 
        /// where ServerName is the name of the root target server that hosts a stand-alone DFS namespace; DfsName is the name of the DFS namespace; and link_path is a DFS link.
        /// 
        /// The second form is as follows:
        /// 
        /// \\DomainName\DomDfsname\link_path
        /// 
        /// where DomainName is the name of the domain that hosts a domain-based DFS namespace; DomDfsname is the name of the domain-based DFS namespace; and link_path is a DFS link.
        /// 
        /// </summary>
        public static string DfsEntryPath = "DFS Entry Path";
        public static string DefaultValueDfsEntryPath = "@\\\\ServerName\\DfsName\\link_path";
        public static string[] ValueListDfsEntryPath = new string[] { @"\\ServerName\DfsName\link_path", @"\\DomainName\DomDfsname\link_path" };
        /// <summary>
        /// Pointer to a string that specifies the name of the server that will host the new DFS root target. This value cannot be an IP address. This parameter is required.
        /// </summary>
        public static string ServerName = "Server Name";
        
        /// <summary>
        /// Pointer to a string that specifies the link target share name. This can also be a share name with a path relative to the share. For example, share1\mydir1\mydir2. This parameter is required.
        /// </summary>
        public static string PathName = "Path Name";
        public static string DefaultValuePathName = @"share1\mydir1\mydir2";
        
        /// <summary>
        /// Pointer to a string that specifies an optional comment associated with the DFS link. This parameter is ignored when the function adds a target to an existing link.
        /// </summary>
        public static string Comment = "Comment";
        
        public static string DFS_ADD_VOLUME = "DFS Add Volume";
        public static string DefaultValue_DFS_ADD_VOLUME = "True";
        public static string[] ValueList_DFS_ADD_VOLUME = new string[] { "True", "False" };
        public static uint Value_DFS_ADD_VOLUME = 0x00000001;
        public static uint Value_NO_DFS_ADD_VOLUME = 0x00000000;

        public static string NERR_Success = "NERR_Success";

        /// <summary>
        /// Pointer to a string that specifies the name of the shared folder on the server that will host the new DFS root target. This parameter is required.
        /// </summary>
        public static string RootShare = "Root Share";
        
        
    }
}
