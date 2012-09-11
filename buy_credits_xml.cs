using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace RESTex7_saved
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/billing/credits?format=xml");
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
                XmlDocument response = new XmlDocument();
                response.LoadXml(ret);
                System.Console.Out.WriteLine("Status: " + response.SelectSingleNode("Response/Status").InnerText);
                System.Console.Out.WriteLine("Code: " + response.SelectSingleNode("Response/Code").InnerText);
                if (isSuccesResponse)
                {
                    System.Console.Out.WriteLine("Credits purchased: " + response.SelectSingleNode("Response/Entry/BoughtCredits").InnerText);
                    System.Console.Out.WriteLine("Amount charged, $: " + response.SelectSingleNode("Response/Entry/Amount").InnerText);
                    System.Console.Out.WriteLine("Discount, $: " + response.SelectSingleNode("Response/Entry/Discount").InnerText);
                    System.Console.Out.WriteLine("Plan credits: " + response.SelectSingleNode("Response/Entry/PlanCredits").InnerText);
                    System.Console.Out.WriteLine("Anytime credits: " + response.SelectSingleNode("Response/Entry/AnytimeCredits").InnerText);
                    System.Console.Out.WriteLine("Total: " + response.SelectSingleNode("Response/Entry/TotalCredits").InnerText);
                }
                else
                {
                    System.Console.Out.WriteLine("Errors: " + ImplodeList(response.SelectNodes("Response/Errors/*")));
                }
            }
        }

        static string ImplodeList(XmlNodeList list)
        {
            string ret = "";
            foreach (XmlNode node in list)
            {
                ret += ", " + node.InnerText;
            }
            return ret.Length > 2 ? ret.Substring(2) : ret;
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

                    