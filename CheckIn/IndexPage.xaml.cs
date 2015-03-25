using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using CheckIn.StepPages;
using CommonLibrary;
using log4net;

namespace CheckIn
{
    /// <summary>
    /// IndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class IndexPage : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private NavigationService _navService;
        private SettingHelper setting = new SettingHelper();
        private string _macId;

        public IndexPage()
        {
            InitializeComponent();
            _macId = setting.MacId;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FramWeb.Navigate(setting.ScUrl);
                _navService = NavigationService.GetNavigationService(this);
                //异常处理
                if (!string.IsNullOrEmpty(CheckInInfo.RoomNum))
                {
                    bool b = new InterFace().RoomLockSet(CheckInInfo.RoomNum, "2", CheckInInfo.PhoneNumber, CheckInInfo.ValidateCode, _macId);
                    if (!b)
                    {
                        _log.Error("解锁房间：" + CheckInInfo.RoomNum + "失败。");
                    }
                }
                //每次跳转到首页都清除入住类的数据。
                CheckInInfo.Clear();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
        /// <summary>
        /// 网络入住
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnIn_OnClick(object sender, RoutedEventArgs e)
        {
            //if (InitDevice()) return;
            if (_navService != null)
            {
                CheckInInfo.EntranceFlag = "团购入住";
                var next = new InputValidation();
                _navService.Navigate(next);
            }
        }
        /// <summary>
        /// 退房点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOut_OnClick(object sender, RoutedEventArgs e)
        {
            //if (InitDevice()) return;
            if (_navService != null)
            {
                CheckInInfo.EntranceFlag = "退房";
                var next = new CollectionRoomCard();
                _navService.Navigate(next);
            }
        }
        /// <summary>
        /// 硬件初始化
        /// </summary>
        /// <returns></returns>
        private bool InitDevice()
        {
            string msg = "";
            CheckHard c = new CheckHard();
            if (!c.CheckHard_All(ref msg))
            {
                _log.Debug("硬件检测：--" + msg);
                MessageBox.Show(msg);
                MachineError.AllLock = true;
                MachineError.ErrMsg = msg;
                MachineError.ErrCode = ErrorCode.OTHER_ERROR;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 散客入住
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInsk_Click(object sender, RoutedEventArgs e)
        {
            //if (InitDevice()) return;
            if (_navService != null)
            {
                CheckInInfo.EntranceFlag = "散客入住";
                var next = new InputPhone();
                _navService.Navigate(next);
            }
        }
    }
}
