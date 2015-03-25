using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.Model;
using CheckIn.common;
using CommonLibrary;
using HotelCheckIn_Interface_Hardware.BankOfCard;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// Interaction logic for BankCardPaymentOfone.xaml
    /// </summary>
    public partial class BankCardPaymentOfone : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _mainTimer = new DispatcherTimer();//主定时器，记录主页面的操作倒计时
        private readonly DispatcherTimer _subTimer = new DispatcherTimer();//收取银行卡的定时器

        private int _num;
        private static int _timeout;//倒计时秒数,默认为100秒
        private int _yajin;
        private string _rate;

        //----------------------------------------------------------------------------------
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

        private readonly BankCardPaymentCall _bankCardPaymentCall;
        private NavigationService _navService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BankCardPaymentOfone()
        {
            _log.Info("打开界面");
            InitializeComponent();
            var setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _yajin = int.Parse(setting.Rate);
            //if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode)) //如果验证码不为空，则价格、价格码取自配置文件或者PMS
            //{
            //    _rate = _yajin.ToString();//是团购，只需要支付押金
            //}
            //else
            //{
            _rate = ((int)(CheckInInfo.RoomRate + _yajin)).ToString();//不是团购需要支付：房费+押金
            //}
            CheckInInfo.PaymentAmount.Add(float.Parse(_rate));
            Money = _rate;
            PreAuthType = CheckInInfo.PreauthType;
            PreAuthNumber = CheckInInfo.PreauthNumber;
            PreAuthDate = CheckInInfo.Dt.ToString("MMdd");
            RoomNumber = CheckInInfo.RoomNum;
            CheckOrderNumber = CheckInInfo.CheckinCode;
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
            _subTimer.Interval = new TimeSpan(0, 0, 1);

            _num = 0;
            Step.BtInit();
            Step.SetStep(3);

            _navService = NavigationService.GetNavigationService(this);

            //---------------------------------------这里放的是张威的代码--------------------------------------
            var result = false;
            try
            {
                ////最多三次调用注册函数
                for (var i = 0; i < 3; i++)
                {
                    result = _bankCardPaymentCall.AppsLogin();//程序注册函数
                    if (result)
                    {
                        _log.Info("注册函数成功");
                        break;//注册函数成功
                    }
                    _log.Info("注册函数失败");
                }
                if (result)
                {
                    result = _bankCardPaymentCall.UmsOpenCard();//读卡器初始化
                    if (result)
                    {
                        _subTimer.Start();//读卡器初始化成功
                        _log.Info("读卡器初始化成功");
                        switch (PreAuthType)
                        {
                            case "1":
                                labelMsg.Content = "本次预授权:" + Money + "元，请插入银行卡";
                                break;
                            case "2":
                                labelMsg.Content = "本次预授权完成:" + Money + "元，请插入银行卡";
                                break;
                            default:
                                _log.Debug("预授权类型不能为其它值");
                                break;
                        }

                    }
                }
                else
                {
                    labelMsg.Content = "注册函数或者读卡器初始化失败,请重试";
                }
            }
            catch (Exception ex)
            {
                MachineError.ErrCode = ErrorCode.BANKCARD_ERROR;
                MachineError.ErrMsg = "注册函数或者读卡器初始化出错：" + ex.Message;
                MachineError.AllLock = true;
                labelMsg.Content = "注册函数或者读卡器初始化出错";
                _log.Error(ex.Message);
                _log.Info("注册函数或者读卡器初始化出错：" + ex.Message);
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
        /// 读插入银行卡状态定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubTimerTick(object sender, EventArgs e)
        {
            try
            {
                var data = _bankCardPaymentCall.UmsCheckCard();//检测卡位置
                if (data == "读卡器内有卡")
                {
                    //跳入输入密码界面
                    _subTimer.Stop();
                    var next = new BankCardPaymentOftwo(Money, PreAuthType, PreAuthNumber, PreAuthDate, RoomNumber, CheckOrderNumber);
                    _navService.Navigate(next);
                    _log.Info("读卡器内有卡,正准备跳入输入密码界面");
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
        /// 关闭定时器
        /// </summary>
        private new void CloseTimer()
        {
            _mainTimer.Stop();
            _subTimer.Stop();
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


    }
}