using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_InterfaceSystem.model
{
   public class ArrivingList
    {

        private string request_id;
        private List<Allocation> allocation_list;
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
        /// 订单列表
        /// </summary>
        public List<Allocation> allocation_List
        {
            get { return this.allocation_list; }
            set { this.allocation_list = value; }
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
