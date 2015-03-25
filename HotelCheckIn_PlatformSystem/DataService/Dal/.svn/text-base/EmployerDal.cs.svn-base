using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class EmployerDal : BaseDal<Employer>
    {
        public override bool Exist(Employer bean)
        {
            Log.Debug("exist方法" + bean);
            var sql = "select * from t_Employer t where deptid='" + bean.DeptId + "'";
            Log.Debug("SQL :" + sql);
            var dt = mso.GetDataTable(sql);
            return dt.Rows.Count > 0;
        }

        public bool Exist2(Employer bean)
        {
            Log.Debug("exist方法");
            var sql = "select * from t_Employer t where name='" + bean.Name + "' and deptid='" + bean.DeptId + "'";
            var dt = mso.GetDataTable(sql);
            return dt.Rows.Count > 0;
        }

        public bool Exist3(Employer bean)
        {
            Log.Debug("exist方法");
            var sql = "select * from t_Employer t where name='" + bean.Name + "' and deptid !='" + bean.DeptId + "'";
            var dt = mso.GetDataTable(sql);
            return dt.Rows.Count > 0;
        }

        public bool Exist4(Employer bean)
        {
            Log.Debug("exist方法");
            var sql = "select * from t_Employer t where worknum='" + bean.WorkNum + "'";
            var dt = mso.GetDataTable(sql);
            return dt.Rows.Count > 0;
        }

        public bool Exist5(Employer bean)
        {
            Log.Debug("exist方法");
            var sql = "select * from t_Employer t where worknum='" + bean.WorkNum + "' and id !='" + bean.Id + "'";
            var dt = mso.GetDataTable(sql);
            return dt.Rows.Count > 0;
        }

        public override void Add(Employer bean)
        {

            Log.Debug("Add方法接收的参数：" + bean.ToString());
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql1.Append(" id,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.WorkNum))
            {
                sql1.Append(" WorkNum,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.WorkNum);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql1.Append(" name,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.DeptId))
            {
                sql1.Append(" deptid,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.DeptId);
            }
            if (!string.IsNullOrEmpty(bean.PhoneNum))
            {
                sql1.Append(" PhoneNum,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.PhoneNum);
            }
            if (!string.IsNullOrEmpty(bean.Email))
            {
                sql1.Append(" Email,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Email);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }

            var sql = "insert into t_Employer(" + sql1 + ") values(" + sql2 + ")";
            Log.Debug("SQL :" + sql + ",params:" + list.ToString());
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(Employer bean)
        {
            throw new NotImplementedException();
        }

        public void DelEmployee(string id)
        {
            Log.Debug("del方法接收的参数：");
            var sql = "delete from t_Employer where id='" + id + "'";
            mso.Execute(sql);
        }

        public override void Modify(Employer employer)
        {
            Log.Debug("更新人员");
            var sql = "update t_Employer set password='" + employer.Password + "' where WorkNum='" + employer.WorkNum + "'";
            mso.Execute(sql);
        }

        public void ModifyEmail(Employer bean)
        {
            Log.Debug("ModifyById方法参数：" + bean.ToString());
            string sql = "update t_Employer set email={0} where id={1}";
            mso.Execute(sql, bean.Email, bean.Id);
        }

        public void UpdateEmployee(Employer employer)
        {
            Log.Debug("更新人员");
            var sql = "update t_Employer set worknum='" + employer.WorkNum +
                "',name='" + employer.Name +
                "',phonenum='" + employer.PhoneNum +
                "',email='" + employer.Email +
                "' where id='" + employer.Id + "'";
            mso.Execute(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public override DataTable Query(Employer bean)
        {
            Log.Debug("Query方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            sql.Append("select * from t_Employer where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql.Append(" and id={" + ++i + "}");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.WorkNum))
            {
                sql.Append(" and WorkNum={" + ++i + "}");
                list.Add(bean.WorkNum);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" and name={" + ++i + "}");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.DeptId))
            {
                sql.Append(" and deptid={" + ++i + "}");
                list.Add(bean.DeptId);
            }
            if (!string.IsNullOrEmpty(bean.PhoneNum))
            {
                sql.Append(" and PhoneNum={" + ++i + "}");
                list.Add(bean.PhoneNum);
            }
            if (!string.IsNullOrEmpty(bean.Email))
            {
                sql.Append(" and email={" + ++i + "}");
                list.Add(bean.Email);
            }
            if (!string.IsNullOrEmpty(bean.Password))
            {
                sql.Append(" and PassWord={" + ++i + "}");
                list.Add(bean.Password);
            }
            Log.Debug(string.Format("SQL :{0},params:{1}", sql, list));
            return mso.GetDataTable(sql.ToString(), list.ToArray());
            //return mso.GetDataTable(sql.ToString(), list.ToArray());
        }


        public DataTable QueryEmp(Employer bean)
        {
            Log.Debug("QueryNode方法参数：" + bean.ToString());
            var sql2 = "select t.*,(select name from t_Department tt where t.deptid=tt.id) deptname from t_Employer t where deptid='" + bean.DeptId + "'";
            return mso.GetDataTable(sql2);
        }
        /// <summary>
        /// 查询不是根结点的所有数据
        /// </summary>
        /// <returns></returns>
        public DataTable QueryNode()
        {
            Log.Debug("QueryNode方法参数：");
            var sql = new StringBuilder();
            sql.Append("select * from t_Department where parentdept is not null  ");
            sql.Append(" union select id,name,deptid from t_Employer where deptid is not null");
            return mso.GetDataTable(sql.ToString());
        }

        /// <summary>
        /// 查询子节点下所有节点(包括部门和人员)
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable QueryNodeTree(string parentId)
        {
            Log.Debug("QueryNodeTree方法参数：");
            var strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append("SELECT id,name,parentdept FROM t_Department ");
            strSql.Append("start with id='" + parentId + "' connect by prior id = parentdept ) t");
            strSql.Append("  where id!='" + parentId + "'");
            strSql.Append(" union select * from (SELECT id,name,deptid");
            strSql.Append(" FROM t_Employer where deptid in(SELECT id FROM t_Department start with id='" + parentId + "' connect by prior id = parentdept )) ");
            return mso.GetDataTable(strSql.ToString());
        }

        /// <summary>
        /// 查询子节点下所有节点(包括部门和人员)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public DataTable QueryNodeTree2(Employer employee)
        {
            Log.Debug("QueryNodeTree方法参数：");
            var strSql = new StringBuilder();
            strSql.Append("select t.id from t_Employer t ");
            strSql.Append("where deptid='" + employee.DeptId + "' ");
            strSql.Append("union select tt.id from t_Department tt ");
            strSql.Append("where tt.parentdept='" + employee.DeptId + "'");
            return mso.GetDataTable(strSql.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bean"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public override DataTable QueryByPage(Employer bean, int page, int rows)
        {
            Log.Debug("Query方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            sql.Append("select * from t_Employer where 1=1 ");
            var list = new List<object>();
            var i = 0;
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql.Append(" and id={" + ++i + "}");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.WorkNum))
            {
                sql.Append(" and WorkNum={" + ++i + "}");
                list.Add(bean.WorkNum);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" and name={" + ++i + "}");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.DeptId))
            {
                sql.Append(" and deptid={" + ++i + "}");
                list.Add(bean.DeptId);
            }
            if (!string.IsNullOrEmpty(bean.PhoneNum))
            {
                sql.Append(" and PhoneNum={" + ++i + "}");
                list.Add(bean.PhoneNum);
            }
            if (!string.IsNullOrEmpty(bean.Email))
            {
                sql.Append(" and email={" + ++i + "}");
                list.Add(bean.Email);
            }
            return mso.GetDataTableByPage(sql.ToString(), page * rows, rows, list.ToArray());
        }

        public override DataTable QueryByPage(Employer bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}
