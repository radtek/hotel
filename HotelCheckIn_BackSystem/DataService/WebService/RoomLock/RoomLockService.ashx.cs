using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Model;
using NPOI.HSSF.Record.Formula.Functions;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.WebService.RoomLock
{
    /// <summary>
    /// RoomLockService 的摘要说明
    /// </summary>
    public class RoomLockService : IHttpHandler, IRequiresSessionState
    {
        private static ILog _log = log4net.LogManager.GetLogger("RoomLockService");
        private static readonly RoomLockBll Rkbll = new RoomLockBll();
        JavaScriptSerializer _jss = new JavaScriptSerializer();
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
                //context.Response.Write("location.href='../../Login.aspx';");
                context.Response.Write("{\"total\":true,\"rows\":[],\"loginout\":true,\"msg\":\"登陆过期，请重新登陆！\"}");
            }
            else if (action.Equals("queryroomlock"))
            {
                QueryRoomLock(context);
            }
            else if(action.Equals("unlock"))
            {
                Unlock(context);
            }
        }
        /// <summary>
        /// 查询锁定的房间
        /// </summary>
        /// <param name="context"></param>
        private void QueryRoomLock(HttpContext context)
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

            var begintime = new DateTime();
            var endtime = new DateTime();
            string sj = context.Request["begintime"] ?? "";
            if (sj.Length > 0)
            {
                begintime = DateTime.Parse(sj);
            }
            string ej = context.Request["endtime"] ?? "";
            if (ej.Length > 0)
            {
                endtime = DateTime.Parse(ej);
            }
            try
            {
                var roomlock = new RoomLockInfo();
                roomlock.BeginTime = begintime;
                roomlock.EndTime = endtime;
                List<RoomLockInfo> ckInfoList = Rkbll.QueryRoomLock(roomlock, page, rows, ref total);
                var pObject = new pageObject<RoomLockInfo> {total = total, rows = ckInfoList};
                context.Response.Write(_jss.Serialize(pObject));
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
                context.Response.Write("{\"total\":0,\"rows\":[]}");
            }
        }
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="context"></param>
        private static void Unlock(HttpContext context)
        {
            string checkid = context.Request["checkid"];
            bool b = Rkbll.Unlock(checkid);
            context.Response.Write(b == true ? "{\"success\":true,\"msg\":\"解锁成功！\"}" : "{\"success\":true,\"msg\":\"验证码已使用！\"}");
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