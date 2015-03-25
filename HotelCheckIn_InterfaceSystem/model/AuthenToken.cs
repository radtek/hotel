using System.Configuration;
using CommonLibrary;

namespace HotelCheckIn_InterfaceSystem.model
{
    public class AuthenToken
    {
        private string request_url;
        private string sob_hotelgroup_id;
        private string sob_hotel_id;
        private string sob_code;
        private string sob_password;
        private string locale;

        /// <summary>
        /// PMS接口只会被后台系统引用，所以配置文件放在后台系统的web.config中。
        /// </summary>
        public AuthenToken()
        {
            var tmp = ConfigurationSettings.AppSettings["request_url"];
            request_url = tmp.Length > 0 ? tmp : "http://test.chinapms.com:8888/gateway/xml";
            tmp = ConfigurationSettings.AppSettings["sob_hotelgroup_id"];
            sob_hotelgroup_id = tmp.Length > 0 ? tmp : "";
            sob_hotel_id = "";
            tmp = ConfigurationSettings.AppSettings["sob_code"];
            sob_code = tmp.Length > 0 ? tmp : "www.test.com";
            tmp = ConfigurationSettings.AppSettings["sob_password"];
            sob_password = tmp.Length > 0 ? tmp : "111111";
            tmp = ConfigurationSettings.AppSettings["locale"];
            locale = tmp.Length > 0 ? tmp : "zh_CN";
        }

        /// <summary>
        /// 请求的url
        /// </summary>
        public string request_Url
        {
            get { return this.request_url; }
            set { this.request_url = value; }
        }
        /// <summary>
        /// 集团编码
        /// </summary>
        public string sob_Hotelgroup_Id
        {
            get { return this.sob_hotelgroup_id; }
            set { this.sob_hotelgroup_id = value; }
        }
        /// <summary>
        /// 酒店编码
        /// </summary>
        public string sob_Hotel_Id
        {
            get { return this.sob_hotel_id; }
            set { this.sob_hotel_id = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string sob_Code
        {
            get { return this.sob_code; }
            set { this.sob_code = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string sob_Password
        {
            get { return this.sob_password; }
            set { this.sob_password = value; }
        }
        /// <summary>
        /// 区域
        /// </summary>
        public string Local
        {
            get { return this.locale; }
            set { this.locale = value; }
        }

        public static AuthenToken getInstance()
        {
            return new AuthenToken();
        }
    }
}
