using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// RoomSelect.xaml 的交互逻辑
    /// </summary>
    public partial class RoomSelect : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        InterFace interFace = new InterFace();

        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private int _num;
        private static int _timeout;
        private string _macId;
        private NavigationService _navService;
        List<Room> infoList = new List<Room>();

        public RoomSelect()
        {
            _log.Info("打开界面");
            InitializeComponent();

            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _macId = setting.MacId;
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
            //绑定房间类型

            RoomQuality[] roomQualities = interFace.QueryRoomQuality("1");//1是房型
            List<RoomQuality> qualities = new List<RoomQuality>();
            roomQualities.ToList().ForEach(quality =>
                {
                    if (quality.Name != "残疾人房")
                    {
                        qualities.Add(quality);
                    }
                });

            RoomType.ItemsSource = qualities;
            RoomType.SelectedValuePath = "Id";
            RoomType.DisplayMemberPath = "Name";

            //绑定房间类型
            if (!string.IsNullOrEmpty(CheckInInfo.RoomType))
            {
                RoomType.SelectedValue = CheckInInfo.RoomType;
                RoomType.SelectionChanged += RoomType_OnSelectionChanged;
                Binding(PageSize, 1);
                RoomNum.Text = CheckInInfo.RoomNum;
                dataGrid1.SelectedIndex = 0;
                GetRate();
            }
            else
            {
                RoomType.SelectedIndex = 0;//设置默认选择项
                RoomNum.Text = "";
            }
            //团购或者预订都不能修改房型
            if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode) || !string.IsNullOrEmpty(CheckInInfo.BookMark))
            {
                RoomType.IsEnabled = false;
            }
            else//散客可以修改房型
            {
                RoomType.IsEnabled = true;
            }
            Common.Speak("请选择入住房间，选择完成请点击下一步！");
        }

        /// <summary>
        /// 获取房价
        /// </summary>
        private void GetRate()
        {
            string rateStr = "";
            //if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode))
            //{
            //    rateStr = interFace.GetRoomRate(new QueryRoomRateInfo()//获取团购价格和价格码
            //    {
            //        PriceSouce = "3",
            //        ValidationCode = CheckInInfo.ValidateCode,
            //    });
            //}
            //else
            //{
            QueryRoomRateInfo rateInfo = new QueryRoomRateInfo()//获取配置的价格和价格码，不需要任何参数
                 {
                     PriceSouce = "1",//如果改为1，则需要添加参数：入住时间，到期时间，房型ID
                     CheckinTime = CheckInInfo.CheckInDateTime,
                     CheckoutTime = CheckInInfo.CheckOutDateTime,
                     RoomTypeCode = CheckInInfo.RoomType,
                 };
            rateInfo.PmsRoomRateCode = CheckInInfo.RoomCode;
            switch (rateInfo.PmsRoomRateCode.ToUpper())
            {
                case "VIP":
                    rateInfo.PriceType = E65.MembershipPrice;
                    break;
                case "NET":
                    rateInfo.PriceType = E65.NetPrice;
                    break;
                case "QTJ":
                    rateInfo.PriceType = E65.IndividualPrice;
                    break;
                default:
                    rateInfo.PriceType = E65.IndividualPrice;
                    break;
            }
            rateStr = interFace.GetRoomRate(rateInfo);

            //}
            if (string.IsNullOrEmpty(rateStr))
            {
                labelMsg.Content = "获取房价出错。";
                Next.Visibility = Visibility.Hidden;
            }
            else//将房价、房价码保存到静态类
            {
                labelMsg.Content = "";
                string[] rateArr = rateStr.Split('|');
                CheckInInfo.RoomRate = float.Parse(rateArr[0]);
                CheckInInfo.OriginalPrice = float.Parse(rateArr[0]);
                CheckInInfo.KnockDownPrice = float.Parse(rateArr[0]);
                CheckInInfo.RoomCode = rateArr[1];

                TbRoomRate.Text = CheckInInfo.RoomRate + "";
                Next.Visibility = Visibility.Visible;
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
            //验证房间锁定信息
            var roomNum = RoomNum.Text;
            if (string.IsNullOrEmpty(roomNum))
            {
                labelMsg.Content = "请选择房间。";
                return;
            }
            bool b = new InterFace().RoomLockSet(roomNum, "1", CheckInInfo.PhoneNumber, CheckInInfo.ValidateCode, _macId);
            if (!b)
            {
                labelMsg.Content = "锁定房间：" + roomNum + "失败，请选择其他房间。";
                return;
            }
            var room = dataGrid1.SelectedItem as Room;
            CheckInInfo.RoomNum = room.room_No;
            CheckInInfo.RoomType = room.room_Type_Id;
            CheckInInfo.RoomTypeName = room.room_Type_Name;
            CheckInInfo.Towards = room.room_Direction_Id;
            CheckInInfo.Floor = room.room_Floor_Id;
            CheckInInfo.Building = room.room_Building_Id;

            _dTimer.Stop();
            if (_navService != null)
            {
                var next = new DisplayValidation();
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
            infoList = new List<Room>();
            _dTimer.Stop();
            _log.Info("退出界面");
        }

        private void Pre_Click(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            if (_navService != null)
            {
                if (!string.IsNullOrEmpty(CheckInInfo.ValidateCode))
                {
                    var next = new InputValidation();
                    _navService.Navigate(next);
                }
                else
                {
                    var next = new InputPhone();
                    _navService.Navigate(next);
                }
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

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="currentSize">number表示每个页面显示的记录数</param>
        /// <param name="number">currentSize表示当前显示页数  </param>
        private void Binding(int currentSize, int number)
        {
            if (infoList.Count <= 0)
            {
                Room[] rooms = new InterFace().QueryEmptyRoomList((RoomType.SelectedValue ?? "").ToString(),
                                                                  CheckInInfo.CheckInDateTime, CheckInInfo.CheckOutDateTime);
                if (rooms == null)
                {
                    labelMsg.Content = "获取数据出错。";
                    _log.Error("PMS连接出错。");
                    return;
                }
                infoList = rooms.ToList();      //获取数据源  
            }

            int count = infoList.Count;             //获取记录总数  
            int pageSize = 0;                       //pageSize表示总页数  
            if (count % currentSize == 0)
            {
                pageSize = count / currentSize;
            }
            else
            {
                pageSize = count / currentSize + 1;
            }
            tbkTotal.Text = pageSize.ToString();

            tbkCurrentsize.Text = number.ToString();
            dataGrid1.ItemsSource = infoList.Take(currentSize * number).Skip(currentSize * (number - 1)).ToList();   //刷选第currentSize页要显示的记录集，重新绑定dataGrid1
        }

        const int PageSize = 10;  //表示每页显示10条记录  

        /// <summary>
        /// 上一页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            int currentsize = int.Parse(tbkCurrentsize.Text); //获取当前页数  
            if (currentsize > 1)
            {
                Binding(PageSize, currentsize - 1);   //调用分页方法  
            }
        }

        //下一页事件  
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int total = int.Parse(tbkTotal.Text); //总页数  
            int currentsize = int.Parse(tbkCurrentsize.Text); //当前页数  
            if (currentsize < total)
            {
                Binding(PageSize, currentsize + 1);   //调用分页方法  
            }
        }

        private void RoomType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                infoList = new List<Room>();
                Binding(PageSize, 1);
                CheckInInfo.RoomType = RoomType.SelectedValue.ToString();
                GetRate();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        private void DataGrid1_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                Room room = dataGrid1.SelectedItem as Room;
                RoomNum.Text = room.room_No;
            }
            else
            {
                RoomNum.Text = "";
            }
        }
    }
}
