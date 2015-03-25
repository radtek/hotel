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

namespace HotelCheckIn_PlatformSystem.DataService.WebService.Main
{
    /// <summary>
    /// Summary description for MainService
    /// </summary>
    public class MainService : IHttpHandler, IRequiresSessionState
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
        /// 初始化主页面数据
        /// </summary>
        /// <param name="context"></param>
        private void InitMainGrid(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<InitMainGrid>();
            var machinedal = new MachineDal();
            var page = context.Request.Params["page"];
            var rows = context.Request.Params["rows"];
            var mypage = 0;
            var mysize = 0;
            var count = 0;
            if (!string.IsNullOrEmpty(page))
                mypage = int.Parse(page);
            if (!string.IsNullOrEmpty(rows))
                mysize = int.Parse(rows);
            try
            {
                var nowdt = DateTime.Now;
                var dt = machinedal.FindByMain(new Model.Machine(), mypage, mysize, ref count);
                dt.Columns.Add("IsOnline");
                foreach (DataRow dataRow in dt.Rows)
                {
                    var dtspan = Convert.ToInt32(_heartbeatdt);
                    var xtsj = Convert.ToDateTime(dataRow["HeartbeatDt"]);
                    TimeSpan ts = nowdt - xtsj;
                    var result = ts.TotalMinutes;
                    if (result >= dtspan*3)
                    {
                        dataRow["IsOnline"] = "离线";
                    }
                    else
                    {
                        dataRow["IsOnline"] = "在线";
                    }
                }
                var list = ConvertHelper<InitMainGrid>.ConvertToList(dt);
                json.Rows = list;
                json.Total = count;
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                json.Msg = e.ToString();
                json.IsSuccess = false;
                Log.Error(e);
            }
            if (strjson != null) context.Response.Write(strjson);
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