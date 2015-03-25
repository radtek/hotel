
namespace HotelCheckIn_InterfaceSystem.model
{
    public class Register
    {
        private string register_id;
        private string room_id;
        private string check_in_date;
        private string check_in_time;
        private string check_out_date;
        private string check_out_time;
        private string room_order_id;
        private string company_id;
        private string rate_code;
        private string room_rate;
        private string guest_names;
        private string member_card_no;
        private string biz_source_id;
        private string guest_market_id;
        private string total_fee;
        private string total_consume;

        /// <summary>
        /// 登记单ID
        /// </summary>
        public string register_Id
        {
            get { return this.register_id; }
            set { this.register_id = value; }
        }
        /// <summary>
        /// 房间ID
        /// </summary>
        public string room_Id
        {
            get { return this.room_id; }
            set { this.room_id = value; }
        }
        /// <summary>
        /// 抵店日期（YYYY-MM-DD）
        /// </summary>
        public string check_In_Date
        {
            get { return this.check_in_date; }
            set { this.check_in_date = value; }
        }
        /// <summary>
        /// 抵店时间（YYYY-MM-DD hh:mm:ss）
        /// </summary>
        public string check_In_Time
        {
            get { return this.check_in_time; }
            set { this.check_in_time = value; }
        }
        /// <summary>
        /// 离店日期（YYYY-MM-DD）
        /// </summary>
        public string check_Out_Date
        {
            get { return this.check_out_date; }
            set { this.check_out_date = value; }
        }
        /// <summary>
        /// 离店时间（YYYY-MM-DD hh:mm:ss）
        /// </summary>
        public string check_Out_Time
        {
            get { return this.check_out_time; }
            set { this.check_out_time = value; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string room_Order_Id
        {
            get { return this.room_order_id; }
            set { this.room_order_id = value; }
        }
        /// <summary>
        /// 协议单位ID
        /// </summary>
        public string company_Id
        {
            get { return this.company_id; }
            set { this.company_id = value; }
        }
        /// <summary>
        /// 房价代码
        /// </summary>
        public string rate_Code
        {
            get { return this.rate_code; }
            set { this.rate_code = value; }
        }
        /// <summary>
        /// 当日房价
        /// </summary>
        public string room_Rate
        {
            get { return this.room_rate; }
            set { this.room_rate = value; }
        }
        /// <summary>
        /// 入住客人
        /// </summary>
        public string guest_Names
        {
            get { return this.guest_names; }
            set { this.guest_names = value; }
        }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string member_Card_No
        {
            get { return this.member_card_no; }
            set { this.member_card_no = value; }
        }
        /// <summary>
        /// 业务来源ID
        /// </summary>
        public string biz_Source_Id
        {
            get { return this.biz_source_id; }
            set { this.biz_source_id = value; }
        }
        /// <summary>
        /// 客源ID
        /// </summary>
        public string guest_Market_Id
        {
            get { return this.guest_market_id; }
            set { this.guest_market_id = value; }
        }
        /// <summary>
        /// 总费用
        /// </summary>
        public string total_Fee
        {
            get { return this.total_fee; }
            set { this.total_fee = value; }
        }
        /// <summary>
        /// 总消费
        /// </summary>
        public string total_Consume
        {
            get { return this.total_consume; }
            set { this.total_consume = value; }
        }
    }
}
