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

namespace HotelCheckIn_PlatformSystem.DataService.WebService.Hotel
{
    /// <summary>
    /// Summary description for HotelService
    /// </summary>
    public class HotelService : IHttpHandler,IRequiresSessionState
    {

        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
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
        /// 初始化酒店表
        /// </summary>
        /// <param name="context"></param>
        private void InitHotelGrid(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<InitHotelGrid>();
            var hotelsdal = new HotelsDal();
            var datapage = new DataPage();
            var page = context.Request.Params["page"];
            var rows = context.Request.Params["rows"];

            if (!string.IsNullOrEmpty(page))
                datapage.Index = int.Parse(page);
            if (!string.IsNullOrEmpty(rows))
                datapage.Count = int.Parse(rows);

            var areaid = context.Request.Params["qyid"];//区域id

            try
            {
                Dictionary<int, DataTable> dic = hotelsdal.FindByHotelsByAreaId(areaid, datapage);
                foreach (var dataTable in dic)
                {
                    var list = ConvertHelper<InitHotelGrid>.ConvertToList(dataTable.Value);
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
        /// 添加和修改酒店数据
        /// </summary>
        /// <param name="context"></param>
        private void AddAndEditHotel(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<string>();
            var hotelsdal = new HotelsDal();

            var flag = context.Request.Params["flag"];
            var hotelid = context.Request.Params["hotelid"];
            var areaid = context.Request.Params["selAreas2"];
            var name = context.Request.Params["txt_name"];
            var address = context.Request.Params["txt_address"];
            var tel = context.Request.Params["txt_tel"];
            var contact = context.Request.Params["txt_contact"];
            var note = context.Request.Params["txt_note"];
            try
            {
                var emp = (context.Session[Constant.LoginUser]) as Employer;
                switch (flag)
                {
                    case "add":

                        if (emp != null)
                            hotelsdal.Add(new Model.Hotels()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = name,
                                    Address = address,
                                    Tel = tel,
                                    Contact = contact,
                                    AreaId = areaid,
                                    Note = note,
                                    CreateDt = DateTime.Now,
                                    CreatePerson = emp.Name,
                                    UpdateDt = DateTime.Now,
                                    UpdatePerson = emp.Name
                                });
                        json.Msg = "添加成功";
                        json.IsSuccess = true;
                        break;
                    case "edit":
                        if (emp != null)
                            hotelsdal.Modify(new Model.Hotels()
                                {
                                    Id = hotelid,
                                    Name = name,
                                    Address = address,
                                    Tel = tel,
                                    Contact = contact,
                                    AreaId = areaid,
                                    Note = note,
                                    UpdateDt = DateTime.Now,
                                    UpdatePerson = emp.Name
                                });
                        json.Msg = "修改成功";
                        json.IsSuccess = true;
                       
                        break;
                }
                json.JsExecuteMethod = "ajax_AddAndEditHotel";
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
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
        /// 获取酒店数据
        /// </summary>
        /// <param name="context"></param>
        private void GetHotels(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Model.Hotels>();
            var hotelsdal = new HotelsDal();

            try
            {
                var hotelid = context.Request.Params["hotelid"];
                var dt = hotelsdal.FindByHotels(hotelid);
                var list = ConvertHelper<Model.Hotels>.ConvertToList(dt);
                json.Rows = list;
                json.IsSuccess = true;
                json.Msg = "获取成功";
                json.JsExecuteMethod = "ajax_GetHotels";
            }
            catch (Exception e)
            {
                json.Msg = "获取失败";
                json.IsSuccess = false;
                Log.Error(e);
            }
            strjson = _jss.Serialize(json);
            context.Response.Write(strjson);
        }

        /// <summary>
        /// 删除酒店数据
        /// </summary>
        /// <param name="context"></param>
        private void DelHotel(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Model.Hotels>();
            var hotelsdal = new HotelsDal();

            try
            {
                var hotelid = context.Request.Params["hotelid"];
                hotelsdal.Del(new Model.Hotels(){Id = hotelid});
                json.IsSuccess = true;
                json.Msg = "删除成功";
                json.JsExecuteMethod = "ajax_DelHotel";
            }
            catch (Exception e)
            {
                json.Msg = "删除失败";
                json.IsSuccess = false;
                Log.Error(e);
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