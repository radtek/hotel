using System;
using System.Runtime.InteropServices;

namespace CheckIn.AddDll
{
    public class FaceID
    {
        [DllImport("YouoFaceDetect.dll", CharSet = CharSet.Ansi)]
        public static extern int InitDll();

        [DllImport("YouoFaceDetect.dll", CharSet = CharSet.Ansi)]
        public static extern void ReleaseDll();

        [DllImport("YouoFaceDetect.dll", SetLastError = true, CharSet = CharSet.Ansi,
            ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartRecognition(string Img, IntPtr ShowHwnd);

        [DllImport("YouoFaceDetect.dll", CharSet = CharSet.Ansi)]
        public static extern int GetRecognitionStatus();

        [DllImport("YouoFaceDetect.dll", SetLastError = true, CharSet = CharSet.Ansi,
             ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StopRecognition(string Img);
    }
}
