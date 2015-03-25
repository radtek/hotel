using System;
using System.IO;

namespace HotelCheckIn_BackSystem.DataService.Common
{
    public class uploadPhoto
    {
        private static string path = System.Configuration.ConfigurationSettings.AppSettings["basePath"];
        public uploadPhoto()
        { }
         string uploadpath = "";
         string imagepath = "";
        /// <summary>
        /// 判断checkout客户签名文件夹是否存在并存盘
        /// </summary>
        /// <param name="MacId"></param>
        /// <param name="OrderId"></param>
        /// <param name="CheckinTime"></param>
        /// <returns></returns>
        public string checkoutImage(string MacId,string OrderId,DateTime CheckinTime)
        {
            uploadpath = path + "upload/" + CheckinTime.ToShortDateString() + "/" + MacId + "/" + OrderId;
            imagepath = "upload/" + CheckinTime.ToShortDateString() + "/" + MacId + "/" + OrderId; 
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
            return uploadpath + "/checkout.jpg" + "#" + imagepath + "/checkout.jpg";
        }
        /// <summary>
        /// 判断checkin客户签名文件夹是否存在并存盘
        /// </summary>
        /// <param name="MacID"></param>
        /// <param name="OrderId"></param>
        /// <param name="CheckinTime"></param>
        /// <returns></returns>
        public string checkinImage(string MacID, string OrderId, DateTime CheckinTime)
        {
            uploadpath = path + "upload/" + CheckinTime.ToShortDateString() + "/" + MacID + "/" + OrderId;
            imagepath = "upload/" + CheckinTime.ToShortDateString() + "/" + MacID + "/" + OrderId;
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
            return uploadpath + "/checkin.jpg" + "#" + imagepath + "/checkin.jpg";
        }
        /// <summary>
        /// 判断客户身份证图片文件夹是否存在并存盘
        /// </summary>
        /// <param name="MacID"></param>
        /// <param name="OrderId"></param>
        /// <param name="CheckinTime"></param>
        /// <param name="IdentitycardNumber"></param>
        /// <returns></returns>
        public string IDcardImage(string MacID, string OrderId, DateTime CheckinTime, string IdentitycardNumber) 
        {
            uploadpath = path + "upload/" + CheckinTime.ToShortDateString() + "/" + MacID + "/" + OrderId + "/" + IdentitycardNumber;
            imagepath = "upload/" + CheckinTime.ToShortDateString() + "/" + MacID + "/" + OrderId + "/" + IdentitycardNumber;
             if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
             return uploadpath + "/ID_" + IdentitycardNumber + ".jpg" + "#" + imagepath + "/ID_" + IdentitycardNumber + ".jpg";
        }
        /// <summary>
        /// 判断客户摄像头图片文件夹是否存在并存盘
        /// </summary>
        /// <param name="MacID"></param>
        /// <param name="OrderId"></param>
        /// <param name="CheckinTime"></param>
        /// <param name="IdentitycardNumber"></param>
        /// <returns></returns>
        public string CMcardImage(string MacID, string OrderId, DateTime CheckinTime, string IdentitycardNumber)
        {
            uploadpath = path + "upload/" + CheckinTime.ToShortDateString() + "/" + MacID + "/" + OrderId + "/" + IdentitycardNumber;
            imagepath = "upload/" + CheckinTime.ToShortDateString() + "/" + MacID + "/" + OrderId + "/" + IdentitycardNumber;
             if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
             return uploadpath + "/c_" + IdentitycardNumber + ".jpg" + "#" + imagepath + "/c_" + IdentitycardNumber + ".jpg";
        }
    }
}
