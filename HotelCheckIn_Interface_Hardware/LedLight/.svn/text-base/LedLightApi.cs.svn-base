using System;
using System.IO.Ports;

namespace HotelCheckIn_Interface_Hardware.LedLight
{
    public class LedLightApi
    {
        public LedLightApi()
        {
            ComPort = "com5";
        }

        public string ComPort { get; set; }

        /// <summary>
        /// 打开所有Led灯
        /// </summary>
        public void OpenAllLed()
        {
            try
            {
                SerialPort sp = new SerialPort(ComPort)
                {
                    BaudRate = 9600, //波特率
                    DataBits = 8,//数据位
                    StopBits = StopBits.One,//两个停止位
                    Parity = Parity.None,//无奇偶校验位
                    ReadTimeout = 100,
                    WriteTimeout = -1
                };
                sp.Open();
                byte[] send = { 104, 00, 01, 00, 00, 00, 105, 22 };
                sp.Write(send, 0, send.Length);
                sp.Close();
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 关闭所有Led灯
        /// </summary>
        public void CloseAllLed()
        {
            try
            {
                SerialPort sp = new SerialPort(ComPort)
                {
                    BaudRate = 9600, //波特率
                    DataBits = 8,//数据位
                    StopBits = StopBits.One,//两个停止位
                    Parity = Parity.None,//无奇偶校验位
                    ReadTimeout = 100,
                    WriteTimeout = -1
                };
                sp.Open();
                byte[] send = { 104, 00, 01, 60, 00, 00, 165, 22 };
                sp.Write(send, 0, send.Length);
                sp.Close();
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        /// 根据传的数值打开led灯
        /// </summary>
        /// <param name="num">1，2，3，4</param>
        public void OpenLedByNum(int num)
        {
            try
            {
                if (num > 4 || num < 1)
                {
                    return;
                }
                SerialPort sp = new SerialPort(ComPort)
                {
                    BaudRate = 9600, //波特率
                    DataBits = 8,//数据位
                    StopBits = StopBits.One,//两个停止位
                    Parity = Parity.None,//无奇偶校验位
                    ReadTimeout = 100,
                    WriteTimeout = -1
                };
                sp.Open();
                switch (num)
                {
                    case 1:
                        byte[] send1 = { 104, 00, 01, 56, 00, 00, 161, 22 };
                        sp.Write(send1, 0, send1.Length);
                        sp.Close();
                        break;
                    case 2:
                        byte[] send2 = { 104, 00, 01, 52, 00, 00, 157, 22 };
                        sp.Write(send2, 0, send2.Length);
                        sp.Close();
                        break;
                    case 3:
                        byte[] send3 = { 104, 00, 01, 44, 00, 00, 149, 22 };
                        sp.Write(send3, 0, send3.Length);
                        sp.Close();
                        break;
                    case 4:
                        byte[] send4 = { 104, 00, 01, 28, 00, 00, 133, 22 };
                        sp.Write(send4, 0, send4.Length);
                        sp.Close();
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
