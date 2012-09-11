ServicePointManager.Expect100Continue = true;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace RESTex1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isSuccesResponse = true;
            string ret = string.Empty;
            WebRequest w = WebRequest.Create("https://app.eztexting.com/sending/messages?format=xml");
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
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    ret = GetResponseString(ex.Response);
                    isSuccesResponse = false;
                }
            }
            XmlDocument response = new XmlDocument();
            response.LoadXml(ret);
            System.Console.Out.WriteLine("Status: "+response.SelectSingleNode("Response/Status").InnerText);
            System.Console.Out.WriteLine("Code: " + response.SelectSingleNode("Response/Code").InnerText);
            if (isSuccesResponse)
            {
                System.Console.Out.WriteLine("Message ID: " + response.SelectSingleNode("Response/Entry/ID").InnerText);
                System.Console.Out.WriteLine("Subject: " + response.SelectSingleNode("Response/Entry/Subject").InnerText);
                System.Console.Out.WriteLine("Message: " + response.SelectSingleNode("Response/Entry/Message").InnerText);
                System.Console.Out.WriteLine("Message Type ID: " + response.SelectSingleNode("Response/Entry/MessageTypeID").InnerText);
                System.Console.Out.WriteLine("Total Recipients: " + response.SelectSingleNode("Response/Entry/RecipientsCount").InnerText);
                System.Console.Out.WriteLine("Credits Charged: " + response.SelectSingleNode("Response/Entry/Credits").InnerText);
                System.Console.Out.WriteLine("Time To Send: " + response.SelectSingleNode("Response/Entry/StampToSend").InnerText);
                System.Console.Out.WriteLine("Phone Numbers: " + ImplodeList(response.SelectNodes("Response/Entry/PhoneNumbers/*")));
                System.Console.Out.WriteLine("Locally Opted Out Numbers: " + ImplodeList(response.SelectNodes("Response/Entry/LocalOptOuts/*")));
                System.Console.Out.WriteLine("Globally Opted Out Numbers: " + ImplodeList(response.SelectNodes("Response/Entry/GlobalOptOuts/*")));
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
              