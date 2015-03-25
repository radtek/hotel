using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckIn.BackService;
using CheckIn.Model;
using HotelCheckIn_Interface_Hardware.BankOfCard;

namespace CheckIn.Bll
{
    public class BankCardPaymentCall
    {
        private readonly RepInfo _repInfo;
        private InterFace _interFace = new InterFace();
        /// <summary> 
        /// 定义委托 
        /// </summary> 
        public delegate void DelegateLog(string sReceived);

        /// <summary> 
        /// 定义一个消息接收事件 
        /// </summary> 
        public event DelegateLog log;

        public BankCardPaymentCall()
        {
            _repInfo = new RepInfo();
        }

        /// <summary>
        /// 程序注册函数
        /// </summary>
        public bool AppsLogin()
        {
            var rescode = new StringBuilder();
            var result = BankCardPaymentService.APPS_Login(1, rescode);
            if (result == 0)
            {
                var data = rescode.ToString().Substring(0, 2);
                if (data == "00")
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 读卡器初始化函数
        /// </summary>
        public bool UmsOpenCard()
        {
            var result = BankCardPaymentService.UMS_OpenCard();
            if (result == 0)
            {
                return true; //todo:成功
            }
            return false;
        }

        /// <summary>
        /// 检测卡函数
        /// </summary>
        public string UmsCheckCard()
        {
            var result = BankCardPaymentService.UMS_CheckCard();
            switch (result)
            {
                case 1: return "读卡器内无卡";
                case 2: return "读卡器内有卡";
                case 3: return "卡在读卡器入口";
                case -1: return "读卡器硬件故障";
                case -2: return "非法数据";
                case -3: return "卡操作失败";
                case -4: return "数据效验错误";
                case -5: return "断电";
                case -6: return "接收到了 IDL";
                case -7: return "超时";
                default: return "";
            }
        }

        /// <summary>
        /// 读卡函数
        /// </summary>
        public bool UmsReadCard()
        {
            var result = BankCardPaymentService.UMS_ReadCard();
            if (result == 0)
            {
                return true; //todo:成功
            }
            return false;
        }

        /// <summary>
        /// 关闭刷卡器
        /// </summary>
        public bool UmsClose()
        {
            var result = BankCardPaymentService.UMS_Close();
            if (result == 0)
            {
                return true; //todo:成功
            }
            return false;
        }

        /// <summary>
        /// 打卡密码键盘
        /// </summary>
        public bool PinOpen()
        {
            var result = BankCardPaymentService.PIN_Open();
            if (result == 0)
            {
                return true; //todo:成功
            }
            return false;
        }

        /// <summary>
        /// 输入密码状态
        /// </summary>
        public string PinReadOneByte()
        {
            var result = BankCardPaymentService.PIN_ReadOneByte();
            switch (result)
            {
                case 0x2A: return "输入了一个字符";
                case 0x08: return "清除";
                case 0x0D: return "确定";
                case 0x02: return "超时";
                case 0x1b: return "取消";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 密码安全函数
        /// </summary>
        public bool PinGetPinValue()
        {
            var result = BankCardPaymentService.PIN_GetPinValue();
            if (result == 0)
            {
                return true; //todo:成功
            }
            return false;
        }

        /// <summary>
        /// 关闭密码键盘函数
        /// </summary>
        public bool PinDestroy()
        {
            var result = BankCardPaymentService.PIN_Destroy();
            if (result == 0)
            {
                return true; //todo:成功
            }
            return false;
        }

        /// <summary>
        /// 消费
        /// </summary>
        public bool UmsPay(string money, ref string bug)
        {
            var bankout = new StringBuilder();
            var str = UmsMoneyConvert(money);
            var bankin = new StringBuilder(str, 100);
            var result = BankCardPaymentService.UMS_PreAuthDone_Dll(bankin, bankout);
            var data = bankout.ToString().Substring(0, 2);
            bug = _repInfo.RepMsg(data);
            if (result == 0)
            {
                return data == "00";
            }
            return false;
        }

        /// <summary>
        /// 预授权
        /// </summary>
        /// <param name="bug">调试出错信息</param>
        /// <returns></returns>
        public bool UmsPreAuth(ref string bug)
        {
            var tradingInfo = new TradingInfoModel();
            var bankOut = new StringBuilder();
            log("预授权金额：" + CheckInInfo.PaymentAmount[0]);
            var str = UmsMoneyConvert(CheckInInfo.PaymentAmount[0].ToString());
            var bankin = new StringBuilder(str + "          ", 100);
            var result = BankCardPaymentService.UMS_PreAuth_Dll(bankin, bankOut);
            var data = bankOut.ToString().Substring(0, 2);
            bug = _repInfo.RepMsg(data);
            log("预授权返回信息：" + bug);
            log("返回全部信息：" + bankOut);
            //-------------------------------------------
            tradingInfo.Id = Guid.NewGuid().ToString();
            tradingInfo.TradingType = "1";
            tradingInfo.RoomNumber = CheckInInfo.RoomNum;
            tradingInfo.CheckOrderNumber = CheckInInfo.CheckinCode;

            tradingInfo.ReturnCode = data;
            log("ReturnCode：" + tradingInfo.ReturnCode);
            tradingInfo.BankNumber = bankOut.ToString().Substring(2, 4);
            log("BankNumber：" + tradingInfo.BankNumber);
            tradingInfo.CardNumber = bankOut.ToString().Substring(6, 20);
            log("CardNumber：" + tradingInfo.CardNumber);
            tradingInfo.Valid = bankOut.ToString().Substring(26, 4);
            log("Valid：" + tradingInfo.Valid);
            tradingInfo.LotNo = bankOut.ToString().Substring(30, 6);
            log("LotNo：" + tradingInfo.LotNo);
            tradingInfo.CertificateNo = bankOut.ToString().Substring(36, 6);
            log("CertificateNo：" + tradingInfo.CertificateNo);
            tradingInfo.Money = bankOut.ToString().Substring(42, 12);
            log("Money：" + tradingInfo.Money);
            tradingInfo.Note = bankOut.ToString().Substring(54, 40);
            log("Note：" + tradingInfo.Note);
            tradingInfo.ContactNo = bankOut.ToString().Substring(94, 15);
            log("ContactNo：" + tradingInfo.ContactNo);
            tradingInfo.TerminalNo = bankOut.ToString().Substring(109, 8);
            log("TerminalNo：" + tradingInfo.TerminalNo);
            tradingInfo.TransactionReferenceNumber = bankOut.ToString().Substring(117, 12);
            log("TransactionReferenceNumber：" + tradingInfo.TransactionReferenceNumber);
            tradingInfo.TradingDate = bankOut.ToString().Substring(129, 4);
            log("TradingDate：" + tradingInfo.TradingDate);
            tradingInfo.TradingTime = bankOut.ToString().Substring(133, 6);
            log("TradingTime：" + tradingInfo.TradingTime);
            tradingInfo.AuthorizationNumber = bankOut.ToString().Substring(139, 6);
            log("AuthorizationNumber：" + tradingInfo.AuthorizationNumber);
            _interFace.AddTradingInfo(tradingInfo);//保存交易信息/-------------------------------------------
            if (result == 0)
            {
                if (data == "00")
                {
                    CheckInInfo.CardNum.Add(tradingInfo.CardNumber.Trim());//卡号
                    CheckInInfo.Validity.Add(tradingInfo.Valid);//有效期
                    var preAuthDate = tradingInfo.TradingDate;//交易日期
                    CheckInInfo.Dt = DateTime.Parse(DateTime.Now.Year + "-" + preAuthDate.Substring(0, 2) + "-" + preAuthDate.Substring(2, 2));
                    CheckInInfo.PreauthNumber = tradingInfo.AuthorizationNumber;//授权号
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 预授权完成
        /// </summary>
        /// <param name="bug"></param>
        /// <returns></returns>
        public bool UmsPreAuthDone(ref string bug)
        {
            var tradingInfo = new TradingInfoModel();
            var bankOut = new StringBuilder();
            var str = UmsMoneyConvert(CheckInInfo.PaymentAmount[0].ToString());
            log("预授权金额：" + CheckInInfo.PaymentAmount[0]);
            var bankIn = new StringBuilder(str + CheckInInfo.Dt.ToString("MMdd") + CheckInInfo.PreauthNumber, 100);
            var result = BankCardPaymentService.UMS_PreAuthDone_Dll(bankIn, bankOut);
            var data = bankOut.ToString().Substring(0, 2);
            bug = _repInfo.RepMsg(data);
            log("预授权完成返回信息：" + bug);
            log("返回全部信息：" + bankOut);
            //-------------------------------------------
            tradingInfo.Id = Guid.NewGuid().ToString();
            tradingInfo.TradingType = "2";
            tradingInfo.RoomNumber = CheckInInfo.RoomNum;//房号
            tradingInfo.CheckOrderNumber = CheckInInfo.CheckinCode;//入住单号

            tradingInfo.ReturnCode = data;
            log("ReturnCode：" + tradingInfo.ReturnCode);
            tradingInfo.BankNumber = bankOut.ToString().Substring(2, 4);
            log("BankNumber：" + tradingInfo.BankNumber);
            tradingInfo.CardNumber = bankOut.ToString().Substring(6, 20);
            log("CardNumber：" + tradingInfo.CardNumber);
            tradingInfo.Valid = bankOut.ToString().Substring(26, 4);
            log("Valid：" + tradingInfo.Valid);
            tradingInfo.LotNo = bankOut.ToString().Substring(30, 6);
            log("LotNo：" + tradingInfo.LotNo);
            tradingInfo.CertificateNo = bankOut.ToString().Substring(36, 6);
            log("CertificateNo：" + tradingInfo.CertificateNo);
            tradingInfo.Money = bankOut.ToString().Substring(42, 12);
            log("Money：" + tradingInfo.Money);
            tradingInfo.Note = bankOut.ToString().Substring(54, 40);
            log("Note：" + tradingInfo.Note);
            tradingInfo.ContactNo = bankOut.ToString().Substring(94, 15);
            log("ContactNo：" + tradingInfo.ContactNo);
            tradingInfo.TerminalNo = bankOut.ToString().Substring(109, 8);
            log("TerminalNo：" + tradingInfo.TerminalNo);
            tradingInfo.TransactionReferenceNumber = bankOut.ToString().Substring(117, 12);
            log("TransactionReferenceNumber：" + tradingInfo.TransactionReferenceNumber);
            tradingInfo.TradingDate = bankOut.ToString().Substring(129, 4);
            log("TradingDate：" + tradingInfo.TradingDate);
            tradingInfo.TradingTime = bankOut.ToString().Substring(133, 6);
            log("TradingTime：" + tradingInfo.TradingTime);
            tradingInfo.AuthorizationNumber = bankOut.ToString().Substring(139, 6);
            log("AuthorizationNumber：" + tradingInfo.AuthorizationNumber);
            _interFace.AddTradingInfo(tradingInfo);//保存交易信息/-------------------------------------------
            if (result == 0)
            {
                if (data == "00")
                {
                    CheckInInfo.CardNum.Add(tradingInfo.CardNumber.Trim());//卡号
                    CheckInInfo.Validity.Add(tradingInfo.Valid);//有效期
                    var preAuthDate = tradingInfo.TradingTime;//交易日期
                    CheckInInfo.Dt = DateTime.Parse(DateTime.Now.Year + "-" + preAuthDate.Substring(0, 2) + "-" + preAuthDate.Substring(2, 2));
                    CheckInInfo.PreauthNumber = tradingInfo.AuthorizationNumber;//授权号
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// 预授权撤销
        /// </summary>
        /// <param name="money"></param>
        /// <param name="date"></param>
        /// <param name="authorizationNumber"></param>
        /// <param name="bug"></param>
        /// <returns></returns>
        public bool UmsPreAuthCancell(string money, string date, string authorizationNumber, ref string bug)
        {
            var tradingInfo = new TradingInfoModel();
            var bankOut = new StringBuilder();
            var str = UmsMoneyConvert(money);
            var bankin = new StringBuilder(str + date + authorizationNumber, 100);
            var result = BankCardPaymentService.UMS_PreAuthCancell_Dll(bankin, bankOut);
            var data = bankOut.ToString().Substring(0, 2);
            bug = _repInfo.RepMsg(data);
            log("预授权完成返回信息：" + bug);
            log("返回全部信息：" + bankOut);
            //-------------------------------------------
            tradingInfo.Id = Guid.NewGuid().ToString();
            tradingInfo.TradingType = "3";

            tradingInfo.ReturnCode = data;
            log("ReturnCode：" + tradingInfo.ReturnCode);
            tradingInfo.BankNumber = bankOut.ToString().Substring(2, 4);
            log("BankNumber：" + tradingInfo.BankNumber);
            tradingInfo.CardNumber = bankOut.ToString().Substring(6, 20);
            log("CardNumber：" + tradingInfo.CardNumber);
            tradingInfo.Valid = bankOut.ToString().Substring(26, 4);
            log("Valid：" + tradingInfo.Valid);
            tradingInfo.LotNo = bankOut.ToString().Substring(30, 6);
            log("LotNo：" + tradingInfo.LotNo);
            tradingInfo.CertificateNo = bankOut.ToString().Substring(36, 6);
            log("CertificateNo：" + tradingInfo.CertificateNo);
            tradingInfo.Money = bankOut.ToString().Substring(42, 12);
            log("Money：" + tradingInfo.Money);
            tradingInfo.Note = bankOut.ToString().Substring(54, 40);
            log("Note：" + tradingInfo.Note);
            tradingInfo.ContactNo = bankOut.ToString().Substring(94, 15);
            log("ContactNo：" + tradingInfo.ContactNo);
            tradingInfo.TerminalNo = bankOut.ToString().Substring(109, 8);
            log("TerminalNo：" + tradingInfo.TerminalNo);
            tradingInfo.TransactionReferenceNumber = bankOut.ToString().Substring(117, 12);
            log("TransactionReferenceNumber：" + tradingInfo.TransactionReferenceNumber);
            tradingInfo.TradingDate = bankOut.ToString().Substring(129, 4);
            log("TradingDate：" + tradingInfo.TradingDate);
            tradingInfo.TradingTime = bankOut.ToString().Substring(133, 6);
            log("TradingTime：" + tradingInfo.TradingTime);
            tradingInfo.AuthorizationNumber = bankOut.ToString().Substring(139, 6);
            log("AuthorizationNumber：" + tradingInfo.AuthorizationNumber);
            _interFace.AddTradingInfo(tradingInfo);//保存交易信息/-------------------------------------------
            if (result == 0)
            {
                return data == "00";
            }
            return false;
        }

        /// <summary>
        /// 预授权撤销
        /// </summary>
        /// <param name="money"></param>
        /// <param name="date"></param>
        /// <param name="authorizationNumber"></param>
        /// <returns></returns>
        public bool UmsPreAuthCancell(string money, string date, string authorizationNumber)
        {
            var bug = "";
            var tradingInfo = new TradingInfoModel();
            var bankOut = new StringBuilder();
            var str = UmsMoneyConvert(money);
            var bankin = new StringBuilder(str + date + authorizationNumber, 100);
            var result = BankCardPaymentService.UMS_PreAuthCancell_Dll(bankin, bankOut);
            var data = bankOut.ToString().Substring(0, 2);
            bug = _repInfo.RepMsg(data);
            log("预授权完成返回信息：" + bug);
            log("返回全部信息：" + bankOut);
            //-------------------------------------------
            tradingInfo.Id = Guid.NewGuid().ToString();
            tradingInfo.TradingType = "3";
            //tradingInfo.RoomNumber = "";//房号
            //tradingInfo.CheckOrderNumber = "";//入住单号

            tradingInfo.ReturnCode = data;
            log("ReturnCode：" + tradingInfo.ReturnCode);
            tradingInfo.BankNumber = bankOut.ToString().Substring(2, 4);
            log("BankNumber：" + tradingInfo.BankNumber);
            tradingInfo.CardNumber = bankOut.ToString().Substring(6, 20);
            log("CardNumber：" + tradingInfo.CardNumber);
            tradingInfo.Valid = bankOut.ToString().Substring(26, 4);
            log("Valid：" + tradingInfo.Valid);
            tradingInfo.LotNo = bankOut.ToString().Substring(30, 6);
            log("LotNo：" + tradingInfo.LotNo);
            tradingInfo.CertificateNo = bankOut.ToString().Substring(36, 6);
            log("CertificateNo：" + tradingInfo.CertificateNo);
            tradingInfo.Money = bankOut.ToString().Substring(42, 12);
            log("Money：" + tradingInfo.Money);
            tradingInfo.Note = bankOut.ToString().Substring(54, 40);
            log("Note：" + tradingInfo.Note);
            tradingInfo.ContactNo = bankOut.ToString().Substring(94, 15);
            log("ContactNo：" + tradingInfo.ContactNo);
            tradingInfo.TerminalNo = bankOut.ToString().Substring(109, 8);
            log("TerminalNo：" + tradingInfo.TerminalNo);
            tradingInfo.TransactionReferenceNumber = bankOut.ToString().Substring(117, 12);
            log("TransactionReferenceNumber：" + tradingInfo.TransactionReferenceNumber);
            tradingInfo.TradingDate = bankOut.ToString().Substring(129, 4);
            log("TradingDate：" + tradingInfo.TradingDate);
            tradingInfo.TradingTime = bankOut.ToString().Substring(133, 6);
            log("TradingTime：" + tradingInfo.TradingTime);
            tradingInfo.AuthorizationNumber = bankOut.ToString().Substring(139, 6);
            log("AuthorizationNumber：" + tradingInfo.AuthorizationNumber);
            _interFace.AddTradingInfo(tradingInfo);//保存交易信息/-------------------------------------------
            if (result == 0)
            {
                return data == "00";
            }
            return false;
        }

        /// <summary>
        /// char(12)，没有小数点"."，精确到分，最后两位为小数位，不足左补0。
        /// </summary>
        /// <param name="money">要转换的金额</param>
        /// <returns>转换后的金额，如：10000->000000010000</returns>
        public string UmsMoneyConvert(string money)
        {
            money += "00";
            var tmp = "";
            var len = money.Length;
            if (len < 12)
            {
                for (var i = 0; i < 12 - len; i++)
                {
                    tmp += "0";
                }
                tmp += money;
                return tmp;
            }
            return money;
        }

        /// <summary>
        /// 预授权,张威封装的过程方法，主要是简化前台的工作任务，使代码更易读和维护
        /// </summary>
        /// <param name="bug">返回的调试信息</param>
        /// <returns>true:成功,false:失败</returns>
        public bool PinGetPinValueToUmsPreAuth(ref string bug)
        {
            try
            {
                var result = PinGetPinValue();//密码安全函数
                if (result)
                {
                    result = PinDestroy();//关闭密码键盘安全函数
                    if (result)
                    {
                        result = UmsPreAuth(ref bug);
                        return result;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 预授权完成,张威封装的过程方法，主要是简化前台的工作任务，使代码更易读和维护
        /// </summary>
        /// <param name="bug">返回的调试信息</param>
        /// <returns>true:成功,false:失败</returns>
        public bool PinGetPinValueToUmsPreAuthDone(ref string bug)
        {
            try
            {
                var result = PinGetPinValue();//密码安全函数
                if (result)
                {
                    result = PinDestroy();//关闭密码键盘安全函数
                    if (result)
                    {
                        result = UmsPreAuthDone(ref bug);
                        return result;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
