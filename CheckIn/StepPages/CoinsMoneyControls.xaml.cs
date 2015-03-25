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
using HotelCheckIn_Interface_Hardware.CoinsMachine;
using HotelCheckIn_Interface_Hardware.Into_Notes;
using HotelCheckIn_Interface_Hardware.Out_Notes;

namespace CheckIn.StepPages
{
    /// <summary>
    /// Interaction logic for CoinsMoneyControls.xaml
    /// </summary>
    public partial class CoinsMoneyControls : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _mainTimer = new DispatcherTimer();//主定时器，记录主页面的操作倒计时
        private readonly DispatcherTimer _subTimer = new DispatcherTimer();//收取银行卡的定时器

        private int _num;
        private static int _timeout;//倒计时秒数,默认为100秒

        private readonly int _totalMoneyOut = 113;

        private NavigationService _navService;
        private readonly CoinsMachineService _coinsMachineService;//出硬币服务类
        private readonly Cc _cc;//出纸币服务类

        /// <summary>
        /// 构造函数
        /// </summary>
        public CoinsMoneyControls()
        {
            _log.Info("打开界面");
            InitializeComponent();
            _coinsMachineService = new CoinsMachineService();
            _coinsMachineService.log += _log.Debug;

            var setting = new SettingHelper();
            _coinsMachineService.ComPort = setting.CoinsComPort;

            _cc = new Cc(setting.CcComPort);
            _cc.log += _log.Debug;

            _timeout = setting.TimeOut;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="total">要出的硬币数量</param>
        public CoinsMoneyControls(int total)
        {
            _log.Info("打开界面");
            InitializeComponent();
            _coinsMachineService = new CoinsMachineService();
            _coinsMachineService.log += _log.Debug;
            var setting = new SettingHelper();
            _coinsMachineService.ComPort = setting.CoinsComPort;
            _timeout = setting.TimeOut;

            _cc = new Cc(setting.CcComPort);
            _cc.log += _log.Debug;

            _totalMoneyOut = total;
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

            _subTimer.Tick += OutNotesAndCoinsMoney;//出钞倒计时
            _subTimer.Interval = new TimeSpan(0, 0, 1);

            _num = 0;
            Step.BtInit();
            Step.SetStep(3);
            _navService = NavigationService.GetNavigationService(this);

            _subTimer.Start();//启动出币线程
        }

        /// <summary>
        /// 出纸币和硬币方法
        /// </summary>
        private void OutNotesAndCoinsMoney(object sender, EventArgs ex)
        {
            var resultBool = false;
            var resultStr = "";

            var coins = _totalMoneyOut % 10;//要出的硬币值
            _log.Info("要出的硬币值" + coins);
            var notes = _totalMoneyOut - coins;//要出的纸币值
            _log.Info("要出的纸币值" + notes);

            //===========================出纸币开始==============================

            try
            {
                var status0 = new Sensor0Status();
                var status1 = new Sensor1Status();
                var error = new Error();
                _cc.QueryStatus(ref status0, ref status1, ref error);
                _log.Info("error代码是：0x" + Convert.ToString(error.Code, 16));
                _log.Info("上部传感器代码是：" + Convert.ToString(status0.NearendSensor, 16));
                _log.Info("下部传感器代码是：" + Convert.ToString(status1.NearendSensor, 16));

                if (status0.NearendSensor == 1 || status1.NearendSensor == 1)
                {
                    _subTimer.Stop();
                    MachineError.ErrCode = ErrorCode.CC_ERROR;
                    MachineError.ErrMsg = "纸币箱钱币不足";
                    MachineError.AllLock = true;
                    labelMsg.Content = "纸币箱钱币不足";
                    _log.Error(ex);
                    _log.Info("纸币箱钱币不足");
                    return;
                }
                //0x30表示good，0x31表示normal stop
                if (error.Code == 0x30 || error.Code == 0x31)
                {
                    resultStr = _cc.OutMoney(notes);//开始出纸币
                }
                else
                {
                    _subTimer.Stop();
                    MachineError.ErrCode = ErrorCode.CC_ERROR;
                    MachineError.ErrMsg = "纸币机状态检测出错，错误原因代码：" + Convert.ToString(error.Code, 16);
                    MachineError.AllLock = true;
                    labelMsg.Content = "纸币机状态检测出错，错误原因代码：" + Convert.ToString(error.Code, 16);
                    _log.Error(ex);
                    _log.Info("纸币机状态检测出错，错误原因代码：" + Convert.ToString(error.Code, 16));
                    return;
                }
            }
            catch (Exception e)
            {
                _subTimer.Stop();
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                MachineError.ErrMsg = "出纸币出错:" + e.Message;
                MachineError.AllLock = true;
                labelMsg.Content = e.Message;
                _log.Error(ex);
                _log.Info("方法名：OutMoney，出错原因：" + e.Message);
            }
            //===========================出纸币结束==============================
            //return;测试用
            if (resultStr == "出钞成功")
            {
                //===========================出硬币开始==============================
                try
                {
                    resultBool = _coinsMachineService.OpenComPort();//打开串口
                }
                catch (Exception e)
                {
                    _subTimer.Stop();
                    _coinsMachineService.CloseComPort();
                    MachineError.ErrCode = ErrorCode.CC_ERROR;
                    MachineError.ErrMsg = "出硬币打开串口出错:" + e.Message;
                    MachineError.AllLock = true;
                    labelMsg.Content = e.Message;
                    _log.Error(ex);
                    _log.Info("方法名：OutCoinsMoney，出错原因：" + e.Message);
                }

                if (resultBool)
                {
                    try
                    {
                        resultBool = _coinsMachineService.Poll();
                    }
                    catch (Exception e)
                    {
                        _subTimer.Stop();
                        _coinsMachineService.CloseComPort();
                        MachineError.ErrCode = ErrorCode.CC_ERROR;
                        MachineError.ErrMsg = "出硬币Poll方法出错:" + e.Message;
                        MachineError.AllLock = true;
                        labelMsg.Content = e.Message;
                        _log.Info("方法名：OutCoinsMoney，出错原因：" + e.Message);
                    }
                }

                if (resultBool)
                {
                    try
                    {
                        resultStr = _coinsMachineService.DispenseHopperCoins(coins);
                        _subTimer.Stop();
                        _log.Info("方法名：DispenseHopperCoins，出硬币记录：" + resultStr);
                        if (resultStr == "出币成功")
                        {
                            labelMsg.Content = "出币完成";
                        }
                        _coinsMachineService.CloseComPort();
                    }
                    catch (Exception e)
                    {
                        _subTimer.Stop();
                        _coinsMachineService.CloseComPort();
                        MachineError.ErrCode = ErrorCode.CC_ERROR;
                        MachineError.ErrMsg = "出钞DispenseHopperCoins方法出错:" + e.Message;
                        MachineError.AllLock = true;
                        labelMsg.Content = e.Message;
                        _log.Info("方法名：OutCoinsMoney，出错原因：" + e.Message);
                    }
                }
                _coinsMachineService.CloseComPort();
                _subTimer.Stop();

                //===========================出硬币结束==============================
            }
            else
            {
                _subTimer.Stop();
                MachineError.ErrCode = ErrorCode.CC_ERROR;
                MachineError.ErrMsg = "出纸币出错:" + resultStr;
                MachineError.AllLock = true;
                labelMsg.Content = resultStr;
                _log.Info("出纸币出错，出错原因：" + resultStr);
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
        private void CloseTimer()
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

