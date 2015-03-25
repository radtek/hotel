using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HotelCheckIn_Interface_Hardware.AdelRead
{
    public class AdelReadClass
    {
        //初始化
        [DllImport("MainDll.dll", EntryPoint = "Init", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int Init(int software, StringBuilder server, StringBuilder username, int port, int Encoder, int TMEncoder);

        //退出
        [DllImport("MainDll.dll", EntryPoint = "EndSession", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int EndSession();

        //读卡
        [DllImport("MainDll.dll", EntryPoint = "ReadCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCard([Out, MarshalAs(UnmanagedType.LPStr)]string room, [Out, MarshalAs(UnmanagedType.LPStr)]string gate, [Out, MarshalAs(UnmanagedType.LPStr)]string stime, [Out, MarshalAs(UnmanagedType.LPStr)]string guestname, [Out, MarshalAs(UnmanagedType.LPStr)]string guestid, [Out, MarshalAs(UnmanagedType.LPStr)]string track1, [Out, MarshalAs(UnmanagedType.LPStr)]string track2, ref long cardno, ref int st, int Breakfast);
        [DllImport("MainDll.dll", EntryPoint = "ReadCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCard1([Out, MarshalAs(UnmanagedType.LPArray)] byte[] room, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] gate, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] stime, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] guestname, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] guestid, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] track1, [Out, MarshalAs(UnmanagedType.LPArray)] byte[] track2, ref long cardno, ref int st, int Breakfast);

        //制新卡
        [DllImport("MainDll.dll", EntryPoint = "NewKey", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int NewKey(StringBuilder room, StringBuilder gate, StringBuilder stime, StringBuilder guestname, StringBuilder guestid, int overflag, int Breakfast, ref long cardno, StringBuilder track1, StringBuilder track2);

        //退房
        //[DllImport("MainDll.dll", EntryPoint = "CheckOut", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        //public static extern int CheckOut(string room, int cardno);

        //注销卡
        [DllImport("MainDll.dll", EntryPoint = "EraseCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int EraseCard(int cardno, StringBuilder track1, StringBuilder track2);
    }
}
