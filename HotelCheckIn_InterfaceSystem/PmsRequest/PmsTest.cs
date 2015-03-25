using System;
using System.Text;

namespace HotelCheckIn_InterfaceSystem.PmsRequest
{
    class PmsTest
    {
        #region CardLogin
        public string cardLogin(string request_id, string cardNo, string password)
        {
            string result = "";
            if ("ID001".Equals(cardNo))
            {       //正确卡号
                if ("1111".Equals(password))
                {   //正确登录
                    result = getCorrectLogin(request_id);
                }
                else
                {   //密码错误
                    result = passWordErr(request_id);
                }
            }
            else if ("ID002".Equals(cardNo))
            {       //未激活卡号
                result = noActiveCard(request_id);
            }
            else
            {       //非法卡号
                result = cardNoErr(request_id);
            }
            return result;
        }
        #endregion

        #region 正确登录
        /// <summary>
        /// 返回正确的登录
        /// </summary>
        /// <returns></returns>
        private string getCorrectLogin(string request_id)
        {
            StringBuilder response = new StringBuilder();
            response.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            response.Append("<pms_response>");
            response.Append("<request_id>" + request_id + "</request_id>");
            response.Append("<card_list>");
            response.Append("<card_detail>");
            response.Append("<card_type_id>01</card_type_id>");
            response.Append("<card_no>ID001</card_no>");
            response.Append("<user_name>刘志诚</user_name>");
            response.Append("<gender>0</gender>");
            response.Append("<id_card_type_id>11</id_card_type_id>");
            response.Append("<id_card_no>342221198809210512</id_card_no>");
            response.Append("<deposit>20000</deposit>");
            response.Append("<card_score>3000</card_score>");
            response.Append("<email>liu653194@vip.qq.com</email>");
            response.Append("<mobile>13625556891</mobile>");
            response.Append("</card_detail>");
            response.Append("</card_list>");
            response.Append("<return_info>");
            response.Append("<return_code></return_code>");
            response.Append("<description></description>");
            response.Append("</return_info>");
            response.Append("</pms_response>");

            return response.ToString();
        }
        #endregion

        #region 非法密码
        private string passWordErr(string request_id)
        {
            StringBuilder response = new StringBuilder();
            response.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            response.Append("<pms_response>");
            response.Append("<request_id>" + request_id + "</request_id>");
            response.Append("<return_info>");
            response.Append("<return_code>error.card.wrong_password</return_code>");
            response.Append("<description>非法密码</description>");
            response.Append("</return_info>");
            response.Append("</pms_response>");

            return response.ToString();
        }
        #endregion

        #region 卡号未激活
        private string noActiveCard(string request_id)
        {
            StringBuilder response = new StringBuilder();
            response.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            response.Append("<pms_response>");
            response.Append("<request_id>" + request_id + "</request_id>");
            response.Append("<return_info>");
            response.Append("<return_code>error.card.not_actived</return_code>");
            response.Append("<description>会员卡未激活</description>");
            response.Append("</return_info>");
            response.Append("</pms_response>");

            return response.ToString();
        }
        #endregion

        #region 非法卡号
        private string cardNoErr(string request_id)
        {
            StringBuilder response = new StringBuilder();
            response.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            response.Append("<pms_response>");
            response.Append("<request_id>" + request_id + "</request_id>");
            response.Append("<return_info>");
            response.Append("<return_code>error.card.wrong_card_no</return_code>");
            response.Append("<description>非法卡号</description>");
            response.Append("</return_info>");
            response.Append("</pms_response>");

            return response.ToString();
        }
        #endregion

        #region 会员卡号查订单
        public string ArrivingListByCard(string request_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>1111</request_id>");
            sb.Append("<allocations>");
            sb.Append("<allocation>");
            sb.Append("<room_order_id>1111</room_order_id>");
            sb.Append("<group_id>111</group_id>");
            sb.Append("<allocation_id>101112160726703561S</allocation_id>");
            sb.Append("<room_id>3769</room_id>");
            sb.Append("<room_type_id>367</room_type_id>");
            sb.Append("<check_in_date>2013-05-18</check_in_date>");
            sb.Append("<check_out_date>2013-05-20</check_out_date>");
            sb.Append("<order_check_in_date>2013-05-18</order_check_in_date>");
            sb.Append("<order_check_out_date>2013-05-20</order_check_out_date>");
            sb.Append("<booker_name>刘志诚</booker_name>");
            sb.Append("<booker_mobile>13625556891</booker_mobile>");
            sb.Append("<booker_time>2013-05-18</booker_time>");
            sb.Append("<rate_code>r001</rate_code>");
            sb.Append("<deposit>0.0</deposit>");
            sb.Append("</allocation>");
            sb.Append("<allocation>");
            sb.Append("<room_order_id>1111</room_order_id>");
            sb.Append("<group_id>111</group_id>");
            sb.Append("<allocation_id>101112160726703562S</allocation_id>");
            sb.Append("<room_id>8203</room_id>");
            sb.Append("<room_type_id>369</room_type_id>");
            sb.Append("<check_in_date>2013-05-18</check_in_date>");
            sb.Append("<check_out_date>2013-05-20</check_out_date>");
            sb.Append("<order_check_in_date>2013-05-18</order_check_in_date>");
            sb.Append("<order_check_out_date>2013-05-20</order_check_out_date>");
            sb.Append("<booker_name>程旭</booker_name>");
            sb.Append("<booker_mobile>13625556891</booker_mobile>");
            sb.Append("<booker_time>2013-05-18</booker_time>");
            sb.Append("<rate_code>r002</rate_code>");
            sb.Append("<deposit>0.0</deposit>");
            sb.Append("</allocation>");
            sb.Append("<allocation>");
            sb.Append("<room_order_id>1111</room_order_id>");
            sb.Append("<group_id>111</group_id>");
            sb.Append("<allocation_id>101112160726703563S</allocation_id>");
            sb.Append("<room_id>8206</room_id>");
            sb.Append("<room_type_id>369</room_type_id>");
            sb.Append("<check_in_date>2013-05-18</check_in_date>");
            sb.Append("<check_out_date>2013-05-20</check_out_date>");
            sb.Append("<order_check_in_date>2013-05-18</order_check_in_date>");
            sb.Append("<order_check_out_date>2013-05-20</order_check_out_date>");
            sb.Append("<booker_name>徐成瑶</booker_name>");
            sb.Append("<booker_mobile>13625556891</booker_mobile>");
            sb.Append("<booker_time>2013-05-18</booker_time>");
            sb.Append("<rate_code>r002</rate_code>");
            sb.Append("<deposit>0.0</deposit>");
            sb.Append("</allocation>");
            sb.Append("<allocation>");
            sb.Append("<room_order_id>1111</room_order_id>");
            sb.Append("<group_id>111</group_id>");
            sb.Append("<allocation_id>101112160726703564S</allocation_id>");
            sb.Append("<room_id>8208</room_id>");
            sb.Append("<room_type_id>369</room_type_id>");
            sb.Append("<check_in_date>2013-05-18</check_in_date>");
            sb.Append("<check_out_date>2013-05-20</check_out_date>");
            sb.Append("<order_check_in_date>2013-05-18</order_check_in_date>");
            sb.Append("<order_check_out_date>2013-05-20</order_check_out_date>");
            sb.Append("<booker_name>张超</booker_name>");
            sb.Append("<booker_mobile>13625556891</booker_mobile>");
            sb.Append("<booker_time>2013-05-18</booker_time>");
            sb.Append("<rate_code>r002</rate_code>");
            sb.Append("<deposit>0.0</deposit>");
            sb.Append("</allocation>");
            sb.Append("</allocations>");
            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }
        #endregion

        public string orderRegist(string request_id)
        {
            if ("111".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_not_exist", "房间不存在");
            }
            else if ("222".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_is_using", "房间正在使用中");
            }
            else if ("333".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.allocation_not_exist", "客房预留不存在");
            }
            else if ("444".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_is_not_fully_avaliable", "登记房间在预入住期间并不完全可售");
            }
            else if ("555".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.no_guests", "请输入登记客人信息或者有效会员卡信息");
            }
            else
            {
                return Correct(request_id);
            }
        }
        #region 正确返回
        private string Correct(string request_id)
        {
            Guid guid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<register_id>" + guid.ToString() + "</register_id>");
            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }
        #endregion
        #region 错误返回
        private string ErrStr(string request_id, string err_code, string msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<return_info>");
            sb.Append("<return_code>" + err_code + "</return_code>");
            sb.Append("<description>" + msg + "</description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }
        #endregion

        public string RoomRateList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>1111</request_id>");
            sb.Append("<room_rates>");
            sb.Append("<room_rate>");
            sb.Append("<room_type_id>367</room_type_id>");
            sb.Append("<room_date>2013-05-18</room_date>");
            sb.Append("<rate_code>WEB</rate_code>");
            sb.Append("<rate>100.0</rate>");
            sb.Append("</room_rate>");
            sb.Append("<room_rate>");
            sb.Append("<room_type_id>369</room_type_id>");
            sb.Append("<room_date>2013-05-18</room_date>");
            sb.Append("<rate_code>WEB</rate_code>");
            sb.Append("<rate>200.0</rate>");
            sb.Append("</room_rate>");
            sb.Append("</room_rates>");
            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string clock_room_rate_list(string request_id, string room_type_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<clock_room_rates>");
            sb.Append("<clock_room_rate>");
            if (string.IsNullOrEmpty(room_type_id))
            {
                sb.Append("<room_type_id>367</room_type_id>");
            }
            else
            {
                sb.Append("<room_type_id>" + room_type_id + "</room_type_id>");
            }
            sb.Append("<rate_code>WEB</rate_code>");
            sb.Append("<hour>2</hour>");
            sb.Append("<rate>60.0</rate>");
            sb.Append("</clock_room_rate>");
            sb.Append("</clock_room_rates>");
            sb.Append("<return_info>");
            sb.Append("<return_code/>");
            sb.Append("<description/>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string clockRegist(string request_id)
        {
            if ("111".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_not_exist", "房间不存在");
            }
            else if ("222".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_is_using", "房间正在使用中");
            }
            else if ("333".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.rate_code_not_exist", "价格代码不存在");
            }
            else if ("444".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.no_room_daily_rate", "无房价");
            }
            else if ("555".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.no_guests", "请输入登记客人信息或者有效会员卡信息");
            }
            else
            {
                return CorrectClock(request_id);
            }
        }

        private string CorrectClock(string request_id)
        {
            Guid guid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<register_id>" + guid.ToString() + "</register_id>");
            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string walkRegist(string request_id)
        {
            if ("111".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_not_exist", "房间不存在");
            }
            else if ("222".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_is_using", "房间正在使用中");
            }
            else if ("333".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.rate_code_not_exist", "价格代码不存在");
            }
            else if ("444".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.room_is_not_fully_avaliable", "登记房间在预入住期间并不完全可售");
            }
            else if ("555".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.no_room_daily_rate", "无房价");
            }
            else if ("666".Equals(request_id))
            {
                return ErrStr(request_id, "error.register.no_guests", "请输入登记客人信息或者有效会员卡信息");
            }
            else
            {
                return CorrectWalk(request_id);
            }
        }

        private string CorrectWalk(string request_id)
        {
            Guid guid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<register_id>" + guid.ToString() + "</register_id>");
            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string get_checking_registers(string request_id)
        {
            Guid guid = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<registers>");
            sb.Append("<register>");
            sb.Append("<register_id>R0809090000077701L</register_id>");
            sb.Append("<room_id>777</room_id>");
            sb.Append("<check_in_date>2013-05-18</check_in_date>");
            sb.Append("<check_in_time>2013-05-18 14:55:12</check_in_time>");
            sb.Append("<check_out_date>2013-05-25</check_out_date>");
            sb.Append("<check_out_time>2013-05-25 12:00:00</check_out_time>");
            sb.Append("<room_order_id></room_order_id>");
            sb.Append("<company_id></company_id>");
            sb.Append("<rate_code>WEB</rate_code>");
            sb.Append("<room_rate>555.0</room_rate>");
            sb.Append("<guest_names>刘志诚</guest_names>");
            sb.Append("<member_card_no>511091198607202148</member_card_no>");
            sb.Append("<biz_source_id>1</biz_source_id>");
            sb.Append("<guest_market_id>4</guest_market_id>");
            sb.Append("<total_fee>2000.0</total_fee>");
            sb.Append("<total_consume>1500.0</total_consume>");
            sb.Append("</register>");

            sb.Append("<register>");
            sb.Append("<register_id>R0809090000077702L</register_id>");
            sb.Append("<room_id>777</room_id>");
            sb.Append("<check_in_date>2013-05-18</check_in_date>");
            sb.Append("<check_in_time>2013-05-18 14:55:12</check_in_time>");
            sb.Append("<check_out_date>2013-05-25</check_out_date>");
            sb.Append("<check_out_time>2013-05-25 12:00:00</check_out_time>");
            sb.Append("<room_order_id></room_order_id>");
            sb.Append("<company_id></company_id>");
            sb.Append("<rate_code>WEB</rate_code>");
            sb.Append("<room_rate>555.0</room_rate>");
            sb.Append("<guest_names>程旭</guest_names>");
            sb.Append("<member_card_no>511091198607202148</member_card_no>");
            sb.Append("<biz_source_id>1</biz_source_id>");
            sb.Append("<guest_market_id>4</guest_market_id>");
            sb.Append("<total_fee>2000.0</total_fee>");
            sb.Append("<total_consume>1500.0</total_consume>");
            sb.Append("</register>");

            sb.Append("</registers>");

            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");

            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string get_room_checking_guests(string request_id, string room_card_no)
        {
            StringBuilder sb = new StringBuilder();
            if ("0003".Equals(room_card_no))
            {
                sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sb.Append("<pms_response>");
                sb.Append("<request_id>" + request_id + "</request_id>");
                sb.Append("<register_info>");
                sb.Append("<register_id>R0809090000077701L</register_id>");
                sb.Append("<register_type>散客</register_type>");
                sb.Append("<room_type>单人间</room_type>");
                sb.Append("<room_no>B 305</room_no>");
                sb.Append("<guest_names>掌上那</guest_names>");
                sb.Append("<check_in_date>2013-05-29</check_in_date>");
                sb.Append("<check_out_date>2013-05-30</check_out_date>");
                sb.Append("<note>登记单备注</note>");
                sb.Append("</register_info>");

                sb.Append("<return_info>");
                sb.Append("<return_code></return_code>");
                sb.Append("<description></description>");
                sb.Append("</return_info>");

                sb.Append("</pms_response>");
            }
            else
            {
                sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sb.Append("<pms_response>");
                sb.Append("<request_id>" + request_id + "</request_id>");
                sb.Append("<return_info>");
                sb.Append("<return_code>error.room.room_card_not_exist</return_code>");
                sb.Append("<description>房间门卡不存在</description>");
                sb.Append("</return_info>");
                sb.Append("</pms_response>");
            }

            return sb.ToString();
        }

        public string check_out_registerStr(string request_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            if ("111".Equals(request_id))
            {
                sb.Append("<return_info>");
                sb.Append("<return_code>error.register.not_exist</return_code>");
                sb.Append("<description>登记单不存在</description>");
                sb.Append("</return_info>");
            }
            else if ("222".Equals(request_id))
            {
                sb.Append("<return_info>");
                sb.Append("<return_code>error.register.has_checkout</return_code>");
                sb.Append("<description>登记单已经退房</description>");
                sb.Append("</return_info>");
            }
            else if ("333".Equals(request_id))
            {
                sb.Append("<return_info>");
                sb.Append("<return_code>error.register.wrong_balance_money</return_code>");
                sb.Append("<description>结账金额不正确</description>");
                sb.Append("</return_info>");
            }
            else
            {
                sb.Append("<register_id>R0809200000078102L</register_id>");
                sb.Append("<return_info>");
                sb.Append("<return_code></return_code>");
                sb.Append("<description></description>");
                sb.Append("</return_info>");
            }
            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string lock_room(string request_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");
            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");
            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string get_avail_room_list(string request_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");

            sb.Append("<avail_rooms>");
            sb.Append("<avail_room>");
            sb.Append("<room_id>3769</room_id>");
            sb.Append("<room_no>8201</room_no>");
            sb.Append("<room_type_id>367</room_type_id>");
            sb.Append("</avail_room>");
            sb.Append("<avail_room>");
            sb.Append("<room_id>8309</room_id>");
            sb.Append("<room_no>8309</room_no>");
            sb.Append("<room_type_id>1</room_type_id>");
            sb.Append("</avail_room>");
            sb.Append("</avail_rooms>");
            sb.Append("<avail_room_count>2</avail_room_count>");

            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");

            sb.Append("</pms_response>");

            return sb.ToString();
        }

        public string get_avail_clock_room_list(string request_id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.Append("<pms_response>");
            sb.Append("<request_id>" + request_id + "</request_id>");

            sb.Append("<avail_rooms>");
            sb.Append("<avail_room>");
            sb.Append("<room_id>8308</room_id>");
            sb.Append("<room_no>a8308</room_no>");
            sb.Append("<room_type_id>1</room_type_id>");
            sb.Append("</avail_room>");
            sb.Append("<avail_room>");
            sb.Append("<room_id>8309</room_id>");
            sb.Append("<room_no>a8309</room_no>");
            sb.Append("<room_type_id>1</room_type_id>");
            sb.Append("</avail_room>");
            sb.Append("</avail_rooms>");
            sb.Append("<avail_room_count>2</avail_room_count>");

            sb.Append("<return_info>");
            sb.Append("<return_code></return_code>");
            sb.Append("<description></description>");
            sb.Append("</return_info>");

            sb.Append("</pms_response>");

            return sb.ToString();
        }

    }
}
