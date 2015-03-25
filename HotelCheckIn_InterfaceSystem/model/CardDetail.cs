
namespace HotelCheckIn_InterfaceSystem.model
{
   public class CardDetail
    {
        private string card_type_id;
        private string card_no;
        private string user_name;
        private string gender;
        private string id_card_type_id;
        private string id_card_no;

        /// <summary>
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
        /// 用户名
        /// </summary>
        public string user_Name
        {
            get { return this.user_name; }
            set { this.user_name = value; }
        }
        /// <summary>
        /// 称呼（0先生，1小姐）
        /// </summary>
        public string Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }
        /// <summary>
        /// 证件类型ID（11身份证，13户口薄，31工作证，32介绍信，40无证件，41驾照，90军官证，91警官证，92士兵证，93护照，99其他证件）
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
    }
}
