using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services.Description;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using System.Web.Script.Serialization;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.WebService.PmsService
{
    /// <summary>
    /// Summary description for PmsService
    /// </summary>
    public class PmsService : IHttpHandler
    {
        private static ILog log = LogManager.GetLogger("PmsService");
        readonly JavaScriptSerializer _jsonSerializer = new JavaScriptSerializer();
        readonly HotelCheckIn_Interface_Hardware.PMS.PmsService _pmsService = new HotelCheckIn_Interface_Hardware.PMS.PmsService(XmlHelper.ReadNode("pmsurl"));
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var action = context.Request.Params["action"];
            Type curType = GetType();
            MethodInfo method = curType.GetMethod(action, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(this, new object[] { HttpContext.Current });
            }
            else
            {
                context.Response.Write("没有这个方法！");
            }
        }

        /// <summary>
        /// 添加房间信息
        /// </summary>
        /// <param name="context"></param>
        public void AddRoom(HttpContext context)
        {
            var roomDal = new RoomDal();
            var baseData = new HotelCheckIn_Interface_Hardware.PMS.InvokeBaseData()
            {
                ClientInfo = "",
                Function = "706",//查询酒店房间列表
                Number = "",
                QueryString = ",,,,,,",
                State = "",
                Version = ""
            };
            var roomList = new List<RoomInfo>();
            try
            {
                var data = _pmsService.InvokeService(baseData);
                var result = _jsonSerializer.Deserialize<HotelCheckIn_Interface_Hardware.PMS.InvokeResultData>(data);
                if (result.R != "0")
                {
                    context.Response.Write("false");
                    return;
                }
                foreach (Dictionary<string, string> da in result.M)
                {
                    var room = new RoomInfo()
                    {
                        RoomNum = da["C01"],
                        //HotelId = da[""],//预留
                        RoomId = da["C00"],
                        RoomTypeId = da["C02"],
                        BuildingId = da["C03"],
                        FloorId = da["C04"]
                    };
                    roomList.Add(room);
                }
                roomDal.Del();
                roomDal.Add(roomList);
            }
            catch (Exception e)
            {
                log.Error("AddRoom出错：" + e);
                context.Response.Write("false");
                return;
            }
            context.Response.Write("true");
        }

        /// <summary>
        /// 点击获取房间属性数据
        /// </summary>
        /// <param name="context"></param>
        public void AddRoomQuality(HttpContext context)
        {
            var result = AddBuildings();
            if (!result)
            {
                context.Response.Write("false");
                return;
            }
            result = AddFloors();
            if (!result)
            {
                context.Response.Write("false");
                return;
            }
            result = AddApartment();
            if (!result)
            {
                context.Response.Write("false");
                return;
            }
            context.Response.Write("true");
        }

        /// <summary>
        /// 添加楼宇
        /// </summary>
        public bool AddBuildings()
        {
            var roomQualityDal = new RoomQualityDal();
            var baseData = new HotelCheckIn_Interface_Hardware.PMS.InvokeBaseData()
            {
                ClientInfo = "",
                Function = "703",//查询酒店楼宇列表
                Number = "",
                QueryString = ",,",
                State = "",
                Version = ""
            };
            try
            {
                var data = _pmsService.InvokeService(baseData);
                var result = _jsonSerializer.Deserialize<HotelCheckIn_Interface_Hardware.PMS.InvokeResultData>(data);
                if (result.R != "0")
                {
                    return false;
                }
                roomQualityDal.Del(2);
                foreach (Dictionary<string, string> da in result.M)
                {
                    var roomQuality = new RoomQualityInfo()
                    {
                        Id = da["C00"],
                        Name = da["C01"],
                        TypeId = "2"
                    };
                    roomQualityDal.Add(roomQuality);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return true;
        }

        /// <summary>
        /// 添加楼层
        /// </summary>
        public bool AddFloors()
        {
            var roomQualityDal = new RoomQualityDal();
            var baseData = new HotelCheckIn_Interface_Hardware.PMS.InvokeBaseData()
            {
                ClientInfo = "",
                Function = "704",//查询酒店楼层列表
                Number = "",
                QueryString = ",,,,",
                State = "",
                Version = ""
            };
            try
            {
                var data = _pmsService.InvokeService(baseData);
                var result = _jsonSerializer.Deserialize<HotelCheckIn_Interface_Hardware.PMS.InvokeResultData>(data);
                if (result.R != "0")
                {
                    return false;
                }
                roomQualityDal.Del(3);
                foreach (Dictionary<string, string> da in result.M)
                {
                    var roomQuality = new RoomQualityInfo()
                    {
                        Id = da["C00"],
                        Name = da["C02"],
                        TypeId = "3"
                    };
                    roomQualityDal.Add(roomQuality);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return true;
        }

        /// <summary>
        /// 添加户型
        /// </summary>
        public bool AddApartment()
        {
            var roomQualityDal = new RoomQualityDal();
            var baseData = new HotelCheckIn_Interface_Hardware.PMS.InvokeBaseData()
            {
                ClientInfo = "",
                Function = "705",//查询酒店户型列表
                Number = "",
                QueryString = ",,,,",
                State = "",
                Version = ""
            };
            try
            {
                var data = _pmsService.InvokeService(baseData);
                var result = _jsonSerializer.Deserialize<HotelCheckIn_Interface_Hardware.PMS.InvokeResultData>(data);
                if (result.R != "0")
                {
                    return false;
                }
                roomQualityDal.Del(1);
                foreach (Dictionary<string, string> da in result.M)
                {
                    var roomQuality = new RoomQualityInfo()
                    {
                        Id = da["C00"],
                        Name = da["C01"],
                        TypeId = "1"
                    };
                    roomQualityDal.Add(roomQuality);
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return true;
        }

        /// <summary>
        /// 查询多天房类资源
        /// </summary>
        /// <param name="context"></param>
        public void QueryMoredaysRoomCategoriesResources(HttpContext context)
        {
            
            var baseData = new HotelCheckIn_Interface_Hardware.PMS.InvokeBaseData()
            {
                ClientInfo = "",
                Function = "201",//查询酒店户型列表
                Number = "",
                QueryString = ",,,,",
                State = "",
                Version = ""
            };
            try
            {
                var data = _pmsService.InvokeService(baseData);
                var result = _jsonSerializer.Deserialize<HotelCheckIn_Interface_Hardware.PMS.InvokeResultData>(data);
                if (result.R != "0")
                {
                    return;
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}