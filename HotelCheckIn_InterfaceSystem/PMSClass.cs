using System;
using CommonLibrary.exception;
using HotelCheckIn_InterfaceSystem.PmsRequest;
using HotelCheckIn_InterfaceSystem.model;

namespace HotelCheckIn_Interface_PMS
{
    public class PMSClass : PMSInterface
    {
        #region PMSInterface 成员

        private CreatRequestXml cReqXml;
        private SendRequest sendRwquest;
        private AnalysisXml analysisXml = new AnalysisXml();

        private string requestXml = "";
        private string responseXml = "";

        PmsTest pmsTest = new PmsTest();

        /// <summary>
        /// 初始化赋值
        /// </summary>
        /// <param name="auth"></param>
        private void Instance(AuthenToken auth)
        {
            cReqXml = new CreatRequestXml(auth);
            sendRwquest = new SendRequest(auth.request_Url);
        }

        /// <summary>
        /// 查询酒店房间信息
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_no"></param>
        /// <returns></returns>
        public RoomList get_room_info_list(string request_id, AuthenToken auth)
        {
            Instance(auth);
            RoomList roomlist = new RoomList();
            try
            {
                requestXml = cReqXml.get_room_info_list_xml(request_id);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                roomlist = analysisXml.get_room_info_list_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return roomlist;
        }

        /// <summary>
        /// 查询酒店当前可用房间列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <param name="check_in_date">入住日期（必填，格式YYYY-MM-DD）</param>
        /// <param name="check_out_date">离店日期（必填，格式YYYY-MM-DD）</param>
        /// <returns></returns>
        public AvailRooms get_avail_room_list(string request_id, AuthenToken auth, string room_type_id, string check_in_date, string check_out_date)
        {
            Instance(auth);
            AvailRooms result = new AvailRooms();
            try
            {
                requestXml = cReqXml.get_avail_room_list_xml(request_id, room_type_id, check_in_date, check_out_date);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = pmsTest.get_avail_room_list(request_id); //sendRwquest.sendRequest(requestXml);  //
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_avail_room_list_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询酒店当前可用钟点房列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <returns></returns>
        public AvailRooms get_avail_clock_room_list(string request_id, AuthenToken auth, string room_type_id)
        {
            Instance(auth);
            AvailRooms result = new AvailRooms();
            try
            {
                requestXml = cReqXml.get_avail_clock_room_list_xml(request_id, room_type_id);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.get_avail_clock_room_list(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_avail_clock_room_list_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询酒店每日房价列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <param name="check_in_date">入住日期（必填，格式YYYY-MM-DD）</param>
        /// <param name="check_out_date">离店日期（必填，格式YYYY-MM-DD）</param>
        /// <param name="rate_codes">价格代码（选填，多个代码用逗号分隔，不填查询全部价格代码）</param>
        /// <returns></returns>
        public RoomRateList get_room_rate_list(string request_id, AuthenToken auth, string room_type_id, string check_in_date, string check_out_date, string rate_codes)
        {
            Instance(auth);
            RoomRateList result = new RoomRateList();
            try
            {
                requestXml = cReqXml.get_room_rate_list_xml(request_id, room_type_id, check_in_date, check_out_date, rate_codes);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      // pmsTest.RoomRateList();
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_room_rate_list_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询酒店钟点房价列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <param name="rate_codes">价格代码（选填，多个代码用逗号分隔，不填查询全部价格代码）</param>
        /// <returns></returns>
        public ClockRoomRateList get_clock_room_rate_list(string request_id, AuthenToken auth, string room_type_id, string rate_codes)
        {
            Instance(auth);
            ClockRoomRateList result = new ClockRoomRateList();
            try
            {
                requestXml = cReqXml.get_clock_room_rate_list_xml(request_id, room_type_id, rate_codes);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.clock_room_rate_list(request_id, room_type_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_clock_room_rate_list_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询会员卡信息
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_no">会员卡号（选填，会员卡号和证件号必填其一）</param>
        /// <param name="id_card_no">证件号（选填，会员卡号和证件号必填其一）</param>
        /// <returns></returns>
        public CardInfo get_card_info(string request_id, AuthenToken auth, string card_no, string id_card_no)
        {
            Instance(auth);
            CardInfo result = new CardInfo();
            try
            {
                requestXml = cReqXml.get_card_info_xml(request_id, card_no, id_card_no);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_card_info_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询酒店今日入住订单列表(通过证件号)
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="id_card_no">证件号（选填）</param>
        /// <returns></returns>
        public ArrivingList get_arriving_list_by_guest(string request_id, AuthenToken auth, string id_card_no)
        {
            Instance(auth);
            ArrivingList result = new ArrivingList();
            try
            {
                requestXml = cReqXml.get_arriving_list_by_guest_xml(request_id, id_card_no);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_arriving_list_by_guest_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询酒店今日入住订单列表(通过会员卡信息)
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_type_id">会员卡类型ID（必填）</param>
        /// <param name="card_no">会员卡号（必填）</param>
        /// <returns></returns>
        public ArrivingList get_arriving_list_by_card(string request_id, AuthenToken auth, string card_type_id, string card_no)
        {
            Instance(auth);
            ArrivingList result = new ArrivingList();
            try
            {
                requestXml = cReqXml.get_arriving_list_by_card_xml(request_id, card_type_id, card_no);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.ArrivingListByCard(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_arriving_list_by_card_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 预订客人登记入住
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="regist">预订订单</param>
        /// <returns></returns>
        public RegistBack create_order_register(string request_id, AuthenToken auth, OrderRegister regist)
        {
            Instance(auth);
            RegistBack result = new RegistBack();
            try
            {
                requestXml = cReqXml.create_order_register_xml(request_id, regist);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.orderRegist(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.create_order_register_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 步入客人登记入住
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="regist">步入订单</param>
        /// <returns></returns>
        public RegistBack create_walking_register(string request_id, AuthenToken auth, OrderRegister regist)
        {
            Instance(auth);
            RegistBack result = new RegistBack();
            try
            {
                requestXml = cReqXml.create_walking_register_xml(request_id, regist);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.walkRegist(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.create_walking_register_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 钟点客人登记入住
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="regist">钟点订单</param>
        /// <returns></returns>
        public RegistBack create_clock_register(string request_id, AuthenToken auth, OrderRegister regist)
        {
            Instance(auth);
            RegistBack result = new RegistBack();
            try
            {
                requestXml = cReqXml.create_clock_register_xml(request_id, regist);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.clockRegist(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.create_clock_register_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询客人在住登记单列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_no">会员卡号（选填，会员卡号和证件号必填其一）</param>
        /// <param name="id_card_no">证件号（选填，会员卡号和证件号必填其一）</param>
        /// <param name="room_no">房间号（选填）</param>
        /// <returns></returns>
        public RegistList get_checking_registers(string request_id, AuthenToken auth, string card_no, string id_card_no, string room_no)
        {
            Instance(auth);
            RegistList result = new RegistList();
            try
            {
                requestXml = cReqXml.get_checking_registers_xml(request_id, card_no, id_card_no, room_no);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.get_checking_registers(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_checking_registers_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 登记单结账退房
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="register_id">登记单ID（必填）</param>
        /// <param name="balance_money">结账金额(选填，正数表示结账收款，负数表示退款)</param>
        /// <param name="payment_method_id">付款方式ID</param>
        /// <param name="credit_card_type_id">信用卡类型ID</param>
        /// <param name="credit_card_no">信用卡号</param>
        /// <returns></returns>
        public RegistBack check_out_register(string request_id, AuthenToken auth, string register_id, string balance_money, string payment_method_id, string credit_card_type_id, string credit_card_no)
        {
            Instance(auth);
            RegistBack result = new RegistBack();
            try
            {
                requestXml = cReqXml.check_out_register_xml(request_id, register_id, balance_money, payment_method_id, credit_card_type_id, credit_card_no);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.check_out_registerStr(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.check_out_register_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询登记单账单
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="register_id">登记单ID</param>
        /// <returns></returns>
        public RegistAccount get_register_bill(string request_id, AuthenToken auth, string register_id)
        {
            Instance(auth);
            RegistAccount result = new RegistAccount();
            try
            {
                requestXml = cReqXml.get_register_bill_xml(request_id, register_id);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_register_bill_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_no">会员卡号或电子邮箱或手机号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public CardDetailList card_login(string request_id, AuthenToken auth, string card_no, string password)
        {
            Instance(auth);
            CardDetailList result = new CardDetailList();
            try
            {
                requestXml = cReqXml.card_login_xml(request_id, card_no, password);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.cardLogin(request_id, card_no, password);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.card_login_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 根据门卡号查询登记单
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_card_no">房间门卡号（必填）</param>
        /// <returns></returns>
        public RegisterInfo get_room_checking_guests(string request_id, AuthenToken auth, string room_card_no)
        {
            Instance(auth);
            RegisterInfo result = new RegisterInfo();
            try
            {
                requestXml = cReqXml.get_room_checking_guests_xml(request_id, room_card_no);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.get_room_checking_guests(request_id, room_card_no);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.get_room_checking_guests_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 锁定或解除锁定房间
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_no">房间号（必填）</param>
        /// <param name="is_locked">锁定或者解锁(true锁定，false解锁，默认true)</param>
        /// <returns></returns>
        public VoidReturn lock_room(string request_id, AuthenToken auth, string room_no, string is_locked)
        {
            Instance(auth);
            VoidReturn result = new VoidReturn();
            try
            {
                requestXml = cReqXml.lock_room_xml(request_id, room_no, is_locked);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);      //pmsTest.lock_room(request_id);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.lock_room_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        /// <summary>
        /// 制作房间门卡
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_no">房间号（必填）</param>
        /// <param name="card_no">门卡号（必填）</param>
        /// <param name="guest_name">门卡领用客人姓名(必填)</param>
        /// <param name="expire_time">门卡过期时间(YYYY-MM-DD HH:MM:SS，选填，不填为不过期)</param>
        /// <returns></returns>
        public VoidReturn issue_door_card(string request_id, AuthenToken auth, string room_no, string card_no, string guest_name, string expire_time)
        {
            Instance(auth);
            VoidReturn result = new VoidReturn();
            try
            {
                requestXml = cReqXml.issue_door_card(request_id, room_no, card_no, guest_name, expire_time);
            }
            catch (Exception e)
            {
                throw new CreateXmlException("生成xml出错：" + e.Message);
            }
            try
            {
                responseXml = sendRwquest.sendRequest(requestXml);
            }
            catch (Exception e)
            {
                throw new SendRequestException("发送http xml请求出错：" + e.Message);
            }
            try
            {
                result = analysisXml.issue_door_card_analysis(responseXml);
            }
            catch (Exception e)
            {
                throw new AnalysisXmlException("解析返回xml出错：" + e.Message);
            }
            return result;
        }

        #endregion
    }
}
