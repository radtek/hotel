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
using HotelCheckIn_PlatformSystem.DataService.Dal;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Model.Parameter;
using log4net;
using System.Web;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.SwfUpload
{
    /// <summary>
    /// Summary description for SwfUploadService
    /// </summary>
    public class SwfUploadService : IHttpHandler, IRequiresSessionState
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
        /// 保存swfupload控件上传的文件
        /// </summary>
        /// <param name="context"></param>
        public void SaveFile(HttpContext context)
        {
            var filesDal = new FilesDal();
            var files = new Model.Files();
            var scid = context.Request.Params["scid"];
            HttpPostedFile file = context.Request.Files["Filedata"];
            if (file == null) return;
            var pathDirectory = _filepath + scid;
            var pathAll = _filepath + scid + @"\" + file.FileName;
           
            files.Scid = scid;
            var size = file.ContentLength / 1024;
            var flag = file.ContentLength % 1024;
            if (flag > 0)
            {
                size++;
            }
            files.Size = size.ToString();
            files.FileName = file.FileName.Split('.')[0];
            files.Extension = "." + file.FileName.Split('.')[1];
            files.Type = file.FileName.Split('.')[1];
            try
            {
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                if (File.Exists(pathAll))
                {
                   filesDal.ModifyByName(files);
                }
                else
                {
                    files.Id = Guid.NewGuid().ToString();
                    files.CreateDt = DateTime.Now;
                    filesDal.Add(files);
                }
                file.SaveAs(pathAll);
            }
            catch (Exception)
            {
                throw;
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