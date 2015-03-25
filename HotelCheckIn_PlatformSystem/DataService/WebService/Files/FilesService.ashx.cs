using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_PlatformSystem.DataService.BLL;
using HotelCheckIn_PlatformSystem.DataService.Bll;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Dal;
using HotelCheckIn_PlatformSystem.DataService.Model.Parameter;
using log4net;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.Files
{
    /// <summary>
    /// Summary description for FilesService
    /// </summary>
    public class FilesService : IHttpHandler, IRequiresSessionState
    {
        protected ILog Log = LogManager.GetLogger("PriceAdjustmentService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        readonly string _filepath = System.Configuration.ConfigurationManager.AppSettings["swffilepath"];
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
            MethodInfo method = curType.GetMethod(action,
                                                  BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
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
        /// 初始化文件datagrid
        /// </summary>
        /// <param name="context"></param>
        public void InitFilesGrid(HttpContext context)
        {
            string strjson = null;
            var scid = context.Request.Params["scid"];
            var json = new ZwJson<InitFilesGrid>();
            var filesdal = new FilesDal();
            try
            {
                var dt = filesdal.FindByFiles(scid);
                var list = ConvertHelper<InitFilesGrid>.ConvertToList(dt);
                json.Rows = list;
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            if (strjson != null)
                context.Response.Write(strjson);
        }

        /// <summary>
        /// 根据文件id删除文件
        /// </summary>
        /// <param name="context"></param>
        public void DelFilesById(HttpContext context)
        {
            string strjson = null;
            var fileid = context.Request.Params["fileid"];//文件id
            var file = context.Request.Params["file"];//文件(如test.htm)
            var scid = context.Request.Params["scid"];//素材id
            var filePath = _filepath + scid + @"\" + file;
            var json = new ZwJson<Type>();
            var filesdal = new FilesDal();
            try
            {
                filesdal.Del(new Model.Files { Id = fileid });
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                json.IsSuccess = true;
                json.Msg = "删除成功";
                json.JsExecuteMethod = "ajax_DelFilesById";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            if (strjson != null)
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