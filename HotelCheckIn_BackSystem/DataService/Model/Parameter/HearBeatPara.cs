using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model.Parameter
{
    public class HearBeatPara
    {
        /// <summary>
        /// 机器id
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// 故障id如果有多个故障id以井号（“#”）为间隔
        /// </summary>
        public string FalutId { get; set; }

        /// <summary>
        /// 机器状态。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 是否获取素材标记(true或者false)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public string NowDt { get; set; }
    }
}