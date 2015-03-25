﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.common;
using CommonLibrary;
using log4net;

namespace CheckIn
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public delegate void DeleHeartHeat();
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int _xtTime;
        private static string _macId;
        private static string _scUrl;
        private static string _password;
        private static string _hotelId;
        readonly InterFace _interFace = new InterFace();
        private static bool flag = true;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                SettingHelper setting = new SettingHelper();
                _xtTime = setting.XtTime;
                _macId = setting.MacId;
                _scUrl = setting.ScUrl;
                _password = setting.PassWord;
                _hotelId = setting.SobHotelId;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            Application.Current.DispatcherUnhandledException += CurrentDispatcherUnhandledException;
        }
        /// <summary>
        /// 机器心跳，传递一个
        /// </summary>
        private void HearHeat()
        {
            while (true)
            {
                try
                {
                    //发送心跳方法
                    var scUrl = _interFace.IHeartBeat_Pt(new HearBeatPara()
                    {
                        MachineId = _macId,
                        FalutId = MachineError.ErrCode,
                        Status = MachineError.ErrMsg,
                        Url = _scUrl,
                        PassWord = _password,
                        NowDt = DateTime.Now.ToString("yyyy-mm-dd")
                    });
                    new SettingHelper().ScUrl = scUrl;//素材有改变的时候，刷新index页面
                    _scUrl = scUrl;
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                Thread.Sleep(_xtTime);
            }
        }

        /// <summary>
        /// 硬件初始化。完成后初始化酒店房间信息
        /// </summary>
        private void InitDevice()
        {
            try
            {
                CheckHard checkHard = new CheckHard();
                string error = string.Empty;
                bool Flag = checkHard.CheckHard_All(ref error);
                if (!Flag)
                {
                    MessageBoxResult mbr = MessageBox.Show(error + "\n" + Properties.Resources.HOLDANDCALLADMIN);
                    if (mbr == MessageBoxResult.OK)
                    {
                        Application.Current.Shutdown();
                    }
                    Thread.Sleep(2000);
                    InitDevice();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                GoIndexPageWithMsg();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GoIndexPageWithMsg()
        {
            MessageBox.Show(Properties.Resources.MACHINE_ERROR_RETRY, Properties.Resources.WRAN, MessageBoxButton.OK);
            var indexPage = new IndexPage
            {
                labelMsg = { Content = Properties.Resources.MACHINE_ERROR },
            };
            indexPage.labelMsg.UpdateLayout();
            NavigationService.Navigate(indexPage);
        }

        void CurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _log.Error(e.Exception);
            e.Handled = true;
            var indexPage = new IndexPage();
            NavigationService.Navigate(indexPage);
        }

        protected override void OnClosed(EventArgs e)
        {
            Application.Current.Shutdown();
            base.OnClosed(e);
            Environment.Exit(0);
        }

        private void NavigationWindow_Loaded(object sender, NavigationEventArgs e)
        {
            if (flag)
            {
                flag = false;
                ThreadStart ts = HearHeat;
                var newThread = new Thread(ts);
                newThread.Start();

                //try
                //{
                //    int nRetFaceID = FaceID.InitDll();
                //    if (nRetFaceID != 1)
                //    {
                //        _log.Error(Properties.Resources.FACEDETECTLOADERROR + nRetFaceID);
                //        MachineError.ErrCode = ErrorCode.FACE_RECOGNITION_ERROR;
                //        MachineError.ErrMsg = Properties.Resources.FACEDETECTERROR;
                //        MachineError.AllLock = true;
                //        return;
                //    }
                //}
                //finally
                //{
                //    //FaceID.ReleaseDll();
                //}

                //==========================
                //硬件检测功能。调试期间关闭
                //==========================
                //InitDevice();
            }
        }

    }
}
