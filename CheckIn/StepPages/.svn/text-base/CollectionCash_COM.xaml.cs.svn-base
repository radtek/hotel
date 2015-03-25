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
using HotelCheckIn_Interface_Hardware.Into_Notes;

namespace CheckIn.StepPages
{
    /// <summary>
    /// CollectionCash_COM.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionCash_COM : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private readonly DispatcherTimer _rcTimer = new DispatcherTimer();
        private readonly IntoNotesService _intoNotesService = new IntoNotesService();
        private readonly LedLightApi _ledLightApi = new LedLightApi();

        private static int _timeout;
        private static string _macId;

        private int _num;
        private bool _retry = false;
        private NavigationService _navService;
        private string _rate;
        private string _insertValue = "0";

        private int _money = 0;//剩余多少钱没有入
        private int _inmoney = 0;//已经入了多少钱
        /// <summary>
        /// 标记是否正在出钞
        /// </summary>
        private bool _ccFlag = false;
        /// <summary>
        /// 标记是否需要退钞
        /// </summary>
        private bool _tcFlag = false;

        public CollectionCash_COM()
        {
            _log.Info("打开界面");
            _intoNotesService.log += _log.Debug;
            InitializeComponent();
            var setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _macId = setting.MacId;

            // _ledLightApi.ComPort = setting.LedCom;
            _intoNotesService.ComPort = setting.IntoNotesPort;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Tick += DTimerTick;//主倒计时
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();

            _rcTimer.Tick += RcTimerTick;//入钞倒计时
            _rcTimer.Interval = new TimeSpan(0, 0, 1);

            _num = 0;
            Step.BtInit();
            Step.SetStep(3);

            _navService = NavigationService.GetNavigationService(this);
            if (string.IsNullOrEmpty(CheckInInfo.ValidateCode))
            {
                var interFace = new InterFace();
                _rate = "118"; //interFace.GetRoomRate();//返回钱
                _money = int.Parse(_rate);
            }
            else
            {
                _rate = XmlHelper.ReadNode("rate");
                _money = int.Parse(_rate);
            }

            //Common.Speak(string.Format(Properties.Resources.PUTINMONEY, int.Parse(_rate) / 100));
            try
            {
                RcInit();
            }
            catch (Exception ex)
            {
                MachineError.ErrMsg = "入钞出错:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex);
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
            //出钞
            if (_ccFlag && _tcFlag)
            {

            }
            if (_num > _timeout - 1)
            {
                try
                {
                    //Back_Click(null, null);
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        private static int _count = 0;//定时器执行的次数
        /// <summary>
        /// 移动纸币定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RcTimerTick(object sender, EventArgs e)
        {
            _count++;
            _log.Debug("第" + _count + "次");
            bool iswhat;
            var result = 0;
            byte[] bt = { 0xfc, 0x05, 0x11, 0x27, 0x56 };
            var btReturn = new byte[] { 0xfc, 0x05, 0x43 };
            var vCrc = CrcCcitt.Crc(btReturn);
            var type = "";
            try
            {
                var data = _intoNotesService.QueryState(bt, type);
                if (string.IsNullOrEmpty(data))
                {
                    _log.Debug("进入了重新查询status状态内(前提是没有查询到状态)，且状态是：" + data);
                    data = _intoNotesService.QueryState(bt);
                }
                switch (data)
                {
                    case "YUAN5":
                        _log.Debug("识别5元");
                        iswhat = _dTimer.IsEnabled;
                        if (iswhat)
                        {
                            _dTimer.IsEnabled = false;
                        }
                        if ((5 > _money) && (5 > (_money + 10)))
                        {
                            _rcTimer.Stop();
                            type = "return";
                            data = _intoNotesService.QueryState(bt, type);//发送退出指令
                            _log.Debug("发送退出指令,状态是：" + data);
                            data = _intoNotesService.QueryState(bt);//确认退出指令
                            _log.Debug("确认退出指令,状态是：" + data);
                            return;
                        }
                        _money -= 5;
                        _inmoney += 5;
                        type = "stack-1";
                        data = _intoNotesService.QueryState(bt, type);
                        _log.Debug("状态是：" + data);
                        break;
                    case "YUAN10":
                        _log.Debug("识别10元");
                        iswhat = _dTimer.IsEnabled;
                        if (iswhat)
                        {
                            _dTimer.IsEnabled = false;
                        }
                        if ((10 > _money) && (10 > (_money + 10)))
                        {
                            _rcTimer.Stop();
                            type = "return";
                            data = _intoNotesService.QueryState(bt, type);//发送退出指令
                            _log.Debug("发送退出指令,状态是：" + data);
                            data = _intoNotesService.QueryState(bt);//确认退出指令
                            _log.Debug("确认退出指令,状态是：" + data);
                            return;
                        }
                        _money -= 10;
                        _inmoney += 10;
                        type = "stack-1";
                        data = _intoNotesService.QueryState(bt, type);
                        _log.Debug("状态是：" + data);
                        break;
                    case "YUAN20":
                        _log.Debug("识别20元");
                        iswhat = _dTimer.IsEnabled;
                        if (iswhat)
                        {
                            _dTimer.IsEnabled = false;
                        }
                        if ((20 > _money) && (20 > (_money + 10)))
                        {
                            _rcTimer.Stop();
                            type = "return";
                            data = _intoNotesService.QueryState(bt, type);//发送退出指令
                            _log.Debug("发送退出指令,状态是：" + data);
                            data = _intoNotesService.QueryState(bt);//确认退出指令
                            _log.Debug("确认退出指令,状态是：" + data);
                            return;
                        }
                        _money -= 20;
                        _inmoney += 20;
                        type = "stack-1";
                        data = _intoNotesService.QueryState(bt, type);
                        _log.Debug("状态是：" + data);
                        break;
                    case "YUAN50":
                        _log.Debug("识别50元");
                        iswhat = _dTimer.IsEnabled;
                        if (iswhat)
                        {
                            _dTimer.IsEnabled = false;
                        }
                        if ((50 > _money) && (50 > (_money + 10)))
                        {
                            _rcTimer.Stop();
                            type = "return";
                            data = _intoNotesService.QueryState(bt, type);//发送退出指令
                            _log.Debug("发送退出指令,状态是：" + data);
                            data = _intoNotesService.QueryState(bt);//确认退出指令
                            _log.Debug("确认退出指令,状态是：" + data);
                            return;
                        }
                        _money -= 50;
                        _inmoney += 50;
                        type = "stack-1";
                        data = _intoNotesService.QueryState(bt, type);
                        _log.Debug("状态是：" + data);
                        break;
                    case "YUAN100":
                        _log.Debug("识别100元");
                        iswhat = _dTimer.IsEnabled;
                        if (iswhat)
                        {
                            _dTimer.IsEnabled = false;
                        }
                        if ((100 > _money) && (100 > (_money + 10)))
                        {
                            _rcTimer.Stop();
                            type = "return";
                            data = _intoNotesService.QueryState(bt, type);//发送退出指令
                            _log.Debug("发送退出指令,状态是：" + data);
                            data = _intoNotesService.QueryState(bt);//确认退出指令
                            _log.Debug("确认退出指令,状态是：" + data);
                            return;
                        }
                        _money -= 100;
                        _inmoney += 100;
                        type = "stack-1";
                        data = _intoNotesService.QueryState(bt, type);
                        _log.Debug("状态是：" + data);
                        break;
                    case "ENABLE_IDLING":
                        if (_inmoney != 0)
                        {
                            _log.Debug("应收" + _rate + "元，实收" + _inmoney + "元");
                            labelMsg.Content = "应收" + _rate + "元，实收" + _inmoney + "元";
                        }
                        if (_money <= 0)
                        {
                            _log.Debug("应收" + _rate + "元，实收" + _inmoney + "元，应该退还" + _money + "元");
                            _rcTimer.Stop();
                            //labelMsg.Content = Properties.Resources.INMONEYOK;
                            _rcTimer.IsEnabled = false;
                            var btEnable = new byte[] { 0xfc, 0x07, 0xc0, 0xff, 0xff };//Enable:00,Disable:ff
                            vCrc = CrcCcitt.Crc(btEnable);
                            _intoNotesService.SettingCommand(vCrc);
                            data = _intoNotesService.QueryState(bt);
                            //收取押金业务逻辑：押金记录--跳到出卡界面
                            //var interFace = new InterFace();
                            //CheckInInfo.Dt = interFace.AddIoJournal(new IoJournal()
                            //{
                            //    IoId = _macId,
                            //    IoMoney = int.Parse(_insertValue),
                            //    IoTag = 1,
                            //    IoFrom = 1,
                            //    OrderId = CheckInInfo.ValidateCode,//订单号保存验证码
                            //    RoomNo = CheckInInfo.RoomNum,
                            //});
                            _log.Debug("收钞完成，状态是：" + data);
                            //Next_Click(null, null);
                        }
                        break;
                    default:
                        break;
                }

                if (_retry)
                {
                    _log.Debug("进来了吗");
                    labelMsg.Content = Properties.Resources.RETRYING;
                    Retry();
                }
            }
            catch (Exception ex)
            {
                _dTimer.IsEnabled = false;
                _rcTimer.IsEnabled = false;
                labelMsg.Content = "应收" + _rate + "元，实收" + _inmoney + "元";
                MachineError.ErrMsg = "入钞出错:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex);
            }

        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            Close();
            //_ledLightApi.CloseAllLed();
            _log.Info("退出界面");
        }

        private void Close()
        {
            _dTimer.Stop();
            _rcTimer.Stop();
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

        private void Retry()
        {
            _num = 0;//每次重试都重置倒计时
            _retry = false;
            _rcTimer.IsEnabled = false;

            RcInit();
        }

        private void RcInit()
        {
            labelMsg.Content = Properties.Resources.DEVICEERROR;
            byte[] btStatus = { 0xfc, 0x05, 0x11, 0x27, 0x56 };
            try
            {
                var result = _intoNotesService.OpenComPort();
            }
            catch (Exception ex)
            {
                _rcTimer.IsEnabled = false;
                MessageBox.Show(Properties.Resources.NETCOMMUNICATION, Properties.Resources.ERROR);
                labelMsg.Content = Properties.Resources.DEVICEINITERROR;
                MachineError.ErrCode = ErrorCode.RC_ERROR;
                MachineError.ErrMsg = "入钞初始化出错:" + ex.Message;
                MachineError.AllLock = true;
                Close();
                if (_navService != null)
                {
                    var next = new IndexPage();
                    _navService.Navigate(next);
                }
                _log.Error(ex);
                return;
            }
            var data = _intoNotesService.QueryState(btStatus);
            //第一次遇到disable状态时要设置成disable
            if (data != "ENABLE_IDLING" && data == "DISABLE_INHIBIT")
            {
                var btEnable = new byte[] { 0xfc, 0x07, 0xc0, 0x00, 0x00 };//Enable:00,Disable:ff
                var vCrc = CrcCcitt.Crc(btEnable);
                _intoNotesService.SettingCommand(vCrc);
            }
            data = _intoNotesService.QueryState(btStatus);
            //如果第一次设置不成功那么第二次遇到enable状态时要自动重置
            if (data != "ENABLE_IDLING")
            {
                _intoNotesService.AutoReset();
            }
            labelMsg.Content = string.Format(Properties.Resources.WAITINMONEY, _rate);
            _rcTimer.Start();
            _ledLightApi.OpenLedByNum(1);
        }
    }
}
