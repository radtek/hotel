using System;
using System.Text;
using System.Threading;
using CheckIn.common;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_Interface_Hardware.AdelRead;
using HotelCheckIn_Interface_Hardware.BA_Bridge;
using HotelCheckIn_Interface_Hardware.BDBridge;
using HotelCheckIn_Interface_Hardware.CardManage;
using HotelCheckIn_Interface_Hardware.LedLight;
using HotelCheckIn_Interface_Hardware.ReadIdCard;
using HotelCheckIn_Interface_Hardware.Into_Notes;
using log4net;

namespace CheckIn.Bll
{
    public class CheckHard
    {
        public CheckHard()
        {

        }

        /// <summary>
        /// 检测所有硬件设备方法
        /// </summary>
        /// <param name="strMessage">故障信息</param>
        /// <returns>检测成功：true，检测故障：false</returns>
        public bool CheckHard_All(ref string strMessage)
        {
            if (!CheckHard_BABridge(ref strMessage)) //入钞
            {
                return false;
            }
            //if (!CheckHard_BACom(ref strMessage)) //入钞
            //{
            //    return false;
            //}
            else if (!CheckHard_BDBridge(ref strMessage)) //出钞
            {
                return false;
            }
            else if (!CheckHard_IOCard(ref strMessage)) //自动发卡器
            {
                return false;
            }
            else if (!CheckHard_IDCard(ref strMessage)) //身份证读卡器
            {
                return false;
            }
            else if (!CheckAdel(ref strMessage))
            {
                return false;
            }
            else
            {
                try
                {
                    LedLightApi ledLight = new LedLightApi();
                    ledLight.ComPort = new SettingHelper().LedCom;
                    ledLight.CloseAllLed();
                }
                catch (Exception)
                {
                }
                return true;
            }
        }

        /// <summary>
        /// 入钞硬件检测
        /// </summary>
        /// <param name="strMessage">故障信息</param>
        /// <returns>检测成功：true，检测故障：false</returns>
        public bool CheckHard_BABridge(ref string strMessage)
        {
            ILog _log = LogManager.GetLogger("入钞硬件检测");
            BABridgeClass _baBridge = new BABridgeClass();
            string _ip = new SettingHelper().Ip;
            int _rcPort = new SettingHelper().RcPort;
            try
            {
                _baBridge.SocketOpen(_ip, _rcPort);
            }
            catch (Exception ex)
            {
                _baBridge.SocketClose();
                strMessage = "入钞通讯错误,请保持本界面状态,联系前台相关人员。";
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                _log.Error(ErrorCode.RC_ERROR + "，入钞\n" + ex);
                return false;
            }

            _baBridge.SocketCommand(0, 0);
            _baBridge.RW_Data();

            if (_baBridge.IStatus == 9)
            {
                _baBridge.SocketClose();
                //判断状态为9的错误，显示错误！界面提示,联系前台
                strMessage = "入钞模块故障：请联系前台人员。" + _baBridge.ShowStatus(_baBridge.IErrorCode, _baBridge.IStatus);
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                _log.Error(ErrorCode.RC_ERROR + "，入钞\n" + _baBridge.ShowStatus(_baBridge.IErrorCode, _baBridge.IStatus));
                return false;
            }
            _baBridge.SocketClose();
            return true;
        }

        /// <summary>
        /// 入钞硬件检测(Com通信)
        /// </summary>
        /// <param name="strMessage">故障信息</param>
        /// <returns>检测成功：true，检测故障：false</returns>
        public bool CheckHard_BACom(ref string strMessage)
        {
            var log = LogManager.GetLogger("入钞硬件检测");
            var port = new SettingHelper().IntoNotesPort;
            var intoNotesService = new IntoNotesService();
            intoNotesService.ComPort = port;
            intoNotesService.log += log.Debug;
            try
            {
                intoNotesService.CloseComPort();
                intoNotesService.OpenComPort();//打开串口
            }
            catch (Exception e)
            {
                strMessage = "打开串口失败,请保持本界面状态,联系前台相关人员。";
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                log.Error(ErrorCode.RC_ERROR + "，入钞\n" + e);
                return false;
            }
            try
            {
                byte[] bt = { 0xfc, 0x05, 0x11, 0x27, 0x56 };
                var result = intoNotesService.QueryState(bt);
                if (result == null)
                {
                    strMessage = "状态查询失败,请保持本界面状态,联系前台相关人员。";
                    MachineError.ErrCode = ErrorCode.RC_ERROR;
                    log.Error(ErrorCode.RC_ERROR + "，入钞\n");
                    return false;
                }
                if (result == "ENABLE_IDLING")
                {
                    //将入钞机状态改成disable
                    bt = new byte[] { 0xfc, 0x07, 0xc0, 0xff, 0xff };//Enable:00,Disable:ff
                    var vCrc = CrcCcitt.Crc(bt);
                    intoNotesService.SettingCommand(vCrc);
                }
                bt = new byte[] { 0xfc, 0x05, 0x11, 0x27, 0x56 };
                result = intoNotesService.QueryState(bt);
                if (result != "DISABLE_INHIBIT")
                {
                    intoNotesService.AutoReset();//恢复出厂设置
                    return false;//葛经理要求如果执行了恢复出厂设置命令，那么就返回false
                }
                return true;
            }
            catch (Exception e)
            {
                strMessage = "状态查询失败,请保持本界面状态,联系前台相关人员。";
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                log.Error(ErrorCode.RC_ERROR + "，入钞\n" + e);
                return false;
            }
        }

        /// <summary>
        /// 出钞硬件检测
        /// </summary>
        /// <param name="strMessage">故障信息</param>
        /// <returns>检测成功：true，检测故障：false</returns>
        public bool CheckHard_BDBridge(ref string strMessage)
        {
            ILog _log = LogManager.GetLogger("出钞硬件检测");
            BDBridgeClass _bdBridge = new BDBridgeClass();
            string _ip = new SettingHelper().Ip;
            int _ccPort = new SettingHelper().CcPort;
            try
            {
                _bdBridge.SocketOpen(_ip, _ccPort);
            }
            catch (Exception ex)
            {
                _bdBridge.SocketClose();
                strMessage = "出钞通讯错误,请保持本界面状态,联系前台相关人员。";
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "，出钞\n" + ex);
                return false;
            }
            _bdBridge.SocketCommand(0);
            _bdBridge.RW_Data();
            if (_bdBridge.IStatus == 2 || _bdBridge.IStatus == 9)
            {
                _bdBridge.SocketClose();
                strMessage = "余额不足或故障，请保持本界面状态,联系前台相关人员。";
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "出钞\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));

                //var ofn = new SettingHelper().OutmoneyFilefolderName;
                //var of = new SettingHelper().OutmoneyFilefolder;
                //foreach (var p in Process.GetProcesses())
                //{
                //    if (p.ProcessName == ofn)
                //    {
                //        p.Kill();
                //    }
                //}
                //Process.Start(of + ofn + ".exe");
                return false;
            }
            if (_bdBridge.IStatus == 8)
            {
                _bdBridge.SocketClose();
                strMessage = "余额不足，请保持本界面状态,联系前台相关人员。";
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "出钞\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));
                return false;
            }

            _bdBridge.SocketClose();
            return true;
        }

        /// <summary>
        /// 自动发卡器硬件检测
        /// </summary>
        /// <param name="strMessage">故障信息</param>
        /// <returns>检测成功：true，检测故障：false</returns>
        public bool CheckHard_IOCard(ref string strMessage)
        {
            ILog _log = LogManager.GetLogger("自动发卡器硬件检测");
            CardManage iocard = new CardManage();
            if (!iocard.InitMachine(ref strMessage))
            {
                MachineError.ErrCode = ErrorCode.SENDCARD_ERROR;
                _log.Error(ErrorCode.SENDCARD_ERROR + "，自动发卡器\n");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 身份证硬件检测
        /// </summary>
        /// <param name="strMessage">故障信息</param>
        /// <returns>检测成功：true，检测故障：false</returns>
        public bool CheckHard_IDCard(ref string strMessage)
        {
            ILog _log = LogManager.GetLogger("身份证读卡器硬件检测");
            IdCard idcard = new IdCard(new SettingHelper().ReadCardType);
            try
            {
                if (!idcard.OpenPort())
                {
                    idcard.ClosePort();
                    strMessage = "身份证读卡器故障：请联系前台人员。";
                    MachineError.ErrCode = ErrorCode.IDCARD_ERROR;
                    _log.Error(ErrorCode.IDCARD_ERROR + "，身份证读卡器\n");
                    return false;
                }
                else
                {
                    idcard.ClosePort();
                    return true;
                }
            }
            catch (BusinessException ex)
            {
                idcard.ClosePort();
                strMessage = "身份证读卡器故障：请联系前台人员。" + ex.Message;
                MachineError.ErrCode = ErrorCode.IDCARD_ERROR;
                _log.Error(ErrorCode.IDCARD_ERROR + "，身份证读卡器\n");
                return false;
            }
        }

        /// <summary>
        /// 初始化制卡机
        /// </summary>
        /// <param name="StrMessage"></param>
        /// <returns></returns>
        public bool CheckAdel(ref string StrMessage)
        {
            int i = AdelReadClass.EndSession();
            if (i == 0)
            {
                i = AdelReadClass.Init(18, new StringBuilder(new SettingHelper().LockServer), new StringBuilder("14"),
                               int.Parse(new SettingHelper().LockPort), 0, 1);
                if (i == 0)
                {
                    return true;
                }
                else
                {
                    StrMessage = "门锁硬件初始化出错。";
                    return false;
                }
            }
            else
            {
                StrMessage = "门锁硬件初始化出错。";
                return false;
            }
        }
    }
}
