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
    [Activity("List Directory Contents")]
    public class ListDirectoryContents : IActivity
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
            designer.AddInput("Folder Path").WithDefaultValue("ftp://contoso.com/");
            designer.AddInput("UseSSL").WithDefaultValue("False").WithListBrowser(new string[] { "True", "False" });
            designer.AddOutput("File Details");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String sourcePath = request.Inputs["Source File Path"].AsString();
            String savePath = request.Inputs["Destination File Path"].AsString();
            bool useSSL = Convert.ToBoolean(request.Inputs["UseSSL"].AsString());

            // Get the object used to communicate with the server.
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(savePath);
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            ftpWebRequest.EnableSsl = useSSL;
            // This example assumes the FTP site uses anonymous logon.
            ftpWebRequest.Credentials = new NetworkCredential(settings.UserName, settings.Password);

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responseStream = ftpWebResponse.GetResponseStream();
            
            using (StreamReader sr = new StreamReader(responseStream)) 
            {
                while (sr.Peek() >= 0) 
                {
                    response.Publish("File Details", sr.ReadLine());
                }
            }

        }
    }
}
