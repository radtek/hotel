using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Model.Parameter
{
    public class QueryFault : Fault
    {
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// 故障名称
        /// </summary>
        public string FaultName { get; set; }

        /// <summary>
        /// 解决时间
        /// </summary>
        public string SolveDtPara { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDtPara { get; set; }
    }
}