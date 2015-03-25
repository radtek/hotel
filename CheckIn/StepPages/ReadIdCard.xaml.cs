﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using CommonLibrary;
using HotelCheckIn_Interface_Hardware.ReadIdCard;
using HotelCheckIn_InterfaceSystem.model;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// ReadIdCard.xaml 的交互逻辑
    /// </summary>
    public partial class ReadIdCard : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private static int _timeout;
        private int _num;
        private ResultIdModel result = new ResultIdModel();
        private IdCard _idCard;
        private NavigationService _navService;
        private string readcardtype;

        public ReadIdCard()
        {
            _log.Info("打开界面");
            InitializeComponent();
            var setting = new SettingHelper();
            _timeout = setting.TimeOut;
            readcardtype = setting.ReadCardType;
            _idCard = new IdCard(readcardtype);

            if (CheckInInfo.CheckedCount == 0)
            {
                //删除第一个客人
                if (CheckInInfo.GuestList.Count > 0)
                {
                    CheckInInfo.GuestList.RemoveAt(0);
                }
            }
            else
            {
                //删除第二个客人
                if (CheckInInfo.GuestList.Count > 1)
                {
                    CheckInInfo.GuestList.RemoveAt(1);
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Common.Speak(Properties.Resources.PLACEIDCARD);

            try
            {
                _log.Debug("打开读身份证");
                _idCard.OpenPort();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;

            Step.BtInit();
            Step.SetStep(2);
            _navService = NavigationService.GetNavigationService(this);

            labelMsg.Content = Properties.Resources.IDCARD_MSG;
            if (CheckInInfo.CustomerCount <= 1)
            {
                return;
            }
            labelMsg.Content = "入住 " + CheckInInfo.CustomerCount + " 人，已验证 "
                               + CheckInInfo.CheckedCount + " 人。";

        }

        /// <summary>
        /// 操作倒计时 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DTimerTick(object sender, EventArgs e)
        {
            if (_num >= _timeout - 1)
            {
                Close();
                if (_navService != null)
                    _navService.Navigate(new IndexPage());
                return;
            }
            _num++;
            labelNum.Content = _timeout - _num;
            try
            {
                labelMsg.Content = Properties.Resources.IDCARD_MSG;
                if (!_idCard.IfHaveCard())
                {
                    _log.Debug("未找到身份证。");
                    return;
                }
                result = _idCard.ReadCard();
                _log.Debug("打开读身份证1");
                _idCard.OpenPort();
                if (null == result)
                {
                    _log.Debug("读取身份证出错。");
                    return;
                }
                string availday = result.UserLifeEnd.Trim();
                if (availday.Length == 8)
                {
                    string str = availday.Substring(0, 4) + "-" + availday.Substring(4, 2) + "-" + availday.Substring(6, 2);
                    if (DateTime.Now > DateTime.Parse(str))
                    {
                        labelMsg.Content = Properties.Resources.IDCARDOVERDUE;
                        return;
                    }
                }

                var checkCount = CheckInInfo.CustomerCount;
                if (checkCount > 0)
                {
                    if (IsIdCardHaveChecked(result.IDCardNo))
                    {
                        labelMsg.Content = Properties.Resources.CHANAGE_IDCARD;
                        return;//判断是否已经验证过。
                    }
                    else
                    {
                        var checkedPeople = CheckInInfo.CheckedCount;
                        var msg = "入住 " + CheckInInfo.CustomerCount + " 人，已验证 " + checkedPeople + " 人。";
                        labelMsg.Content = msg;
                        _log.Debug(msg);
                    }
                }
            }
            catch (Exception ex)
            {
                var indexPage = new IndexPage
                {
                    labelMsg = { Content = Properties.Resources.MACHINE_ERROR },
                };
                indexPage.labelMsg.UpdateLayout();

                MachineError.AllLock = true;
                MachineError.ErrCode = ErrorCode.IDCARD_ERROR;
                MachineError.ErrMsg = Properties.Resources.IDCARDDEVICEFAULT;
                Close();
                if (_navService != null)
                    _navService.Navigate(indexPage);
                _log.Error(ex);
                return;
            }
            try
            {
                Guest guest = GetGuestInfoFromIdCard();
                Close();
                if (_navService != null)
                {
                    //身份证之后拍照识别
                    var next = new Detect(CheckInInfo.CustomerCount, guest);
                    _navService.Navigate(next);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        /// <summary>
        /// 读取住客信息
        /// </summary>
        /// <returns></returns>
        public Guest GetGuestInfoFromIdCard()
        {
            var guest = new Guest();
            if (string.IsNullOrEmpty(result.IDCardNo))
            {
                _log.Debug("身份证号读取异常，身份证号为空!");
                return null;
            }

            var birthdayStr = result.Born;
            guest.Birthday = birthdayStr.Substring(0, 4) + "-" + birthdayStr.Substring(4, 2) + "-" + birthdayStr.Substring(6, 2);
            guest.id_Card_No = result.IDCardNo;
            guest.guest_Name = result.Name;
            guest.gender_Id = result.Sex;//男，女
            guest.id_Card_Type_Id = "1";//1表示身份证
            guest.NamePy = "";

            int i = CheckInInfo.CheckedCount;

            guest.Nation = result.Nation;
            guest.Availability = result.UserLifeEnd;
            guest.Address = result.Address;
            guest.Admin = "0";
            guest.PhotoFromIdCard = null;
            guest.PhotoFromIdCardSave = result.img;
            var idCardImagePath = Path.GetTempPath() + "idcard.bmp";
            File.WriteAllBytes(idCardImagePath, result.img);
            if (i == 0)
            {
                if (CheckInInfo.CustomImage.Count > 0)
                {
                    CheckInInfo.CustomImage[i] = guest.PhotoFromIdCardSave;
                }
                else
                {
                    CheckInInfo.CustomImage.Add(guest.PhotoFromIdCardSave);
                }
            }
            else
            {
                if (CheckInInfo.CustomImage.Count > 2)
                {
                    CheckInInfo.CustomImage[i + 1] = guest.PhotoFromIdCardSave;
                }
                else
                {
                    CheckInInfo.CustomImage.Add(guest.PhotoFromIdCardSave);
                }
            }

            return guest;
        }

        /// <summary>
        /// 根据身份证号，判断是否已经验证
        /// </summary>
        /// <param name="idCardNo"></param>
        /// <returns></returns>
        private bool IsIdCardHaveChecked(string idCardNo)
        {
            var guestList = CheckInInfo.GuestList;
            return guestList.Any(guest => guest.id_Card_No == idCardNo);
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            Close();
            _log.Info("退出界面");
        }

        private void Close()
        {
            _dTimer.Stop();
            _idCard.ClosePort();
        }

        private void Pre_Click(object sender, RoutedEventArgs e)
        {
            if (_navService != null)
            {
                var next = new DisplayValidation();
                _navService.Navigate(next);
            }
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
    }
}
