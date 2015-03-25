using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.WebService
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            //if (string.IsNullOrEmpty(action))
            //{
            //    action = context.Request.Form["ActionName"];
            //}
            //if (!string.IsNullOrEmpty(action))
            //{
            //    Type curType = this.GetType();
            //    MethodInfo method = curType.GetMethod(action, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            //    if (method != null)
            //    {
            //        method.Invoke(this, new object[] { HttpContext.Current });
            //    }
            //    else
            //    {
            //        context.Response.Write("");
            //    }
            //}
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