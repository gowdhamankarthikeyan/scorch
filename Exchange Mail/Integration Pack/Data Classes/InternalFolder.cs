using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;

namespace ExchangeMail
{
    [ActivityData("Folder")]
    public class InternalFolder
    {
        private String FolderName = String.Empty;
        private String FolderID = String.Empty;
        private String ParentFolderId = String.Empty;

        internal InternalFolder(String FolderName, String FolderID, String ParentFolderId)
        {
            this.FolderName = FolderName;
            this.FolderID = FolderID;
            this.ParentFolderId = ParentFolderId;
        }

        [ActivityOutput, ActivityFilter]
        public String folderName
        {
            get { return FolderName; }
        }

        [ActivityOutput, ActivityFilter]
        public String folderID
        {
            get { return FolderID; }
        }

        [ActivityOutput, ActivityFilter]
        public String parentFolderId
        {
            get { return ParentFolderId; }
        }
    }
}

