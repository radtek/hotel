using System;
using HotelCheckIn_BackSystem.DataService.Model.Parameter;
using HotelCheckIn_BackSystem.DataService.WebService;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InterFace interFace = new InterFace();
            interFace.IHeartBeat_Pt(new HearBeatPara()
                {
                    FalutId="123123",
                    MachineId = "66",
                    NowDt = DateTime.Now.ToString("yyyy-MM-dd"),
                    PassWord = "2",
                    Status = "123123",
                    Url = "true",
                });

        }
    }
}