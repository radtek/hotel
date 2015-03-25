using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HotelCheckIn_Interface_Hardware.CardManage
{
    [StructLayout(LayoutKind.Sequential)]
    public class PackageK100Dll
    {
        /// <summary>
        /// 打开串口，默认的波特率“9600, n, 8, 1”
        /// </summary>
        /// <param name="port">要打开的串口，例如打开com1，则*Port 存储”com1”</param>
        /// <returns>正确返回串口的句柄；错误=0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_CommOpen", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr M100A_CommOpen(string port);

        /// <summary>
        /// 以相应的波特率打开串口
        /// </summary>
        /// <param name="port">要打开的串口，例如打开com1，则*Port 存储”com1”</param>
        /// <param name="baudRate">
        /// [in]BaudRate 波特率选项，有效值如下：
        ///                 9600
        ///                 19200
        ///                 38400
        /// </param>
        /// <returns>正确返回串口的句柄；错误=0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_CommOpenWithBaud", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr M100A_CommOpenWithBaud(string port, uint baudRate);

        /// <summary>
        /// 关闭当前打开的串口
        /// </summary>
        /// <param name="comHandle">要关闭的串口的句柄</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_CommClose", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_CommClose(IntPtr comHandle);

        /// <summary>
        /// 设置 M100A 的通讯波特率
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="baud">波特率选项
        ///                     =0x33, 设置波特率为9600
        ///                     =0x34, 设置波特率为19200
        ///                     =0x35, 设置波特率为38400
        /// </param>
        /// <param name="recrodInfo">存储该条命令的通讯记录
        ///                             格式如下： TX: 02 00 02 46 33 03 76
        ///                             RX: 06
        ///                             TX: 05
        ///                             RX: 02 00 03 50 46 33 03 27
        /// </param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_SetCommBaud", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_SetCommBaud(IntPtr comHandle, bool bHasMacAddr, byte macAddr, byte baud, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recrodInfo);

        /// <summary>
        /// M100A 复位命令
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="pm">
        /// 复位选项
        ///=0x30，初始化读卡器,有卡弹卡（不上传版本信息）
        ///=0x31，初始化读卡器,有卡回收（不上传版本信息）
        ///=0x32，初始化读卡器,有卡停在读磁卡位置（不上传版本信息）
        ///=0x33，初始化读卡器，有卡停在读IC 卡位置（不上传版本信息）
        ///=0x34，初始化读卡器，不动作(上传版本信息)
        /// </param>
        /// <param name="verCode">存储的是机器的版本信息，如“TTCE_M100A_V*.**”</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_Reset", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_Reset(IntPtr comHandle, bool bHasMacAddr, byte macAddr, byte pm, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] verCode, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);

        /// <summary>
        /// 读取卡片在机器里的位置
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="cardStates">CardStates[0]，表示通道卡片位置，具体含义如下
        ///=0x30：通道无卡
        ///=0x31：读磁卡位置有卡
        ///=0x32：IC卡位置有卡
        ///=0x33：前端夹卡位置有卡
        ///=0x34：前端不夹卡位置有卡
        ///=0x35：卡不在标准位置(标准位置指的是上面5个位置
        ///（0x30~0x34）.当卡不在标准位置时，可以通过移动卡片
        ///命令将卡移动到标准位置)
        ///=0x36：卡片正在移动中
        ///=0x37：射频卡位置有卡
        ///CardStates[1]，表示卡箱卡片状态
        ///=0x30:卡箱无卡
        ///=0x31:卡箱卡片不足,提醒需要加卡
        ///=0x32:卡箱卡片足够</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_CheckCardPosition", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_CheckCardPosition(IntPtr comHandle, bool bHasMacAddr, byte macAddr, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] cardStates, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);

        /// <summary>
        /// 读每个传感器的状态
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="sensorStates">9组传感器的状态值（各个传感器位置分步请查看演示程序）
        ///上传顺序：C_S1，C_S2，C_S3，C_S4，C_S5，C_S6，B_S1，B_S2，B_S3
        ///=0x30：无卡。
        ///=0x31：有卡。</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_CheckSensorStates", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_CheckSensorStates(IntPtr comHandle, bool bHasMacAddr, byte macAddr, byte[] sensorStates, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);

        /// <summary>
        /// 读前6组传感器的电压
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="sensorVoltage">存储前6组传感器的电压信息，计算方法请参照通讯协议中“读取传
        ///感器电压”指令</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_CheckSensorVoltage", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_CheckSensorVoltage(IntPtr comHandle, bool bHasMacAddr, byte macAddr, byte[] sensorVoltage, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);

        /// <summary>
        /// 进卡设置，为立即返回方式，卡片一旦进入，则命令失效，卡片是否到具体位置，必须通过“读
        /// 取卡片在机器里的位置”命令来判断
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="enterType">具体有效值如下
        ///0x30: 禁止进卡(将取消先前设置好的进卡指令)
        ///0x31: 使能进卡，进卡后停卡在读磁卡位置
        ///0x32: 使能进卡，进卡后停卡在读IC卡位置
        ///0x33: 使能进卡，进卡后将卡回收到回收箱
        ///0x34: 使能进卡，进卡后停卡在前端夹卡位置
        ///0x35: 使能进卡，进卡后将卡弹出</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_EnterCard", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_EnterCard(IntPtr comHandle, bool bHasMacAddr, byte macAddr, byte enterType, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);

        /// <summary>
        /// 卡片传动指令
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="pm">卡片传动的选项，有效值如下：
        ///0x30: 将卡片传动到读磁卡位置
        ///0x31: 将卡片传动到IC卡位置
        ///0x32: 将卡片传动到前端夹卡位置
        ///0x33: 将卡片弹出
        ///0x34: 将卡片回收到回收箱</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_MoveCard", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_MoveCard(IntPtr comHandle, bool bHasMacAddr, byte macAddr, byte pm, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);

        /// <summary>
        /// 向机器发送EOT，取消命令
        /// </summary>
        /// <param name="comHandle">已经打开的串口的句柄</param>
        /// <param name="bHasMacAddr">是否为多机通讯版本(使用方式请参文档前部“接口函数公有的参数说明“)</param>
        /// <param name="macAddr">机器的地址，有效取值（0 到15）</param>
        /// <param name="recordInfo">存储该条命令的通讯记录</param>
        /// <returns>正确=0，错误=非0</returns>
        [DllImport("M100A_DLL.dll", EntryPoint = "M100A_Eot", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int M100A_Eot(IntPtr comHandle, bool bHasMacAddr, byte macAddr, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] recordInfo);


        public static byte READ_CARD_LOCATION = 0x30;//将卡片传动到读磁卡位置
        public static byte IC_CARD_LOCATION = 0x31;// 将卡片传动到IC卡位置
        public static byte RECOVERBOX_CARD_LOCATION = 0x34;// 将卡片回收到回收箱
        public static byte FRONTEND_CARD_LOCATION = 0x32;//将卡片传动到前端夹卡位置
        public static byte POB_CARD = 0x33;//将卡片弹出

    }
}
