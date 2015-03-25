using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace HotelCheckIn_Interface_Hardware.Out_Notes
{
    public class Cc
    {
        public string ComPort { get; set; }
        private static SerialPort sp = null;

        #region 私有常量
        //通讯字节常量
        private const int Soh = 0X01;
        private const int Stx = 0X02;
        private const int Etx = 0X03;
        private const int Eot = 0X04;
        private const int Ack = 0X06;
        private const int Nck = 0X15;
        private const int Id = 0X50;
        //命令常量
        private const int Purge = 0x44;
        private const int Dipsense = 0x45;
        private const int UpperLower = 0x56;
        private const int Status = 0x46;
        private const int RomVersion = 0x47;
        private const int TestDipsense = 0x76;

        #endregion

        /// <summary> 
        /// 定义委托 
        /// </summary> 
        public delegate void DelegateLog(string sReceived);

        /// <summary> 
        /// 定义一个消息接收事件 
        /// </summary> 
        public event DelegateLog log;

        public Cc(string port)
        {
            ComPort = port;
            sp = new SerialPort(ComPort)
            {
                BaudRate = 9600, //波特率
                DataBits = 8,//数据位
                StopBits = StopBits.One,//一个停止位
                Parity = Parity.None,//无奇偶校验位
                ReadTimeout = 3000,//超时时间设置为1000毫秒
            };
        }

        /// <summary>
        /// 重置
        /// </summary>
        public string Reset()
        {
            log("重置出钞设备");
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            try
            {
                var send = new byte[] { Eot, Id, Stx, Purge, Etx, 0x01 };
                var bcc = 0;
                for (int i = 0; i < send.Length - 1; i++)
                    bcc ^= send[i];
                send[send.Length - 1] = (byte)bcc;
                sp.Write(send, 0, send.Length);
                log("操作命令：" + GetBytesString(send, 0, send.Length, " "));
                for (int i = 0; i < 3; i++)
                {
                    var r = sp.ReadByte();
                    log("发送命令，收到的反馈。" + r);
                    if (r != Ack)
                    {
                        sp.Write(send, 0, send.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                var recive = new byte[7];

                for (var i = 0; i < 3; i++)
                {
                    bcc = 0;
                    //recive = GetData(recive.Length);
                    Thread.Sleep(6000);//停止6秒以获取数据
                    sp.Read(recive, 0, recive.Length);
                    for (var j = 0; j < recive.Length - 1; j++)
                    {
                        bcc ^= recive[j];
                    }
                    if (bcc != recive[recive.Length - 1])
                    {
                        sp.Write(new byte[] { Nck }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：NCK：失败");
                    }
                    else
                    {
                        sp.Write(new byte[] { Ack }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：ACK：成功");
                        break;
                    }
                }
                var errorCode = new Error { Code = recive[4] };
                log("接收到的命令：" + recive[3] + ",接收到的错误码:" + errorCode.Code + "--" + errorCode.ErrorMsg);
                if (recive[3] != Purge || errorCode.Code > 0x31)
                {
                    log("重置出钞模块出错");
                    return "重置出钞模块出错";
                }
                else
                {
                    log("重置出钞模块成功");
                    return "重置出钞模块成功";
                }
            }
            catch (Exception e)
            {

                log(e.ToString());
                return "重置出错，错误原因：" + e.Message;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
        }

        /// <summary>
        /// 出钞 DISPENSE(0x45)
        /// </summary>
        public void Chuchao()
        {
            log("出钞设备出钞");
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            try
            {

                var send = new byte[] { Eot, Id, Stx, Dipsense, 48, 51, Etx, 0x01 };
                int bcc = 0;
                for (int i = 0; i < send.Length - 1; i++)
                    bcc ^= send[i];
                send[send.Length - 1] = (byte)bcc;
                sp.Write(send, 0, send.Length);
                log("操作命令：" + GetBytesString(send, 0, send.Length, " "));
                for (int i = 0; i < 3; i++)
                {
                    var r = sp.ReadByte();
                    log("发送命令，收到的反馈。" + r);
                    if (r != Ack)
                    {
                        sp.Write(send, 0, send.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                byte[] recive = new byte[14];

                for (int i = 0; i < 3; i++)
                {
                    bcc = 0;
                    recive = GetData(recive.Length);
                    for (int j = 0; j < recive.Length - 1; j++)
                    {
                        bcc ^= recive[j];
                    }
                    if (bcc != recive[recive.Length - 1])
                    {
                        sp.Write(new byte[] { Nck }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：NCK：失败");
                    }
                    else
                    {
                        sp.Write(new byte[] { Ack }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：ACK：成功");
                        break;
                    }
                }
                Error errorCode = new Error();
                errorCode.Code = recive[9];
                log("接收到的命令：" + recive[3] + ",接收到的错误码:" + errorCode.Code + "--" + errorCode.ErrorMsg);
                if (recive[3] != Dipsense || errorCode.Code > 0x31)
                {
                    throw new Exception("出钞出错");
                }
            }
            catch (Exception e)
            {
                log(e.ToString());
                //throw;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
        }
        /// <summary>
        /// 出钞 DISPENSE(0x45),注意：10元是高位，100元是低位
        /// </summary>
        /// <param name="money">必需个位为0，如：220</param>
        public string OutMoney(int money)
        {
            var yuan100S = 0;//几百元
            var yuan10S = 0;//几十元
            var yuan100Count = 0;//100元数量
            var yuan10Count = 0;//10元数量
            var upper10S = 0;//高位的十位
            var upper1S = 0;//高位的个位
            var lower10S = 0;//低位的十位
            var lower1S = 0;//低位的个位


            log("出钞设备出钞");
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            if (money < 0 || (money % 10) > 0 || money > 6000)
            {
                return "出纸币金额不在指定范围内";
            }
            yuan10S = money % 100;//【以3210为例：10】 | 【以210为例：10】
            yuan100S = money - yuan10S;//【以3210为例：3200】 | 【以210为例：200】

            yuan10Count = yuan10S / 10;//【以3210为例：1】 | 【以210为例：1】
            yuan100Count = yuan100S / 100;//【以3210为例：32】 | 【以210为例：2】

            //================================【高位计算byte值】=========================================

            upper1S = yuan10Count % 10;//【以3210为例：1】 | 【以210为例：1】
            byte upperByte1S = ConvertAscii(upper1S);

            upper10S = (yuan10Count - upper1S) / 10;//【以3210为例：0】 | 【以210为例：0】
            byte upperByte10S = ConvertAscii(upper10S);

            //================================【低位计算byte值】=========================================

            lower1S = yuan100Count % 10;//【以3210为例：2】 | 【以210为例：2】
            byte lowerByte1S = ConvertAscii(lower1S);

            lower10S = (yuan100Count - lower1S) / 10;//【以3210为例：3】 | 【以210为例：0】
            byte lowerByte10S = ConvertAscii(lower10S);

            try
            {
                var send = new byte[] { Eot, Id, Stx, UpperLower, upperByte10S, upperByte1S, lowerByte10S, lowerByte1S, Etx, 0x01 };

                int bcc = 0;
                for (int i = 0; i < send.Length - 1; i++)
                    bcc ^= send[i];
                send[send.Length - 1] = (byte)bcc;
                sp.Write(send, 0, send.Length);
                log("操作命令：" + GetBytesString(send, 0, send.Length, " "));
                for (int i = 0; i < 3; i++)
                {
                    var r = sp.ReadByte();
                    log("发送命令，收到的反馈。" + r);
                    if (r != Ack)
                    {
                        sp.Write(send, 0, send.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                var recive = new byte[21];//因为采用的是upper和lower来出纸币所以根据文档内容这里的接收的字节数组大小应该是21

                for (var i = 0; i < 3; i++)
                {
                    bcc = 0;
                    //recive = GetData(recive.Length);//这是蕾锅之前写的方法，我觉得不适用所以注释了
                    Thread.Sleep(10000);//todo:这个时间是不是停太长了呢
                    sp.Read(recive, 0, recive.Length);
                    for (var j = 0; j < recive.Length - 1; j++)
                    {
                        bcc ^= recive[j];
                    }
                    if (bcc != recive[recive.Length - 1])
                    {
                        sp.Write(new byte[] { Nck }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：NCK：失败");
                    }
                    else
                    {
                        sp.Write(new byte[] { Ack }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：ACK：成功");
                        break;
                    }
                }
                var errorCode = new Error { Code = recive[9] };
                log("接收到的命令：" + recive[3] + ",接收到的错误码:" + errorCode.Code + "--" + errorCode.ErrorMsg);
                if (recive[3] != UpperLower || errorCode.Code > 0x31)
                {
                    log("出钞出错");
                    return "出钞出错";
                }
                else
                {
                    log("出钞成功");
                    return "出钞成功";
                }
            }
            catch (Exception e)
            {
                log(e.ToString());
                return "出纸币出错，错误原因：" + e.Message;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
        }

        /// <summary>
        /// 将int值转换成Ascii码值
        /// </summary>
        /// <param name="moneyCount"></param>
        /// <returns></returns>
        public byte ConvertAscii(int moneyCount)
        {
            switch (moneyCount)
            {
                case 0: return 0x30;
                case 1: return 0x31;
                case 2: return 0x32;
                case 3: return 0x33;
                case 4: return 0x34;
                case 5: return 0x35;
                case 6: return 0x36;
                case 7: return 0x37;
                case 8: return 0x38;
                case 9: return 0x39;
                default:
                    return 0x30;
            }
        }

        /// <summary>
        /// 查询状态 STATUS 0X46
        /// </summary>
        public void QueryStatus(ref Sensor0Status sensor0Status, ref Sensor1Status sensor1Status, ref Error error)
        {
            log("查询出钞设备状态");
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            try
            {
                var send = new byte[] { Eot, Id, Stx, Status, Etx, 0x01 };
                var bcc = 0;
                for (var i = 0; i < send.Length - 1; i++)
                    bcc ^= send[i];
                send[send.Length - 1] = (byte)bcc;
                sp.Write(send, 0, send.Length);
                log("操作命令：" + GetBytesString(send, 0, send.Length, " "));
                for (var i = 0; i < 3; i++)
                {
                    var r = sp.ReadByte();
                    log("发送命令，收到的反馈。" + r);
                    if (r != Ack)
                    {
                        sp.Write(send, 0, send.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                var recive = new byte[10];//接受数据的长度
                var recFalg = false;
                for (int i = 0; i < 3; i++)
                {
                    bcc = 0;
                    //recive = GetData(recive.Length);
                    Thread.Sleep(600);//停止600毫秒以获取数据
                    sp.Read(recive, 0, recive.Length);
                    for (var j = 0; j < recive.Length - 1; j++)
                    {
                        bcc ^= recive[j];
                    }
                    if (bcc != recive[recive.Length - 1])
                    {
                        sp.Write(new byte[] { Nck }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：NCK：失败");
                    }
                    else
                    {
                        sp.Write(new byte[] { Ack }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：ACK：成功");
                        recFalg = true;
                        break;
                    }
                }

                if (!recFalg)
                {
                    return;
                }
                int[] sensor0 = GetbitsValue(recive[6]);
                int[] sensor1 = GetbitsValue(recive[7]);

                sensor0Status.ChkSensor1 = sensor0[0];
                sensor0Status.ChkSensor2 = sensor0[1];
                sensor0Status.DivSensor1 = sensor0[2];
                sensor0Status.DivSensor2 = sensor0[3];
                sensor0Status.EjtSensor = sensor0[4];
                sensor0Status.ExitSensor = sensor0[5];
                sensor0Status.NearendSensor = sensor0[6];
                sensor0Status.Always1 = sensor0[7];

                sensor1Status.SqlSensor = sensor1[0];
                sensor1Status.Cassette0Sensor = sensor1[1];
                sensor1Status.Cassette1Sensor = sensor1[2];
                sensor1Status.ChkSensor3 = sensor1[3];
                sensor1Status.ChkSensor4 = sensor1[4];
                sensor1Status.NearendSensor = sensor1[5];
                sensor1Status.RejectTraySw = sensor1[6];
                sensor1Status.NotUsed = sensor1[7];

                error.Code = recive[5];
                log("接收到的命令：" + recive[3] + ",接收到的错误码:" + error.Code + "--" + error.ErrorMsg);
            }
            catch (Exception e)
            {
                log("QueryStatus出错：" + e.ToString());
                //throw;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
        }

        /// <summary>
        /// 获取硬件版本 STATUS 0X47
        /// </summary>
        public void GetRomVersion()
        {
            log("出钞设备出钞");
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            try
            {
                var send = new byte[] { Eot, Id, Stx, RomVersion, Etx, 0x01 };
                var bcc = 0;
                for (int i = 0; i < send.Length - 1; i++)
                    bcc ^= send[i];
                send[send.Length - 1] = (byte)bcc;
                sp.Write(send, 0, send.Length);
                log("操作命令：" + GetBytesString(send, 0, send.Length, " "));
                for (int i = 0; i < 3; i++)
                {
                    var r = sp.ReadByte();
                    log("发送命令，收到的反馈。" + r);
                    if (r != Ack)
                    {
                        sp.Write(send, 0, send.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                var recive = new byte[14];

                for (int i = 0; i < 3; i++)
                {
                    bcc = 0;
                    recive = GetData(recive.Length);
                    for (int j = 0; j < recive.Length - 1; j++)
                    {
                        bcc ^= recive[j];
                    }
                    if (bcc != recive[recive.Length - 1])
                    {
                        sp.Write(new byte[] { Nck }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：NCK：失败");
                    }
                    else
                    {
                        sp.Write(new byte[] { Ack }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：ACK：成功");
                        break;
                    }
                }
            }
            catch (Exception e)
            {

                log(e.ToString());
                //throw;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
        }

        /// <summary>
        /// 测试出钞 TestDispense 0X76
        /// </summary>
        public void TestDispense()
        {
            log("出钞设备出钞");
            if (!sp.IsOpen)
            {
                sp.Open();
            }
            try
            {
                byte[] send = new byte[] { Eot, Id, Stx, TestDipsense, Etx, 0x01 };
                int bcc = 0;
                for (int i = 0; i < send.Length - 1; i++)
                    bcc ^= send[i];
                send[send.Length - 1] = (byte)bcc;
                sp.Write(send, 0, send.Length);
                log("操作命令：" + GetBytesString(send, 0, send.Length, " "));
                for (int i = 0; i < 3; i++)
                {
                    var r = sp.ReadByte();
                    log("发送命令，收到的反馈。" + r);
                    if (r != Ack)
                    {
                        sp.Write(send, 0, send.Length);
                    }
                    else
                    {
                        break;
                    }
                }

                byte[] recive = new byte[14];

                for (int i = 0; i < 3; i++)
                {
                    bcc = 0;
                    recive = GetData(recive.Length);
                    for (int j = 0; j < recive.Length - 1; j++)
                    {
                        bcc ^= recive[j];
                    }
                    if (bcc != recive[recive.Length - 1])
                    {
                        sp.Write(new byte[] { Nck }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：NCK：失败");
                    }
                    else
                    {
                        sp.Write(new byte[] { Ack }, 0, 1);
                        log("收到信息：" + GetBytesString(recive, 0, recive.Length, " ") + ",接收标识：ACK：成功");
                        break;
                    }
                }
                Error errorCode = new Error();
                errorCode.Code = recive[9];
                log("接收到的命令：" + recive[3] + ",接收到的错误码:" + errorCode.Code + "--" + errorCode.ErrorMsg);
                if (recive[3] != Dipsense || errorCode.Code > 0x31)
                {
                    throw new Exception("出钞出错");
                }
            }
            catch (Exception e)
            {
                log(e.ToString());
                //throw;
            }
            finally
            {
                if (sp.IsOpen)
                {
                    sp.Close();
                }
            }
        }

        /// <summary>
        /// 根据需要取指定长度的数据
        /// </summary>
        /// <param name="lenght">指定长度</param>
        /// <returns></returns>
        private static byte[] GetData(int lenght)
        {
            var dataList = new List<byte>();

            do
            {
                byte d = (byte)sp.ReadByte();
                dataList.Add(d);
                Thread.Sleep(100);
            } while (dataList.Count < lenght);
            return dataList.ToArray();
        }

        /// <summary>
        /// 获取一个数字8位的值，返回一个8个元素的int数组
        /// </summary>
        /// <param name="input"></param>
        /// <returns>格式：0,1,0,1,0,1,0,1,</returns>
        public static int[] GetbitsValue(int input)
        {
            var ret = new int[8];
            for (int i = 0; i < 8; i++)
            {
                ret[i] = GetbitValue(input, i);
            }
            return ret;
        }


        /// <summary>
        /// 获取数据中某一位的值
        /// </summary>
        /// <param name="input">传入的数据类型,可换成其它数据类型,比如Int</param>
        /// <param name="index">要获取的第几位的序号,从0开始</param>
        /// <returns>返回值为-1表示获取值失败</returns>
        public static int GetbitValue(int input, int index)
        {
            return (input & ((uint)1 << index)) > 0 ? 1 : 0;
        }

        /// <summary>
        /// 将Byte数组转换成字符串
        /// </summary>
        /// <param name="bytes">数组对象</param>
        /// <param name="index">起始位置</param>
        /// <param name="count">转换长度</param>
        /// <param name="sep">间隔符</param>
        /// <returns></returns>
        public static string GetBytesString(byte[] bytes, int index, int count, string sep)
        {
            return String.Join(sep, bytes.Skip(index).Take(count).Select(b => b.ToString("X2")));
        }

    }
}
