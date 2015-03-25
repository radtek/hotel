using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 房间属性表
    /// </summary>
    [Serializable]
    public class RoomQuality : BaseModel<RoomQuality>
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型id
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 酒店id
        /// </summary>
        public string HotelId { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}