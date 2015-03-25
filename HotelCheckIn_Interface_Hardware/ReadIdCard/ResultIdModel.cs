using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.ReadIdCard
{
    public class ResultIdModel
    {
        public string Name { get; set; } //姓名   

        public string Sex { get; set; }   //性别

        public string Nation { get; set; } //名族

        public string Born { get; set; } //出生日期

        public string Address { get; set; }//住址

        public string IDCardNo { get; set; } //身份证号

        public string GrantDept { get; set; } //发证机关

        public string UserLifeBegin { get; set; } // 有效开始日期

        public string UserLifeEnd { get; set; } // 有效截止日期

        public string reserved { get; set; }// 保留

        public string PhotoFileName { get; set; } // 照片路径

        public byte[] img { get; set; } //头像
    }
}
