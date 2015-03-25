
namespace HotelCheckIn_InterfaceSystem.model
{
    public class CardInfo
    {
        private string request_id;                  //请求ID
        private string card_id;                     //会员卡ID
        private string card_type_id;                //会员卡类型ID
        private string card_type_name;              //会员卡类型名称
        private string rate_code;                   //会员享受价格代码
        private string guest_market_code;           //会员对应客源类型编码
        private string card_no;                     //会员卡号
        private string user_name;                   //用户名
        private string gender_id;                   //性别(0男，1女)
        private string id_card_type_id;             //证件类型ID(11身份证，13户口薄，31工作证，32介绍信，40无证件，41驾照，90军官证，91警官证，92士兵证，93护照，99其他证件)
        private string id_card_no;                  //证件号
        private string birthday;                    //出生日期
        private string deposit;                     //储值余额
        private string email;                       //电子邮件
        private string mobile;                      //手机号
        private string address;                     //地址
        private ReturnInfo return_info;             //返回信息

        /// <summary>
        /// 请求ID
        /// </summary>
        public string request_Id
        {
            get { return this.request_id; }
            set { this.request_id = value; }
        }
        /// <summary>
        /// 会员卡ID
        /// </summary>
        public string card_Id
        {
            get { return this.card_id; }
            set { this.card_id = value; }
        }
        /// <summary>
        /// 会员卡类型ID
        /// </summary>
        public string card_Type_Id
        {
            get { return this.card_type_id; }
            set { this.card_type_id = value; }
        }
        /// <summary>
        /// 会员卡类型名称
        /// </summary>
        public string card_Type_Name
        {
            get { return this.card_type_name; }
            set { this.card_type_name = value; }
        }
        /// <summary>
        /// 会员享受价格代码
        /// </summary>
        public string rate_Code
        {
            get { return this.rate_code; }
            set { this.rate_code = value; }
        }
        /// <summary>
        /// 会员对应客源类型编码
        /// </summary>
        public string guest_Market_Code
        {
            get { return this.guest_market_code; }
            set { this.guest_market_code = value; }
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
        /// 用户名
        /// </summary>
        public string user_Name
        {
            get { return this.user_name; }
            set { this.user_name = value; }
        }
        /// <summary>
        /// 性别(0男，1女)
        /// </summary>
        public string gender_Id
        {
            get { return this.gender_id; }
            set { this.gender_id = value; }
        }
        /// <summary>
        /// 证件类型ID(11身份证，13户口薄，31工作证，32介绍信，40无证件，41驾照，90军官证，91警官证，92士兵证，93护照，99其他证件)
        /// </summary>
        public string id_Card_Type_Id
        {
            get { return this.id_card_type_id; }
            set { this.id_card_type_id = value; }
        }
        /// <summary>
        /// 证件号
        /// </summary>
        public string id_Card_No
        {
            get { return this.id_card_no; }
            set { this.id_card_no = value; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }
        /// <summary>
        /// 储值余额
        /// </summary>
        public string Deposit
        {
            get { return this.deposit; }
            set { this.deposit = value; }
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile
        {
            get { return this.mobile; }
            set { this.mobile = value; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public ReturnInfo return_Info
        {
            get { return this.return_info; }
            set { this.return_info = value; }
        }
    }
}
