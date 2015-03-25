using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using CommonLibrary.exception;

namespace HotelCheckIn_Interface_Hardware.NewReadIdCard
{
    public class ReadIdCardDal
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct IDCardData
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string Name; //姓名   
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string Sex;   //性别
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public string Nation; //名族
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string Born; //出生日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
            public string Address; //住址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string IDCardNo; //身份证号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string GrantDept; //发证机关
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeBegin; // 有效开始日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public string UserLifeEnd;  // 有效截止日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 38)]
            public string reserved; // 保留
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string PhotoFileName; // 照片路径
            public byte[] img { get; set; } //头像
            
        }
        /************************端口类API *************************/
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_GetCOMBaud", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetCOMBaud(int iComID, ref uint puiBaud);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_SetCOMBaud", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetCOMBaud(int iComID, uint uiCurrBaud, uint uiSetBaud);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_OpenPort", CharSet = CharSet.Ansi)]
        public static extern int Syn_OpenPort(int iPortID);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_ClosePort", CharSet = CharSet.Ansi)]
        public static extern int Syn_ClosePort(int iPortID);

        /************************ SAM类API *************************/
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_GetSAMStatus", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMStatus(int iPortID, int iIfOpen);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_ResetSAM", CharSet = CharSet.Ansi)]
        public static extern int Syn_ResetSAM(int iPortID, int iIfOpen);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_GetSAMID", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMID(int iPortID, ref byte pucSAMID, int iIfOpen);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_GetSAMIDToStr", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMIDToStr(int iPortID, ref byte pcSAMID, int iIfOpen);
        /********************身份证卡类API *************************/
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_StartFindIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_StartFindIDCard(int iPortID, ref byte pucManaInfo, int iIfOpen);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_SelectIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_SelectIDCard(int iPortID, ref byte pucManaMsg, int iIfOpen);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_ReadMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadMsg(int iPortID, int iIfOpen, ref IDCardData pIDCardData);
        /********************附加类API *****************************/
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_SendSound", CharSet = CharSet.Ansi)]
        public static extern int Syn_SendSound(int iCmdNo);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_DelPhotoFile", CharSet = CharSet.Ansi)]
        public static extern void Syn_DelPhotoFile();

        int iPort;
        //获取空闲的端口、串口
        public void GetIPort()
        {
            iPort = 0;
            for (iPort = 1001; iPort < 1017;iPort++ )
            {
                if (Syn_OpenPort(iPort) == 0)
                {
                    if (Syn_GetSAMStatus(iPort,0)==0)
                    {
                        Syn_ClosePort(iPort);
                        return;
                    }
                }
                Syn_ClosePort(iPort);
            }
            for (iPort = 1; iPort < 17;iPort++ )
            {
                if (Syn_OpenPort(iPort) == 0)
                {
                    if (Syn_GetSAMStatus(iPort,0)==0)
                    {
                        Syn_ClosePort(iPort);
                        return;
                    }
                }
                Syn_ClosePort(iPort);
            }
        }
        //检测读卡器连接和端口是否正常
        public bool CheckMachine()
        {
            byte[] cSAMID = new byte[128];
            if (iPort == 0)
            {
                throw new BusinessException("没有连接读卡器！");
            }
            if (Syn_OpenPort(iPort)!=0)
            {
                throw new BusinessException("打开端口错误！");
            }
            if (Syn_GetSAMIDToStr(iPort,ref cSAMID[0],0)==0)
            {
                ASCIIEncoding encoding = new ASCIIEncoding( ); 
                string constructedString = encoding.GetString(cSAMID);
            }
            else
            {
                throw new BusinessException("获得安全模块ID错误！");
            }
            Syn_ClosePort(iPort);
            return true;
        }

        //读取身份证信息
        public IDCardData GetIdCardData()
        {
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            IDCardData CardMsg = new IDCardData();
            int nRet = Syn_OpenPort(iPort);
            if (nRet == 0)
            {
                nRet = Syn_GetSAMStatus(iPort, 0);
                nRet = Syn_StartFindIDCard(iPort, ref pucIIN[0], 0);
                nRet = Syn_SelectIDCard(iPort, ref pucSN[0], 0);
                if (Syn_ReadMsg(iPort, 0, ref CardMsg) == 0)
                {
                    CardMsg.img = File.ReadAllBytes(CardMsg.PhotoFileName);
                    return CardMsg;
                }
                else
                {
                    throw new BusinessException("读二代证信息错误!");
                }
            }
            else
            {
                throw new BusinessException("打开端口错误!");
            }
        }
    
    }
}
