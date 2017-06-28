using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace MSMQ
{
    [Activity("Send MSMQ Message")]
    public class SendMSMQMessage : IActivity
    {
        private string destinationQueueFormatExample = @"FormatName:DIRECT=OS:mymachine\PRIVATE$\MyQueueQueue";
        private string destinationQueue = "Destination Message Queue";
        private string inputMessage = "Message";
        private string status = "Status";
        private string authentiation = "Use Authentation";

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(destinationQueue).WithDefaultValue(destinationQueueFormatExample);
            designer.AddInput(inputMessage);
            designer.AddInput(authentiation).WithBooleanBrowser();
            designer.AddOutput(destinationQueue);
            designer.AddOutput(inputMessage);
            designer.AddOutput(authentiation);
            designer.AddOutput(status);
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string messageQueue = request.Inputs[destinationQueue].AsString();
            string message = request.Inputs[inputMessage].AsString();
            bool useAuthentication = request.Inputs[authentiation].AsBoolean();

            // check if queue exists, if not create it
            
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(messageQueue))
            {
                throw new Exception(String.Format("The message queue\n{0}\ndoes not exist", messageQueue));
            }
            else
            {
                msMq = new MessageQueue(messageQueue);
            }
            using (msMq)
            {
                Message msg = new Message(message);
                msg.UseAuthentication = useAuthentication;
                msMq.Send(msg, MessageQueueTransactionType.Single);
            }

            response.Publish(destinationQueue, messageQueue);
            response.Publish(inputMessage, message);
            response.Publish(authentiation, useAuthentication);
            response.Publish(status, "Success");

        }
    }
}
