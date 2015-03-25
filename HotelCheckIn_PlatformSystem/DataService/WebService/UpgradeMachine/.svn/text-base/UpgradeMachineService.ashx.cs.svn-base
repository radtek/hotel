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

namespace HotelCheckIn_PlatformSystem.DataService.WebService.UpgradeMachine
{
    /// <summary>
    /// Summary description for UpgradeMachineService
    /// </summary>
    public class UpgradeMachineService : IHttpHandler, IRequiresSessionState
    {

        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        readonly string _filepath = System.Configuration.ConfigurationManager.AppSettings["upgradefilepath"];
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
        /// 保存升级机器表
        /// </summary>
        /// <param name="context"></param>
        private void SaveUpgradeMachine(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<GetUpgradeFile>();
            var upgrademachinedal = new UpgradeMachineDal();
            var jqid = context.Request.Params["jqid"];
            var ckseleck = context.Request.Params["ckseleck"].Split('&');
            var download = context.Request.Params["download"].Split('&');
            try
            {
                var emp = (context.Session[Constant.LoginUser]) as Employer;
                upgrademachinedal.Del(jqid);
                foreach (string s in ckseleck)
                {
                    var isdownload = (from s1 in download
                                      where s == s1
                                      select s1).Count();
                    upgrademachinedal.Add(new Model.UpgradeMachine()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UpgradeFileId = s,
                            MachineId = jqid,
                            IsDownland = isdownload,
                            CreateDt = DateTime.Now,
                            CreatePerson = emp.Name
                        });
                }
                json.IsSuccess = true;
                json.JsExecuteMethod = "ajax_SaveUpgradeMachine";
                json.Msg = "添加成功";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "添加失败" + e;
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