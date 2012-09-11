using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmsTexting;

namespace SmsTextingApiExamples
{
    class Contacts
    {
        static void Main(string[] args)
        {
            var sms = new SmsTextingRestClient("demouser", "password", SmsTextingRestClient.JSON);
            System.Console.Out.WriteLine("JSON encoding.");

            var contacts = sms.GetContacts(null, null, null, "Honey Lovers", null, null, null, null);
            System.Console.Out.WriteLine("Get contacts:");
            contacts.ForEach(t => System.Console.Out.WriteLine(t));


            var contact = new Contact("2123456899", "Piglet", "P.", "piglet@small-animals-alliance.org", "It is hard to be brave, when you are only a Very Small Animal.", null);
            contact = sms.CreateContact(contact);
            System.Console.Out.WriteLine("Contact create: " + contact);

            contact = sms.GetContact(contact.ID);
            System.Console.Out.WriteLine("Contact get: " + contact);

            contact.Note = "The note";
            contact.Groups.Add("Friends");
            contact.Groups.Add("Neighbors");
            contact = sms.UpdateContact(contact);
            System.Console.Out.WriteLine("Contact update: " + contact);

            sms.DeleteContact(contact.ID);
            System.Console.Out.WriteLine("Contact delete.");
            try
            {
                sms.DeleteContact(contact.ID);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Get Exception after delete not existent contact: " + e.Message);
            }

        }
    }
}

