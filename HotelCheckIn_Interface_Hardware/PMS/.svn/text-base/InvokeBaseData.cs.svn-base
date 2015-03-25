using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.PMS
{
    public class InvokeBaseData
    {
        /// <summary>
        /// 接口版本号，不同版本的接口可能有不同的使用方法，当前为1.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 保留参数
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 要调用的方法名称
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 是客户端调用接口时的参数列表字符串，各个业务参数之间是以单字符逗号(,)隔开，接口
        /// 获取时，根据不同的参数协议，对querystring进行不同的拆分
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// 保留参数
        /// </summary>
        public string ClientInfo { get; set; }

        /// <summary>
        /// 字符串格式，客户端调用者存储的状态信息，会由服务器原样返回。
        /// </summary>
        public string State { get; set; }
    }
}
