/****************************************************************************************
 * 注意：                                                                               *
 * 1：如果只有reset，那么设置enable/disable后，状态全为disable，所以要恢复出厂设置      *
 *                                                                                      *
 *                                                                                      *
 * ************************ 2014年6月26日 张威 ******************************************/

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace HotelCheckIn_Interface_Hardware.Into_Notes
{
    public class IntoNotesService
    {
        /// <summary> 
        /// 定义委托 
        /// </summary> 
        public delegate void DelegateLog(string sReceived);

        /// <summary> 
        /// 定义一个消息接收事件 
        /// </summary> 
        public event DelegateLog log;

        private SerialPort _sp = null;
        /// <summary>
        /// com端口
        /// </summary>
        public string ComPort { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="comport">com端口</param>
        public IntoNotesService()
        {
            ComPort = "com4";
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        public bool OpenComPort()
        {

            log("打开串口");
            _sp = new SerialPort(ComPort)
                {
                    BaudRate = 9600,//波特率
                    DataBits = 8,//数据位
                    StopBits = StopBits.One,//两个停止位
                    Parity = Parity.Even, //奇偶校验位
                    ReadTimeout = 100,
                    WriteTimeout = -1
                };

            try
            {
                _sp.Open();
                return true;
            }
            catch (Exception e)
            {
                log("打开串口失败:" + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        public bool CloseComPort()
        {
            log("关闭串口");
            try
            {
                _sp.Close();
                return true;
            }
            catch (Exception e)
            {
                log("关闭串口失败:" + e.Message);
                return false;
            }
        }

        /// <summary>
        /// 查询状态
        /// </summary>
        /// <returns>状态</returns>
        public string QueryState(byte[] send)
        {
            var count = 0;
            var total = 0;
            var logtxt = "";
            _sp.Write(send, 0, send.Length);
            while (true)
            {
                if (count >= 4)
                {
                    _sp.Write(send, 0, send.Length);
                    count = 0;
                    total++;
                }
                if (total > 3)
                {
                    return "超时";
                }
                Thread.Sleep(200);
                var recive = new byte[_sp.BytesToRead];
                if (recive.Length == 0)
                {
                    log("重置");
                    AutoReset();
                }
                try
                {
                    _sp.Read(recive, 0, _sp.BytesToRead);
                    if (recive[0] != 0x00 && recive[1] != 0x00 && recive[2] != 0x00)
                    {
                        switch (recive[2])
                        {
                            case (byte)Status.ESCROW:
                                logtxt = Enum.GetName(typeof(Escrow), recive[3]);
                                log("返回QueryState1状态:" + logtxt );
                                return logtxt;
                            case (byte)Status.REJECTING:
                                logtxt = Enum.GetName(typeof(Rejecting), recive[3]);
                                log("返回QueryState1状态:" + logtxt);
                                return logtxt;
                            case (byte)Status.FAILURE:
                                logtxt = Enum.GetName(typeof(Failure), recive[3]);
                                log("返回QueryState1状态:" + logtxt);
                                return logtxt;
                            default:
                                logtxt = Enum.GetName(typeof(Status), recive[2]);
                                log("返回QueryState1状态:" + logtxt);
                                return logtxt;
                        }
                    }
                    count++;
                }
                catch (Exception e)
                {
                    log("QueryState1出错：" + e.Message);
                    AutoReset();
                    return null;
                }
            }
        }

        /// <summary>
        /// 查询状态并搬运和收纳纸币
        /// </summary>
        /// <param name="send">发送命令</param>
        /// <param name="type">发送stack类型：stack-1,stack-2,return,inhibit</param>
        /// <returns>状态</returns>
        public string QueryState(byte[] send, string type)
        {
            log("type是" + type);
            var count = 0;
            var total = 0;
            _sp.Write(send, 0, send.Length);
            while (true)
            {
                if (count >= 4)
                {
                    _sp.Write(send, 0, send.Length);
                    count = 0;
                    total++;
                }
                if (total > 3)
                {
                    log("超时");
                    return "超时";
                }
                Thread.Sleep(200);
                var recive = new byte[_sp.BytesToRead];
                if (recive.Length == 0)
                {
                    log("重置");
                    AutoReset();
                }
                try
                {
                    _sp.Read(recive, 0, _sp.BytesToRead);

                    if (recive[0] != 0x00 && recive[1] != 0x00 && recive[2] != 0x00)
                    {
                        var bt = new byte[] { };
                        var txt = "";
                        switch (recive[2])
                        {
                            case (byte)Status.ENABLE_IDLING:
                                log("QueryState状态:ENABLE_IDLING");
                                if (type == "inhibit")
                                {
                                    //todo:禁止收纳纸币
                                }
                                return "ENABLE_IDLING";
                            case (byte)Status.ACCEPTING:
                                log("QueryState状态:ACCEPTING");
                                return "ACCEPTING";
                            case (byte)Status.ESCROW://todo:识别后的纸币如果属于设置的则搬运stacking，否则返回纸币
                                log("QueryState状态:ESCROW");
                                txt = Enum.GetName(typeof(Escrow), recive[3]);
                                if (type == "stack-1" || string.IsNullOrEmpty(type))
                                {
                                    bt = new byte[] { 0xfc, 0x05, 0x41 };//stack-1收纳纸币
                                }
                                if (type == "stack-2")
                                {
                                    bt = new byte[] { 0xfc, 0x05, 0x42 };//stack-2收纳纸币
                                }
                                if (type == "return")
                                {
                                    bt = new byte[] { 0xfc, 0x05, 0x43 };//返回纸币
                                }
                                var vCrc = CrcCcitt.Crc(bt);
                                OperationCommand(vCrc);//发送stack-1或return
                                return txt;
                            case (byte)Status.STACKING://todo:搬运有问题时到rejecting状态
                                log("QueryState状态:STACKING");
                                return "STACKING";
                            case (byte)Status.VEND_VALID:
                                log("QueryState状态:VEND_VALID");
                                bt = new byte[] { 0xfc, 0x05, 0x50, 0xAA, 0x05 };
                                Ack(bt);//发送ack
                                log("发送ack");
                                Thread.Sleep(500);
                                return "VEND_VALID";
                            case (byte)Status.STACKED:
                                log("QueryState状态:STACKED");
                                return "STACKED";
                            case (byte)Status.STACKER_FULL://todo:如果装满就要取出纸币，但是我现在不能确定是机器自动退还，还是必须人工退还
                                log("QueryState状态:STACKER_FULL");
                                return "STACKER_FULL";
                            case (byte)Status.REJECTING:
                                log("QueryState状态:REJECTING");
                                return "REJECTING";
                            case (byte)Status.JAM_IN_ACCEPTOR://todo:发生纸币阻塞就要取出纸币，但是我现在不能确定是机器自动退还，还是必须人工退还
                                log("QueryState状态:JAM_IN_ACCEPTOR");
                                return "JAM_IN_ACCEPTOR";
                            case (byte)Status.RETURNING:
                                log("QueryState状态:RETURNING");
                                return "RETURNING";
                            case (byte)Status.HOLDING:
                                log("QueryState状态:HOLDING");
                                return "HOLDING";
                            case (byte)Status.DISABLE_INHIBIT:
                                log("QueryState状态:DISABLE_INHIBIT");
                                return "DISABLE_INHIBIT";
                            case (byte)Status.INITIALIZE:
                                log("QueryState状态:INITIALIZE");
                                return "INITIALIZE";
                            case (byte)Status.FAILURE:
                                log("QueryState状态:FAILURE");
                                return "FAILURE";
                            case (byte)Status.POWER_UP_WITH_BILL_IN_ACCEPTOR://在可退还的位置残留着纸币
                            case (byte)Status.POWER_UP_WITH_BILL_IN_STACKER://在不能退还的位置残留着纸币
                            case (byte)Status.POWERUP:
                                log("QueryState状态:POWERUP");
                                return "POWERUP";
                            default:
                                break;
                        }
                    }
                    count++;
                }
                catch (Exception e)
                {
                    log("QueryState2出错：" + e.Message);
                    AutoReset();
                    return "\t  <--- " + e.Message + Environment.NewLine + "\t\t[ ]";
                }
            }
        }

        /// <summary>
        /// 操作命令
        /// </summary>
        /// <param name="bt">发送的命令字节，如：reset[0xfc, 0x05, 0x40, CRC(这是经过差错控制的数据，两个字节)]</param>
        /// <returns></returns>
        public string OperationCommand(byte[] bt)
        {
            try
            {
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(200);
                var recive = new byte[_sp.BytesToRead];
                if (recive.Length == 0)
                {
                    log("重置");
                    AutoReset();
                }
                _sp.Read(recive, 0, _sp.BytesToRead);
                string logtxt = "";
                switch (recive[2])
                {
                    case (byte)ResponseToOperationCommand.ACK:
                        logtxt = "ack";
                        log("返回OperationCommand状态:" + logtxt);
                        return logtxt;
                    case (byte)ResponseToOperationCommand.INVALID_COMMAND:
                        logtxt = "invalid command";
                        log("返回OperationCommand状态:" + logtxt);
                        return logtxt;
                    default:
                        return "\t  <--- 300msec Timeout Error for Response." + Environment.NewLine + "\t\t[ ]";
                }
            }
            catch (Exception e)
            {
                log("OperationCommand出错：" + e.Message);
                AutoReset();
                return "OperationCommand出错：" + e.Message;
            }
        }

        /// <summary>
        /// 发送ack
        /// </summary>
        /// <param name="bt">ack指令</param>
        public void Ack(byte[] bt)
        {
            try
            {
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                log("Ack出错：" + e.Message);
                AutoReset();
            }
        }

        /// <summary>
        /// 设置命令
        /// </summary>
        /// <param name="bt">命令字节数组</param>
        /// <returns></returns>
        public string SettingCommand(byte[] bt)
        {
            try
            {
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(200);
                var recive = new byte[_sp.BytesToRead];
                if (recive.Length == 0)
                {
                    log("重置");
                    AutoReset();
                }
                _sp.Read(recive, 0, _sp.BytesToRead);
                var logtxt = "";
                switch (recive[2])
                {
                    case (byte)ResponseToSettingCommand.COMMUNICATION_MODE:
                        logtxt = Enum.GetName(typeof(ResponseToSettingCommand), recive[3]);
                        log("返回SettingCommand状态:" + logtxt);
                        return logtxt;
                    case (byte)ResponseToSettingCommand.DIRECTION:
                        logtxt = Enum.GetName(typeof(ResponseToSettingCommand), recive[3]);
                        log("返回SettingCommand状态:" + logtxt);
                        return logtxt;
                    case (byte)ResponseToSettingCommand.ENABLE_DISABLE:
                        logtxt = Enum.GetName(typeof(ResponseToSettingCommand), recive[3]);
                        log("返回SettingCommand状态:" + logtxt);
                        return logtxt;
                    case (byte)ResponseToSettingCommand.INHIBIT:
                        logtxt = Enum.GetName(typeof(ResponseToSettingCommand), recive[3]);
                        log("返回SettingCommand状态:" + logtxt);
                        return logtxt;
                    case (byte)ResponseToSettingCommand.OPTIONAL_FUNCTION:
                        logtxt = Enum.GetName(typeof(ResponseToSettingCommand), recive[3]);
                        log("返回SettingCommand状态:" + logtxt);
                        return logtxt;
                    case (byte)ResponseToSettingCommand.SECURITY:
                        logtxt = Enum.GetName(typeof(ResponseToSettingCommand), recive[3]);
                        log("返回SettingCommand状态:" + logtxt);
                        return logtxt;

                    default:
                        return "\t  <--- 300msec Timeout Error for Response." + Environment.NewLine + "\t\t[ ]";
                }
            }
            catch (Exception e)
            {
                log("SettingCommand出错：" + e.Message);
                AutoReset();
                return "SettingCommand出错：" + e.Message;
            }
        }

        /// <summary>
        /// 设置命令请求
        /// </summary>
        /// <param name="bt">命令字节数组</param>
        /// <returns></returns>
        public string SettingStatusRequest(byte[] bt)
        {
            try
            {
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(200);
                var recive = new byte[_sp.BytesToRead];
                if (recive.Length == 0)
                {
                    log("重置");
                    AutoReset();
                }
                _sp.Read(recive, 0, _sp.BytesToRead);
                var logtxt = "";
                switch (recive[2])
                {
                    case (byte)SettingStatusRequests.COMMUNICATION_MODE:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.DIRECTION:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.ENABLE_DISABLE:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.INHIBIT:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.OPTIONAL_FUNCTION:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.SECURITY:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.VERSION_REQUEST:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.BOOT_VERSION_REQUEST:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    case (byte)SettingStatusRequests.CURRENCY_ASSIGN_REQUEST:
                        logtxt = Enum.GetName(typeof(SettingStatusRequests), recive[3]);
                        log("返回SettingStatusRequest状态:" + Environment.NewLine + logtxt + Environment.NewLine);
                        return logtxt;
                    default:
                        return "\t  <--- 300msec Timeout Error for Response." + Environment.NewLine + "\t\t[ ]";
                }
            }
            catch (Exception e)
            {
                log("SettingStatusRequest出错：" + e.Message);
                AutoReset();
                return "SettingStatusRequest出错：" + e.Message;
            }
        }

        /// <summary>
        /// 恢复出厂设置
        /// </summary>
        public void AutoReset()
        {
            /*               重置开始             */
            var bt = new byte[] { 0xfc, 0x05, 0x40 };
            var vCrc = CrcCcitt.Crc(bt);
            OperationCommand(vCrc);
            /*               重置结束             */
            //Thread.Sleep(200);

            /*             查询状态开始             */
            bt = new byte[] { 0xfc, 0x05, 0x11, 0x27, 0x56 };
            QueryState(bt);
            /*             查询状态结束             */
            // Thread.Sleep(500);

            /*          设置enable:0x00(可用)开始         */
            bt = new byte[] { 0xfc, 0x07, 0xc0, 0x00, 0x00 };
            vCrc = CrcCcitt.Crc(bt);
            SettingCommand(vCrc);
            /*          设置enable:0x00(可用)结束         */
            //Thread.Sleep(500);

            /*          设置security:0x00(可用)开始         */
            bt = new byte[] { 0xfc, 0x07, 0xc1, 0x00, 0x00 };
            vCrc = CrcCcitt.Crc(bt);
            SettingCommand(vCrc);
            /*          设置security:0x00(可用)结束         */
            //Thread.Sleep(500);

            /*          设置Inhibit:0x00(可用)开始         */
            bt = new byte[] { 0xfc, 0x06, 0xc3, 0x00 };
            vCrc = CrcCcitt.Crc(bt);
            SettingCommand(vCrc);
            /*          设置Inhibit:0x00(可用)结束         */
            Thread.Sleep(500);

            /*          设置Direction:0x00(可用)开始         */
            bt = new byte[] { 0xfc, 0x06, 0xc4, 0x00 };
            vCrc = CrcCcitt.Crc(bt);
            SettingCommand(vCrc);
            /*          设置Direction:0x00(可用)结束         */
            // Thread.Sleep(500);

            /*          设置comm:0x00(可用)开始         */
            bt = new byte[] { 0xfc, 0x06, 0xc2, 0x00 };
            vCrc = CrcCcitt.Crc(bt);
            SettingCommand(vCrc);
            /*          设置comm:0x00(可用)结束         */
            // Thread.Sleep(500);

            /*          设置function:0x00(可用)开始         */
            bt = new byte[] { 0xfc, 0x06, 0xc5, 0x00 };
            vCrc = CrcCcitt.Crc(bt);
            SettingCommand(vCrc);
            /*          设置function:0x00(可用)结束         */
        }

    }
}
