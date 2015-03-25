using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_PlatformSystem.DataService.Model
{
    public class Heartbeat
    {
        /// <summary>
        /// 心跳id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 机器id
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// 心跳时间
        /// </summary>
        public DateTime? CreateDt { get; set; }
    }
}