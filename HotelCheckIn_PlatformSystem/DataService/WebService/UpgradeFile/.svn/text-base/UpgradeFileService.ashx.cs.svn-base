using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using CommonLibrary;
using HotelCheckIn_PlatformSystem.DataService.BLL;
using HotelCheckIn_PlatformSystem.DataService.Bll;
using HotelCheckIn_PlatformSystem.DataService.Dal;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Model.Parameter;
using log4net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Data;

namespace HotelCheckIn_PlatformSystem.DataService.WebService.UpgradeFile
{
    /// <summary>
    /// Summary description for UpgradeFileService
    /// </summary>
    public class UpgradeFileService : IHttpHandler, IRequiresSessionState
    {

        protected ILog Log = LogManager.GetLogger("MachineService");
        readonly JavaScriptSerializer _jss = new JavaScriptSerializer();
        readonly string _filepath = System.Configuration.ConfigurationManager.AppSettings["upgradefilepath"];
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
        /// 初始化升级文件表
        /// </summary>
        /// <param name="context"></param>
        private void InitUpgradeFile(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<InitUpgradeFile>();
            var upgraderfiledal = new UpgradeFileDal();
            var datapage = new DataPage();
            var page = context.Request.Params["page"];
            var rows = context.Request.Params["rows"];

            if (!string.IsNullOrEmpty(page))
                datapage.Index = int.Parse(page);
            if (!string.IsNullOrEmpty(rows))
                datapage.Count = int.Parse(rows);
            try
            {
                var dic = upgraderfiledal.QueryUpgradeFile(datapage);
                foreach (var dataTable in dic)
                {
                    var list = ConvertHelper<InitUpgradeFile>.ConvertToList(dataTable.Value);
                    json.Rows = list;
                    json.Total = dataTable.Key;
                }
                json.IsSuccess = true;
                json.Msg = "查询成功";
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "查询失败" + e;
                strjson = _jss.Serialize(json);
            }
            context.Response.Write(strjson);
        }

        /// <summary>
        /// 保存升级文件
        /// </summary>
        /// <param name="context"></param>
        private void SaveUpgradeFile(HttpContext context)
        {
            var upgradefiledal = new UpgradeFileDal();
            var upgradefile = new Model.UpgradeFile();
            var upgrademachinedal = new UpgradeMachineDal();
            HttpPostedFile file = context.Request.Files["Filedata"];
            if (file == null) return;
            var pathAll = _filepath + file.FileName;
            var size = file.ContentLength / 1024;
            var flag = file.ContentLength % 1024;
            if (flag > 0)
            {
                size++;
            }
            upgradefile.Size = size.ToString();
            upgradefile.FileName = file.FileName.Split('.')[0];
            upgradefile.Extension = "." + file.FileName.Split('.')[1];
            upgradefile.Type = file.FileName.Split('.')[1];
            upgradefile.Url = pathAll;
            try
            {
                if (File.Exists(pathAll))//已存在相同文件(更新升级文件表)
                {
                    upgradefiledal.Modify(upgradefile);
                    var id = upgradefiledal.QueryUpgradeId(upgradefile.FileName);
                    //更新升级机器表里的isflag=0字段
                    upgrademachinedal.ModifyByUpgradeFileId(new Model.UpgradeMachine()
                        {
                            IsFlag = 0,
                            UpgradeFileId = id
                        });
                }
                else//不存在则向升级文件表中插入数据
                {
                    upgradefile.Id = Guid.NewGuid().ToString();
                    upgradefile.CreateDt = DateTime.Now;
                    upgradefiledal.Add(upgradefile);
                }
                file.SaveAs(pathAll);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除升级文件
        /// </summary>
        /// <param name="context"></param>
        private void DelUpgradeFile(HttpContext context)
        {
            string strjson = null;
            var upgradeid = context.Request.Params["id"];//升级文件id
            var file = context.Request.Params["file"];//升级文件(如test.htm)
            var filePath = _filepath + file;
            var json = new ZwJson<Type>();
            var upgradefiledal = new UpgradeFileDal();
            var upgrademachinedal = new UpgradeMachineDal();
            try
            {
                upgradefiledal.Del(new Model.UpgradeFile() { Id = upgradeid });
                upgrademachinedal.DelByUpgradeId(new Model.UpgradeMachine() { UpgradeFileId = upgradeid });//联动删除，删除升级文件表的同时，删除升级机器表里的数据
                File.Delete(filePath);
                json.IsSuccess = true;
                json.Msg = "删除成功";
                json.JsExecuteMethod = "ajax_DelUpgradeFile";
                strjson = _jss.Serialize(json);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            if (strjson != null)
                context.Response.Write(strjson);
        }

        /// <summary>
        /// 获取升级文件
        /// </summary>
        /// <param name="context"></param>
        private void GetUpgradeFile(HttpContext context)
        {
            string strjson = null;
            var json = new ZwJson<GetUpgradeFile>();
            var upgraderfiledal = new UpgradeFileDal();
            var jqid = context.Request.Params["jqid"];
            try
            {
                var dt = upgraderfiledal.QueryUpgradeFile(jqid);
                var list = ConvertHelper<GetUpgradeFile>.ConvertToList(dt);
                json.Rows = list;
                json.IsSuccess = true;
                json.Msg = "查询成功";
                strjson = _jss.Serialize(json);
                strjson = strjson.Replace("Total", "total");
                strjson = strjson.Replace("Rows", "rows");
            }
            catch (Exception e)
            {
                json.IsSuccess = false;
                json.Msg = "查询失败" + e;
                strjson = _jss.Serialize(json);
            }
            context.Response.Write(strjson);
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