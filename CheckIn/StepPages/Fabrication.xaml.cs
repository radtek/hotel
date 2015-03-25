﻿using System;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Threading;
using CheckIn.AddDll;
using CheckIn.BackService;
using CheckIn.Bll;
using CheckIn.common;
using CheckIn.Model;
using CommonLibrary;
using CommonLibrary.exception;
using HotelCheckIn_Interface_Hardware.AdelRead;
using HotelCheckIn_Interface_Hardware.CardManage;
using HotelCheckIn_Interface_Hardware.LedLight;
using log4net;

namespace CheckIn.StepPages
{
    /// <summary>
    /// Fabrication.xaml 的交互逻辑
    /// </summary>
    public partial class Fabrication : Page
    {
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly DispatcherTimer _dTimer = new DispatcherTimer();
        private readonly LedLightApi _ledLightApi = new LedLightApi();

        private int _num;
        private static int _timeout;
        private string _macId;
        private string _sobHotelId;
        private string _outCardPort;
        private NavigationService _navService;

        /// <summary>
        /// 后台上传结果，三个变量
        /// </summary>
        bool b = false;
        string errorCode = "";
        string errorMsg = "";

        public Fabrication()
        {
            _log.Info("打开界面");
            InitializeComponent();
            if (!string.IsNullOrEmpty(CheckInInfo.PreauthNumber))
            {
                CheckInInfo.PayType.Add(CheckInInfo.PreauthType); //E01.支付类型 （0-未支付<或不限>，1-预授权<信用>，2-预付，3-结算）
                CheckInInfo.PayWay.Add("6");
                //E02.支付方式 （0-不限，1-现金，2-支票，3-汇款，4-代金券，5-会员卡，6-银行卡内卡，7-银行卡外卡，8-积分，9-款待，10-签单（团购），11-退现金，12-支付宝，13-第三方储值卡
                CheckInInfo.PaymentAmount.Add(CheckInInfo.RoomRate);
                CheckInInfo.HepPayGuid.Add(DateTime.Now.Ticks.ToString());
                CheckInInfo.Batch.Add(CheckInInfo.PreauthNumber);
                CheckInInfo.JopbNumberPmsCode.Add("1");
                CheckInInfo.OperTime.Add(CheckInInfo.Dt);
                CheckInInfo.TransactionType.Add("3"); //E74.交易类型 （0-不限，1-刷卡消费，2-消费撤销<退款>，3-预授权，4-预授权撤销，5-预授权完成，6-预授权完成撤销）
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SettingHelper setting = new SettingHelper();
            _timeout = setting.TimeOut;
            _macId = setting.MacId;
            _sobHotelId = setting.SobHotelId;
            _outCardPort = setting.LockRoomCom;
            _ledLightApi.ComPort = setting.LedCom;

            _dTimer.Tick += DTimerTick;
            _dTimer.Interval = new TimeSpan(0, 0, 1);
            _dTimer.Start();
            _num = 0;
            Step.BtInit();
            Step.SetStep(4);
            Common.Speak(Properties.Resources.ROOMCARDFABRICATION);
            _navService = NavigationService.GetNavigationService(this);
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
            _log.Debug(_timeout - _num);
            if (_num == 1)
            {
                UploadInfo();
                if (b)
                {
                    OutCard();
                }
            }
            else if (_num == 2)
            {
                if (b)
                {
                    PrintM();
                    labelMsg.Content = "打印完成，将在10秒后自动返回主界面";
                    _timeout = _num + 10;
                }
                else
                {
                    labelMsg.Content = "入住信息上传出错，将在10秒后自动返回主界面";
                    _timeout = _num + 10;
                }
            }
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

        /// <summary>
        /// 制卡发卡
        /// </summary>
        private void UploadInfo()
        {
            //先将数据传递到PMS，再出卡、然后打印
            try
            {
                labelMsg.Content = Properties.Resources.CHECKININFOUPLOAD;
                InterFace interFace = new InterFace();
                CheckInInfo.IndividualOrGroup = "1";//设置“散团”为“散客”
                CheckInInfo.CardCount = 1;
                CheckInInfo.TimeType = "0";
                CheckInInfo.OrderType = "1";
                CheckInModel checkInModel = new CheckInModel()
                {
                    CheckId = CheckInInfo.ValidateCode,
                    OrderId = CheckInInfo.OrderPmsCode,
                    CheckinCode = CheckInInfo.CheckinCode,
                    HotelId = _sobHotelId,
                    RoomNum = CheckInInfo.RoomNum,
                    RoomCode = CheckInInfo.RoomCode,
                    RoomType = CheckInInfo.RoomType,
                    RoomRate = CheckInInfo.RoomRate,
                    OriginalPrice = CheckInInfo.OriginalPrice,
                    KnockDownPrice = CheckInInfo.KnockDownPrice,
                    Building = CheckInInfo.Building,
                    OrderType = CheckInInfo.OrderType,
                    PeopleNum = CheckInInfo.CustomerCount,
                    CheckInTime = CheckInInfo.CheckInDateTime,
                    CheckOutTime = CheckInInfo.CheckOutDateTime,
                    OrderTime = DateTime.Now,
                    MacId = _macId,
                    CardCount = CheckInInfo.CardCount,
                    TimeType = CheckInInfo.TimeType,
                    IndividualOrGroup = CheckInInfo.IndividualOrGroup,
                    PhoneNumber = CheckInInfo.PhoneNumber,

                    //---------支付方式-------------
                    PayType = CheckInInfo.PayType.ToArray(),
                    PayWay = CheckInInfo.PayWay.ToArray(),
                    PaymentAmount = CheckInInfo.PaymentAmount.ToArray(),
                    HepPayGuid = CheckInInfo.HepPayGuid.ToArray(),
                    Batch = CheckInInfo.Batch.ToArray(),
                    JobNumberPmsCode = CheckInInfo.JopbNumberPmsCode.ToArray(),
                    OperTime = CheckInInfo.OperTime.ToArray(),
                    TransactionType = CheckInInfo.TransactionType.ToArray(),
                    CardNum = CheckInInfo.CardNum.ToArray(),
                    Validity = CheckInInfo.Validity.ToArray(),
                    //------------------------------
                    Images = new byte[4][] 
                    {
                        CheckInInfo.CustomImage[0], 
                        CheckInInfo.CustomImage[0], 
                        CheckInInfo.CustomImage[0], 
                        CheckInInfo.CustomImage[0]
                    },
                };

                //图片数组顺序：人1身份证照片，人2身份证照片，人1摄像头照片，人2摄像头照片。
                if (CheckInInfo.CustomImage.Count > 2)
                {
                    checkInModel.Images[0] = CheckInInfo.CustomImage[0];
                    checkInModel.Images[1] = CheckInInfo.CustomImage[2];
                    checkInModel.Images[2] = CheckInInfo.CustomImage[1];
                    checkInModel.Images[3] = CheckInInfo.CustomImage[3];

                    checkInModel.IdentityCardNum = new string[]
                        {
                            CheckInInfo.GuestList[0].id_Card_No, 
                            CheckInInfo.GuestList[1].id_Card_No
                        };
                    checkInModel.Name = new string[]
                        {
                            CheckInInfo.GuestList[0].guest_Name, 
                            CheckInInfo.GuestList[1].guest_Name
                        };
                    checkInModel.Sex = new string[]
                        {
                            CheckInInfo.GuestList[0].gender_Id, 
                            CheckInInfo.GuestList[1].gender_Id
                        };
                    checkInModel.IdentificationType = new string[]
                        {
                            CheckInInfo.GuestList[0].id_Card_Type_Id,
                            CheckInInfo.GuestList[1].id_Card_Type_Id,
                        };
                    checkInModel.Nation = new string[]
                        {
                            CheckInInfo.GuestList[0].Nation,
                            CheckInInfo.GuestList[1].Nation,
                        };
                    checkInModel.Adrress = new string[]
                        {
                            CheckInInfo.GuestList[0].Address,
                            CheckInInfo.GuestList[1].Address,
                        };
                    checkInModel.NamePy = new string[]
                        {
                            CheckInInfo.GuestList[0].NamePy,
                            CheckInInfo.GuestList[1].NamePy,
                        };
                    checkInModel.Birthday = new DateTime[]{
                       DateTime.Parse(CheckInInfo.GuestList[0].Birthday),
                       DateTime.Parse(CheckInInfo.GuestList[1].Birthday),
                    };
                }
                else
                {
                    _log.Debug(CheckInInfo.CustomImage.Count);
                    checkInModel.Images = CheckInInfo.CustomImage.ToArray();
                    checkInModel.IdentityCardNum = new string[]
                        {
                            CheckInInfo.GuestList[0].id_Card_No
                        };
                    checkInModel.Name = new string[]
                        {
                            CheckInInfo.GuestList[0].guest_Name
                        };
                    checkInModel.Sex = new string[]
                        {
                            CheckInInfo.GuestList[0].gender_Id
                        };
                    checkInModel.IdentificationType = new string[]
                        {
                            CheckInInfo.GuestList[0].id_Card_Type_Id,
                        };
                    checkInModel.Nation = new string[]
                        {
                            CheckInInfo.GuestList[0].Nation,
                        };
                    checkInModel.Adrress = new string[]
                        {
                            CheckInInfo.GuestList[0].Address,
                        };
                    checkInModel.NamePy = new string[]
                        {
                            CheckInInfo.GuestList[0].NamePy,
                        };
                    checkInModel.Birthday = new DateTime[]{
                       DateTime.Parse(CheckInInfo.GuestList[0].Birthday),
                    };
                }
                string ret = interFace.Checkin(checkInModel);

                AnalysisXml.getResponse(ref b, ref errorCode, ref errorMsg, ret);
                if (!b)
                {
                    labelMsg.Content = Properties.Resources.CHECKININFOUPLOADFAILURE;
                    //取消银行卡的预授权
                    BankCardPaymentCall bankCardPaymentCall = new BankCardPaymentCall();
                    bankCardPaymentCall.log += _log.Debug;
                    string bug = "";
                    bankCardPaymentCall.UmsPreAuthCancell(((int)CheckInInfo.RoomRate).ToString(),
                                                          CheckInInfo.Dt.ToString("yyyy-MM-dd"),
                                                          CheckInInfo.PreauthNumber, ref bug);
                    MessageBox.Show("预授权已经取消。");
                    //todo:前面的流程正常完成，信息上传失败，直接返回？特殊处理？
                    _log.Error("上传入住信息失败：errorCode:" + errorCode + ",errorMsg:" + errorMsg);
                    MachineError.ErrMsg = "上传入住信息失败：errorCode:" + errorCode + ",errorMsg:" + errorMsg;
                    MachineError.AllLock = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                labelMsg.Content = Properties.Resources.CHECKININFOUPLOADERROR;
                _log.Error("入住信息上传出错:" + ex);
                MachineError.ErrMsg = "入住信息上传出错:" + ex.Message;
                MachineError.AllLock = true;
                //todo:上传入住记录出错。处理逻辑
                return;
            }
        }

        /// <summary>
        /// 出卡
        /// </summary>
        private void OutCard()
        {
            CardManage cardManage = new CardManage();
            cardManage.ComPort = _outCardPort;
            labelMsg.Content = Properties.Resources.FABRICATIONCARD;
            try
            {
                _ledLightApi.OpenLedByNum(2);
            }
            catch (Exception e)
            {
                _log.Error("开灯出错：" + e);
            }
            _log.Debug("开始出卡。");
            try
            {
                cardManage.CardBoxPositionToRead(); //卡盒到读写位置
                _log.Debug("出卡到读写位置");
                //卡读写
                long CardNo = 0;
                //门锁操作
                int MakeCardFlag = AdelReadClass.NewKey(new StringBuilder("01" + CheckInInfo.RoomNum), new StringBuilder("00"),
                                                        new StringBuilder(
                                                            CheckInInfo.CheckInDateTime.ToString("yyyyMMddHHmm") +
                                                            CheckInInfo.CheckOutDateTime.ToString("yyyyMMddHHmm")),
                                                        new StringBuilder(""), new StringBuilder(""), 1, 0, ref CardNo,
                                                        new StringBuilder(""), new StringBuilder(""));
                if (MakeCardFlag != 0)
                {
                    labelMsg.Content = "制卡失败，请取卡，并联系前台服务员。";
                    MessageBox.Show("制卡失败，请取卡，并联系前台服务员。", "出错");
                    _log.Error("制卡失败，制卡返回码：" + MakeCardFlag + "房间号：" + CheckInInfo.RoomNum);
                }
                labelMsg.Content = Properties.Resources.OUTCARD;
                cardManage.ReadPositionToEntrance(); //读写位置到出卡口
                InterFace interFace = new InterFace();
                _log.Debug("出卡到出口位置");
                bool b = interFace.EditIoIfCard(new IoJournal()
                    {
                        IoTime = CheckInInfo.Dt,
                        IoId = _macId,
                    });
                _log.Debug("上传发卡状态：" + b + "--时间：" + CheckInInfo.Dt.ToString("yyyy-MM-dd HH:mm:ss") + "--机器ID：" + _macId);
                Common.Speak(Properties.Resources.GETROOMCARD);
            }
            catch (BusinessException ex)
            {
                MachineError.ErrMsg = "发卡出错:" + ex.Message;
                MachineError.AllLock = true;
                labelMsg.Content = ex.Message;
                _log.Error(ex);
                MessageBoxResult mbr = MessageBox.Show(ex.Message + "\n点击“确定”，将重试；点击“取消”，将返回主界面。",
                                                       Properties.Resources.ERROR,
                                                       MessageBoxButton.OKCancel);
                if (MessageBoxResult.OK == mbr)
                {
                    OutCard();
                }
                else
                {
                    _dTimer.Stop();
                    if (_navService != null)
                    {
                        var next = new IndexPage();
                        _navService.Navigate(next);
                    }
                }
            }
            catch (Exception exception)
            {
                MachineError.ErrCode = ErrorCode.SENDCARD_ERROR;
                MachineError.ErrMsg = "发卡出错:" + exception.Message;
                MachineError.AllLock = true;
                _log.Error(exception);
                //todo:出卡失败，但是钱收了，逻辑处理
            }
        }

        /// <summary>
        /// 打印方法
        /// </summary>
        private void PrintM()
        {
            try
            {
                //_ledLightApi.OpenLedByNum(1);
                labelMsg.Content = Properties.Resources.PRINTING;
                //初始化PrintDialog
                var printDialog = new PrintDialog();
                //从本地计算机中获取所有打印机对象(PrintQueue)
                //var printers = new LocalPrintServer().GetPrintQueues();
                //选择一个打印机
                //var selectedPrinter = printers.FirstOrDefault(p => p.Name == "Microsoft XPS Document Writer");
                var selectedPrinter = new LocalPrintServer().DefaultPrintQueue;
                if (selectedPrinter == null)
                {
                    labelMsg.Content = "没有找到默认打印机";
                    return;
                }
                //设置打印机
                printDialog.PrintQueue = selectedPrinter;
                //创建要打印的内容
                string text = @"自助入住登记凭条
-------------
楼栋：{0}
楼层：{1}
房型：{2}
房间号：{3}
姓名：{4}
人数：{5}
入住时间：{6}
退房时间：{7}
支付金额：{8}
备注：{9}
不含基金
=============
签名：


________
";
                float rate = 0f;
                CheckInInfo.PaymentAmount.ForEach(m => rate += m);
                string names = "";
                CheckInInfo.GuestList.ForEach(guest => names += guest.guest_Name);
                text = string.Format(text, CheckInInfo.Building, CheckInInfo.Floor, CheckInInfo.RoomTypeName,
                                     CheckInInfo.RoomNum, names, CheckInInfo.CustomerCount,
                                     CheckInInfo.CheckInDateTime.ToString("yyyy-MM-dd HH:mm"),
                                     CheckInInfo.CheckOutDateTime.ToString("yyyy-MM-dd HH:mm"), rate.ToString(),
                                     CheckInInfo.Note);
                Run run = new Run(text);
                run.FontSize = 16;
                run.FontWeight = FontWeights.Bold;
                var tbl = new TextBlock(run);
                //new TextRange(tbl.ContentStart, tbl.ContentStart.GetPositionAtOffset(3));
                var size = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
                tbl.Measure(size);
                tbl.Arrange(new Rect(new Point(20, 0), size));
                //打印
                printDialog.PrintVisual(tbl, "入住登记单");
                labelMsg.Content = Properties.Resources.PRINTOK;
                Common.Speak(Properties.Resources.GETPAPPER);
            }
            catch (Exception ex)
            {
                MachineError.ErrCode = ErrorCode.PRINTER_ERROR;
                MachineError.ErrMsg = "发卡出错:" + ex.Message;
                MachineError.AllLock = true;
                _log.Error(ex);
                labelMsg.Content = Properties.Resources.PRINTERROR;
            }
            if (b)//在入住单打一完成后，清空数据。
            {
                CheckInInfo.Clear();//清空入住信息类的数据，防止进入Index界面是解锁已经入住的房间。
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            _dTimer.Stop();
            _ledLightApi.CloseAllLed();
            _log.Info("退出界面");
        }
    }
}