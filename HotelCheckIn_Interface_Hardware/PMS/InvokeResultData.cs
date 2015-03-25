using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_Interface_Hardware.PMS
{
    public class InvokeResultData
    {
        /// <summary>
        /// Result 是接口处理的结果， 若接口处理正确，则为0，否则为其它值：
        /// </summary>
        public string R { get; set; }

        /// <summary>
        /// State  是客户端传递服务器的State，由服务器原样返回;
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Type   是Message的结果类型，默认是0，表示Message默认结构，非0时，表明Message是自定义结构;
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// Message是接口处理后的返回数据。
        /// </summary>
        public List<Dictionary<string, string>> M { get; set; }
    }
}
