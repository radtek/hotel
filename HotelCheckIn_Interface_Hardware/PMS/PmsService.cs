using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.PMS
{
    public class PmsService
    {
        private string WebServiceUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">酒店接口webservice地址</param>
        public PmsService(string url)
        {
            WebServiceUrl = url;
        }

        /// <summary>
        /// pms接口
        /// </summary>
        /// <param name="invokeBaseData">接口传递数据类</param>
        /// <returns> </returns>
        public string InvokeService(InvokeBaseData invokeBaseData)
        {
            string result = "", tempUrl = "", content = "";
            tempUrl = !string.IsNullOrEmpty(WebServiceUrl) ? WebServiceUrl : "http://192.168.20.200:8083/HepInterface_Service.asmx/WebConnector";
            
            content = "v=" + invokeBaseData.Version + "&n=" + invokeBaseData.Number +
                "&f=" + invokeBaseData.Function + "&q=" + invokeBaseData.QueryString +
                "&c=" + invokeBaseData.ClientInfo + "&s=" + invokeBaseData.State;
            StreamReader myStreamReader;
            var bs = Encoding.UTF8.GetBytes(content);
            var request = (HttpWebRequest)HttpWebRequest.Create(tempUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bs.Length;
            try
            {
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                }
                var wr = request.GetResponse();
                var myResponseStream = wr.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream);
                result = myStreamReader.ReadToEnd();
            }
            catch (WebException ce)
            {
                var res = (HttpWebResponse)ce.Response;
                try
                {
                    myStreamReader = new StreamReader(res.GetResponseStream());
                    result = myStreamReader.ReadToEnd();
                }
                catch (Exception e)
                {
                    result = ce.Message;
                }
            }
            return result;
        }
    }
}
