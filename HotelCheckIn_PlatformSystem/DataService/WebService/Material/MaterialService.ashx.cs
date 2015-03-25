using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using HotelCheckIn_PlatformSystem.DataService.BLL;
using HotelCheckIn_PlatformSystem.DataService.Bll;
using HotelCheckIn_PlatformSystem.DataService.Dal;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Model.Parameter;
using log4net;
using CommonLibrary;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.Material
{
    /// <summary>
    /// Summary description for MaterialService
    /// </summary>
    public class MaterialService : IHttpHandler, IRequiresSessionState
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
        /// 页面初始化素材表数据
        /// </summary>
        /// <param name="context"></param>
        private void InitMaterialGrid(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<InitMaterialGrid>();
            var materialdal = new MaterialDal();
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

                var dt = materialdal.QueryByPage(new Model.Material(), mypage, mysize, ref count);
                var list = ConvertHelper<InitMaterialGrid>.ConvertToList(dt);
                json.Rows = list;
                json.Total = count;
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            if (strjson != null) context.Response.Write(strjson);
        }

        /// <summary>
        /// 保存文件路径
        /// </summary>
        /// <param name="context"></param>
        private void SaveFilePath(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Type>();
            MaterialDal materialDal = new MaterialDal();
            var scid = context.Request.Params["scid"];
            var file = context.Request.Params["file"];
            try
            {
                var filepath = "/UploadFiles/" + scid + "/" + file;
                var url = UrlHelper.Resolve(filepath);
                var material = new Model.Material
                    {
                        Id = scid,
                        Url = url
                    };
                materialDal.Modify(material);
                json.IsSuccess = true;
                json.Msg = "修改成功";
                json.JsExecuteMethod = "ajax_SaveFilePath";
                strjson = _jss.Serialize(json);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
            context.Response.Write(strjson);
        }

        /// <summary>
        /// 获取素材名称和路径
        /// </summary>
        /// <param name="context"></param>
        private void FindByMaterialGrid(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Model.Material>();
            var materialdal = new MaterialDal();
            try
            {
                var dt = materialdal.FindByMaterial();
                var list = ConvertHelper<Model.Material>.ConvertToList(dt);
                json.Rows = list;
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            if (strjson != null) context.Response.Write(strjson);
        }

        /// <summary>
        /// 添加和修改素材
        /// </summary>
        /// <param name="context"></param>
        private void AddAndEditMaterial(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<string>();
            var materialdal = new MaterialDal();
            var flag = context.Request.Params["flag"];
            var id = context.Request.Params["id"];
            var name = context.Request.Params["txt_name"];
            var note = context.Request.Params["txt_note"];
            var datetime = DateTime.Now;
            try
            {
                var emp = (context.Session[Constant.LoginUser]) as Employer;
                switch (flag)
                {
                    case "add":
                        materialdal.Add(new Model.Material()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = name,
                            Note = note,
                            DateTime = datetime,
                            Operator = emp.Name,
                            UpdateDt = datetime,
                            UpdatePerson = emp.Name
                        });
                        json.Msg = "添加成功";
                        json.IsSuccess = true;
                        json.JsExecuteMethod = "ajax_AddAndEditMaterial";
                        strjson = _jss.Serialize(json);
                        context.Response.Write(strjson);
                        break;
                    case "edit":
                        materialdal.Modify(new Model.Material()
                        {
                            Id = id,
                            Name = name,
                            Note = note,
                            UpdateDt = datetime,
                            UpdatePerson = emp.Name
                        });
                        json.Msg = "修改成功";
                        json.IsSuccess = true;
                        json.JsExecuteMethod = "ajax_AddAndEditMaterial";
                        strjson = _jss.Serialize(json);
                        context.Response.Write(strjson);
                        break;
                }

            }
            catch (Exception e)
            {
                json.Msg = "添加失败！";
                json.IsSuccess = false;
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
                Log.Error(e);
            }
        }

        /// <summary>
        /// 获取素材
        /// </summary>
        /// <param name="context"></param>
        private void GetMaterial(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Model.Material>();
            var materialdal = new MaterialDal();
            var id = context.Request.Params["id"];
            try
            {
                var dt = materialdal.FindById(id);
                var list = ConvertHelper<Model.Material>.ConvertToList(dt);
                json.IsSuccess = true;
                json.Rows = list;
                json.Msg = "获取成功";
                json.JsExecuteMethod = "ajax_GetMaterial";
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "获取失败";
            }

            strjson = _jss.Serialize(json);
            context.Response.Write(strjson);
        }

        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="context"></param>
        private void DelMaterial(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Model.Material>();
            var materialdal = new MaterialDal();
            var id = context.Request.Params["id"];
            try
            {
                materialdal.Del(new Model.Material(){Id = id});
                json.IsSuccess = true;
                json.Msg = "删除成功";
                json.JsExecuteMethod = "ajax_DelMaterial";
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "删除失败";
            }

            strjson = _jss.Serialize(json);
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