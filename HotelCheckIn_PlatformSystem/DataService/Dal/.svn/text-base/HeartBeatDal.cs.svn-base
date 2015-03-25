using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;
namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class HeartBeatDal : BaseDal<Heartbeat>
    {
        public override bool Exist(Heartbeat bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Heartbeat bean)
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
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Heartbeat(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public string Add(Heartbeat bean, ref object[] objects)
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
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Heartbeat(" + sql1 + ") values(" + sql2 + ")";
            objects = list.ToArray();
            return sql;
        }

        public override void Del(Heartbeat bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(Heartbeat bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Heartbeat set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql.Append(" MachineId={" + i++ + "}");
                dList.Add(bean.MachineId);
            }
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "}");
                dList.Add(bean.CreateDt);
            }
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public string Modify(Heartbeat bean,ref object[] objects)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Heartbeat set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql.Append(" MachineId={" + i++ + "}");
                dList.Add(bean.MachineId);
            }
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "}");
                dList.Add(bean.CreateDt);
            }
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            objects = dList.ToArray();
            return sql.ToString();
            //mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override DataTable Query(Heartbeat bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Heartbeat bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Heartbeat bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}