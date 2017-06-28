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
    [Activity("Download File")]
    public class DownloadFiles : IActivity
    {
        private ConnectionCredentials settings;

        [ActivityConfiguration]
        public ConnectionCredentials Settings
        {
            get { return settings; }
            set { settings = value; }
        }
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("FTP File Path").WithDefaultValue("ftp://contoso.com/someFile.txt");
            designer.AddInput("UseSSL").WithDefaultValue("False").WithListBrowser(new string[] { "True", "False" });
            designer.AddInput("File Save Path").WithFileBrowser().WithDefaultValue(@"\\myfileserver\fileShare\Filename");

            designer.AddOutput("Downloaded File Path");
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String sourcePath = request.Inputs["FTP File Path"].AsString();
            String savePath = request.Inputs["File Save Path"].AsString();
            bool useSSL = Convert.ToBoolean(request.Inputs["UseSSL"].AsString());

            // Get the object used to communicate with the server.
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(sourcePath);
            ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            
            ftpWebRequest.EnableSsl = useSSL;
            ftpWebRequest.Credentials = new NetworkCredential(settings.UserName, settings.Password);

            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();

            Stream responseStream = ftpWebResponse.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            StreamWriter writer = new StreamWriter(savePath);
            writer.Write(reader.ReadToEnd());
            writer.Close();
            reader.Close();
            ftpWebResponse.Close();  


            response.Publish("Downloaded File Path", savePath);
        }
    }
}
