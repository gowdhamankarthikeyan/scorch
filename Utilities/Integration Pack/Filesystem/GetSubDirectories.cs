using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Filesystem
{
    [ActivityData("Folder")]
    public class Folder
    {
        private DirectoryInfo directory;

        internal Folder(DirectoryInfo directory)
        {
            this.directory = directory;
        }

        [ActivityOutput, ActivityFilter]
        public String FolderPath
        {
            get { return directory.FullName.ToString(); }
        }

        [ActivityOutput,ActivityFilter]
        public DateTime CreatedDate
        {
            get { return directory.CreationTime; }
        }
    }

    [Activity("Get Sub Directories")]
    public class GetSubDirectories : IActivity
    {
        private DateTime createdAfter = DateTime.MinValue;

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Base Directory Location").WithFolderBrowser();
            designer.AddInput("Created After").NotRequired();
            designer.AddOutput("Base Directory").AsString();
            designer.AddCorellatedData(typeof(Folder));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String DirectoryLocation = request.Inputs["Base Directory Location"].AsString();
            createdAfter = Convert.ToDateTime(request.Inputs["Created After"].AsString());
            response.WithFiltering().PublishRange(getFolders(DirectoryLocation));
            response.Publish("Base Directory", DirectoryLocation);
        }

        public IEnumerable<Folder> getFolders(String DirectoryLocation)
        {
            DirectoryInfo dir = new DirectoryInfo(DirectoryLocation);

            foreach(DirectoryInfo directory in dir.GetDirectories())
            {
                if (directory.CreationTime.CompareTo(createdAfter) >= 0)
                {
                    yield return new Folder(directory);
                }
            }
        }
    }
}

