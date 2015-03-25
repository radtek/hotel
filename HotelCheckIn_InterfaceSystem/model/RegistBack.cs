
namespace HotelCheckIn_InterfaceSystem.model
{
    public class RegistBack
    {
        private string request_id;
        private string register_id;
        private ReturnInfo return_info;

        /// <summary>
        /// 请求ID
        /// </summary>
        public string request_Id
        {
            get { return this.request_id; }
            set { this.request_id = value; }
        }
        /// <summary>
        /// 登记ID
        /// </summary>
        public string register_Id
        {
            get { return this.register_id; }
            set { this.register_id = value; }
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
