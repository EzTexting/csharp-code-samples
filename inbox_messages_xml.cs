using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmsTexting;

namespace SmsTextingApiExamples
{
    class InboxMessages
    {
        static void Main(string[] args)
        {
            var sms = new SmsTextingRestClient("centerft", "texting121212", SmsTextingRestClient.XML);
            System.Console.Out.WriteLine("XML encoding.");

            var inboxMessages = sms.GetInboxMessages(null, null, null, null, null, null);
            System.Console.Out.WriteLine("Get inboxMessages:");
            inboxMessages.ForEach(t => System.Console.Out.WriteLine(t));

            sms.MoveInboxMessages(inboxMessages.Select(m => m.ID).ToList(), "77");
            System.Console.Out.WriteLine("InboxMessages Move.");

            var msg = inboxMessages.First();

            sms.MoveInboxMessage(msg.ID, "77");
            System.Console.Out.WriteLine("InboxMessage Move.");


            var inboxMessage = sms.GetInboxMessage(msg.ID);
            System.Console.Out.WriteLine("InboxMessage get: " + inboxMessage);


            sms.DeleteInboxMessage(inboxMessage.ID);
            System.Console.Out.WriteLine("InboxMessage delete.");
            try
            {
                sms.DeleteInboxMessage(inboxMessage.ID);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Get Exception after delete not existent inboxMessage: " + e.Message);
            }
        }
    }
}
