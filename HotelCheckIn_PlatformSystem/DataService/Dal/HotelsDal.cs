using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Bll;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class HotelsDal : BaseDal<Hotels>
    {
        public override bool Exist(Hotels bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Hotels bean)
        {
            Log.Debug("Add方法参数：" + bean);
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql1.Append(" Id,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql1.Append(" Name,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.Address))
            {
                sql1.Append(" Address,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Address);
            }
            if (!string.IsNullOrEmpty(bean.Tel))
            {
                sql1.Append(" Tel,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Tel);
            }
            if (!string.IsNullOrEmpty(bean.Contact))
            {
                sql1.Append(" Contact,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Contact);
            }
            if (!string.IsNullOrEmpty(bean.AreaId))
            {
                sql1.Append(" AreaId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.AreaId);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql1.Append(" Note,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Note);
            }
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.CreatePerson))
            {
                sql1.Append(" CreatePerson,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreatePerson);
            }
            if (bean.UpdateDt != null)
            {
                sql1.Append(" UpdateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.UpdateDt);
            }
            if (!string.IsNullOrEmpty(bean.UpdatePerson))
            {
                sql1.Append(" UpdatePerson,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.UpdatePerson);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Hotels(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(Hotels bean)
        {
            Log.Debug("Del方法参数：");
            var sql = new StringBuilder();
            sql.Append("delete from t_Hotels ");
            sql.Append("where Id={0} ");
            mso.Execute(sql.ToString(), bean.Id);
        }

        public override void Modify(Hotels bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Hotels set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" Name={" + i++ + "},");
                dList.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.Address))
            {
                sql.Append(" Address={" + i++ + "},");
                dList.Add(bean.Address);
            }
            if (!string.IsNullOrEmpty(bean.Tel))
            {
                sql.Append(" Tel={" + i++ + "},");
                dList.Add(bean.Tel);
            }
            if (!string.IsNullOrEmpty(bean.Contact))
            {
                sql.Append(" Contact={" + i++ + "},");
                dList.Add(bean.Contact);
            }
            if (!string.IsNullOrEmpty(bean.AreaId))
            {
                sql.Append(" AreaId={" + i++ + "},");
                dList.Add(bean.AreaId);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql.Append(" Note={" + i++ + "},");
                dList.Add(bean.Note);
            }
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "},");
                dList.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.CreatePerson))
            {
                sql.Append(" CreatePerson={" + i++ + "},");
                dList.Add(bean.CreatePerson);
            }
            if (bean.UpdateDt != null)
            {
                sql.Append(" UpdateDt={" + i++ + "},");
                dList.Add(bean.UpdateDt);
            }
            if (!string.IsNullOrEmpty(bean.UpdatePerson))
            {
                sql.Append(" UpdatePerson={" + i++ + "},");
                dList.Add(bean.UpdatePerson);
            }
           
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override DataTable Query(Hotels bean)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select * ");
            sql.Append("from t_Hotels ");
            sql.Append("where Id={0} ");
            return mso.GetDataTable(sql.ToString(), bean.Id);
        }

        public override DataTable QueryByPage(Hotels bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Hotels bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据区域id获取酒店
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public DataTable FindByHotelsByAreaId(string areaid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Id,Name ");
            sql.Append("from t_Hotels ");
            sql.Append("where AreaId={0} ");
            return mso.GetDataTable(sql.ToString(), areaid);
        }

        /// <summary>
        /// 获取酒店数据
        /// </summary>
        /// <returns></returns>
        public DataTable FindByHotels(string hotelid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select * ");
            sql.Append("from t_Hotels th ");
            sql.Append("where th.id={0} ");
            return mso.GetDataTable(sql.ToString(), hotelid);
        }


        /// <summary>
        /// 根据区域id获取酒店
        /// </summary>
        /// <param name="areaid"></param>
        /// <param name="dataPage"></param>
        /// <returns></returns>
        public Dictionary<int,DataTable> FindByHotelsByAreaId(string areaid, DataPage dataPage)
        {
            Log.Debug("Query方法参数：");
            var dic = new Dictionary<int, DataTable>();
            var sqlquery = new StringBuilder();
            var sqlcount = new StringBuilder();
            var sqlWhere = new StringBuilder();

            var list = new List<object>();
            sqlcount.Append("select count(*) count ");
            sqlcount.Append("from t_Hotels th ");
            sqlcount.Append("where 1=1 ");
            var i = 0;
            if (!string.IsNullOrEmpty(areaid))
            {
                sqlWhere.Append("and th.AreaId={" + i++ + "} ");
                list.Add(areaid);
            }

            sqlcount.Append(sqlWhere);
            dataPage.Sum = int.Parse(mso.GetScalar(sqlcount.ToString(), list.ToArray()).ToString());

            sqlquery.Append("select th.Id,th.Address,th.`Name`,th.Note,th.Tel,th.Contact, ");
            sqlquery.Append("(SELECT tc.`Name` FROM t_Code tc where tc.Id=th.AreaId and tc.type='jdqy') areaname ");
            sqlquery.Append("from t_Hotels th ");
            sqlquery.Append("where 1=1 ");
            sqlquery.Append(sqlWhere);
            sqlquery.Append("order by th.AreaId ");
            sqlquery.Append("limit " + (dataPage.Index - 1) * dataPage.Count + "," + dataPage.Count);
            var dt= mso.GetDataTable(sqlquery.ToString(), list.ToArray());
            dic.Add(dataPage.Sum,dt);
            return dic;
        }
    }
}