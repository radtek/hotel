using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;
using CommonLibrary.exception;

namespace HotelCheckIn_Interface_Hardware.ReadIdCard
{
    public class IdCard
    {
        private IdCardInterFace idCard_P;

        public IdCard(string str)
        {
            switch (str)
            {
                case "p1":
                   idCard_P=new IdCard_P1();
                    break;
                case "p2":
                    idCard_P = new IdCard_P2();
                    break;
                default:
                    idCard_P = null;
                    break;
            }
        }


        /// <summary>
        /// 打开端口
        /// </summary>
        /// <returns>0x90/打开端口成功 1/打开端口失败/端口号不合法</returns>
        public bool OpenPort()
        {
            if (idCard_P == null)
            {
                return false;
            }
            return idCard_P.OpenPort();
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <returns>0x90/关闭端口成功。0x01/端口号不合法</returns>
        public bool ClosePort()
        {
            if (idCard_P == null)
            {
                return false;
            }
            return idCard_P.ClosePort();
        }

        /// <summary>
        /// 判断是否存在卡/证
        /// </summary>
        /// <returns>0x9f/找卡成功 0x80/找卡失败</returns>
        public bool IfHaveCard()
        {
            if (idCard_P == null)
            {
                return false;
            }
            return idCard_P.IfHaveCard();
        }

        /// <summary>
        /// 读身份证
        /// </summary>
        /// <returns></returns>
        public ResultIdModel ReadCard()
        {
            if (idCard_P == null)
            {
                return new ResultIdModel();
            }
            return idCard_P.ReadCard();
        }
    }
}
