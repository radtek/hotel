
namespace CheckIn.Bll
{
    public class MachineError
    {
        static MachineError()
        {
            ErrMsg = "";
            ErrCode = "";
            AllLock = false;
        }

        public static string ErrMsg { get; set; }
        public static string ErrCode { get; set; }
        public static bool AllLock { get; set; }
    }
}
