using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 房间锁定信息表
    /// </summary>
    [Serializable]
    public class RoomLockInfo : BaseModel<RoomLockInfo>
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string CheckId { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNum { get; set; }
        /// <summary>
        /// 酒店id
        /// </summary>
        public string HotelId { get; set; }
        /// <summary>
        /// 机器id
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime LockTime { get; set; }
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}