using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;
using HotelCheckIn_BackSystem.DataService.Common;

namespace HotelCheckIn_BackSystem.DataService.WebService.InternetGroup
{
    /// <summary>
    /// CheckNoService 的摘要说明
    /// </summary>
    public class CheckNoService : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger("CheckNoService");
        private static readonly InternetGroupBll CkinBll = new InternetGroupBll();
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
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
                //context.Response.Write("location.href='../../Login.aspx';");
                //context.Response.Write("{\"loginout\":true,\"msg\":\"登陆过期，请重新登陆！\"}");
                context.Response.Write("{\"total\":true,\"rows\":[],\"loginout\":true,\"msg\":\"登陆过期，请重新登陆！\"}");
            }
            else if (action.Equals("querycheckno"))
            {
                QueryCheckNo(context);
            }
            else if (action.Equals("addcheckno"))
            {
                AddCheckNo(context);
            }
            else if (action.Equals("expall"))
            {
                Expall(context);
            }
            else if (action.Equals("getyzmsl"))
            {
                GetYzmsl(context);
            }
            else if (action.Equals("checkgroup"))
            {
                CheckGroup(context);
            }
        }

        /// <summary>
        /// 获取可用验证码数量
        /// </summary>
        /// <param name="context"></param>
        private void GetYzmsl(HttpContext context)
        {
            string tgs = context.Request["tgs"];
            string xmmc = context.Request["xmmc"];
            const int machinecheck = 1;
            const int groupcheck = 1;
            try
            {
                DataTable dt = _checknobll.GetCheckIdNo(tgs, xmmc, machinecheck, groupcheck);
                var list = ConvertHelper<CheckNoInfo>.ConvertToList(dt);
                var pageO = new PageObject<CheckNoInfo> {total = list.Count, rows = list};
                var str = _jss.Serialize(pageO);
                context.Response.Write(str);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }

        /// <summary>
        /// 查询验证码
        /// </summary>
        /// <param name="context"></param>
        private void QueryCheckNo(HttpContext context)
        {
            //定义参数
            int page = 1, rows = 10, total = 0;
            string tgs, xmmc, sfyz,checkno;
            string ej = "";
            var begintime = new DateTime();
            var endtime = new DateTime();

            //接收分页参数
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
            checkno = context.Request["checkno"];
            tgs = context.Request["tgs"];
            xmmc = context.Request["xmmc"];
            sfyz = context.Request["sfyz"];
            string sj = context.Request["begintime"] ?? "";
            if (sj.Length > 0)
            {
                begintime = DateTime.Parse(sj);
            }
            ej = context.Request["endtime"] ?? "";
            if (ej.Length > 0)
            {
                endtime = DateTime.Parse(ej);
            }

            //查询处理
            try
            {
                var qnapj = new QueryNoAndPj();
                qnapj.CheckNo = checkno;
                qnapj.InternetGroupId = tgs;
                qnapj.ProjectFrontNum = xmmc;
                qnapj.CheckQuery = sfyz;
                qnapj.CreateTimeBegin = begintime;
                qnapj.CreateTimeEnd = endtime;
                List<CheckNoInfo> ckInfoList = _checknobll.QueryCheckNo(qnapj, page, rows, ref total);
                var pObject = new pageObject<CheckNoInfo> {total = total, rows = ckInfoList};
                context.Response.Write(_jss.Serialize(pObject));
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                context.Response.Write("{\"total\":0,\"rows\":[]}");
            }
        }

        /// <summary>
        /// 生成团购验证码
        /// </summary>
        /// <param name="context"></param>
        private void AddCheckNo(HttpContext context)
        {
            try
            {
                var checknoinfo = new CheckNoInfo();
                string tgs = context.Request["t_tgs"];
                checknoinfo.InternetGroupId = tgs;
                checknoinfo.InternetGroup = tgs;
                checknoinfo.CheckID_Front = context.Request["t_xmmc"];
                string beginsj = context.Request["t_begintime"] ?? "";
                if (beginsj.Length > 0)
                {
                    checknoinfo.CheckIdBeginTime = DateTime.Parse(beginsj);
                }
                string endsj = context.Request["t_endtime"] ?? "";
                if (endsj.Length > 0)
                {
                    checknoinfo.CheckIdEndTime = DateTime.Parse(endsj);
                }
                checknoinfo.InSumDate = int.Parse(context.Request["t_rzts"]);
                if (null != context.Session[Constant.LoginUser])
                {
                    checknoinfo.CreatePeople =
                        (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                }
                checknoinfo.MachineCheck = 1;
                checknoinfo.InternetCheck = 1; //1(未验证)，2(已验证)，默认1
                int scsl = int.Parse(context.Request["t_scsl"]);
                int reint = 0;
                for (int i = 0; i < scsl; i++)
                {
                    //var a = new Random(unchecked((int)DateTime.Now.Ticks));
                    //int randKey = a.Next(100000000, 999999999);
                    var sb = new StringBuilder();
                    char[] chars = "0123456789".ToCharArray();
                    int length = RNG.Next(9, 9);
                    for (int j = 0; j < length; j++)
                    {
                        sb.Append(chars[RNG.Next(chars.Length - 1)]);
                    }
                    string randKey = sb.ToString();
                    checknoinfo.CheckId = checknoinfo.CheckID_Front + randKey;
                    DataTable dt = _checknobll.QueryCheckId(checknoinfo.CheckID_Front + randKey);
                    if (dt.Rows.Count > 0)
                    {
                        i = i - 1;
                        reint = reint + 1;
                        continue;
                    }
                    
                    _checknobll.AddCheckNo(checknoinfo);
                }
                context.Response.Write("{\"success\":true,\"msg\":\"保存成功！\"}");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                context.Response.Write("{\"success\":true,\"msg\":\"保存失败！\"}");
            }
        }

        /// <summary>
        /// 导出验证码
        /// </summary>
        /// <param name="context"></param>
        private void Expall(HttpContext context)
        {
            string tgs = context.Request["tgs"];
            string xmmc = context.Request["xmmc"];
            string dcyzmsl = context.Request["dcyzmsl"];
            const int machinecheck = 1;
            const int groupcheck = 1;
            try
            {
                var expCheckNoString = _checknobll.ExpAll(tgs, xmmc, machinecheck, groupcheck, dcyzmsl);
                context.Response.ContentType = "application/x-zip-compressed";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" +
                HttpUtility.UrlEncode("验证码数据.xls", System.Text.Encoding.UTF8));
                context.Response.TransmitFile(expCheckNoString);
                context.Response.Write("{\"success\":true,\"msg\":\"成功导出" + dcyzmsl + "条验证码！\"}");
            }
            catch (Exception e)
            {
                Log.Error("传递数据格式化出错：" + e.Message);
                context.Response.ContentType = "application/x-text";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" +
                    HttpUtility.UrlEncode("错误.txt", System.Text.Encoding.UTF8));
                context.Response.Write(e.Message);
            }
        }

        /// <summary>
        /// 对验证码进行团购验证
        /// </summary>
        /// <param name="context"></param>
        private void CheckGroup(HttpContext context)
        {
            try
            {
                var checkno = new CheckNoInfo();
                string checkid = context.Request["checkid"];
                checkno.InternetCheck = 2;
                checkno.CheckId = checkid;
                if (null != context.Session[Constant.LoginUser])
                {
                    checkno.InternetCheckPeople =
                        (context.Session[Constant.LoginUser] as Employer ?? new Employer()).Name;
                }
                _checknobll.EditCheckNoIntnetCheck(checkno);
                context.Response.Write("{\"success\":true,\"msg\":\"验证成功！\"}");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                context.Response.Write("{\"success\":false,\"msg\":\"验证失败！\"}");
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