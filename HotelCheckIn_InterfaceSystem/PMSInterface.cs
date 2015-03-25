using HotelCheckIn_InterfaceSystem.model;

namespace HotelCheckIn_Interface_PMS
{
    public interface PMSInterface
    {
        /// <summary>
        /// 查询酒店房间信息
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        RoomList get_room_info_list(string request_id, AuthenToken auth);

        /// <summary>
        /// 查询酒店当前可用房间列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <param name="check_in_date">入住日期（必填，格式YYYY-MM-DD）</param>
        /// <param name="check_out_date">离店日期（必填，格式YYYY-MM-DD）</param>
        /// <returns></returns>
        AvailRooms get_avail_room_list(string request_id, AuthenToken auth, string room_type_id, string check_in_date, string check_out_date);

        /// <summary>
        /// 查询酒店当前可用钟点房列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <returns></returns>
        AvailRooms get_avail_clock_room_list(string request_id, AuthenToken auth, string room_type_id);

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
        RoomRateList get_room_rate_list(string request_id, AuthenToken auth, string room_type_id, string check_in_date, string check_out_date, string rate_codes);

        /// <summary>
        /// 查询酒店钟点房价列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_type_id">房型ID（选填，不填查询全部房型）</param>
        /// <param name="rate_codes">价格代码（选填，多个代码用逗号分隔，不填查询全部价格代码）</param>
        /// <returns></returns>
        ClockRoomRateList get_clock_room_rate_list(string request_id, AuthenToken auth, string room_type_id, string rate_codes);

        /// <summary>
        /// 查询会员卡信息
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_no">会员卡号（选填，会员卡号和证件号必填其一）</param>
        /// <param name="id_card_no">证件号（选填，会员卡号和证件号必填其一）</param>
        /// <returns></returns>
        CardInfo get_card_info(string request_id, AuthenToken auth, string card_no, string id_card_no);

        /// <summary>
        /// 查询酒店今日入住订单列表(通过客人姓名和证件号或手机号)
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="id_card_no">证件号（选填）</param>
        /// <returns></returns>
        ArrivingList get_arriving_list_by_guest(string request_id, AuthenToken auth, string id_card_no);

        /// <summary>
        /// 查询酒店今日入住订单列表(通过会员卡信息)
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_type_id">会员卡类型ID（必填）</param>
        /// <param name="card_no">会员卡号（必填）</param>
        /// <returns></returns>
        ArrivingList get_arriving_list_by_card(string request_id, AuthenToken auth, string card_type_id, string card_no);

        /// <summary>
        /// 预订客人登记入住
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="regist">预订订单</param>
        /// <returns></returns>
        RegistBack create_order_register(string request_id, AuthenToken auth, OrderRegister regist);

        /// <summary>
        /// 步入客人登记入住
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="regist">步入订单</param>
        /// <returns></returns>
        RegistBack create_walking_register(string request_id, AuthenToken auth, OrderRegister regist);

        /// <summary>
        /// 钟点客人登记入住
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="regist">钟点订单</param>
        /// <returns></returns>
        RegistBack create_clock_register(string request_id, AuthenToken auth, OrderRegister regist);

        /// <summary>
        /// 查询客人在住登记单列表
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_no">会员卡号（选填，会员卡号和证件号必填其一）</param>
        /// <param name="id_card_no">证件号（选填，会员卡号和证件号必填其一）</param>
        /// <param name="room_no">房间号（选填）</param>
        /// <returns></returns>
        RegistList get_checking_registers(string request_id, AuthenToken auth, string card_no, string id_card_no, string room_no);

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
        RegistBack check_out_register(string request_id, AuthenToken auth, string register_id, string balance_money, string payment_method_id, string credit_card_type_id, string credit_card_no);

        /// <summary>
        /// 查询登记单账单
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="register_id">登记单ID</param>
        /// <returns></returns>
        RegistAccount get_register_bill(string request_id, AuthenToken auth, string register_id);

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="card_no">会员卡号或电子邮箱或手机号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        CardDetailList card_login(string request_id, AuthenToken auth, string card_no, string password);

        /// <summary>
        /// 根据门卡号查询登记单
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_card_no">房间门卡号（必填）</param>
        /// <returns></returns>
        RegisterInfo get_room_checking_guests(string request_id, AuthenToken auth, string room_card_no);

        /// <summary>
        /// 锁定或解除锁定房间
        /// </summary>
        /// <param name="request_id"></param>
        /// <param name="auth"></param>
        /// <param name="room_no">房间号（必填）</param>
        /// <param name="is_locked">锁定或者解锁(true锁定，false解锁，默认true)</param>
        /// <returns></returns>
        VoidReturn lock_room(string request_id, AuthenToken auth, string room_no, string is_locked);

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
        VoidReturn issue_door_card(string request_id, AuthenToken auth, string room_no, string card_no, string guest_name, string expire_time);

    }
}
