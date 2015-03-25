using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class MaterialDal : BaseDal<Material>
    {
        public override bool Exist(Material bean)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Id ");
            sql.Append("from t_Material ");
            sql.Append("where Id={0}");
            var dt = mso.GetDataTable(sql.ToString(), bean.Id);
            if (dt == null)
            {
                return false;
            }
            return dt.Rows.Count > 0;
        }

        public override void Add(Material bean)
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
            if (!string.IsNullOrEmpty(bean.Url))
            {
                sql1.Append(" Url,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Url);
            }
            if (bean.DateTime != null)
            {
                sql1.Append(" DateTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.DateTime);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql1.Append(" Note,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Note);
            }
            if (!string.IsNullOrEmpty(bean.Operator))
            {
                sql1.Append(" Operator,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Operator);
            }
            if (bean.UpdateDt != null)
            {
                sql1.Append(" UpdateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.UpdateDt);
            }
            if (bean.UpdatePerson != null)
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
            var sql = "insert into t_Material(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(Material bean)
        {
            Log.Debug("del方法参数：");
            var sql = "delete from t_Material where Id={0}";
            mso.Execute(sql, bean.Id);
        }

        public override void Modify(Material bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Material set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" Name={" + i++ + "},");
                dList.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.Url))
            {
                sql.Append(" Url={" + i++ + "},");
                dList.Add(bean.Url);
            }
            if (bean.DateTime != null)
            {
                sql.Append(" DateTime={" + i++ + "},");
                dList.Add(bean.DateTime);
            }
            if (!string.IsNullOrEmpty(bean.Operator))
            {
                sql.Append(" Operator={" + i++ + "},");
                dList.Add(bean.Operator);
            }
            if (bean.UpdateDt != null)
            {
                sql.Append(" UpdateDt={" + i++ + "},");
                dList.Add(bean.UpdateDt);
            }
            if (bean.UpdatePerson != null)
            {
                sql.Append(" UpdatePerson={" + i++ + "},");
                dList.Add(bean.UpdatePerson);
            }
            sql.Append(" Note={" + i++ + "},");
            dList.Add(bean.Note);

            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override DataTable Query(Material bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Material bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bean"></param>
        /// <param name="index">第几页</param>
        /// <param name="datacount">每一页的数据行数量</param>
        /// <param name="sum">总共数据量</param>
        /// <returns></returns>
        public override DataTable QueryByPage(Material bean, int index, int datacount, ref int sum)
        {
            Log.Debug("Modify方法参数：" + bean);
            var list = new List<int>();
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            sqlCount.Append("select count(*) count from t_Material");
            sum = int.Parse(mso.GetScalar(sqlCount.ToString()).ToString());
            sqlDt.Append("select Id,Name,Operator,Note,Url,UpdatePerson, ");
            sqlDt.Append("Date_Format(UpdateDt,'%Y-%m-%d %H:%i:%s') UpdateDtPara,Date_Format(DateTime,'%Y-%m-%d %H:%i:%s') DateTimePara ");
            sqlDt.Append("from t_Material where 1=1 ");
            if (!string.IsNullOrEmpty(bean.Id))
                sqlDt.Append(" and Id={" + bean.Id + "}");

            if (!string.IsNullOrEmpty(bean.Name))
                sqlDt.Append(" and Name={" + bean.Name + "}");

            if (!string.IsNullOrEmpty(bean.Url))
                sqlDt.Append(" and Url={" + bean.Url + "}");

            if (bean.DateTime != null)
                sqlDt.Append(" and DateTime={" + bean.DateTime + "}");

            if (!string.IsNullOrEmpty(bean.Operator))
                sqlDt.Append(" and Operator={" + bean.Operator + "}");

            if (!string.IsNullOrEmpty(bean.Note))
                sqlDt.Append(" and Note={" + bean.Note + "}");

            if (bean.UpdateDt != null)
                sqlDt.Append(" and UpdateDt={" + bean.UpdateDt + "}");

            if (bean.UpdatePerson != null)
                sqlDt.Append(" and UpdatePerson={" + bean.UpdatePerson + "}");
            sqlDt.Append(" order by UpdateDt desc ");
            sqlDt.Append("limit " + (index - 1) * datacount + "," + datacount);
            return mso.GetDataTable(sqlDt.ToString());
        }

        /// <summary>
        /// 获取素材名称和路径
        /// </summary>
        /// <returns></returns>
        public DataTable FindByMaterial()
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Name,Url,Note ");
            sql.Append("from t_Material ");
            return mso.GetDataTable(sql.ToString());
        }

        /// <summary>
        /// 获取素材名称和路径
        /// </summary>
        /// <returns></returns>
        public DataTable FindById(string id)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Name,Url,Note ");
            sql.Append("from t_Material t ");
            sql.Append("where t.Id={0} ");
            return mso.GetDataTable(sql.ToString(), id);
        }
    }
}