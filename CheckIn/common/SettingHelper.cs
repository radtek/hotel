using CommonLibrary;

namespace CheckIn.common
{
    class SettingHelper
    {
        /// <summary>
        /// 团购网站列表
        /// </summary>
        public string Tg
        {
            get
            {
                string tmp = XmlHelper.ReadNode("tg");
                return tmp.Length > 0 ? tmp : "";
            }
            set
            {
                XmlHelper.WriteNode("tg", value);
            }
        }

        /// <summary>
        /// 心跳时间间隔，间隔（毫秒），默认：60000
        /// </summary>
        public int XtTime
        {
            get
            {
                string tmp = XmlHelper.ReadNode("xttime");
                return int.Parse(tmp.Length > 0 ? tmp : "60000");
            }
            set
            {
                XmlHelper.WriteNode("xttime", value.ToString());
            }
        }

        /// <summary>
        /// 团购密钥长度，默认12
        /// </summary>
        public int KeyLenght
        {
            get
            {
                string tmp = XmlHelper.ReadNode("keylenght");
                return int.Parse(tmp.Length > 0 ? tmp : "12");
            }
            set
            {
                XmlHelper.WriteNode("keylenght", value.ToString());
            }
        }

        /// <summary>
        /// 素材url:默认：http://www.youotech.com
        /// </summary>
        public string ScUrl
        {
            get
            {
                string tmp = XmlHelper.ReadNode("scurl");
                return tmp.Length > 0 ? tmp : "http://www.youotech.com";
            }
            set
            {
                XmlHelper.WriteNode("scurl", value);
            }
        }

        /// <summary>
        /// 机器ID，默认：1
        /// </summary>
        public string MacId
        {
            get
            {
                string tmp = XmlHelper.ReadNode("macid");
                return tmp.Length > 0 ? tmp : "1";
            }
            set
            {
                XmlHelper.WriteNode("macid", value);
            }
        }

        /// <summary>
        /// 机器密码：默认：12345678
        /// </summary>
        public string PassWord
        {
            get
            {
                string tmp = XmlHelper.ReadNode("password");
                return tmp.Length > 0 ? tmp : "12345678";
            }
            set
            {
                XmlHelper.WriteNode("password", value);
            }
        }

        /// <summary>
        /// 界面操作超时，单位：秒（s），默认：100
        /// </summary>
        public int TimeOut
        {
            get
            {
                string tmp = XmlHelper.ReadNode("timeout");
                return int.Parse(tmp.Length > 0 ? tmp : "100");
            }
            set
            {
                XmlHelper.WriteNode("timeout", value.ToString());
            }
        }

        /// <summary>
        /// 酒店ID，默认：00000
        /// </summary>
        public string SobHotelId
        {
            get
            {
                string tmp = XmlHelper.ReadNode("sobhotelid");
                return tmp.Length > 0 ? tmp : "00000";
            }
            set
            {
                XmlHelper.WriteNode("sobhotelid", value);
            }
        }

        /// <summary>
        /// 人脸识别时间限制，秒（s），默认：30
        /// </summary>
        public int CheckFaceTimeOut
        {
            get
            {
                string tmp = XmlHelper.ReadNode("checkfacetimeout");
                return int.Parse(tmp.Length > 0 ? tmp : "30");
            }
            set
            {
                XmlHelper.WriteNode("checkfacetimeout", value.ToString());
            }
        }

        /// <summary>
        /// 摄像头截图存放位置，默认：C:\head.bmp
        /// </summary>
        public string VideoImagePath
        {
            get
            {
                string tmp = XmlHelper.ReadNode("videoimagepath");
                return tmp.Length > 0 ? tmp : "C:\\head.bmp";
            }
            set
            {
                XmlHelper.WriteNode("videoimagepath", value);
            }
        }

        /// <summary>
        /// 终端配置IP,默认：127.0.0.1
        /// </summary>
        public string Ip
        {
            get
            {
                string tmp = XmlHelper.ReadNode("ip");
                return tmp.Length > 0 ? tmp : "127.0.0.1";
            }
            set
            {
                XmlHelper.WriteNode("ip", value);
            }
        }

        /// <summary>
        /// 入钞端口，默认：4930
        /// </summary>
        public int RcPort
        {
            get
            {
                string tmp = XmlHelper.ReadNode("rcport");
                return int.Parse(tmp.Length > 0 ? tmp : "4930");
            }
            set
            {
                XmlHelper.WriteNode("rcport", value.ToString());
            }
        }

        /// <summary>
        /// 出钞端口，默认：4950
        /// </summary>
        public int CcPort
        {
            get
            {
                string tmp = XmlHelper.ReadNode("ccport");
                return int.Parse(tmp.Length > 0 ? tmp : "4950");
            }
            set
            {
                XmlHelper.WriteNode("ccport", value.ToString());
            }
        }
        
        /// <summary>
        /// 出钞纸币端口，默认：Com4
        /// </summary>
        public string CcComPort
        {
            get
            {
                var tmp = XmlHelper.ReadNode("ccComPort");
                return tmp.Length > 0 ? tmp : "Com4";
            }
            set
            {
                XmlHelper.WriteNode("ccComPort", value);
            }
        }

        /// <summary>
        /// 出钞硬币端口，默认：Com5
        /// </summary>
        public string CoinsComPort
        {
            get
            {
                string tmp = XmlHelper.ReadNode("coinsCom");
                return tmp.Length > 0 ? tmp : "Com5";
            }
            set
            {
                XmlHelper.WriteNode("coinsCom", value);
            }
        }

        /// <summary>
        /// 入钞com端口;默认：com1
        /// </summary>
        public string IntoNotesPort
        {
            get
            {
                var tmp = XmlHelper.ReadNode("intonotesCom");
                return !string.IsNullOrEmpty(tmp) ? tmp : "com1";
            }
            set
            {
                XmlHelper.WriteNode("intonotesCom", value);
            }
        }

        /// <summary>
        /// 自动发卡器端口，默认：com2
        /// </summary>
        public string LockRoomCom
        {
            get
            {
                string tmp = XmlHelper.ReadNode("lockroomcom");
                return tmp.Length > 0 ? tmp : "com2";
            }
            set
            {
                XmlHelper.WriteNode("lockroomcom", value);
            }
        }

        /// <summary>
        /// LED端口，默认：com7
        /// </summary>
        public string LedCom
        {
            get
            {
                string tmp = XmlHelper.ReadNode(" ledcom");
                return tmp.Length > 0 ? tmp : "com7";
            }
            set
            {
                XmlHelper.WriteNode("ledcom", value);
            }
        }


        public string OutmoneyFilefolder
        {
            get
            {
                string tmp = XmlHelper.ReadNode("outmoney_filefolder");
                return tmp.Length > 0 ? tmp : @"C:\Program Files\Vinsky\BD_Bridge\";
            }
            set
            {
                XmlHelper.WriteNode("outmoney_filefolder", value);
            }
        }

        public string OutmoneyFilefolderName
        {
            get
            {
                string tmp = XmlHelper.ReadNode("outmoney_filefolder_name");
                return tmp.Length > 0 ? tmp : "BD_Bridge";
            }
            set
            {
                XmlHelper.WriteNode("outmoney_filefolder_name", value);
            }
        }
        /// <summary>
        /// 押金，默认：100
        /// </summary>
        public string Rate
        {
            get
            {
                string tmp = XmlHelper.ReadNode("rate");
                return tmp.Length > 0 ? tmp : "100";
            }
            set
            {
                XmlHelper.WriteNode("rate", value);
            }
        }
        /// <summary>
        /// 身份证读卡器类型，默认：p1
        /// </summary>
        public string ReadCardType
        {
            get
            {
                string tmp = XmlHelper.ReadNode("readcardtype");
                return tmp.Length > 0 ? tmp : "p1";
            }
            set
            {
                XmlHelper.WriteNode("readcardtype", value);
            }
        }
        /// <summary>
        /// 门锁服务器IP，默认：192.168.10.140
        /// </summary>
        public string LockServer
        {
            get
            {
                string tmp = XmlHelper.ReadNode("lock_server");
                return tmp.Length > 0 ? tmp : "192.168.10.140";
            }
            set
            {
                XmlHelper.WriteNode("lock_server", value);
            }
        }

        /// <summary>
        /// 门锁设备COM口，默认4
        /// </summary>
        public string LockPort
        {
            get
            {
                string tmp = XmlHelper.ReadNode("lock_port");
                return tmp.Length > 0 ? tmp : "4";
            }
            set
            {
                XmlHelper.WriteNode("lock_port", value);
            }
        }
    }
}
