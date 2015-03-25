using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelCheckIn_PlatformSystem.DataService.WebService.Interface;

namespace HotelCheckIn_PlatformSystem.Views.Test
{
    public partial class UpdateFileWebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpgradeFile upgradeFile = new UpgradeFile();
            //var s = upgradeFile.IUpgradeFile_Pt("888");
            var list = new List<string>();
            list.Add("49d3a973-4579-40ea-aabd-3bcf73c0b4c6_6.jpg");
            list.Add("1-111121113237.jpg");
            var ss = upgradeFile.IUpdateState_Pt("888", list);
        }
    }
}