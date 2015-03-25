
namespace HotelCheckIn_InterfaceSystem.model
{
    public class ClockRate
    {
        private string room_type_id;            //房间类型ID
        private string room_code;               //价格代码
        private string hour;                    //钟点小时数
        private string rate;                    //房价

        /// <summary>
        /// 房间类型ID
        /// </summary>
        public string room_Type_Id
        {
            get { return this.room_type_id; }
            set { this.room_type_id = value; }
        }
        /// <summary>
        /// 价格代码
        /// </summary>
        public string room_Code
        {
            get { return this.room_code; }
            set { this.room_code = value; }
        }
        /// <summary>
        /// 钟点小时数
        /// </summary>
        public string Hour
        {
            get { return this.hour; }
            set { this.hour = value; }
        }
        /// <summary>
        /// 房价
        /// </summary>
        public string Rate
        {
            get { return this.rate; }
            set { this.rate = value; }
        }
    }
}
