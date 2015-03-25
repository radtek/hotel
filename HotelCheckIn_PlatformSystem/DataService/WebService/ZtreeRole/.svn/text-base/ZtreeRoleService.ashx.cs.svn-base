using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using CommonLibrary;
using HotelCheckIn_PlatformSystem.DataService.BLL;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.ZtreeRole
{
    /// <summary>
    /// Summary description for ZtreeRoleService
    /// </summary>
    public class ZtreeRoleService : IHttpHandler, IRequiresSessionState
    {
        readonly ResMenuBll _resMenuBll = new ResMenuBll();
        readonly RoleBll _roleBll = new RoleBll();
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
            //var id = context.Request.Params["id"];
            var roleid = context.Request.Params["roleid"];
            var str = new StringBuilder();

            switch (action)
            {
                case "menu":
                    str = GetMenuTree(roleid);
                    break;
                case "role":
                    str = GetRoleTree();
                    break;
                default:
                    str.Append("");
                    break;
            }
            context.Response.Write(str.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetRoleTree()
        {
            var str = new StringBuilder();
            var role = new Model.Role();
            var dtRoot = _roleBll.Query(role);
            str.Append("[");
            if (dtRoot.Rows.Count > 0)
            {
                for (var i = 0; i < dtRoot.Rows.Count; i++)
                {
                    var rId = dtRoot.Rows[i]["roleid"].ToString();
                    var rName = dtRoot.Rows[i]["rolename"].ToString();
                    if (i > 0)
                    {
                        str.Append(",");
                    }
                    str.Append("{id:'" + rId + "',pId:'0' ,name:'" + rName + "',icon:'../../images/renyuan.png',open:true }");
                }

            }
            str.Append("]");
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public StringBuilder GetMenuTree(string roleid)
        {
            var str = new StringBuilder();
            var dtRoot = _resMenuBll.FindBy();
            str.Append("[");
            if (dtRoot.Rows.Count > 0)
            {
                for (var i = 0; i < dtRoot.Rows.Count; i++)
                {
                    var rId = dtRoot.Rows[i]["resid"].ToString();
                    var rName = dtRoot.Rows[i]["caption"].ToString();
                    if (i > 0)
                    {
                        str.Append(",");
                    }
                    str.Append("{id:'" + rId + "',pId:'0' ,name:'" + rName + "',icon:'../../images/1_open.png',isParent:true,open:true }");

                    var resMenu = new ResMenu { ParentId = rId };
                    var dtNodes = _resMenuBll.Query(resMenu);
                    if (dtNodes.Rows.Count <= 0) continue;
                    for (var j = 0; j < dtNodes.Rows.Count; j++)
                    {
                        var nId = dtNodes.Rows[j]["resid"].ToString(); // 节点id
                        var nName = dtNodes.Rows[j]["caption"].ToString(); // 节点名称 
                        var pId = dtNodes.Rows[j]["parentid"].ToString(); // 上级节点id

                        str.Append(",");
                        if (IsCheckedNode(roleid, nId))
                        {
                            str.Append("{id:'" + nId + "',pId:'" + pId + "',name:'" + nName + "',icon:'../../images/2.png',checked:true,open:true }");
                            var resMenu3 = new ResMenu { ParentId = nId };
                            var dtNodes3 = _resMenuBll.Query(resMenu3);
                            if (dtNodes3.Rows.Count <= 0) continue;
                            for (var k = 0; k < dtNodes3.Rows.Count; k++)
                            {
                                var nId3 = dtNodes3.Rows[k]["resid"].ToString(); // 节点id
                                var nName3 = dtNodes3.Rows[k]["caption"].ToString(); // 节点名称 
                                var pId3 = dtNodes3.Rows[k]["parentid"].ToString(); // 上级节点id

                                str.Append(",");
                                if (IsCheckedNode(roleid, nId3))
                                {
                                    str.Append("{id:'" + nId3 + "',pId:'" + pId3 + "',name:'" + nName3 + "',icon:'../../images/2.png',checked:true,open:true }");
                                }
                                else
                                {
                                    str.Append("{id:'" + nId3 + "',pId:'" + pId3 + "',name:'" + nName3 + "',icon:'../../images/2.png',open:true }");
                                }

                            }
                        }
                        else
                        {
                            str.Append("{id:'" + nId + "',pId:'" + pId + "',name:'" + nName + "',icon:'../../images/2.png',open:true }");
                            var resMenu3 = new ResMenu { ParentId = nId };
                            var dtNodes3 = _resMenuBll.Query(resMenu3);
                            if (dtNodes3.Rows.Count <= 0) continue;
                            for (var k = 0; k < dtNodes3.Rows.Count; k++)
                            {
                                var nId3 = dtNodes3.Rows[k]["resid"].ToString(); // 节点id
                                var nName3 = dtNodes3.Rows[k]["caption"].ToString(); // 节点名称 
                                var pId3 = dtNodes3.Rows[k]["parentid"].ToString(); // 上级节点id

                                str.Append(",");
                                if (IsCheckedNode(roleid, nId3))
                                {
                                    str.Append("{id:'" + nId3 + "',pId:'" + pId3 + "',name:'" + nName3 + "',icon:'../../images/2.png',checked:true,open:true }");
                                }
                                else
                                {
                                    str.Append("{id:'" + nId3 + "',pId:'" + pId3 + "',name:'" + nName3 + "',icon:'../../images/2.png',open:true }");
                                }

                            }
                        }
                            
                    }
                }

            }
            str.Append("]");
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="menuid"></param>
        /// <returns></returns>
        public bool IsCheckedNode(string roleid, string menuid)
        {
            var menuPowerBll = new MenuPowerBll();
            var menuPower = new MenuPower
                {
                    MenuId = menuid,
                    PowerId = roleid
                };
            return menuPowerBll.Exist(menuPower);
        }

        public bool IsReusable { get { return false; } }

    }
}