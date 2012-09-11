using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using SmsTexting.Extensions;

namespace SmsTexting
{
    public class Contact
    {
        public string ID { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string Source { get; set; }
        public List<string> Groups { get; set; }
        public DateTime CreatedAt { get; set; }

        public Contact() { }

        public Contact(string phoneNumber, string firstName, string lastName, string email, string note, List<string> groups)
        {
            this.PhoneNumber = phoneNumber;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Note = note;
            this.Groups = groups;
        }


        public override string ToString()
        {
            return "Contact{" +
             "ID='" + ID + '\'' +
             ", PhoneNumber='" + PhoneNumber + '\'' +
             ", FirstName='" + FirstName + '\'' +
             ", LastName='" + LastName + '\'' +
             ", Email='" + Email + '\'' +
             ", Note='" + Note + '\'' +
             ", Source='" + Source + '\'' +
             ", Groups=" + (Groups == null ? null : "{" + (Groups.Count > 0 ? Groups.Aggregate((current, next) => current + "; " + next) : "") + "}") +
             ", CreatedAt='" + CreatedAt.ToShortDateString() + '\'' +
             '}';
 
        }

        internal void PutParams(RestRequest request)
        {
            Require.Argument("PhoneNumber", PhoneNumber);

            request.AddParameter("PhoneNumber", PhoneNumber);
            request.AddParameterIfHasValue("FirstName", FirstName);
            request.AddParameterIfHasValue("LastName", LastName);
            request.AddParameterIfHasValue("Email", Email);
            request.AddParameterIfHasValue("Note", Note);
            if (Groups != null)
            {
                for (int i = 0; i < Groups.Count; i++)
                {
                    request.AddParameter("Groups[" + i + "]", Groups[i]);
                }
            }

        }
    }
}
