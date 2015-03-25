using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class MachineDal : BaseDal<MachineInfo>
    {
        public override bool Exist(MachineInfo bean)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            var list = new List<string>();
            sql.Append("select jqid ");
            sql.Append("from t_Machine ");
            sql.Append("where jqid={0}");
            list.Add(bean.JqId);
            list.Add(bean.Password);
            var dt = mso.GetDataTable(sql.ToString(), list.ToArray());
            if (dt == null)
            {
                return false;
            }
            return dt.Rows.Count > 0;
        }

        public bool Exist(MachineInfo bean, ref string url)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            var list = new List<string>();
            sql.Append("select jqid,MaterialUrl ");
            sql.Append("from t_Machine ");
            sql.Append("where jqid={0} and `Password`={1} ");
            list.Add(bean.JqId);
            list.Add(bean.Password);
            var dt = mso.GetDataTable(sql.ToString(), list.ToArray());
            if (dt.Rows.Count <= 0)
            {
                url = null;
                return false;
            }
            url = dt.Rows[0]["MaterialUrl"].ToString();
            return true;
        }

        public override void Add(MachineInfo bean)
        {
            Log.Debug("Add方法参数：" + bean);
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.JqId))
            {
                sql1.Append(" jqId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.JqId);
            }
            if (!string.IsNullOrEmpty(bean.FaultId))
            {
                sql1.Append(" FaultID,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.FaultId);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql1.Append(" HotelId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql1.Append(" Name,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.IP))
            {
                sql1.Append(" IP,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IP);
            }
            if (bean.IscheckIDcard != null)
            {
                sql1.Append(" IscheckIDcard,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.IscheckIDcard);
            }
            if (bean.Isdisabled != null)
            {
                sql1.Append(" Isdisabled,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Isdisabled);
            }
            if (!string.IsNullOrEmpty(bean.MaterialUrl))
            {
                sql1.Append(" MaterialUrl,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.MaterialUrl);
            }
            if (bean.HeartbeatDt != null)
            {
                sql1.Append(" HeartbeatDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.HeartbeatDt);
            }
            if (!string.IsNullOrEmpty(bean.Status))
            {
                sql1.Append(" status,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Status);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql1.Append(" Note,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Note);
            }
            if (bean.CreateDt != null)
            {
                sql1.Append(" CreateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.Creater))
            {
                sql1.Append(" Creater,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Creater);
            }
            if (bean.UpdateDt != null)
            {
                sql1.Append(" UpdateDt,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.UpdateDt);
            }
            if (!string.IsNullOrEmpty(bean.UpdatePerson))
            {
                sql1.Append(" UpdatePerson,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.UpdatePerson);
            }
            if (!string.IsNullOrEmpty(bean.Password))
            {
                sql1.Append(" Password,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Password);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_Machine(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(MachineInfo bean)
        {
            Log.Debug("del方法参数：" + bean);
            var sql = "delete from t_Machine where jqId={0}";
            Log.Debug("SQL :" + sql);
            mso.Execute(sql, bean.JqId);
        }

        public override void Modify(MachineInfo bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Machine set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.FaultId))
            {
                sql.Append(" FaultID={" + i++ + "},");
                dList.Add(bean.FaultId);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql.Append(" HotelId={" + i++ + "},");
                dList.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" Name={" + i++ + "},");
                dList.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.IP))
            {
                sql.Append(" IP={" + i++ + "},");
                dList.Add(bean.IP);
            }
            if (bean.IscheckIDcard != null)
            {
                sql.Append(" IscheckIDcard={" + i++ + "},");
                dList.Add(bean.IscheckIDcard);
            }
            if (bean.Isdisabled != null)
            {
                sql.Append(" Isdisabled={" + i++ + "},");
                dList.Add(bean.Isdisabled);
            }
            if (!string.IsNullOrEmpty(bean.MaterialUrl))
            {
                sql.Append(" MaterialUrl={" + i++ + "},");
                dList.Add(bean.MaterialUrl);
            }
            if (bean.HeartbeatDt != null)
            {
                sql.Append(" HeartbeatDt={" + i++ + "},");
                dList.Add(bean.HeartbeatDt);
            }
            if (!string.IsNullOrEmpty(bean.Status))
            {
                sql.Append(" status={" + i++ + "},");
                dList.Add(bean.Status);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql.Append(" Note={" + i++ + "},");
                dList.Add(bean.Note);
            }
            if (bean.CreateDt != null)
            {
                sql.Append(" CreateDt={" + i++ + "},");
                dList.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.Creater))
            {
                sql.Append(" Creater={" + i++ + "},");
                dList.Add(bean.Creater);
            }
            if (bean.UpdateDt != null)
            {
                sql.Append(" UpdateDt={" + i++ + "},");
                dList.Add(bean.UpdateDt);
            }
            if (!string.IsNullOrEmpty(bean.UpdatePerson))
            {
                sql.Append(" UpdatePerson={" + i++ + "},");
                dList.Add(bean.UpdatePerson);
            }
            if (!string.IsNullOrEmpty(bean.Password))
            {
                sql.Append(" Password={" + i++ + "},");
                dList.Add(bean.Password);
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where jqId={" + i++ + "}");
            dList.Add(bean.JqId);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public string Modify(MachineInfo bean, ref object[] objects)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("update t_Machine set");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.FaultId))
            {
                sql.Append(" FaultID={" + i++ + "},");
                dList.Add(bean.FaultId);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql.Append(" HotelId={" + i++ + "},");
                dList.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql.Append(" Name={" + i++ + "},");
                dList.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.IP))
            {
                sql.Append(" IP={" + i++ + "},");
                dList.Add(bean.IP);
            }
            if (bean.IscheckIDcard != null)
            {
                sql.Append(" IscheckIDcard={" + i++ + "},");
                dList.Add(bean.IscheckIDcard);
            }
            if (bean.Isdisabled != null)
            {
                sql.Append(" Isdisabled={" + i++ + "},");
                dList.Add(bean.Isdisabled);
            }
            if (!string.IsNullOrEmpty(bean.MaterialUrl))
            {
                sql.Append(" MaterialUrl={" + i++ + "},");
                dList.Add(bean.MaterialUrl);
            }
            if (bean.HeartbeatDt != DateTime.MinValue)
            {
                sql.Append(" HeartbeatDt={" + i++ + "},");
                dList.Add(bean.HeartbeatDt);
            }
            if (!string.IsNullOrEmpty(bean.Status))
            {
                sql.Append(" status={" + i++ + "},");
                dList.Add(bean.Status);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql.Append(" Note={" + i++ + "},");
                dList.Add(bean.Note);
            }
            if (bean.CreateDt != DateTime.MinValue)
            {
                sql.Append(" CreateDt={" + i++ + "},");
                dList.Add(bean.CreateDt);
            }
            if (!string.IsNullOrEmpty(bean.Creater))
            {
                sql.Append(" Creater={" + i++ + "},");
                dList.Add(bean.Creater);
            }
            if (bean.UpdateDt != DateTime.MinValue)
            {
                sql.Append(" UpdateDt={" + i++ + "},");
                dList.Add(bean.UpdateDt);
            }
            if (!string.IsNullOrEmpty(bean.UpdatePerson))
            {
                sql.Append(" UpdatePerson={" + i++ + "},");
                dList.Add(bean.UpdatePerson);
            }
            if (!string.IsNullOrEmpty(bean.Password))
            {
                sql.Append(" Password={" + i++ + "},");
                dList.Add(bean.Password);
            }
            sql = sql.Remove(sql.Length - 1, 1);
            sql.Append(" where jqid={" + i++ + "}");
            dList.Add(bean.JqId);
            Log.Debug("SQL :" + sql + ",params:" + dList);
            objects = dList.ToArray();
            return sql.ToString();
        }

        public override DataTable Query(MachineInfo bean)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select HeartbeatDt ");
            sql.Append("from t_Machine ");
            sql.Append("where jqid={0} ");
            return mso.GetDataTable(sql.ToString(), bean.JqId);
        }

        public DataTable QueryName(string jqid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select Name ");
            sql.Append("from t_Machine ");
            sql.Append("where jqid={0} ");
            return mso.GetDataTable(sql.ToString(), jqid);
        }

        public override DataTable QueryByPage(MachineInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public DataTable QueryByPage(string qyid, string jdid, int index, int datacount, ref int sum)
        {
            Log.Debug("Modify方法参数：");
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            var sqlwhere = new StringBuilder();
            var list = new List<object>();
            var i = 0;
            if (!string.IsNullOrEmpty(qyid))
            {
                sqlwhere.Append(" and th.AreaId={" + i++ + "} ");
                list.Add(qyid);
            }
            if (!string.IsNullOrEmpty(jdid))
            {
                sqlwhere.Append(" and HotelId={" + i + "} ");
                list.Add(jdid);
            }
            sqlwhere.Append(" ORDER BY tm.HotelId ");

            sqlCount.Append("select count(*) count from t_Machine tm inner join t_Hotels th on tm.HotelId=th.id  where 1=1 ");
            sqlCount.Append(sqlwhere);
            sum = int.Parse(mso.GetScalar(sqlCount.ToString(), list.ToArray()).ToString());

            sqlDt.Append("select jqId,FaultID,Creater,tm.UpdatePerson,`Password`,tm.`Name`,IP,IscheckIDcard,Isdisabled,MaterialUrl,HeartbeatDt,`status`,tm.Note, ");
            sqlDt.Append("(SELECT `Name` FROM t_Hotels th where th.Id=tm.HotelId) HotelName, ");
            sqlDt.Append("Date_Format(tm.CreateDt,'%Y-%m-%d %H:%i:%s') CreateDtPara,Date_Format(tm.UpdateDt,'%Y-%m-%d %H:%i:%s') UpdateDtPara,Date_Format(HeartbeatDt,'%Y-%m-%d %H:%i:%s') HeartbeatDtPara ");
            sqlDt.Append("from t_Machine tm inner join t_Hotels th on tm.HotelId=th.id where 1=1 ");
            sqlDt.Append(sqlwhere);
            sqlDt.Append("limit " + (index - 1) * datacount + "," + datacount);
            return mso.GetDataTable(sqlDt.ToString(), list.ToArray());
        }


        public override DataTable QueryByPage(MachineInfo bean, int index, int datacount, ref int sum)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            var sqlwhere = new StringBuilder();
            sqlwhere.Append(!string.IsNullOrEmpty(bean.HotelId) ? " and HotelId={0} " : " ORDER BY tm.HotelId ");
            sqlCount.Append("select count(*) count from t_Machine tm where 1=1 ");
            sqlCount.Append(sqlwhere);
            sum = int.Parse(mso.GetScalar(sqlCount.ToString(), bean.HotelId).ToString());

            sqlDt.Append("SELECT jqId,FaultID,Creater,UpdatePerson,`Password`,`Name`,IP,IscheckIDcard,Isdisabled,MaterialUrl,HeartbeatDt,`status`,Note, ");
            sqlDt.Append("(SELECT `Name` FROM t_Hotels th where th.Id=tm.HotelId) HotelName, ");
            sqlDt.Append("Date_Format(CreateDt,'%Y-%m-%d %H:%i:%s') CreateDtPara,Date_Format(UpdateDt,'%Y-%m-%d %H:%i:%s') UpdateDtPara,Date_Format(HeartbeatDt,'%Y-%m-%d %H:%i:%s') HeartbeatDtPara ");
            sqlDt.Append("from t_Machine tm where 1=1 ");
            sqlDt.Append(sqlwhere);
            sqlDt.Append("limit " + (index - 1) * datacount + "," + datacount);
            return mso.GetDataTable(sqlDt.ToString(), bean.HotelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jqid">机器id</param>
        /// <param name="now">当前时间</param>
        /// <param name="timespan">时间间隔</param>
        /// <returns></returns>
        public bool IsTimeOut(string jqid, DateTime now, int timespan)
        {
            var dt = Query(new MachineInfo() { JqId = jqid });
            if (dt.Rows.Count <= 0)
            {
                return false;
            }
            DateTime xtsj = string.IsNullOrEmpty(dt.Rows[0]["HeartbeatDt"].ToString())
                                ? DateTime.MinValue
                                : Convert.ToDateTime(dt.Rows[0]["HeartbeatDt"]);
            TimeSpan ts = now - xtsj;
            var result = ts.TotalMinutes;
            return result >= timespan;
        }

        /// <summary>
        /// 根据机器id查询素材url
        /// </summary>
        /// <param name="jqid"></param>
        /// <returns></returns>
        public string FindByMasterialUrl(string jqid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("select MaterialUrl ");
            sql.Append("from t_Machine ");
            sql.Append("where jqid={0} ");
            var dt = mso.GetDataTable(sql.ToString(), jqid);
            return dt.Rows.Count > 0 ? dt.Rows[0]["MaterialUrl"].ToString() : null;
        }

        /// <summary>
        /// 根据机器id查询素材url
        /// </summary>
        /// <param name="jqid"></param>
        /// <returns></returns>
        public DataTable GetMachineInfo(string jqid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("SELECT t.jqId,t.HotelId,t.`Name`,t.IP,t.Note,t.`Password`,");
            sql.Append("(SELECT th.AreaId FROM t_Hotels th where th.Id=t.HotelId) areaid ");
            sql.Append("from t_Machine t ");
            sql.Append("where jqid={0} ");
            return mso.GetDataTable(sql.ToString(), jqid);
        }

        /// <summary>
        /// 主页查询(每隔一段时间)
        /// </summary>
        /// <param name="bean"></param>
        /// <param name="index"></param>
        /// <param name="datacount"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public DataTable FindByMain(MachineInfo bean, int index, int datacount, ref int sum)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            sqlCount.Append("select count(*) count from t_Machine tm where 1=1 ");
            sum = int.Parse(mso.GetScalar(sqlCount.ToString(), bean.HotelId).ToString());

            sqlDt.Append("SELECT tm.jqId,tm.`Name`,tm.HeartbeatDt,tm.IP,tm.Note,tm.IscheckIDcard, ");
            sqlDt.Append("Date_Format(CreateDt,'%Y-%m-%d %H:%i:%s') CreateDtPara,Date_Format(UpdateDt,'%Y-%m-%d %H:%i:%s') UpdateDtPara,Date_Format(HeartbeatDt,'%Y-%m-%d %H:%i:%s') HeartbeatDtPara ");
            sqlDt.Append("from t_Machine tm where 1=1 ");
            sqlDt.Append("order by tm.HeartbeatDt desc ");
            sqlDt.Append("limit " + (index - 1) * datacount + "," + datacount);
            return mso.GetDataTable(sqlDt.ToString(), bean.HotelId);
        }
        /// <summary>
        /// 根据机器id查询素材url
        /// </summary>
        /// <param name="jqid"></param>
        /// <returns></returns>
        public DataTable GetMachine(string jqid)
        {
            Log.Debug("Query方法参数：");
            var sql = new StringBuilder();
            sql.Append("SELECT t.jqId,t.HotelId,t.`Name`,t.IP,t.Note,t.`Password`,t.isdisabled,");
            sql.Append("(SELECT th.AreaId FROM t_Hotels th where th.Id=t.HotelId) areaid ");
            sql.Append("from t_Machine t ");
            sql.Append("where jqid={0} ");
            return mso.GetDataTable(sql.ToString(), jqid);
        }
    }
}