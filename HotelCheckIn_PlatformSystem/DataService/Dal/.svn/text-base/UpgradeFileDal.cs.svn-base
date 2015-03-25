using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Bll;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class UpgradeFileDal : BaseDal<UpgradeFile>
    {
        public override bool Exist(UpgradeFile bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(UpgradeFile bean)
        {
            Log.Debug("Add方法参数：" + bean);
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql1.Append(" Id,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.Type))
            {
                sql1.Append(" Type,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Type);
            }
            if (!string.IsNullOrEmpty(bean.Size))
            {
                sql1.Append(" Size,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Size);
            }
            if (!string.IsNullOrEmpty(bean.FileName))
            {
                sql1.Append(" FileName,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.FileName);
            }
            if (!string.IsNullOrEmpty(bean.Extension))
            {
                sql1.Append(" Extension,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Extension);
            }
            if (!string.IsNullOrEmpty(bean.Url))
            {
                sql1.Append(" Url,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Url);
            }
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.CreatePerson))
            {
                sql1.Append(" CreatePerson,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreatePerson);
            }

            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_UpgradeFile(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(UpgradeFile bean)
        {
            Log.Debug("del方法参数：");
            var sql = "delete from t_UpgradeFile where Id={0}";
            mso.Execute(sql, bean.Id);
        }

        public override void Modify(UpgradeFile bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_UpgradeFile set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.Type))
            {
                sql.Append(" Type={" + i++ + "},");
                dList.Add(bean.Type);
            }
            if (!string.IsNullOrEmpty(bean.Size))
            {
                sql.Append(" Size={" + i++ + "},");
                dList.Add(bean.Size);
            }
            if (!string.IsNullOrEmpty(bean.Extension))
            {
                sql.Append(" Extension={" + i++ + "},");
                dList.Add(bean.Extension);
            }
            if (!string.IsNullOrEmpty(bean.Url))
            {
                sql.Append(" Url={" + i++ + "},");
                dList.Add(bean.Url);
            }
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "},");
                dList.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.CreatePerson))
            {
                sql.Append(" CreatePerson={" + i++ + "},");
                dList.Add(bean.CreatePerson);
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where FileName={" + i++ + "}");
            dList.Add(bean.FileName);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override DataTable Query(UpgradeFile bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(UpgradeFile bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(UpgradeFile bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询升级文件
        /// </summary>
        /// <param name="dataPage"></param>
        /// <returns></returns>
        public Dictionary<int, DataTable> QueryUpgradeFile(DataPage dataPage)
        {
            Log.Debug("Modify方法参数：");
            var dic = new Dictionary<int, DataTable>();
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            var list = new List<object>();

            sqlCount.Append("select count(*) count from t_UpgradeFile tuf ");
            dataPage.Sum = int.Parse(mso.GetScalar(sqlCount.ToString()).ToString());

            sqlDt.Append("SELECT tuf.Id,tuf.type,tuf.Size,tuf.FileName,tuf.Extension,tuf.url,tuf.CreatePerson, ");
            sqlDt.Append("Date_Format( tuf.CreateDt,'%Y-%m-%d %H:%i:%s') CreateDtPara ");
            sqlDt.Append("FROM t_UpgradeFile tuf  ");
            sqlDt.Append("order by tuf.CreateDt desc ");
            sqlDt.Append("limit " + (dataPage.Index - 1) * dataPage.Count + "," + dataPage.Count);
            var dt = mso.GetDataTable(sqlDt.ToString(), list.ToArray());
            dic.Add(dataPage.Sum, dt);
            return dic;
        }

        /// <summary>
        /// 根据升级文件名查找升级文件id
        /// </summary>
        /// <param name="upgradename"></param>
        /// <returns></returns>
        public string QueryUpgradeId(string upgradename)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Id ");
            sql.Append("from t_UpgradeFile ");
            sql.Append("where FileName={0} ");
            var dt = mso.GetDataTable(sql.ToString(), upgradename);
            return dt.Rows[0]["Id"].ToString();
        }

        /// <summary>
        /// 查询升级文件
        /// </summary>
        /// <returns></returns>
        public DataTable QueryUpgradeFile(string jqid)
        {
            Log.Debug("Modify方法参数：");
            var sqlDt = new StringBuilder();
            var list = new List<object>();
            sqlDt.Append("SELECT tuf.Id,tuf.FileName,tuf.type,tuf.url,tum.MachineId,tum.IsDownland,tum.IsFlag ");
            sqlDt.Append("FROM t_UpgradeFile tuf  ");
            sqlDt.Append("LEFT join (SELECT t.* from  t_Upgrade_Machine t where t.MachineId={0} ) tum on tuf.Id=tum.UpgradeFileId ");
            sqlDt.Append("ORDER BY tuf.CreateDt desc ");
            list.Add(jqid);
            var dt = mso.GetDataTable(sqlDt.ToString(), list.ToArray());
            return dt;
        }


        /// <summary>
        /// 根据升级文件名和扩展名查找升级文件id
        /// </summary>
        /// <returns></returns>
        public string QueryUpgradeId(string name, string extension)
        {
            Log.Debug("Query方法参数：");
            var list = new List<object>();
            var sql = new StringBuilder();
            sql.Append("select Id ");
            sql.Append("from t_UpgradeFile ");
            sql.Append("where FileName={0} and Extension={1} ");
            list.Add(name);
            list.Add(extension);
            var dt = mso.GetDataTable(sql.ToString(), list.ToArray());
            return dt.Rows[0]["Id"].ToString();
        }
    }
}