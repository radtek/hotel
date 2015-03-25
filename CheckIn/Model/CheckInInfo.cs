using System;
using System.Collections.Generic;
using HotelCheckIn_InterfaceSystem.model;


namespace CheckIn.Model
{
    class CheckInInfo
    {
        static CheckInInfo()
        {
            EntranceFlag = "";
            ValidateCode = "";
            RoomType = "";
            RoomTypeName = "";
            RoomNum = "";
            CustomerCount = 1;
            CheckedCount = 0;
            CheckInDateTime = DateTime.Now;
            CheckOutDateTime = DateTime.Now.AddDays(1);
            Note = "";
            GuestList = new List<Guest>();
            CustomImage = new List<byte[]>();
            Dt = DateTime.MinValue;
            PhoneNumber = "";
            //===============================
            OrderPmsCode = "";
            RoomCode = "";
            RoomRate = 0.0f;
            KnockDownPrice = 0.0f;
            OriginalPrice = 0.0f;
            VipCardNum = "";
            VipCardType = "";
            PayMethod = "";
            PayType = new List<string>();
            PayWay = new List<string>();
            PaymentAmount = new List<float>();
            HepPayGuid = new List<string>();
            Batch = new List<string>();
            JopbNumberPmsCode = new List<string>();
            OperTime = new List<DateTime>();
            TransactionType = new List<string>();
            CardNum = new List<string>();
            Validity = new List<string>();
            Coupon = "";
            CardCount = 0;
            ClassPmsCode = "";
            RelevancyCheckinPmsCode = "";
            TimeType = "0";
            IndividualOrGroup = "";
            OrderType = "0";
            OrderTime = DateTime.MinValue;
            RoomNumPmsCode = "";
            //===============================
            BookMark = "";
        }

        public static void Clear()
        {
            EntranceFlag = "";
            ValidateCode = "";
            RoomType = "";
            RoomTypeName = "";
            RoomNum = "";
            CustomerCount = 1;
            CheckedCount = 0;
            CheckInDateTime = DateTime.Now;
            CheckOutDateTime = DateTime.Now.AddDays(1);
            Note = "";
            GuestList = new List<Guest>();
            CustomImage = new List<byte[]>();
            Dt = DateTime.MinValue;

            PhoneNumber = "";
            OrderPmsCode = "";
            RoomCode = "";
            RoomRate = 0.0f;
            KnockDownPrice = 0.0f;
            OriginalPrice = 0.0f;
            VipCardNum = "";
            VipCardType = "";
            PayMethod = "";
            PayType = new List<string>();
            PayWay = new List<string>();
            PaymentAmount = new List<float>();
            HepPayGuid = new List<string>();
            Batch = new List<string>();
            JopbNumberPmsCode = new List<string>();
            OperTime = new List<DateTime>();
            TransactionType = new List<string>();
            CardNum = new List<string>();
            Validity = new List<string>();
            Coupon = "";
            CardCount = 0;
            ClassPmsCode = "";
            RelevancyCheckinPmsCode = "";
            TimeType = "0";
            IndividualOrGroup = "";
            OrderType = "0";
            OrderTime = DateTime.MinValue;
            RoomNumPmsCode = "";
            BookMark = "";
        }

        /// <summary>
        /// 清除客人信息
        /// </summary>
        public static void ClearCustomInfo()
        {
            GuestList = new List<Guest>();
            CustomImage = new List<byte[]>();
            CheckedCount = 0;
        }

        /// <summary>
        /// 入口标记
        /// </summary>
        public static string EntranceFlag { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public static string ValidateCode { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public static string PhoneNumber { get; set; }

        /// <summary>
        /// 楼栋
        /// </summary>
        public static string Building { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public static string Floor { get; set; }

        /// <summary>
        /// 朝向
        /// </summary>
        public static string Towards { get; set; }

        /// <summary>
        /// 房间类型
        /// </summary>
        public static string RoomType { get; set; }

        /// <summary>
        /// 房间类型名称
        /// </summary>
        public static string RoomTypeName { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public static string RoomNum { get; set; }

        /// <summary>
        /// 客户人数
        /// </summary>
        public static int CustomerCount { get; set; }

        /// <summary>
        /// 客人身份证照片和摄像头照片
        /// </summary>
        public static List<byte[]> CustomImage { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        public static DateTime CheckInDateTime { get; set; }

        /// <summary>
        /// 退房时间
        /// </summary>
        public static DateTime CheckOutDateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public static string Note { get; set; }

        /// <summary>
        /// 入住客人信息
        /// </summary>
        public static List<Guest> GuestList { get; set; }

        /// <summary>
        /// 已经验证过的客人数量
        /// </summary>
        public static int CheckedCount { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        public static DateTime Dt { get; set; }

        /// <summary>
        /// 预授权类型,1:预授权,2:预授权完成
        /// </summary>
        public static string PreauthType { get; set; }
        
        /// <summary>
        /// 预授权号
        /// </summary>
        public static string PreauthNumber { get; set; }

        /// <summary>
        /// 预订订单PMS编码
        /// </summary>
        public static string OrderPmsCode { get; set; }

        /// <summary>
        /// 入住编码
        /// </summary>
        public static string CheckinCode { get; set; }

        /// <summary>
        /// 房价代码
        /// </summary>
        public static string RoomCode { get; set; }

        /// <summary>
        /// 总房费
        /// </summary>
        public static float RoomRate { get; set; }

        /// <summary>
        /// 成交房价
        /// </summary>
        public static float KnockDownPrice { get; set; }

        /// <summary>
        /// 原始房价
        /// </summary>
        public static float OriginalPrice { get; set; }

        ///<summary>
        /// 会员卡号
        /// </summary>
        public static string VipCardNum { get; set; }

        /// <summary>
        /// 会员级别PMS编码
        /// </summary>
        public static string VipCardType { get; set; }

        /// <summary>
        /// 支付方式：银行卡或现金
        /// </summary>
        public static string PayMethod { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public static List<string> PayType { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public static List<string> PayWay { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public static List<float> PaymentAmount { get; set; }

        /// <summary>
        /// Hep的支付记录GUID
        /// </summary>
        public static List<string> HepPayGuid { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public static List<string> Batch { get; set; }

        /// <summary>
        /// 工号PMS编码
        /// </summary>
        public static List<string> JopbNumberPmsCode { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public static List<DateTime> OperTime { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public static List<string> TransactionType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public static List<string> CardNum { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public static List<string> Validity { get; set; }

        /// <summary>
        /// 优惠券编号
        /// </summary>
        public static string Coupon { get; set; }

        /// <summary>
        /// 发（门）卡数
        /// </summary>
        public static int CardCount { get; set; }

        /// <summary>
        /// 班别PMS编码
        /// </summary>
        public static string ClassPmsCode { get; set; }

        /// <summary>
        /// 关联入住单PMS编码
        /// </summary>
        public static string RelevancyCheckinPmsCode { get; set; }

        /// <summary>
        /// 时间属性
        /// </summary>
        public static string TimeType { get; set; }

        /// <summary>
        /// 散团（1-散客，2-团体）
        /// </summary>
        public static string IndividualOrGroup { get; set; }

        /// <summary>
        /// 下单方式
        /// </summary>
        public static string OrderType { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public static DateTime OrderTime { get; set; }

        /// <summary>
        /// 房号PMS编码
        /// </summary>
        public static string RoomNumPmsCode { get; set; }
        /// <summary>
        /// 预订标记
        /// </summary>
        public static string BookMark { get; set; }

    }
}
