ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace RESTex4
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/keywords/honey?format=xml");
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
            XmlDocument response = new XmlDocument();
            response.LoadXml(ret);
            System.Console.Out.WriteLine("Status: " + response.SelectSingleNode("Response/Status").InnerText);
            System.Console.Out.WriteLine("Code: " + response.SelectSingleNode("Response/Code").InnerText);
            if (isSuccesResponse)
            {
                System.Console.Out.WriteLine("Keyword ID: " + response.SelectSingleNode("Response/Entry/ID").InnerText);
                System.Console.Out.WriteLine("Keyword: " + response.SelectSingleNode("Response/Entry/Keyword").InnerText);
                System.Console.Out.WriteLine("Is double opt-in enabled: " + response.SelectSingleNode("Response/Entry/EnableDoubleOptIn").InnerText);
                System.Console.Out.WriteLine("Confirm message: " + response.SelectSingleNode("Response/Entry/ConfirmMessage").InnerText);
                System.Console.Out.WriteLine("Join message: " + response.SelectSingleNode("Response/Entry/JoinMessage").InnerText);
                System.Console.Out.WriteLine("Forward email: " + response.SelectSingleNode("Response/Entry/ForwardEmail").InnerText);
                System.Console.Out.WriteLine("Forward url: " + response.SelectSingleNode("Response/Entry/ForwardUrl").InnerText);
                System.Console.Out.WriteLine("Groups: " + ImplodeList(response.SelectNodes("Response/Entry/ContactGroupIDs/*")));
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

