using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_InterfaceSystem.model
{
    public class ClockRoomRateList
    {
        private string request_id;
        private List<ClockRate> clock_room_rate_list;
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
        /// 房价列表
        /// </summary>
        public List<ClockRate> clock_Room_Rate_List
        {
            get { return this.clock_room_rate_list; }
            set { this.clock_room_rate_list = value; }
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
