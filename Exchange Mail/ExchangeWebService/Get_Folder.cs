using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.IO;

namespace ExchangeWebService
{
    [Cmdlet(VerbsCommon.Get, "EWSFolder")]
    public class Get_Folder : PSCmdlet
    {
        #region Parameters
        private ExchangeService _exchangeService;
        [Parameter(
           Position = 0,
           Mandatory = true,
           ValueFromPipeline = true
        )]
        public ExchangeService mailboxConnection
        {
            get { return _exchangeService; }
            set { _exchangeService = value; }
        }

        [Parameter(
           Position = 1,
           Mandatory = false
        )]
        public string FolderName
        {
            get { return _folderName; }
            set { _folderName = value; }
        }
        private string _folderName = "Inbox";
        #endregion
        protected override void ProcessRecord()
        {
            FindFoldersResults results;
            if (!FolderName.ToLower().Equals("inbox"))
            {
                SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, FolderName);
                FolderView view = new FolderView(int.MaxValue);
                view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                view.PropertySet.Add(FolderSchema.DisplayName);
                view.Traversal = FolderTraversal.Deep;
                results = mailboxConnection.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, view);
            }
            else
            {
                FolderId folderID = new FolderId(WellKnownFolderName.Inbox);
                SearchFilter folderFilter = new SearchFilter.IsEqualTo(FolderSchema.Id, folderID);
                FolderView view = new FolderView(int.MaxValue);
                view.PropertySet = new PropertySet(BasePropertySet.IdOnly);
                view.PropertySet.Add(FolderSchema.DisplayName);
                view.Traversal = FolderTraversal.Deep;
                results = mailboxConnection.FindFolders(WellKnownFolderName.MsgFolderRoot, folderFilter, view);
            }
            foreach (Folder folder in results)
            {
                WriteObject(folder);
            }
        }
    }
}
