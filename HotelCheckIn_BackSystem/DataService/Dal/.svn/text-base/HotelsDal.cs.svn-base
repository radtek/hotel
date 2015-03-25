using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class HotelsDal:BaseDal<Hotels>
    {
        public override bool Exist(Hotels bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Hotels bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(Hotels bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(Hotels bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(Hotels bean)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select * ");
            sql.Append("from t_Hotels ");
            sql.Append("where Id={0} ");
            return mso.GetDataTable(sql.ToString(), bean.Id);
        }

        public override DataTable QueryByPage(Hotels bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Hotels bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据区域id获取酒店
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public DataTable FindByHotelsByAreaId(string areaid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Id,Name ");
            sql.Append("from t_Hotels ");
            sql.Append("where AreaId={0} ");
            return mso.GetDataTable(sql.ToString(), areaid);
        }
    }
}