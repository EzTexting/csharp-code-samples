ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RESTex8J
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/sending/phone-numbers/2123456786?format=json&User=winnielkup&Password=winnielkup1");
            try
            {
                using (HttpWebResponse r = (HttpWebResponse)w.GetResponse())
                {
                    ret = GetResponseString(r);
                }
            }
            catch (System.Net.WebException ex)
            {
                isSuccesResponse = false;
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    ret = GetResponseString(ex.Response);
                }
            }
            // use free Json.NET library (http://json.codeplex.com/) for JSON handling
            JObject response = JObject.Parse(ret);
            System.Console.Out.WriteLine("Status: " + response.SelectToken("Response.Status"));
            System.Console.Out.WriteLine("Code: " + response.SelectToken("Response.Code"));
            if (isSuccesResponse)
            {
                System.Console.Out.WriteLine("Phone number: " + response.SelectToken("Response.Entry.PhoneNumber"));
                System.Console.Out.WriteLine("CarrierName: " + response.SelectToken("Response.Entry.CarrierName"));
            }
            else
            {
                System.Console.Out.WriteLine("Errors: " + response.SelectToken("Response.Errors"));
            }
        }

        static string GetResponseString(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                {
                    return readStream.ReadToEnd();
                }
            }
        }
    }
}

