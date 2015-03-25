using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class PayStyle
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 入住id
        /// </summary>
        public string CheckinId { get; set; }

        /// <summary>
        /// 入住订单pms编码
        /// </summary>
        public string CheckinPmsCode { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayWay { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public float PayMoney { get; set; }

        /// <summary>
        /// Hep支付记录id
        /// </summary>
        public string HepRecordId { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string DealRecordId { get; set; }

        /// <summary>
        /// 工号pms编码
        /// </summary>
        public string WorkNumPmsCode { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime DealTime { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string DealType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNum { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public string ValidityTime { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime CheckinTime { get; set; }

        /// <summary>
        /// 退房时间
        /// </summary>
        public DateTime CheckoutTime { get; set; }
    }
}