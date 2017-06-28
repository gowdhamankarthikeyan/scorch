using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;
using System.IO.Compression;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Zip
{
    [Activity("Decompress Files")]
    class DecompressFiles : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            string[] overwriteOptions = { "Overwrite", "Do not overwrite" };
            designer.AddInput("Origin Zip");
            designer.AddInput("Destination Location");
            designer.AddInput("If the Destination Files Exist").WithListBrowser(overwriteOptions).WithDefaultValue("Overwrite");

            designer.AddOutput("Destination Location");
            designer.AddOutput("Number of files decompressed");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string destinationFile = request.Inputs["Destination Location"].AsString();
            string originZip = request.Inputs["Origin Zip"].AsString();
            string destinationExistsChoice = request.Inputs["If the Destination Files Exist"].AsString();

            int numberOfFiles = 0;
            bool warn = false;
            String warnMessage = String.Empty;

            DirectoryInfo DestinationFolder = new DirectoryInfo(destinationFile);
            FileInfo zipFile = new FileInfo(originZip);

            if (zipFile.Exists)
            {
                if (!DestinationFolder.Exists) { DestinationFolder.Create(); }

                using (ZipFile zip1 = ZipFile.Read(zipFile.FullName.ToString()))
                {
                    foreach (ZipEntry e in zip1)
                    {
                        numberOfFiles = numberOfFiles + 1;
                        try
                        {
                            switch (destinationExistsChoice)
                            {
                                case "Overwrite":
                                    e.Extract(DestinationFolder.ToString(), ExtractExistingFileAction.OverwriteSilently);
                                    break;
                                case "Do not overwrite":
                                    e.Extract(DestinationFolder.ToString(), ExtractExistingFileAction.DoNotOverwrite);
                                    break;
                                default:
                                    e.Extract(DestinationFolder.ToString(), ExtractExistingFileAction.OverwriteSilently);
                                    break;
                            }
                        }
                        catch (Exception error) { warn = true; warnMessage += error.Message + "\n"; }
                    }
                }
            }
            else
            {
                response.LogErrorMessage("Cannot find Zip file " + request.Inputs["Origin Zip"].AsString());
            }

            response.Publish("Destination Location", DestinationFolder.FullName.ToString());
            response.Publish("Number of files decompressed", numberOfFiles.ToString());

            if (warn) { response.LogWarningMessage(warnMessage); }
        }
        private void DecompressZip(FileInfo fi)
        {
            string unpackDirectory = fi.Directory.ToString() + "\\SourceFiles";
            using (ZipFile zip1 = ZipFile.Read(fi.FullName.ToString()))
            {
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }

    
}
