/*************************************************************
 *                                                           *
 *                                                           *
 *                                                           *
 *                                                           *
 *                      2014年6月26日张威                    *
 *************************************************************/
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace HotelCheckIn_Interface_Hardware.CoinsMachine
{
    public class CoinsMachineService
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
        public CoinsMachineService()
        {
            ComPort = "com4";
        }

        /// <summary>
        /// 打开端口
        /// </summary>
        public bool OpenComPort()
        {
            log("打开串口");
            _sp = new SerialPort(ComPort);
            //波特率
            _sp.BaudRate = 9600;
            //数据位
            _sp.DataBits = 8;
            //1个停止位
            _sp.StopBits = StopBits.One;
            //无奇偶校验位
            _sp.Parity = Parity.None;
            _sp.ReadTimeout = 100;
            _sp.WriteTimeout = -1;
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
        /// 发送poll命令——检测连接是否正常（通电、通讯）
        /// </summary>
        public bool Poll()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xFE, 0xFE };
                var result = new byte[] { 0x01, 0x00, 0x03, 0x00, 0xFC };
                var tmp = new byte[result.Length];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, bt.Length, tmp, 0, result.Length);
                return CompareArray(tmp, result);
            }
            catch (Exception e)
            {
                log("Poll出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 发送Request payout high/low status命令——查询高、低位传感器状态，可查询硬币找零器内还有多少硬币（根据安装传感器不同，反馈有所不同）
        /// </summary>
        public byte RequestPayoutHighLowStatus()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xD9, 0x23 };
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                return recive[9];
            }
            catch (Exception e)
            {
                log("RequestPayoutHighLowStatus出错：" + e.Message);
                CloseComPort();
                return 0x00;
            }
        }

        /// <summary>
        /// 发送enable命令——激活硬币找零器，使其处于可出币状态
        /// </summary>
        public bool Enable()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x01, 0x01, 0xA4, 0xA5, 0xB2 };
                var result = new byte[] { 0x01, 0x00, 0x03, 0x00, 0xFC };
                var tmp = new byte[result.Length];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, bt.Length, tmp, 0, result.Length);
                return CompareArray(tmp, result);
            }
            catch (Exception e)
            {
                log("SetEnable出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 发送Request cipher key命令——询问密码，反馈的8位数是随机的。每次发找币命令前都一定要发一次“询问密码”的命令
        /// </summary>
        /// <returns>返回8位字节码</returns>
        public byte[] RequestCipherKey()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xA0, 0x5C };
                var tmp = new byte[8];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, 9, tmp, 0, 8);
                return tmp;
            }
            catch (Exception e)
            {
                log("RequestCipherKey出错：" + e.Message);
                CloseComPort();
                return null;
            }
        }

        /// <summary>
        /// 发送Dispense hopper coins命令——非加密Hopper不需要使用8位密码，8个字节都为00即可。
        /// 可以找1，2，5个硬币
        /// </summary>
        public string DispenseHopperCoins(int count)
        {
            var isLitterOrMoreDataResult = RequestPayoutHighLowStatus();
            if (isLitterOrMoreDataResult == 0x31)
            {
                log("很少、没有硬币");
                return "很少、没有硬币";
            }

            var divided5 = count / 5;
            var mod5 = count % 5;
            var divided2 = mod5 / 2;
            var mod2 = mod5 % 2;
            try
            {
                byte[] tmp;
                for (var i = 0; i < divided5; i++)
                {
                    isLitterOrMoreDataResult = RequestPayoutHighLowStatus();
                    if (isLitterOrMoreDataResult == 0x31)
                    {
                        log("很少、没有硬币");
                        return "很少、没有硬币";
                    }
                    tmp = DispenseHopperConinsAid();
                    tmp[12] = 0x05;
                    tmp[13] = CalculateParityBit(tmp);
                    var isenable = Enable();
                    if (!isenable)
                    {
                        log("enable有问题");
                        return "enable有问题";
                    }
                    DispenseHopperCoinsExe(tmp);
                    Thread.Sleep(200);
                    var data = RequestStatus();
                    data = RequestStatus();
                    if (data == null)
                    {
                        log("查询出错");
                        return "查询出错";
                    }
                    while (data[2] != 0x05 && data[1] != 0x00)
                    {
                        Thread.Sleep(200);
                        data = RequestStatus();
                        if (data[3] == 0x00) continue;
                        log("出币出错：有" + data[3] + "未找出");
                        return "出币出错：有" + data[3] + "未找出";
                    }
                }

                for (var i = 0; i < divided2; i++)
                {
                    isLitterOrMoreDataResult = RequestPayoutHighLowStatus();
                    if (isLitterOrMoreDataResult == 0x31)
                    {
                        log("很少、没有硬币");
                        return "很少、没有硬币";
                    }
                    tmp = DispenseHopperConinsAid();
                    tmp[12] = 0x02;
                    tmp[13] = CalculateParityBit(tmp);
                    var isenable = Enable();
                    if (!isenable)
                    {
                        log("enable有问题");
                        return "enable有问题";
                    }
                    DispenseHopperCoinsExe(tmp);
                    Thread.Sleep(200);
                    var data = RequestStatus();
                    data = RequestStatus();
                    if (data == null)
                    {
                        log("查询出错");
                        return "查询出错";
                    }
                    while (data[2] != 0x02 && data[1] != 0x00)
                    {
                        Thread.Sleep(200);
                        data = RequestStatus();
                        if (data[3] == 0x00) continue;
                        log("出币出错：有" + data[3] + "未找出");
                        return "出币出错：有" + data[3] + "未找出";
                    }
                }

                for (var i = 0; i < mod2; i++)
                {
                    isLitterOrMoreDataResult = RequestPayoutHighLowStatus();
                    if (isLitterOrMoreDataResult == 0x31)
                    {
                        log("很少、没有硬币");
                        return "很少、没有硬币";
                    }
                    tmp = DispenseHopperConinsAid();
                    tmp[12] = 0x01;
                    tmp[13] = CalculateParityBit(tmp);
                    var isenable = Enable();
                    if (!isenable)
                    {
                        log("enable有问题");
                        return "enable有问题";
                    }
                    DispenseHopperCoinsExe(tmp);
                    Thread.Sleep(200);
                    var data = RequestStatus();
                    data = RequestStatus();
                    if (data == null)
                    {
                        log("查询出错");
                        return "查询出错";
                    }
                    while (data[2] != 0x01 && data[1] != 0x00)
                    {
                        Thread.Sleep(200);
                        data = RequestStatus();
                        if (data[3] == 0x00) continue;
                        log("出币出错：有" + data[3] + "未找出");
                        return "出币出错：有" + data[3] + "未找出";
                    }
                }
                return "出币成功";
            }
            catch (Exception e)
            {
                log("DispenseHopperCoins出错：" + e.Message);
                CloseComPort();
                return "出币失败";
            }
        }

        /// <summary>
        /// 出币辅助方法
        /// </summary>
        /// <returns></returns>
        public byte[] DispenseHopperConinsAid()
        {
            var key = RequestCipherKey();
            var bt1 = new byte[] { 0x03, 0x09, 0x01, 0xA7 };
            var tmp = new byte[14];
            try
            {
                Array.Copy(bt1, tmp, 4);
                Array.Copy(key, 0, tmp, bt1.Length, key.Length);
                return tmp;
            }
            catch (Exception e)
            {
                log("DispenseHopperConinsAid出错：" + e.Message);
                CloseComPort();
                return null;
            }
        }

        /// <summary>
        /// 出币执行方法
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        public bool DispenseHopperCoinsExe(byte[] tmp)
        {
            try
            {
                _sp.Write(tmp, 0, tmp.Length);
                return true;
            }
            catch (Exception e)
            {
                log("DispenseHopperCoins出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 发送Request status命令——询问Hopper出币状态，发送出币命令后，需要每隔200ms发送一次该命令。
        /// </summary>
        public byte[] RequestStatus()
        {
            var tmp = new byte[4];
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xA6, 0x56 };
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, 9, tmp, 0, tmp.Length);
                return tmp;
            }
            catch (Exception e)
            {
                log("RequestStatus出错：" + e.Message);
                CloseComPort();
                return null;
            }
        }

        /// <summary>
        /// 查询hopper累计出币数
        /// </summary>
        /// <returns></returns>
        public bool QueryCoinsOutTotal()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xA8, 0x54 };
                var result = new byte[] { 0x01, 0x00, 0x03, 0x00, 0xFC };
                var tmp = new byte[5];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, 6, tmp, 0, 5);
                return CompareArray(tmp, result);
            }
            catch (Exception e)
            {
                log("RequestStatus出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 查询传感器状态
        /// </summary>
        /// <returns></returns>
        public bool RequestSensorStatus()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xD9, 0x23 };
                var result = new byte[] { 0x01, 0x00, 0x03, 0x00, 0xFC };
                var tmp = new byte[5];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, 6, tmp, 0, 5);
                return CompareArray(tmp, result);
            }
            catch (Exception e)
            {
                log("Reset出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 重置命令
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0x01, 0xFB };
                var result = new byte[] { 0x01, 0x00, 0x03, 0x00, 0xFC };
                var tmp = new byte[5];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, 6, tmp, 0, 5);
                return CompareArray(tmp, result);
            }
            catch (Exception e)
            {
                log("Reset出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 紧急停止
        /// </summary>
        /// <returns></returns>
        public bool EmergencyStop()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xAC, 0x50 };
                var result = new byte[] { 0x01, 0x00, 0x03, 0x00, 0xFC };
                var tmp = new byte[5];
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                Array.Copy(recive, 6, tmp, 0, 5);
                return CompareArray(tmp, result);
            }
            catch (Exception e)
            {
                log("EmergencyStop出错：" + e.Message);
                CloseComPort();
                return false;
            }
        }

        /// <summary>
        /// 测试Hopper
        /// TX=03 00 01 A3 59	反馈的16个2进制数值分别表示各种情况，具体参考下表。
        ///RX=01 02 03 00 40 00 BA	
        ///40                                             00	
        ///[hopper status register 1]	[hopper status register 2]
        ///B0-Abs. 侦测到电流过大           1 = YES	B0-找币工作时传感器被短路    1 = YES
        ///B1-找币超时                      1 = YES 	B1-单个找币模式              1 = YES 
        ///B2-上次找币时马达反转清除卡币    1 = YES 	B2-Checksum A error          1 = 错误 
        ///B3-空闲状态时光电传感器被挡      1 = YES 	B3-Checksum B error          1 = 错误 
        ///B4-空闲状态时传感器被短路        1 = YES 	B4-Checksum C error          1 = 错误 
        ///B5-找币工作时光电传感器被挡      1 = YES 	B5-Checksum D error          1 = 错误 
        ///B6-Power-up detected已上电       1 = YES 	B6-掉电 during NV write      1 = 失败 
        ///B7-禁止找币                      1 = YES	    B7-PIN number 机构           1 = 打开 
        /// </summary>
        public byte[] TestHopper()
        {
            try
            {
                var bt = new byte[] { 0x03, 0x00, 0x01, 0xA3, 0x59 };
                _sp.Write(bt, 0, bt.Length);
                Thread.Sleep(100);
                var recive = new byte[_sp.BytesToRead];
                _sp.Read(recive, 0, _sp.BytesToRead);
                return recive;
            }
            catch (Exception e)
            {
                log("TestHopper出错：" + e.Message);
                CloseComPort();
                return null;
            }
        }

        /// <summary>
        /// 数组比较是否相等
        /// </summary>
        /// <param name="bt1">数组1</param>
        /// <param name="bt2">数组2</param>
        /// <returns>true:相等，false:不相等</returns>
        public bool CompareArray(byte[] bt1, byte[] bt2)
        {
            try
            {
                var len1 = bt1.Length;
                var len2 = bt2.Length;
                if (len1 != len2)
                {
                    return false;
                }
                for (var i = 0; i < len1; i++)
                {
                    if (bt1[i] != bt2[i])
                        return false;
                }
                return true;
            }
            catch (Exception e)
            {
                log("CalculateParityBit出错：" + e.Message);
                CloseComPort();
                return false;
            }

        }

        /// <summary>
        /// 计算校验位
        /// </summary>
        /// <param name="bt"></param>
        public byte CalculateParityBit(byte[] bt)
        {
            try
            {
                if (bt == null || bt[0] == null)
                {
                    return 0x00;
                }

                var tmp = bt.Aggregate<byte, byte>(0x00, (current, b) => (byte)(current + b));
                return (byte)(0xff - tmp + 0x01);
            }
            catch (Exception e)
            {
                log("CalculateParityBit出错：" + e.Message);
                CloseComPort();
                return 0x00;
            }

        }
    }
}
