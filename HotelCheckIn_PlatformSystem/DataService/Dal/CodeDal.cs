using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class CodeDal:BaseDal<Code>
    {
        public override bool Exist(Code bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Code bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(Code bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(Code bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(Code bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Code bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Code bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询酒店区域
        /// </summary>
        /// <returns></returns>
        public DataTable FindByArea()
        {
            Log.Debug("FindByArea方法参数：");
            var sql = new StringBuilder();
            sql.Append("SELECT Id,`Name`  ");
            sql.Append("FROM `t_Code` ");
            sql.Append("where type='jdqy' ");
            return mso.GetDataTable(sql.ToString());
        }

        /// <summary>
        /// 查询故障
        /// </summary>
        /// <returns></returns>
        public DataTable QueryFault()
        {
            Log.Debug("FindByArea方法参数：");
            var sql = new StringBuilder();
            sql.Append("SELECT Id,`Name`  ");
            sql.Append("FROM `t_Code` ");
            sql.Append("where type='gz' ");
            return mso.GetDataTable(sql.ToString());
        }
    }
}