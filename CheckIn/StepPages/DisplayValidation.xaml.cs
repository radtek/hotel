using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.BackService;
using CheckIn.common;
using CheckIn.Model;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// DisplayValidation.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayValidation : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private int _num;
        private static int _timeout;
        private string _macId;
        private NavigationService _navService;

        public DisplayValidation()
        {
            _log.Info("打开界面");
            InitializeComponent();
            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _macId = setting.MacId;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Common.Speak(Properties.Resources.CHECKCHECKININFO);
            _navService = NavigationService.GetNavigationService(this);

            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;
            Step.BtInit();
            Step.SetStep(1);
            if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode))
            {
                LbValidationCode.Content = CheckInInfo.ValidateCode;
            }
            else
            {
                LbValidateCodewz.Visibility = Visibility.Hidden;
                LbValidationCode.Visibility = Visibility.Hidden;
            }
            LbPhone.Content = CheckInInfo.PhoneNumber;
            LbBuilding.Content = CheckInInfo.Building;
            LbFloor.Content = CheckInInfo.Floor;
            //LbTowards.Content = CheckInInfo.Towards;
            LbRate.Content = CheckInInfo.RoomRate + "元";
            LbRoomType.Content = CheckInInfo.RoomTypeName;
            LbRoomNum.Content = CheckInInfo.RoomNum;
            if (CheckInInfo.CustomerCount != 0)
            {
                TbCustomerCount.Text = CheckInInfo.CustomerCount.ToString();
            }
            LbCheckinDate.Content = CheckInInfo.CheckInDateTime.ToString("yyyy-MM-dd HH:mm");
            LbCheckoutDate.Content = CheckInInfo.CheckOutDateTime.ToString("yyyy-MM-dd HH:mm");

            LbNote.TextWrapping = TextWrapping.Wrap;
            LbNote.Text = CheckInInfo.Note;
            try
            {
                CheckInInfo.ClearCustomInfo();
            }
            catch (Exception ex)
            {
                _log.Debug("清除入住人信息出错：" + ex);
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

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CheckInInfo.CustomerCount = int.Parse(TbCustomerCount.Text);
            _dTimer.Stop();
            if (_navService != null)
            {
                var next = new ReadIdCard();
                _navService.Navigate(next);
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            _log.Info("退出界面");
        }

        private void PopCutClick(object sender, RoutedEventArgs e)
        {
            var tmp = TbCustomerCount.Text.Length > 0 ? TbCustomerCount.Text : "1";
            int cou = int.Parse(tmp);
            if (cou > 1)
            {
                TbCustomerCount.Text = (cou - 1).ToString();
            }
        }

        private void PopAddClick(object sender, RoutedEventArgs e)
        {
            var tmp = TbCustomerCount.Text.Length > 0 ? TbCustomerCount.Text : "1";
            int cou = int.Parse(tmp);
            if (cou < 2)
            {
                TbCustomerCount.Text = (cou + 1).ToString();
            }
        }

        private void Pre_Click(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            if (_navService != null)
            {
                if (!string.IsNullOrEmpty(CheckInInfo.RoomNum))
                {
                    bool b = new InterFace().RoomLockSet(CheckInInfo.RoomNum, "2", CheckInInfo.PhoneNumber, CheckInInfo.ValidateCode, _macId);
                    if (!b)
                    {
                        _log.Error("解锁房间：" + CheckInInfo.RoomNum + "失败。");
                    }
                }
                if (!string.IsNullOrEmpty(CheckInInfo.BookMark))
                {
                    if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode))
                    {
                        var next = new InputValidation();
                        _navService.Navigate(next);
                    }
                    else
                    {
                        var next = new InputPhone();
                        _navService.Navigate(next);
                    }
                }
                else
                {
                    var next = new RoomSelect();
                    _navService.Navigate(next);
                }
            }
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
    }
}
