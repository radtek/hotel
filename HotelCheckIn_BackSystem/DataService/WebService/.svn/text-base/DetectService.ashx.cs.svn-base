using System;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using HotelCheckIn_BackSystem.HumanIdentify;

namespace HotelCheckIn_BackSystem.DataService.WebService
{
    /// <summary>
    /// Summary description for DetectService
    /// </summary>
    public class DetectService : IHttpHandler, IRequiresSessionState
    {
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
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
        /// 获取数据
        /// </summary>
        /// <param name="httpContext"></param>
        public void GetFirstData(HttpContext httpContext)
        {
            var detectDal = new DetectDal();
            var zwJson = new ZwJson<Detect>();

            try
            {
                var data = detectDal.GetFirstData();
                zwJson.IsSuccess = true;
                zwJson.JsExecuteMethod = "ajax_GetFirstData";
                zwJson.Data = data;
                var json = _jss.Serialize(zwJson);
                httpContext.Response.Write(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="httpContext"></param>
        public void GetPicture(HttpContext httpContext)
        {
            var detectDal = new DetectDal();
            var id = httpContext.Request.Params["id"];
            var type = httpContext.Request.Params["type"];
            try
            {
                var data = detectDal.GetPictureData(id, type);
                switch (type)
                {
                    case "IdCardImg":
                        if (data[0].IdCardImg!=null)
                        {
                            httpContext.Response.BinaryWrite(data[0].IdCardImg);
                        }
                       
                        break;
                    case "Camera1":
                        if (data[0].Camera1!=null)
                        {
                            httpContext.Response.BinaryWrite(data[0].Camera1);
                        }
                        
                        break;
                    case "Camera2":
                        if (data[0].Camera2 != null)
                        {
                            httpContext.Response.BinaryWrite(data[0].Camera2);
                        }
                        break;
                    case "Camera3":
                        if (data[0].Camera3 != null)
                        {
                            httpContext.Response.BinaryWrite(data[0].Camera3);
                        }
                        break;
                    default:break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新三个字段
        /// </summary>
        /// <param name="httpContext"></param>
        public void Update(HttpContext httpContext)
        {
            var detectDal = new DetectDal();
            var zwjson = new ZwJson<object>();
            var id = httpContext.Request.Params["id"];
            var sfyz = httpContext.Request.Params["sftg"];
           
            var name = "";
            if (null != httpContext.Session["user"])
            {
                name = (httpContext.Session["user"] as Employer).Name;
            }
            else
            {
                zwjson.IsSuccess = false;
                zwjson.Msg = "请重新登录系统！";
                var json1 = _jss.Serialize(zwjson);
                httpContext.Response.Write(json1);
                return;
            }
            var detect = new Detect
            {
                Id = id,
                Status = int.Parse(sfyz),//是否验证
                Operator = name,//操作人
                UpdateDt = DateTime.Now//验证时间
            };
            try
            {
                detectDal.Update(detect);
                zwjson.IsSuccess = true;
                zwjson.JsExecuteMethod = "ajax_Update";
                zwjson.Msg = "更新成功";
            }
            catch (Exception)
            {
                zwjson.IsSuccess = false;
                zwjson.Msg = "更新失败";
                throw;
            }
            var json = _jss.Serialize(zwjson);
            httpContext.Response.Write(json);
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