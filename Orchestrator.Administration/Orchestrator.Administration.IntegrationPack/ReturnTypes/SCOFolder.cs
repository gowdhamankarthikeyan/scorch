using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Xml;

namespace Orchestrator.Administration.IntegrationPack
{
    [ActivityData("Orchestrator Folder")]
    public class SCOFolder
    {
        public SCOFolder(XmlNode folderNode, string basePath)
        {
            this.UniqueID = string.Empty;
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.FullPath = string.Empty;

            this.UniqueID = folderNode.SelectSingleNode("UniqueID").InnerText;
            this.Name = folderNode.SelectSingleNode("Name").InnerText;
            this.Description = folderNode.SelectSingleNode("Description").InnerText;
            this.FullPath = basePath + "\\" + this.Name;
        }

        [ActivityOutput, ActivityFilter]
        public String UniqueID
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
        public String Description
        {
            get;
            set;
        }
        [ActivityOutput, ActivityFilter]
        public String FullPath
        {
            get;
            set;
        }

    }
}
