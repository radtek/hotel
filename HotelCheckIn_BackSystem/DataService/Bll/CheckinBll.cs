using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using HotelCheckIn_BackSystem.DataService.Common;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Bll
{
    public class CheckinBll
    {
        private static ILog log = log4net.LogManager.GetLogger("CheckinBll");
        private CheckinDal checkindal = new CheckinDal();
        private string basePath = System.Configuration.ConfigurationSettings.AppSettings["basePath"];
        public CheckinBll()
        { }

        /// <summary>
        /// 保存checkin订单信息
        /// </summary>
        /// <param name="checkin"></param>
        /// <param name="customList"></param>
        /// <returns></returns>
        public bool UploadCheckin(CheckinInfo checkin, List<CustomerInfo> customList)
        {
            log.Debug("checkin信息：CheckinTime" + checkin.CheckinTime + "#Orderid" + checkin.OrderId + "#Name" + checkin.MacName + "#VIPcardNum" + checkin.ViPcardNum
                + "#RoomRate" + checkin.RoomRate + "#ProcessTime" + checkin.OrderTime + "#SettleAccount" + checkin.AdvancePayment + "#RoomRateCode" + checkin.RoomCode);
            try
            {
                return this.checkindal.UploadCheckinOrder(checkin, customList);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据指定条件查询数据
        /// </summary>
        /// <param name="ckInfo">查询条件实体对象</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public List<CheckinInfo> Query(CheckinQuery ckInfo, int page, int rows, ref int total)
        {
            ckInfo.CheckinTimeEnd = ckInfo.CheckinTimeEnd.AddDays(1);
            total = checkindal.GetCheckinRowsCount(ckInfo);
            List<CheckinInfo> ckList = new List<CheckinInfo>();

            DataTable dt = checkindal.Query(ckInfo, page, rows);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckinInfo ckIn = new CheckinInfo();
                    DataRow dr = dt.Rows[i];
                    ckIn.OrderId = dr["OrderId"].ToString();
                    ckIn.RoomNum = dr["RoomNum"].ToString();
                    ckIn.RoomType = dr["RoomType"].ToString();
                    ckIn.Building = dr["Building"].ToString();
                    ckIn.RoomCode = dr["RoomCode"].ToString();
                    ckIn.RoomRate = dr["RoomRate"].ToString() == "" ? 0 : float.Parse(dr["RoomRate"].ToString());
                    ckIn.CheckinType = dr["CheckinType"].ToString();
                    ckIn.ViPcardNum = dr["ViPcardNum"].ToString();
                    ckIn.ViPcardType = dr["ViPcardType"].ToString();
                    ckIn.PeopleNum = dr["PeopleNum"].ToString() == "" ? 0 : int.Parse(dr["PeopleNum"].ToString());
                    ckIn.CheckinTime = DateTime.Parse(dr["checkintime"].ToString());
                    ckIn.Hours = dr["hours"].ToString() == "" ? 0 : int.Parse(dr["hours"].ToString());
                    ckIn.AdvancePayment = dr["AdvancePayment"].ToString() == "" ? 0 : float.Parse(dr["AdvancePayment"].ToString());
                    ckIn.AdvanceType = dr["AdvanceType"].ToString();
                    ckIn.MacId = dr["MacId"].ToString();
                    ckIn.MacName = dr["macname"].ToString();
                    ckIn.OrderTime = DateTime.Parse(dr["OrderTime"].ToString());
                    ckIn.InternetGroup = dr["InternetGroup"].ToString();
                    ckIn.CheckId = dr["CheckId"].ToString();
                    ckList.Add(ckIn);
                }
            }
            return ckList;
        }

        /// <summary>
        /// 导出符合条件的身份证图片,并记录下载次数
        /// </summary>
        /// <param name="ckinInfo">查询条件</param>
        /// <returns>图片压缩包字节流</returns>
        public string expIDCardImg(CheckinQuery ckinInfo, int page, int rows)
        {
            //符合条件（机器ID，起始日期，结束日期，是否导出）的图片，
            /*
             * 代码逻辑：
             * 
             * 1.先找到所有身份证图片路径，
             * 2.读取所有文件到压缩流中，将压缩流保存为文件，
             * 3.返回文件名称，供ashx发送到客户端
             * 
             * */
            
            ckinInfo.CheckinTimeEnd = ckinInfo.CheckinTimeEnd.AddDays(1);
            DataTable dt = checkindal.Query(ckinInfo);
            if (null == dt || dt.Rows.Count < 1)
            {
                throw new Exception("无符合条件的数据。");
            }
            lock (this)
            {
                List<string> orderIDList = new List<string>();
                List<string> IDimagePath = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    orderIDList.Add(dr["orderid"].ToString());
                    IDimagePath.Add(dr["identitycardphoto"].ToString());
                }
                ZipClass zip = new ZipClass();
                string zipFileName = basePath + "zipfile.zip";
                File.WriteAllBytes(zipFileName, ZipClass.Zip(basePath, IDimagePath));
                //向数据库(客人表)添加下载次数
                checkindal.AddDoenloadCount(orderIDList.ToArray());
                return zipFileName;
            }
        }

        public string ExpAllImg(CheckinQuery ckinInfo, int page, int rows)
        {
            //符合条件（机器ID，起始日期，结束日期，是否导出）的图片（身份证，摄像头，checkin签名，checkout签名）、订单数据，
            /*
             * 代码逻辑：
             * 
             * 1.先找到所有图片（身份证，摄像头，checkin签名）的路径，
             * 2.把图片读取到压缩流中，
             * 4.创建导出表格，并存放在临时目录，
             * 5.打包压缩临时目录，并发送到客户端
             * 
             * */

            ckinInfo.CheckinTimeEnd = ckinInfo.CheckinTimeEnd.AddDays(1);
            DataTable dsCheckin = checkindal.Query(ckinInfo, page, rows);
            //查找并保存checkin签名
            List<string> ckinOrderId = new List<string>();
            List<string> imgPath = new List<string>();
            if (null != dsCheckin && dsCheckin.Rows.Count > 0)
            {
                for (int i = 0; i < dsCheckin.Rows.Count; i++)
                {
                    ckinOrderId.Add(dsCheckin.Rows[i]["orderid"].ToString());
                    imgPath.Add(dsCheckin.Rows[i]["checkinimage"].ToString());
                }
            }
            DataTable dsCustom = checkindal.Query(ckinInfo);
            if (null == dsCheckin || dsCheckin.Rows.Count < 1 || dsCustom.Rows.Count < 1)
            {
                throw new Exception("无符合条件的数据。");
            }
            lock (this)
            {
                DataTable dt = dsCustom;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    imgPath.Add(dr["identitycardphoto"].ToString());
                    imgPath.Add(dr["cameraphoto"].ToString());
                }
                ZipClass zip = new ZipClass();
                string zipFileName = basePath + "zipfile.zip";
                //向文件夹中添加订单信息文件，
                dsCheckin.Columns.Remove("id");
                dsCheckin.Columns.Remove("checkinimage");
                dsCheckin.Columns.Remove("exportcount");
                dsCustom.Columns.Remove("id");
                dsCustom.Columns.Remove("identitycardphoto");
                dsCustom.Columns.Remove("cameraphoto");
                dsCustom.Columns.Remove("exportcount");
                dsCustom.Columns.Remove("checkidcard");
                string[][] titles = new string[2][];
                titles[0] = new string[] {"订单ID","房间号","房间类型","楼栋","房价代码","房价","入住类型","会员卡号","会员卡类型",
                    "入住人数","入住日期","钟点数","预付金额","预付方式","入住签名","终端名称","处理时间", "团购商", "验证码" };
                titles[1] = new string[] { "订单ID", "姓名", "性别", "身份证号" };
                MemoryStream ms = NPOIHelper.ToExcel2Sheet(titles, dsCheckin, dt) as MemoryStream;
                File.WriteAllBytes(zipFileName, ZipClass.Zip(basePath, imgPath, ms.ToArray()));
                //向订单表中添加下载次数，客人信息表中添加下载次数。
                checkindal.AddDownloadCount(ckinOrderId.ToArray());
                checkindal.AddDoenloadCount(ckinOrderId.ToArray());
                return zipFileName;
            }
        }

        /// <summary>
        /// 复制图片文件到临时文件夹
        /// </summary>
        /// <param name="BasePath">基本文件夹</param>
        /// <param name="FilePath">源文件路径</param>
        /// <param name="TmpDir">目标文件夹</param>
        private void CopyFileToTmpDir(string BasePath, string FilePath, string TmpDir)
        {
            if (File.Exists(BasePath + FilePath))
            {
                string TmpFile = TmpDir + FilePath;
                string TmpDirSon = TmpFile.Substring(0, TmpFile.LastIndexOf('/'));
                if (!Directory.Exists(TmpDirSon))
                {
                    Directory.CreateDirectory(TmpDirSon);
                }
                File.Copy(BasePath + FilePath, TmpFile);
            }
            else
            {
                log.Error("图片路径：" + BasePath + FilePath + "不存在。");
            }
        }

        /// <summary>
        /// 获取符合条件的实体
        /// </summary>
        /// <param name="ckinInfo"></param>
        /// <returns></returns>
        public CheckinInfo GetModule(CheckinInfo ckinInfo)
        {
            return checkindal.GetModule(ckinInfo);
        }

        //查询终端信息
        public DataTable Query(MachineInfo macInfo)
        {
            log.Debug("查询终端信息：" + macInfo);
            try
            {
                return checkindal.Query(macInfo);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw new Exception("查询终端信息出错！");
            }

        }

        /// <summary>
        /// 根据实体类的条件查询数据
        /// </summary>
        /// <param name="cInfo">条件</param>
        /// <returns></returns>
        public List<CustomerInfo> Query(CustomerInfo cInfo)
        {
            DataTable dt = checkindal.Query(cInfo);
            List<CustomerInfo> cList = null;
            if (null != dt && dt.Rows.Count > 0)
            {
                cList = new List<CustomerInfo>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    cInfo = new CustomerInfo
                        {
                            Id = dr["id"].ToString(),
                            OrderId = dr["orderid"].ToString(),
                            Name = dr["name"].ToString(),
                            Sex = dr["sex"].ToString(),
                            IdentityCardNum = dr["identitycardnum"].ToString(),
                            IdentityCardPhoto = dr["identitycardphoto"].ToString(),
                            CameraPhoto = dr["cameraphoto"].ToString(),
                            ExportCount = string.IsNullOrEmpty(dr["ExportCount"].ToString())? 0 : int.Parse(dr["ExportCount"].ToString()),
                            CheckIDcard = string.IsNullOrEmpty(dr["CheckIDcard"].ToString()) ? 0 : int.Parse(dr["CheckIDcard"].ToString())                            
                        };
                    cList.Add(cInfo);
                }
            }
            return cList;
        }


    }
}
