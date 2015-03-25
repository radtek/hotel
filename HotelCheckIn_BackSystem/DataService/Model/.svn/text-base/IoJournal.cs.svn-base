using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 收支流水帐表
    /// </summary>
    [Serializable]
    public class IoJournal : BaseModel<IoJournal>
    {
        /// <summary>
        /// 收支时间
        /// </summary>
        public DateTime IoTime { get; set; }
        /// <summary>
        /// 查询开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 查询结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 收支人员id
        /// </summary>
        public string IoId { get; set; }
        /// <summary>
        /// 收支人员名称
        /// </summary>
        public string IoName { get; set; }
        /// <summary>
        /// 收支金额
        /// </summary>
        public decimal IoMoney { get; set; }
        /// <summary>
        /// 收支标识
        /// </summary>
        public int IoTag { get; set; }
        /// <summary>
        /// 作废标识
        /// </summary>
        public int IsUse { get; set; }
        /// <summary>
        /// 收支来源标志
        /// </summary>
        public int IoFrom { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string RoomNo { get; set; }
        /// <summary>
        /// 收入是否支出(1-未进行对应支出，2-已进行对应支出)
        /// </summary>
        public int InIsOrNotOut { get; set; }
        /// <summary>
        /// 查询标记（1—结算维护中具体时间的传递，不需要日期+1）
        /// </summary>
        public int QuerySign { get; set; }
        /// <summary>
        /// 是否发卡（机器来源收入：1-未发卡，2-已发卡）
        /// </summary>
        public int InOrOutCard { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}