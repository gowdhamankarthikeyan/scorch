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
    [Activity("Get Zip File Item Count")]
    class GetZipFileItemCount : IActivity
    {
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Target Zip");
            
            designer.AddOutput("Number of Files");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string targetZip = request.Inputs["Target Zip"].AsString();

            FileInfo zipFile = new FileInfo(targetZip);

            if (zipFile.Exists)
            {
                using (ZipFile zip1 = ZipFile.Read(zipFile.FullName.ToString()))
                {
                    response.Publish("Number of Files", zip1.Count());
                }
            }
            else
            {
                throw new Exception(String.Format("Zip File at location {0} not found", targetZip));
            }
        }
    }


}
