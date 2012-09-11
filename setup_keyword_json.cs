ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RESTex4J
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/keywords/honey?format=json");
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            using (Stream writeStream = w.GetRequestStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("User=winnie&Password=the-pooh&EnableDoubleOptIn=1&ConfirmMessage=Reply Y to join our sweetest list&JoinMessage=The only reason for being a bee that I know of, is to make honey. And the only reason for making honey, is so as I can eat it.&ForwardEmail=honey@bear-alliance.co.uk&ForwardUrl=http://bear-alliance.co.uk/honey-donations/&ContactGroupIDs[]=honey lovers");
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
                System.Console.Out.WriteLine("Keyword ID: " + response.SelectToken("Response.Entry.ID"));
                System.Console.Out.WriteLine("Keyword: " + response.SelectToken("Response.Entry.Keyword"));
                System.Console.Out.WriteLine("Is double opt-in enabled: " + response.SelectToken("Response.Entry.EnableDoubleOptIn"));
                System.Console.Out.WriteLine("Confirm message: " + response.SelectToken("Response.Entry.ConfirmMessage"));
                System.Console.Out.WriteLine("Join message: " + response.SelectToken("Response.Entry.JoinMessage"));
                System.Console.Out.WriteLine("Forward email: " + response.SelectToken("Response.Entry.ForwardEmail"));
                System.Console.Out.WriteLine("Forward url: " + response.SelectToken("Response.Entry.ForwardUrl"));
                System.Console.Out.WriteLine("Groups: " + response.SelectToken("Response.Entry.ContactGroupIDs"));
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

