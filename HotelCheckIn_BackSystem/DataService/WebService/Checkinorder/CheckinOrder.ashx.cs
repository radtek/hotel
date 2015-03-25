using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;
using System.Data;

namespace HotelCheckIn_BackSystem.DataService.WebService.Checkinorder
{
    /// <summary>
    /// checkinOrder 的摘要说明
    /// </summary>
    //[WebService(Namespace = "http://www.youotech.com/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CheckinOrder : IHttpHandler, IRequiresSessionState
    {
        private static ILog log = log4net.LogManager.GetLogger("CheckinOrder");
        private static readonly CheckinBll CkinBll = new CheckinBll();
        private string basePath = System.Configuration.ConfigurationSettings.AppSettings["basePath"];
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                return;
            }
            action = action.ToLower();
            var jss = new JavaScriptSerializer();

            //session过期验证
            if (context.Session[Constant.LoginUser] == null)
            {
                action = "loginout";
            }

            if (action.Equals("loginout"))
            {
                context.Response.Write("location.href='../../Login.aspx';");
            }
            else if (action.Equals("getdata"))
            {
                GetData(context, jss);
            }
            else if (action.Equals("getcustomer"))
            {
                GetCustomer(context, jss);
            }
            else if (action.Equals("getcustomeridimg"))
            {
                GetCustomerIdImg(context);
            }
            else if (action.Equals("getcustomercaneraimg"))
            {
                GetCustomerCanmeraImg(context);
            }
            else if (action.Equals("expidcardonly"))
            {
                ExpIDCardOnly(context);
            }
            else if (action.Equals("expall"))
            {
                ExpAll(context);
            }
            else if (action.Equals("getcheckinimage"))
            {
                GetCheckinImage(context);
            }
            else if (action.Equals("gettime"))
            {
                context.Response.Write(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "#" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else if (action.Equals("getmachine"))
            {
                GetMachine(context);
            }
            else
            {
                log.Error("action值:" + action + "不存在！");
                context.Response.Write("{\"msg\":\"action值错误。\"}");
                return;
            }
        }

        /// <summary>
        /// 获取终端数据
        /// </summary>
        /// <param name="context"></param>
        private static void GetMachine(HttpContext context)
        {
            try
            {
                var bean = new MachineInfo();
                bean.Isdisabled = 1;
                DataTable ds = CkinBll.Query(bean);
                string str = JsonHelper.ToJson.DataTableToJson("rows", ds);
                context.Response.Write(str);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                context.Response.Write("0");
            }
        }



        /// <summary>
        /// 获取入住订单信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jss"></param>
        private static void GetData(HttpContext context, JavaScriptSerializer jss)
        {
            //接收参数
            int page = 1, rows = 10, notDownload = 0, total = 0;
            string macId, rzlx = null;
            DateTime dateBegin = DateTime.Now.AddDays(-7), dateEnd = DateTime.Now;
            try
            {
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
                macId = context.Request["macid"];
                rzlx = context.Request["checkintype"].ToString();
                tmp = context.Request["datebegin"].ToString();
                dateBegin = tmp == "" ? DateTime.Now.AddDays(-7) : DateTime.Parse(tmp);
                tmp = context.Request["dateend"].ToString();
                dateEnd = tmp == "" ? DateTime.Now : DateTime.Parse(tmp);
                notDownload = int.Parse(context.Request["notdownload"]);
            }
            catch (Exception e)
            {
                log.Error("传递数据格式化出错：" + e.Message);
                context.Response.Write("{\"total\":0,\"rows\":[]}");
                return;
            }
            //查询处理
            try
            {
                CheckinQuery ckInfo = new CheckinQuery();
                ckInfo.MacId = macId;
                ckInfo.CheckinType = rzlx;
                ckInfo.CheckinTimeBegin = dateBegin;
                ckInfo.CheckinTimeEnd = dateEnd;
                ckInfo.ExportCount = notDownload;
                List<CheckinInfo> ckInfoList = CkinBll.Query(ckInfo, page, rows, ref total);
                var pObject = new pageObject<CheckinInfo>();
                pObject.total = total;
                pObject.rows = ckInfoList;
                context.Response.Write(jss.Serialize(pObject));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                context.Response.Write("{\"total\":0,\"rows\":[]}");
            }
        }

        /// <summary>
        /// 获取客人信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jss"></param>
        private static void GetCustomer(HttpContext context, JavaScriptSerializer jss)
        {
            try
            {
                string orderid = context.Request["orderid"].ToString();
                CustomerInfo cInfo = new CustomerInfo();
                cInfo.OrderId = orderid;
                List<CustomerInfo> cList = CkinBll.Query(cInfo);
                pageObject<CustomerInfo> pObject = new pageObject<CustomerInfo>(cList.Count, cList);
                context.Response.Write(jss.Serialize(pObject));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                context.Response.Write("{\"total\":0,\"rows\":[]}");
            }
        }

        /// <summary>
        /// 获取客人身份证图片
        /// </summary>
        /// <param name="context"></param>
        private void GetCustomerIdImg(HttpContext context)
        {
            try
            {
                CustomerInfo ctInfo = new CustomerInfo();
                ctInfo.Id = context.Request["id"];
                ctInfo = CkinBll.Query(ctInfo)[0];
                context.Response.BinaryWrite(File.ReadAllBytes(basePath + ctInfo.IdentityCardPhoto));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        /// <summary>
        /// 获取客人摄像头图片
        /// </summary>
        /// <param name="context"></param>
        private void GetCustomerCanmeraImg(HttpContext context)
        {
            try
            {
                CustomerInfo ctInfo = new CustomerInfo();
                ctInfo.Id = context.Request["id"];
                ctInfo = CkinBll.Query(ctInfo)[0];
                context.Response.BinaryWrite(File.ReadAllBytes(basePath + ctInfo.CameraPhoto));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        /// <summary>
        /// 导出身份证图片
        /// </summary>
        /// <param name="context"></param>
        private static void ExpIDCardOnly(HttpContext context)
        {
            int page = 1, rows = 10000, notDownload = 0;
            string macId = null;
            DateTime dateBegin = DateTime.Now.AddDays(-7), dateEnd = DateTime.Now;
            try
            {
                macId = context.Request["macid"];
                string tmp = context.Request["datebegin"].ToString();
                dateBegin = DateTime.Parse(tmp == "" ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : tmp);
                tmp = context.Request["dateend"].ToString();
                dateEnd = DateTime.Parse(tmp == "" ? DateTime.Now.ToString("yyyy-MM-dd") : tmp);
                notDownload = int.Parse(context.Request["notdownload"]);
            }
            catch (Exception e)
            {
                log.Error("传递数据格式化出错：" + e.Message);
                context.Response.Write("{\"sucess\":false,\"msg\":\"传递数据格式化出错。\"}");
                return;
            }
            var ckinInfo = new CheckinQuery
                {
                    MacId = macId,
                    CheckinTimeBegin = dateBegin,
                    CheckinTimeEnd = dateEnd,
                    ExportCount = notDownload
                };
            try
            {
                var expidstring = CkinBll.expIDCardImg(ckinInfo, page, rows);
                context.Response.ContentType = "application/x-zip-compressed";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" +
                    HttpUtility.UrlEncode("身份证.zip", System.Text.Encoding.UTF8));
                context.Response.TransmitFile(expidstring);
            }
            catch (Exception e)
            {
                log.Error("传递数据格式化出错：" + e.Message);
                context.Response.ContentType = "application/x-text";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" +
                    HttpUtility.UrlEncode("错误.txt", System.Text.Encoding.UTF8));
                context.Response.Write(e.Message);
            }
        }

        /// <summary>
        /// 导出所有数据
        /// </summary>
        /// <param name="context"></param>
        private static void ExpAll(HttpContext context)
        {
            int page = 1, rows = 10000, notDownload = 0;
            string macId = null;
            DateTime dateBegin = DateTime.Now.AddDays(-7), dateEnd = DateTime.Now;
            try
            {
                macId = context.Request["macid"];
                string tmp = context.Request["datebegin"].ToString();
                dateBegin = DateTime.Parse(tmp == "" ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : tmp);
                tmp = context.Request["dateend"].ToString();
                dateEnd = DateTime.Parse(tmp == "" ? DateTime.Now.ToString("yyyy-MM-dd") : tmp);
                notDownload = int.Parse(context.Request["notdownload"]);
            }
            catch (Exception e)
            {
                log.Error("传递数据格式化出错：" + e.Message);
                context.Response.Write("{\"sucess\":false,\"msg\":\"传递数据格式化出错。\"}");
                return;
            }
            var ckinInfo = new CheckinQuery
                {
                    MacId = macId,
                    CheckinTimeBegin = dateBegin,
                    CheckinTimeEnd = dateEnd,
                    ExportCount = notDownload
                };
            try
            {
                var expallstring = CkinBll.ExpAllImg(ckinInfo, page, rows);
                context.Response.ContentType = "application/x-zip-compressed";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" +
                    HttpUtility.UrlEncode("所有图片和订单数据.zip", System.Text.Encoding.UTF8));
                context.Response.TransmitFile(expallstring);
            }
            catch (Exception e)
            {
                log.Error("传递数据格式化出错：" + e.Message);
                context.Response.ContentType = "application/x-text";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" +
                    HttpUtility.UrlEncode("错误.txt", System.Text.Encoding.UTF8));
                context.Response.Write(e.Message);
            }
        }

        /// <summary>
        /// 获取入住签字图片
        /// </summary>
        /// <param name="context"></param>
        private void GetCheckinImage(HttpContext context)
        {
            try
            {
                string orderid = context.Request["orderid"];
                CheckinInfo ckinInfo = new CheckinInfo();
                ckinInfo.OrderId = orderid;
                CheckinInfo checkin = CkinBll.GetModule(ckinInfo);
                context.Response.BinaryWrite(File.ReadAllBytes(basePath + checkin.CheckinImage));
            }
            catch (Exception e)
            {
                log.Error(e.Message);
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
