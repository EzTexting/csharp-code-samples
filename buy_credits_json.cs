using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RESTex7J_saved
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/billing/credits?format=json");
            w.Method = "POST";
            w.ContentType = "application/x-www-form-urlencoded";
            using (Stream writeStream = w.GetRequestStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("User=demo&Password=texting121212&NumberOfCredits=1000&CouponCode=honey2011&StoredCreditCard=1111");
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
                    System.Console.Out.WriteLine(((HttpWebResponse)ex.Response).StatusCode);
                    ret = GetResponseString(ex.Response);
                }
            }
            if (ret.Length > 0)
            {
                // use free Json.NET library (http://json.codeplex.com/) for JSON handling
                JObject response = JObject.Parse(ret);

                System.Console.Out.WriteLine("Status: " + response.SelectToken("Response.Status"));
                System.Console.Out.WriteLine("Code: " + response.SelectToken("Response.Code"));
                if (isSuccesResponse)
                {
                    System.Console.Out.WriteLine("Credits purchased: " + response.SelectToken("Response.Entry.BoughtCredits"));
                    System.Console.Out.WriteLine("Amount charged, $: " + response.SelectToken("Response.Entry.Amount"));
                    System.Console.Out.WriteLine("Discount, $: " + response.SelectToken("Response.Entry.Discount"));
                    System.Console.Out.WriteLine("Plan credits: " + response.SelectToken("Response.Entry.PlanCredits"));
                    System.Console.Out.WriteLine("Anytime credits: " + response.SelectToken("Response.Entry.AnytimeCredits"));
                    System.Console.Out.WriteLine("Total: " + response.SelectToken("Response.Entry.TotalCredits"));
                }
                else
                {
                    System.Console.Out.WriteLine("Errors: " + response.SelectToken("Response.Errors"));
                }
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
