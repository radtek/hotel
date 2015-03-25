using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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
    /// SettleService 的摘要说明
    /// </summary>
    public class SettleService : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger("SettleService");
        private static readonly SettleBll SeBll = new SettleBll();
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
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
            else if (action.Equals("querysettle"))
            {
                QuerySettle(context);
            }
            else if (action.Equals("querynewtime"))
            {
                QueryNewTime(context);
            }
            else if (action.Equals("settleio"))
            {
                SettleIo(context);
            }
            else if (action.Equals("addsettle"))
            {
                AddSettle(context);
            }
            else if (action.Equals("getrate"))
            {
                String rate = XmlHelper.ReadNode("room_rate");
                context.Response.Write(rate + "#" + rate);
            }
            else if (action.Equals("editrate"))
            {
                EditRate(context);
            }
        }

        /// <summary>
        /// 修改房价
        /// </summary>
        /// <param name="context"></param>
        private static void EditRate(HttpContext context)
        {
            try
            {
                //接收参数
                string rate = context.Request["rate"];
                XmlHelper.WriteNode("room_rate", rate.ToString());
                context.Response.Write("{\"success\":true,\"msg\":\"保存成功！\"}");
            }
            catch (Exception e)
            {
                context.Response.Write("{\"success\":false,\"msg\":\"保存失败！\"}");
            }
        }

        /// <summary>
        /// 查询结算记录
        /// </summary>
        /// <param name="context"></param>
        private void QuerySettle(HttpContext context)
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
            //接收查询参数
            var begintime = new DateTime();
            var endtime = new DateTime();
            string sj = context.Request["qbegintime"] ?? "";
            if (sj.Length > 0)
            {
                begintime = DateTime.Parse(sj);
            }
            string ej = context.Request["qendtime"] ?? "";
            if (ej.Length > 0)
            {
                endtime = DateTime.Parse(ej);
            }
            try
            {
                var bean = new Settle { QBeginTime = begintime, QEndTime = endtime };
                List<Settle> seList = SeBll.QuerySettleList(bean, page, rows, ref total);
                var pageO = new PageObject<Settle> { total = total, rows = seList };
                var str = _jss.Serialize(pageO);
                context.Response.Write(str);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                ;
            }
        }
        /// <summary>
        /// 查询最近的结算记录结束时间
        /// </summary>
        /// <param name="context"></param>
        private void QueryNewTime(HttpContext context)
        {
            DataTable dt = SeBll.QueryNewSettleTime();
            var list = ConvertHelper<Settle>.ConvertToList(dt);
            var pageO = new PageObject<Settle> { total = list.Count, rows = list };
            var str = _jss.Serialize(pageO);
            context.Response.Write(str);
        }
        /// <summary>
        /// 结算收支记录
        /// </summary>
        /// <param name="context"></param>
        private void SettleIo(HttpContext context)
        {
            //接收参数
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
                var bean = new IoJournal { BeginTime = begintime, EndTime = endtime };
                DataTable dt = SeBll.QueryIojournal(bean);
                var list = ConvertHelper<IoJournal>.ConvertToList(dt);
                var pageO = new PageObject<IoJournal> { total = list.Count, rows = list };
                var str = _jss.Serialize(pageO);
                context.Response.Write(str);
            }
            catch (Exception e)
            {
                Log.Debug(e);
                ;
            }
        }
        /// <summary>
        /// 添加结算记录
        /// </summary>
        /// <param name="context"></param>
        private static void AddSettle(HttpContext context)
        {
            //接收参数
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
                var bean = new IoJournal { BeginTime = begintime, EndTime = endtime };
                DataTable dt = SeBll.QueryIojournal(bean);
                var inmoney = 0;
                var outmoney = 0;
                var summoney = 0;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (int.Parse(dt.Rows[i]["IsUse"].ToString()) == 1)
                        {
                            if (int.Parse(dt.Rows[i]["IoTag"].ToString()) == 1)
                            {
                                inmoney = inmoney + int.Parse(dt.Rows[i]["IoMoney"].ToString());
                            }
                            else if (int.Parse(dt.Rows[i]["IoTag"].ToString()) == 2)
                            {
                                outmoney = outmoney + int.Parse(dt.Rows[i]["IoMoney"].ToString());
                            }
                        }
                    }
                    if (inmoney > outmoney)
                    {
                        summoney = inmoney - outmoney;
                    }
                    else if (inmoney < outmoney)
                    {
                        summoney = outmoney - inmoney;
                    }
                }
                string name = "", id = "";
                if (null != context.Session[Constant.LoginUser])
                {
                    name = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                    id = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Id;
                }
                var settle = new Settle();
                settle.InMoney = inmoney;
                settle.OutMoney = outmoney;
                settle.SumMoney = summoney;
                settle.OptId = id;
                settle.OptName = name;
                settle.BeginTime = begintime;
                settle.EndTime = endtime;
                SeBll.AddSettle(settle);
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