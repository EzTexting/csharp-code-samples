using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Validation;
using SmsTexting.Extensions;

namespace SmsTexting
{
    public class InboxFolder
    {
        /// <summary>
        /// Unique ID referencing the folder
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Name of the folder
        /// </summary>
        public string Name { get; set; }

        public InboxFolder() { }

        public InboxFolder(string Name)
        {
            this.Name = Name;
        }

        public override string ToString()
        {
            return "InboxFolder{" +
                     "ID='" + ID + '\'' +
                     ", Name='" + Name + '\'' +
                     '}';
        }

        internal void PutParams(RestRequest request)
        {
            Require.Argument("Name", Name);
            request.AddParameter("Name", Name);
        }

    }
}
