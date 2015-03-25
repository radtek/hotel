using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Session[Constant.LoginUser])
            {
                Response.Redirect("Login.aspx");
                return;
            }
            var emp = (Session[Constant.LoginUser]) as Employer;
            tbTip.Text = "欢迎您：" + emp.Name;
        }

        /// <summary>
        /// 生成菜单html代码
        /// </summary>
        /// <returns></returns>
        public string OutPutMenu()
        {
            if (null == Session[Constant.LoginUser])
            {
                return "<script>location.href ='Login.aspx'</script>";
            }
            //菜单功能
            DataTable dt = (new MenuPowerBll()).QueryMenuByRole(((Employer)(Session[Constant.LoginUser])).Id);
            string menuStr = "";
            if ((null == dt || dt.Rows.Count == 0) && ((Employer)(Session[Constant.LoginUser])).Id == "admin")
            {
                return "<li><a href=\"javascript:void(0)\" onclick=\"sf('pages/role/roleManage.htm',null)\">权限</a></li>"
                       + "<script>var mainSrc ='pages/role/roleManage.htm'</script>";
                //return "<script>location.href ='Login.aspx'</script>";
            }
            int count = dt.Rows.Count;
            foreach (DataRow row in dt.Rows) //使所有的ppname不为空
            {
                if (row["ppname"].ToString().Length == 0)
                {
                    row["ppname"] = row["parentname"];
                }
            }
            for (int i = 0; i < count; i++)
            {
                if (i == 0 && count > 1) //第一个菜单项
                {
                    if (dt.Rows[i]["level_id"].ToString() == "1") //二级菜单
                    {
                        if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"] + "',null)\">" + dt.Rows[i]["caption"] + "</a></li>";
                        }
                        else
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["parentname"].ToString()
                                       + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] + "</a></li>";
                        }
                    }
                    else //三级菜单
                    {
                        if (dt.Rows[i]["ppname"].ToString() != dt.Rows[i + 1]["ppname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                       + "</a><ul><li><a href=\"javascript:void(0)\">" + dt.Rows[i]["parentname"]
                                       + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                       + "</a></li></ul></li></ul></li>";
                            //三级菜单只有一个菜单项
                        }
                        else if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                       + "</a><ul><li><a href=\"javascript:void(0)\">" + dt.Rows[i]["parentname"]
                                       + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                       + "</a></li></ul></li>"; //三级菜单多个菜单项-起始
                        }
                        else
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                       + "</a><ul><li><a href=\"javascript:void(0)\">" + dt.Rows[i]["parentname"]
                                       + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] + "</a></li>";
                        }
                    }
                }
                else if (i < count - 1) //中间菜单项
                {
                    if (dt.Rows[i]["level_id"].ToString() == "1") //二级菜单
                    {
                        #region 二级菜单

                        if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString() &&
                            dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"].ToString() +
                                       "',null)\">" + dt.Rows[i]["parentname"].ToString() + "</a><ul>";
                        }
                        else if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString() &&
                                 dt.Rows[i]["parentname"].ToString() == dt.Rows[i + 1]["parentname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["parentname"].ToString()
                                       + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"].ToString() +
                                       "',this)\">" + dt.Rows[i]["caption"].ToString() + "</a></li>";
                        }
                        else if (dt.Rows[i]["parentname"].ToString() == dt.Rows[i - 1]["parentname"].ToString() &&
                                 dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"].ToString() +
                                       "',this)\">" + dt.Rows[i]["caption"].ToString() + "</a></li></ul>";
                        }
                        else
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"].ToString() +
                                       "',this)\">" + dt.Rows[i]["caption"].ToString() + "</a></li>";
                        }

                        #endregion
                    }
                    else //三级菜单
                    {
                        if (dt.Rows[i]["ppname"].ToString() != dt.Rows[i - 1]["ppname"].ToString() &&
                            dt.Rows[i]["ppname"].ToString() != dt.Rows[i + 1]["ppname"].ToString())
                        {
                            if (dt.Rows[i]["parentname"].ToString() == dt.Rows[i]["ppname"].ToString())
                            {
                                menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('"
                                           + dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                           + "</a></li></ul></li>";
                            }
                            else
                            {
                                menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                           + "</a><ul><li><a href=\"javascript:void(0)\">" + dt.Rows[i]["parentname"]
                                           + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('"
                                           + dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                           + "</a></li></ul></li></ul></li>";
                                //三级菜单只有一个菜单项
                            }
                        }
                        else if (dt.Rows[i]["ppname"].ToString() != dt.Rows[i - 1]["ppname"].ToString() &&
                                 dt.Rows[i]["ppname"].ToString() == dt.Rows[i + 1]["ppname"].ToString())
                        {
                            if (dt.Rows[i]["parentname"].ToString() == dt.Rows[i]["ppname"].ToString())
                            {
                                menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                           + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('"
                                           + dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                           + "</a></li>";
                            }
                            else
                            {
                                if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\">" +
                                               dt.Rows[i]["parentname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] +
                                               "',this)\">" + dt.Rows[i]["caption"] + "</a></li></ul></li>";
                                    //三级菜单多个菜单项-起始
                                }
                                else
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\">" +
                                               dt.Rows[i]["parentname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] +
                                               "',this)\">" + dt.Rows[i]["caption"] + "</a></li>"; //三级菜单多个菜单项-多个三级子菜单
                                }
                            }
                        }
                        else if (dt.Rows[i]["ppname"].ToString() == dt.Rows[i - 1]["ppname"].ToString() &&
                                 dt.Rows[i]["ppname"].ToString() != dt.Rows[i + 1]["ppname"].ToString())
                        {
                            if (dt.Rows[i]["parentname"].ToString() == dt.Rows[i]["ppname"].ToString())
                            {
                                menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('"
                                           + dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                           + "</a></li></ul></li>";
                            }
                            else
                            {
                                if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\">" +
                                               dt.Rows[i]["parentname"] +
                                               "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] +
                                               "</a></li></ul></li></ul></li>";
                                }
                                else
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                               + "</a></li></ul></li></ul></li>";
                                }
                            }
                        }
                        else //ppname等于前等于后
                        {
                            if (dt.Rows[i]["parentname"].ToString() == dt.Rows[i]["ppname"].ToString())
                            {
                                menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('"
                                           + dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                           + "</a></li>";
                            }
                            else
                            {
                                if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString() &&
                                    dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\">" +
                                               dt.Rows[i]["parentname"] +
                                               "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] +
                                               "</a></li></ul></li>";
                                }
                                else if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i + 1]["parentname"].ToString() &&
                                         dt.Rows[i]["parentname"].ToString() == dt.Rows[i - 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] +
                                               "</a></li></ul></li>";
                                }
                                else if (dt.Rows[i]["parentname"].ToString() ==
                                         dt.Rows[i + 1]["parentname"].ToString() &&
                                         dt.Rows[i]["parentname"].ToString() !=
                                         dt.Rows[i - 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\">" +
                                               dt.Rows[i]["parentname"] +
                                               "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] +
                                               "</a></li>";
                                }
                                else
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"] +
                                               "</a></li>";
                                }
                            }
                        }
                    }
                }
                else if (i == count - 1 && count > 1) //最后一个菜单项
                {
                    if (dt.Rows[i]["level_id"].ToString() == "1") //二级菜单
                    {
                        if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"].ToString() +
                                       "',null)\">" + dt.Rows[i]["parentname"].ToString() + "</a></li>";
                        }
                        else
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                       dt.Rows[i]["href"].ToString() +
                                       "',this)\">" + dt.Rows[i]["caption"].ToString() + "</a></li></ul>";
                        }
                    }
                    else //三级菜单
                    {
                        if (dt.Rows[i]["parentname"].ToString() == dt.Rows[i]["ppname"].ToString())
                        {
                            menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('"
                                       + dt.Rows[i]["href"] + "',this)\">" + dt.Rows[i]["caption"]
                                       + "</a></li></ul></li>";
                        }
                        else
                        {
                            if (dt.Rows[i]["ppname"].ToString() != dt.Rows[i - 1]["ppname"].ToString())
                            {
                                if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" >" + dt.Rows[i]["ppname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\">" +
                                               dt.Rows[i]["parentname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] +
                                               "',this)\">" + dt.Rows[i]["caption"] + "</a></li></ul></li></ul></li>";
                                    //三级菜单只有一个菜单项
                                }
                                else
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" + dt.Rows[i]["href"] +
                                               "',this)\">" + dt.Rows[i]["caption"] + "</a></li></ul></li></ul></li>";
                                }
                            }
                            else
                            {
                                if (dt.Rows[i]["parentname"].ToString() != dt.Rows[i - 1]["parentname"].ToString())
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\">" + dt.Rows[i]["parentname"]
                                               + "</a><ul><li><a href=\"javascript:void(0)\" onclick=\"sf('" +
                                               dt.Rows[i]["href"] +
                                               "',this)\">" + dt.Rows[i]["caption"] + "</a></li></ul></li></ul></li>";
                                }
                                else
                                {
                                    menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" + dt.Rows[i]["href"] +
                                               "',this)\">" + dt.Rows[i]["caption"] + "</a></li></ul></li></ul></li>";
                                }
                            }
                        }
                    }
                }
                else //只有一个菜单项
                {
                    menuStr += "<li><a href=\"javascript:void(0)\" onclick=\"sf('" + dt.Rows[i]["href"].ToString() +
                               "',null)\">" + dt.Rows[i]["caption"].ToString() + "</a></li>";
                }
            }
            menuStr += "<script>var mainSrc ='" + dt.Rows[0]["href"].ToString() +
                       "'</script>";
            return menuStr;
        }


        public string GetMenuList()
        {
            var listMainMenu = new Dictionary<string, string>();
            var listSubMenu = new List<string>();
            if (null == Session[Constant.LoginUser])
            {
                return "<script>location.href ='Login.aspx'</script>";
            }
            var dtSub = (new MenuPowerBll()).QueryMenuSub(((Employer)(Session[Constant.LoginUser])).Id);
            foreach (DataRow dataRow in dtSub.Rows)
            {
                var tmp1 = dataRow["parentId"].ToString();
                var tmp2 = dataRow["ResId"].ToString();
                if (!listMainMenu.ContainsKey(tmp1))
                {
                    listMainMenu.Add(tmp1, dataRow["Caption"].ToString());
                }
                listSubMenu.Add(tmp2);
            }
            var html = string.Empty;
            foreach (var li in listMainMenu)
            {
                html += "<li class='dropdown'><a href=\"javascript:void(0)\" class='dropdown-toggle' data-toggle='dropdown'>" + li.Value
                    + "  <b class='caret'></b></a>";
                var tmp = from list in listSubMenu
                          where list.Substring(0, 2) == li.Key
                          select list;
                html += " <ul class=\"dropdown-menu\">";
                html = tmp.ToList().Select(t => (new MenuPowerBll()).QuerySubMenu(t)).Aggregate(html, (current, dtMain) => current + ("<li><a href=\"javascript:void(0)\" onclick=\"sf('" + dtMain.Rows[0]["Href"] + "',null)\">" + dtMain.Rows[0]["Caption"] + "</a></li>"));
                html += "</ul>";
                html += "</li>";
            }
            return html;

        }
    }

}