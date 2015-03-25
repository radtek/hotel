using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.common;
using ChuchaoDll;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_Interface_Hardware.BDBridge;
using HotelCheckIn_Interface_Hardware.CardManage;
using HotelCheckIn_Interface_Hardware.LedLight;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// ReturnCash_Com.xaml 的交互逻辑
    /// </summary>
    public partial class ReturnCash_Com : Page
    {
        private Cc cc = null;
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private readonly LedLightApi _ledLightApi = new LedLightApi();

        private int _num;
        private static int _timeout;
        private readonly BDBridgeClass _bdBridge = new BDBridgeClass();
        private readonly DispatcherTimer _ccTimer = new DispatcherTimer();
        private static string _ip;
        private static int _ccPort;
        private static string _macId;
        private readonly CardManage _cardManage = new CardManage();
        private NavigationService _navService;

        public ReturnCash_Com()
        {
            _log.Info("打开界面");
            InitializeComponent();
            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _ip = setting.Ip;
            _ccPort = setting.CcPort;
            _macId = setting.MacId;
            _ledLightApi.ComPort = setting.LedCom;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();

            _ccTimer.Tick += CcTimerTick;
            _ccTimer.Interval = new TimeSpan(0, 0, 2);

            _num = 0;
            Step.BtInit();
            Step.SetStep(1);

            _navService = NavigationService.GetNavigationService(this);
            //先退房，退房成功，再退款。

            //退房PMS操作统一放到后台
            //PMSClass  pms =new PMSClass();

            ////todo:退房参数需要修改
            //RegistBack ret = pms.check_out_register("1234567890", new AuthenToken(), "123", "0", "0", "111", "00000");

            //if (ret.return_Info.return_Code.Length > 0)//返回信息为空，则认为正确执行。
            //{
            //    ReciveCard();
            //}
            //else
            //{
            //    ReturnCard();
            //}
            try
            {
                CcInit();
            }
            catch (BusinessException ex)
            {
                ReturnCard();
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                MachineError.ErrMsg = "出钞加载出错:" + ex.Message;
                MachineError.AllLock = true;
                labelMsg.Content = ex.Message;
                _log.Error(ex);
                Next_Click(null, null);
            }
            catch (Exception ex)
            {
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                MachineError.ErrMsg = "未知错误:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex);
            }
        }

        /// <summary>
        /// 操作倒计时 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DTimerTick(object sender, EventArgs e)
        {
            _num++;
            labelNum.Content = _timeout - _num;
            if (_num > _timeout - 1)
            {
                Close();
                try
                {
                    if (_navService != null)
                        _navService.Navigate(new IndexPage());
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        int ccNum = 0;
        private void CcTimerTick(object sender, EventArgs e)
        {
            if (ccNum == 0 || ccNum == 1)
            {
                ccNum++;
                return;
            }
            ccNum++;
            //_bdBridge.SocketCommand(0);
            _bdBridge.RW_Data();
            if (ccNum >= 4 && _bdBridge.IStatus == 2)
            {
                ReturnCard();
                _ccTimer.Stop();

                var ofn = new SettingHelper().OutmoneyFilefolderName;
                var of = new SettingHelper().OutmoneyFilefolder;
                foreach (var p in Process.GetProcesses())
                {
                    if (p.ProcessName == ofn)
                    {
                        p.Kill();
                    }
                }

                Process.Start(of + ofn + ".exe");
                _timeout = _num + 5;
                labelMsg.Content = "5秒后返回主界面，请重试。";
            }
            _log.Debug("出钞计时器记录设备状态：" + _bdBridge.IStatus);
            if (_bdBridge.IStatus == 4 || _bdBridge.IStatus == 8)
            {
                _bdBridge.SocketCommand(2);
                _bdBridge.RW_Data();
                _log.Debug("出钞完成，状态：" + _bdBridge.IStatus);
                _ccTimer.Stop();
                _bdBridge.SocketClose();
                Common.Speak(Properties.Resources.GETMONEY);
                //出钞成功，添加出钞记录，吞卡。
                InterFace interFace = new InterFace();
                interFace.AddIoJournal(new IoJournal()
                {
                    IoId = _macId,
                    IoMoney = 100,
                    IoTag = 2,
                    IoFrom = 1,
                    RoomNo = "000",//房间号需要查询
                    OrderId = "001",//订单号需要查询。
                });

                ReciveCard();
                labelMsg.Content = "退房完成，将在10秒后自动返回主界面";
                _timeout = _num + 10;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (_navService != null)
            {
                var next = new IndexPage();
                _navService.Navigate(next);
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            Close();
            _ledLightApi.CloseAllLed();
            _log.Info("退出界面");
        }

        private void Close()
        {
            _dTimer.Stop();
            _ccTimer.Stop();
            _bdBridge.SocketClose();
        }

        private void CcInit()
        {
            Status status = new Status();
            Error error = new Error();
            try
            {
                cc = new Cc(_ccPort.ToString());
                cc.Reset();//重置
                cc.QueryStatus(ref status, ref error); //查询状态
            }
            catch (Exception ex)
            {
                _ccTimer.IsEnabled = false;
                var ret = MessageBox.Show("网络通讯错误,请保持本界面状态,联系前台相关人员。\n点击“确认”后将返回主界面。", "错误");
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                //退卡，返回主界面
                ReturnCard();
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + ex);
                throw new BusinessException(Properties.Resources.NETCOMMUNICATIONERROR);
            }
            if (status.ChkSensor1 != 0 || status.ChkSensor2 != 0 || status.DivSensor1 != 0 ||
                status.DivSensor2 != 0 || status.EjtSensor != 0 || status.ExitSensor != 0 ||
                status.NearendSensor != 0 || status.RejectTraysw != 0 || status.CassetteSensor != 0
                || status.SolSensor != 0)//判断有没有卡钱
            {
                _ccTimer.IsEnabled = false;
                var ret = MessageBox.Show("出钞机状态出错。\n点击“确认”后将返回主界面。", "错误");
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                //退卡，返回主界面
                ReturnCard();
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + "出钞机状态出错。");
                throw new BusinessException("出钞机状态出错");
            }
            cc.Chuchao();//出钞数量需要添加参数
            _bdBridge.SocketCommand(1);
            _bdBridge.Lval = 1;//设置出钞数量
            _bdBridge.RW_Data();
            if (_bdBridge.IStatus == 8)
            {
                _ccTimer.IsEnabled = false;
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));
                labelMsg.Content = "余额不足，如未出钞，请联系前台,将在5秒后自动返回主界面";
                //labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                ReturnCard();
                return;
            }
            //此处再判断状态是否为1或者2，如果不是，则提示重试！
            if (_bdBridge.IStatus != 2 && _bdBridge.IStatus != 1)
            {
                _ccTimer.IsEnabled = false;
                MessageBox.Show("状态故障：" + _bdBridge.IStatus + "请重试。", "信息");
                _log.Error("状态故障：" + _bdBridge.IStatus);
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                ReturnCard();
                return;
            }

            _ccTimer.Start();
            _ledLightApi.OpenLedByNum(4);
        }

        /// <summary>
        /// 退卡方法
        /// </summary>
        private void ReturnCard()
        {
            try
            {
                _log.Debug("退卡");
                _ledLightApi.OpenLedByNum(2);
                _cardManage.ReadPositionToEntrance();
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message);
                _log.Error(ex);
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
        }

        /// <summary>
        /// 收卡
        /// </summary>
        private void ReciveCard()
        {
            try
            {
                _log.Debug("收卡");
                _cardManage.ReadPositionToRecoverBox();
            }
            catch (BusinessException ex)
            {
                MessageBox.Show(ex.Message);
                _log.Error(ex);
            }
            catch (Exception exception)
            {
                MachineError.ErrMsg = "收卡出错:" + exception;
                _log.Error(exception);
            }
        }
    }
}
