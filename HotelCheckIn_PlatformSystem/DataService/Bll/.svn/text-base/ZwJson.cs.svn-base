using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.BLL;

namespace HotelCheckIn_PlatformSystem.DataService.Bll
{
    public class ZwJson<T> : EasyUiJson<T>
    {
        /// <summary>
        /// 信息状态
        ///  false:失败，true:成功, 2:其它
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// js前台执行方法
        /// </summary>
        public string JsExecuteMethod { get; set; }

        /// <summary>
        /// 其它需要的参数
        /// </summary>
        public string Other { get; set; }

        public override string ToString()
        {
            return "Total:" + base.Total +
                   ",Rows:" + base.Rows +
                   ",IsSuccess:" + this.IsSuccess +
                   ",Msg:" + this.Msg;
        }
    }
}
