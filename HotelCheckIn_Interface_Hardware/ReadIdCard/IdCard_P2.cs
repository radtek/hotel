using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using CommonLibrary.exception;

namespace HotelCheckIn_Interface_Hardware.ReadIdCard
{
    public class IdCard_P2 :IdCardInterFace
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
        public static extern int Syn_ReadMsg(int iPortID, int iIfOpen, ref IdCardModel pIDCardData);
        /********************附加类API *****************************/
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_SendSound", CharSet = CharSet.Ansi)]
        public static extern int Syn_SendSound(int iCmdNo);
        [DllImport("Syn_IDCardRead.dll", EntryPoint = "Syn_DelPhotoFile", CharSet = CharSet.Ansi)]
        public static extern void Syn_DelPhotoFile();

        int iPort = 1001;//端口号
        int st = 0;//返回码
        /// <summary>
        /// 打开端口
        /// </summary>
        /// <returns></returns>
        public bool OpenPort()
        {
            st = Syn_OpenPort(iPort);
            if (st != 0)
            {
                Syn_ClosePort(iPort);
                throw new BusinessException("打开端口失败！");
            }
            return true;
        }
        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <returns></returns>
        public bool ClosePort()
        {
            st = Syn_ClosePort(iPort);
            if (st != 0)
            {
                Syn_ClosePort(iPort);
                throw new BusinessException("关闭端口失败！");
            }
            return true;
        }
        /// <summary>
        /// 找卡，选卡
        /// </summary>
        /// <returns></returns>
        public bool IfHaveCard()
        {
            byte[] pucIIN = new byte[4];
            byte[] pucSN = new byte[8];
            //找卡
            st = Syn_StartFindIDCard(iPort, ref pucIIN[0], 0);
            if (st != 0)
            {
                return false;
            }
            //选卡   
            st = Syn_SelectIDCard(iPort, ref pucSN[0], 0);
            if (st != 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        public ResultIdModel ReadCard()
        {
            //byte[] pucIIN = new byte[4];
            //byte[] pucSN = new byte[8];
            IdCardModel CardMsg = new IdCardModel();
            ResultIdModel result = new ResultIdModel();
            //int nRet = Syn_OpenPort(iPort);
            //if (nRet == 0)
            //{
                //nRet = Syn_GetSAMStatus(iPort, 0);
                //nRet = Syn_StartFindIDCard(iPort, ref pucIIN[0], 0);
                //nRet = Syn_SelectIDCard(iPort, ref pucSN[0], 0);
                if (Syn_ReadMsg(iPort, 0, ref CardMsg) == 0)
                {
                    CardMsg.Sex = GetSex(CardMsg.Sex);
                    CardMsg.Nation = GetNation(CardMsg.Nation);

                    result.Name = CardMsg.Name;
                    result.Sex = CardMsg.Sex;
                    result.Nation = CardMsg.Nation;
                    result.Born = CardMsg.Address;
                    result.Address = CardMsg.Address;
                    result.IDCardNo = CardMsg.IDCardNo;
                    result.GrantDept = CardMsg.GrantDept;
                    result.UserLifeBegin = CardMsg.UserLifeBegin;
                    result.UserLifeEnd = CardMsg.UserLifeEnd;
                    result.PhotoFileName = CardMsg.PhotoFileName;
                    result.img = File.ReadAllBytes(CardMsg.PhotoFileName);
                    return result;
                }
                else
                {
                    //throw new BusinessException("读二代证信息错误!");
                    return result;
                }
            //}
            //else
            //{
            //    throw new BusinessException("打开端口错误!");
            //}
        }
        /// <summary>
        /// 解析性别
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public string GetSex(string sex)
        {
            if ("1".Equals(sex))
            {
                return "男";
            }
            else if ("2".Equals(sex))
            {
                return "女";
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 解析民族
        /// </summary>
        /// <param name="nation"></param>
        /// <returns></returns>
        public string GetNation(string nation)
        {
            switch (nation)
            {
                case "01":
                    return "汉";
                case "02":
                    return "蒙古";
                case "03":
                    return "回";
                case "04":
                    return "藏";
                case "05":
                    return "维吾尔";
                case "06":
                    return "苗";
                case "07":
                    return "彝";
                case "08":
                    return "壮";
                case "09":
                    return "布依";
                case "10":
                    return "朝鲜";
                case "11":
                    return "满";
                case "12":
                    return "侗";
                case "13":
                    return "瑶";
                case "14":
                    return "白";
                case "15":
                    return "土家";
                case "16":
                    return "哈尼";
                case "17":
                    return "哈萨克";
                case "18":
                    return "傣";
                case "19":
                    return "黎";
                case "20":
                    return "傈僳";
                case "21":
                    return "佤";
                case "22":
                    return "畲";
                case "23":
                    return "高山";
                case "24":
                    return "拉祜";
                case "25":
                    return "水";
                case "26":
                    return "东乡";
                case "27":
                    return "纳西";
                case "28":
                    return "景颇";
                case "29":
                    return "柯尔克孜";
                case "30":
                    return "土";
                case "31":
                    return "达翰尔";
                case "32":
                    return "仫佬";
                case "33":
                    return "羌";
                case "34":
                    return "布朗";
                case "35":
                    return "撒拉";
                case "36":
                    return "毛南";
                case "37":
                    return "仡佬";
                case "38":
                    return "锡伯";
                case "39":
                    return "阿昌";
                case "40":
                    return "普米";
                case "41":
                    return "塔吉克";
                case "42":
                    return "怒";
                case "43":
                    return "乌孜别克";
                case "44":
                    return "俄罗斯";
                case "45":
                    return "鄂温克";
                case "46":
                    return "德昂";
                case "47":
                    return "保安";
                case "48":
                    return "裕固";
                case "49":
                    return "京";
                case "50":
                    return "塔塔尔";
                case "51":
                    return "独龙";
                case "52":
                    return "鄂伦春";
                case "53":
                    return "赫哲";
                case "54":
                    return "门巴";
                case "55":
                    return "珞巴";
                case "56":
                    return "基诺";
                case "57":
                    return "其它";
                case "98":
                    return "外国人入籍";

                default:
                    return "";
            }
        }
        


    }
}
