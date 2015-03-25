using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.Bll;
using CheckIn.common;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_Interface_Hardware.CardManage;
using HotelCheckIn_Interface_Hardware.LedLight;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// CollectionRoomCard.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionRoomCard : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private readonly LedLightApi _ledLightApi = new LedLightApi();

        private int _num;
        private static int _timeout;
        private static string _lockroomcom;

        private CardManage cardManage;

        private Point p1 = new Point(0, 0);
        private Point p2 = new Point(0, 0);
        private Point p3 = new Point(0, 0);
        private Point p4 = new Point(0, 0);
        private int _clickIndex = 1;    //第几次点击
        private int temp = 200;         //正方形边长
        private readonly DispatcherTimer timer3 = new DispatcherTimer();
        private NavigationService _navService;
        private IntPtr handlerCom;

        public CollectionRoomCard()
        {
            _log.Info("打开界面");
            InitializeComponent();

            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _lockroomcom = setting.LockRoomCom;
            _ledLightApi.ComPort = setting.LedCom;
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);

            _num = 0;
            Step.BtInit();
            Step.SetStep(0);

            timer3.Interval = new TimeSpan(0, 0, 5);
            timer3.Tick += new EventHandler(timer3_Tick);
            this.timer3.Start();
            this.MouseLeftButtonUp += new MouseButtonEventHandler(Grid1_MouseLeftButtonDown);//设置捕获鼠标点击事件
            cardManage = new CardManage();
            cardManage.ComPort = _lockroomcom;
            handlerCom = new IntPtr();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                labelMsg.Content = "退房功能暂未开放,请到前台办理。";
                Common.Speak("退房功能暂未开放,请到前台办理！");
                //Common.Speak(Properties.Resources.INSERTROOMCARD);
                _navService = NavigationService.GetNavigationService(this);
                //cardManage.EnterCard();
                cardManage.OpenComPort(ref handlerCom);
                _ledLightApi.OpenLedByNum(2);
            }
            catch (Exception ex)
            {
                MachineError.ErrCode = ErrorCode.SENDCARD_ERROR;
                MachineError.ErrMsg = "收卡界面打开出错:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex);
            }
            finally
            {
                _dTimer.Start();
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
            try
            {
                int i = cardManage.CustomCheckCardPosition(handlerCom);
                if (i == 0X36)//查询卡片位置，并且判断是否在读卡位置
                {
                    //查询卡片是否有效，并且进入下一个界面
                    bool b = CheckCardStatus();
                    if (b)
                    {
                        Next_Click(null, null);
                    }
                }
            }
            catch (BusinessException ex)
            {
                labelMsg.Content = ex.Message;
                MachineError.ErrCode = ErrorCode.SENDCARD_ERROR;
                MachineError.ErrMsg = "收卡出错:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error("查询卡片位置：" + ex);
                //todo:读卡失败执行逻辑
            }
            catch (Exception exception)
            {
                MachineError.ErrMsg = "未知错误:" + exception.Message;
                MachineError.AllLock = true;
                _log.Error(exception);
                //todo:读卡失败执行逻辑
            }

            if (_num > _timeout - 1)
            {
                try
                {
                    string s = cardManage.Reset(handlerCom, 0x31);
                    _log.Debug("重置发卡器：" + s);
                }
                catch (Exception ex)
                {
                    _log.Debug(ex.Message);
                }
                Close();
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
            if (_navService != null)
            {
                var next = new ReturnCash();
                _navService.Navigate(next);
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            Close();
            _ledLightApi.CloseAllLed();
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
        private void Close()
        {
            _dTimer.Stop();
            timer3.Stop();

            try
            {
                cardManage.CloseComPort(handlerCom);
            }
            catch (Exception ex)
            {
                cardManage.CloseComPort(handlerCom);
                _log.Error(ex);
            }
        }

        /// <summary>
        /// 查询卡片是否有效
        /// </summary>
        private bool CheckCardStatus()
        {
            //读卡，查询房间号，然后根据房间号查询是否超出退房时间。
            //string roomNum = "1203";
            //string IdCard = "342623198702103412";
            //InterFace interFace = new InterFace();
            //string ret = interFace.CheckOutExamine(new RoomCardInfo()
            //    {
            //        IdCardNum = IdCard,
            //        RoomNum = roomNum,
            //    });
            //bool b = false;
            //string errorCode = "";
            //string errorMsg = "";
            //AnalysisXml.getResponse(ref b, ref errorCode, ref errorMsg, ret);
            //if (!b)
            //{
            //    MessageBox.Show(errorMsg);
            //    var next = new IndexPage();
            //    _navService.Navigate(next);
            //    return false;
            //}
            return true;
        }

        private void Grid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(null);       //获取当前点击点的坐标
            if (_clickIndex == 1)
                p1 = p;
            else if (_clickIndex == 2)
                p2 = p;
            else if (_clickIndex == 3)
                p3 = p;
            else if (_clickIndex == 4)
                p4 = p;

            if (isClose())
            {
                string s = cardManage.Reset(handlerCom, 0x31);
                _log.Debug("退出程序时，关闭收卡：" + s);
                Close();
                Application.Current.Shutdown();  //关闭程序  
            }
            _clickIndex++;
            e.Handled = false;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            _clickIndex = 1;
            p1 = new Point(0, 0);
            p2 = new Point(0, 0);
            p3 = new Point(0, 0);
            p4 = new Point(0, 0);
        }

        private bool isClose()
        {
            double x1 = p1.X;
            double y1 = p1.Y;

            double x2 = p2.X;
            double y2 = p2.Y;

            double x3 = p3.X;
            double y3 = p3.Y;

            double x4 = p4.X;
            double y4 = p4.Y;

            double width = this.ActualWidth;      //窗口的宽度
            double height = this.ActualHeight;    //窗口的高度

            /*********判断4个点是否落在屏幕四角100*100的矩形内*********/
            if (x1 < temp && y1 < temp && x2 > (width - temp) && y2 < temp && x3 < temp && y3 > (height - temp) && x4 > (width - temp) && y4 > (height - temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
