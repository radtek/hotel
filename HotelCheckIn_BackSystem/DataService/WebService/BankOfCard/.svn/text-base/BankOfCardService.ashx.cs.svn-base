using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Data;

namespace HotelCheckIn_BackSystem.DataService.WebService.BankOfCard
{
    /// <summary>
    /// Summary description for BankOfCardService
    /// </summary>
    public class BankOfCardService : IHttpHandler
    {

        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        readonly string _heartbeatdt = System.Configuration.ConfigurationManager.AppSettings["heartbeatdt"];
        public void ProcessRequest(HttpContext context)
        {
           context.Response.ContentType = "text/plain";
            var action = context.Request.Params["action"];
            var curType = GetType();
            var method = curType.GetMethod(action, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(this, new object[] { HttpContext.Current });
            }
            else
            {
                context.Response.Write("没有这个方法！");
            }
        }


        /// <summary>
        /// 查询交易信息
        /// </summary>
        /// <param name="context"></param>
        private void QueryTradingData(HttpContext context)
        {
            var hashTable = new Hashtable();
            var tradingDal = new TradingInfoDal();
            var tradingInfo = new TradingInfoModel();

            var page = context.Request.Params["page"];
            var rows = context.Request.Params["rows"];
            var mypage = 0;
            var myrows = 0;
            var count = 0;
            if (!string.IsNullOrEmpty(page))
                mypage = int.Parse(page);
            if (!string.IsNullOrEmpty(rows))
                myrows = int.Parse(rows);
            try
            {
                var dt = tradingDal.QueryByPage(tradingInfo, mypage, myrows, ref count);
                var list = from row in dt.AsEnumerable()
                           select new TradingInfoModel
                           {
                               Id = row.Field<string>("Id"),
                               TradingType = row.Field<string>("TradingType"),
                               RoomNumber = row.Field<string>("RoomNumber"),
                               CheckOrderNumber = row.Field<string>("CheckOrderNumber"),
                               Temp1 = row.Field<string>("Temp1"),
                               Temp2 = row.Field<string>("Temp2"),
                               Temp3 = row.Field<string>("Temp3"),
                               ReturnCode = row.Field<string>("ReturnCode"),
                               BankNumber = row.Field<string>("BankNumber"),
                               CardNumber = row.Field<string>("CardNumber"),
                               Valid = row.Field<string>("Valid"),
                               LotNo = row.Field<string>("LotNo"),
                               CertificateNo = row.Field<string>("CertificateNo"),
                               Money = row.Field<string>("Money"),
                               ContactNo = row.Field<string>("ContactNo"),
                               TerminalNo = row.Field<string>("TerminalNo"),
                               TransactionReferenceNumber = row.Field<string>("TransactionReferenceNumber"),
                               TradingDate = row.Field<string>("TradingDate"),
                               TradingTime = row.Field<string>("TradingTime"),
                               AuthorizationNumber = row.Field<string>("AuthorizationNumber")
                           };
                hashTable["total"] = count;
                hashTable["rows"] = list;
                var json = _jss.Serialize(hashTable);
                context.Response.Write(json);
            }
            catch (Exception e)
            {
                hashTable["IsSuccess"] = false;
                hashTable["Msg"] = "错误" + e.Message;
                var json = _jss.Serialize(hashTable);
                context.Response.Write(json);
                Log.Error(e.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}