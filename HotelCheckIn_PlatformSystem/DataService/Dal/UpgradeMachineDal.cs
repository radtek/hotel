using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Dal
{
    public class UpgradeMachineDal : BaseDal<UpgradeMachine>
    {
        public override bool Exist(UpgradeMachine bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(UpgradeMachine bean)
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
            if (!string.IsNullOrEmpty(bean.UpgradeFileId))
            {
                sql1.Append(" UpgradeFileId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.UpgradeFileId);
            }
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql1.Append(" MachineId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.MachineId);
            }
            if (bean.IsFlag != null)
            {
                sql1.Append(" IsFlag,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IsFlag);
            }
            if (bean.IsDownland != null)
            {
                sql1.Append(" IsDownland,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IsDownland);
            }
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (bean.CreatePerson != null)
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
            var sql = "insert into t_Upgrade_Machine(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(UpgradeMachine bean)
        {
            Log.Debug("del方法参数：");
            var sql = "delete from t_Upgrade_Machine where Id={0}";
            mso.Execute(sql, bean.Id);
        }

        public override void Modify(UpgradeMachine bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Upgrade_Machine set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.UpgradeFileId))
            {
                sql.Append(" UpgradeFileId={" + i++ + "},");
                dList.Add(bean.UpgradeFileId);
            }
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql.Append(" MachineId={" + i++ + "},");
                dList.Add(bean.MachineId);
            }
            if (bean.IsFlag != null)
            {
                sql.Append(" IsFlag={" + i++ + "},");
                dList.Add(bean.IsFlag);
            }
            if (bean.IsDownland != null)
            {
                sql.Append(" IsDownland={" + i++ + "},");
                dList.Add(bean.IsDownland);
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
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override DataTable Query(UpgradeMachine bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(UpgradeMachine bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(UpgradeMachine bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据升级文件id删除升级机器表里的关联数据
        /// </summary>
        /// <param name="bean"></param>
        public void DelByUpgradeId(UpgradeMachine bean)
        {
            Log.Debug("del方法参数：");
            var sql = "delete from t_Upgrade_Machine where UpgradeFileId={0}";
            mso.Execute(sql, bean.UpgradeFileId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bean"></param>
        public void ModifyByUpgradeFileId(UpgradeMachine bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Upgrade_Machine set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.MachineId))
            {
                sql.Append(" MachineId={" + i++ + "},");
                dList.Add(bean.MachineId);
            }
            if (bean.IsFlag != null)
            {
                sql.Append(" IsFlag={" + i++ + "},");
                dList.Add(bean.IsFlag);
            }
            if (bean.IsDownland != null)
            {
                sql.Append(" IsDownland={" + i++ + "},");
                dList.Add(bean.IsDownland);
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
            sql.Append(" where UpgradeFileId={" + i++ + "}");
            dList.Add(bean.UpgradeFileId);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        /// <summary>
        /// 根据机器id和升级文件id删除升级机器数据
        /// </summary>
        /// <param name="jqid"></param>
        public void Del(string jqid)
        {
            Log.Debug("del方法参数：");
            var list = new List<string>();
            var sql = "delete from t_Upgrade_Machine where MachineId={0}";
            list.Add(jqid);
            mso.Execute(sql, list.ToArray());
        }

        /// <summary>
        /// 根据机器id返回升级机器
        /// </summary>
        /// <param name="jqid"></param>
        /// <returns></returns>
        public DataTable FindBy(string jqid)
        {
            Log.Debug("FindBy方法参数：");
            var list = new List<string>();
            var sql = new StringBuilder();
            sql.Append("SELECT tuf.FileName,tuf.Extension ");
            sql.Append("FROM t_Upgrade_Machine tum ");
            sql.Append("INNER JOIN t_UpgradeFile tuf ON tum.UpgradeFileId = tuf.Id ");
            sql.Append("WHERE tum.IsFlag=0 and tum.MachineId={0} ");
            list.Add(jqid);
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 根据机器id和文件列表(文件名)来更新isflag=1
        /// </summary>
        /// <param name="jqid"></param>
        /// <param name="bean"></param>
        public void Modify(string jqid, string fileid, UpgradeMachine bean)
        {
            Log.Debug("Modify方法参数：");
            var sql = new StringBuilder();
            sql.Append("update t_Upgrade_Machine set ");
            var i = 0;
            var dList = new List<object>();

            if (bean.IsFlag != null)
            {
                sql.Append(" IsFlag={" + i++ + "},");
                dList.Add(bean.IsFlag);
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where 1=1 ");
            if (!string.IsNullOrEmpty(jqid))
            {
                sql.Append(" and MachineId={" + i++ + "} ");
                dList.Add(jqid);
            }
            if (!string.IsNullOrEmpty(jqid))
            {
                sql.Append(" and UpgradeFileId in ('" + fileid + "') ");
            }
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }
    }
}