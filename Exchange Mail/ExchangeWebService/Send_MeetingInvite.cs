using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Exchange.WebServices.Autodiscover;
using System.IO;

namespace ExchangeWebService
{
    [Cmdlet(VerbsCommunications.Send, "EWSMeetingInvite", DefaultParameterSetName = "NewEmail")]
    public class Send_MeetingInvite : PSCmdlet
    {
        #region Parameters
        private ExchangeService _exchangeService;
        [Parameter(
           Position = 0,
           Mandatory = true,
           ValueFromPipeline = true,
           ParameterSetName = "NewEmail"
        )]
        public ExchangeService mailboxConnection
        {
            get { return _exchangeService; }
            set { _exchangeService = value; }
        }

        [Parameter(
           Position = 1,
           Mandatory = true,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 2,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public string[] Recipients
        {
            get;
            set;
        }

        [Parameter(
           Position = 2,
           Mandatory = true,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 3,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public string Subject
        {
            get;
            set;
        }

        [Parameter(
           Position = 3,
           Mandatory = true,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 4,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public string Body
        {
            get;
            set;
        }

        private Importance _importanceLevel = Importance.Normal;
        [Parameter(
           Position = 4,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 5,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public Importance ImportanceLevel
        {
            get { return _importanceLevel; }
            set { _importanceLevel = value; }
        }

        [Parameter(
           Position = 5,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 6,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public string[] CCRecipients
        {
            get;
            set;
        }

        [Parameter(
           Position = 6,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 7,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public string[] BCCRecipients
        {
            get;
            set;
        }

        [Parameter(
           Position = 7,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 8,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public SwitchParameter RequestReadResponse
        {
            get;
            set;
        }

        [Parameter(
           Position = 8,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        public SwitchParameter RequestResponse
        {
            get;
            set;
        }

        [Parameter(
           Position = 9,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 9,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public SwitchParameter RequestDeliveryReceipt
        {
            get;
            set;
        }
        private BodyType _bodyFormat = BodyType.HTML;
        [Parameter(
           Position = 10,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        [Parameter(
            Position = 10,
            Mandatory = false,
            ParameterSetName = "existingEmail"
            )
        ]
        public BodyType bodyFormat
        {
            get { return _bodyFormat; }
            set { _bodyFormat = value; }
        }

        private FileInfo[] _attachments = null;
        [Parameter(
           Position = 11,
           Mandatory = false,
           ParameterSetName = "NewEmail"
        )]
        public FileInfo[] attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipeline = true,
            ParameterSetName = "existingEmail"
            )
        ]
        public EmailMessage emailMessage
        {
            get;
            set;
        }

        public enum emailMessageSendTypeOptions
        {
            reply,
            replyAll,
            forward,
            send
        }
        [Parameter(
            Position = 1,
            Mandatory = true,
            ParameterSetName = "existingEmail"
            )
        ]
        public emailMessageSendTypeOptions emailMessageSendType
        {
            get;
            set;
        }

        #endregion

        protected override void ProcessRecord()
        {
            EmailMessage message;
            switch (ParameterSetName)
            {
                case "NewEmail":
                default:
                    message = new EmailMessage(mailboxConnection);
                    sendMessage(message);

                    break;
                case "existingEmail":
                    message = emailMessage;
                    switch (emailMessageSendType)
                    {
                        case emailMessageSendTypeOptions.forward:
                            forwardMessage(message);
                            break;
                        case emailMessageSendTypeOptions.reply:
                            replyToMessage(message);
                            break;
                        case emailMessageSendTypeOptions.send:
                            sendMessage(message);
                            break;
                        case emailMessageSendTypeOptions.replyAll:
                            replyToMessage(message);
                            break;
                    }
                    break;
            }
            WriteObject(message);

        }

        private void sendMessage(EmailMessage message)
        {
            // Setup Recipients
            foreach (string recipient in Recipients)
            {
                message.ToRecipients.Add(recipient);
            }
            if (BCCRecipients != null)
            {
                foreach (string recipient in BCCRecipients)
                {
                    message.BccRecipients.Add(recipient);
                }
            }
            if (CCRecipients != null)
            {
                foreach (string recipient in CCRecipients)
                {
                    message.CcRecipients.Add(recipient);
                }
            }

            if (Subject != null) { message.Subject = Subject; }
            if (Body != null) { MessageBody body = new MessageBody(bodyFormat, Body); message.Body = body; }

            if (attachments != null)
            {
                foreach (FileInfo attachLocation in attachments)
                {
                    if (attachLocation.Exists)
                    {
                        message.Attachments.AddFileAttachment(attachLocation.FullName);
                    }
                }
            }
            message.Importance = ImportanceLevel;

            if (RequestReadResponse.IsPresent) { message.IsReadReceiptRequested = true; }
            if (RequestResponse.IsPresent) { message.IsResponseRequested = true; }
            if (RequestDeliveryReceipt.IsPresent) { message.IsDeliveryReceiptRequested = true; }

            message.SendAndSaveCopy();
        }
        private void forwardMessage(EmailMessage message)
        {
            ResponseMessage rMessage = message.CreateForward();
            // Setup Recipients
            if (Recipients != null)
            {
                foreach (string recipient in Recipients)
                {
                    rMessage.ToRecipients.Add(recipient);
                }
            }
            if (BCCRecipients != null)
            {
                foreach (string recipient in BCCRecipients)
                {
                    rMessage.BccRecipients.Add(recipient);
                }
            }
            if (CCRecipients != null)
            {
                foreach (string recipient in CCRecipients)
                {
                    rMessage.CcRecipients.Add(recipient);
                }
            }

            if (Subject != null) { rMessage.Subject = Subject; }
            if (Body != null) { MessageBody body = new MessageBody(bodyFormat, Body); rMessage.BodyPrefix = body; }

            if (RequestReadResponse.IsPresent) { rMessage.IsReadReceiptRequested = true; }
            if (RequestDeliveryReceipt.IsPresent) { rMessage.IsDeliveryReceiptRequested = true; }

            rMessage.SendAndSaveCopy();
        }
        private void replyToMessage(EmailMessage message)
        {
            ResponseMessage rMessage;
            if (emailMessageSendType.Equals(emailMessageSendTypeOptions.replyAll)) { rMessage = message.CreateReply(true); }
            else { rMessage = message.CreateReply(false); }
            // Setup Recipients
            if (Recipients != null)
            {
                foreach (string recipient in Recipients)
                {
                    rMessage.ToRecipients.Add(recipient);
                }
            }
            if (BCCRecipients != null)
            {
                foreach (string recipient in BCCRecipients)
                {
                    rMessage.BccRecipients.Add(recipient);
                }
            }
            if (CCRecipients != null)
            {
                foreach (string recipient in CCRecipients)
                {
                    rMessage.CcRecipients.Add(recipient);
                }
            }

            if (Subject != null) { rMessage.Subject = Subject; }
            if (Body != null) { MessageBody body = new MessageBody(bodyFormat, Body); rMessage.BodyPrefix = body; }

            if (RequestReadResponse.IsPresent) { rMessage.IsReadReceiptRequested = true; }
            if (RequestDeliveryReceipt.IsPresent) { rMessage.IsDeliveryReceiptRequested = true; }

            rMessage.SendAndSaveCopy();
        }
    }
}
