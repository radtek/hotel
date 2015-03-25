using System;

namespace HotelCheckIn_BackSystem
{
    public partial class TopFrame : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetUserName()
        {
            if (Session["user"] != null)
            {
                return Session["user"].ToString();
            }
            return "";
        }
    }
}
