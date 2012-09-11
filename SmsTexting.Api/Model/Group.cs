using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using SmsTexting.Extensions;

namespace SmsTexting
{
    public class Group
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int ContactCount { get; set; }

        public Group() { }

        public Group(string Name, string Note)
        {
            this.Name = Name;
            this.Note = Note;
        }

        public override string ToString()
        {
            return "Group{" +
                     "ID='" + ID + '\'' +
                     ", Name='" + Name + '\'' +
                     ", Note='" + Note + '\'' +
                     ", ContactCount=" + ContactCount +
                     '}';
        }

        internal void PutParams(RestRequest request)
        {
            Require.Argument("Name", Name);

            request.AddParameter("Name", Name);
            request.AddParameterIfHasValue("Note", Note);
        }

    }
}
