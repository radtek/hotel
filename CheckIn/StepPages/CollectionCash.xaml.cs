using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_Interface_Hardware.BA_Bridge;
using HotelCheckIn_Interface_Hardware.BDBridge;
using HotelCheckIn_Interface_Hardware.LedLight;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// CollectionCash.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionCash : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private readonly DispatcherTimer _rcTimer = new DispatcherTimer();
        private readonly BABridgeClass _baBridge = new BABridgeClass();
        private readonly BDBridgeClass _bdBridge = new BDBridgeClass();
        private readonly LedLightApi _ledLightApi = new LedLightApi();

        private static int _timeout;
        private static string _ip;
        private static int _rcPort;
        private static string _macId;
        private static int _ccPort;
        private readonly int _yajin;

        private int _num;
        private bool _retry = false;
        private NavigationService _navService;
        private string _rate;
        private string _insertValue = "0";
        /// <summary>
        /// 标记是否正在出钞
        /// </summary>
        private bool _ccFlag = false;
        /// <summary>
        /// 标记是否需要退钞
        /// </summary>
        private bool _tcFlag = false;

        public CollectionCash()
        {
            _log.Info("打开界面");
            InitializeComponent();

            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _ip = setting.Ip;
            _rcPort = setting.RcPort;
            _macId = setting.MacId;
            _ccPort = setting.CcPort;
            _yajin = int.Parse(setting.Rate);
            _ledLightApi.ComPort = setting.LedCom;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();

            _rcTimer.Tick += RcTimerTick;
            _rcTimer.Interval = new TimeSpan(0, 0, 1);

            _num = 0;
            Step.BtInit();
            Step.SetStep(3);

            _navService = NavigationService.GetNavigationService(this);
            //if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode)) //如果验证码不为空，则价格、价格码取自配置文件或者PMS
            //{
            //    _rate = _yajin.ToString();//是团购，只需要支付押金
            //}
            //else
            //{
            _rate = ((int)(CheckInInfo.RoomRate + _yajin)).ToString();//不是团购需要支付：房费+押金
            //}


            Common.Speak(string.Format(Properties.Resources.PUTINMONEY, _rate));
            try
            {
                RcInit();
            }
            catch (Exception ex)
            {
                MachineError.ErrMsg = "入钞出错:" + ex.Message;
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
            #region 出钞
            //if (_ccFlag && _tcFlag)
            //{
            //    _bdBridge.RW_Data();
            //    if (_bdBridge.IStatus == 2)
            //    {
            //        _log.Debug("出钞状态为：" + _bdBridge.IStatus);
            //        var ofn = new SettingHelper().OutmoneyFilefolderName;
            //        var of = new SettingHelper().OutmoneyFilefolder;
            //        foreach (var p in Process.GetProcesses())
            //        {
            //            if (p.ProcessName == ofn)
            //            {
            //                p.Kill();
            //            }
            //        }

            //        Process.Start(of + ofn + ".exe");
            //        _num = 0;
            //        labelNum.Content = _timeout - _num;
            //        Thread thread = new Thread(RetryCc);
            //        thread.Start();
            //        _dTimer.Stop();
            //        labelMsg.Content = "应收" + _rate + "元，实收" + _insertValue + "元,正在退钞，请稍候";
            //        return;
            //    }

            //    _log.Debug("出钞计时器记录设备状态：" + _bdBridge.IStatus);
            //    if (_bdBridge.IStatus == 4 || _bdBridge.IStatus == 8)
            //    {
            //        _bdBridge.SocketCommand(2);
            //        _bdBridge.RW_Data();
            //        _log.Debug("退钞完成，状态：" + _bdBridge.IStatus);
            //        _bdBridge.SocketClose();
            //        Common.Speak(Properties.Resources.GETMONEY);
            //        //要在计时器中进行。
            //        InterFace interFace = new InterFace();
            //        interFace.AddIoJournal(new IoJournal()
            //        {
            //            IoId = _macId,
            //            IoMoney = int.Parse(_insertValue),
            //            IoTag = 2,
            //            IoFrom = 1,
            //            IsUse = 2,
            //            OrderId = CheckInInfo.ValidateCode,//订单号保存验证码
            //            RoomNo = CheckInInfo.RoomNum,
            //        });
            //        //todo:解锁房间锁定。
            //        labelMsg.Content = "退钞完成，将在10秒后自动返回主界面";
            //        _timeout = _num + 10;
            //        _ccFlag = false;
            //    }
            //}
            #endregion
            if (_num > _timeout - 6)
            {
                _baBridge.SocketCommand(0, 0);
                _baBridge.RW_Data();
                if (_baBridge.IStatus == 2 || _baBridge.IStatus == 1 || _baBridge.IStatus == 4)
                {
                    _baBridge.SocketCommand(2, 0);
                    _baBridge.RW_Data();
                    _baBridge.SocketCommand(3, 0);
                    _baBridge.RW_Data();
                }
            }

            if (_num > _timeout - 1)
            {
                try
                {
                    Back_Click(null, null);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        private void RetryCc()
        {
            Thread.Sleep(12000);
            _tcFlag = true;
            CcInit();
        }
        int insertValue = 0;
        string insertValueOld = "0";
        private void RcTimerTick(object sender, EventArgs e)
        {
            _baBridge.SocketCommand(0, 0);
            _baBridge.RW_Data();
            _insertValue = _baBridge.ShowValue();
            if (insertValue == 0)
            {
                insertValueOld = _insertValue;
                insertValue = 1;
            }
            if (insertValueOld != _insertValue)
            {
                insertValueOld = "-1";
            }
            _log.Debug("入钞，状态：" + _baBridge.IStatus + ",金额：" + _insertValue);
            //if (_baBridge.IStatus != 1)
            //{
            //    AdminCheck.Visibility = Visibility.Hidden;
            //}
            labelMsg.Content = "应收" + _rate + "元，实收" + (insertValueOld == "-1" ? _insertValue : "0") + "元";
            if (int.Parse(_insertValue) >= int.Parse(_rate) && insertValueOld == "-1")//_baBridge.IStatus == 4 //入钞大于需要交的Shiite房费+押金就不要判断状态4了
            {
                labelMsg.Content = Properties.Resources.INMONEYOK;
                _rcTimer.IsEnabled = false;
                _baBridge.SocketCommand(2, 0);
                _baBridge.RW_Data();
                _baBridge.SocketCommand(3, 0);
                _baBridge.RW_Data();
                _baBridge.SocketClose();

                //收取押金业务逻辑：押金记录--跳到出卡界面
                InterFace interFace = new InterFace();
                CheckInInfo.Dt = interFace.AddIoJournal(new IoJournal()
                {
                    IoId = _macId,
                    IoMoney = int.Parse(_insertValue),
                    IoTag = 1,
                    IoFrom = 1,
                    OrderId = CheckInInfo.ValidateCode,//订单号保存验证码
                    RoomNo = CheckInInfo.RoomNum,
                });
                //添加支付方式
                CheckInInfo.PayType.Add("2");
                CheckInInfo.PayWay.Add("1");
                CheckInInfo.PaymentAmount.Add(float.Parse(_insertValue));//_insertValue在散客入住时为实收费用。
                CheckInInfo.HepPayGuid.Add(DateTime.Now.Ticks.ToString());
                CheckInInfo.Batch.Add("");
                CheckInInfo.JopbNumberPmsCode.Add("1");
                CheckInInfo.OperTime.Add(DateTime.Now);
                CheckInInfo.TransactionType.Add("0");
                //2#14#188#1234567890987654##1#2014-07-22 14:44:44#0##--------------团购支付方式
                //if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode))
                //{
                //    CheckInInfo.PayType.Add("2");
                //    CheckInInfo.PayWay.Add("14");//14是团购的支付方式
                //    CheckInInfo.PaymentAmount.Add(CheckInInfo.RoomRate);
                //    CheckInInfo.HepPayGuid.Add((DateTime.Now.Ticks + 1).ToString());
                //    CheckInInfo.Batch.Add("");
                //    CheckInInfo.JopbNumberPmsCode.Add("1");
                //    CheckInInfo.OperTime.Add(DateTime.Now);
                //    CheckInInfo.TransactionType.Add("0");
                //}
                _log.Debug("收钞完成：" + _baBridge.IStatus + "---" + _insertValue);
                Next_Click(null, null);
            }
            if (_baBridge.IStatus == 1 && int.Parse(_insertValue) != 0)
            {
                _dTimer.Stop();

                _log.Debug("入钞成功：状态：" + _baBridge.IStatus + ",金额：" + _insertValue);
                //if (_rate != _insertValue && !_tcFlag)
                //{
                //    AdminCheck.Visibility = Visibility.Visible;
                //}

                #region 初始化退钞

                //if (_tcFlag)
                //{
                //    if (int.Parse(_insertValue) == 0)
                //    {
                //        Close();
                //        _num = _timeout - 10;
                //        _dTimer.Start();
                //        labelMsg.Content = "将在10秒后自动返回主界面";
                //        return;
                //    }

                //    _baBridge.SocketCommand(2, 0);
                //    _baBridge.RW_Data();
                //    _baBridge.SocketCommand(3, 0);
                //    _baBridge.RW_Data();

                //    _dTimer.Stop();
                //    _rcTimer.Stop();

                //    while (true)
                //    {
                //        Thread.Sleep(500);
                //        _baBridge.SocketCommand(0, 0);
                //        _baBridge.RW_Data();
                //        _insertValue = _baBridge.ShowValue();
                //        _log.Debug("入钞=======：状态：" + _baBridge.IStatus + ",金额：" + _insertValue);
                //        if (_baBridge.IStatus == 4)
                //        {
                //            break;
                //        }
                //    }

                //    //向收入明细中添加收款纪录
                //    InterFace interFace = new InterFace();
                //    CheckInInfo.Dt = interFace.AddIoJournal(new IoJournal()
                //    {
                //        IoId = _macId,
                //        IoMoney = int.Parse(_insertValue),
                //        IoTag = 1,
                //        IoFrom = 1,
                //        OrderId = CheckInInfo.ValidateCode,//订单号保存验证码
                //    });

                //    try
                //    {
                //        labelMsg.Content = "应收" + _rate + "元，实收" + _insertValue + "元";
                //        CcInit();
                //    }
                //    catch (Exception exception)
                //    {
                //        _log.Error(exception);
                //    }
                //}
                #endregion
                return;
            }
            //todo:入钞完成

            if (_retry)
            {
                labelMsg.Content = Properties.Resources.RETRYING;
                Retry();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (_navService != null)
            {
                var next = new Fabrication();
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
            _rcTimer.Stop();
            _baBridge.SocketClose();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            if (_navService != null)
            {
                var next = new IndexPage();
                _navService.Navigate(next);
            }
        }

        private void Retry()
        {
            _num = 0;//每次重试都重置倒计时
            _retry = false;
            _rcTimer.IsEnabled = false;
            _baBridge.SocketClose();
            RcInit();
        }

        /// <summary>
        /// 入钞初始化
        /// </summary>
        private void RcInit()
        {
            labelMsg.Content = Properties.Resources.DEVICEERROR;
            try
            {
                _baBridge.SocketOpen(_ip, _rcPort);
            }
            catch (Exception ex)
            {
                _rcTimer.IsEnabled = false;
                MessageBox.Show(Properties.Resources.NETCOMMUNICATION, Properties.Resources.ERROR);
                labelMsg.Content = Properties.Resources.DEVICEINITERROR;
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                MachineError.ErrMsg = "入钞初始化出错:" + ex.Message;
                MachineError.AllLock = true;
                Close();
                if (_navService != null)
                {
                    var next = new IndexPage();
                    _navService.Navigate(next);
                }
                _log.Error(ex);
                return;
            }
            _baBridge.SocketCommand(0, 0);
            _baBridge.RW_Data();
            if (_baBridge.IStatus == 9)
            {
                _rcTimer.IsEnabled = false;
                _baBridge.SocketClose();
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                _log.Error(ErrorCode.RC_ERROR + "\n" + _baBridge.ShowStatus(_baBridge.IErrorCode, _baBridge.IStatus));
                //判断状态为9的错误，显示错误！界面提示,联系前台
                var ret = MessageBox.Show(Properties.Resources.INMONEYERROR + _baBridge.ShowStatus(_baBridge.IErrorCode, _baBridge.IStatus),
                                          Properties.Resources.ERROR, MessageBoxButton.OKCancel);
                labelMsg.Content = Properties.Resources.DEVICEERROR;
                if (ret == MessageBoxResult.OK)
                {
                    _dTimer.IsEnabled = true;
                    _rcTimer.IsEnabled = true;
                    _retry = true;
                }
                else
                {
                    Close();
                    if (_navService != null)
                    {
                        var next = new IndexPage();
                        _navService.Navigate(next);
                    }
                }
                return;
            }

            if (_baBridge.IStatus == 2 || _baBridge.IStatus == 1 || _baBridge.IStatus == 4)
            {
                _baBridge.SocketCommand(2, 0);
                _baBridge.RW_Data();
                _baBridge.SocketCommand(3, 0);
                _baBridge.RW_Data();
            }
            _baBridge.SocketCommand(1, 0);//第二个1改为0，1表示限额收取，0表示不限额
            _baBridge.Lval = int.Parse(_rate);
            _baBridge.RW_Data();
            if (_baBridge.IStatus != 2 && _baBridge.IStatus != 1)
            {
                _dTimer.IsEnabled = false;
                _rcTimer.IsEnabled = false;
                //此处再判断状态是否为1或者2，不是提示重试！
                MessageBox.Show("状态出错：" + _baBridge.IStatus + "请重试。", Properties.Resources.INFO);
                _dTimer.IsEnabled = true;
                _rcTimer.IsEnabled = true;
                _retry = true;
                return;
            }
            labelMsg.Content = string.Format(Properties.Resources.WAITINMONEY, _rate);
            _rcTimer.Start();
            _ledLightApi.OpenLedByNum(1);
        }

        private void AdminCheck_Click(object sender, RoutedEventArgs e)
        {
            _tcFlag = true;
            AdminCheck.Visibility = Visibility.Hidden;
        }

        private void CcInit()
        {
            try
            {
                _bdBridge.SocketOpen(_ip, _ccPort);
            }
            catch (Exception ex)
            {
                var ret = MessageBox.Show("网络通讯错误,请保持本界面状态,联系前台相关人员。", "错误");
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                //退卡，返回主界面
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + ex);
                throw new BusinessException(Properties.Resources.NETCOMMUNICATIONERROR);
            }
            _bdBridge.SocketCommand(0);
            _bdBridge.RW_Data();
            if (_bdBridge.IStatus == 2)
            {
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));
                //此处需要注意出钞失败，需要退卡
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;

                MessageBox.Show(Properties.Resources.MONEYNOTENOUGH, Properties.Resources.INFO);
                throw new BusinessException(Properties.Resources.MONEYNOTENOUGH);
            }
            if (_bdBridge.IStatus == 8)
            {
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;

                _log.Error(Properties.Resources.MONEYNOTENOUGH);
                _log.Error(ErrorCode.CC_ERROR + "\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));
                MessageBox.Show(Properties.Resources.MONEYNOTENOUGH, Properties.Resources.INFO);
                throw new BusinessException(Properties.Resources.MONEYNOTENOUGH);
            }
            if (_bdBridge.IStatus == 9)
            {
                _bdBridge.SocketClose();
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));
                //判断状态为9的错误，显示错误！界面提示,联系前台
                MessageBox.Show("出钞模块故障：请联系前台人员。\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus),
                                         "错误");
                //此处需要注意，需要退卡
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                throw new BusinessException("出钞模块故障");
            }
            _bdBridge.SocketCommand(1);
            _bdBridge.Lval = int.Parse(_insertValue) / 100;//设置出钞数量
            _bdBridge.RW_Data();
            if (_bdBridge.IStatus == 8)
            {
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                _log.Error(ErrorCode.CC_ERROR + "\n" + _bdBridge.ShowStatus(_bdBridge.IErrorCode, _bdBridge.IStatus));
                labelMsg.Content = "余额不足，如未出钞，请联系前台,将在5秒后自动返回主界面";
                //labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                return;
            }
            //此处再判断状态是否为1或者2，如果不是，则提示重试！
            if (_bdBridge.IStatus != 2 && _bdBridge.IStatus != 1)
            {
                MessageBox.Show("状态故障：" + _bdBridge.IStatus + "请重试。", "信息");
                _log.Error("状态故障：" + _bdBridge.IStatus);
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                labelMsg.Content = "退房失败，将在5秒后自动返回主界面";
                _timeout = _num + 5;
                return;
            }
            _ccFlag = _tcFlag;
            _dTimer.Start();
            _ledLightApi.OpenLedByNum(4);
        }


        public string ChangeInt2Character(int num)
        {
            string c = "NULL", ret = "", a = num.ToString();
            string[] dw = "万,仟,佰,拾,圆".Split(',');
            for (int i = 0; i < a.Length; i++)
            {
                if (a.Length == 5) c = dw[i];
                if (a.Length == 4) c = dw[i + 1];
                if (a.Length == 3) c = dw[i + 2];
                if (a.Length == 2) c = dw[i + 3];
                if (a.Length == 1) c = dw[i + 4];
                switch (a[i])
                {
                    case '0': if (a[1] == '0' || a[2] == '0') Console.Write("零"); break;
                    case '1': ret += "壹" + c; break;
                    case '2': ret += "贰" + c; break;
                    case '3': ret += "叁" + c; break;
                    case '4': ret += "肆" + c; break;
                    case '5': ret += "伍" + c; break;
                    case '6': ret += "陆" + c; break;
                    case '7': ret += "柒" + c; break;
                    case '8': ret += "捌" + c; break;
                    case '9': ret += "玖" + c; break;
                }
            }
            return ret;
        }
    }
}
