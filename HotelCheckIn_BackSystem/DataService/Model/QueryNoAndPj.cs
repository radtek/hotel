using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    
    /// <summary>
    /// 查询实体类表
    /// </summary>
    [Serializable]
    public class QueryNoAndPj : BaseModel<QueryNoAndPj>
    {

        /// <summary>
        /// 终端验证状态
        /// </summary>
        public int MachineCheck { get; set; }
        /// <summary>
        /// 入住天数
        /// </summary>
        public int InSumDate { get; set; }
        /// <summary>
        /// 不可使用开始时间
        /// </summary>
        public DateTime CheckIdBeginTime { get; set; }
        /// <summary>
        /// 不可使用截止时间
        /// </summary>
        public DateTime CheckIdEndTime { get; set; }
        /// <summary>
        /// 房间类型
        /// </summary>
        public string RoomTypeId { get; set; }
        /// <summary>
        /// 房间类型名称
        /// </summary>
        public string RoomTypeName { get; set; }

        //===============================================================

        /// <summary>
        /// 前缀码
        /// </summary>
        public string ProjectFrontNum { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 团购商id
        /// </summary>
        public string InternetGroupId { get; set; }
        /// <summary>
        /// 团购商
        /// </summary>
        public string InternetGroup { get; set; }
        /// <summary>
        /// 生成日期开始
        /// </summary>
        public DateTime CreateTimeBegin { get; set; }
        /// <summary>
        /// 生成日期结束
        /// </summary>
        public DateTime CreateTimeEnd{ get; set; }
        /// <summary>
        /// 条件验证
        /// </summary>
        public string CheckQuery { get; set; }

        /// <summary>
        /// 楼栋
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        public string RoomNum { get; set; }

        /// <summary>
        /// 朝向
        /// </summary>
        public string Towards { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        public DateTime CheckInTime { get; set; }

        /// <summary>
        /// 退房时间
        /// </summary>
        public DateTime CheckOutTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 验证状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string CheckNo { get; set; }

        /// <summary>
        /// 是否预订,1-是，2-否
        /// </summary>
        public string IsBook { get; set; }

        /// <summary>
        /// 预订单PMS编码
        /// </summary>
        public string PmsOrderId { get; set; }
        
        /// <summary>
        /// 房价码
        /// </summary>
        public string RoomRateCode { get; set; }
        /// <summary>
        /// 总房费
        /// </summary>
        public float Rate { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}