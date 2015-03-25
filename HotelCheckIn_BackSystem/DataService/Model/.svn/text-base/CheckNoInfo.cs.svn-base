using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 验证码表
    /// </summary>
    [Serializable]
    public class CheckNoInfo : BaseModel<CheckNoInfo>
    {
        
        /// <summary>
        /// 酒店ID
        /// </summary>
        public string HotelId { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string CheckId { get; set; }
        /// <summary>
        /// 前缀码
        /// </summary>
        public string CheckID_Front { get; set; }
        /// <summary>
        /// 团购商id
        /// </summary>
        public string InternetGroupId { get; set; }
        /// <summary>
        /// 团购商
        /// </summary>
        public string InternetGroup { get; set; }
        /// <summary>
        /// 不可使用开始时间
        /// </summary>
        public DateTime CheckIdBeginTime { get; set; }
        /// <summary>
        /// 不可使用截止时间
        /// </summary>
        public DateTime CheckIdEndTime { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 生成人
        /// </summary>
        public string CreatePeople { get; set; }
        /// <summary>
        /// 终端验证
        /// </summary>
        public int MachineCheck { get; set; }
        /// <summary>
        /// 终端验证日期
        /// </summary>
        public DateTime MachineCheckDateTime { get; set; }
        /// <summary>
        /// 终端验证机器ID
        /// </summary>
        public int MachineCheckPeople { get; set; }
        /// <summary>
        /// 团购验证
        /// </summary>
        public int InternetCheck { get; set; }
        /// <summary>
        /// 团购验证日期
        /// </summary>
        public DateTime InternetCheckDateTime { get; set; }
        /// <summary>
        /// 团购验证人
        /// </summary>
        public string InternetCheckPeople { get; set; }
        /// <summary>
        /// 入住天数
        /// </summary>
        public int InSumDate { get; set; }

        //===============================分割线============================================

        /// <summary>
        /// 可用验证码数量
        /// </summary>
        public Int64 YesCheckIdNo { get; set; }
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