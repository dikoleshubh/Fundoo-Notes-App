using System;
using System.Collections.Generic;
using System.Linq;
using Experimental.System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FundooMSMQ
{
    public class Sender
    {
        public void SendMessage()
        {
            var url = "Click on following link to reset your credentials for Fundoonotes App: https://localhost:44387/api/User/ResetPassword";
            MessageQueue msmqQueue = new MessageQueue();
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msmqQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msmqQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            msmqQueue.Label = "url link";
            msmqQueue.Send(message);
        }
    }
}
