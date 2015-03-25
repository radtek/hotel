using System;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class CheckinQuery
    {
        /// <summary>
        /// 终端ID
        /// </summary>
        public string MacId { get; set; }

        public string CheckinType { get; set; }

        /// <summary>
        /// 查询入住时间开始
        /// </summary>
        public DateTime CheckinTimeBegin { get; set; }

        /// <summary>
        /// 查询入住时间结束
        /// </summary>
        public DateTime CheckinTimeEnd { get; set; }

        /// <summary>
        /// 导出次数
        /// </summary>
        public int ExportCount { get; set; }



    }
}
