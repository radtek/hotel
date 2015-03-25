
namespace HotelCheckIn_InterfaceSystem.model
{
    public class Guest
    {

        private string guest_name;              //客人姓名（必填）
        private string id_card_type_id;         //证件类型
        private string id_card_no;              //证件号
        private string birthday;                //生日
        private string gender_id;               //性别ID
        private string salutation_id;           //称呼ID
        private string address;                 //地址
        private string nation;                  //民族
        private string availability;            //身份证有效期
        private string admin;                   //允许通过的管理员id
        
        /// <summary>
        /// 客人姓名（必填）
        /// </summary>
        public string guest_Name
        {
            get { return this.guest_name; }
            set { this.guest_name = value; }
        }
        /// <summary>
        /// 证件类型（必填：11身份证，13户口薄，31工作证，32介绍信，40无证件，41驾照，90军官证，91警官证，92士兵证，93护照，99其他证件，默认11）
        /// </summary>
        public string id_Card_Type_Id
        {
            get { return this.id_card_type_id; }
            set { this.id_card_type_id = value; }
        }
        /// <summary>
        /// 证件号（必填）
        /// </summary>
        public string id_Card_No
        {
            get { return this.id_card_no; }
            set { this.id_card_no = value; }
        }
        /// <summary>
        /// 生日（必填YYYY-MM-DD）
        /// </summary>
        public string Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }
        /// <summary>
        /// 性别ID（必填：0男，1女）
        /// </summary>
        public string gender_Id
        {
            get { return this.gender_id; }
            set { this.gender_id = value; }
        }
        /// <summary>
        /// 称呼ID（必填：1先生，2女士，3小姐，4教授）
        /// </summary>
        public string salutation_Id
        {
            get { return this.salutation_id; }
            set { this.salutation_id = value; }
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
        /// 民族
        /// </summary>
        public string Nation
        {
            get { return this.nation; }
            set { this.nation = value; }
        }

        /// <summary>
        /// 身份证有效期
        /// </summary>
        public string Availability
        {
            get { return this.availability; }
            set { this.availability = value; }
        }

        /// <summary>
        /// 允许此人通过的管理员
        /// </summary>
        public string Admin
        {
            get { return this.admin; }
            set { this.admin = value; }
        }
        /// <summary>
        /// 姓名拼音
        /// </summary>
        public string NamePy { get; set; }
        /// <summary>
        /// 身份证照片
        /// </summary>
        public byte[] PhotoFromIdCard { get; set; }

        /// <summary>
        /// 摄像头照片
        /// </summary>
        public byte[] PhotoFromCamera { get; set; }

        /// <summary>
        /// 身份证头像--用于存储
        /// </summary>
        public byte[] PhotoFromIdCardSave { get; set; }

        /// <summary>
        /// 摄像头图片--用于存储
        /// </summary>
        public byte[] PhotoFromCameraSave { get; set; }

        /// <summary>
        /// 身份证照片本地存放路径
        /// </summary>
        public string PhotoPathFromIdCard { get; set; }

        /// <summary>
        /// 摄像头照片本地存放路径
        /// </summary>
        public string PhotoPathFromCamera { get; set; }

    }
}
