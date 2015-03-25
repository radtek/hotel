using System;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    /// <summary>
    /// 身份识别类(张威：2014-3-11)
    /// </summary>
    public class Detect
    {
        private int? _status = 1;

        /// <summary>
        /// 身份识别id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 身份证图片
        /// </summary>
        public byte[] IdCardImg { get; set; }

        /// <summary>
        /// 拍摄图片1
        /// </summary>
        public byte[] Camera1 { get; set; }

        /// <summary>
        /// 拍摄图片2
        /// </summary>
        public byte[] Camera2 { get; set; }

        /// <summary>
        /// 拍摄图片3
        /// </summary>
        public byte[] Camera3 { get; set; }

        /// <summary>
        /// 视频
        /// </summary>
        public byte[] Vedio { get; set; }

        /// <summary>
        /// 是否验证,1:未验证，2：验证不通过，3：验证通过
        /// </summary>
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        ///验证时间 
        /// </summary>
        public DateTime? UpdateDt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 机器id
        /// </summary>
        public string Jqid { get; set; }


        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
    }
}