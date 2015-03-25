using System;
using System.Collections.Generic;

namespace HotelCheckIn_InterfaceSystem.model
{
   public class OrderRegister
    {
        private string hotel_id;            //酒店编码
        private string room_id;             //房间ID（必填）
        private string allocation_id;       //预留ID（必填）
        private string card_type_id;        //会员卡类型ID
        private string card_no;             //会员卡号
        private string guest_market_code;   //客源类型编码
        private string deposit_money;       //预付金额
        private string payment_method_id;   //付款方式ID
        private string credit_card_type_id; //信用卡类型ID
        private string credit_card_no;      //信用卡号
        private List<Guest> guest_list;     //入住客户

        private string check_in_date;       //入住日期（必填，YYYY-MM-DD）
        private string check_out_date;      //离店日期（必填，YYYY-MM-DD）

        private string rate_code;           //价格代码（必填）
        private string check_hours;         //钟点房小时数（必填，大于0）

        /// <summary>
        /// 酒店编码（必填）
        /// </summary>
        public string hotel_Id
        {
            get { return this.hotel_id; }
            set { this.hotel_id = value; }
        }

        /// <summary>
        /// 房间ID（必填）
        /// </summary>
        public string room_Id
        {
            get { return this.room_id; }
            set { this.room_id = value; }
        }

        /// <summary>
        /// 预留ID（必填）
        /// </summary>
        public string allocation_Id
        {
            get { return this.allocation_id; }
            set { this.allocation_id = value; }
        }

        /// 会员卡类型ID
        /// </summary>
        public string card_Type_Id
        {
            get { return this.card_type_id; }
            set { this.card_type_id = value; }
        }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string card_No
        {
            get { return this.card_no; }
            set { this.card_no = value; }
        }

        /// <summary>
        /// 客源类型编码
        /// </summary>
        public string guest_Market_Code
        {
            get { return this.guest_market_code; }
            set { this.guest_market_code = value; }
        }

        /// <summary>
        /// 预付金额
        /// </summary>
        public string deposit_Money
        {
            get { return this.deposit_money; }
            set { this.deposit_money = value; }
        }

        /// <summary>
        /// 付款方式ID
        /// </summary>
        public string payment_Method_Id
        {
            get { return this.payment_method_id; }
            set { this.payment_method_id = value; }
        }

        /// <summary>
        /// 信用卡类型ID
        /// </summary>
        public string credit_Card_Type_Id
        {
            get { return this.credit_card_type_id; }
            set { this.credit_card_type_id = value; }
        }

        /// <summary>
        /// 信用卡号
        /// </summary>
        public string credit_Card_No
        {
            get { return this.credit_card_no; }
            set { this.credit_card_no = value; }
        }

        /// <summary>
        /// 入住客户
        /// </summary>
        public List<Guest> guest_List
        {
            get { return this.guest_list; }
            set { this.guest_list = value; }
        }

        /// <summary>
        /// 入住日期（必填，YYYY-MM-DD）
        /// </summary>
        public string check_In_Date
        {
            get { return this.check_in_date; }
            set { this.check_in_date = value; }
        }

        /// <summary>
        /// 离店日期（必填，YYYY-MM-DD）
        /// </summary>
        public string check_Out_Date
        {
            get { return this.check_out_date; }
            set { this.check_out_date = value; }
        }

        /// <summary>
        /// 价格代码（必填）
        /// </summary>
        public string rate_Code
        {
            get { return this.rate_code; }
            set { this.rate_code = value; }
        }

        /// <summary>
        /// 钟点房小时数（必填，大于0）
        /// </summary>
        public string check_Hours
        {
            get { return this.check_hours; }
            set { this.check_hours = value; }
        }

        /// <summary>
        /// 入住方式
        /// </summary>
        public string CheckInType { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ProcessTime { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string SignImage { get; set; }

        /// <summary>
        /// 入住类型
        /// </summary>
        public string CheckinType { get; set; }

        /// <summary>
        /// 房价
        /// </summary>
        public float RoomRate { get; set; }
    }
}
