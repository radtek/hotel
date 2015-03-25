using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.Model;
using CheckIn.common;
using CommonLibrary;
using HotelCheckIn_InterfaceSystem.model;
using WPFMediaKit.DirectShow.Controls;

namespace CheckIn.StepPages
{
    /// <summary>
    /// Detect.xaml 的交互逻辑
    /// </summary>
    public partial class Detect : Page
    {
        public int PeopleCount { get; set; }
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int _peopleCount;
        private Guest guest;
        private int _num;
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private static int Timeout = 100;

        private NavigationService navService;
        private List<byte[]> Camera = new List<byte[]>();
        private bool detectFlag = false;
        private string detectId = "";
        private string msg = "请稍候";

        /// <summary>
        /// 查询标记，默认查询
        /// </summary>
        private bool queryFlag = true;

        private int macId = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="peopleCount">需要验证的人数</param>
        /// <param name="_guest">正在验证的身份证用户</param>
        public Detect(int peopleCount, Guest _guest)
        {
            //var peopleCount = 1;
            //var _guest = new Guest();

            if (_guest == null) throw new ArgumentNullException("_guest");
            PeopleCount = peopleCount;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Grid_Loaded);
            this.Unloaded += new RoutedEventHandler(Grid_Unloaded);
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();

            SettingHelper settingHelper = new SettingHelper();
            Timeout = settingHelper.TimeOut;
            macId = int.Parse(settingHelper.MacId);

            _peopleCount = peopleCount;
            guest = _guest;

            labelNum.Content = Timeout;
            if ("男".Equals(_guest.gender_Id))
            {
                textBlockMsg.Text = "请 " + _guest.guest_Name + " 先生 将脸部对准摄像头";
            }
            else
            {
                textBlockMsg.Text = "请 " + _guest.guest_Name + " 女士 将脸部对准摄像头";
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            _dTimer.Start();
            _num = 0;
            this.step.BtInit();
            this.step.SetStep(2);
            OkButton.Visibility = Visibility.Hidden;
            if (MultimediaUtil.VideoInputNames.Length > 0)
            {
                vce.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
            }
            else
            {
                MessageBox.Show("未检测到任何可用摄像头!");
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            vce.VideoCaptureSource = null;
        }

        /// <summary>
        /// 操作倒计时 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DTimerTick(object sender, EventArgs e)
        {
            _num++;
            labelNum.Content = Timeout - _num;

            if (_num >= 4 && _num <= 7)
            {
                try
                {
                    labelMsg.Content = "正在识别，请稍后！";
                    Camera.Add(GetImg()); //打开摄像头，并保存照片。
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                    labelMsg.Content = "拍照失败！";
                    MachineError.ErrCode = ErrorCode.CAMERA_ERROR;
                    MachineError.ErrMsg = "摄像头故障";
                    return;
                }
                if (_num == 7)
                {
                    new Thread(UploadImg).Start();
                }
            }
            labelMsg.Content = msg;
            if (_num > 7 && detectFlag)
            {
                GoNext();
                return;
            }

            if (!queryFlag)
            {
                OkButton.Visibility = Visibility.Visible;
            }
            if (_num > Timeout - 1)
            {
                _dTimer.Stop();
                try
                {
                    OkButton.Visibility = Visibility.Visible;
                    Application.Current.Properties.Clear();
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        private void UploadImg()
        {
            InterFace device = new InterFace();
            detectId = DateTime.Now.Ticks.ToString();
            try
            {
                bool b = device.SaveDetect(new BackService.Detect()
                {
                    Id = detectId,
                    Jqid = macId.ToString(),
                    IdCard = guest.id_Card_No,
                    Name = guest.guest_Name,
                    Sex = guest.gender_Id,
                    IdCardImg = guest.PhotoFromIdCardSave,
                    Camera1 = Camera[0],
                    Camera2 = Camera[1],
                    Camera3 = Camera[2],
                    UpdateDt = DateTime.Now,
                    CreateDt = DateTime.Now,
                });
                new Thread(GetDetectResult).Start();//上传完成后，查询验证结果。
                if (!b)
                {
                    msg = "验证出错，请重试、或前往前台办理入住手续！";
                }
            }
            catch (Exception ex)
            {
                msg = "验证出错，请重试、或前往前台办理入住手续！";
            }
        }

        /// <summary>
        /// 查询验证状态
        /// </summary>
        private void GetDetectResult()
        {
            while (!detectFlag && queryFlag && _num < Timeout - 1)
            {
                Thread.Sleep(1000);
                InterFace device = new InterFace();
                int i = device.QueryDetect(detectId);//0：没有数据，1：未验证，2：验证通过，3：身份证与本人不符,4:人不在视频范围内
                _log.Debug("detectId:" + detectId + ",验证结果：" + i);
                //验证标识
                detectFlag = true;
                //查询标识状态
                queryFlag = false;
                msg = "验证通过！";
                
                //if (i == 0)
                //{
                //    msg = "验证出错，请重试、或前往前台办理入住手续！";
                //    queryFlag = false;
                //}
                //else if (i == 1)
                //{
                //    //等待验证
                //    queryFlag = true;
                //    msg = "正在识别，请稍后！";
                //}
                //else if (i == 2)
                //{
                //    detectFlag = true;
                //    queryFlag = false;
                //    msg = "验证通过！";
                //}
                //else if (i == 3)
                //{
                //    msg = "身份证与本人不符，请重试、或前往前台办理入住手续！";
                //    queryFlag = false;
                //}
                //else if (i == 4)
                //{
                //    msg = "人不在视频范围内，请重试、或前往前台办理入住手续！";
                //    queryFlag = false;
                //}
                //else
                //{

                //}
            }
        }

        /// <summary>
        /// 验证通过后，跳转。
        /// </summary>
        private void GoNext()
        {
            guest.PhotoFromCameraSave = Camera[1];//拍第二张照片作为保存的照片
            CheckInInfo.CustomImage.Add(Camera[1]);
            var checkedCount = CheckInInfo.CheckedCount;
            CheckInInfo.GuestList.Add(guest);

            checkedCount++;
            if (checkedCount == _peopleCount)
            {
                if (navService != null)
                {
                    var next = new SwitchPayMethod();
                    navService.Navigate(next);
                }
            }
            else
            {
                CheckInInfo.CheckedCount++;
                if (navService != null)
                {
                    var readIdCard = new ReadIdCard();
                    navService.Navigate(readIdCard);
                }
            }
            _dTimer.Stop();
        }

        /// <summary>
        /// 返回首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastButtonClick(object sender, RoutedEventArgs e)
        {
            if (navService != null)
            {
                _dTimer.Stop();
                navService.Navigate(new Uri("IndexPage.xaml", UriKind.Relative));
            }
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if (navService != null)
            {
                _dTimer.Start();
                _num = 0;
                Camera = new List<byte[]>();
                detectFlag = false;
                detectId = "";
                msg = "";
                queryFlag = true;
                OkButton.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// 获取摄像头照片
        /// </summary>
        /// <returns></returns>
        public byte[] GetImg()
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)vce.ActualWidth, (int)vce.ActualHeight, 96, 96, PixelFormats.Default);

            vce.Measure(vce.RenderSize);
            vce.Arrange(new Rect(vce.RenderSize));

            bmp.Render(vce);

            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
