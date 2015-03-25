using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Model.Parameter
{
    public class InitMachineGrid:MachineInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string CreateDtPara { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UpdateDtPara { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HeartbeatDtPara { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HotelName { get; set; }
    }
}