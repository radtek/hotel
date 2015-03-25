using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;
using MySql.Data.MySqlClient;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Dal
{

    public class SettleDal : BaseDal<Settle>
    {
        private new static ILog _log = log4net.LogManager.GetLogger("SettleDal");

        public SettleDal()
        {
        }

        /// <summary>
        /// 查询结算
        /// </summary>
        /// <param name="settle"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable QuerySettle(Settle settle, int page, int rows)
        {
            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select t.* from t_Settle t where 1=1  ");
            var paramList = new List<object>();
            if (settle.QBeginTime!=new DateTime()&&settle.QEndTime!=new DateTime())
            {
                sql.Append(" and t.SettleDateTime between {" + i++ + "} and {" + i++ + "} ");
                paramList.Add(settle.QBeginTime);
                paramList.Add(settle.QEndTime);
            }
            sql.Append(" order by SettleDateTime desc limit " + (page - 1) * rows + "," + rows);
            try
            {
                return mso.GetDataTable(sql.ToString(), paramList.ToArray());
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }
        /// <summary>
        /// 查询流水账行数
        /// </summary>
        /// <param name="settle"></param>
        /// <returns></returns>
        public int QuerySettleRows(Settle settle)
        {
            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select count(*) from t_Settle t where 1=1  ");
            var paramList = new List<object>();
            if (settle.QBeginTime != new DateTime() && settle.QEndTime != new DateTime())
            {
                sql.Append(" and t.SettleDateTime between {" + i++ + "} and {" + i++ + "} ");
                paramList.Add(settle.QBeginTime);
                paramList.Add(settle.QEndTime);
            }
            sql.Append(" order by SettleDateTime desc ");
            try
            {
                var sum = int.Parse(mso.GetScalar(sql.ToString(), paramList.ToArray()).ToString());
                return sum;
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }

        /// <summary>
        /// 查询最近的结算时间
        /// </summary>
        /// <returns></returns>
        public DataTable QueryNewSettleTime()
        {
            var sql = new StringBuilder();
            sql.Append("select max(EndTime) as EndTime from t_Settle ");
            try
            {
                return mso.GetDataTable(sql.ToString());
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }

        /// <summary>
        /// 查询流水账
        /// </summary>
        /// <param name="iobean"></param>
        /// <returns></returns>
        public DataTable QueryIojournal(IoJournal iobean)
        {
            var sql = new StringBuilder();
            var i = 0;
            var paramList = new List<object>();
            sql.Append("select t.* from t_IOJournal t where 1=1  ");
            if (iobean.BeginTime != new DateTime() && iobean.EndTime != new DateTime())
            {
                sql.Append(" and t.iotime between {" + i++ + "} and {" + i++ + "} ");
                paramList.Add(iobean.BeginTime);
                paramList.Add(iobean.EndTime);
            }
            sql.Append(" order by iotime desc " );
            try
            {
                return mso.GetDataTable(sql.ToString(), paramList.ToArray());
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }

        /// <summary>
        /// 添加结算记录
        /// </summary>
        /// <param name="bean"></param>
        public void AddSettle(Settle bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();

            if (!string.IsNullOrEmpty(bean.OptId))
            {
                sql1.Append(" OptId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.OptId);
            }
            if (!string.IsNullOrEmpty(bean.OptName))
            {
                sql1.Append(" OptName,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.OptName);
            }
            if (bean.InMoney!=0)
            {
                sql1.Append(" InMoney,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.InMoney);
            }
            if (bean.OutMoney!=0)
            {
                sql1.Append(" OutMoney,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.OutMoney);
            }
            if (bean.SumMoney != 0)
            {
                sql1.Append(" SumMoney,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.SumMoney);
            }
            if (bean.BeginTime != new DateTime())
            {
                sql1.Append(" BeginTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.BeginTime);
            }
            if (bean.EndTime != new DateTime())
            {
                sql1.Append(" EndTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.EndTime);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Settle(" + sql1 + ",SettleDateTime) values(" + sql2 + ",now())";
            mso.Execute(sql, list.ToArray());
        }

        public override bool Exist(Settle bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Settle bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(Settle bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(Settle bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable Query(Settle bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(Settle bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(Settle bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}