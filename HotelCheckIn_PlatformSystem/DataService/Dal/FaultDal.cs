using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;
using HotelCheckIn_PlatformSystem.DataService.Bll;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class FaultDal : BaseDal<Fault>
    {
        public override bool Exist(Fault bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Fault bean)
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
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql1.Append(" MachineId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.MachineId);
            }
            if (!string.IsNullOrEmpty(bean.FaultId))
            {
                sql1.Append(" FaultId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.FaultId);
            }
            if (!string.IsNullOrEmpty(bean.Reason))
            {
                sql1.Append(" Reason,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Reason);
            }
            if (!string.IsNullOrEmpty(bean.SolvePerson))
            {
                sql1.Append(" SolvePerson,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.SolvePerson);
            }
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (bean.SolveDt != null)
            {
                sql1.Append(" SolveDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.SolveDt);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Fault(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public string Add(Fault bean, ref object[] objects)
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
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql1.Append(" MachineId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.MachineId);
            }
            if (!string.IsNullOrEmpty(bean.FaultId))
            {
                sql1.Append(" FaultId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.FaultId);
            }
            if (!string.IsNullOrEmpty(bean.Reason))
            {
                sql1.Append(" Reason,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Reason);
            }
            if (!string.IsNullOrEmpty(bean.SolvePerson))
            {
                sql1.Append(" SolvePerson,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.SolvePerson);
            }
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (bean.SolveDt != null)
            {
                sql1.Append(" SolveDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.SolveDt);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Fault(" + sql1 + ") values(" + sql2 + ")";
            objects = list.ToArray();
            return sql;
        }

        public override void Del(Fault bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(Fault bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(Fault bean)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select * ");
            sql.Append("from t_Fault ");
            sql.Append("where Id={0} ");
            return mso.GetDataTable(sql.ToString(), bean.Id);
        }

        public override DataTable QueryByPage(Fault bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Fault bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询故障数据
        /// </summary>
        /// <param name="jssj">结束时间</param>
        /// <param name="dataPage">分页类</param>
        /// <param name="jdid">酒店id</param>
        /// <param name="gzid">故障Id</param>
        /// <param name="kssj">开始时间</param>
        /// <returns></returns>
        public Dictionary<int, DataTable> QueryFault(string jdid, string gzid, DateTime? kssj, DateTime? jssj, DataPage dataPage)
        {
            Log.Debug("Modify方法参数：");
            var dic = new Dictionary<int, DataTable>();
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            var sqlInnerJoin = new StringBuilder();
            var sqlWhere = new StringBuilder();
            var list = new List<object>();
            sqlInnerJoin.Append("INNER JOIN t_Machine tm ON tf.MachineId=tm.jqId ");
            sqlInnerJoin.Append("INNER JOIN t_Hotels th ON th.Id=tm.HotelId ");
            sqlInnerJoin.Append("LEFT JOIN t_Code tc ON tc.Id=tf.FaultId where 1=1 ");
            var i = 0;
            if (!string.IsNullOrEmpty(jdid))
            {
                sqlWhere.Append("and th.Id={" + i++ + "} ");
                list.Add(jdid);
            }
            if (!string.IsNullOrEmpty(gzid))
            {
                sqlWhere.Append("and tf.FaultId={" + i++ + "} ");
                list.Add(gzid);
            }
            if (kssj != null && jssj != null)
            {
                sqlWhere.Append("and tf.CreateDt BETWEEN {" + i++ + "} AND {" + i++ + "} ");
                list.Add(kssj);
                list.Add(jssj);
            }

            sqlCount.Append("select count(*) count from t_Fault tf ");
            sqlCount.Append(sqlInnerJoin);
            sqlCount.Append(sqlWhere);
            dataPage.Sum = int.Parse(mso.GetScalar(sqlCount.ToString(), list.ToArray()).ToString());

            sqlDt.Append("SELECT tf.MachineId,tm.`Name` MachineName,tc.`Name` FaultName,tf.Reason,tf.SolvePerson, ");
            sqlDt.Append("Date_Format( tf.SolveDt,'%Y-%m-%d %H:%i:%s') SolveDtPara, ");
            sqlDt.Append("Date_Format( tf.CreateDt,'%Y-%m-%d %H:%i:%s') CreateDtPara ");
            sqlDt.Append("FROM t_Fault tf  ");
            sqlDt.Append(sqlInnerJoin);
            sqlDt.Append(sqlWhere);
            sqlDt.Append("order by tf.CreateDt desc ");
            sqlDt.Append("limit " + (dataPage.Index - 1) * dataPage.Count + "," + dataPage.Count);
            var dt = mso.GetDataTable(sqlDt.ToString(), list.ToArray());
            dic.Add(dataPage.Sum, dt);
            return dic;
        }
    }
}