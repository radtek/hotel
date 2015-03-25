using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    
    /// <summary>
    /// 结算表
    /// </summary>
    [Serializable]
    public class Settle : BaseModel<Settle>
    {
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime QBeginTime { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime QEndTime { get; set; }
        /// <summary>
        /// 结算开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结算结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal InMoney { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal OutMoney { get; set; }
        /// <summary>
        /// 差额
        /// </summary>
        public decimal SumMoney { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettleDateTime { get; set; }
        /// <summary>
        /// 结算人id
        /// </summary>
        public string OptId { get; set; }
        /// <summary>
        /// 结算人名称
        /// </summary>
        public string OptName { get; set; }


        public override string ToString()
        {
            return "";
        }
    }
}