﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Services;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Common;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using HotelCheckIn_Interface_Hardware.PMS;
using HotelCheckIn_Interface_PMS;
using HotelCheckIn_InterfaceSystem.model;
using log4net;
using HearBeatPara = HotelCheckIn_BackSystem.DataService.Model.Parameter.HearBeatPara;
using HeartBeatUpload = HotelCheckIn_BackSystem.PlatServ.HeartBeatUpload;
using HotelCheckIn_BackSystem.DataService.BLL;
using InvokeResultData = HotelCheckIn_Interface_Hardware.PMS.InvokeResultData;

namespace HotelCheckIn_BackSystem.DataService.WebService
{
    /// <summary>
    /// InterFace 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class InterFace : System.Web.Services.WebService
    {
        private static ILog log = LogManager.GetLogger("InterFace");
        private readonly InternetGroupBll _igroupbll = new InternetGroupBll();
        private readonly CheckinBll _checkinbll = new CheckinBll();
        private readonly MachineInfo _machine = new MachineInfo();
        readonly string _timespan = ConfigurationManager.AppSettings["heartbeatdt"];
        private readonly AuthenToken _authen = new AuthenToken();
        readonly RoomLockBll _roomLockBll = new RoomLockBll();
        readonly CheckNoBll _checkNoBll = new CheckNoBll();
        readonly IoJournalBll _ioJournalBll = new IoJournalBll();
        readonly PMSInterface _pms = new PMSClass();
        private readonly PmsDataService _pmsDataService;
        private string RequestId = new Random().Next().ToString();
        private readonly string _checkInMInTime = ConfigurationSettings.AppSettings["check_in_time"];
        private readonly string _checkOutTime = ConfigurationSettings.AppSettings["check_out_time"];
        private readonly int daycounts = int.Parse(ConfigurationSettings.AppSettings["daycounts"]);
        private string freeRoomType = ConfigurationSettings.AppSettings["free_room_type"];

        public InterFace()
        {
            _pmsDataService = new PmsDataService(XmlHelper.ReadNode("pmsurl"));
        }

        /// <summary>
        /// 反馈信息
        /// </summary>
        /// <param name="success"></param>
        /// <param name="errorcode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string GetMsg(int success, string errorcode, string msg)
        {
            return "<?xml version='1.0' encoding='utf-8'?><root><success>" + success + "</success><errorcode>"
                   + errorcode + "</errorcode><msg>" + msg + "</msg></root>";
        }

        /// <summary>
        /// 入住客户开房信息,上传到PMS之前要先解锁房间，才能提交订单。
        /// </summary>
        /// <param name="checkIn"></param>
        /// <returns></returns>
        [WebMethod]
        public string Checkin(CheckInModel checkIn)
        {
            log.Debug("入住信息上传开始。。。");
            List<byte[]> images = checkIn.Images;

            string msg = "";
            _machine.JqId = checkIn.MacId;
            DataTable ds = _checkinbll.Query(_machine);
            int count = ds.Rows.Count;

            if (count <= 0)
            {
                return GetMsg(0, constant.CHECK_CODE, "");
            }
            CheckinInfo checkin = new CheckinInfo();
            uploadPhoto upload = new uploadPhoto();
            if (!(string.IsNullOrEmpty(checkIn.OrderId) || string.IsNullOrEmpty(checkIn.RoomNum) || string.IsNullOrEmpty(checkIn.RoomType) ||
                string.IsNullOrEmpty(checkIn.Building) || string.IsNullOrEmpty(checkIn.RoomCode) || checkIn.RoomRate <= 0.0 ||
                 string.IsNullOrEmpty(checkIn.VipCardNum) || string.IsNullOrEmpty(checkIn.VipCardType)
                || checkIn.Name.Length > 0 || checkIn.Sex.Length > 0 || checkIn.IdentityCardNum.Length > 0
                || checkIn.PeopleNum <= 0 || checkIn.CheckInTime != DateTime.MinValue || checkIn.CheckOutTime != DateTime.MinValue
                || checkIn.CheckInTime >= checkIn.CheckOutTime || string.IsNullOrEmpty(checkIn.MacId) || checkIn.OrderTime != DateTime.MinValue))
            {
                return GetMsg(0, constant.PARAM_CHECK, "");
            }
            checkin.Id = Guid.NewGuid().ToString();
            checkin.OrderId = checkIn.OrderId;
            checkin.CheckinCode = checkIn.CheckinCode;
            checkin.RoomNum = checkIn.RoomNum;
            checkin.RoomType = checkIn.RoomType;
            checkin.Building = checkIn.Building;
            checkin.RoomCode = checkIn.RoomCode;
            checkin.RoomRate = checkIn.RoomRate;
            checkin.ViPcardNum = checkIn.VipCardNum;
            checkin.ViPcardType = checkIn.VipCardType;
            checkin.PeopleNum = checkIn.PeopleNum;
            checkin.CheckinTime = checkIn.CheckInTime;
            checkin.CheckOutTime = checkIn.CheckOutTime;
            checkin.HotelId = checkIn.HotelId;
            checkin.MacId = checkIn.MacId;
            checkin.OrderTime = checkIn.OrderTime;
            checkin.InternetGroup = checkIn.InternetGroup;
            checkin.CheckId = checkIn.CheckId;
            checkin.PhoneNumber = checkIn.PhoneNumber;
            checkin.CardCount = checkIn.CardCount;
            checkin.Images = checkIn.Images;
            checkin.TimeType = checkIn.TimeType;
            checkin.IndividualOrGroup = checkIn.IndividualOrGroup;
            checkin.OrderType = (E26)Enum.Parse(typeof(E26), checkIn.OrderType);
            checkin.OrderTime = checkIn.OrderTime;
            int number = Convert.ToInt32(checkin.PeopleNum);
            //-----入住客人信息--------
            string[] Name = checkIn.Name;
            string[] Sex = checkIn.Sex;
            string[] IDcardnumber = checkIn.IdentityCardNum;
            string[] IdentificationType = checkIn.IdentificationType;
            string[] nation = checkIn.Nation;
            string[] adress = checkIn.Adrress;
            DateTime[] Birthday = checkIn.Birthday;
            string[] NamePy = checkIn.NamePy;
            //----入住订单支付信息------
            string[] PayType = checkIn.PayType;
            string[] PayWay = checkIn.PayWay;
            float[] PaymentAmount = checkIn.PaymentAmount;
            string[] HepPayGuid = checkIn.HepPayGuid;
            string[] Batch = checkIn.Batch;
            string[] JobNumberPmsCode = checkIn.JobNumberPmsCode;
            string[] CardNum = checkIn.CardNum;
            string[] Validity = checkIn.Validity;
            DateTime[] OperTime = checkIn.OperTime;
            string[] dealType = checkIn.TransactionType;

            int length_name = Name.Length;
            int length_ID = IDcardnumber.Length;
            int length_sex = Sex.Length;
            if (length_name != length_ID || length_ID != length_sex || length_sex != number)
            {
                return GetMsg(0, constant.PARAM_CHECK, "");
            }
            List<CustomerInfo> customList = new List<CustomerInfo>();
            //循环判断number个客户信息文件夹是否存在并存盘
            for (int i = 0; i < number; i++)
            {
                //保存客户身份信息
                CustomerInfo customer = new CustomerInfo();
                string[] IDcardpathandname =
                    (upload.IDcardImage(checkin.MacId, checkin.OrderId, checkin.CheckinTime, IDcardnumber[i])).Split('#');
                //返回身份证图片保存路径
                string[] CMcardpathandname =
                    (upload.CMcardImage(checkin.MacId, checkin.OrderId, checkin.CheckinTime, IDcardnumber[i])).Split('#');
                //返回摄像头图片保存路径

                customer.OrderId = checkin.OrderId;
                customer.IdentityCardNum = IDcardnumber[i];
                customer.Name = Name[i];
                customer.Sex = Sex[i];
                customer.Nation = nation[i];
                customer.Adress = adress[i];
                customer.Birthday = Birthday[i];
                customer.IdentificationType = (E11)Enum.Parse(typeof(E11), IdentificationType[i]);
                customer.NamePy = NamePy[i];
                customer.IdentityCardPhoto = IDcardpathandname[1];
                customer.CameraPhoto = CMcardpathandname[1];
                customList.Add(customer);
                byte[] IDimage = null;
                byte[] Cimage = null;
                if (null != checkin.Images && checkin.Images.Count > 0)//1
                {
                    if (null != checkin.Images[i])
                    {
                        IDimage = checkin.Images[i];
                    }
                    if (null != checkin.Images[i + 1] && number == 1)
                    {
                        customList[i].CheckIDcard = 1;
                        Cimage = checkin.Images[i + 1];
                    }
                    if (number > 1)
                    {
                        if (null != checkin.Images[i + 2])
                        {
                            customList[i].CheckIDcard = 1;
                            Cimage = checkin.Images[i + 2];
                        }
                    }
                    if (null == Cimage || Cimage.Length <= 0)
                    {
                        customList[i].CheckIDcard = 0;
                        Cimage = File.ReadAllBytes(Server.MapPath("") + "../../../Images/c_default.jpg");
                    }
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(IDimage))
                        {
                            Bitmap bmp = new Bitmap(ms);
                            bmp.Save(IDcardpathandname[0], System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("checkin图片保存失败" + e.Message);
                    }
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(Cimage))
                        {
                            Bitmap bmp = new Bitmap(ms);
                            bmp.Save(CMcardpathandname[0], System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("checkin图片保存失败" + e.Message);
                    }
                }
            }
            try
            {
                bool b = RoomLockSet(checkin.RoomNum, "2", checkin.PhoneNumber, checkIn.CheckId, checkin.MacId);
                if (!b)
                {
                    var error = "解锁房间：" + checkin.RoomNum + "失败。";
                    msg = GetMsg(0, error, "");
                    log.Debug(error);
                    return msg;
                }

                #region PMS query字段说明
                /*
                    Q00：预留；
                    Q01：预留；
                    Q02：酒店 PMS 编码；[要将酒店编码传给 POS 交易备注]
                    Q03：订单 PMS 编码；（来自 303 的 C00，为空时表示无订单的直接入住）
                    Q04：房型 PMS 编码；
                    Q05：房号；
                    Q06：房价码；（PMS 接口专用，可以是房价码）
                    Q07：总房费；
                    Q08：入住日期#成交房价#原始房价|…；
                    Q09：总餐券金额；
                    Q10：入住日期#餐券种类 Guid#餐券数量#餐券单价|…；（免费餐券也在这里）（餐券种类
                        编 Guid 都为 0，是预留字段，接口可以不考虑）
                    Q11：会员卡号#会员级别 PMS 编码；（空时表示非会员）
                    Q12：入住人姓名#证件类型（见 E11）#证件号#民族（文本）#性别（男性|女性|未知）
                        #住址#出生日期（yyyy-M-d）#姓名全拼|…；（第一个入住人是主入住人）
                    Q13：手机号；
                    Q14：支付类型（见 E01）#支付方式（见 E02）#支付金额#Hep 的支付记录 Guid#交易流水号
                        #工号 PMS 编码#操作时间#交易类型（见 E74）#卡号#有效期|…；（交易流水号是用来对账）；
                    Q15：优惠券编号；
                    Q16：发卡数；
                    Q17：设备 Guid；
                    Q18：班别 PMS 编码；
                    Q19：联房入住单 PMS 编码；（空时表示单独入住）
                    Q20：入住时间；（格式：yyyy-MM-dd HH:mm:ss）
                    Q21：离店时间；（格式同上）
                    Q22：时间属性（见 E12）；
                    Q23：散团（1-散客，2-团队）；
                    Q24：下单方式（见 E26）；
                    Q25：下单时间；
                    Q26：房号 PMS 编码；
                 */
                #endregion

                //数字需要去掉，文字需要替换  --预订单编号
                string queryStr = ",,," + checkin.OrderId + "," + checkin.RoomType + "," + checkin.RoomNum + "," + checkin.RoomCode
                    + "," + checkin.RoomRate + "," + checkin.CheckinTime.ToString("yyyy-MM-dd")
                                  + "#" + checkin.RoomRate + "#" + checkin.RoomRate + ",0," + checkin.CheckinTime.ToString("yyyy-MM-dd")
                                  + "#0#0#0,";
                if (!string.IsNullOrEmpty(checkin.ViPcardNum))
                {
                    queryStr += checkin.ViPcardNum + "#" + checkin.ViPcardType;
                }
                queryStr += ",";
                List<string> customStr = new List<string>();
                customList.ForEach(custom =>
                        customStr.Add(custom.Name + "#" + (int)custom.IdentificationType
                        + "#" + custom.IdentityCardNum + "#" + custom.Nation + "#"
                        + custom.Sex + "#" + custom.Adress + "#"
                        + custom.Birthday.ToString("yyyy-MM-dd") + "#pingyin" + custom.NamePy)
                    );

                queryStr = queryStr + string.Join("|", customStr.ToArray()) + "," + checkin.PhoneNumber + ",";

                //支付方式
                List<PayStyle> zhifus = new List<PayStyle>();//支付方式列表，从终端传过来

                for (int i = 0; i < PayType.Length; i++)
                {
                    PayStyle bean = new PayStyle()
                        {
                            PayType = PayType[i],
                            PayWay = PayWay[i],
                            PayMoney = PaymentAmount[i],
                            HepRecordId = HepPayGuid[i],
                            DealRecordId = Batch[i],
                            WorkNumPmsCode = JobNumberPmsCode[i],
                            DealTime = OperTime[i],
                            DealType = dealType[i],
                            CheckinTime = checkin.CheckinTime,
                            CheckoutTime = checkin.CheckOutTime,
                            Id = Guid.NewGuid().ToString(),
                            CheckinId = checkin.Id,
                        };
                    if (CardNum != null && string.IsNullOrEmpty(CardNum[i]))
                    {
                        bean.CardNum = CardNum[i];
                    }
                    if (Validity != null && string.IsNullOrEmpty(Validity[i]))
                    {
                        bean.ValidityTime = Validity[i];
                    }
                    zhifus.Add(bean);
                }

                //保存内容类似："支付类型#支付方式#支付金额#Hep的支付记录Guid#交易流水号#工号PMS编码#操作时间#交易类型#卡号#有效期"
                List<string> zhifuStr = new List<string>();
                zhifus.ForEach(zhifu => zhifuStr.Add(zhifu.PayType + "#" + zhifu.PayWay + "#"
                                            + zhifu.PayMoney + "#" + zhifu.HepRecordId + "#"
                                            + zhifu.DealRecordId + "#" + zhifu.WorkNumPmsCode + "#"
                                            + zhifu.DealTime.ToString("yyyy-MM-dd HH:mm:ss") + "#"
                                            + zhifu.DealType + "#" + zhifu.CardNum + "#" + zhifu.ValidityTime)
                    );

                queryStr = queryStr + string.Join("|", zhifuStr.ToArray()) + ",";
                queryStr = queryStr + "," + checkin.CardCount + "," + checkin.MacId + ",1," + checkin.RelevancyCheckinPmsCode + ","
                           + checkin.CheckinTime.ToString("yyyy-MM-dd HH:mm:ss") + ","
                           + checkin.CheckOutTime.ToString("yyyy-MM-dd HH:mm:ss") + "," + checkin.TimeType + "," + checkin.IndividualOrGroup
                           + "," + (int)checkin.OrderType + "," + checkin.OrderTime.ToString("yyyy-MM-dd HH:mm:ss") + "," + checkin.RoomNum + ",";
                log.Debug("QueryString:" + queryStr);
                /*
                    Message：（只返回一条记录）
                    [{
                    C00：入住单 PMS 编码；
                    C01：Hep 的支付记录 Guid#支付记录 PMS 编码|…；
                    C02：客人证件号#客人入住流水号|…；（规则如：客人入住流水号=入住单 PMS 编码+客人证件号）
                    }]
                 * */
                InvokeResultData resultData = _pmsDataService.CheckIn(queryStr);
                if (resultData.R != "0" || resultData.T != "0")
                {
                    var error = "入住信息上传PMS出错:" + resultData.M[0]["C00"];
                    msg = GetMsg(0, error, "");
                    log.Debug(error);
                    return msg;
                }
                checkin.PmsSign = resultData.M[0]["C00"];//入住PMS订单编码
                checkin.PayRecord = resultData.M[0]["C01"];//支付记录
                checkin.Custom = resultData.M[0]["C02"];//客户信息
                log.Debug("PMS返回结果：checkin.PmsSign:" + checkin.PmsSign + ",checkin.PayRecord:" + checkin.PayRecord + ",checkin.Custom:" + checkin.Custom);
                
                checkin.OrderId = string.IsNullOrEmpty(checkIn.OrderId) ? Guid.NewGuid().ToString() : checkin.OrderId;//如果 OrderId不为空则保存，为空则生成guid
                //现在后台的入住单查询是根据预订订单号查询的，所以暂时没有把预订订单号修改为可空↑
                bool flag = _checkinbll.UploadCheckin(checkin, customList);
                PayStyleBll payStyleBll = new PayStyleBll();
                zhifus.ForEach(payStyleBll.Add);//添加支付记录
                new RoomLockDal().Del(new RoomLockInfo() { CheckId = checkin.CheckId });
                //入住成功，修改“验证码”-“机器验证”状态
                if (!string.IsNullOrEmpty(checkin.CheckId))
                {
                    new CheckNoBll().Modify(new CheckNoInfo() { CheckId = checkin.CheckId, MachineCheck = 2 });
                }
                if (flag)
                {
                    msg = GetMsg(1, "", "");
                }
                else
                {
                    msg = GetMsg(0, constant.ORDER_SAVE, "");
                }
            }
            catch (Exception e)
            {
                msg = GetMsg(0, "checkin订单信息保存出错", "");
                log.Error("checkin订单信息保存出错" + e.Message);
            }
            log.Debug("入住信息上传结束。。。");
            return msg;
        }

        /// <summary>
        /// 终端验证
        /// </summary>
        /// <param name="qcnInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public QueryNoAndPj QueryCheckNo(QueryCheckNoInfo qcnInfo)
        {
            #region 查询是否有预定

            InvokeResultData result = null;
            if (!string.IsNullOrEmpty(qcnInfo.CheckNo))
            {
                result = QueryBookRoom("5", qcnInfo.CheckNo);//根据手机号查询订单
                if (result.R == "0" && result.T == "0" && result.M.Count == 0)
                {
                    return new QueryNoAndPj()
                        {
                            Status = false,
                            Message = "订单未能找到，请到前台办理入住。",
                        };
                }
            }
            else
            {
                result = QueryBookRoom("3", qcnInfo.PhoneNumber);//根据手机号查询订单
            }

            if (result.R != "0")
            {
                return new QueryNoAndPj()
                {
                    Status = false,
                    Message = "查询信息出错，请到前台办理入住。",
                };
            }
            if (result.R == "0" && result.T == "0" && result.M.Count > 0)
            {
                if (result.M.Count > 1) //有预定且预订房间超过一个
                {
                    return new QueryNoAndPj()
                        {
                            IsBook = "m",
                            Status = false,
                            Message = "您预订了多个房间，请到前台办理入住。",
                        };
                }
                else
                {
                    //if (!string.IsNullOrEmpty(result.M[0]["C16"]) || !string.IsNullOrEmpty(result.M[0]["C14"]))//如果是协议价、会员不能入住
                    //{
                    //    return new QueryNoAndPj()
                    //        {
                    //            Status = false,
                    //            Message = "协议价入住或会员客人，请在前台办理。"
                    //        };
                    //}//会员、散客预订可以入住，收现金
                    var roomNo = result.M[0]["C11"];//C11是房号

                    if (DateTime.Now.Date < DateTime.Parse(result.M[0]["C05"]).Date)
                    {
                        return new QueryNoAndPj()
                        {
                            Status = false,
                            Message = "预定日期与当前日期不符。",
                        };
                    }

                    RoomBll roomBll = new RoomBll();
                    DataTable dt = roomBll.Query(new RoomInfo()
                        {
                            RoomNum = roomNo,
                            RoomTypeId = result.M[0]["C09"],
                        });
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        return new QueryNoAndPj()
                            {
                                Status = false,
                                Message = "获取房间信息出错。",
                            };
                    }
                    string breakfasts = result.M[0]["C13"];//早餐数量
                    string[] breakfast = breakfasts.Split('|');//入住日期#早餐份数（见 E51）
                    string note = "";
                    foreach (string s in breakfast)
                    {
                        var ss = s.Split('#');
                        note += ss[0] + "-";
                        switch (int.Parse(ss[1]))
                        {
                            case (int)E51.NotLimited:
                                note += "不限,";
                                break;
                            case (int)E51.SingleBreakfast:
                                note += "单早,";
                                break;
                            case (int)E51.DoubleBreakfase:
                                note += "双早,";
                                break;
                            case (int)E51.FullBreakfast:
                                note += "全早,";
                                break;
                            case (int)E51.Nothing:
                                note += "无,";
                                break;
                            default:
                                note += "出错,";
                                break;
                        }
                    }
                    #region 查询预订单返回内容
                    /*
                     
                    C00：预订单 PMS 编码；（在预订转入住时，会作为 301 的 Q03 传给 PMS 接口）
                    C01：预订人名称；
                    C02：入住人名称；
                    C03：预订人手机号；
                    C04：预订时间；
                    C05：入住时间；
                    C06：离店时间；
                    C07：担保类型（见 E41）；
                    C08：已支付金额；
                    C09：房型 PMS 编码；
                    C10：房间数量；
                    C11：房号|...；（没有分配房号时，返回空字符串）
                    C12：入住日期#成交房价#原始房价#优惠券数量#优惠券类型（见 E72）#优惠值|…；
                    C13：入住日期#早餐份数（见 E51）|…；
                    C14：会员卡号；
                    C15：会员级别 PMS 编码；（跟 C14 同时有值）
                    C16：协议单位名称；
                    C17： 渠道商名称；
                    C18： 订单号；
                    C19： 备注；
                    C20： 入住类型（见E12）；
                    C21： 预付的支付方式（E02）；
                    C22： 联房标记（不联房时，为空字符串）；
                    C23： 是否是主订单（1-是，2-否）；【不联房的就是1】
                    C24： PMS房价码； 
                     
                     * 
                     */
                    #endregion
                    QueryNoAndPj ret = new QueryNoAndPj()
                        {
                            IsBook = "1",
                            Status = true,
                            Message = "",
                            RoomNum = roomNo,
                            Building = dt.Rows[0]["buildingId"].ToString(),
                            Floor = dt.Rows[0]["floor"].ToString(),
                            RoomTypeId = dt.Rows[0]["roomtypeId"].ToString(),
                            RoomTypeName = dt.Rows[0]["roomtype"].ToString(),
                            Towards = dt.Rows[0]["towards"].ToString(),
                            CheckInTime = DateTime.Parse(result.M[0]["C05"]),
                            CheckOutTime = DateTime.Parse(result.M[0]["C06"]),
                            PmsOrderId = result.M[0]["C18"],
                            Note = note,
                        };
                    QueryRoomRateInfo rateInfo = new QueryRoomRateInfo()
                        {
                            PriceSouce = "1",
                            CheckinTime = ret.CheckInTime,
                            CheckoutTime = ret.CheckOutTime,
                            RoomTypeCode = ret.RoomTypeId,
                            PmsRoomRateCode = result.M[0]["C24"],
                        };
                    switch (rateInfo.PmsRoomRateCode.ToUpper())
                    {
                        case "VIP":
                            rateInfo.PriceType = E65.MembershipPrice;
                            break;
                        case "NET":
                            rateInfo.PriceType = E65.NetPrice;
                            break;
                        case "QTJ":
                            rateInfo.PriceType = E65.IndividualPrice;
                            break;
                        default:
                            rateInfo.PriceType = E65.IndividualPrice;
                            break;
                    }
                    string strRate = GetRoomRate(rateInfo);
                    if (string.IsNullOrEmpty(strRate))
                    {
                        return new QueryNoAndPj()
                            {
                                Status = false,
                                Message = "获取房价信息出错。",
                            };
                    }
                    ret.Rate = float.Parse(strRate.Split('|')[0]);
                    ret.RoomRateCode = strRate.Split('|')[1];
                    return ret;
                }
            }

            #endregion

            QueryNoAndPj queryNoAndPj = new QueryNoAndPj();

            if (!string.IsNullOrEmpty(qcnInfo.CheckNo))//有验证码（团购入住）
            {
                #region 有验证码（团购入住）
                CheckNoInfo cni = new CheckNoInfo()
                {
                    CheckId = qcnInfo.CheckNo,
                    InternetGroupId = qcnInfo.GroupId,
                    HotelId = _authen.sob_Hotel_Id,
                };

                DataTable dtCheckNo = _checkNoBll.QueryCheckNo(cni); //查询验证码表
                var machinCheck = 0;
                if (dtCheckNo != null && dtCheckNo.Rows.Count > 0) //验证码存在
                {
                    machinCheck = int.Parse(dtCheckNo.Rows[0]["machinecheck"].ToString());
                }
                else
                {
                    queryNoAndPj.Status = false;
                    queryNoAndPj.Message = "验证码不存在。";
                    log.Info(queryNoAndPj.Message);
                    return queryNoAndPj;
                }
                if (machinCheck == 2) //验证码存在，并且有终端验证状态,终端验证状态1-未入住，2-已入住
                {
                    queryNoAndPj.Status = false;
                    queryNoAndPj.Message = "验证码已使用。";
                    log.Info(queryNoAndPj.Message);
                    return queryNoAndPj;
                }
                else //没有终端验证状态
                {
                    //从PMS取空闲房间信息，并设置锁定（终端、后台、PMS）
                    var checkIdFront = dtCheckNo.Rows[0]["checkid_front"].ToString();
                    var internetGroupId = dtCheckNo.Rows[0]["internetgroupid"].ToString();
                    DataTable dtProject = _igroupbll.QueryProject(new InternetGroupInfo()
                    {
                        ProjectFrontNum = checkIdFront,
                        InternetGroupId = internetGroupId
                    });

                    if (dtProject == null || dtProject.Rows.Count <= 0)
                    {
                        queryNoAndPj.Status = false;
                        queryNoAndPj.Message = "查询房间类型信息出错。";
                        log.Info(queryNoAndPj.Message);
                        return queryNoAndPj;
                    }
                    //查询可用标间房间。
                    var inTime = DateTime.Now;
                    var outTime = DateTime.Now;
                    string[] cit = _checkInMInTime.Split(':');
                    if (cit.Length < 3)
                    {
                        queryNoAndPj.Status = false;
                        queryNoAndPj.Message = "开始入住时间格式错误。";
                        log.Info(queryNoAndPj.Message);
                        return queryNoAndPj;
                    }
                    int h = int.Parse(cit[0]);
                    int m = int.Parse(cit[1]);
                    int s = int.Parse(cit[2]);
                    if (DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(h, m, s)) > 0)
                    {
                        var days = int.Parse(dtCheckNo.Rows[0]["insumdate"].ToString());
                        outTime = DateTime.Parse(DateTime.Now.AddDays(days).ToString("yyyy-MM-dd ") + _checkOutTime);
                    }
                    else
                    {
                        outTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + _checkOutTime);
                    }

                    /***************************************[张威帮助李蕾加的方法-开始]***********************************************/
                    var querystr = ",,," + dtProject.Rows[0]["roomtypeid"] + "," + inTime.ToString("yyyy-MM-dd HH:mm:ss")
                        + "," + outTime.ToString("yyyy-MM-dd HH:mm:ss") + ",,,,,,,,2,";
                    var invokeResultData = _pmsDataService.EnableEmptyRoomList(querystr);
                    if (invokeResultData.R != "0" || invokeResultData.T != "0")
                    {
                        queryNoAndPj.Status = false;
                        queryNoAndPj.Message = "PMS接口出错";
                        return queryNoAndPj;
                    }
                    var ar = invokeResultData.M;
                    if (ar.Count > 0)
                    {
                        Dictionary<string, string> arOne = ar[0];
                        var room = new Room()
                            {
                                room_No = arOne["C02"],
                                room_Type_Id = arOne["C04"],
                                room_Building_Id = arOne["C00"],
                                room_Floor_Id = arOne["C01"]
                            };

                        Room r = room;//总是选择第一个，在多人同时操作时，很可能会冲突。
                        var tmp = dtCheckNo.Rows[0]["CheckIdBeginTime"].ToString();
                        var checkIdBeginTime = tmp.Length > 0 ? DateTime.Parse(tmp) : DateTime.MinValue;
                        tmp = dtCheckNo.Rows[0]["checkIdEndtime"].ToString();
                        var checkIdEndTime = tmp.Length > 0 ? DateTime.Parse(tmp) : DateTime.MinValue;

                        //在PMS，后台锁定房间
                        //querystr = ",,2,1,," + r.room_Building_Id + "," + r.door_Room_No + ",,";
                        //var result = _pmsDataService.LockEmptyRoom(querystr);
                        //if (result.M[0]["C00"] == "2")
                        //{
                        //    queryNoAndPj.Status = false;
                        //    queryNoAndPj.Message = "锁定房间失败";
                        //    log.Error("锁定房间失败");
                        //    return queryNoAndPj;
                        //}
                        //log.Info("锁定PMS房间，返回内容：锁定成功");
                        /***************************************[张威帮助李蕾加的方法-结束]***********************************************/
                        //try
                        //{
                        //    _roomLockBll.Add(new RoomLockInfo()//在后台锁定房间
                        //    {
                        //        HotelId = _authen.sob_Hotel_Id,
                        //        CheckId = qcnInfo.CheckNo,
                        //        LockTime = DateTime.Now,
                        //        MacId = qcnInfo.MacId,
                        //        RoomNum = r.room_No,
                        //    });
                        //}
                        //catch (BusinessException ex)
                        //{
                        //    log.Error(ex.Message);
                        //    queryNoAndPj.Status = false;
                        //    queryNoAndPj.Message = ex.Message;
                        //    return queryNoAndPj;
                        //}
                        //返回房间信息
                        return new QueryNoAndPj()
                        {
                            InSumDate = int.Parse(dtCheckNo.Rows[0]["insumdate"].ToString()),
                            CheckIdBeginTime = checkIdBeginTime,
                            CheckIdEndTime = checkIdEndTime,
                            Building = r.room_Building_Id,
                            Floor = r.room_Floor_Id,
                            RoomTypeId = r.room_Type_Id,
                            RoomTypeName = r.room_Type_Name,
                            RoomNum = r.room_No,
                            Towards = r.room_Direction_Id,
                            CheckInTime = inTime,
                            CheckOutTime = outTime,
                            Status = true,
                            Message = "",
                        };
                    }
                    else
                    {
                        queryNoAndPj.Status = false;
                        queryNoAndPj.Message = "无可用房间。";
                        log.Info(queryNoAndPj.Message);
                        return queryNoAndPj;
                    }
                }
                #endregion
            }
            else if (string.IsNullOrEmpty(qcnInfo.CheckNo) && !string.IsNullOrEmpty(qcnInfo.PhoneNumber))//无验证码，有手机号（散客入住）
            {
                #region 无验证码，有手机号（散客入住）
                DataTable dtRoomLock = _checkNoBll.QueryCheckNoIsKnock(new CheckNoInfo() { PhoneNumber = qcnInfo.PhoneNumber });
                if (dtRoomLock != null && dtRoomLock.Rows.Count > 0) //锁定表有记录，说明该验证码正在锁定房间。
                {
                    RoomBll rb = new RoomBll();
                    DataTable dtRoomLockInfo = rb.Query(new RoomInfo()
                    {
                        RoomNum = dtRoomLock.Rows[0]["roomnum"].ToString(),
                        HotelId = dtRoomLock.Rows[0]["hotelid"].ToString()
                    });
                    if (dtRoomLockInfo != null && dtRoomLockInfo.Rows.Count > 0)
                    {
                        var inTime = DateTime.Now;
                        var outTime = DateTime.Now;
                        if (qcnInfo.CheckinTime != DateTime.MinValue && qcnInfo.CheckoutTime != DateTime.MinValue)
                        {
                            freeRoomType = "";//设置房型为随机
                            inTime = qcnInfo.CheckinTime;
                            outTime = qcnInfo.CheckoutTime;
                        }
                        else
                        {
                            string[] cit = _checkInMInTime.Split(':');
                            if (cit.Length < 3)
                            {
                                queryNoAndPj.Status = false;
                                queryNoAndPj.Message = "开始入住时间格式错误。";
                                log.Info(queryNoAndPj.Message);
                                return queryNoAndPj;
                            }
                            int h = int.Parse(cit[0]);
                            int m = int.Parse(cit[1]);
                            int s = int.Parse(cit[2]);
                            if (DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(h, m, s)) > 0)
                            {
                                var days = daycounts;
                                outTime =
                                    DateTime.Parse(DateTime.Now.AddDays(days).ToString("yyyy-MM-dd ") + _checkOutTime);
                            }
                            else
                            {
                                outTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + _checkOutTime);
                            }
                        }
                        return new QueryNoAndPj()
                        {
                            Building = dtRoomLockInfo.Rows[0]["building"].ToString(),
                            Floor = dtRoomLockInfo.Rows[0]["floor"].ToString(),
                            RoomTypeId = dtRoomLockInfo.Rows[0]["roomtypeId"].ToString(),
                            RoomTypeName = dtRoomLockInfo.Rows[0]["roomtype"].ToString(),
                            RoomNum = dtRoomLockInfo.Rows[0]["roomnum"].ToString(),
                            InSumDate = daycounts,
                            Towards = dtRoomLockInfo.Rows[0]["towards"].ToString(),
                            CheckInTime = inTime,
                            CheckOutTime = outTime,
                            Status = true,
                            Message = "",
                        };
                    }
                    else
                    {
                        log.Error("获取房间信息出错:锁定表有记录，但房间表没有对应记录。");
                        queryNoAndPj.Status = false;
                        queryNoAndPj.Message = "获取房间信息出错。";
                        return queryNoAndPj;
                    }
                }
                else//锁定表没有记录
                {
                    //查询可用房间
                    var inTime = DateTime.Now;
                    var outTime = DateTime.Now;
                    if (qcnInfo.CheckinTime != DateTime.MinValue && qcnInfo.CheckoutTime != DateTime.MinValue)
                    {
                        freeRoomType = "";//设置房型为随机
                        inTime = qcnInfo.CheckinTime;
                        outTime = qcnInfo.CheckoutTime;
                    }
                    else
                    {
                        string[] cit = _checkInMInTime.Split(':');
                        if (cit.Length < 3)
                        {
                            queryNoAndPj.Status = false;
                            queryNoAndPj.Message = "开始入住时间格式错误。";
                            log.Info(queryNoAndPj.Message);
                            return queryNoAndPj;
                        }
                        int h = int.Parse(cit[0]);
                        int m = int.Parse(cit[1]);
                        int s = int.Parse(cit[2]);
                        if (DateTime.Now.TimeOfDay.CompareTo(new TimeSpan(h, m, s)) > 0)
                        {
                            var days = daycounts;
                            outTime = DateTime.Parse(DateTime.Now.AddDays(days).ToString("yyyy-MM-dd ") + _checkOutTime);
                        }
                        else
                        {
                            outTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd ") + _checkOutTime);
                        }
                    }
                    /***************************************[张威帮助李蕾加的方法-开始]***********************************************/
                    var querystr = ",,," + freeRoomType + "," + inTime.ToString("yyyy-MM-dd HH:mm:ss")
                        + "," + outTime.ToString("yyyy-MM-dd HH:mm:ss") + ",,,,,,,,2,";
                    var invokeResultData = _pmsDataService.EnableEmptyRoomList(querystr);
                    var ar = invokeResultData.M;
                    if (ar.Count > 0)
                    {
                        Dictionary<string, string> arOne = ar[0];
                        var room = new Room()
                            {
                                room_No = arOne["C02"],
                                room_Type_Id = arOne["C04"],
                                room_Building_Id = arOne["C00"],
                                room_Floor_Id = arOne["C01"]
                            };
                        Room r = room;//总是选择第一个，在多人同时操作时，很可能会冲突。
                        //在PMS，后台锁定房间
                        //querystr = ",,2,1,," + r.room_Building_Id + "," + r.room_No + ",,";
                        //var result = _pmsDataService.LockEmptyRoom(querystr);
                        //if (result.M[0]["C00"] == "2")
                        //{
                        //    queryNoAndPj.Status = false;
                        //    queryNoAndPj.Message = "锁定房间失败";
                        //    log.Error("锁定房间失败");
                        //    return queryNoAndPj;
                        //}
                        //log.Info("锁定PMS房间，返回内容：锁定成功");
                        /***************************************[张威帮助李蕾加的方法-结束]***********************************************/

                        //try
                        //{
                        //    _roomLockBll.Add(new RoomLockInfo()//在后台锁定房间
                        //    {
                        //        HotelId = _authen.sob_Hotel_Id,//锁定房间所在酒店的ID
                        //        PhoneNumber = qcnInfo.PhoneNumber,
                        //        CheckId = qcnInfo.CheckNo,
                        //        LockTime = DateTime.Now,
                        //        MacId = qcnInfo.MacId,
                        //        RoomNum = r.room_No,
                        //    });
                        //}
                        //catch (BusinessException ex)
                        //{
                        //    log.Error(ex.Message);
                        //    queryNoAndPj.Status = false;
                        //    queryNoAndPj.Message = ex.Message;
                        //    return queryNoAndPj;
                        //}
                        //返回房间信息
                        return new QueryNoAndPj()
                        {
                            InSumDate = daycounts,
                            Building = r.room_Building_Id,
                            Floor = r.room_Floor_Id,
                            RoomTypeName = r.room_Type_Name,
                            RoomTypeId = r.room_Type_Id,
                            RoomNum = r.room_No,
                            Towards = r.room_Direction_Id,
                            CheckInTime = inTime,
                            CheckOutTime = outTime,
                            Status = true,
                            Message = "",
                        };
                    }
                    else
                    {
                        queryNoAndPj.Status = false;
                        queryNoAndPj.Message = "无可用房间。";
                        log.Info(queryNoAndPj.Message);
                        return queryNoAndPj;
                    }
                }
                #endregion
            }
            else
            {
                queryNoAndPj.Status = false;
                queryNoAndPj.Message = "验证码和手机号不能都为空。";
                log.Info(queryNoAndPj.Message);
                return queryNoAndPj;
            }
        }

        /// <summary>
        /// 从PMS获取酒店房间信息，以及房间属性信息
        /// </summary>
        [WebMethod]
        public void UpdateHotel(string hotelId, string macId)
        {
            _authen.sob_Hotel_Id = hotelId;
            RoomList roomList = _pms.get_room_info_list(RequestId, _authen);
            RoomQualityBll roomQualityBll = new RoomQualityBll();
            bool b = roomQualityBll.UpdateRoomAndQualityInfo(roomList, hotelId);
            log.Debug(b ? "更新房间信息已房间属性完成。" : "更新房间信息已房间属性出错。");
        }


        /// <summary>
        /// 心跳上传接口
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [WebMethod]
        public string IHeartBeat_Pt(HearBeatPara para)
        {
            var scurl = para.Url;
            try
            {
                HeartBeatUpload heartBeat = new HeartBeatUpload();
                scurl = heartBeat.IHeartBeat_Pt(new PlatServ.HearBeatPara()
                    {
                        FalutId = para.FalutId,
                        MachineId = para.MachineId,
                        NowDt = para.NowDt,
                        PassWord = para.PassWord,
                        Status = para.Status,
                        Url = para.Url,
                    });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            bool data;
            var localUrl = string.Empty;
            var machineDal = new MachineDal();
            var heartbeatDal = new HeartBeatDal();
            var faultDal = new FaultDal();
            var translaction = new Transaction();
            //1:检测用户名和密码
            var jqid = para.MachineId;
            var password = para.PassWord;
            var falutid = para.FalutId;
            var status = para.Status;
            var url = para.Url;
            var nowdt = para.NowDt;
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(jqid) || string.IsNullOrEmpty(password))
            {
                log.Debug("机器id或密码不能为空！");
                return scurl;
            }
            data = machineDal.IsTimeOut(jqid, now, Convert.ToInt32(_timespan));
            if (!data)
            {
                log.Debug("超过时间限制！");
                return scurl;

            }
            data = machineDal.Exist(new Model.MachineInfo() { JqId = jqid, Password = password }, ref localUrl);
            if (!data)
            {
                log.Debug("信息：未查找到机器id" + jqid + "用户名或密码不正确！");
                return scurl;
            }

            var list = new List<object[]>(); //记录object[]
            var sqls = new List<string>(); //记录sql
            object[] objects = null;

            //添加心跳数据(添加一条记录)
            var sql =
                heartbeatDal.Add(
                    new Heartbeat() { Id = Guid.NewGuid().ToString(), CreateDt = DateTime.Now, MachineId = jqid },
                    ref objects);
            sqls.Add(sql);
            list.Add(objects);

            //修改机器数据(添加心跳时间和机器状态)
            sql = machineDal.Modify(new Model.MachineInfo() { JqId = jqid, HeartbeatDt = now, Status = status },
                                    ref objects);
            sqls.Add(sql);
            list.Add(objects);

            if (!string.IsNullOrEmpty(falutid))
            {
                var falutidlist = falutid.Split('#').ToList();
                foreach (var fil in falutidlist)
                {
                    //添加故障数据
                    sql =
                        faultDal.Add(
                            new Model.Fault
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    MachineId = jqid,
                                    FaultId = fil,
                                    CreateDt = DateTime.Now
                                }, ref objects);
                    sqls.Add(sql);
                    list.Add(objects);
                }

                //修改机器数据
                sql = machineDal.Modify(new Model.MachineInfo { JqId = jqid, FaultId = falutid }, ref objects);
                sqls.Add(sql);
                list.Add(objects);
            }
            //执行事务
            data = translaction.Execute(sqls, list);
            //if (data)
            //{
            //    if (!url.Equals(localUrl))
            //    {
            //        return localUrl;
            //    }
            //}
            return scurl;
        }

        /// <summary>
        /// 添加收支流水账
        /// </summary>
        /// <param name="io"></param>
        /// <returns></returns>
        [WebMethod]
        public DateTime AddIoJournal(IoJournal io)
        {
            var iobean = new IoJournal
                {
                    IoId = io.IoId,
                    IoName = io.IoName,
                    IoMoney = io.IoMoney,
                    IoTag = io.IoTag,
                    IoFrom = io.IoFrom,
                    OrderId = io.OrderId,
                    RoomNo = io.RoomNo,
                    IsUse = io.IsUse,
                    InOrOutCard = 1,
                    IoTime = DateTime.Now,
                };
            if (io.IsUse != 2)
            {
                iobean.IsUse = 1;
            }
            if (!string.IsNullOrEmpty(iobean.IoId))
            {
                DataTable dt = _ioJournalBll.QueryName(iobean.IoId);
                if (dt.Rows.Count > 0)
                {
                    iobean.IoName = dt.Rows[0]["Name"].ToString();
                }
            }
            try
            {
                _ioJournalBll.AddIoJournal(iobean);
                return iobean.IoTime;
            }
            catch (Exception e)
            {
                log.Error(e);
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 退房检查，
        /// </summary>
        /// <param name="roomCardInfo">房卡信息</param>
        /// <returns></returns>
        [WebMethod]
        public string CheckOutExamine(RoomCardInfo roomCardInfo)
        {
            /***************************************[张威帮助李蕾加的方法-开始]***********************************************/
            var querystr = ",,,2," + roomCardInfo.IdCardNum + ",2,";
            var invokeResultData = _pmsDataService.QueryCheckInInfo(querystr);
            var ar = invokeResultData.M;
            if (ar.Count <= 0)//todo：需要修改
            {
                return GetMsg(0, "", "住客信息不存在。");
            }
            DateTime checkoutDate = DateTime.Parse(ar[0]["C03"]);//离店时间，todo：需要修改
            /***************************************[张威帮助李蕾加的方法-结束]***********************************************/
            if (checkoutDate < DateTime.Now)
            {
                return GetMsg(0, "", "退房时间超出预定时间，请去前台退房。");
            }
            //todo：读取卡片内容，从PMS验证是否有效
            return GetMsg(1, "", "");
        }

        /// <summary>
        /// 退房订单登记
        /// </summary>
        /// <param name="brbean"></param>
        /// <returns></returns>
        [WebMethod]
        public ReturnInfo CheckOut(BalanceRegister brbean)
        {
            var registerid = brbean.RegisterId;//todo:这里我们默认为入住单
            var balancemoney = brbean.BalanceMoney;//todo:这里我们默认为支付金额
            var partypeid = brbean.PaymentTypeId;
            var credittypeid = brbean.CreditCardTypeId;
            var creditcard = brbean.CreditCardNo;

            /***************************************[张威帮助李蕾加的方法-开始]***********************************************/
            var querystr = ",,," + registerid + ",1,2#1#" + balancemoney + "###0###,";
            var invokeResultData = _pmsDataService.CheckOut(querystr);
            var ar = invokeResultData.M;
            var returnInfo = new ReturnInfo()
                {
                    Description = ar[0]["C02"],
                    return_Code = ar[0]["C01"]//1:成功，2:失败
                };
            return returnInfo;
            /***************************************[张威帮助李蕾加的方法-结束]***********************************************/
        }

        /// <summary>
        /// 锁定房间设置
        /// </summary>
        /// <param name="roomno">房号</param>
        /// <param name="type">1:上锁,2:解锁</param>
        /// <param name="phone">电话号码</param>
        /// <param name="checkno">团购码</param>
        /// <param name="macid">机器id</param>
        /// <returns></returns>
        [WebMethod]
        public bool RoomLockSet(string roomno, string type, string phone, string checkno, string macid)
        {
            //在PMS，后台锁定房间
            var querystr = ",,2," + type + ",,," + roomno + ",,";
            try
            {
                var result = _pmsDataService.LockEmptyRoom(querystr);
                if (result.M[0]["C00"] == "1")
                {
                    log.Info("锁定PMS房间，锁定成功");
                }
                if (result.M[0]["C00"] == "2")
                {
                    log.Info("锁定PMS房间，锁定失败");
                    return false;
                }
                if (type == "1")
                {
                    _roomLockBll.Add(new RoomLockInfo()//在后台锁定房间
                    {
                        HotelId = _authen.sob_Hotel_Id,
                        PhoneNumber = phone,
                        CheckId = checkno,
                        LockTime = DateTime.Now,
                        MacId = macid,
                        RoomNum = roomno,
                    });
                }
                if (type == "2")
                {
                    _roomLockBll.Dellock(new RoomLockInfo
                        {
                            CheckId = checkno,
                            PhoneNumber = phone,
                            RoomNum = roomno,
                        });
                }
                return true;
            }
            catch (Exception e)
            {
                log.Info("锁定PMS房间，发生错误:" + e.Message);
                return false;
            }
        }


        /// <summary>
        /// 查找订单
        /// PMS 实现逻辑：当查询类型为 6 时，先根据身份证号查询，若找到了结果，就返回，根据
        ///     身份证号找不到订单时，再根据入住人姓名查询（若姓名为空，也无需查询）。
        /// </summary>
        /// <param name="type">查询类型（1-订单号，2-预订人身份证号，3-预订人手机号，4-所有）；</param>
        /// <param name="value">查询值；（查询类型为 4 时，此项为空）</param>
        /// <returns></returns>
        public InvokeResultData QueryBookRoom(string type, string value)
        {
            var querystr = ",,," + type + "," + value + ",,";
            try
            {
                return _pmsDataService.QueryBookRoom(querystr);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询可用房
        /// </summary>
        /// <param name="checkInDate">入住时间</param>
        /// <param name="checkOutDate">离店时间</param>
        /// <returns></returns>
        [WebMethod]
        public List<Room> QueryEmptyRoomList(string roomType, DateTime checkInDate, DateTime checkOutDate)
        {
            var querystr = ",,," + roomType + "," + checkInDate.ToString("yyyy-MM-dd HH:mm:ss")
                           + "," + checkOutDate.ToString("yyyy-MM-dd HH:mm:ss") + ",,,,,,,,2,";
            var roomList = new List<Room>();
            try
            {
                var invokeResultData = _pmsDataService.EnableEmptyRoomList(querystr);
                var ar = invokeResultData.M;
                roomList.AddRange(ar.Select(re => new Room()
                    {
                        room_No = re["C02"],
                        room_Type_Id = re["C04"],
                        room_Building_Id = re["C00"],
                        room_Floor_Id = re["C01"]
                    }));

                var roomQuality = QueryRoomQuality("1");
                roomList.ForEach(room =>
                    {
                        roomQuality.Find(quality =>
                            {
                                if (room.room_Type_Id == quality.Id)
                                {
                                    room.room_Type_Name = quality.Name;
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            });
                    });
                return roomList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 终端检查管理员密码是否合法
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public bool CheckPassword(string password)
        {
            var employerBll = new EmployerBll();
            var dt = employerBll.Query(new Employer()
                {
                    Id = "admin",
                    Password = password,
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 修改机器来源收入是否发卡状态
        /// </summary>
        /// <param name="bean"></param>
        [WebMethod]
        public bool EditIoIfCard(IoJournal bean)
        {
            var ioBll = new IoJournalBll();
            try
            {
                ioBll.EditIoIfCard(new IoJournal()
                {
                    IoTime = bean.IoTime,
                    IoId = bean.IoId,
                    IoFrom = 1,
                    IoTag = 1,
                    InOrOutCard = 2,
                });
                return true;
            }
            catch (Exception e)
            {
                log.Error("修改发卡状态错误：" + e);
                return false;
            }
        }

        /// <summary>
        /// 获取房价
        /// </summary>
        /// <param name="bean">1PMS价格,2配置价格，3团购价格</param>
        /// <returns>格式：房价|房价码</returns>
        [WebMethod]
        public string GetRoomRate(QueryRoomRateInfo bean)
        {
            string rateStr = "";
            if (bean.PriceSouce == "1")
            {
                try
                {
                    string queryStr = "";
                    if (string.IsNullOrEmpty(bean.PmsRoomRateCode))
                    {
                        queryStr = ",,," + bean.CheckinTime.ToString("yyyy-MM-dd HH:mm:ss,")
                                   + bean.CheckinTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss,") + bean.RoomTypeCode +
                                   ",0,0,0,1,0,0,";
                    }
                    else
                    {
                        queryStr = ",,," + bean.CheckinTime.ToString("yyyy-MM-dd HH:mm:ss,")
                                   + bean.CheckinTime.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss,") + bean.RoomTypeCode +
                                   ",0,1,0," + (int)bean.PriceType + ",," + bean.PmsRoomRateCode + ",";
                    }
                    log.Debug(queryStr);
                    InvokeResultData resultData = _pmsDataService.QueryOvernightRates(queryStr);

                    float rate = 0.0f;

                    int days = (bean.CheckoutTime.Date - bean.CheckinTime.Date).Days;
                    string TimeBegin = bean.CheckinTime.ToString("yyyy-MM-dd ") + QueryRztfTime().Split('|')[0];
                    if (bean.CheckinTime < DateTime.Parse(TimeBegin))
                    {
                        days += 1;
                    }
                    rate = float.Parse(resultData.M[0]["C08"]) * days;
                    //resultData.M.ForEach(a => rate += float.Parse(a["C08"]));
                    rateStr = rate.ToString() + "|" + resultData.M[0]["C06"];
                }
                catch (Exception e)
                {
                    log.Debug("从PMS获取房价错误：" + e);
                }
            }
            else if (bean.PriceSouce == "2")
            {
                try
                {
                    rateStr = XmlHelper.ReadNode("room_rate") + "|" + XmlHelper.ReadNode("room_rate_code");
                }
                catch (Exception e)
                {
                    log.Error("获取房价错误：" + e);
                }
            }
            else if (bean.PriceSouce == "3")
            {
                DataTable dt = _checkNoBll.QueryCheckNoAndPj(new CheckNoInfo() { CheckId = bean.ValidationCode });
                if (dt != null && dt.Rows.Count > 0)
                {
                    rateStr = dt.Rows[0]["rate"].ToString() + "|" + dt.Rows[0]["ratecode"].ToString();
                }
            }
            return rateStr;
        }

        /// <summary>
        /// 修改收支记录的isuse状态
        /// </summary>
        [WebMethod]
        public bool EditIoIsUse(IoJournal io)
        {
            var ioBll = new IoJournalBll();
            try
            {
                ioBll.DelIoJournal(new IoJournal()
                {
                    IoTime = io.IoTime,
                    IoId = io.IoId,
                    IsUse = 2
                });
                return true;
            }
            catch (Exception e)
            {
                log.Error("修改收支记录的isuse状态失败：" + e);
                return false;
            }
        }

        /// <summary>
        /// 查询身份验证数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>0：没有数据，1：未验证，2：验证不通过，3：验证通过</returns>
        [WebMethod]
        public int QueryDetect(string id)
        {
            var detectDal = new DetectDal();
            var data = detectDal.QueryDetectStatus(id);
            var status = 0;
            if (data == null)
            {
                return status;
            }
            foreach (var detect in data.Where(detect => detect.Status != null))
            {
                if (detect.Status != null) status = (int)detect.Status;
            }
            return status;
        }

        /// <summary>
        /// 保存身份验证数据
        /// </summary>
        /// <param name="bean"></param>
        /// <returns>true:成功，false:失败</returns>
        [WebMethod]
        public bool SaveDetect(Detect bean)
        {
            var detectDal = new DetectDal();
            try
            {
                var dt = DateTime.Now;
                bean.CreateDt = dt;
                bean.UpdateDt = dt;
                detectDal.Save(bean);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取房间属性
        /// </summary>
        /// <param name="typeId">类型ID</param>
        /// <returns></returns>
        [WebMethod]
        public List<RoomQuality> QueryRoomQuality(string typeId)
        {
            if (!string.IsNullOrEmpty(typeId))
            {
                typeId = "1";//1是房型
            }
            InternetGroupBll internetGroupBll = new InternetGroupBll();
            DataTable dt = internetGroupBll.QueryRoomQuality(new RoomQuality() { TypeId = typeId });
            List<RoomQuality> roomQualities = new List<RoomQuality>();
            foreach (DataRow row in dt.Rows)
            {
                roomQualities.Add(new RoomQuality()
                    {
                        HotelId = row["HotelId"].ToString(),
                        Id = row["Id"].ToString(),
                        Name = row["Name"].ToString(),
                        Note = row["Note"].ToString(),
                        TypeId = row["Typeid"].ToString()
                    });
            }
            return roomQualities;
        }

        /// <summary>
        /// 添加交易信息
        /// </summary>
        /// <param name="tradingInfoModel"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddTradingInfo(TradingInfoModel tradingInfoModel)
        {
            try
            {
                var tradingInfoDal = new TradingInfoDal();
                tradingInfoDal.Add(tradingInfoModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod]
        public string QueryRztfTime()
        {
            return XmlHelper.ReadNode("check_in_time") + "|" + XmlHelper.ReadNode("check_out_time");
        }
    }
}