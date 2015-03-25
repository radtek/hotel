using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CheckIn.common;
using log4net;
using System.Windows.Threading;
using HotelCheckIn_Interface_Hardware.LedLight;
using CheckIn.BackService;
using CheckIn.Model;
using CommonLibrary;
using CheckIn.AddDll;
using CheckIn.Bll;
using System.Threading;
using CheckIn.StepPages;
using CheckIn.Bll;
namespace CheckIn.StepPages
{
    /// <summary>
    /// Interaction logic for BankCardPaymentOftwo.xaml
    /// </summary>
    public partial class BankCardPaymentOftwo : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _mainTimer = new DispatcherTimer();//主定时器，记录主页面的操作倒计时
        private readonly DispatcherTimer _subTimer = new DispatcherTimer();//输入银行卡密码的定时器

        private int _num;
        private static int _timeout;//倒计时秒数,默认为100秒
        private int _count = 0;//输入密码计数器

        private readonly BankCardPaymentCall _bankCardPaymentCall;

        private NavigationService _navService;

        /// <summary>
        /// 预授权金额
        /// </summary>
        public string Money;

        /// <summary>
        /// 预授权类型,1:预授权,2:预授权完成 
        /// </summary>
        public string PreAuthType;

        /// <summary>
        /// 预授权号
        /// </summary>
        public string PreAuthNumber;

        /// <summary>
        /// 返回的调试信息
        /// </summary>
        public string Bug;

        /// <summary>
        /// 预授权日期
        /// </summary>
        public string PreAuthDate;

        /// <summary>
        /// 房号
        /// </summary>
        public string RoomNumber;

        /// <summary>
        /// 入住单号
        /// </summary>
        public string CheckOrderNumber;

        public BankCardPaymentOftwo()
        {
            _log.Info("打开界面");
            InitializeComponent();
            var setting = new SettingHelper();
            _timeout = setting.TimeOut;

            _bankCardPaymentCall = new BankCardPaymentCall();
            _bankCardPaymentCall.log += _log.Debug;
        }

        /// <summary>
        /// 多参数构造函数
        /// </summary>
        /// <param name="money">预授权和完成的金额</param>
        /// <param name="preAuthType">预授权类型,1:预授权,2:预授权完成 </param>
        /// <param name="preAuthNumber">预授权号</param>
        /// <param name="preAuthDate">预授权日期</param>
        /// <param name="roomNumber">房号</param>
        /// <param name="checkOrderNumber">入住单号</param>
        public BankCardPaymentOftwo(string money, string preAuthType, string preAuthNumber, string preAuthDate, string roomNumber, string checkOrderNumber)
        {
            _log.Info("打开界面");
            InitializeComponent();
            var setting = new SettingHelper();
            _timeout = setting.TimeOut;

            Money = money;
            PreAuthType = preAuthType;
            PreAuthNumber = preAuthNumber;
            PreAuthDate = preAuthDate;
            RoomNumber = roomNumber;
            CheckOrderNumber = checkOrderNumber;

            _bankCardPaymentCall = new BankCardPaymentCall();
            _bankCardPaymentCall.log += _log.Debug;
        }

        /// <summary>
        /// 启动wpf页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _mainTimer.Tick += MainTimerTick;//主倒计时
            _mainTimer.Interval = new TimeSpan(0, 0, 1);
            _mainTimer.Start();

            _subTimer.Tick += SubTimerTick;//主倒计时
            _subTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            _num = 0;
            Step.BtInit();
            Step.SetStep(3);

            _navService = NavigationService.GetNavigationService(this);

            try
            {
                var result = _bankCardPaymentCall.UmsReadCard();//读卡
                if (result)
                {
                    result = _bankCardPaymentCall.UmsClose();//关闭读卡器，弹卡
                    if (result)
                    {
                        result = _bankCardPaymentCall.PinOpen();//打开密码键盘
                        if (result)
                        {
                            _subTimer.Start();
                            switch (PreAuthType)
                            {
                                case "1":
                                    labelMsg.Content = "本次[预授权]:" + Money + "元，请输入银行卡密码";
                                    break;
                                case "2":
                                    labelMsg.Content = "本次[预授权完成]:" + Money + "元，请输入银行卡密码";
                                    break;
                                default:
                                    _log.Debug("预授权类型不能为其它值");
                                    break;
                            }
                        }
                        else
                        {
                            labelMsg.Content = "打开密码键盘失败";
                            _log.Error("打开密码键盘失败");
                            _log.Info("打开密码键盘失败");
                        }
                    }
                    else
                    {
                        labelMsg.Content = "关闭读卡器失败";
                        _log.Error("关闭读卡器失败");
                        _log.Info("关闭读卡器失败");
                    }
                }
                else
                {
                    labelMsg.Content = "读卡失败";
                    _log.Error("读卡失败");
                    _log.Info("读卡失败");
                }
            }
            catch (Exception ex)
            {
                _subTimer.Stop();
                MachineError.ErrCode = ErrorCode.BANKCARD_ERROR;
                MachineError.ErrMsg = "检测卡位置出错：" + ex.Message;
                MachineError.AllLock = true;
                labelMsg.Content = "检测卡位置出错";
                _log.Error(ex.Message);
                _log.Info("检测卡位置出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 读插入银行卡状态定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubTimerTick(object sender, EventArgs e)
        {
            //解释：_count>=6说明已经输入了6个字符，这样就要进入[预授权]和[预授权完成]阶段了
            if (_count >= 6)
            {
                _subTimer.Stop();
                if (tbPassword.Text == "******")
                {
                    switch (PreAuthType)
                    {
                        case "1":
                            var result1 = _bankCardPaymentCall.PinGetPinValueToUmsPreAuth(ref Bug);
                            _log.Debug("返回的数据信息是：" + Bug);
                            switch (Bug)
                            {
                                case "密码错误":
                                    labelMsg.Content = "输入密码有误，请点击重试";
                                    Retry.Visibility = Visibility.Visible;
                                    break;
                                case "交易成功":
                                    labelMsg.Content = "本次[预授权]:" + Money + "元,成功";
                                    var nextSuccess = new Fabrication();
                                    _navService.Navigate(nextSuccess);
                                    return;
                                default:
                                    labelMsg.Content = "本次[预授权]" + Money + "元,失败";
                                    OtherPayment.Visibility = Visibility.Visible;
                                    Retry.Visibility = Visibility.Visible;
                                    return;
                            }
                            break;
                        case "2":
                            var result2 = _bankCardPaymentCall.PinGetPinValueToUmsPreAuthDone(ref Bug);
                            switch (Bug)
                            {
                                case "密码错误":
                                    labelMsg.Content = "输入密码有误，请点击重试";
                                    Retry.Visibility = Visibility.Visible;
                                    break;
                                case "交易成功":
                                    labelMsg.Content = "本次[预授权完成]:" + Money + "元,成功";
                                    var nextSuccess = new Fabrication();
                                    _navService.Navigate(nextSuccess);
                                    break;
                                default:
                                    labelMsg.Content = "本次[预授权完成]" + Money + "元,失败";
                                    OtherPayment.Visibility = Visibility.Visible;
                                    Retry.Visibility = Visibility.Visible;
                                    break;
                            }
                            break;
                        default:
                            _log.Debug("预授权类型不能为其它值");
                            break;
                    }
                }
                else
                {
                    labelMsg.Content = "预授权异常";
                    _log.Error("预授权异常");
                    _log.Info("预授权异常");
                }
                return;
            }
            try
            {
                //解释：进入这个里面说明还在输入密码阶段
                var data = _bankCardPaymentCall.PinReadOneByte();//获取输入密码状态
                switch (data)
                {
                    case "输入了一个字符":
                        tbPassword.Text += "*";
                        _count++;
                        break;
                    case "清除":
                        var len = tbPassword.Text.Length;
                        tbPassword.Text = tbPassword.Text.Substring(0, len - 1);
                        _count--;
                        break;
                    case "确定": tbPassword.Text += "*"; break;
                    case "超时": tbPassword.Text += "*"; break;
                    case "取消": tbPassword.Text += "*"; break;
                    default:
                        _log.Error("输入密码异常");
                        _log.Info("输入密码异常");
                        break;
                }
            }
            catch (Exception ex)
            {
                labelMsg.Content = "收取100元出错";
                _subTimer.Stop();
                MachineError.ErrCode = ErrorCode.BANKCARD_ERROR;
                MachineError.ErrMsg = "收取100元出错：" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex.Message);
                _log.Info("收取100元出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 关闭wpf页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            CloseTimer();
            _log.Info("退出界面");
        }

        /// <summary>
        /// 操作倒计时 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTimerTick(object sender, EventArgs e)
        {
            _num++;
            labelNum.Content = _timeout - _num;
            if (_num > _timeout - 1)
            {
                try
                {
                    ReturnToMainPage();
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }


        /// <summary>
        /// 关闭定时器
        /// </summary>
        private new void CloseTimer()
        {
            _mainTimer.Stop();
        }

        /// <summary>
        /// 返回到主页面
        /// </summary>
        private void ReturnToMainPage()
        {
            CloseTimer();
            if (_navService == null) return;
            var next = new IndexPage();
            _navService.Navigate(next);
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            var next = new BankCardPaymentOfone();
            _navService.Navigate(next);
        }

        private void OtherPayment_Click(object sender, RoutedEventArgs e)
        {
            var next = new SwitchPayMethod();
            _navService.Navigate(next);
        }
    }
}
