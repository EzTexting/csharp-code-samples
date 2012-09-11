ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace RESTex5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            HttpStatusCode retStatus = HttpStatusCode.Unused;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/keywords/honey?format=xml&_method=DELETE");
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
            if (!isSuccesResponse && ret.Length > 0)
            {
                XmlDocument response = new XmlDocument();
                response.LoadXml(ret);
                System.Console.Out.WriteLine("Status: " + response.SelectSingleNode("Response/Status").InnerText);
                System.Console.Out.WriteLine("Code: " + response.SelectSingleNode("Response/Code").InnerText);
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

