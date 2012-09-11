using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace SmsTexting.Extensions
{
    public static class RequestExtension
    {
        public static void AddParameterIfHasValue(this RestRequest request, string name, string value)
        {
            if (!string.IsNullOrEmpty(value)) request.AddParameter(name, value);
        }
    }
}
