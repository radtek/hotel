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
    /// IoJournalService 的摘要说明
    /// </summary>
    public class IoJournalService : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger("IoJournalService");
        private static readonly IoJournalBll IoBll = new IoJournalBll();
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
            else if (action.Equals("queryio"))
            {
                 QueryIo(context);
            }
            else if (action.Equals("addio"))
            {
                AddIo(context);
            }
            else if (action.Equals("delio"))
            {
                DelIo(context);
                
            }
            
        }
        /// <summary>
        /// 查询收支记录
        /// </summary>
        /// <param name="context"></param>
        private void QueryIo(HttpContext context)
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
            //接收参数
            int querysign = int.Parse(context.Request["querysign"]);
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
                var bean = new IoJournal {BeginTime = begintime, EndTime = endtime,QuerySign = querysign};
                List<IoJournal> ioList = IoBll.QueryIojournal(bean, page, rows, ref total);
                var pageO = new PageObject<IoJournal> {total = total, rows = ioList};
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
        /// 添加收支记录
        /// </summary>
        /// <param name="context"></param>
        private static void AddIo(HttpContext context)
        {
            try
            {
                decimal szje = decimal.Parse(context.Request["t_szje"]);
                int szbz = int.Parse(context.Request["t_szbz"]);
                const int szly = 3;
                string name = "", id = "";
                if (null != context.Session[Constant.LoginUser])
                {
                    name = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                    id = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Id;
                }
                const int isuse = 1;
                const int inOrOutCard = 1;
                var iobean = new IoJournal
                    {
                        IoFrom = szly,
                        IoId = id,
                        IoName = name,
                        IoMoney = szje,
                        IoTag = szbz,
                        IsUse = isuse,
                        InOrOutCard =inOrOutCard,
                        IoTime = DateTime.Now
                    };
                IoBll.AddIoJournal(iobean);
                context.Response.Write("{\"success\":true,\"msg\":\"保存成功！\"}");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                context.Response.Write("{\"success\":false,\"msg\":\"保存失败！\"}");
            }
        }
        /// <summary>
        /// 作废收支记录
        /// </summary>
        /// <param name="context"></param>
        private static void DelIo(HttpContext context)
        {
            try
            {
                string name = "";
                if (null != context.Session[Constant.LoginUser])
                {
                    name = (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                }
                DataTable dt = SeBll.QueryNewSettleTime();
                var settleEndTime = new DateTime();
                if (dt.Rows.Count > 0)
                {
                    settleEndTime = DateTime.Parse(dt.Rows[0]["EndTime"].ToString());
                }
                var ioname = context.Request["ioname"];
                var iotime = DateTime.Parse(context.Request["iotime"]);
                var ioid = context.Request["ioid"];
                const int isuse = 2;
                if (ioname != name)
                {
                    context.Response.Write("{\"success\":true,\"msg\":\"操作人和收支人需一致！\"}");
                    return;
                }
                if (iotime > settleEndTime)
                {
                    context.Response.Write("{\"success\":true,\"msg\":\"收支时间应该小于等于最后一次结算时间！\"}");
                    return;
                }
                var iobean = new IoJournal() {IoTime = iotime, IoId = ioid, IsUse = isuse};
                IoBll.DelIoJournal(iobean);
                context.Response.Write("{\"success\":true,\"msg\":\"操作成功！\"}");
            }
            catch (Exception e)
            {
                Log.Debug(e);
                context.Response.Write("{\"success\":false,\"msg\":\"操作失败！\"}");
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