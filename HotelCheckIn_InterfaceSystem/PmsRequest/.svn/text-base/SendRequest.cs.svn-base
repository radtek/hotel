using System.IO;
using System.Net;
using System.Text;

namespace HotelCheckIn_InterfaceSystem.PmsRequest
{
    public class SendRequest
    {

        private string url;

        public SendRequest(string reqUrl)
        {
            this.url = reqUrl;
        }

        public string sendRequest(string xmlStr)
        {
            HttpWebRequest request;
            HttpWebResponse response;

            System.Net.ServicePointManager.Expect100Continue = false;
            request = WebRequest.Create(this.url) as HttpWebRequest;
            request.Method = "post";
            request.ContentType = "text/xml; charset=UTF-8";

            byte[] reqByte = Encoding.UTF8.GetBytes(xmlStr);
            request.ContentLength = reqByte.Length;
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(reqByte, 0, reqByte.Length);
            reqStream.Close();

            response = request.GetResponse() as HttpWebResponse;
            StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

            return responseStream.ReadToEnd();
        }
    }
}
