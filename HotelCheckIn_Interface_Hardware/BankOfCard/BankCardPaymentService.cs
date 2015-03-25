using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.BankOfCard
{
    [StructLayout(LayoutKind.Sequential)]
    public class BankCardPaymentService
    {
        /// <summary>
        /// 1)	程序注册函数
        /// </summary>
        /// <param name="type">1   银行卡（银行卡消费、预授权等） </param>
        /// <param name="rescode">response返回：‘00’表示成功，其他表示失败</param>
        /// <returns></returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "APPS_Login", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int APPS_Login(int type, StringBuilder rescode);

        /// <summary>
        /// 2)	读卡器初始化函数
        /// </summary>
        /// <returns>返回值:0，表示成功，其他表示失败</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_OpenCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_OpenCard();

        /// <summary>
        /// 3)	检测卡函数
        /// </summary>
        /// <returns>返回值
        ///	读取卡片状态
        ///返回值
        /// 1--读卡器内无卡
        /// 2--读卡器内有卡
        /// 3--卡在读卡器入口
        ///-1--读卡器硬件故障
        ///-2--非法数据
        ///-3--卡操作失败
        ///-4--数据效验错误
        ///-5--断电
        ///-6--接收到了 IDL
        ///-7--超时  
        ///</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_CheckCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_CheckCard();

        /// <summary>
        /// 4)	读卡函数
        /// </summary>
        /// <returns>返回值:0，表示成功</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_ReadCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_ReadCard();

        /// <summary>
        /// 5)	关闭刷卡器
        /// </summary>
        /// <returns>返回值:0，表示成功；其他表示失败</returns>a
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_Close", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_Close();

        /// <summary>
        /// 6)	打卡密码键盘
        /// </summary>
        /// <returns>返回值:0，表示成功；其他表示失败</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "PIN_Open", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int PIN_Open();

        /// <summary>
        /// 7)	输入密码状态
        /// </summary>
        /// <returns>返回值:
        ///0x2A:输入了一个字符
        ///0x08:清除
        ///0x0D:确定
        ///0x02:超时
        ///0x1b:取消
        ///</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "PIN_ReadOneByte", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int PIN_ReadOneByte();

        /// <summary>
        /// 8)	密码安全函数
        /// </summary>
        /// <returns>返回值:0代表成功，其他代表失败。</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "PIN_GetPinValue", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int PIN_GetPinValue();

        /// <summary>
        /// 9)	关闭密码键盘函数
        /// </summary>
        /// <returns>返回值:0代表成功，其他代表失败。</returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "PIN_Destroy", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int PIN_Destroy();

        /// <summary>
        /// 10)	消费
        /// </summary>
        /// <param name="bankIn">内容	  格式	长度	描述
        ///                       金额	   N	  12	  消费金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足左补0。
        ///                       日期	   N	   4	  日期
        ///                       预授权号 N	   6	  原授权号，在预授权完成和撤销时使用
        ///</param>
        /// <param name="bankOut">
        /// 返回码	N	2	00 表示成功，其它表示失败
        ///银行行号	N	4	发卡行代码
        ///卡号  	N	20	卡号（屏蔽部分，保留前6后4）
        ///有效期	N	4	
        ///批次号	N	6	
        ///凭证号	N	6	
        ///金额	    N	12	
        ///备注	   ANS	40	
        ///商户号	N	15	
        ///终端号	N	8	
        ///交易参考号	N	12	
        ///交易日期	N	4	
        ///交易时间	N	6	
        ///授权号	N	6	
        ///</param>
        /// <returns></returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_Pay_Dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_Pay_Dll(StringBuilder bankIn, StringBuilder bankOut);

        /// <summary>
        /// 11)	预授权
        /// </summary>
        /// <param name="bankIn">内容	  格式	长度	描述
        ///                       金额	   N	  12	  消费金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足左补0。
        ///                       日期	   N	   4	  日期
        ///                       预授权号 N	   6	  原授权号，在预授权完成和撤销时使用
        ///</param>
        /// <param name="bankOut">
        /// 返回码	N	2	00 表示成功，其它表示失败
        ///银行行号	N	4	发卡行代码
        ///卡号  	N	20	卡号（屏蔽部分，保留前6后4）
        ///有效期	N	4	
        ///批次号	N	6	
        ///凭证号	N	6	
        ///金额	    N	12	
        ///备注	   ANS	40	
        ///商户号	N	15	
        ///终端号	N	8	
        ///交易参考号	N	12	
        ///交易日期	N	4	
        ///交易时间	N	6	
        ///授权号	N	6	
        ///</param>
        /// <returns></returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_PreAuth_Dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_PreAuth_Dll(StringBuilder bankIn, StringBuilder bankOut);

        /// <summary>
        /// 12)	预授权完成
        /// </summary>
        /// <param name="bankIn">内容	  格式	长度	描述
        ///                       金额	   N	  12	  消费金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足左补0。
        ///                       日期	   N	   4	  日期
        ///                       预授权号 N	   6	  原授权号，在预授权完成和撤销时使用
        ///</param>
        /// <param name="bankOut">
        /// 返回码	N	2	00 表示成功，其它表示失败
        ///银行行号	N	4	发卡行代码
        ///卡号  	N	20	卡号（屏蔽部分，保留前6后4）
        ///有效期	N	4	
        ///批次号	N	6	
        ///凭证号	N	6	
        ///金额	    N	12	
        ///备注	   ANS	40	
        ///商户号	N	15	
        ///终端号	N	8	
        ///交易参考号	N	12	
        ///交易日期	N	4	
        ///交易时间	N	6	
        ///授权号	N	6	
        ///</param>
        /// <returns></returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_PreAuthDone_Dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_PreAuthDone_Dll(StringBuilder bankIn, StringBuilder bankOut);

        /// <summary>
        /// 13)	预授权撤销
        /// </summary>
        /// <param name="bankIn">内容	  格式	长度	描述
        ///                       金额	   N	  12	  消费金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足左补0。
        ///                       日期	   N	   4	  日期
        ///                       预授权号 N	   6	  原授权号，在预授权完成和撤销时使用
        ///</param>
        /// <param name="bankOut">
        /// 返回码	N	2	00 表示成功，其它表示失败
        ///银行行号	N	4	发卡行代码
        ///卡号  	N	20	卡号（屏蔽部分，保留前6后4）
        ///有效期	N	4	
        ///批次号	N	6	
        ///凭证号	N	6	
        ///金额	    N	12	
        ///备注	   ANS	40	
        ///商户号	N	15	
        ///终端号	N	8	
        ///交易参考号	N	12	
        ///交易日期	N	4	
        ///交易时间	N	6	
        ///授权号	N	6	
        ///</param>
        /// <returns></returns>
        [DllImport("c:/gmc/posinf.dll", EntryPoint = "UMS_PreAuthCancell_Dll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int UMS_PreAuthCancell_Dll(StringBuilder bankIn, StringBuilder bankOut);

    }
}
