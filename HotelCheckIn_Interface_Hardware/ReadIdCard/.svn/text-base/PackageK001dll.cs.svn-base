using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media.Imaging;

namespace HotelCheckIn_Interface_Hardware.ReadIdCard
{
    public class PackageK001dll
    {

        #region API声明
        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <returns>0x90/打开端口成功 1/打开端口失败/端口号不合法</returns>
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SDT_OpenPort(int iPort);
        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <returns>0x90/关闭端口成功。0x01/端口号不合法</returns>
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SDT_ClosePort(int iPort);
        /// <summary>
        /// 开始找卡。
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucManaInfo">证/卡芯片管理号</param>
        /// <param name="iIfOpen"></param>
        /// <returns>0x9f/找卡成功 0x80/找卡失败</returns>
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SDT_StartFindIDCard(int iPort, byte[] pucManaInfo, int iIfOpen);
        /// <summary>
        /// 选卡
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucManaMsg">证/卡芯片序列号</param>
        /// <param name="iIfOpen"></param>
        /// <returns>0x90/选卡成功 0x81/选卡失败</returns>
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SDT_SelectIDCard(int iPort, byte[] pucManaMsg, int iIfOpen);
        /// <summary>
        /// 读取证/卡固定信息
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="pucCHMsg">读到的文字信息</param>
        /// <param name="puiCHMsgLen">读到的文字信息长度</param>
        /// <param name="pucPHMsg">读到的照片信息</param>
        /// <param name="puiPHMsgLen">读到的照片信息长度</param>
        /// <param name="iIfOpen"></param>
        /// <returns>0x90/读固定信息成功 其他/读固定信息失败</returns>
        [DllImport("sdtapi.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SDT_ReadBaseMsg(int iPort, byte[] pucCHMsg, ref UInt32 puiCHMsgLen, byte[] pucPHMsg, ref UInt32 puiPHMsgLen, int iIfOpen);
        /// <summary>
        /// 解析身份证照片信息
        /// </summary>
        /// <param name="pucPHMsg"></param>
        /// <param name="intf"></param>
        /// <returns></returns>
        [DllImport("WltRS.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetBmp(string pucPHMsg, int intf);

        #endregion
        

    }
}
