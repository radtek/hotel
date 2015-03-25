using System;
using HotelCheckIn_Interface_Hardware.PMS;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 查询房价信息
    /// </summary>
    [Serializable]
    public class QueryRoomRateInfo
    {
        /// <summary>
        /// 价格来源
        /// </summary>
        public string PriceSouce { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidationCode { get; set; }

        /// <summary>
        /// 酒店PMS编码
        /// </summary>
        public string HotelPmsCode { get; set; }
        /// <summary>
        /// 开始日期yyyy-MM-dd
        /// </summary>
        public DateTime CheckinTime { get; set; }
        /// <summary>
        /// 开始日期yyyy-MM-dd
        /// </summary>
        public DateTime CheckoutTime { get; set; }

        /// <summary>
        /// 酒店房型PMS编码
        /// </summary>
        public string RoomTypeCode { get; set; }

        /// <summary>
        /// 日期属性
        /// </summary>
        public E29 DateType { get; set; }

        /// <summary>
        /// 时间属性
        /// </summary>
        public E12 TimeType { get; set; }

        /// <summary>
        /// 下单方式
        /// </summary>
        public E26 OrderType { get; set; }

        /// <summary>
        /// 价格类别
        /// </summary>
        public E65 PriceType { get; set; }

        /// <summary>
        /// 价格类别二级属性
        /// </summary>
        public string PriceType2 { get; set; }

        /// <summary>
        /// PMS房价码；（空时不做筛选）
        /// </summary>
        public string PmsRoomRateCode { get; set; }

    }
}