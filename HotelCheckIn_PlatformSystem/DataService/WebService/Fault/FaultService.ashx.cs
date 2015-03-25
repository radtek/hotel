using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using CommonLibrary;
using HotelCheckIn_PlatformSystem.DataService.BLL;
using HotelCheckIn_PlatformSystem.DataService.Bll;
using HotelCheckIn_PlatformSystem.DataService.Dal;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Model.Parameter;
using log4net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Data;


namespace HotelCheckIn_PlatformSystem.DataService.WebService.Fault
{
    /// <summary>
    /// Summary description for FaultService
    /// </summary>
    public class FaultService : IHttpHandler, IRequiresSessionState
    {
        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        readonly string _heartbeatdt = System.Configuration.ConfigurationManager.AppSettings["heartbeatdt"];
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Session[Constant.LoginUser] == null)
                {
                    context.Response.Write("location.href='../../Login.aspx';");
                }
            }
            catch (Exception)
            {
                context.Response.Write("location.href='../../Login.aspx';");
            }
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
        /// 查询故障数据
        /// </summary>
        /// <param name="context"></param>
        private void QueryFault(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<QueryFault>();
            var faultdal = new FaultDal();
            var datapage = new DataPage();
            var page = context.Request.Params["page"];
            var rows = context.Request.Params["rows"];

            if (!string.IsNullOrEmpty(page))
                datapage.Index = int.Parse(page);
            if (!string.IsNullOrEmpty(rows))
                datapage.Count = int.Parse(rows);

            var jdid = context.Request.Params["jdid"];//酒店id
            var gzid = context.Request.Params["gzid"];//故障id
            var kssj = context.Request.Params["kssj"];//开始时间
            var jssj = context.Request.Params["jssj"];//结束时间

            DateTime? kssjdt = !string.IsNullOrEmpty(kssj) ? (DateTime?)DateTime.Parse(kssj) : null;
            DateTime? jssjdt = !string.IsNullOrEmpty(jssj) ? (DateTime?)DateTime.Parse(jssj).AddDays(1) : null;
            try
            {
                Dictionary<int, DataTable> dic = faultdal.QueryFault(jdid, gzid, kssjdt, jssjdt, datapage);
                foreach (var dataTable in dic)
                {
                    var list = ConvertHelper<QueryFault>.ConvertToList(dataTable.Value);
                    json.Rows = list;
                    json.Total = dataTable.Key;
                }
                json.IsSuccess = true;
                json.Msg = "查询成功";
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "查询失败" + e;
                strjson = _jss.Serialize(json);
            }
            context.Response.Write(strjson);
        }


        /// <summary>
        /// 绑定故障
        /// </summary>
        /// <param name="context"></param>
        private void BindFault(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Code>();
            var codedal = new CodeDal();
            try
            {
                var dt = codedal.QueryFault();
                var list = ConvertHelper<Code>.ConvertToList(dt);
                json.Rows = list;
                json.IsSuccess = true;
                json.Msg = "查询成功";
                json.JsExecuteMethod = "ajax_BindFault";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "查询失败";
                strjson = _jss.Serialize(json);
            }
            context.Response.Write(strjson);
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