using System;
using System.Collections.Generic;
using HotelCheckIn_Interface_Hardware.PMS;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 入住订单表
    /// </summary>
    [Serializable]
    public class CheckinInfo : BaseModel<CheckinInfo>
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 机器id
        /// </summary>
        public string MacId { get; set; }
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MacName { get; set; }
        /// <summary>
        /// 酒店id
        /// </summary>
        public string HotelId { get; set; }
        /// <summary>
        /// 预订订单id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 入住编码
        /// </summary>
        public string CheckinCode { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNum { get; set; }
        /// <summary>
        /// 房间类型
        /// </summary>
        public string RoomType { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        public string Building { get; set; }
        /// <summary>
        /// 房间代码
        /// </summary>
        public string RoomCode { get; set; }
        /// <summary>
        /// 房价
        /// </summary>
        public float RoomRate { get; set; }
        /// <summary>
        /// 入住类型
        /// </summary>
        public string CheckinType { get; set; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string ViPcardNum { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public string ViPcardType { get; set; }
        /// <summary>
        /// 入住人数
        /// </summary>
        public int PeopleNum { get; set; }
        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime CheckinTime { get; set; }
        /// <summary>
        /// 退房时间
        /// </summary>
        public DateTime CheckOutTime { get; set; }
        /// <summary>
        /// 入住签字图片
        /// </summary>
        public string CheckinImage { get; set; }
        /// <summary>
        /// 订单处理时间
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 钟点数
        /// </summary>
        public int Hours { get; set; }
        /// <summary>
        /// 预付金额
        /// </summary>
        public float AdvancePayment { get; set; }
        /// <summary>
        /// 预付款方式
        /// </summary>
        public string AdvanceType { get; set; }
        /// <summary>
        /// 导出次数
        /// </summary>
        public int ExportCount { get; set; }
        /// <summary>
        /// 门锁标识
        /// </summary>
        public int DoorLockSign { get; set; }
        /// <summary>
        /// 入住单PMS编码
        /// </summary>
        public string PmsSign { get; set; }
        /// <summary>
        /// 团购商
        /// </summary>
        public string InternetGroup { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string CheckId { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 支付记录
        /// </summary>
        public string PayRecord { get; set; }
        /// <summary>
        /// 客户信息
        /// </summary>
        public string Custom { get; set; }
        /// <summary>
        /// 图片信息
        /// </summary>
        public List<byte[]> Images { get; set; }
        /// <summary>
        /// 发（房）卡数
        /// </summary>
        public int CardCount { get; set; }
        /// <summary>
        /// 关联入住单PMS编码
        /// </summary>
        public string RelevancyCheckinPmsCode { get; set; }
        /// <summary>
        /// 时间属性E12
        /// </summary>
        public string TimeType { get; set; }
        /// <summary>
        /// 散团（1-散客，2-团体）
        /// </summary>
        public string IndividualOrGroup { get; set; }
        /// <summary>
        /// 下单方式
        /// </summary>
        public E26 OrderType { get; set; }

        /// <summary>
        /// 房号PMS编码
        /// </summary>
        public string RoomNumPmsCode { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}
