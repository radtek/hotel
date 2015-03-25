using System.Collections.Generic;

namespace HotelCheckIn_BackSystem.HumanIdentify
{
    public class ZwJson<T>
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

        public IList<T> Data { get; set; }

        public object FirstData { get; set; }

        public override string ToString()
        {
            return "IsSuccess:" + this.IsSuccess +
                   ",Msg:" + this.Msg;
        }
    }
}
