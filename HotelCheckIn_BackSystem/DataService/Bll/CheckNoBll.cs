using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Common;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Bll
{

    public class CheckNoBll
    {
        private static ILog _log = log4net.LogManager.GetLogger("CheckNoBll");
        readonly CheckNoDal _ckdal = new CheckNoDal();
        private string basePath = System.Configuration.ConfigurationSettings.AppSettings["basePath"];
        public CheckNoBll()
        { }
        /// <summary>
        /// 验证码验证
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryCheckNoAndPj(CheckNoInfo bean)
        {
            return _ckdal.QueryCheckNoAndPj(bean);
        }

        /// <summary>
        /// 根据指定条件查询验证码数据
        /// </summary>
        /// <param name="qnpj">查询条件实体对象</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public List<CheckNoInfo> QueryCheckNo(QueryNoAndPj qnpj, int page, int rows, ref int total)
        {
            qnpj.CreateTimeEnd = qnpj.CreateTimeEnd.AddDays(1);
            total = _ckdal.QueryCheckNoRows(qnpj);
            var ckList = new List<CheckNoInfo>();

            DataTable dt = _ckdal.QueryCheckNo(qnpj, page, rows);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var ckIn = new CheckNoInfo();
                    DataRow dr = dt.Rows[i];
                    ckIn.CheckId = dr["CheckID"].ToString();
                    ckIn.CheckID_Front = dr["CheckID_Front"].ToString();
                    ckIn.InternetGroupId = dr["InternetGroupId"].ToString();
                    ckIn.InternetGroup = dr["InternetGroup"].ToString();
                    if (dr["CheckIdBeginTime"].ToString() == "" || dr["CheckIdBeginTime"] == null)
                        ckIn.CheckIdBeginTime = new DateTime();
                    else
                        ckIn.CheckIdBeginTime = DateTime.Parse(dr["CheckIdBeginTime"].ToString());
                    if (dr["CheckIdEndTime"].ToString() == "" || dr["CheckIdEndTime"] == null)
                        ckIn.CheckIdEndTime = new DateTime();
                    else
                        ckIn.CheckIdEndTime = DateTime.Parse(dr["CheckIdEndTime"].ToString());
                    if (dr["CreateDateTime"].ToString() == "" || dr["CreateDateTime"] == null)
                        ckIn.CreateDateTime = new DateTime();
                    else
                        ckIn.CreateDateTime = DateTime.Parse(dr["CreateDateTime"].ToString());
                    ckIn.CreatePeople = dr["CreatePeople"].ToString();
                    ckIn.MachineCheck = dr["MachineCheck"].ToString() == "" ? 0 : int.Parse(dr["MachineCheck"].ToString());
                    if (dr["MachineCheckDateTime"].ToString() == "" || dr["MachineCheckDateTime"] == null)
                        ckIn.MachineCheckDateTime = new DateTime();
                    else
                        ckIn.MachineCheckDateTime = DateTime.Parse(dr["MachineCheckDateTime"].ToString());
                    ckIn.MachineCheckPeople = dr["MachineCheckPeople"].ToString() == "" ? 0 : int.Parse(dr["MachineCheckPeople"].ToString());
                    ckIn.InternetCheck = dr["InternetCheck"].ToString() == "" ? 0 : int.Parse(dr["InternetCheck"].ToString());
                    if (dr["InternetCheckDateTime"].ToString() == "" || dr["InternetCheckDateTime"] == null)
                        ckIn.InternetCheckDateTime = new DateTime();
                    else
                        ckIn.InternetCheckDateTime = DateTime.Parse(dr["InternetCheckDateTime"].ToString());
                    ckIn.InternetCheckPeople = dr["InternetCheckPeople"].ToString();
                    ckIn.InSumDate = dr["InSumDate"].ToString() == "" ? 0 : int.Parse(dr["InSumDate"].ToString());
                    ckList.Add(ckIn);
                }
            }
            return ckList;
        }
        /// <summary>
        /// 添加验证码
        /// </summary>
        /// <param name="bean"></param>
        public void AddCheckNo(CheckNoInfo bean)
        {
            _ckdal.AddCheckNo(bean);
        }
        public DataTable QueryCheckNo(CheckNoInfo bean)
        {
            return _ckdal.QueryCheckNo(bean);
        }

        public DataTable QueryCheckNoIsKnock(CheckNoInfo bean)
        {
            return _ckdal.QueryCheckNoIsKnock(bean);
        }
        /// <summary>
        /// 导出验证码
        /// </summary>
        /// <returns></returns>
        public string ExpAll(string tgs, string xmmc, int machinecheck,int groupcheck,string dcyzmsl)
        {
            DataTable dtcheckno = _ckdal.ExpQueryNo(tgs, xmmc, machinecheck,groupcheck,dcyzmsl);

            if (null == dtcheckno || dtcheckno.Rows.Count < 1)
            {
                throw new Exception("无符合条件的数据。");
            }
            lock (this)
            {
                DataTable dt = dtcheckno;
                string excelFileName = basePath + "checkno.xls";
                //向文件夹中添加订单信息文件，
                dtcheckno.Columns.Remove("CheckID_Front");
                dtcheckno.Columns.Remove("InternetGroupId");
                dtcheckno.Columns.Remove("InternetGroup");
                dtcheckno.Columns.Remove("CheckIdBeginTime");
                dtcheckno.Columns.Remove("CheckIdEndTime");
                dtcheckno.Columns.Remove("CreateDateTime");
                dtcheckno.Columns.Remove("CreatePeople");
                dtcheckno.Columns.Remove("MachineCheck");
                dtcheckno.Columns.Remove("MachineCheckDateTime");
                dtcheckno.Columns.Remove("MachineCheckPeople");
                dtcheckno.Columns.Remove("InternetCheck");
                dtcheckno.Columns.Remove("InternetCheckDateTime");
                dtcheckno.Columns.Remove("InternetCheckPeople");
                dtcheckno.Columns.Remove("InSumDate");
                var titles = new string[] { "验证码" };
                var nh = new NPOIHelper(titles, dt);
                var ms = (MemoryStream)nh.ToExcel();
                File.WriteAllBytes(excelFileName, ms.ToArray());
                return excelFileName;
            }
        }

        /// <summary>
        /// 根据id查询验证码
        /// </summary>
        /// <param name="checkid"></param>
        /// <returns></returns>
        public DataTable QueryCheckId(string checkid)
        {
            return _ckdal.QueryCheckId(checkid);
        }

        /// <summary>
        /// 根据验证码前缀查询验证码
        /// </summary>
        /// <param name="checkidfront"></param>
        /// <returns></returns>
        public DataTable QueryNoByFront(string checkidfront)
        {
            return _ckdal.QueryNoByFront(checkidfront);
        }

        /// <summary>
        /// 团购验证—修改团购验证状态,验证时间，验证人
        /// </summary>
        /// <param name="bean"></param>
        public void EditCheckNoIntnetCheck(CheckNoInfo bean)
        {
            _ckdal.EditCheckNoIntnetCheck(bean);
        }

        /// <summary>
        /// 修改验证码的验证状态
        /// </summary>
        /// <param name="bean"></param>
        public void Modify(CheckNoInfo bean)
        {
            _ckdal.Modify(bean);
        }

        /// <summary>
        /// 导出查询未验证验证码的数量
        /// </summary>
        /// <param name="tgs"></param>
        /// <param name="xmmc"></param>
        /// <param name="machinecheck"></param>
        /// <param name="groupcheck"></param>
        /// <returns></returns>
        public DataTable GetCheckIdNo(string tgs, string xmmc, int machinecheck, int groupcheck)
        {
            return _ckdal.GetCheckIdNo(tgs, xmmc, machinecheck, groupcheck);
        }
    }

}