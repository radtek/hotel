using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Model.Parameter
{
    public class InitMainGrid : Machine
    {
        /// <summary>
        /// 在线状态
        /// </summary>
        public string IsOnline { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HeartbeatDtPara { get; set; }
    }
}