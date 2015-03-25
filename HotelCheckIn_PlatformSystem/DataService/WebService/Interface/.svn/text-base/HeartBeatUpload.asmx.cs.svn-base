using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Model.Parameter;
using HotelCheckIn_PlatformSystem.DataService.Dal;
using CommonLibrary;
using log4net;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.Interface
{
    /// <summary>
    /// Summary description for HeartBeatUpload
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HeartBeatUpload : System.Web.Services.WebService
    {
        readonly string _timespan = System.Configuration.ConfigurationManager.AppSettings["heartbeatdt"];
        protected ILog Log = LogManager.GetLogger("HeartBeatUpload");
        /// <summary>
        /// 心跳上传接口
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        [WebMethod]
        public string IHeartBeat_Pt(HearBeatPara para)
        {
            bool data;
            var localUrl = string.Empty;
            var machineDal = new MachineDal();
            var heartbeatDal = new HeartBeatDal();
            var faultDal = new FaultDal();
            var translaction = new Transaction();
            //1:检测用户名和密码
            var jqid = para.MachineId;
            var password = para.PassWord;
            var falutid = para.FalutId;
            var status = para.Status;
            var url = para.Url;
            var nowdt = para.NowDt;
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(jqid) || string.IsNullOrEmpty(password))
            {
                throw new Exception("机器id或密码不能为空！");
            }
            data = machineDal.IsTimeOut(jqid, now, Convert.ToInt32(_timespan));
            if (!data)
            {
                throw new Exception("超过时间限制！");
            }
            data = machineDal.Exist(new Model.Machine { JqId = jqid, Password = password }, ref localUrl);
            if (!data)
            {
                Log.Debug("信息：未查找到机器id" + jqid);
                throw new Exception("用户名或密码不正确！");
            }


            var list = new List<object[]>();//记录object[]
            var sqls = new List<string>();//记录sql
            object[] objects = null;

            //添加心跳数据(添加一条记录)
            var sql = heartbeatDal.Add(new Heartbeat() { Id = Guid.NewGuid().ToString(), CreateDt = DateTime.Now, MachineId = jqid }, ref objects);
            sqls.Add(sql);
            list.Add(objects);

            //修改机器数据(添加心跳时间和机器状态)
            sql = machineDal.Modify(new Model.Machine() { JqId = jqid, HeartbeatDt = now, Status = status }, ref objects);
            sqls.Add(sql);
            list.Add(objects);

            if (!string.IsNullOrEmpty(falutid))
            {
                var falutidlist = falutid.Split('#').ToList();
                foreach (var fil in falutidlist)
                {
                    //添加故障数据
                    sql = faultDal.Add(new Model.Fault { Id = Guid.NewGuid().ToString(), MachineId = jqid, FaultId = fil, CreateDt = DateTime.Now }, ref objects);
                    sqls.Add(sql);
                    list.Add(objects);
                }

                //修改机器数据
                sql = machineDal.Modify(new Model.Machine { JqId = jqid, FaultId = falutid }, ref objects);
                sqls.Add(sql);
                list.Add(objects);
            }
            //执行事务
            data = translaction.Execute(sqls, list);
            if (data)
            {
                if (!url.Equals(localUrl))
                {
                    return localUrl;
                }
            }
            return url;
        }


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
