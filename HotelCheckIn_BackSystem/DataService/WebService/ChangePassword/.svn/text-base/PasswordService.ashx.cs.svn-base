using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.WebService.ChangePassword
{
    /// <summary>
    /// PasswordService 的摘要说明
    /// </summary>
    public class PasswordService : IHttpHandler, IRequiresSessionState
    {
        protected log4net.ILog Log = log4net.LogManager.GetLogger("PasswordService");
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["action"];
            var str = "";

            switch (action)
            {
                case "modifypass":
                    str = ModifyPassword(context);
                    break;
                case "login":
                    str = Login(context);
                    break;
                case "login1":
                    str = Login1(context);
                    break;
            }
            context.Response.Write(str);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string Login(HttpContext context)
        {
            string WorkNum = context.Request["name"] ?? "";
            string pwd = context.Request["pwd"] ?? "";
            var str = new StringBuilder();
            try
            {
                var employeeBll = new EmployerBll();
                if (employeeBll.Login(WorkNum, pwd))
                {
                    context.Session[Constant.LoginUser] =
                        employeeBll.GetEntity(new Employer() { WorkNum = WorkNum, Password = pwd });
                    str.Append("1");
                    return str.ToString();
                }
                else
                {
                    str.Append("0");
                    return str.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                str.Append("0");
                return str.ToString();
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string Login1(HttpContext context)
        {
            string id = context.Request["id"] ?? "";
            try
            {
                var employeeBll = new EmployerBll();
                context.Session[Constant.LoginUser] =
                    employeeBll.GetEntity(new Employer() { Id = id });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return "";
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string ModifyPassword(HttpContext context)
        {
            var employeeBll = new EmployerBll();
            var str = new StringBuilder();
            var worknum = context.Request.Params["worknum"];
            var pass = context.Request.Params["pass"];
            var newpass = context.Request.Params["newpass"];

            var employee = new Employer { WorkNum = worknum, Password = newpass };
            try
            {
                var value = employeeBll.Login(worknum, pass);
                if (value)
                {
                    employeeBll.Modify(employee);
                    str.Append("true");
                }
                else
                {
                    str.Append("falsepwd");
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                str.Append("false");
            }
            return str.ToString();
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