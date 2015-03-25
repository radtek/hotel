using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class FilesDal : BaseDal<Files>
    {
        public override bool Exist(Files bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(Files bean)
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
            if (!string.IsNullOrEmpty(bean.Scid))
            {
                sql1.Append(" scid,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Scid);
            }
            if (!string.IsNullOrEmpty(bean.Type))
            {
                sql1.Append(" type,");
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
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Files(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(Files bean)
        {
            Log.Debug("del方法参数：");
            var sql = "delete from t_Files where Id={0}";
            mso.Execute(sql, bean.Id);
        }

        public override void Modify(Files bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Files set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.Scid))
            {
                sql.Append(" Scid={" + i++ + "},");
                dList.Add(bean.Scid);
            }
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
            if (!string.IsNullOrEmpty(bean.FileName))
            {
                sql.Append(" FileName={" + i++ + "},");
                dList.Add(bean.FileName);
            }
            if (!string.IsNullOrEmpty(bean.Extension))
            {
                sql.Append(" Extension={" + i++ + "},");
                dList.Add(bean.Extension);
            }
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "},");
                dList.Add(bean.CreateDt);
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override DataTable Query(Files bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("select * from t_Files where 1=1 ");

            if (!string.IsNullOrEmpty(bean.Id))
                sql.Append(" and Id={" + bean.Id + "}");

            if (!string.IsNullOrEmpty(bean.Scid))
                sql.Append(" and Scid={" + bean.Scid + "}");

            if (!string.IsNullOrEmpty(bean.Type))
                sql.Append(" and Type={" + bean.Type + "}");

            if (!string.IsNullOrEmpty(bean.Size))
                sql.Append(" and Size={" + bean.Size + "}");

            if (!string.IsNullOrEmpty(bean.FileName))
                sql.Append(" and FileName={" + bean.FileName + "}");

            if (!string.IsNullOrEmpty(bean.Extension))
                sql.Append(" and Extension={" + bean.Extension + "}");

            if (bean.CreateDt != null)
                sql.Append(" and CreateDt={" + bean.CreateDt + "}");
            Log.Debug("SQL :" + sql);
            return mso.GetDataTable(sql.ToString());
        }

        public override DataTable QueryByPage(Files bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(Files bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询文件
        /// </summary>
        /// <param name="scid"></param>
        /// <returns></returns>
        public DataTable FindByFiles(string scid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Id,Extension,Size,Type,FileName,Date_Format(CreateDt,'%Y-%m-%d %H:%i:%s') StrCreateDt ");
            sql.Append("from t_Files ");
            sql.Append("where scid={0} ");
            sql.Append("order by CreateDt desc");
            return mso.GetDataTable(sql.ToString(), scid);
        }


        public void ModifyByName(Files bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Files set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.Scid))
            {
                sql.Append(" Scid={" + i++ + "},");
                dList.Add(bean.Scid);
            }
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
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "},");
                dList.Add(bean.CreateDt);
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where FileName={" + i++ + "}");
            dList.Add(bean.FileName);
            mso.Execute(sql.ToString(), dList.ToArray());
        }
    }
}