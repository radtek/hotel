using System;
using System.Collections.Generic;
using System.Data;
using YOUO.Framework.DataAccess;

namespace CommonLibrary
{
    public class Transaction
    {
        private MySqlOperator mso;
        protected log4net.ILog Log;
        public Transaction()
        {
            mso = new MySqlOperator(System.Configuration.ConfigurationSettings.AppSettings["dbconnect"]);
            Log = log4net.LogManager.GetLogger("Transaction");
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="lists"></param>
        /// <returns></returns>
        public bool Execute(List<string> sqls, List<object[]> lists)
        {
            try
            {
                mso.BeginTransaction();
                for (var i = 0; i < sqls.Count; i++)
                {
                    Log.Debug("sql:" + sqls[i]);
                    foreach (var o in lists[i])
                    {
                        Log.Debug("list:" + o);
                    }
                   
                    mso.Execute(sqls[i], lists[i]);
                }
                mso.Submit(true);
            }
            catch (Exception e)
            {
                mso.Rollback(true);
                Log.Debug("事务错误信息：" + e);
                return false;
            }
            return true;
        }


    }
}
