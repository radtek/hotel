using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using CommonLibrary;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Bll;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using HotelCheckIn_BackSystem.DataService.Model.Parameter;
using log4net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Data;

namespace HotelCheckIn_BackSystem.DataService.WebService.Machine
{
    /// <summary>
    /// Summary description for MachineService
    /// </summary>
    public class MachineService : IHttpHandler, IRequiresSessionState
    {
        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Session[Constant.LoginUser] == null)
                {
                    context.Response.Write("location.href='../../Login.aspx';");
                }
            }
            catch (Exception)
            {
                context.Response.Write("location.href='../../Login.aspx';");
            }
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
        /// 初始化机器表数据
        /// </summary>
        /// <param name="context"></param>
        private void InitMachineGrid(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<InitMachineGrid>();
            var machinedal = new MachineDal();
            var jdid = context.Request.Params["jdid"];//酒店id
            var qyid = context.Request.Params["qyid"];//区域id
            var page = context.Request.Params["page"];
            var rows = context.Request.Params["rows"];
            var mypage = 0;
            var mysize = 0;
            var count = 0;
            if (!string.IsNullOrEmpty(page))
                mypage = int.Parse(page);
            if (!string.IsNullOrEmpty(rows))
                mysize = int.Parse(rows);
            try
            {

                var dt = machinedal.QueryByPage(qyid, jdid, mypage, mysize, ref count);
                var list = ConvertHelper<InitMachineGrid>.ConvertToList(dt);
                json.Rows = list;
                json.Total = count;
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            if (strjson != null) context.Response.Write(strjson);
        }

        /// <summary>
        /// 从码表里获取区域数据
        /// </summary>
        /// <param name="context"></param>
        private void GetAreaData(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Code>();
            var codeDal = new CodeDal();
            var type = context.Request.Params["type"];
            try
            {
                var dt = codeDal.FindByArea();
                var list = ConvertHelper<Code>.ConvertToList(dt);
                json.Rows = list;
                json.IsSuccess = true;
                json.Msg = "获取成功";
                switch (type)
                {
                    case "1":
                        json.Other = "1";
                        break;
                    case "2":
                        json.Other = "2";
                        break;
                }
                json.JsExecuteMethod = "ajax_GetAreaData";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                Log.Debug("错误信息：" + e);
                throw;
            }

            context.Response.Write(strjson);
        }

        /// <summary>
        /// 根据机器id修改机器数据(素材url)
        /// </summary>
        /// <param name="context"></param>
        private void ModifyMachineByJqid(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Type>();
            var machinedal = new MachineDal();
            var jqid = context.Request.Params["jqid"];
            var materialurl = context.Request.Params["materialurl"];
            var jqids = jqid.Split('&');
            try
            {
                foreach (string s in jqids)
                {
                    machinedal.Modify(new Model.MachineInfo() { JqId = s, MaterialUrl = materialurl });
                }
                json.IsSuccess = true;
                json.Msg = "修改成功！";
                json.JsExecuteMethod = "ajax_ModifyMachineByJqid";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                Log.Debug("错误信息：" + e);
                throw;
            }

            context.Response.Write(strjson);
        }

        /// <summary>
        /// 新增/编辑机器
        /// </summary>
        /// <param name="context"></param>
        private void AddAndEditMachine(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<string>();
            var machinedal = new MachineDal();
            var flag = context.Request.Params["flag"];
            var hotelid = context.Request.Params["selHotels2"];
            var jqid = context.Request.Params["txt_jqid"];
            var name = context.Request.Params["txt_name"];
            var pass = context.Request.Params["txt_pass"];
            var ip = context.Request.Params["txt_ip"];
            var isdisabled = context.Request.Params["txt_disabled"];
            var note = context.Request.Params["txt_note"];
            try
            {
                var emp = (context.Session[Constant.LoginUser]) as Employer;
                switch (flag)
                {
                    case "add":
                        var data = machinedal.Exist(new Model.MachineInfo() { JqId = jqid });
                        if (!data)
                        {

                            machinedal.Add(new Model.MachineInfo()
                            {
                                JqId = jqid,
                                HotelId = hotelid,
                                Name = name,
                                IP = ip,
                                Password = pass,
                                Isdisabled = int.Parse(isdisabled),
                                Note = note,
                                CreateDt = DateTime.Now,
                                Creater = emp.Name,
                                UpdateDt = DateTime.Now,
                                UpdatePerson = emp.Name,
                                HeartbeatDt = DateTime.Now
                            });
                            json.Msg = "添加成功";
                            json.IsSuccess = true;
                        }
                        else
                        {
                            json.Msg = "已经存在相同机器！";
                            json.IsSuccess = false;

                        }
                        strjson = _jss.Serialize(json);
                        context.Response.Write(strjson);
                        break;
                    case "edit":
                        machinedal.Modify(new Model.MachineInfo()
                        {
                            JqId = jqid,
                            HotelId = hotelid,
                            Name = name,
                            IP = ip,
                            Password = pass,
                            Isdisabled = int.Parse(isdisabled),
                            Note = note,
                            UpdateDt = DateTime.Now,
                            UpdatePerson = emp.Name,
                            HeartbeatDt = DateTime.Now
                        });
                        json.Msg = "修改成功";
                        json.IsSuccess = true;
                        strjson = _jss.Serialize(json);
                        context.Response.Write(strjson);
                        break;
                }

            }
            catch (Exception e)
            {
                json.Msg = "添加失败！";
                json.IsSuccess = false;
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
                Log.Error(e);
            }
        }

        /// <summary>
        /// 删除机器
        /// </summary>
        /// <param name="context"></param>
        private void DelMachine(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<Type>();
            var machinedal = new MachineDal();
            var jqid = context.Request.Params["jqid"];
            var jqids = jqid.Split('&');
            try
            {
                foreach (string s in jqids)
                {
                    machinedal.Del(new Model.MachineInfo() { JqId = s });
                }

                json.IsSuccess = true;
                json.Msg = "删除成功！";
                json.JsExecuteMethod = "ajax_DelMachine";
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
            }
            catch (Exception e)
            {
                json.IsSuccess = true;
                json.Msg = "删除失败！";
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
                Log.Debug("错误信息：" + e);
                throw;
            }
        }

        /// <summary>
        /// 根据机器id获取机器数据
        /// </summary>
        /// <param name="context"></param>
        private void GetMachine(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<GetMachine>();
            var machinedal = new MachineDal();
            var jqid = context.Request.Params["jqid"];
            try
            {
                var dt = machinedal.GetMachine(jqid);
                var list = ConvertHelper<GetMachine>.ConvertToList(dt);
                json.Rows = list;
                json.IsSuccess = true;
                json.Msg = "获取成功";
                json.JsExecuteMethod = "ajax_GetMachine";
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
            }
            catch (Exception)
            {
                json.IsSuccess = true;
                json.Msg = "获取失败";
                strjson = _jss.Serialize(json);
                context.Response.Write(strjson);
                throw;
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