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

    public class IoJournalDal : BaseDal<IoJournal>
    {
        private new static ILog _log = log4net.LogManager.GetLogger("CheckNoDal");

        public IoJournalDal()
        {
        }

        /// <summary>
        /// 查询流水账
        /// </summary>
        /// <param name="iobean"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable QueryIojournal( IoJournal iobean, int page, int rows)
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
            sql.Append(" order by iotime desc limit " + (page - 1) * rows + "," + rows);
            try
            {
                return mso.GetDataTable(sql.ToString(),paramList.ToArray());
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
        /// <param name="iobean"></param>
        /// <returns></returns>
        public int QueryIojournalRows(IoJournal iobean)
        {
            var sql = new StringBuilder();
            var i = 0;
            var paramList = new List<object>();
            sql.Append("select count(*) from t_IOJournal t where 1=1  ");
            if (iobean.BeginTime != new DateTime() && iobean.EndTime != new DateTime())
            {
                sql.Append(" and t.iotime between {" + i++ + "} and {" + i++ + "} ");
                paramList.Add(iobean.BeginTime);
                paramList.Add(iobean.EndTime);
            }
            sql.Append(" order by iotime desc ");
            try
            {
                var sum = int.Parse(mso.GetScalar(sql.ToString(),paramList.ToArray()).ToString());
                return sum;
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }

        /// <summary>
        /// 添加流水账记录
        /// </summary>
        /// <param name="bean"></param>
        public void AddIoJournal(IoJournal bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            
            if (!string.IsNullOrEmpty(bean.OrderId))
            {
                sql1.Append(" OrderId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.OrderId);
            }
            if (!string.IsNullOrEmpty(bean.RoomNo))
            {
                sql1.Append(" RoomNo,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.RoomNo);
            }
            if (!string.IsNullOrEmpty(bean.IoId))
            {
                sql1.Append(" IoId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IoId);
            }
            if (!string.IsNullOrEmpty(bean.IoName))
            {
                sql1.Append(" IoName,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IoName);
            }
            if (bean.IoMoney != 0)
            {
                sql1.Append(" IoMoney,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IoMoney);
            }
            if (bean.IoTag != 0)
            {
                sql1.Append(" IoTag,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IoTag);
            }
            if (bean.IsUse != 0)
            {
                sql1.Append(" IsUse,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IsUse);
            }
            if (bean.IoFrom != 0)
            {
                sql1.Append(" IoFrom,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IoFrom);
            }
            if (bean.InOrOutCard != 0)
            {
                sql1.Append(" InOrOutCard,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.InOrOutCard);
            }
            sql1.Append(" IoTime,");
            sql2.Append(" {" + i++ + "},");
            list.Add(bean.IoTime);

            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_IOJournal(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        /// <summary>
        /// 删除方法——更改流水账作废状态
        /// </summary>
        /// <param name="bean"></param>
        public void DelIoJournal(IoJournal bean)
        {
            Log.Debug("DEL方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            var i = 0;
            var dList = new List<object>();
            sql.Append(" update t_IOJournal set IsUse={" + i++ + "} where IoTime={" + i++ + "} and IoId={" + i++ + "} ");
            dList.Add(bean.IsUse);
            dList.Add(bean.IoTime);
            dList.Add(bean.IoId);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        /// <summary>
        /// 多条件查询流水账
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryIoBean(IoJournal bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("select t.* from t_IOJournal t where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.RoomNo))
            {
                sql.Append(" and RoomNo={" + ++i + "}");
                list.Add(bean.RoomNo);
            }
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 修改机器来源收入是否发卡状态
        /// </summary>
        /// <param name="bean"></param>
        public void EditIoIfCard(IoJournal bean)
        {
            Log.Debug("Modify方法参数：" + bean.InOrOutCard+","+bean.IoTime+","+bean.IoId);
            var sql = new StringBuilder();
            sql.Append(" update t_IOJournal set ");
            var i = 0;
            var dList = new List<object>();
            if (bean.InOrOutCard!=0)
            {
                sql.Append(" InOrOutCard={" + i++ + "} ");
                dList.Add(bean.InOrOutCard);
            }
            sql.Append("where IoTime={" + i++ + "} and IoId={" + i++ + "}  ");
            sql.Append("and IoFrom={" + i++ + "} and IoTag={" + i++ + "}  ");
            dList.Add(bean.IoTime);
            dList.Add(bean.IoId);
            dList.Add(bean.IoFrom);
            dList.Add(bean.IoTag);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());
        }


        public override bool Exist(IoJournal bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(IoJournal bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(IoJournal bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(IoJournal bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable Query(IoJournal bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(IoJournal bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(IoJournal bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }

}