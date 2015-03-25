using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using CommonLibrary;
using HotelCheckIn_InterfaceSystem.model;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// IdAuthentication.xaml 的交互逻辑
    /// </summary>
    public partial class IdAuthentication : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Guest guest;
        private int _num;
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private static int _timeout;
        private readonly int _checkfaceTimeout;
        private readonly string _videoImagePath;
        private Thread _detectThread;
        private bool _isDetectThreadOn = true;
        private bool _isPassed = false;
        private int num = 0;
        private NavigationService _navService;

        public IdAuthentication(Guest _guest)
        {
            _log.Info("打开界面");
            if (_guest == null) throw new ArgumentNullException("_guest");
            guest = _guest;
            InitializeComponent();

            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _checkfaceTimeout = setting.CheckFaceTimeOut;
            _videoImagePath = setting.VideoImagePath;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            string str = "";
            str = "请将脸部对准摄像头！";

            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();

            labelNum.Content = _timeout;
            if ("1".Equals(guest.salutation_Id))
            {
                textBlockMsg.Text = "请 " + guest.guest_Name + " 先生 将脸部对准摄像头";
            }
            else
            {
                textBlockMsg.Text = "请 " + guest.guest_Name + " 女士 将脸部对准摄像头";
            }
            Common.Speak(str);
            _navService = NavigationService.GetNavigationService(this);

            _num = 0;
            Step.BtInit();
            Step.SetStep(2);
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            _log.Info("退出界面");
        }

        private void GoNextPage()
        {
            var checkedCount = CheckInInfo.CheckedCount;
            checkedCount++;
            _dTimer.Stop();
            if (checkedCount == CheckInInfo.CustomerCount)
            {
                if (_navService != null)
                {
                    var next = new CollectionCash();
                    _navService.Navigate(next);
                }
            }
            else
            {
                CheckInInfo.CheckedCount = checkedCount;
                if (_navService != null)//验证下一个人
                {
                    var readIdCard = new ReadIdCard();
                    _navService.Navigate(readIdCard);
                }
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
            if (_num == 1)
            {
                try
                {
                    _detectThread = new Thread(new ThreadStart(DetectLiveFace));
                    _detectThread.SetApartmentState(ApartmentState.STA);
                    _detectThread.IsBackground = true;
                    _detectThread.Start();

                    var idCardImagePath = Path.GetTempPath() + "idcard.bmp";
                    var pich2 = pictureBox2.Child as PictureBox;

                    FaceID.StartRecognition(idCardImagePath, pich2.Handle);
                    //    int f = FaceID.InitDll();
                    //    if (f == 1)
                    //    {
                    //        FaceID.StartRecognition(idCardImagePath, pich2.Handle);
                    //    }
                    //    else
                    //    {
                    //        MachineError.ErrCode = ErrorCode.FACE_RECOGNITION_ERROR;
                    //        MachineError.ErrMsg = Properties.Resources.FACEDETECTERROR;
                    //        MachineError.AllLock = true;
                    //        //在人脸识别模块出错后，返回首页。
                    //        //Next_Click(null,null);
                    //        //return;
                    //    }
                }
                //catch (Exception ex)
                //{
                //    _log.Error(ex);
                //    labelMsg.Content = Properties.Resources.FACEDETECTLOADERROR;
                //    MachineError.ErrCode = ErrorCode.CAMERA_ERROR;
                //    MachineError.ErrMsg = Properties.Resources.FACEDETECTERROR;
                //    MachineError.AllLock = true;
                //    return;
                //}
                finally
                {
                    //FaceID.ReleaseDll();
                    _detectThread.Abort();
                }
            }
            guest.PhotoFromCameraSave = GetCameraPhotoSave();

            if (CheckInInfo.CheckedCount == 0)
            {
                if (CheckInInfo.CustomImage.Count <=2)
                {
                    CheckInInfo.CustomImage.Add(guest.PhotoFromCameraSave);
                }
                else
                {
                    CheckInInfo.CustomImage[1] = guest.PhotoFromCameraSave;
                }
            }
            else
            {
                if (CheckInInfo.CustomImage.Count <= 3)
                {
                    CheckInInfo.CustomImage.Add(guest.PhotoFromCameraSave);
                }
                else
                {
                    CheckInInfo.CustomImage[3] = guest.PhotoFromCameraSave;
                }
            }

            CheckInInfo.GuestList.Add(guest);
            if (_isPassed)//验证通过
            {
                GoNextPage();
            }
            else//验证未通过
            {
                _navService.Navigate(new SwitchRecognizeMethod());
                return;
            }
            if (_num > _timeout - 1)
            {
                _dTimer.Stop();
                Back_Click(null, null);
            }
        }

        private void DetectLiveFace()
        {
            try
            {
                while (_isDetectThreadOn)
                {
                    num++;
                    if (_checkfaceTimeout - num <= 0)//超时
                    {
                        FaceID.StopRecognition(_videoImagePath);
                        _detectThread.Abort();
                    }
                    int Status = FaceID.GetRecognitionStatus();
                    _log.Info("人脸识别状态值：" + Status);
                    if (Status == 0)
                    {
                        /* 识别中 */
                    }
                    else if (Status == 1)
                    {
                        _log.Debug("人脸识别中：识别成功");
                        /* 识别成功 */
                        _isPassed = true;
                        int i = FaceID.StopRecognition(_videoImagePath);
                        _detectThread.Abort();
                    }
                    else
                    {
                        _log.Debug("人脸识别中：模块故障");
                        /* 模块故障 */
                        _isDetectThreadOn = false;
                        int i = FaceID.StopRecognition(_videoImagePath);
                        _detectThread.Abort();
                    }
                    Thread.Sleep(1000);
                }
                Dispatcher.Run();
            }
            catch (Exception ex)
            {
                MachineError.ErrCode = ErrorCode.FACE_RECOGNITION_ERROR;
                MachineError.ErrMsg = "人脸识别出错:" + ex.Message;
                MachineError.AllLock = true;
                int i = FaceID.StopRecognition(_videoImagePath);
                _detectThread.Abort();
                _log.Info(ex);
            }
        }

        /// <summary>
        /// 取得摄像头图片
        /// </summary>
        /// <returns></returns>
        private byte[] GetCameraPhotoSave()
        {
            if (File.Exists(_videoImagePath))
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(_videoImagePath);
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    return ms.ToArray();
                }
            }
            return null;
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
