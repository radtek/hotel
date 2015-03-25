using System.Web.Security;

namespace HotelCheckIn_BackSystem.DataService.Common
{
    public class encrypt
    {
        public encrypt()
        { }

        /// <summary>  
        /// SHA1加密字符串  
        /// </summary>  
        /// <param name="source">源字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string SHA1(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        }


        /// <summary>  
        /// MD5加密字符串  
        /// </summary>  
        /// <param name="source">源字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string MD5(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "MD5");
        } 

    }
}
