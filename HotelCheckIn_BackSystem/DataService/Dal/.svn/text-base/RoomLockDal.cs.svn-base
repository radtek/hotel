using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Dal
{

    public class RoomLockDal : BaseDal<RoomLockInfo>
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger("RoomLockDal");
        public RoomLockDal()
        { }

        /// <summary>
        /// 查询房间锁定信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAllRoomLock()
        {
            var sql = new StringBuilder();
            sql.Append("select t.* from t_RoomLockInfo t ");
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
        /// 分页查询房间锁定信息
        /// </summary>
        /// <param name="rkinfo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable QueryRoomLock(RoomLockInfo rkinfo, int page, int rows)
        {
            _log.Debug("方法Query接收参数：查询日期区间:" + rkinfo.BeginTime + rkinfo.EndTime
                + "-page:" + page + "-rows:" + rows);
            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select t.* from t_RoomLockInfo t where t.LockTime "
                + "between {" + i++ + "} and {" + i++ + "} ");
            var paramList = new List<object> { rkinfo.BeginTime, rkinfo.EndTime };
            sql.Append(" order by LockTime desc limit " + (page - 1) * rows + "," + rows);
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
        /// 分页查询房间锁定信息行数
        /// </summary>
        /// <param name="rkinfo"></param>
        /// <returns></returns>
        public int QueryRoomLock(RoomLockInfo rkinfo)
        {
            _log.Debug("方法Query接收参数：查询日期区间:" + rkinfo.BeginTime + rkinfo.EndTime);
            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select count(*) from t_RoomLockInfo t where t.LockTime "
                + "between {" + i++ + "} and {" + i++ + "} ");
            var paramList = new List<object> { rkinfo.BeginTime, rkinfo.EndTime };
            sql.Append(" order by LockTime desc ");
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
        /// 删除房间锁定信息
        /// </summary>
        /// <param name="checkid"></param>
        public void DelRoomLock(string checkid)
        {
            var strSql = new StringBuilder();
            strSql.Append(" delete from t_RoomLockInfo where CheckId={0} ");
            var parameters = new object[] { checkid };
            try
            {
                mso.Execute(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                _log.Error("删除房间锁定信息失败！", e);
            }
        }

        public override bool Exist(RoomLockInfo bean)
        {
            _log.Debug("方法Query接收参数：查询日期区间:" + bean.ToString());
            var sql = new StringBuilder("select count(*) from t_RoomLockInfo t where 1=1 ");
            var sqlWhere = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                sqlWhere.Append(" and CheckId={" + i++ + "},");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sqlWhere.Append(" and  HotelId={" + i++ + "},");
                list.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.MacId))
            {
                sqlWhere.Append(" and MacId={" + i++ + "},");
                list.Add(bean.MacId);
            }
            if (!string.IsNullOrEmpty(bean.RoomNum))
            {
                sqlWhere.Append(" and RoomNum={" + i++ + "},");
                list.Add(bean.RoomNum);
            }
            if (bean.LockTime != DateTime.MinValue)
            {
                sqlWhere.Append(" and LockTime={" + i + "},");
                list.Add(bean.LockTime);
            }
            if (sqlWhere.Length > 0)
            {
                sqlWhere = sqlWhere.Remove(sqlWhere.Length - 1, 1);
            }
            try
            {
                var sum = int.Parse(mso.GetScalar(sql.Append(sqlWhere).ToString(), list.ToArray()).ToString());
                if (sum > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }

        /// <summary>
        /// 添加房间锁定信息,并且修改验证码表的机器验证字段值
        /// </summary>
        /// <param name="bean"></param>
        public override void Add(RoomLockInfo bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                sql1.Append(" CheckId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql1.Append(" HotelId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.MacId))
            {
                sql1.Append(" MacId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.MacId);
            }
            if (!string.IsNullOrEmpty(bean.RoomNum))
            {
                sql1.Append(" RoomNum,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.RoomNum);
            }
            if (!string.IsNullOrEmpty(bean.PhoneNumber))
            {
                sql1.Append(" PhoneNumber,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.PhoneNumber);
            }
            if (bean.LockTime != DateTime.MinValue)
            {
                sql1.Append(" LockTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.LockTime);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_RoomLockInfo(" + sql1 + ") values(" + sql2 + ");update t_CheckNo set";
            sql += " MachineCheck={" + i++ + "} where CheckId={" + i++ + "}";
            list.AddRange(new object[] { 2, bean.CheckId });
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(RoomLockInfo bean)
        {
            var strSql = new StringBuilder();
            strSql.Append(" delete from t_RoomLockInfo where 1=1 ");
            List<object> list = new List<object>();
            int i = 0;
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                strSql.Append(" and CheckId={" + i++ + "}");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.PhoneNumber))
            {
                strSql.Append(" and phoneNumber={" + i++ + "}");
                list.Add(bean.PhoneNumber);
            }
            if (!string.IsNullOrEmpty(bean.RoomNum))
            {
                strSql.Append(" and RoomNum={" + i++ + "}");
                list.Add(bean.RoomNum);
            }
            try
            {
                mso.Execute(strSql.ToString(), list.ToArray());
            }
            catch (Exception e)
            {
                _log.Error("删除房间锁定信息失败！", e);
            }
        }

        public override void Modify(RoomLockInfo bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable Query(RoomLockInfo bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(RoomLockInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(RoomLockInfo bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}