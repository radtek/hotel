namespace CommonLibrary
{
    public class ErrorCode
    {
        /*故障描述	    故障代码
        银行卡模块故障	    101
        发卡机模块故障	    102
        身份证模块故障	    103
        加密键盘故障	    104
        摄像头故障	        105
        人脸识别模块故障	106
        PMS通信故障	        107
        其他	            108
        入钞故障            109
        出钞故障            110
        打印故障            111
         */
        public const string BANKCARD_ERROR = "101";
        public const string SENDCARD_ERROR = "102";
        public const string IDCARD_ERROR = "103";
        public const string PWD_KEYBOARD_ERROR = "104";
        public const string CAMERA_ERROR = "105";
        public const string FACE_RECOGNITION_ERROR = "106";
        public const string PMS_ERROR = "107";
        public const string OTHER_ERROR = "108";
        public const string RC_ERROR = "109";
        public const string CC_ERROR = "110";
        public const string PRINTER_ERROR = "111";

        /**************通用错误，以异常抛出***************/
        public const string EXCEPTION_STR1 = "error.params.wrong_request_xml";         //XML上送包格式错误
        public const string EXCEPTION_STR2 = "error.params.wrong_request_content";     //XML上送包内容错误
        public const string EXCEPTION_STR3 = "error.auth.illegal_user";                //非法用户
        public const string EXCEPTION_STR4 = "error.params.missing_service_name";      //缺少服务名称
        public const string EXCEPTION_STR5 = "error.params.wrong_service_name";        //错误服务名称

        /**************客人登记入住返回错误代码***************/
        public const string REGISTER_ROOM_NOT_EXIST = "error.register.room_not_exist";     //房间不存在
        public const string REGISTER_ROOM_IS_USING = "error.register.room_is_using";       //房间正在使用中
        public const string REGISTER_ALLOCATION_NOT_EXIST = "error.register.allocation_not_exist";     //客房预留不存在
        public const string REGISTER_ROOM_IS_NOT_FULLY_AVALIABLE = "error.register.room_is_not_fully_avaliable";   //登记房间在预入住期间并不完全可售
        public const string REGISTER_NO_GUESTS = "error.register.no_guests";               //请输入登记客人信息或者有效会员卡信息

        public const string REGISTER_RATE_CODE_NOT_EXIST = "error.register.rate_code_not_exist";    //价格代码不存在
        public const string REGISTER_NO_ROOM_DAILY_RATE = "error.register.no_room_daily_rate";      //无房价

        /**************CHECKOUT返回错误代码***************/
        public const string REGISTER_NOT_EXIST = "error.register.not_exist";            //登记单不存在  get_register_bill也会产生此异常
        public const string REGISTER_HAS_CHECKOUT = "error.register.has_checkout";      //登记单已经退房
        public const string REGISTER_WRONG_BALANCE_MONEY = "error.register.wrong_balance_money";    //结账金额不正确

        /**************会员登录返回错误代码***************/
        public const string CARD_WRONG_CARD_NO = "error.card.wrong_card_no";                //非法卡号
        public const string CARD_WRONG_PASSWORD = "error.card.wrong_password";              //非法密码
        public const string CARD_NOT_ACTIVED = "error.card.not_actived";                    //会员卡未激活

        /**************根据门卡号查询登记单错误代码***************/
        public const string ROOM_CARD_NOT_EXIST = "error.room.room_card_not_exist";         //房间门卡不存在

        /**************锁定或解除锁定房间错误代码***************/
        public const string ROOM_NOT_EXIT = "error.room.not_exist";                         //房间不存在

        /**************制作房间门卡错误代码***************/
        public const string MISSING_DOOR_CARD_NO = "room.missing_door_card_no";             //门卡号必填
    }
}
