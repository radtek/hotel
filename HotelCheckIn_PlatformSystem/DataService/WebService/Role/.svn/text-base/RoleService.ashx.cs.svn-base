using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CommonLibrary;
using HotelCheckIn_PlatformSystem.DataService.BLL;
using HotelCheckIn_PlatformSystem.DataService.Model;
using log4net;
using System.Web.SessionState;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.Role
{
    /// <summary>
    /// Summary description for RoleService
    /// </summary>
    public class RoleService : IHttpHandler, IRequiresSessionState
    {
        private static readonly ILog Log = LogManager.GetLogger("RoleService");
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Session[Constant.LoginUser] == null)
                {
                    context.Response.Write("location.href='../../Login.aspx';");
                }
            }
            catch (Exception)
            {
                context.Response.Write("location.href='../../Login.aspx';");
            }
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["action"];
            var sb = new StringBuilder();
            switch (action)
            {
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
        public StringBuilder SaveMenuRole(HttpContext context)
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
           
            return sb.Append("保存成功！");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StringBuilder SaveRole(HttpContext context)
        {
            var roleBll = new RoleBll();
            var sb = new StringBuilder();
            var roleid = Guid.NewGuid().ToString();
            var rolename = context.Request.Params["jsmc"];
            var roledesc = context.Request.Params["bz"];
            var role = new Model.Role
            {
                RoleId = roleid,
                RoleName = rolename,
                RoleDesc = roledesc,
                CreateDT = DateTime.Now
            };
            try
            {
                roleBll.Add(role);
              return sb.Append("保存成功！");
            }
            catch (Exception e)
            {
                Log.Error("保存角色出错", e);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public StringBuilder DelRole(HttpContext context)
        {
            var menuPowerBll = new MenuPowerBll();
            var roleBll = new RoleBll();
            var userrolebll = new UseRolesBll();
            var sb = new StringBuilder();
            var roleId = context.Request.Params["nodeid"];
            var role = new Model.Role
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
                    return sb.Append("3");
                }
                //联动删除
                roleBll.Del(role);
                userrolebll.Del2(userrole);
                menuPowerBll.Del(menuPowerDel);
               
                return sb.Append("删除成功！");
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