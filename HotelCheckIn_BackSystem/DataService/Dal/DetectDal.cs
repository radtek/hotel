using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HotelCheckIn_BackSystem.DataService.Common;
using HotelCheckIn_BackSystem.DataService.Model;
using HotelCheckIn_BackSystem.HumanIdentify;
using MySql.Data.MySqlClient;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class DetectDal
    {
        private static ILog _log = log4net.LogManager.GetLogger("DetectDal");

        /// <summary>
        /// 获取条件为：未验证&创建时间最新的第一条数据
        /// </summary>
        /// <returns></returns>
        public IList<Detect> GetFirstData()
        {
            const string sql = @" SELECT *
                            FROM t_detect t
                            where t.Status=1
                            order by t.CreateDt
                            limit 1 ";

            DataSet ds = null;
            try
            {
                ds = MySQLOperater.ExecuteQuery(sql);
            }
            catch (MySqlException e)
            {
                _log.Error("sql:" + sql + e.Message);
                throw new Exception("查询数据出错！");
            }
            var data = Helper.ToList<Detect>(ds.Tables[0]);
            return data;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<Detect> GetPictureData(string id, string type)
        {
            var sql = "";
            var paramList = new List<MySqlParameter>();
            switch (type)
            {
                case "IdCardImg":
                    sql = " SELECT t.IdCardImg";
                    break;
                case "Camera1":
                    sql = " SELECT t.Camera1";
                    break;
                case "Camera2":
                    sql = " SELECT t.Camera2";
                    break;
                case "Camera3":
                    sql = " SELECT t.Camera3";
                    break;
                default: break;
            }
            sql += @" FROM t_detect t
                   where t.id=?id";
            paramList.Add(new MySqlParameter("?id", id));
            DataSet ds;
            try
            {
                ds = MySQLOperater.ExecuteQuery(sql, paramList.ToArray());
            }
            catch (MySqlException e)
            {
                _log.Error("sql:" + sql + e.Message);
                throw new Exception("查询数据出错！");
            }
            var data = Helper.ToList<Detect>(ds.Tables[0]);
            return data;
        }

        /// <summary>
        /// 查询身份验证状态
        /// </summary>
        /// <param name="id">身份验证表id</param>
        /// <returns></returns>
        public IList<Detect> QueryDetectStatus(string id)
        {
            var paramList = new List<MySqlParameter>();
            const string sql = @" SELECT t.status
                            FROM t_detect t
                            where t.id=?id";
            paramList.Add(new MySqlParameter("?id", id));

            DataSet ds = null;
            try
            {
                ds = MySQLOperater.ExecuteQuery(sql, paramList.ToArray());
            }
            catch (MySqlException e)
            {
                _log.Error("sql:" + sql + e.Message);
                throw new Exception("查询数据出错！");
            }
            var data = Helper.ToList<Detect>(ds.Tables[0]);
            return data;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="bean"></param>
        public void Update(Detect bean)
        {
            var sql = new StringBuilder();
            sql.Append(" update t_detect t set ");
            var paramList = new List<MySqlParameter>();
            if (bean.Status != 0)
            {
                sql.Append(" Status=?Status,");
                paramList.Add(new MySqlParameter("?Status", bean.Status));
            }
            if (bean.UpdateDt != null)
            {
                sql.Append(" UpdateDt=?UpdateDt,");
                paramList.Add(new MySqlParameter("?UpdateDt", bean.UpdateDt));
            }
            if (!string.IsNullOrEmpty(bean.Operator))
            {
                sql.Append(" Operator=?Operator,");
                paramList.Add(new MySqlParameter("?Operator", bean.Operator));
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" where Id=?Id");
            paramList.Add(new MySqlParameter("?Id", bean.Id));
            MySQLOperater.ExecuteNonQuery(sql.ToString(), paramList.ToArray());
        }

        /// <summary>
        /// 保存身份验证信息
        /// </summary>
        /// <param name="bean"></param>
        public void Save(Detect bean)
        {
            string sql = @"insert into t_detect(Id,IdCard,Name,Sex,IdCardImg,Camera1,Camera2,Camera3,Vedio,UpdateDt,CreateDt,Note,Jqid) 
                           values(?Id,?IdCard,?Name,?Sex,?IdCardImg,?Camera1,?Camera2,?Camera3,?Vedio,?UpdateDt,?CreateDt,?Note,?Jqid)";
            List<MySqlParameter> paramList = new List<MySqlParameter>();
            paramList.Add(new MySqlParameter("?Id", bean.Id));
            paramList.Add(new MySqlParameter("?IdCard", bean.IdCard));
            paramList.Add(new MySqlParameter("?Name", bean.Name));
            paramList.Add(new MySqlParameter("?Sex", bean.Sex));
            paramList.Add(new MySqlParameter("?IdCardImg", bean.IdCardImg));
            paramList.Add(new MySqlParameter("?Camera1", bean.Camera1));
            paramList.Add(new MySqlParameter("?Camera2", bean.Camera2));
            paramList.Add(new MySqlParameter("?Camera3", bean.Camera3));
            paramList.Add(new MySqlParameter("?Vedio", bean.Vedio));
            paramList.Add(new MySqlParameter("?UpdateDt", bean.UpdateDt));
            paramList.Add(new MySqlParameter("?CreateDt", bean.CreateDt));
            paramList.Add(new MySqlParameter("?Note", bean.Note));
            paramList.Add(new MySqlParameter("?Jqid", bean.Jqid));
            try
            {
                MySQLOperater.ExecuteTrans(sql, paramList.ToArray());
            }
            catch (MySqlException e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("保存终端信息出错！");
            }
        }
    }
}