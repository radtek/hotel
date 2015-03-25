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
    /// InputPhone.xaml 的交互逻辑
    /// </summary>
    public partial class InputPhone : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private int _num;
        private static int _timeout;
        private string _macId;
        private NavigationService _navService;
        const int PhoneNumberLength = 11; //手机号长度
        private string beginTime = "";//每天起始入住时间
        private string endTime = "";//退房截止时间

        public InputPhone()
        {
            _log.Info("打开界面");
            InitializeComponent();

            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _macId = setting.MacId;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Common.Speak(Properties.Resources.INPUTPHONE);

            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;
            Step.BtInit();
            Step.SetStep(0);

            _navService = NavigationService.GetNavigationService(this);
            if (!string.IsNullOrEmpty(CheckInInfo.PhoneNumber))
            {
                tbPhone.Text = CheckInInfo.PhoneNumber;
            }
            tbPhone.Focus();
            if (CheckInInfo.CheckInDateTime != DateTime.MinValue && CheckInInfo.CheckOutDateTime != DateTime.MinValue)
            {
                TbCheckinYear.Text = CheckInInfo.CheckInDateTime.Year + "";
                TbCheckinMonth.Text = CheckInInfo.CheckInDateTime.Month + "";
                TbCheckinDay.Text = CheckInInfo.CheckInDateTime.Day + "";
                TbCheckoutYear.Text = CheckInInfo.CheckOutDateTime.Year + "";
                TbCheckoutMonth.Text = CheckInInfo.CheckOutDateTime.Month + "";
                TbCheckoutDay.Text = CheckInInfo.CheckOutDateTime.Day + "";
            }
            else
            {
                DateTime now = DateTime.Now;//当前年月日
                TbCheckinYear.Text = now.Year + "";
                TbCheckinMonth.Text = now.Month + "";
                TbCheckinDay.Text = now.Day + "";
                now = now.AddDays(1);//默认住一天，下一天的年月日
                TbCheckoutYear.Text = now.Year + "";
                TbCheckoutMonth.Text = now.Month + "";
                TbCheckoutDay.Text = now.Day + "";
            }
            string tag = "当天{0}之后入住,到次日的{1}为一天。";
            InterFace interFace = new InterFace();
            string rztfTime = interFace.QueryRztfTime();
            string[] times = rztfTime.Split('|');
            beginTime = times[0];
            endTime = times[1];
            label1.Content = string.Format(tag, beginTime, endTime);
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
            LbDateTime.Content = "当前时间：" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss");
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
            if (string.IsNullOrEmpty(phoneNumber))
            {
                labelMsg.Content = Properties.Resources.PHONENULL;
                tbPhone.Focus();
                return;
            }
            if (phoneNumber.Length != PhoneNumberLength)
            {
                labelMsg.Content = Properties.Resources.PHONENUMBERLENGTH;
                tbPhone.Focus();
                return;
            }
            //检查时间
            if (!CheckDateTime())
            {
                return;
            }

            //todo:手机号正则验证。
            //if (CheckInInfo.PhoneNumber != phoneNumber)
            //{
            try
            {
                QueryNoAndPj pj = CheckInBll.GetCheckInInfo("", phoneNumber, CheckInInfo.CheckInDateTime, CheckInInfo.CheckOutDateTime);

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
                else if (pj.Status)
                {
                    CheckInInfo.Building = pj.Building;
                    CheckInInfo.Floor = pj.Floor;
                    CheckInInfo.Towards = pj.Towards;
                    CheckInInfo.RoomType = pj.RoomTypeId;
                    CheckInInfo.RoomTypeName = pj.RoomTypeName;
                    CheckInInfo.RoomNum = pj.RoomNum;
                    CheckInInfo.CheckInDateTime = pj.CheckInTime;
                    CheckInInfo.CheckOutDateTime = pj.CheckOutTime;
                    CheckInInfo.Note = pj.Note + "双早";//散客入住都是双早
                    CheckInInfo.PhoneNumber = phoneNumber;
                }
                else
                {
                    labelMsg.Content = pj.Message;
                    tbPhone.Focus();
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

        /// <summary>
        /// 界面关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 检查入住日期、退房时间是否合法；如果合法保存到静态变量中
        /// </summary>
        /// <returns></returns>
        private bool CheckDateTime()
        {
            var checkinYear = int.Parse(TbCheckinYear.Text);
            var checkinMonth = int.Parse(TbCheckinMonth.Text);
            var checkinDay = int.Parse(TbCheckinDay.Text);
            var checkoutYear = int.Parse(TbCheckoutYear.Text);
            var checkoutMonth = int.Parse(TbCheckoutMonth.Text);
            var checkoutDay = int.Parse(TbCheckoutDay.Text);

            if (checkinYear < DateTime.Now.Year)
            {
                labelMsg.Content = "入住年份有错，请检查。";
                TbCheckinYear.Focus();
                return false;
            }
            if (checkinMonth <= 0 || checkinMonth > 12)
            {
                labelMsg.Content = "入住月份有错，请检查。";
                TbCheckinMonth.Focus();
                return false;
            }
            if (checkinDay <= 0 || checkinDay > 31)
            {
                labelMsg.Content = "入住日期有错，请检查。";
                TbCheckinDay.Focus();
                return false;
            }

            DateTime checkinTime;
            DateTime checkoutTime;
            try
            {
                checkinTime = DateTime.Parse(checkinYear + "-" + checkinMonth + "-" + checkinDay + " " + DateTime.Now.ToString("HH:mm"));
            }
            catch (Exception exp)
            {
                labelMsg.Content = "入住时间有错，请检查。";
                TbCheckinDay.Focus();
                _log.Debug(exp);
                return false;
            }

            if (checkoutYear < CheckInInfo.CheckInDateTime.Year)
            {
                labelMsg.Content = "退房年份不能小于入住年份，请检查。";
                TbCheckoutYear.Focus();
                return false;
            }
            if (checkoutMonth <= 0 || checkoutMonth > 12)
            {
                labelMsg.Content = "退房月份有错，请检查。";
                TbCheckoutMonth.Focus();
                return false;
            }
            if (checkoutDay <= 0 || checkoutDay > 31)
            {
                labelMsg.Content = "退房日期有错，请检查。";
                TbCheckoutDay.Focus();
                return false;
            }

            try
            {
                checkoutTime = DateTime.Parse(checkoutYear + "-" + checkoutMonth + "-" + checkoutDay + " " + endTime);
            }
            catch (Exception exp)
            {
                labelMsg.Content = "退房时间有错，请检查。";
                TbCheckoutDay.Focus();
                _log.Debug(exp);
                return false;
            }
            if (CheckInInfo.CheckInDateTime > checkoutTime)
            {
                labelMsg.Content = "入住时间不能大于退房时间。";
                TbCheckoutDay.Focus();
                return false;
            }
            CheckInInfo.CheckInDateTime = checkinTime;
            CheckInInfo.CheckOutDateTime = checkoutTime;
            return true;
        }

        private void TbCheckYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            var year = tb.Text;
            if (year.Length > 4)
            {
                tb.Text = year.Substring(0, 4);
                tb.Select(PhoneNumberLength, 0);
            }
            if (year.Length <= 0)
            {
                tb.Text = DateTime.Now.Year + "";
                tb.Select(PhoneNumberLength, 0);
            }
        }

        private void TbCheckMonthDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            var year = tb.Text;
            if (year.Length > 2)
            {
                tb.Text = year.Substring(0, 2);
                tb.Select(PhoneNumberLength, 0);
            }
            if (year.Length <= 0)
            {
                tb.Text = "1";
                tb.Select(PhoneNumberLength, 0);
            }
        }
    }
}
