using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Bll
{
    public class InternetGroupBll
    {
        private static ILog log = log4net.LogManager.GetLogger("InternetGroupBll");
        readonly InternetGroupDal _intGpDal = new InternetGroupDal();
        public InternetGroupBll()
        { }

        /// <summary>
        /// 添加团购项目
        /// </summary>
        /// <param name="bean"></param>
        public void AddProject(InternetGroupInfo bean)
        {
            _intGpDal.AddProject(bean);
        }

        /// <summary>
        /// 删除团购项目
        /// </summary>
        /// <param name="qzm"></param>
        public void DelProject(string qzm,string tgsid)
        {
            _intGpDal.DelProject(qzm,tgsid);
        }

        /// <summary>
        /// 修改团购项目
        /// </summary>
        /// <param name="bean"></param>
        public void EditProject(InternetGroupInfo bean)
        {
            _intGpDal.EditProject(bean);
        }

        /// <summary>
        /// 条件查询团购项目
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryProject(InternetGroupInfo bean)
        {
            return _intGpDal.QueryProjectBean(bean);
        }


        /// <summary>
        /// 查询团购项目list
        /// </summary>
        /// <param name="bean"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public List<InternetGroupInfo> QueryProjectList(InternetGroupInfo bean, int page, int rows, ref int total)
        {
            total = _intGpDal.QueryProjectRows(bean);
            var ckList = new List<InternetGroupInfo>();

            DataTable dt = _intGpDal.QueryProject(bean,page,rows);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var intgroup = new InternetGroupInfo();
                    DataRow dr = dt.Rows[i];
                    intgroup.ProjectFrontNum = dr["ProjectFrontNum"].ToString();
                    intgroup.InternetGroupId = dr["InternetGroupId"].ToString();
                    intgroup.InternetGroup = dr["InternetGroup"].ToString();
                    intgroup.ProjectName = dr["ProjectName"].ToString();
                    intgroup.RoomTypeId = dr["RoomTypeId"].ToString();
                    intgroup.RoomTypeName = dr["RoomTypeName"].ToString();
                    intgroup.Remarks = dr["Remarks"].ToString();
                    intgroup.HotelId = dr["HotelId"].ToString();
                    if (dr["UpdateDt"].ToString() == "" || dr["UpdateDt"] == null)
                        intgroup.UpdateDt = new DateTime();
                    else
                        intgroup.UpdateDt = DateTime.Parse(dr["UpdateDt"].ToString());
                    intgroup.Updater = dr["Updater"].ToString();
                    intgroup.Creater = dr["Creater"].ToString();
                    if (dr["CreateDt"].ToString() == "" || dr["CreateDt"] == null)
                        intgroup.CreateDt = new DateTime();
                    else
                        intgroup.CreateDt = DateTime.Parse(dr["CreateDt"].ToString());
                    intgroup.RateCode = dr["RateCode"].ToString();
                    intgroup.Rate = dr["Rate"].ToString() == "" ? 0 : float.Parse(dr["Rate"].ToString());
                    ckList.Add(intgroup);
                }
            }
            return ckList;
        }

        /// <summary>
        /// 查询房间属性
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryRoomQuality(RoomQuality bean)
        {
            return _intGpDal.QueryRoomQuality(bean);
        }
    }
}