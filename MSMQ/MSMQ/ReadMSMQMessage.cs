using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace MSMQ
{
    [Activity("Read MSMQ Message")]
    public class ReadMSMQMessage : IActivity
    {
        private string destinationQueueFormatExample = @"FormatName:DIRECT=OS:mymachine\PRIVATE$\MyQueueQueue";
        private string destinationQueue = "Message Queue Name";
        private string outputMessage = "Message";
        private string authentiation = "Use Authentation";

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput(destinationQueue).WithDefaultValue(destinationQueueFormatExample);            
            designer.AddInput(authentiation).WithBooleanBrowser();
            designer.AddOutput(destinationQueue);
            designer.AddOutput(outputMessage);
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string messageQueue = request.Inputs[destinationQueue].AsString();
            Message msg = new Message();
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
                msg = msMq.Receive();
            }

            response.Publish(destinationQueue, messageQueue);
            response.Publish(outputMessage, msg.Body.ToString());
        }
    }
}
