﻿using System.Data;
using YOUO.Framework.DataAccess;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    /// <summary>
    /// 数据操作层基类，对于没有实现的方法，一定要抛出异常。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDal<T>
    {
        protected MySqlOperator mso = new MySqlOperator(System.Configuration.ConfigurationSettings.AppSettings["dbconnect"]);
        protected log4net.ILog Log = log4net.LogManager.GetLogger("dal");
        public abstract bool Exist(T bean);
        public abstract void Add(T bean);

        public abstract void Del(T bean);

        public abstract void Modify(T bean);

        public abstract DataTable Query(T bean);

        public abstract DataTable QueryByPage(T bean, int page, int rows);
        public abstract DataTable QueryByPage(T bean, int page, int rows, ref int count);
    }
}
