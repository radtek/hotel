using System;
using System.Runtime.InteropServices;

namespace CheckIn.AddDll
{
    public class IdCardManage
    {
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        [DllImport("PassportRead.dll", EntryPoint = "OpenDevice")]
        public static extern Int32 OpenDevice();

        /// <summary>
        /// 读取身份信息
        /// </summary>
        /// <returns></returns>
        [DllImport("PassportRead.dll", EntryPoint = "ReadPassport", CharSet = CharSet.Unicode)]
        public static extern string ReadPassport();
    }
}
