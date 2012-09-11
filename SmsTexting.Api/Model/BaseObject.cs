using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmsTexting
{
    public class BaseObject
    {
        /// <summary>
        /// Exception encountered during API request
        /// </summary>
        public List<string> Errors { get; set; }
        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        public int Code { get; set; }
    }
}
