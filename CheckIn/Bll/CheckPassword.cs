using CheckIn.BackService;

namespace CheckIn.Bll
{
    class CheckPassword
    {
        public static bool CheckPwd(string password)
        {
            InterFace inf = new InterFace();
            return inf.CheckPassword(password);
        }
    }
}
