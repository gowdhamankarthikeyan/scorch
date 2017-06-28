using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Text;

namespace Utilities.Filesystem
{
    [Activity("Read File to String",ShowFilters = false)]
    public class ReadFileToString : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("FileName").WithFileBrowser();
            designer.AddInput("FilePath").WithFolderBrowser();
            designer.AddOutput("File Contents").AsString();
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String fileName = request.Inputs["FileName"].AsString();
            String DirectoryLocation = request.Inputs["FilePath"].AsString();
            
            String fileFullPath = DirectoryLocation + "\\" + fileName;

            StreamReader SR = File.OpenText(fileFullPath);

            String ReturnString = SR.ReadToEnd();

            SR.Close();

            response.Publish("File Contents", ReturnString);
        }
    }
}

