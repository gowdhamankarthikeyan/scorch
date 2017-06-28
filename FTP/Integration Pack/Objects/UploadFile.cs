using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Net;
using System.Threading;
using System.IO;

namespace SCORCHDev_FTP.Objects
{
    [Activity("Upload File")]
    public class UploadFile : IActivity
    {
        private ConnectionCredentials settings;

        private int ObjCount = 0;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Source File Path").WithFolderBrowser();
            designer.AddInput("Destination File Path").WithDefaultValue("ftp://contoso.com/someFile.txt");
            designer.AddInput("UseSSL").WithDefaultValue("False").WithListBrowser(new string[] { "True", "False" });
            designer.AddOutput("Status");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String sourcePath = request.Inputs["Source File Path"].AsString();
            String savePath = request.Inputs["Destination File Path"].AsString();
            bool useSSL = Convert.ToBoolean(request.Inputs["UseSSL"].AsString());

            // Get the object used to communicate with the server.
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(savePath);
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpWebRequest.EnableSsl = useSSL;
            // This example assumes the FTP site uses anonymous logon.
            ftpWebRequest.Credentials = new NetworkCredential(settings.UserName, settings.Password);


            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(sourcePath);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            ftpWebRequest.ContentLength = fileContents.Length;

            Stream requestStream = ftpWebRequest.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
            ftpWebResponse.Close();
            
            response.Publish("Status",ftpWebResponse.StatusDescription);
        }
    }
}
