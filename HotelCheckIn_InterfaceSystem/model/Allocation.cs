
namespace HotelCheckIn_InterfaceSystem.model
{
    public class Allocation
    {

        private string allocation_id;               //预留ID
        private string room_id;                     //分房房间ID
        private string room_type_id;                //房间类型ID
        private string check_in_date;               //入住日期
        private string check_out_date;              //离店日期
        private string order_check_in_date;         //订单入住日期
        private string order_check_out_date;        //订单离店日期
        private string booker_name;                 //预订人
        private string booker_mobile;               //预订人手机号
        private string booker_time;                 //预订时间  2010-11-12 16:07:26  格式
        private string rate_code;                   //订单价格代码
        private string deposit;                     //订单已支付预订金

        /// <summary>
        /// 预留ID
        /// </summary>
        public string allocation_Id
        {
            get { return this.allocation_id; }
            set { this.allocation_id = value; }
        }
        /// <summary>
        /// 分房房间ID
        /// </summary>
        public string room_Id
        {
            get { return this.room_id; }
            set { this.room_id = value; }
        }
        /// <summary>
        /// 房间类型ID
        /// </summary>
        public string room_Type_Id
        {
            get { return this.room_type_id; }
            set { this.room_type_id = value; }
        }
        /// <summary>
        /// 入住日期
        /// </summary>
        public string check_In_Date
        {
            get { return this.check_in_date; }
            set { this.check_in_date = value; }
        }
        /// <summary>
        /// 离店日期
        /// </summary>
        public string check_Out_Date
        {
            get { return this.check_out_date; }
            set { this.check_out_date = value; }
        }
        /// <summary>
        /// 订单入住日期
        /// </summary>
        public string order_Check_In_Date
        {
            get { return this.order_check_in_date; }
            set { this.order_check_in_date = value; }
        }
        /// <summary>
        /// 订单离店日期
        /// </summary>
        public string order_Check_Out_Date
        {
            get { return this.order_check_out_date; }
            set { this.order_check_out_date = value; }
        }
        /// <summary>
        /// 预订人
        /// </summary>
        public string booker_Name
        {
            get { return this.booker_name; }
            set { this.booker_name = value; }
        }
        /// <summary>
        /// 预订人手机号
        /// </summary>
        public string booker_Mobile
        {
            get { return this.booker_mobile; }
            set { this.booker_mobile = value; }
        }
        /// <summary>
        /// 预订时间  2010-11-12 16:07:26  格式
        /// </summary>
        public string booker_Time
        {
            get { return this.booker_time; }
            set { this.booker_time = value; }
        }
        /// <summary>
        /// 订单价格代码
        /// </summary>
        public string rate_Code
        {
            get { return this.rate_code; }
            set { this.rate_code = value; }
        }
        /// <summary>
        /// 订单已支付预订金
        /// </summary>
        public string Deposit
        {
            get { return this.deposit; }
            set { this.deposit = value; }
        }
    }
}
