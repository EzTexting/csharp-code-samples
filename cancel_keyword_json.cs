ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RESTex5J
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            HttpStatusCode retStatus = HttpStatusCode.Unused;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/keywords/honey?format=json&_method=DELETE");
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            using (Stream writeStream = w.GetRequestStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("User=winnie&Password=the-pooh");
                writeStream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                using (HttpWebResponse r = (HttpWebResponse)w.GetResponse())
                {
                    retStatus = r.StatusCode;
                }
            }
            catch (System.Net.WebException ex)
            {
                isSuccesResponse = false;
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    retStatus = ((HttpWebResponse)ex.Response).StatusCode;
                    ret = GetResponseString(ex.Response);
                }
            }
            System.Console.Out.WriteLine("Status: " + retStatus);
            if (!isSuccesResponse)
            {
                // use free Json.NET library (http://json.codeplex.com/) for JSON handling
                JObject response = JObject.Parse(ret);
                System.Console.Out.WriteLine("Status: " + response.SelectToken("Response.Status"));
                System.Console.Out.WriteLine("Code: " + response.SelectToken("Response.Code"));
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

                      