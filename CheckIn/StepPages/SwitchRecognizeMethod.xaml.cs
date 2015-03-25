using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.common;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// SwitchRecognizeMethod.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchRecognizeMethod : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private int _num;
        private static int _timeout;
        private NavigationService _navService;

        public SwitchRecognizeMethod()
        {
            _log.Info("打开界面");
            InitializeComponent();

            _timeout = new SettingHelper().TimeOut;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Common.Speak("身份证与本人不符！");
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();

            _num = 0;
            Step.BtInit();
            Step.SetStep(2);

            _navService = NavigationService.GetNavigationService(this);
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
                _dTimer.Stop();
                try
                {
                    if (_navService != null)
                    {
                        var next = new IndexPage();
                        _navService.Navigate(next);
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            _log.Info("退出界面");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            if (_navService != null)
            {
                var next = new IndexPage();
                _navService.Navigate(next);
            }
        }

        private void Retry_Click(object sender, RoutedEventArgs e)
        {
            if (_navService != null)
            {
                _navService.Navigate(new ReadIdCard());
            }
        }

        private void AdminCheck_Click(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            if (_navService != null)
            {
                _navService.Navigate(new AdminCheck());
            }
        }
    }
}
