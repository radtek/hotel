using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HotelCheckIn_BackSystem.DataService.BLL;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class MenuPowerDal : BaseDal<MenuPower>
    {
        public override bool Exist(MenuPower bean)
        {
            Log.Debug("exist方法" + bean);
            var sql = new StringBuilder();
            sql.Append("select * from t_Menu_Power t where menuid='" + bean.MenuId);
            sql.Append("' and powerid='" + bean.PowerId + "'");
            Log.Debug("SQL :" + sql);
            var dt = mso.GetDataTable(sql.ToString());
            return dt.Rows.Count > 0;
        }

        public override void Add(MenuPower bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
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
            if (!string.IsNullOrEmpty(bean.MenuId))
            {
                sql1.Append(" MenuId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.MenuId);
            }

            if (!string.IsNullOrEmpty(bean.PowerId))
            {
                sql1.Append(" PowerId,");
                sql2.Append(" {" + i + "},");
                list.Add(bean.PowerId);
            }

            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }

            var sql = "insert into t_Menu_Power(" + sql1 + ") values(" + sql2 + ")";
            Log.Debug("SQL :" + sql + ",params:" + list.ToString());
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(MenuPower bean)
        {
            Log.Debug("del方法参数：" + bean);
            var sql = "delete from t_Menu_Power where powerid='" + bean.PowerId + "'";
            Log.Debug("SQL :" + sql);
            mso.Execute(sql);
        }

        public override void Modify(MenuPower bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Menu_Power set");
            var i = 0;
            var dList = new List<object>();

            if (string.IsNullOrEmpty(bean.MenuId))
            {
                sql.Append(" MenuId={" + i++ + "}");
                dList.Add(bean.MenuId);
            }

            if (string.IsNullOrEmpty(bean.PowerId))
            {
                sql.Append(" PowerId={" + i++ + "}");
                dList.Add(bean.PowerId);
            }

            sql.Append(" where Id={" + i + "}");
            dList.Add(bean.Id);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());

        }

        public override DataTable Query(MenuPower bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("select * from t_Menu_Power where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql.Append(" and Id={" + ++i + "}");
                list.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.MenuId))
            {
                sql.Append(" and MenuId={" + ++i + "}");
                list.Add(bean.MenuId);
            }

            if (!string.IsNullOrEmpty(bean.PowerId))
            {
                sql.Append(" and PowerId={" + ++i + "}");
                list.Add(bean.PowerId);
            }
            Log.Debug("SQL :" + sql + ",params:" + list.ToString());
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        public DataTable QueryMenuByRole(string roleid)
        {
            string sql = @"select t.*,(select caption from t_ResMenu t_m where t_m.resid=t.parentid) parentname,
(select caption from t_ResMenu t_m where t_m.resid=(select parentid from t_ResMenu t_m1 where t_m1.resid=t.parentid)) ppname 
from t_ResMenu t where  t.resid in ( select resid from t_ResMenu srm inner join t_menu_power tmp on tmp.menuid=srm.resid 
inner join t_Role sr on sr.roleid=tmp.powerid inner join t_User_Roles sur on sur.roleid = sr.roleid 
inner join t_Employer te on te.id=sur.loginname where te.id='" + roleid + "') order by t.resid";
            return mso.GetDataTable(sql);
        }


        /// <summary>
        /// 查询子菜单
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public DataTable QuerySubMenu(string parentid)
        {
            var sql = new StringBuilder();
            sql.Append("select t.*,( ");
            sql.Append("select caption from t_ResMenu t_m where t_m.resid=t.parentid) parentname,(  ");
            sql.Append("select caption from t_ResMenu t_m where t_m.resid=( ");
            sql.Append("select parentid from t_ResMenu t_m1 where t_m1.resid=t.parentid)) ppname  ");
            sql.Append("from t_ResMenu t where  t.resid in (  ");
            sql.Append("select resid from t_ResMenu srm inner join t_Menu_Power tmp on tmp.menuid=srm.resid  ");
            sql.Append("inner join t_Role sr on sr.roleid=tmp.powerid inner join t_User_Roles sur on sur.roleid = sr.roleid   ");
            sql.Append("inner join t_Employer te on te.id=sur.loginname where t.resId={0})  order by t.resid ");
            return mso.GetDataTable(sql.ToString(), parentid);
        }

        /// <summary>
        /// 查询主菜单
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMainMenu(string roleid)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT t_rm.ResId,t_rm.Caption,t_rm.parentId,t_rm.Href ");
            sql.Append("from t_ResMenu t_rm where t_rm.resId in ( ");
            sql.Append("SELECT DISTINCT(t.parentId) from t_ResMenu t where t.ResId in (SELECT t_mp.MenuId from t_Menu_Power t_mp ");
            sql.Append("WHERE t_mp.PowerId in(SELECT t_ur.RoleId from t_User_Roles t_ur where t_ur.LoginName={0}))) ");
            sql.Append("ORDER BY t_rm.ResId ");
            return mso.GetDataTable(sql.ToString(), roleid);
        }


        /// <summary>
        /// 我新添加的方法，我就是张威
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryMenuSub(string id)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT t_rm.ResId,t_rm.Caption,t_rm.parentId,t_rm.Href  ");
            sql.Append("from t_ResMenu t_rm where t_rm.resId  ");
            sql.Append("in (SELECT t_mp.MenuId 		from t_Menu_Power t_mp		WHERE t_mp.PowerId 		in( ");
            sql.Append("SELECT t_ur.RoleId 			from t_User_Roles t_ur 			where t_ur.LoginName={0})) ");
            sql.Append("ORDER BY t_rm.ResId ");
            return mso.GetDataTable(sql.ToString(), id);
        }
        public override DataTable QueryByPage(MenuPower bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(MenuPower bean, int page, int rows, ref int count)
        {
            Log.Debug("QueryByPage方法参数：" + bean);
            var getpage = new GetPage();
            var sql = new StringBuilder();
            sql.Append("select * from t_Menu_Power where 1=1 ");

            if (!string.IsNullOrEmpty(bean.Id))
                sql.Append(" and Id={" + bean.Id + "}");

            if (!string.IsNullOrEmpty(bean.MenuId))
                sql.Append(" and MenuId={" + bean.MenuId + "}");

            if (!string.IsNullOrEmpty(bean.PowerId))
                sql.Append(" and PowerId={" + bean.PowerId + "}");
            Log.Debug("SQL :" + sql);
            return getpage.GetPageByProcedure(page, rows, sql.ToString(), ref count);
        }
    }
}
