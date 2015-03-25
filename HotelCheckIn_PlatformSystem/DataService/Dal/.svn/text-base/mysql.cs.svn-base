using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class MySql
    {
        #region 静态方法
        private static string _ConnectString;
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string ConnectString
        {
            get
            {
                return _ConnectString;
            }
        }
        static MySql()
        {
            try
            {
                _ConnectString = System.Configuration.ConfigurationManager.AppSettings["dbconnect"];
            }
            catch
            {
                throw new Exception("数据库连接串需要保存在配置文件中!");
            }
        }
        /// <summary>
        /// 查询单一个结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            MySqlConnection conn = new MySqlConnection(_ConnectString);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                return cmd.ExecuteScalar();
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public static void ExecuteNoQuery(string sql)
        {
            MySqlConnection conn = new MySqlConnection(_ConnectString);
            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行查询并返回结果集
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql)
        {
            try
            {
                MySqlDataAdapter mysqlAdp = new MySqlDataAdapter(sql, _ConnectString);

                if (mysqlAdp != null)
                {
                    DataTable dt = new DataTable("Result");

                    mysqlAdp.Fill(dt);

                    mysqlAdp.Dispose();

                    return dt;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 执行查询返回指定数量记录的结果集
        /// </summary>
        /// <param name="sql">语句</param>
        /// <param name="start">起始记录</param>
        /// <param name="count">记录数</param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql, int start, int count)
        {
            try
            {
                MySqlDataAdapter mysqlAdp = new MySqlDataAdapter(sql, _ConnectString);

                if (mysqlAdp != null)
                {
                    DataTable dt = new DataTable("Result");

                    mysqlAdp.Fill(start, count, dt);

                    mysqlAdp.Dispose();

                    return dt;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 执行带参数的查询
        /// </summary>
        /// <param name="sqlByParam">查询语句，参数用@P#表示，#表示数字</param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sqlByParam, params object[] queryParams)
        {
            try
            {
                MySqlDataAdapter adp = new MySqlDataAdapter(sqlByParam, _ConnectString);

                MySqlCommand cmd = adp.SelectCommand;

                for (int i = 0; i < queryParams.Length; i++)
                {
                    cmd.Parameters.AddWithValue(string.Format("@P{0}", i), queryParams[i]);
                }

                DataTable dt = new DataTable("Result");

                adp.Fill(dt);

                adp.Dispose();

                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static void ExecuteNoQuery(string sqlByParam, params object[] queryParams)
        {
            MySqlConnection con = new MySqlConnection(_ConnectString);
            MySqlTransaction sqlTrans = null;
            try
            {
                con.Open();

                sqlTrans = con.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand(sqlByParam, con, sqlTrans);

                for (int i = 0; i < queryParams.Length; i++)
                {
                    cmd.Parameters.AddWithValue(string.Format("@P{0}", i), queryParams[i]);
                }

                cmd.ExecuteNonQuery();
                sqlTrans.Commit();
            }
            catch (Exception ex)
            {
                if (sqlTrans != null) sqlTrans.Rollback();
                throw new Exception("执行更新语句失败,可能原因：语句错误或外键关联");
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public static object ExecuteScalar(string sqlByParam, params object[] queryParams)
        {
            MySqlConnection con = new MySqlConnection(_ConnectString);

            con.Open();

            MySqlCommand cmd = new MySqlCommand(sqlByParam, con);

            for (int i = 0; i < queryParams.Length; i++)
            {
                cmd.Parameters.AddWithValue(string.Format("@P{0}", i), queryParams[i]);
            }

            object o = cmd.ExecuteScalar();

            con.Close();

            return o;
        }
        #endregion
    }
}