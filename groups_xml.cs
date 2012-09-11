using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmsTexting;

namespace SmsTextingApiExamples
{
    class Groups
    {
        static void Main(string[] args)
        {
            var sms = new SmsTextingRestClient("demouser", "password", SmsTextingRestClient.XML);
            System.Console.Out.WriteLine("XML encoding.");

            var groups = sms.GetGroups("Name", "asc", "10", "1");
            System.Console.Out.WriteLine("Get groups:");
            groups.ForEach(t => System.Console.Out.WriteLine(t));


            var group = new Group("Tubby Bears", "A bear, however hard he tries, grows tubby without exercise");
            group = sms.CreateGroup(group);
            System.Console.Out.WriteLine("Group create: " + group);

            group = sms.GetGroup(group.ID);
            System.Console.Out.WriteLine("Group get: " + group);

            group.Note = "The note";
            group = sms.UpdateGroup(group);
            System.Console.Out.WriteLine("Group update: " + group);

            sms.DeleteGroup(group.ID);
            System.Console.Out.WriteLine("Group delete.");
            try
            {
                sms.DeleteGroup(group.ID);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine("Get Exception after delete not existent group: " + e.Message);
            }


        }
    }
}



