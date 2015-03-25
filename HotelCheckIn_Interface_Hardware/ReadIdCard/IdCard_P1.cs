using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using CommonLibrary.exception;
using System.Windows.Media.Imaging;

namespace HotelCheckIn_Interface_Hardware.ReadIdCard
{
    public class IdCard_P1:IdCardInterFace
    {
        public string name { get; set; } //姓名
        public string sex { get; set; } //性别
        public string nation { get; set; } //民族
        public string birthday { get; set; } //出生日期
        public string address { get; set; } //住址
        public string card_no { get; set; } //身份证号
        public string sign_organ { get; set; } //签发机关
        public string valid_date_start { get; set; } //有效起始日期
        public string valid_date_end { get; set; } //有效截止日期
        public byte[] img { get; set; } //头像

        //变量声明
        byte[] CardPUCIIN = new byte[255];//证/卡芯片管理号
        byte[] pucManaMsg = new byte[255];//证/卡芯片序列号
        byte[] pucCHMsg = new byte[256];//读到的文字信息
        byte[] pucPHMsg = new byte[3024];//读到的照片信息
        UInt32 puiCHMsgLen = 0;//读到的文字信息的长度
        UInt32 puiPHMsgLen = 0;//读到的照片信息的长度

        int st = 0;//返回码
        int port = 1001;//端口号
        int iIfOpen; //是否需要打开端口

        public byte[] pReadByte = new byte[0];


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


        /// <summary>
        /// 打开端口
        /// </summary>
        /// <returns>0x90/打开端口成功 1/打开端口失败/端口号不合法</returns>
        public bool OpenPort()
        {
            st = SDT_OpenPort(port);
            if (st != 0x90)
            {
                SDT_ClosePort(port);
                throw new BusinessException("打开端口失败！");
            }
            return true;
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <returns>0x90/关闭端口成功。0x01/端口号不合法</returns>
        public bool ClosePort()
        {
            st = SDT_ClosePort(port);
            if (st != 0x90)
            {
                SDT_ClosePort(port);
                throw new BusinessException("关闭端口失败！");
            }
            return true;
        }

        /// <summary>
        /// 判断是否存在卡/证
        /// </summary>
        /// <returns>0x9f/找卡成功 0x80/找卡失败</returns>
        public bool IfHaveCard()
        {
            //找卡
            st = SDT_StartFindIDCard(port, CardPUCIIN, 0);
            if (st != 0x9f)
            {
                return false;
            }
            //选卡   
            st = SDT_SelectIDCard(port, pucManaMsg, 1);
            if (st != 0x90)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读身份证
        /// </summary>
        /// <returns></returns>
        public ResultIdModel ReadCard()
        {
            
            //读卡
            st = SDT_ReadBaseMsg(port, pucCHMsg, ref puiCHMsgLen, pucPHMsg, ref puiPHMsgLen, 1);
            if (st != 0x90)
            {
                SDT_ClosePort(port);
                throw new BusinessException("读卡失败！");
            }
            else
            {
                SDT_ClosePort(port);
            }

            //解析身份证信息
            string idStr = System.Text.ASCIIEncoding.Unicode.GetString(pucCHMsg);
            ResultIdModel result = AnalysisIdCard(idStr);

            //获取身份证照片
            BitmapImage bmp = null;
            try
            {
                string CurrentPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                string picture = CurrentPath + "\\temp.wlt";
                picture = picture.Substring(6);

                File.Delete(picture);
                FileStream fs = File.Open(picture, FileMode.Append);
                fs.Write(pucPHMsg, 0, pucPHMsg.Length);
                fs.Close();

                int returnStr = GetBmp(picture, 2);

                string bmpPath = CurrentPath + "\\temp.bmp";
                bmpPath = bmpPath.Substring(6);

                FileStream fsbmp = new FileStream(bmpPath, FileMode.Open);

                BinaryReader r = new BinaryReader(fsbmp);
                r.BaseStream.Seek(0, SeekOrigin.Begin);
                pReadByte = r.ReadBytes((int)r.BaseStream.Length);

                fsbmp.Close();
            }
            catch
            {
                bmp = null;
                //throw new BusinessException("获取身份证照片失败！");
                return result;
            }
            result.img = pReadByte;
            return result;
        }

        /// <summary>
        /// 解析身份证
        /// </summary>
        /// <param name="idStr"></param>
        /// <returns></returns>
        public ResultIdModel AnalysisIdCard(string idStr)
        {
            if (idStr.Length < 110)
            {
                return null;
            }

            ResultIdModel result = new ResultIdModel();
            string name = idStr.Substring(0, 15);
            result.Name = name.Trim();
            string sex = idStr.Substring(15, 1);
            result.Sex = GetSex(sex);
            string nation = idStr.Substring(16, 2);
            result.Nation = GetNation(nation);
            string birthday = idStr.Substring(18, 8);
            result.Born = birthday;
            string address = idStr.Substring(26, 35);
            result.Address = address.Trim();
            string id_num = idStr.Substring(61, 18);
            result.IDCardNo = id_num;
            string sign_organ = idStr.Substring(79, 15);
            result.GrantDept = sign_organ.Trim();
            string valid_date_start = idStr.Substring(94, 8);
            result.UserLifeBegin = valid_date_start;
            string valid_date_end = idStr.Substring(102, 8);
            result.UserLifeEnd = valid_date_end;
            return result;
        }
        /// <summary>
        /// 解析性别，1男，0女，否则直接返回
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
