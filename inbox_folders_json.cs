﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmsTexting;

namespace SmsTextingApiExamples
{
    class InboxFolders
    {
        static void Main(string[] args)
        {
            sms = new SmsTextingRestClient("centerft", "texting121212", SmsTextingRestClient.JSON);
            System.Console.Out.WriteLine("JSON encoding.");

            inboxFolders = sms.GetInboxFolders();
            System.Console.Out.WriteLine("Get inboxFolders:");
            inboxFolders.ForEach(t => System.Console.Out.WriteLine(t));


            inboxFolder = new InboxFolder("Customers");
            inboxFolder = sms.CreateInboxFolder(inboxFolder);
            System.Console.Out.WriteLine("InboxFolder create: " + inboxFolder);

            inboxFolderId = inboxFolder.ID;

            inboxFolder = sms.GetInboxFolder(inboxFolderId);
            System.Console.Out.WriteLine("InboxFolder get: " + inboxFolder);

            inboxFolder.Name = "Customers2";
            inboxFolder.ID = inboxFolderId;
            sms.UpdateInboxFolder(inboxFolder);
            System.Console.Out.WriteLine("InboxFolder update. ");

            inboxFolder = sms.GetInboxFolder(inboxFolderId);
            System.Console.Out.WriteLine("InboxFolder get: " + inboxFolder);

            sms.DeleteInboxFolder(inboxFolderId);
            System.Console.Out.WriteLine("InboxFolder delete.");
            try
            {
                sms.DeleteInboxFolder(inboxFolderId);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Get Exception after delete not existent inboxFolder: " + e.Message);
            }

        }
    }
}
