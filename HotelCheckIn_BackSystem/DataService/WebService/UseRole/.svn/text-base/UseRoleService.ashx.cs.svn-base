using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.WebService.UseRole
{
    /// <summary>
    /// Summary description for UseRoleService
    /// </summary>
    public class UseRoleService : IHttpHandler, IRequiresSessionState
    {
        protected log4net.ILog Log = log4net.LogManager.GetLogger("UseRoleService");
        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["action"];
            string str = "";
            //session过期验证
            if (context.Session[Constant.LoginUser] == null)
            {
                action = "loginout";
            }
            switch (action)
            {
                case "loginout":
                    str = "location.href='../../Login.aspx';";
                    break;
                case "getRole":
                    str = GetRole(context);
                    break;
              
            }
            context.Response.Write(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String GetRole(HttpContext context)
        {
            var userolebll = new UseRolesBll();
            var str=new StringBuilder();
            var empid = context.Request.Params["Id"];
            var jss = new JavaScriptSerializer();
            try
            {
                var dt = userolebll.FindBy(empid);
                var list = ConvertHelper<UseRoles>.ConvertToList(dt);
                str.Append(jss.Serialize(list));
                //str = str.Substring(1, str.Length - 2);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
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