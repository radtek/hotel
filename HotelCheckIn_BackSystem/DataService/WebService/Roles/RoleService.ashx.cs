using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;
using System.Web.SessionState;

namespace HotelCheckIn_BackSystem.DataService.WebService.Roles
{
    /// <summary>
    /// Summary description for RoleService
    /// </summary>
    public class RoleService : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog Log = LogManager.GetLogger("RoleService");
        public void ProcessRequest(HttpContext context)
        {
            
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["action"];
            string sb="";

            //session过期验证
            if (context.Session[Constant.LoginUser] == null)
            {
                action = "loginout";
            }

            switch (action)
            {
                case "loginout":
                    sb = "location.href='../../Login.aspx';";
                    break;
                case "SaveRole":
                    sb = SaveRole(context);
                    break;
                case "DelRole":
                    sb = DelRole(context);
                    break;
                case "SaveMenuRole":
                    sb = SaveMenuRole(context);
                    break;
            }
            context.Response.Write(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String SaveMenuRole(HttpContext context)
        {
            var menuPowerBll = new MenuPowerBll { };
            var sb = new StringBuilder();
            var roleid = context.Request.Params["roleid"];
            var menuid = context.Request.Params["menuid"].Split(',');
            var menuPowerDel = new MenuPower
            {
                PowerId = roleid
            };
            menuPowerBll.Del(menuPowerDel);
            foreach (var s in menuid)
            {
                try
                {
                    var id = Guid.NewGuid().ToString();
                    var menuPower = new MenuPower
                        {
                            Id = id,
                            MenuId = s,
                            PowerId = roleid
                        };
                    menuPowerBll.Add(menuPower);

                }
                catch (Exception e)
                {
                    Log.Error("保存菜单角色出错", e);
                    throw;

                }
            }
            return sb.Append("保存成功！").ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String SaveRole(HttpContext context)
        {
            var roleBll = new RoleBll();
            var sb = new StringBuilder();
            var roleid = Guid.NewGuid().ToString();
            var rolename = context.Request.Params["jsmc"];
            var roledesc = context.Request.Params["bz"];
            var role = new Role
            {
                RoleId = roleid,
                RoleName = rolename,
                RoleDesc = roledesc,
                CreateDT = DateTime.Now
            };
            try
            {
                roleBll.Add(role);
               
            }
            catch (Exception e)
            {
                Log.Error("保存角色出错", e);
            }

            return sb.Append("保存成功！").ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public String DelRole(HttpContext context)
        {
            var menuPowerBll = new MenuPowerBll();
            var roleBll = new RoleBll();
            var userrolebll = new UseRolesBll();
            var sb = new StringBuilder();
            var roleId = context.Request.Params["nodeid"];
            var role = new Role
                {
                    RoleId = roleId
                };
            var userrole = new UseRoles
                {
                    RoleId = roleId
                };
            var menuPowerDel = new MenuPower
                {
                    PowerId = roleId
                };

            try
            {
                if (roleId == "role_admin")
                {
                    return sb.Append("3").ToString();
                }
                //联动删除
                roleBll.Del(role);
                userrolebll.Del2(userrole);
                menuPowerBll.Del(menuPowerDel);
               
                return sb.Append("删除成功！").ToString();
            }
            catch (Exception e)
            {
                Log.Error("保存角色出错", e);
                throw;
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