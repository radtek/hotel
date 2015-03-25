using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.Bll;
using CheckIn.common;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// AdminProcess.xaml 的交互逻辑
    /// </summary>
    public partial class AdminProcess : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private static int _keyLen;
        private int _num;
        private static int _timeout;
        private NavigationService _navService;

        public AdminProcess(int rate, int insertValue)
        {
            _log.Info("打开界面");
            InitializeComponent();
            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _keyLen = setting.KeyLenght;
            labelMsg.Content = "应收款：" + rate + "实收款：" + insertValue;
            _log.Debug("应收款：" + rate + "实收款：" + insertValue);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _navService = NavigationService.GetNavigationService(this);
            TbKey.Password = string.Empty;
            TbKey.Focus();

            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;
            Step.BtInit();
            Step.SetStep(3);
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
                        _navService.Navigate(new IndexPage());
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
            //验证密码合法性
            string pwd = TbKey.Password.Trim();
            if (pwd.Length <= 0)
            {
                labelMsg.Content = Properties.Resources.PASSWORDNULL;
                TbKey.Focus();
                return;
            }
            bool b = CheckPassword.CheckPwd(pwd);
            if (!b)
            {
                labelMsg.Content = Properties.Resources.PASSWORDWORRY;
                TbKey.Focus();
                return;
            }
            if (_navService != null)
            {
                _navService.Navigate(new IndexPage());
            }
        }


        private void TbKey_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            keyboard.Visibility = Visibility.Visible;
        }

        private void TbKey_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var key = TbKey.Password.Trim();
            if (key.Length > _keyLen)
            {
                TbKey.Password = key.Substring(0, _keyLen);
                TbKey.SelectAll();
            }
        }
    }
}
