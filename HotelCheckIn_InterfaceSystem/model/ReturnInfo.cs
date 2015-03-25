
namespace HotelCheckIn_InterfaceSystem.model
{
    public class ReturnInfo
    {
        private string return_code;
        private string description;

        /// <summary>
        /// 返回代码
        /// </summary>
        public string return_Code
        {
            get { return this.return_code; }
            set { this.return_code = value; }
        }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

    }
}
