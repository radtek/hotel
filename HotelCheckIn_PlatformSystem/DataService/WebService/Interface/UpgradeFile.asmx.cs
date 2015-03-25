using System;
using System.Collections.Generic;
using System.Data;
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
    /// Summary description for UpgradeFile
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UpgradeFile : System.Web.Services.WebService
    {
        protected ILog Log = LogManager.GetLogger("UpgradeFile");
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 根据机器 id 和升级状态(isflag==0)来返回文件列表(文件名)List:string
        /// </summary>
        /// <param name="machineId"></param>
        [WebMethod]
        public List<string> IUpgradeFile_Pt(string machineId)
        {
            var upgrademachine = new UpgradeMachineDal();
            var list = new List<string>();
            try
            {
                var dt = upgrademachine.FindBy(machineId);
                list.AddRange(
                    from DataRow dataRow in dt.Rows
                    let filename = dataRow["FileName"].ToString()
                    let extension = dataRow["Extension"].ToString()
                    select filename + extension
                    );
            }
            catch (Exception e)
            {
                throw new Exception("出错" + e);
            }
            return list;
        }

        /// <summary>
        /// 根据机器id和文件列表(文件名)来更新isflag=1
        /// </summary>
        /// <param name="machineId"></param>
        /// <param name="list"></param>
        [WebMethod]
        public bool IUpdateState_Pt(string machineId, List<string> list)
        {
            var upgrademachinedal = new UpgradeMachineDal();
            var upgradefiledal = new UpgradeFileDal();
            try
            {
                var sid = string.Join("','", list.Select(s => upgradefiledal.QueryUpgradeId(s.Split('.')[0], "." + s.Split('.')[1])).ToArray());
                upgrademachinedal.Modify(machineId, sid, new Model.UpgradeMachine { IsFlag = 1 });
            }
            catch (Exception e)
            {
                throw new Exception("错误：" + e);
            }
            return true;
        }
    }
}
