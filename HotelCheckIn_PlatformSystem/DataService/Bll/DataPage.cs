using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_PlatformSystem.DataService.Bll
{
    public class DataPage
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 每一页共多少条数据
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 一共多少条数据
        /// </summary>
        public int Sum { get; set; }
    }
}