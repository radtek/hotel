using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using HotelCheckIn_BackSystem.DataService.Model.Parameter;
using log4net;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.WebService.Hotels
{
    /// <summary>
    /// Summary description for HotelsService
    /// </summary>
    public class HotelsService : IHttpHandler, IRequiresSessionState
    {
        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            //session过期验证
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
            Type curType = GetType();
            MethodInfo method = curType.GetMethod(action, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
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
        /// 获取数据绑定到页面select的酒店数据
        /// </summary>
        /// <param name="context"></param>
        private void BindHotelsData(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Model.Hotels>();
            var hotelDal = new HotelsDal();
            var areaid = context.Request.Params["areaid"];
            var type = context.Request.Params["type"];
            try
            {
                var dt = hotelDal.FindByHotelsByAreaId(areaid);
                var list = ConvertHelper<Model.Hotels>.ConvertToList(dt);
                json.Rows = list;
                json.IsSuccess = true;
                json.Msg = "获取成功";
                switch (type)
                {
                    case "1":
                        json.Other = "1";
                        break;
                    case "2":
                        json.Other = "2";
                        break;
                }
                json.JsExecuteMethod = "ajax_BindHotelsData";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                Log.Debug("错误信息：" + e);
                throw;
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