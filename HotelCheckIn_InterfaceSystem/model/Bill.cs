
namespace HotelCheckIn_InterfaceSystem.model
{
    public class Bill
    {

        private string account_type_name;
        private string payment;
        private string credit;
        private string debit;
        private string money;
        private string created_at;

        /// <summary>
        /// 类型
        /// </summary>
        public string account_Type_Name
        {
            get { return this.account_type_name; }
            set { this.account_type_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Payment
        {
            get { return this.payment; }
            set { this.payment = value; }
        }
        /// <summary>
        /// 贷方金额
        /// </summary>
        public string Credit
        {
            get { return this.credit; }
            set { this.credit = value; }
        }
        /// <summary>
        /// 借方金额
        /// </summary>
        public string Debit
        {
            get { return this.debit; }
            set { this.debit = value; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public string Money
        {
            get { return this.money; }
            set { this.money = value; }
        }
        /// <summary>
        /// 2011-11-11 12:11:22时间
        /// </summary>
        public string created_At
        {
            get { return this.created_at; }
            set { this.created_at = value; }
        }
    }
}
