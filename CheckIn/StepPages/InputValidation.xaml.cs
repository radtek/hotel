using System;
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
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// InputValidation.xaml 的交互逻辑
    /// </summary>
    public partial class InputValidation : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static int _keyLen;
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private int _num;
        private string _macId;
        private static int _timeout;
        private NavigationService _navService;
        const int PhoneNumberLength = 11; //手机号长度

        public InputValidation()
        {
            _log.Info("打开界面");
            InitializeComponent();

            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _keyLen = setting.KeyLenght;
            _macId = setting.MacId;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Common.Speak("请输入订单号和手机号！");

            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;
            Step.BtInit();
            Step.SetStep(0);

            _navService = NavigationService.GetNavigationService(this);
            if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode))
            {
                TbKey.Text = CheckInInfo.ValidateCode;
            }
            if (!string.IsNullOrEmpty(CheckInInfo.PhoneNumber))
            {
                tbPhone.Text = CheckInInfo.PhoneNumber;
            }
            TbKey.Focus();
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
            var phoneNumber = tbPhone.Text;
            if (string.IsNullOrEmpty(TbKey.Text))
            {
                labelMsg.Content = Properties.Resources.VALIDATIONNULL;
                TbKey.Focus();
                return;
            }
            if (string.IsNullOrEmpty(phoneNumber))
            {
                labelMsg.Content = Properties.Resources.PHONENULL;
                tbPhone.Focus();
                return;
            }
            else
            {
                if (phoneNumber.Length != PhoneNumberLength)
                {
                    labelMsg.Content = Properties.Resources.PHONENUMBERLENGTH;
                    tbPhone.Focus();
                    return;
                }
            }
            //todo:手机号正则验证。
            //if (CheckInInfo.ValidateCode != TbKey.Text)
            //{
            try
            {
                //判断验证码是否有效(如果有效存入数据库中)
                QueryNoAndPj pj = CheckInBll.GetCheckInInfo(TbKey.Text, tbPhone.Text, DateTime.MinValue, DateTime.MinValue);
                //是否预订,1-是，2-否
                if (pj.Status && pj.IsBook == "1")//预订一个房间
                {
                    CheckInInfo.BookMark = pj.IsBook;
                    CheckInInfo.Building = pj.Building;
                    CheckInInfo.Floor = pj.Floor;
                    CheckInInfo.Towards = pj.Towards;
                    CheckInInfo.RoomType = pj.RoomTypeId;
                    CheckInInfo.RoomTypeName = pj.RoomTypeName;
                    CheckInInfo.RoomNum = pj.RoomNum;
                    CheckInInfo.CheckInDateTime = pj.CheckInTime;
                    CheckInInfo.CheckOutDateTime = pj.CheckOutTime;
                    CheckInInfo.Note = pj.Note;
                    CheckInInfo.ValidateCode = TbKey.Text;
                    CheckInInfo.PhoneNumber = phoneNumber;
                    CheckInInfo.RoomRate = pj.Rate;
                    CheckInInfo.RoomCode = pj.RoomRateCode;
                    CheckInInfo.OrderPmsCode = pj.PmsOrderId;
                    if (!string.IsNullOrEmpty(pj.RoomNum))
                    {
                        bool b = new InterFace().RoomLockSet(pj.RoomNum, "1", CheckInInfo.PhoneNumber, CheckInInfo.ValidateCode, _macId);
                        if (!b)
                        {
                            labelMsg.Content = "锁定房间：" + pj.RoomNum + "失败，请选择其他房间。";
                            return;
                        }
                        _dTimer.Stop();
                        if (_navService != null)
                        {
                            var next = new DisplayValidation();
                            _navService.Navigate(next);
                        }
                        return;
                    }
                }
                    //无预定房间
                else if (pj.Status)
                {
                    CheckInInfo.Building = pj.Building;
                    CheckInInfo.Floor = pj.Floor;
                    CheckInInfo.Towards = pj.Towards;
                    CheckInInfo.RoomType = pj.RoomTypeId;
                    CheckInInfo.RoomNum = pj.RoomNum;
                    CheckInInfo.CheckInDateTime = pj.CheckInTime;
                    CheckInInfo.CheckOutDateTime = pj.CheckOutTime;
                    CheckInInfo.Note = pj.Note;
                    CheckInInfo.ValidateCode = TbKey.Text;
                    CheckInInfo.PhoneNumber = tbPhone.Text;
                }
                else
                {
                    labelMsg.Content = pj.Message;
                    TbKey.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MachineError.ErrCode = ErrorCode.SENDCARD_ERROR;
                MachineError.ErrMsg = "通讯出错:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex);
                return;
            }
            //}
            _dTimer.Stop();
            if (_navService != null) 
            {
                var next = new RoomSelect();
                _navService.Navigate(next);
            }
        }

        private void TbKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            var key = TbKey.Text;
            if (key.Length > _keyLen)
            {
                TbKey.Text = key.Substring(0, _keyLen);
                TbKey.Select(_keyLen, 0);
            }
        }

        private void TbKey_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            keyboard.Visibility = Visibility.Visible;
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            _log.Info("退出界面");
        }

        private void Pre_Click(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            if (_navService != null)
            {
                var next = new IndexPage();
                _navService.Navigate(next);
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

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            var key = tbPhone.Text;
            if (key.Length > PhoneNumberLength)
            {
                tbPhone.Text = key.Substring(0, PhoneNumberLength);
                tbPhone.Select(PhoneNumberLength, 0);
            }
        }

        private void tbPhone_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            keyboard.Visibility = Visibility.Visible;
        }
    }
}
