ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace RESTex8
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/sending/phone-numbers/2123456786?format=xml&User=winnielkup&Password=winnielkup");
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
            XmlDocument response = new XmlDocument();
            response.LoadXml(ret);
            System.Console.Out.WriteLine("Status: " + response.SelectSingleNode("Response/Status").InnerText);
            System.Console.Out.WriteLine("Code: " + response.SelectSingleNode("Response/Code").InnerText);
            if (isSuccesResponse)
            {
                System.Console.Out.WriteLine("Phone number: " + response.SelectSingleNode("Response/Entry/PhoneNumber").InnerText);
                System.Console.Out.WriteLine("CarrierName: " + response.SelectSingleNode("Response/Entry/CarrierName").InnerText);
            }
            else
            {
                System.Console.Out.WriteLine("Errors: " + ImplodeList(response.SelectNodes("Response/Errors/*")));
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

