using System;
using System.Collections.Generic;
using Microsoft.SystemCenter.Orchestrator.Integration;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.Text;
using System.Net;
using System.IO;

namespace ExchangeMail
{
    [Activity("Get Attachments")]
    public class GetAttachment : IActivity
    {
        private MailboxSettings settings;
        private String exchangeVersion = String.Empty;
        private String emailID = String.Empty;
        private String SaveLocation = String.Empty;
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String serviceURL = String.Empty;

        [ActivityConfiguration]
        public MailboxSettings Settings
        {
            get { return settings; }
            set { settings = value; }
        }


        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Email ID");
            designer.AddInput("Save Location").WithFolderBrowser();
            designer.AddInput("Alternate Mailbox").NotRequired();
            designer.AddInput("Overwrite Existing Attachment").NotRequired().WithListBrowser(new string[2] { "True", "False" }).WithDefaultValue("True");
            designer.AddInput("Attachment Save Name").NotRequired().WithDefaultValue("myfile.csv");
            designer.AddOutput("Attachments Saved").AsNumber();
            designer.AddCorellatedData(typeof(SavedAttachment));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            userName = settings.UserName;
            password = settings.Password;
            domain = settings.Domain;
            serviceURL = settings.ServiceUrl;
            exchangeVersion = settings.ExchangeVersion;

            emailID = request.Inputs["Email ID"].AsString();
            SaveLocation = request.Inputs["Save Location"].AsString();

            string alternateMailbox = string.Empty;
            if (request.Inputs.Contains("Alternate Mailbox")) { alternateMailbox = request.Inputs["Alternate Mailbox"].AsString(); }

            bool overwriteExistingAttachment = true;
            if (request.Inputs.Contains("Overwrite Existing Attachment")) { overwriteExistingAttachment = Convert.ToBoolean(request.Inputs["Overwrite Existing Attachment"].AsString()); }

            string attachmentSaveName = string.Empty;
            if (request.Inputs.Contains("Attachment Save Name")) { attachmentSaveName = request.Inputs["Attachment Save Name"].AsString(); }
            

            ExchangeService service = new ExchangeService();
            switch (exchangeVersion)
            {
                case "Exchange2007_SP1":
                    service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
                    break;
                case "Exchange2010":
                    service = new ExchangeService(ExchangeVersion.Exchange2010);
                    break;
                case "Exchange2010_SP1":
                    service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                    break;
                default:
                    service = new ExchangeService();
                    break;
            }

            service.Credentials = new WebCredentials(userName, password, domain);
            String AccountUrl = userName + "@" + domain;

            if (serviceURL.Equals("Autodiscover")) { service.AutodiscoverUrl(AccountUrl, (a) => { return true; }); }
            else { service.Url = new Uri(serviceURL); }

            if (!alternateMailbox.Equals(String.Empty)) { service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, alternateMailbox); }

            EmailMessage message = EmailMessage.Bind(service, emailID);

            if (message.HasAttachments)
            {
                response.Publish("Attachments Saved", message.Attachments.Count);
                response.WithFiltering().PublishRange(getAttachments(message, overwriteExistingAttachment, attachmentSaveName));
            }
            else
            {
                response.Publish("Attachments Saved", 0);
            }
        }
        private IEnumerable<SavedAttachment> getAttachments(EmailMessage message, bool overwriteExistingAttachment, string attachmentSaveName)
        {
            foreach(Attachment attachment in message.Attachments)
            {
                if (attachment is FileAttachment)
                {
                    FileAttachment fileAttachment = attachment as FileAttachment;

                    // Load the file attachment into memory and print out its file name.
                    fileAttachment.Load();


                    if (overwriteExistingAttachment)
                    {
                        if (attachmentSaveName.Equals(string.Empty))
                        {
                            // Load attachment contents into a file.
                            fileAttachment.Load(SaveLocation + "\\" + fileAttachment.Name);
                            yield return new SavedAttachment(fileAttachment.Name.ToString(), SaveLocation);
                        }
                        else
                        {
                            // Load attachment contents into a file.
                            fileAttachment.Load(SaveLocation + "\\" + attachmentSaveName);
                            yield return new SavedAttachment(fileAttachment.Name.ToString(), SaveLocation);
                        }
                    }
                    else
                    {
                        if (attachmentSaveName.Equals(string.Empty))
                        {
                            string attachmentLocation = SaveLocation + "\\" + fileAttachment.Name;
                            attachmentLocation = saveUniqueName(fileAttachment, attachmentLocation, 1);
                            yield return new SavedAttachment(fileAttachment.Name.ToString(), attachmentLocation);
                        }
                        else
                        {
                            string attachmentLocation = SaveLocation + "\\" + attachmentSaveName;
                            attachmentLocation = saveUniqueName(fileAttachment, attachmentLocation, 1);
                            yield return new SavedAttachment(fileAttachment.Name.ToString(), attachmentLocation);
                        }
                    }
                }
                else 
                {
                    // Attachment is an item attachment.
                }
            }
        }

        private string saveUniqueName(FileAttachment fileAttachment, string attachmentFullName, int seed)
        {
            FileInfo destinationFile = new FileInfo(attachmentFullName);
            if (destinationFile.Exists)
            {
                string newName = destinationFile.FullName.Substring(0,destinationFile.FullName.Length - destinationFile.Extension.Length);

                if (seed > 1)
                {
                    int offsetSeed = seed - 1;
                    newName = newName.Substring(0, newName.Length - (3 + offsetSeed.ToString().Length));
                }
                newName += string.Format(" ({0}){1}", seed.ToString(), destinationFile.Extension);

                FileInfo newFile = new FileInfo(newName);
                if (newFile.Exists)
                {
                    int seedInc = seed + 1;
                    return saveUniqueName(fileAttachment, newName, seedInc);
                }
                else
                {
                    // Load attachment contents into a file.
                    fileAttachment.Load(newName);
                    return newName;
                }
            }
            else
            {
                // Load attachment contents into a file.
                fileAttachment.Load(attachmentFullName);
                return attachmentFullName;
            }
        }
    }
}

