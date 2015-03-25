using System.Collections.Generic;

namespace HotelCheckIn_InterfaceSystem.model
{
    public class CardDetailList
    {
        private string request_id;
        private List<CardDetail> card_list;
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
        /// 会员卡信息列表
        /// </summary>
        public List<CardDetail> card_List
        {
            get { return this.card_list; }
            set { this.card_list = value; }
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
