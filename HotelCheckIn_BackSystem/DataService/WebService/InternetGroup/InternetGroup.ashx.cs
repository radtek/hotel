using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.WebService.InternetGroup
{
    /// <summary>
    /// InternetGroup 的摘要说明
    /// </summary>
    public class InternetGroup : IHttpHandler, IRequiresSessionState
    {
        private static ILog Log = log4net.LogManager.GetLogger("CheckinOrder");
        private static readonly InternetGroupBll CkinBll = new InternetGroupBll();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        readonly InternetGroupBll _igroupBll = new InternetGroupBll();
        readonly CheckNoBll _checknobll = new CheckNoBll();
        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                return;
            }
            action = action.ToLower();

            //session过期验证
            if (context.Session[Constant.LoginUser] == null)
            {
                action = "loginout";
            }

            if (action.Equals("loginout"))
            {
                context.Response.Write("location.href='../../Login.aspx';");
            }
            else if (action.Equals("addproject"))
            {
                AddProject(context);
            }
            else if (action.Equals("delproject"))
            {
                DelProject(context);
            }
            else if (action.Equals("editproject"))
            {
                EditProject(context);
            }
            else if(action.Equals("queryproject"))
            {
                QueryProject(context);
            }
            else if(action.Equals("queryroomquality"))
            {
                QueryRoomQuality(context);
            }
        }
        /// <summary>
        /// 删除团购项目
        /// </summary>
        /// <param name="context"></param>
        private void DelProject(HttpContext context)
        {
            try
            {
                string qzm = context.Request["qzm"];
                string tgsid = context.Request["tgsid"];
                DataTable dt = _checknobll.QueryNoByFront(qzm);
                if (dt.Rows.Count > 0)
                {
                    context.Response.Write("{\"success\":true,\"msg\":\"前缀码已被使用，不能删除！\"}");
                    return;
                }
                _igroupBll.DelProject(qzm, tgsid);
                context.Response.Write("{\"success\":true,\"msg\":\"删除成功！\"}");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                context.Response.Write("{\"success\":false,\"msg\":\"删除失败！\"}");
            }
        }
        /// <summary>
        /// 修改团购项目
        /// </summary>
        /// <param name="context"></param>
        private void EditProject(HttpContext context)
        {
            try
            {
                var bean = new InternetGroupInfo();
                string hotelid = context.Request["hotel"];
                string tgsid = context.Request["htgs"];
                string qzm = context.Request["hqzm"];
                bean.ProjectFrontNum = qzm;
                bean.InternetGroupId = tgsid;
                string projecmc = context.Request["projecmc"];
                string roomarr = context.Request["roomtype"];
                string roomtype = roomarr.Split('#')[0];
                string roomtypemc = roomarr.Split('#')[1];
                string bz = context.Request["bz"];
                float rate = float.Parse(context.Request["rate"].ToString());
                string ratecode = context.Request["rateCode"];
                string name = "";
                if (null != context.Session[Constant.LoginUser])
                {
                    name = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                }
                bean.HotelId = hotelid;
                bean.ProjectName = projecmc;
                bean.RoomTypeId = roomtype;
                bean.RoomTypeName = roomtypemc;
                bean.Remarks = bz;
                bean.Updater = name;
                bean.Rate = rate;
                bean.RateCode = ratecode;
                _igroupBll.EditProject(bean);
                context.Response.Write("{\"success\":true,\"msg\":\"保存成功！\"}");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                context.Response.Write("{\"success\":false,\"msg\":\"保存失败！\"}");
            }
        }
        /// <summary>
        /// 查询团购项目
        /// </summary>
        /// <param name="context"></param>
        private void QueryProject(HttpContext context)
        {
            try
            {
                //接收分页参数
                int page = 1, rows = 10, total = 0;
                string tmp = context.Request["page"];
                if (!string.IsNullOrEmpty(tmp))
                {
                    page = int.Parse(tmp);
                }
                tmp = context.Request["rows"];
                if (!string.IsNullOrEmpty(tmp))
                {
                    rows = int.Parse(tmp);
                }
                var bean = new InternetGroupInfo();
                string tgsid = context.Request["tgs"];
                bean.InternetGroupId = tgsid;
                List<InternetGroupInfo> iglist = _igroupBll.QueryProjectList(bean, page, rows, ref total);
                var pageO = new PageObject<InternetGroupInfo> {total = total, rows = iglist};
                var str = jss.Serialize(pageO);
                context.Response.Write(str);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                ;
            }
        }

        /// <summary>
        /// 查询房间属性
        /// </summary>
        /// <param name="context"></param>
        private void QueryRoomQuality(HttpContext context)
        {
            try
            {
                var bean = new RoomQuality {TypeId = context.Request["typeid"]};
                DataTable dt = _igroupBll.QueryRoomQuality(bean);
                var list = ConvertHelper<RoomQuality>.ConvertToList(dt);
                var pageO = new PageObject<RoomQuality> {total = list.Count, rows = list};
                var str = jss.Serialize(pageO);
                context.Response.Write(str);
            }
            catch (Exception e)
            {
                Log.Debug(e);;
            }
        }

        /// <summary>
        /// 添加团购项目
        /// </summary>
        /// <param name="context"></param>
        private void AddProject(HttpContext context)
        {
            try
            {
                var bean = new InternetGroupInfo();
                string hotelid = context.Request["hotel"];
                string tgs = context.Request["tgs"];
                string qzm = context.Request["qzm"];
                bean.ProjectFrontNum = qzm;
                bean.InternetGroupId = tgs;
                if (_igroupBll.QueryProject(bean).Rows.Count > 0)
                {
                    context.Response.Write("{\"success\":false,\"msg\":\"前缀码已存在！\"}");
                    return;
                }
                string projecmc = context.Request["projecmc"];
                string roomarr = context.Request["roomtype"];
                string roomtype = roomarr.Split('#')[0];
                string roomtypemc = roomarr.Split('#')[1];
                string bz = context.Request["bz"];
                float rate = float.Parse(context.Request["rate"].ToString());
                string ratecode = context.Request["rateCode"];
                string name = "";
                if (null != context.Session[Constant.LoginUser])
                {
                    name = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                }
                bean.HotelId = hotelid;
                bean.InternetGroup = tgs;
                bean.ProjectName = projecmc;
                bean.RoomTypeId = roomtype;
                bean.RoomTypeName = roomtypemc;
                bean.Remarks = bz;
                bean.Rate = rate;
                bean.RateCode = ratecode;
                bean.Creater = name;
                _igroupBll.AddProject(bean);
                context.Response.Write("{\"success\":true,\"msg\":\"保存成功！\"}");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                context.Response.Write("{\"success\":false,\"msg\":\"保存失败！\"}");
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