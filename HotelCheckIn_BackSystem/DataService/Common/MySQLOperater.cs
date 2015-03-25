using System.Data;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HotelCheckIn_BackSystem.DataService.Common
{
    public class MySQLOperater
    {
        private static ILog log = log4net.LogManager.GetLogger("MySQLOperater");
        private static String connectionString = System.Configuration.ConfigurationSettings.AppSettings["dbconnect"];
        public MySQLOperater()
        {
        }

        /// <summary>
        /// 执行单条插入语句，并返回id，不需要返回id的用ExceuteNonQuery执行。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteInsert(string sql, MySqlParameter[] parameters)
        {
            log.Debug("ExecuteInsert执行SQL：" + sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"select LAST_INSERT_ID()";
                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
                    return value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static int ExecuteInsert(string sql)
        {
            return ExecuteInsert(sql, null);
        }

        /// <summary>
        /// 执行带参数的sql语句,返回影响的记录数（insert,update,delete)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, MySqlParameter[] parameters)
        {
            log.Debug("ExecuteNonQuery执行SQL：" + sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 执行不带参数的sql语句，返回影响的记录数、不建议使用拼出来SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null);
        }

        /// <summary>
        /// 执行单条语句返回第一行第一列,可以用来返回count(*)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string sql, MySqlParameter[] parameters)
        {
            log.Debug("ExecuteScalar执行SQL：" + sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    int value = Int32.Parse(cmd.ExecuteScalar().ToString());
                    return value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 执行无参数，有返回值的sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null);
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlList"></param>
        /// <param name="paraList"></param>
        public static void ExecuteTrans(List<string> sqlList, List<MySqlParameter[]> paraList)
        {
            log.Debug("ExecuteTrans执行SQL：" + sqlList.ToString());
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction = null;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;

                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        cmd.CommandText = sqlList[i];
                        if (paraList != null && paraList[i] != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(paraList[i]);
                        }
                        log.Info(sqlList[i]);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {

                    }
                    throw e;
                }

            }
        }
        public static void ExecuteTrans(List<string> sqlList)
        {
            ExecuteTrans(sqlList, null);
        }


        public static void ExecuteTrans(string sql1, MySqlParameter[] param1)
        {
            log.Debug("ExecuteTrans执行SQL1：" + sql1 + "param1" + param1.ToString());
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction = null;
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;

                    int ID = ExecuteInsert(sql1, param1);
                    //List<MySqlParameter> paramList = new List<MySqlParameter>();
                    //string encryptMacID = encrypt.SHA1(ID.ToString());
                    //string sql2 = "update t_Machine set encryptMacID=?encryptMacID where jqid=?id";
                    //paramList.Add(new MySqlParameter("?encryptMacID", encryptMacID));
                    //paramList.Add(new MySqlParameter("?id", ID));
                    //MySqlParameter[] param2 = paramList.ToArray();
                    //cmd.CommandText = sql2;
                    //if (param2 != null)
                    //{
                    //    cmd.Parameters.Clear();
                    //    cmd.zParameters.AddRange(param2);
                    //}
                    //cmd.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {

                    }
                    throw e;
                }

            }
        }
        /// <summary>
        /// 执行查询语句，返回dataset
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteQuery(string sql, MySqlParameter[] parameters)
        {
            log.Debug("ExecuteQuery执行SQL：" + sql);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, connection);
                    if (parameters != null) da.SelectCommand.Parameters.AddRange(parameters);
                    da.Fill(ds, "ds");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return ds;
            }
        }
        public static DataSet ExecuteQuery(string sql)
        {
            return ExecuteQuery(sql, null);
        }
    }

}
