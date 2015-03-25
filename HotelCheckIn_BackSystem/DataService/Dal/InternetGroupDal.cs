using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class InternetGroupDal : BaseDal<InternetGroupInfo>
    {
        private static ILog log = log4net.LogManager.GetLogger("InternetGroupDal");
        public InternetGroupDal()
        { }

        /// <summary>
        /// 添加团购项目
        /// </summary>
        /// <param name="bean"></param>
        public void AddProject(InternetGroupInfo bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql1.Append(" HotelId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.ProjectFrontNum))
            {
                sql1.Append(" ProjectFrontNum,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.ProjectFrontNum);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql1.Append(" InternetGroupId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.InternetGroupId);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroup))
            {
                sql1.Append(" InternetGroup,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.InternetGroup);
            }
            if (!string.IsNullOrEmpty(bean.ProjectName))
            {
                sql1.Append(" ProjectName,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.ProjectName);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeId))
            {
                sql1.Append(" RoomTypeId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.RoomTypeId);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeName))
            {
                sql1.Append(" RoomTypeName,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.RoomTypeName);
            }
            if (!string.IsNullOrEmpty(bean.Creater))
            {
                sql1.Append(" Creater,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Creater);
            }
            if (!string.IsNullOrEmpty(bean.Remarks))
            {
                sql1.Append(" Remarks,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Remarks);
            }
            if (bean.Rate!=0.0f)
            {
                sql1.Append(" Rate,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Rate);
            }
            if (!string.IsNullOrEmpty(bean.RateCode))
            {
                sql1.Append(" RateCode,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.RateCode);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Project(" + sql1 + ",CreateDt) values(" + sql2 + ",now())";
            mso.Execute(sql, list.ToArray());
        }

        /// <summary>
        /// 删除团购项目
        /// </summary>
        /// <param name="qzm"></param>
        public void DelProject(string qzm, string tgsid)
        {
            var strSql = new StringBuilder();
            strSql.Append(" delete from t_Project where ProjectFrontNum={0} and InternetGroupId={1} ");
            var parameters = new object[] {
                qzm,tgsid
            };
            try
            {
                mso.Execute(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                Log.Error("删除团购项目失败！", e);
            }
        }
        /// <summary>
        /// 修改团购项目
        /// </summary>
        /// <param name="bean"></param>
        public void EditProject(InternetGroupInfo bean)
        {
            Log.Debug("Modify方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            sql.Append(" update t_Project set ");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql.Append(" HotelId={" + i++ + "},");
                dList.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.ProjectName))
            {
                sql.Append(" ProjectName={" + i++ + "},");
                dList.Add(bean.ProjectName);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeId))
            {
                sql.Append(" RoomTypeId={" + i++ + "},");
                dList.Add(bean.RoomTypeId);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeName))
            {
                sql.Append(" RoomTypeName={" + i++ + "},");
                dList.Add(bean.RoomTypeName);
            }
            if (!string.IsNullOrEmpty(bean.Updater))
            {
                sql.Append(" Updater={" + i++ + "},");
                dList.Add(bean.Updater);
            }
            if (!string.IsNullOrEmpty(bean.Updater))
            {
                sql.Append(" Updater={" + i++ + "},");
                dList.Add(bean.Updater);
            }
            if (bean.Rate!=0.0f)
            {
                sql.Append(" Rate={" + i++ + "},");
                dList.Add(bean.Rate);
            }
            if (!string.IsNullOrEmpty(bean.RateCode))
            {
                sql.Append(" RateCode={" + i++ + "},");
                dList.Add(bean.RateCode);
            }
            if (sql.Length > 0)
            {
                sql = sql.Remove(sql.Length - 1, 1);
            }
            sql.Append(",Remarks={" + i++ + "} ,UpdateDt=now() where ProjectFrontNum={" + i++ + "} and InternetGroupId={" + i++ + "} ");
            dList.Add(bean.Remarks);
            dList.Add(bean.ProjectFrontNum);
            dList.Add(bean.InternetGroupId);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        /// <summary>
        /// 查询团购项目
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryProjectBean(InternetGroupInfo bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("select t.* from t_Project t where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.ProjectFrontNum))
            {
                sql.Append(" and ProjectFrontNum={" + ++i + "}");
                list.Add(bean.ProjectFrontNum);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql.Append(" and InternetGroupId={" + ++i + "}");
                list.Add(bean.InternetGroupId);
            }
            if (!string.IsNullOrEmpty(bean.ProjectName))
            {
                sql.Append(" and ProjectName={" + ++i + "}");
                list.Add(bean.ProjectName);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeId))
            {
                sql.Append(" and RoomTypeId={" + ++i + "}");
                list.Add(bean.RoomTypeId);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeName))
            {
                sql.Append(" and RoomTypeName={" + ++i + "}");
                list.Add(bean.RoomTypeName);
            }
            if (!string.IsNullOrEmpty(bean.Remarks))
            {
                sql.Append(" and Remarks={" + ++i + "}");
                list.Add(bean.Remarks);
            }
            if (bean.Rate!=0.0f)
            {
                sql.Append(" and Rate={" + ++i + "}");
                list.Add(bean.Rate);
            }
            if (!string.IsNullOrEmpty(bean.RateCode))
            {
                sql.Append(" and RateCode={" + ++i + "}");
                list.Add(bean.RateCode);
            }
            if (!string.IsNullOrEmpty(bean.Creater))
            {
                sql.Append(" and Creater={" + ++i + "}");
                list.Add(bean.Creater);
            }
            if (!string.IsNullOrEmpty(bean.Updater))
            {
                sql.Append(" and Updater={" + ++i + "} order by t.CreateDt desc ");
                list.Add(bean.Updater);
            }
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 分页查询团购项目
        /// </summary>
        /// <param name="bean"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable QueryProject(InternetGroupInfo bean, int page, int rows)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            var list = new List<object>();
            var i = -1;
            sql.Append("select t.* from t_Project t where 1=1 ");
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql.Append(" and InternetGroupId={" + ++i + "}");
                list.Add(bean.InternetGroupId);
            }
            sql.Append(" order by t.CreateDt desc limit " + (page - 1) * rows + "," + rows);

            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 查询团购项目行数
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public int QueryProjectRows(InternetGroupInfo bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            var list = new List<object>();
            var i = -1;
            sql.Append("select count(*) from t_Project t where 1=1 ");
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql.Append(" and InternetGroupId={" + ++i + "}");
                list.Add(bean.InternetGroupId);
            }
            sql.Append(" order by t.CreateDt desc ");
            var sum = int.Parse(mso.GetScalar(sql.ToString(), list.ToArray()).ToString());
            return sum;
        }

        /// <summary>
        /// 查询房间属性
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryRoomQuality(RoomQuality bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("select t.* from t_RoomQuality t where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql.Append(" and Id={" + ++i + "}");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" and Name={" + ++i + "}");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql.Append(" and HotelId={" + ++i + "}");
                list.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.TypeId))
            {
                sql.Append(" and TypeId={" + ++i + "}");
                list.Add(bean.TypeId);
            }
            return mso.GetDataTable(sql.Append(" order by name").ToString(), list.ToArray());
        }



        public override bool Exist(InternetGroupInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(InternetGroupInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(InternetGroupInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(InternetGroupInfo bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable Query(InternetGroupInfo bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(InternetGroupInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(InternetGroupInfo bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}