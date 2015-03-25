using System;
using System.Collections.Generic;
using System.Xml;
using HotelCheckIn_InterfaceSystem.model;

namespace HotelCheckIn_InterfaceSystem.PmsRequest
{
    public class CreatRequestXml
    {
        private string sob_hotelgroup_id = "";  //集团编码
        private string local = "";              //区域
        private string sob_hotel_id = "";       //酒店编码
        private string sob_code = "";           //用户名
        private string sob_password = "";       //密码

        public CreatRequestXml(AuthenToken auther)
        {
            this.sob_hotelgroup_id = auther.sob_Hotelgroup_Id;
            this.local = auther.Local;
            this.sob_hotel_id = auther.sob_Hotel_Id;
            this.sob_code = auther.sob_Code;
            this.sob_password = auther.sob_Password;
        }

        /// <summary>
        /// 产生get_room_info_list请求的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public string get_room_info_list_xml(string requestId)
        {
            XmlDocument basicDoc = this.createBasicDoc(requestId, "get_room_info_list");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_room_info_list_node = basicDoc.CreateNode(XmlNodeType.Element, "get_room_info_list", "");
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            reqNode.AppendChild(get_room_info_list_node);
            get_room_info_list_node.AppendChild(hotel_id_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_avail_room_list请求的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="room_type_id"></param>
        /// <param name="check_in_date"></param>
        /// <param name="check_out_date"></param>
        /// <returns></returns>
        public string get_avail_room_list_xml(string requestId, string room_type_id, string check_in_date, string check_out_date)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_avail_room_list");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode avail_room_node = basicDoc.CreateNode(XmlNodeType.Element, "get_avail_room_list", "");
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_type_id", "");
            room_type_id_node.InnerText = room_type_id;
            XmlNode check_in_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_in_date", "");
            check_in_date_node.InnerText = check_in_date;
            XmlNode check_out_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_out_date", "");
            check_out_date_node.InnerText = check_out_date;
            reqNode.AppendChild(avail_room_node);
            avail_room_node.AppendChild(hotel_id_node);
            avail_room_node.AppendChild(room_type_id_node);
            avail_room_node.AppendChild(check_in_date_node);
            avail_room_node.AppendChild(check_out_date_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_avail_clock_room_list请求的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local">默认为zh_CN,如果不输则是zh_CN</param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="room_type_id">不输则查全部</param>
        /// <returns></returns>
        public string get_avail_clock_room_list_xml(string requestId, string room_type_id)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_avail_clock_room_list");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_avail_clock_room_list = basicDoc.CreateNode(XmlNodeType.Element, "get_avail_clock_room_list", "");
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_type_id", "");
            room_type_id_node.InnerText = room_type_id;
            reqNode.AppendChild(get_avail_clock_room_list);
            get_avail_clock_room_list.AppendChild(hotel_id_node);
            get_avail_clock_room_list.AppendChild(room_type_id_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_room_rate_list请求xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="room_type_id"></param>
        /// <param name="check_in_date">格式YYYY-MM-DD</param>
        /// <param name="check_out_date"></param>
        /// <param name="rate_codes">选填，多个代码用逗号分隔，不填查询全部价格代码</param>
        /// <returns></returns>
        public string get_room_rate_list_xml(string requestId, string room_type_id, string check_in_date, string check_out_date, string rate_codes)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_room_rate_list");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_room_rate_list_node = basicDoc.CreateNode(XmlNodeType.Element, "get_room_rate_list", "");
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_type_id", "");
            room_type_id_node.InnerText = room_type_id;
            XmlNode check_in_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_in_date", "");
            check_in_date_node.InnerText = check_in_date;
            XmlNode check_out_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_out_date", "");
            check_out_date_node.InnerText = check_out_date;
            XmlNode rate_codes_node = basicDoc.CreateNode(XmlNodeType.Element, "rate_codes", "");
            rate_codes_node.InnerText = rate_codes;
            reqNode.AppendChild(get_room_rate_list_node);
            get_room_rate_list_node.AppendChild(hotel_id_node);
            get_room_rate_list_node.AppendChild(room_type_id_node);
            get_room_rate_list_node.AppendChild(check_in_date_node);
            get_room_rate_list_node.AppendChild(check_out_date_node);
            get_room_rate_list_node.AppendChild(rate_codes_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_clock_room_rate_list请求的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="room_type_id"></param>
        /// <param name="rate_codes">价格代码（选填，多个代码用逗号分隔，不填查询全部价格代码）</param>
        /// <returns></returns>
        public string get_clock_room_rate_list_xml(string requestId, string room_type_id, string rate_codes)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_clock_room_rate_list");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_clock_room_rate_list_node = basicDoc.CreateNode(XmlNodeType.Element, "get_clock_room_rate_list", "");
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_type_id", "");
            room_type_id_node.InnerText = room_type_id;
            XmlNode rate_codes_node = basicDoc.CreateNode(XmlNodeType.Element, "rate_codes", "");
            rate_codes_node.InnerText = rate_codes;
            reqNode.AppendChild(get_clock_room_rate_list_node);
            get_clock_room_rate_list_node.AppendChild(hotel_id_node);
            get_clock_room_rate_list_node.AppendChild(room_type_id_node);
            get_clock_room_rate_list_node.AppendChild(rate_codes_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_card_info请求的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="card_no">会员卡号</param>
        /// <param name="id_card_no">证件号</param>
        /// <returns></returns>
        public string get_card_info_xml(string requestId, string card_no, string id_card_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_card_info");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_card_info_node = basicDoc.CreateNode(XmlNodeType.Element, "get_card_info", "");
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = card_no;
            XmlNode id_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "id_card_no", "");
            id_card_no_node.InnerText = id_card_no;
            reqNode.AppendChild(get_card_info_node);
            get_card_info_node.AppendChild(card_no_node);
            get_card_info_node.AppendChild(id_card_no_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 生成get_arriving_list_by_guest请求的xml/查询酒店今日入住订单列表
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="guest_name">必填</param>
        /// <param name="id_card_no">选填</param>
        /// <param name="mobile">选填</param>
        /// <returns></returns>
        public string get_arriving_list_by_guest_xml(string requestId, string id_card_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_arriving_list_by_guest");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_arriving_list_by_guest_node = basicDoc.CreateNode(XmlNodeType.Element, "get_arriving_list_by_guest", "");

            reqNode.AppendChild(get_arriving_list_by_guest_node);

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            get_arriving_list_by_guest_node.AppendChild(hotel_id_node);
            XmlNode id_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "id_card_no", "");
            id_card_no_node.InnerText = id_card_no;
            get_arriving_list_by_guest_node.AppendChild(id_card_no_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_arriving_list_by_card请求的xml查询酒店今日入住订单列表(通过会员卡信息)
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="card_type_id"></param>
        /// <param name="card_no"></param>
        /// <returns></returns>
        public string get_arriving_list_by_card_xml(string requestId, string card_type_id, string card_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_arriving_list_by_card");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_arriving_list_by_card_node = basicDoc.CreateNode(XmlNodeType.Element, "get_arriving_list_by_card", "");
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "card_type_id", "");
            card_type_id_node.InnerText = card_type_id;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = card_no;
            reqNode.AppendChild(get_arriving_list_by_card_node);
            get_arriving_list_by_card_node.AppendChild(hotel_id_node);
            get_arriving_list_by_card_node.AppendChild(card_type_id_node);
            get_arriving_list_by_card_node.AppendChild(card_no_node);
            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生create_order_register请求的xml/预订客人登记入住
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="regist">实体类</param>
        /// <returns></returns>
        public string create_order_register_xml(string requestId, OrderRegister regist)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "create_order_register");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode create_order_register_node = basicDoc.CreateNode(XmlNodeType.Element, "create_order_register", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = regist.hotel_Id;
            XmlNode room_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_id", "");//1
            room_id_node.InnerText = regist.room_Id;
            XmlNode allocation_id_node = basicDoc.CreateNode(XmlNodeType.Element, "allocation_id", "");//2
            allocation_id_node.InnerText = regist.allocation_Id;
            XmlNode card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "card_type_id", "");//10
            card_type_id_node.InnerText = regist.card_Type_Id;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");//11
            card_no_node.InnerText = regist.card_No;
            XmlNode guest_market_code_node = basicDoc.CreateNode(XmlNodeType.Element, "guest_market_code", "");//12
            guest_market_code_node.InnerText = regist.guest_Market_Code;
            XmlNode deposit_money_node = basicDoc.CreateNode(XmlNodeType.Element, "deposit_money", "");//13
            deposit_money_node.InnerText = regist.deposit_Money;
            XmlNode payment_method_id_node = basicDoc.CreateNode(XmlNodeType.Element, "payment_method_id", "");//14
            payment_method_id_node.InnerText = regist.payment_Method_Id;
            XmlNode credit_card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_type_id", "");//15
            credit_card_type_id_node.InnerText = regist.credit_Card_Type_Id;
            XmlNode credit_card_no = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_no", "");//16
            credit_card_no.InnerText = regist.credit_Card_No;
            reqNode.AppendChild(create_order_register_node);

            create_order_register_node.AppendChild(hotel_id_node);
            create_order_register_node.AppendChild(room_id_node);
            create_order_register_node.AppendChild(allocation_id_node);
            create_order_register_node.AppendChild(card_type_id_node);
            create_order_register_node.AppendChild(card_no_node);
            create_order_register_node.AppendChild(guest_market_code_node);
            create_order_register_node.AppendChild(deposit_money_node);
            create_order_register_node.AppendChild(payment_method_id_node);
            create_order_register_node.AppendChild(credit_card_type_id_node);
            create_order_register_node.AppendChild(credit_card_no);

            XmlNode guests_node = this.createGuestsNode(regist.guest_List, basicDoc);
            create_order_register_node.AppendChild(guests_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生create_walking_register请求的xml/步入客人登记入住
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="regist"></param>
        /// <returns></returns>
        public string create_walking_register_xml(string requestId, OrderRegister wRegist)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "create_walking_register");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode create_walking_register_node = basicDoc.CreateNode(XmlNodeType.Element, "create_walking_register", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = wRegist.hotel_Id;
            XmlNode room_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_id", "");
            room_id_node.InnerText = wRegist.room_Id;
            XmlNode rate_code_node = basicDoc.CreateNode(XmlNodeType.Element, "rate_code", "");
            rate_code_node.InnerText = wRegist.rate_Code;
            XmlNode check_in_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_in_date", "");
            check_in_date_node.InnerText = wRegist.check_In_Date;
            XmlNode check_out_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_out_date", "");
            check_out_date_node.InnerText = wRegist.check_Out_Date;
            XmlNode card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "card_type_id", "");
            card_type_id_node.InnerText = wRegist.card_Type_Id;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = wRegist.card_No;
            XmlNode guest_market_code_node = basicDoc.CreateNode(XmlNodeType.Element, "guest_market_code", "");
            guest_market_code_node.InnerText = wRegist.guest_Market_Code;
            XmlNode deposit_money_node = basicDoc.CreateNode(XmlNodeType.Element, "deposit_money", "");
            deposit_money_node.InnerText = wRegist.deposit_Money;
            XmlNode payment_method_id_node = basicDoc.CreateNode(XmlNodeType.Element, "payment_method_id", "");
            payment_method_id_node.InnerText = wRegist.payment_Method_Id;
            XmlNode credit_card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_type_id", "");
            credit_card_type_id_node.InnerText = wRegist.credit_Card_Type_Id;
            XmlNode credit_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_no", "");
            credit_card_no_node.InnerText = wRegist.credit_Card_No;

            reqNode.AppendChild(create_walking_register_node);
            create_walking_register_node.AppendChild(hotel_id_node);
            create_walking_register_node.AppendChild(room_id_node);
            create_walking_register_node.AppendChild(rate_code_node);
            create_walking_register_node.AppendChild(check_in_date_node);
            create_walking_register_node.AppendChild(check_out_date_node);
            create_walking_register_node.AppendChild(card_type_id_node);
            create_walking_register_node.AppendChild(card_no_node);
            create_walking_register_node.AppendChild(guest_market_code_node);
            create_walking_register_node.AppendChild(deposit_money_node);
            create_walking_register_node.AppendChild(payment_method_id_node);
            create_walking_register_node.AppendChild(credit_card_type_id_node);
            create_walking_register_node.AppendChild(credit_card_no_node);

            XmlNode guests_node = this.createGuestsNode(wRegist.guest_List, basicDoc);
            create_walking_register_node.AppendChild(guests_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生create_clock_register请求的xml/钟点客人登记入住
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="cRegist"></param>
        /// <returns></returns>
        public string create_clock_register_xml(string requestId, OrderRegister cRegist)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "create_clock_register");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode create_clock_register_node = basicDoc.CreateNode(XmlNodeType.Element, "create_clock_register", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = cRegist.hotel_Id;
            XmlNode room_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_id", "");
            room_id_node.InnerText = cRegist.room_Id;
            XmlNode rate_code_node = basicDoc.CreateNode(XmlNodeType.Element, "rate_code", "");
            rate_code_node.InnerText = cRegist.rate_Code;
            XmlNode check_hours_node = basicDoc.CreateNode(XmlNodeType.Element, "check_hours", "");
            check_hours_node.InnerText = cRegist.check_Hours;
            XmlNode card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "card_type_id", "");
            card_type_id_node.InnerText = cRegist.card_Type_Id;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = cRegist.card_No;
            XmlNode guest_market_code_node = basicDoc.CreateNode(XmlNodeType.Element, "guest_market_code", "");
            guest_market_code_node.InnerText = cRegist.guest_Market_Code;
            XmlNode deposit_money_node = basicDoc.CreateNode(XmlNodeType.Element, "deposit_money", "");
            deposit_money_node.InnerText = cRegist.deposit_Money;
            XmlNode payment_method_id_node = basicDoc.CreateNode(XmlNodeType.Element, "payment_method_id", "");
            payment_method_id_node.InnerText = cRegist.payment_Method_Id;
            XmlNode credit_card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_type_id", "");
            credit_card_type_id_node.InnerText = cRegist.credit_Card_Type_Id;
            XmlNode credit_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_no", "");
            credit_card_no_node.InnerText = cRegist.credit_Card_No;

            reqNode.AppendChild(create_clock_register_node);
            create_clock_register_node.AppendChild(hotel_id_node);
            create_clock_register_node.AppendChild(room_id_node);
            create_clock_register_node.AppendChild(rate_code_node);
            create_clock_register_node.AppendChild(check_hours_node);
            create_clock_register_node.AppendChild(card_type_id_node);
            create_clock_register_node.AppendChild(card_no_node);
            create_clock_register_node.AppendChild(guest_market_code_node);
            create_clock_register_node.AppendChild(deposit_money_node);
            create_clock_register_node.AppendChild(payment_method_id_node);
            create_clock_register_node.AppendChild(credit_card_type_id_node);
            create_clock_register_node.AppendChild(credit_card_no_node);

            XmlNode guests_node = this.createGuestsNode(cRegist.guest_List, basicDoc);
            create_clock_register_node.AppendChild(guests_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_checking_registers请求的xml/查询客人在住登记单列表
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="card_no"></param>
        /// <param name="id_card_no"></param>
        /// <param name="room_no">房间号（选填）</param>
        /// <returns></returns>
        public string get_checking_registers_xml(string requestId, string card_no, string id_card_no, string room_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_checking_registers");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_checking_registers_node = basicDoc.CreateNode(XmlNodeType.Element, "get_checking_registers", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = card_no;
            XmlNode id_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "id_card_no", "");
            id_card_no_node.InnerText = id_card_no;
            XmlNode room_no_node = basicDoc.CreateNode(XmlNodeType.Element, "room_no", "");
            room_no_node.InnerText = room_no;

            reqNode.AppendChild(get_checking_registers_node);
            get_checking_registers_node.AppendChild(hotel_id_node);
            get_checking_registers_node.AppendChild(card_no_node);
            get_checking_registers_node.AppendChild(id_card_no_node);
            get_checking_registers_node.AppendChild(room_no_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生is_room_avaliable请求的xml/查询房间在一段时间内是否可用
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="room_id"></param>
        /// <param name="register_id"></param>
        /// <param name="check_in_date"></param>
        /// <param name="check_out_date"></param>
        /// <returns></returns>
        public string is_room_avaliable_xml(string requestId, string room_id, string register_id, DateTime check_in_date, DateTime check_out_date)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "is_room_avaliable");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode is_room_avaliable_node = basicDoc.CreateNode(XmlNodeType.Element, "is_room_avaliable", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_id_node = basicDoc.CreateNode(XmlNodeType.Element, "room_id", "");
            room_id_node.InnerText = room_id;
            XmlNode register_id_node = basicDoc.CreateNode(XmlNodeType.Element, "register_id", "");
            room_id_node.InnerText = register_id;
            XmlNode check_in_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_in_date", "");
            check_in_date_node.InnerText = check_in_date.ToString();
            XmlNode check_out_date_node = basicDoc.CreateNode(XmlNodeType.Element, "check_out_date", "");
            check_out_date_node.InnerText = check_out_date.ToString();

            reqNode.AppendChild(is_room_avaliable_node);
            is_room_avaliable_node.AppendChild(hotel_id_node);
            is_room_avaliable_node.AppendChild(room_id_node);
            is_room_avaliable_node.AppendChild(register_id_node);
            is_room_avaliable_node.AppendChild(check_in_date_node);
            is_room_avaliable_node.AppendChild(check_out_date_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生continue_register请求的xml/登记单延住
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="register_id"></param>
        /// <param name="new_check_out_date"></param>
        /// <param name="deposit_money"></param>
        /// <param name="payment_method_id"></param>
        /// <param name="credit_card_type_id"></param>
        /// <param name="credit_card_no"></param>
        /// <returns></returns>
        public string continue_register_xml(string requestId, string register_id, DateTime new_check_out_date, string deposit_money, string payment_method_id, string credit_card_type_id, string credit_card_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "continue_register");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode continue_register_node = basicDoc.CreateNode(XmlNodeType.Element, "continue_register", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode register_id_node = basicDoc.CreateNode(XmlNodeType.Element, "register_id", "");
            register_id_node.InnerText = register_id;
            XmlNode new_check_out_date_node = basicDoc.CreateNode(XmlNodeType.Element, "new_check_out_date", "");
            new_check_out_date_node.InnerText = new_check_out_date.ToString();
            XmlNode deposit_money_node = basicDoc.CreateNode(XmlNodeType.Element, "deposit_money", "");
            deposit_money_node.InnerText = deposit_money.ToString();
            XmlNode payment_method_id_node = basicDoc.CreateNode(XmlNodeType.Element, "payment_method_id", "");
            payment_method_id_node.InnerText = payment_method_id;
            XmlNode credit_card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_type_id", "");
            credit_card_type_id_node.InnerText = credit_card_type_id;
            XmlNode credit_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_no", "");
            credit_card_no_node.InnerText = credit_card_no;

            reqNode.AppendChild(continue_register_node);
            continue_register_node.AppendChild(hotel_id_node);
            continue_register_node.AppendChild(register_id_node);
            continue_register_node.AppendChild(new_check_out_date_node);
            continue_register_node.AppendChild(deposit_money_node);
            continue_register_node.AppendChild(payment_method_id_node);
            continue_register_node.AppendChild(credit_card_no_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_register_balance请求的xml/查询登记单余额
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="register_id"></param>
        /// <returns></returns>
        public string get_register_balance_xml(string requestId, string register_id)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_register_balance");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_register_balance_node = basicDoc.CreateNode(XmlNodeType.Element, "get_register_balance", "");
            XmlNode register_id_node = basicDoc.CreateNode(XmlNodeType.Element, "register_id", "");
            register_id_node.InnerText = register_id;

            reqNode.AppendChild(get_register_balance_node);
            get_register_balance_node.AppendChild(register_id_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生check_out_register请求的xml/登记单结账退房
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="register_id"></param>
        /// <param name="balance_money"></param>
        /// <param name="payment_method_id"></param>
        /// <param name="credit_card_type_id"></param>
        /// <param name="credit_card_no"></param>
        /// <returns></returns>
        public string check_out_register_xml(string requestId, string register_id, string balance_money, string payment_method_id, string credit_card_type_id, string credit_card_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "check_out_register");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode check_out_register_node = basicDoc.CreateNode(XmlNodeType.Element, "check_out_register", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode register_id_node = basicDoc.CreateNode(XmlNodeType.Element, "register_id", "");
            register_id_node.InnerText = register_id;
            XmlNode balance_money_node = basicDoc.CreateNode(XmlNodeType.Element, "balance_money", "");
            balance_money_node.InnerText = balance_money;
            XmlNode payment_method_id_node = basicDoc.CreateNode(XmlNodeType.Element, "payment_method_id", "");
            payment_method_id_node.InnerText = payment_method_id;
            XmlNode credit_card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_type_id", "");
            credit_card_type_id_node.InnerText = credit_card_type_id;
            XmlNode credit_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "credit_card_no", "");
            credit_card_no_node.InnerText = credit_card_no;

            reqNode.AppendChild(check_out_register_node);
            check_out_register_node.AppendChild(hotel_id_node);
            check_out_register_node.AppendChild(register_id_node);
            check_out_register_node.AppendChild(balance_money_node);
            check_out_register_node.AppendChild(payment_method_id_node);
            check_out_register_node.AppendChild(credit_card_type_id_node);
            check_out_register_node.AppendChild(credit_card_no_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生can_check_out_room请求的xml/询问房间是否可以退房
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="register_id"></param>
        /// <returns></returns>
        public string can_check_out_room_xml(string requestId, string register_id)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "can_check_out_room");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode can_check_out_room_node = basicDoc.CreateNode(XmlNodeType.Element, "can_check_out_room", "");

            XmlNode register_id_node = basicDoc.CreateNode(XmlNodeType.Element, "register_id", "");
            register_id_node.InnerText = register_id;

            reqNode.AppendChild(can_check_out_room_node);
            can_check_out_room_node.AppendChild(register_id_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生get_register_bill请求的xml/查询登记单账单
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <param name="register_id"></param>
        /// <returns></returns>
        public string get_register_bill_xml(string requestId, string register_id)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_register_bill");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_register_bill_node = basicDoc.CreateNode(XmlNodeType.Element, "get_register_bill", "");

            XmlNode register_id_node = basicDoc.CreateNode(XmlNodeType.Element, "register_id", "");
            register_id_node.InnerText = register_id;

            reqNode.AppendChild(get_register_bill_node);
            get_register_bill_node.AppendChild(register_id_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生会员登录 card_login的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="card_no"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string card_login_xml(string requestId, string card_no, string password)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "card_login");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode card_login_info_node = basicDoc.CreateNode(XmlNodeType.Element, "card_login_info", "");

            XmlNode hotelgroup_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotelgroup_id", "");
            hotelgroup_id_node.InnerText = this.sob_hotelgroup_id;
            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = card_no;
            XmlNode password_node = basicDoc.CreateNode(XmlNodeType.Element, "password", "");
            password_node.InnerText = password;
            XmlNode accept_card_types_node = basicDoc.CreateNode(XmlNodeType.Element, "accept_card_types", "");


            reqNode.AppendChild(card_login_info_node);
            card_login_info_node.AppendChild(hotelgroup_id_node);
            card_login_info_node.AppendChild(hotel_id_node);
            card_login_info_node.AppendChild(card_no_node);
            card_login_info_node.AppendChild(password_node);
            card_login_info_node.AppendChild(accept_card_types_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生 根据门卡号查询登记单get_room_checking_guests的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="room_card_no">房间门卡号（必填）</param>
        /// <returns></returns>
        public string get_room_checking_guests_xml(string requestId, string room_card_no)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "get_register_by_room_card_no");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode get_register_by_room_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "get_register_by_room_card_no", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "room_card_no", "");
            room_card_no_node.InnerText = room_card_no;

            reqNode.AppendChild(get_register_by_room_card_no_node);
            get_register_by_room_card_no_node.AppendChild(hotel_id_node);
            get_register_by_room_card_no_node.AppendChild(room_card_no_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生 锁定或解除锁定房间lock_room的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="room_no"></param>
        /// <param name="is_locked"></param>
        /// <returns></returns>
        public string lock_room_xml(string requestId, string room_no, string is_locked)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "lock_room");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode lock_room_info_node = basicDoc.CreateNode(XmlNodeType.Element, "lock_room_info", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_no_node = basicDoc.CreateNode(XmlNodeType.Element, "room_no", "");
            room_no_node.InnerText = room_no;
            XmlNode is_locked_node = basicDoc.CreateNode(XmlNodeType.Element, "is_locked", "");
            is_locked_node.InnerText = is_locked;

            reqNode.AppendChild(lock_room_info_node);
            lock_room_info_node.AppendChild(hotel_id_node);
            lock_room_info_node.AppendChild(room_no_node);
            lock_room_info_node.AppendChild(is_locked_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生 制作房间门卡issue_door_card的xml
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="room_no">房间号（必填）</param>
        /// <param name="card_no">门卡号（必填）</param>
        /// <param name="guest_name">门卡领用客人姓名(必填)</param>
        /// <param name="expire_time">门卡过期时间(YYYY-MM-DD HH:MM:SS，选填，不填为不过期)</param>
        /// <returns></returns>
        public string issue_door_card(string requestId, string room_no, string card_no, string guest_name, string expire_time)
        {
            XmlDataDocument basicDoc = this.createBasicDoc(requestId, "issue_door_card");
            XmlNode reqNode = basicDoc.ChildNodes[1];
            XmlNode issue_door_card_info_node = basicDoc.CreateNode(XmlNodeType.Element, "issue_door_card_info", "");

            XmlNode hotel_id_node = basicDoc.CreateNode(XmlNodeType.Element, "hotel_id", "");
            hotel_id_node.InnerText = this.sob_hotel_id;
            XmlNode room_no_node = basicDoc.CreateNode(XmlNodeType.Element, "room_no", "");
            room_no_node.InnerText = room_no;
            XmlNode card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "card_no", "");
            card_no_node.InnerText = card_no;
            XmlNode guest_name_node = basicDoc.CreateNode(XmlNodeType.Element, "guest_name", "");
            guest_name_node.InnerText = guest_name;
            XmlNode expire_time_node = basicDoc.CreateNode(XmlNodeType.Element, "expire_time", "");
            expire_time_node.InnerText = expire_time;

            reqNode.AppendChild(issue_door_card_info_node);
            issue_door_card_info_node.AppendChild(hotel_id_node);
            issue_door_card_info_node.AppendChild(room_no_node);
            issue_door_card_info_node.AppendChild(card_no_node);
            issue_door_card_info_node.AppendChild(guest_name_node);
            issue_door_card_info_node.AppendChild(expire_time_node);

            return basicDoc.InnerXml;
        }

        /// <summary>
        /// 产生基本的XmlDocument，其他的请求都要用到这个XmlDocument，在此基础上增加节点
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="local"></param>
        /// <param name="sob_hotal_id"></param>
        /// <param name="sob_code"></param>
        /// <param name="sob_password"></param>
        /// <returns></returns>
        private XmlDataDocument createBasicDoc(string requestId, string service_name)
        {
            if (string.IsNullOrEmpty(local))
                local = "zh_CN";
            XmlDataDocument doc = new XmlDataDocument();
            XmlNode topNode = doc.CreateNode(XmlNodeType.XmlDeclaration, "xml", "");
            topNode.Value = "version=\"1.0\" encoding=\"UTF-8\"";
            XmlNode node1 = doc.CreateNode(XmlNodeType.Element, "pms_request", "");
            doc.AppendChild(topNode);
            doc.AppendChild(node1);
            XmlNode node11 = doc.CreateNode(XmlNodeType.Element, "request_id", "");
            node11.InnerText = requestId;
            XmlNode node12 = doc.CreateNode(XmlNodeType.Element, "service_name", "");
            node12.InnerText = service_name;
            XmlNode node13 = doc.CreateNode(XmlNodeType.Element, "locale", "");
            node13.InnerText = this.local;
            XmlNode node14 = doc.CreateNode(XmlNodeType.Element, "authen_token", "");
            node1.AppendChild(node11);
            node1.AppendChild(node12);
            node1.AppendChild(node13);
            node1.AppendChild(node14);
            XmlNode node20 = doc.CreateNode(XmlNodeType.Element, "sob_hotelgroup_id", "");
            node20.InnerText = this.sob_hotelgroup_id;
            XmlNode node21 = doc.CreateNode(XmlNodeType.Element, "sob_hotel_id", "");
            node21.InnerText = this.sob_hotel_id;
            XmlNode node22 = doc.CreateNode(XmlNodeType.Element, "sob_code", "");
            node22.InnerText = this.sob_code;
            XmlNode node23 = doc.CreateNode(XmlNodeType.Element, "sob_password", "");
            node23.InnerText = this.sob_password;
            node14.AppendChild(node20);
            node14.AppendChild(node21);
            node14.AppendChild(node22);
            node14.AppendChild(node23);
            return doc;
        }

        private XmlNode createGuestsNode(List<Guest> list, XmlDataDocument basicDoc)
        {
            XmlNode guests_node = basicDoc.CreateNode(XmlNodeType.Element, "guests", "");

            if (null != list)
            {
                int count = list.Count;
                XmlNode guest_node;
                for (int i = 0; i < count; i++)
                {
                    guest_node = basicDoc.CreateNode(XmlNodeType.Element, "guest", "");

                    XmlNode guest_name_node = basicDoc.CreateNode(XmlNodeType.Element, "guest_name", "");
                    guest_name_node.InnerText = list[i].guest_Name;
                    XmlNode id_card_type_id_node = basicDoc.CreateNode(XmlNodeType.Element, "id_card_type_id", "");
                    id_card_type_id_node.InnerText = list[i].id_Card_Type_Id;
                    XmlNode id_card_no_node = basicDoc.CreateNode(XmlNodeType.Element, "id_card_no", "");
                    id_card_no_node.InnerText = list[i].id_Card_No;
                    XmlNode birthday_node = basicDoc.CreateNode(XmlNodeType.Element, "birthday", "");
                    birthday_node.InnerText = list[i].Birthday;
                    XmlNode gender_id_node = basicDoc.CreateNode(XmlNodeType.Element, "gender_id", "");
                    gender_id_node.InnerText = list[i].gender_Id;
                    XmlNode salutation_id_node = basicDoc.CreateNode(XmlNodeType.Element, "salutation_id", "");
                    salutation_id_node.InnerText = list[i].salutation_Id;
                    XmlNode address_node = basicDoc.CreateNode(XmlNodeType.Element, "address", "");
                    address_node.InnerText = list[i].Address;

                    guest_node.AppendChild(guest_name_node);
                    guest_node.AppendChild(id_card_type_id_node);
                    guest_node.AppendChild(id_card_no_node);
                    guest_node.AppendChild(birthday_node);
                    guest_node.AppendChild(gender_id_node);
                    guest_node.AppendChild(salutation_id_node);
                    guest_node.AppendChild(address_node);

                    guests_node.AppendChild(guest_node);
                }
            }

            return guests_node;
        }

    }
}
