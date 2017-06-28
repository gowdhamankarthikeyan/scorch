using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Net;

namespace SCORCHDev_FTP.Objects
{
    [Activity("Delete File")]
    public class DeleteFile : IActivity
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
            designer.AddInput("FTP File Path").WithDefaultValue("ftp://contoso.com/someFile.txt");
            designer.AddInput("UseSSL").WithDefaultValue("False").WithListBrowser(new string[] { "True", "False" });
        }
        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            String deletePath = request.Inputs["FTP File Path"].AsString();
            bool useSSL = Convert.ToBoolean(request.Inputs["UseSSL"].AsString());

            DeleteFileOnServer(new Uri(deletePath), useSSL);
        }
        /// <summary>
        /// Deletes a file on an FTP Server
        /// </summary>
        /// <param name="serverUri"> 
        /// The serverUri parameter should use the ftp:// scheme.
        /// It contains the name of the server file that is to be deleted.
        /// Example: ftp://contoso.com/someFile.txt.
        /// </param>
        /// <param name="useSSL">Use SSL Connection or Not</param>
        /// <returns></returns>
        public static bool DeleteFileOnServer(Uri serverUri, bool useSSL)
        {
            // The serverUri parameter should use the ftp:// scheme.
            // It contains the name of the server file that is to be deleted.
            // Example: ftp://contoso.com/someFile.txt.
            // 

            if (serverUri.Scheme != Uri.UriSchemeFtp)
            {
                return false;
            }
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
            return true;
        }
    }
}
