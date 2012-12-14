using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using SmsTexting.Extensions;

namespace SmsTexting
{
    public class InboxMessage
    {
        /// <summary>
        /// Unique ID referencing the message
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Phone number of the sender
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Subject of the message
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Message Body
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// If Messsage is New (Unread in Ez Texting Web App)
        /// </summary>
        public bool New { get; set; }

        /// <summary>
        /// ID of the folder which contains message. If FolderID is not present then message is located in Inbox.
        /// </summary>
        public string FolderID { get; set; }

        /// <summary>
        /// ID of the Contact who sent the message. If ContactID is not present then the contact doesn't exist.
        /// </summary>
        public string ContactID { get; set; }

        /// <summary>
        /// Date when message was received
        /// </summary>
        public DateTime ReceivedOn { get; set; }

        public InboxMessage() { }

        public override string ToString()
        {
            return "InboxMessage{" +
                     "ID='" + ID + '\'' +
                     ", PhoneNumber='" + PhoneNumber + '\'' +
                     ", Subject='" + Subject + '\'' +
                     ", Message='" + Message + '\'' +
                     ", New='" + New + '\'' +
                     ", FolderID='" + FolderID + '\'' +
                     ", ContactID='" + ContactID + '\'' +
                     ", ReceivedOn='" + ReceivedOn.ToShortDateString() + '\'' +
                     '}';
        }
    }
}
