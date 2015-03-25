using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelCheckIn_BackSystem.DataService.Model;
using CommonLibrary;

namespace HotelCheckIn_BackSystem.Views.ChangePassword
{
    public partial class ChangePassword1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetUserName()
        {
            var user = Session[Constant.LoginUser];
            if (user != null)
            {
                return ((Employer)(user)).Name;
            }
            Response.Redirect("../../Login.aspx");
            return "";
        }

        public string GetWorkNum()
        {
            var user = Session[Constant.LoginUser];
            if (user != null)
            {
                return ((Employer)(user)).WorkNum;
            }
            Response.Redirect("../../Login.aspx");
            return "";
        }
    }
}