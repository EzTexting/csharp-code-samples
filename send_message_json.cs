ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RESTex1J
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/sending/messages?format=json");
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            using (Stream writeStream = w.GetRequestStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("User=winnie&Password=the-pooh&PhoneNumbers[]=2123456785&PhoneNumbers[]=2123456786&PhoneNumbers[]=2123456787&PhoneNumbers[]=2123456788&Subject=From Winnie&Message=I am a Bear of Very Little Brain, and long words bother me&StampToSend=1305582245&MessageTypeID=1");
                writeStream.Write(bytes, 0, bytes.Length);
            }
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
                System.Console.Out.WriteLine("Message ID: " + response.SelectToken("Response.Entry.ID"));
                System.Console.Out.WriteLine("Subject: " + response.SelectToken("Response.Entry.Subject"));
                System.Console.Out.WriteLine("Message: " + response.SelectToken("Response.Entry.Message"));
                System.Console.Out.WriteLine("Message Type ID: " + response.SelectToken("Response.Entry.MessageTypeID"));
                System.Console.Out.WriteLine("Total Recipients: " + response.SelectToken("Response.Entry.RecipientsCount"));
                System.Console.Out.WriteLine("Credits Charged: " + response.SelectToken("Response.Entry.Credits"));
                System.Console.Out.WriteLine("Time To Send: " + response.SelectToken("Response.Entry.StampToSend"));
                System.Console.Out.WriteLine("Phone Numbers: " + response.SelectToken("Response.Entry.PhoneNumbers"));
                System.Console.Out.WriteLine("Locally Opted Out Numbers: " + response.SelectToken("Response.Entry.LocalOptOuts"));
                System.Console.Out.WriteLine("Globally Opted Out Numbers: " + response.SelectToken("Response.Entry.GlobalOptOuts"));
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

              