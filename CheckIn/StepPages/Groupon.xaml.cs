using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// Groupon.xaml 的交互逻辑
    /// </summary>
    public partial class Groupon : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private int _num;
        private static int _timeout;
        private static string TgStr;
        private NavigationService _navService;

        public Groupon()
        {
            _log.Info("打开界面");
            InitializeComponent();
            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            TgStr = setting.Tg;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;
            Step.BtInit();
            Step.SetStep(0);

            _navService = NavigationService.GetNavigationService(this);
            Common.Speak(Properties.Resources.WELCOME);
            Button btn;
            if (TgStr.Length > 0)
            {
                string[] Tgs = TgStr.Split(';');
                foreach (string tg in Tgs)
                {
                    string[] tuan = tg.Split('#');
                    if (tuan.Length < 2)
                    {
                        _log.Error("配置文件中团购商配置出错。");
                        MachineError.ErrMsg = "配置文件中团购商配置出错。";
                        MachineError.AllLock = true;
                        return;
                    }
                    btn = new Button();
                    btn.Height = 90;
                    btn.Width = 90;
                    btn.ToolTip = tuan[0];
                    btn.Margin = new Thickness(10, 10, 10, 10);
                    ImageBrush imgBrush = new ImageBrush();
                    imgBrush.ImageSource = new BitmapImage(new Uri(tuan[1], UriKind.Relative));
                    imgBrush.Stretch = Stretch.Fill;
                    Image img = new Image();
                    img.Source = imgBrush.ImageSource;
                    btn.Content = img;
                    btn.Click += new RoutedEventHandler(Next_Click);
                    Wp1.Children.Add(btn);
                }
            }
            else
            {
                MachineError.ErrMsg = "配置文件中团购商未配置。";
                MachineError.AllLock = true;
                _log.Error("配置文件中团购商未配置。");
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            if (_navService != null)
            {
                var next = new InputValidation();
                _navService.Navigate(next);
            }
            e.Handled = false;
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
                    {
                        var next = new IndexPage();
                        _navService.Navigate(next);
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
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
    }
}
