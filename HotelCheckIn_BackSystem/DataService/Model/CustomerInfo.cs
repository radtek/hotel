using System;
using HotelCheckIn_Interface_Hardware.PMS;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 顾客表
    /// </summary>
    [Serializable]
    public class CustomerInfo : BaseModel<CustomerInfo>
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 顾客名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCardNum { get; set; }
        /// <summary>
        /// 身份证照片
        /// </summary>
        public string IdentityCardPhoto { get; set; }
        /// <summary>
        /// 摄像头照片
        /// </summary>
        public string CameraPhoto { get; set; }
        /// <summary>
        /// 导出次数
        /// </summary>
        public int ExportCount { get; set; }
        /// <summary>
        /// 是否验证身份证（1/是，2/否）
        /// </summary>
        public int CheckIDcard { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /*
         * 民族
         * 住址
         * 出生日期datetime
         * 姓名全拼
         * 证件类型E11
         */
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Adress { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public E11 IdentificationType { get; set; }
        /// <summary>
        /// 姓名拼音
        /// </summary>
        public string NamePy { get; set; }
        
        public override string ToString()
        {
            return "";
        }
    }
}
