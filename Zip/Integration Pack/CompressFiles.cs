using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Ionic.Zip;
using System.IO;

namespace Zip
{
    [Activity("Compress Files")]
    public class CompressFiles : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            string[] archiveOptions = { "Add Files to Existing Archive", "Overwrite Existing Archive", "Fail if the Archive Exists", "Create a unique named Archive" };
            string[] compressOptions = { "None", "Low", "Medium", "High" };
            designer.AddInput("Origin Folder");
            designer.AddInput("Destination File");
            designer.AddInput("If the Destination Archive already exists").WithListBrowser(archiveOptions).WithDefaultValue("Add Files to Existing Archive");
            designer.AddInput("Compression Level").WithListBrowser(compressOptions).WithDefaultValue("Medium");

            designer.AddOutput("Archive Name and Path").WithDescription("c:\\temp\\test.zip");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string originFolder = request.Inputs["Origin Folder"].AsString();
            string destinationFile = request.Inputs["Destination File"].AsString();
            string destinationExistsChoice = request.Inputs["If the Destination Archive already exists"].AsString();
            string compressionLevel = request.Inputs["Compression Level"].AsString();

            FileInfo destFile = new FileInfo(destinationFile);

            if (destFile.Exists)
            {
                switch (destinationExistsChoice)
                {
                    case "Add Files to Existing Archive":
                        using (ZipFile zipFile = new ZipFile(destFile.FullName))
                        {
                            DirectoryInfo origFolder = new DirectoryInfo(originFolder);
                            zipFile.AddItem(origFolder.FullName.ToString());
                            int count = zipFile.Count;
                            switch (compressionLevel)
                            {
                                case "None":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                                    break;
                                case "Low":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Level1;
                                    break;
                                case "Medium":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                                    break;
                                case "High":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                                    break;
                                default:
                                    break;
                            }

                            zipFile.Save();
                            response.Publish("Archive Name and Path", destinationFile);
                        }
                        break;
                    default:
                    case "Overwrite Existing Archive":
                        destFile.Delete();
                        using (ZipFile zipFile = new ZipFile())
                        {
                            DirectoryInfo origFolder = new DirectoryInfo(originFolder);
                            zipFile.AddItem(origFolder.FullName.ToString());

                            switch (compressionLevel)
                            {
                                case "None":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                                    break;
                                case "Low":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Level1;
                                    break;
                                case "Medium":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                                    break;
                                case "High":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                                    break;
                            }

                            zipFile.Save();
                            response.Publish("Archive Name and Path", destinationFile);
                        }
                        break;
                    case "Fail if the Archive Exists":
                        response.LogErrorMessage("File Archive already Exists");
                        throw new Exception("File Archive already Exists");
                    case "Create a unique named Archive":
                        destinationFile = destinationFile + DateTime.Now.ToString();
                        using (ZipFile zipFile = new ZipFile(destinationFile))
                        {
                            DirectoryInfo origFolder = new DirectoryInfo(originFolder);
                            zipFile.AddItem(origFolder.FullName.ToString());

                            switch (compressionLevel)
                            {
                                case "None":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                                    break;
                                case "Low":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Level1;
                                    break;
                                case "Medium":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                                    break;
                                case "High":
                                    zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                                    break;
                                default:
                                    break;
                            }

                            zipFile.Save();
                            response.Publish("Archive Name and Path", destinationFile);
                        }
                        break;
                }
            }
            else
            {
                using (ZipFile zipFile = new ZipFile(destFile.FullName))
                {
                    DirectoryInfo origFolder = new DirectoryInfo(originFolder);
                    zipFile.AddItem(origFolder.FullName.ToString());

                    switch (compressionLevel)
                    {
                        case "None":
                            zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                            break;
                        case "Low":
                            zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Level1;
                            break;
                        case "Medium":
                            zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                            break;
                        case "High":
                            zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                            break;
                        default:
                            break;
                    }

                    zipFile.Save();
                    response.Publish("Archive Name and Path", destinationFile);
                }
            }
        }
    }
}
