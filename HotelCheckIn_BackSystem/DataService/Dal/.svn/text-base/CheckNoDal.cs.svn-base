using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HotelCheckIn_BackSystem.DataService.Model;
using MySql.Data.MySqlClient;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Dal
{

    public class CheckNoDal : BaseDal<CheckNoInfo>
    {
        private new static ILog _log = log4net.LogManager.GetLogger("CheckNoDal");
        public CheckNoDal()
        { }

        /// <summary>
        /// 验证码验证
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryCheckNoAndPj(CheckNoInfo bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder();
            sql.Append("select t.*,p.* from t_CheckNo t left join t_Project p "
                + "on p.InternetGroupID=t.InternetGroupId and p.ProjectFrontNum=t.CheckID_Front  where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                sql.Append(" and CheckId={" + ++i + "}");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql.Append(" and p.InternetGroupId={" + ++i + "}");
                list.Add(bean.InternetGroupId);
            }
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 查询验证码是否有效
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryCheckNo(CheckNoInfo bean)
        {
            Log.Debug("QueryCheckNo方法参数：CheckId:" + bean.CheckId + ",InternetGroupId:" + bean.InternetGroupId);
            var sql = new StringBuilder(@"SELECT tc.*,tp.remarks note FROM t_CheckNo tc left join t_Project tp on
tp.internetgroupid=tc.internetgroupid and tp.projectfrontnum=tc.checkid_front where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                sql.Append(" and tc.CheckId={" + ++i + "}");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql.Append(" and tc.InternetGroupId={" + ++i + "}");
                list.Add(bean.InternetGroupId);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql.Append(" and tp.hotelid={" + ++i + "}");
                list.Add(bean.HotelId);
            }
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 查询掩码是否锁定
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryCheckNoIsKnock(CheckNoInfo bean)
        {
            Log.Debug("QueryCheckNoIsKnock方法参数：CheckId:" + bean.CheckId);
            var sql = new StringBuilder("SELECT * FROM t_RoomLockInfo where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                sql.Append(" and CheckId={" + ++i + "}");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.PhoneNumber))
            {
                sql.Append(" and PhoneNumber={" + ++i + "}");
                list.Add(bean.PhoneNumber);
            }
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 根据id查询验证码
        /// </summary>
        /// <param name="checkid"></param>
        /// <returns></returns>
        public DataTable QueryCheckId(string checkid)
        {
            Log.Debug("Query方法参数：" + checkid);
            var sql = new StringBuilder();
            sql.Append("select t.* from t_CheckNo t where t.checkid={0} ");
            var list = new List<object> { checkid };
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 根据验证码前缀查询验证码
        /// </summary>
        /// <param name="checkidfront"></param>
        /// <returns></returns>
        public DataTable QueryNoByFront(string checkidfront)
        {
            Log.Debug("Query方法参数：" + checkidfront);
            var sql = new StringBuilder();
            sql.Append(" select t.* from t_CheckNo t where t.CheckID_Front={0} ");
            var list = new List<object> { checkidfront };
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 导出查询未验证验证码
        /// </summary>
        /// <param name="tgs"></param>
        /// <param name="xmmc"></param>
        /// <param name="machinecheck"></param>
        /// <param name="groupcheck"></param>
        /// <param name="dcyzmsl"></param>
        /// <returns></returns>
        public DataTable ExpQueryNo(string tgs, string xmmc, int machinecheck, int groupcheck,string dcyzmsl)
        {
            Log.Debug("Query方法参数：" + tgs + "," + xmmc);
            var sql = new StringBuilder();
            sql.Append(" select t.* from t_CheckNo t where t.InternetGroupID={0} and t.CheckID_Front={1} ");
            sql.Append(" and t.MachineCheck={2} and t.InternetCheck={3} order by t.CreateDateTime desc limit "+dcyzmsl);
            var list = new List<object> {tgs, xmmc,machinecheck,groupcheck };
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 导出查询未验证验证码的数量
        /// </summary>
        /// <param name="tgs"></param>
        /// <param name="xmmc"></param>
        /// <param name="machinecheck"></param>
        /// <param name="groupcheck"></param>
        /// <returns></returns>
        public DataTable GetCheckIdNo(string tgs, string xmmc, int machinecheck, int groupcheck)
        {
            Log.Debug("Query方法参数：" + tgs + "," + xmmc);
            var sql = new StringBuilder();
            sql.Append(" select count(*) as YesCheckIdNo from t_CheckNo t where t.InternetGroupID={0} and t.CheckID_Front={1} ");
            sql.Append(" and t.MachineCheck={2} and t.InternetCheck={3} order by t.CreateDateTime desc ");
            var list = new List<object> { tgs, xmmc, machinecheck, groupcheck };
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        /// <summary>
        /// 查询验证码
        /// </summary>
        /// <param name="qnpj"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable QueryCheckNo(QueryNoAndPj qnpj, int page, int rows)
        {
            _log.Debug("方法Query接收参数：团购商id:" + qnpj.InternetGroupId + "-前缀码:" + qnpj.ProjectFrontNum
                + "验证状态:" + qnpj.CheckQuery + "生成日期区间:" + qnpj.CreateTimeBegin + qnpj.CreateTimeEnd
                + "-page:" + page + "-rows:" + rows);
            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select t.* from t_CheckNo t where 1=1  ");
            var paramList = new List<object>();
            if (!string.IsNullOrEmpty(qnpj.CheckNo))
            {
                sql.Append(" and t.CheckID={" + i++ + "} ");
                paramList.Add(qnpj.CheckNo);
            }
            if (!string.IsNullOrEmpty(qnpj.InternetGroupId))
            {
                sql.Append(" and t.InternetGroupID={" + i++ + "} ");
                paramList.Add(qnpj.InternetGroupId);
            }
            if (!string.IsNullOrEmpty(qnpj.ProjectFrontNum))
            {
                sql.Append(" and t.CheckID_Front={" + i++ + "} ");
                paramList.Add(qnpj.ProjectFrontNum);
            }
            if (qnpj.CheckQuery == "未验证")
            {
                sql.Append(" and t.MachineCheck=1 and InternetCheck=1 ");
            }
            else if (qnpj.CheckQuery == "终端验证")
            {
                sql.Append(" and t.MachineCheck=2 ");
            }
            else if (qnpj.CheckQuery == "团购验证")
            {
                sql.Append(" and t.InternetCheck=2 ");
            }
            if (qnpj.CreateTimeBegin != new DateTime() && qnpj.CreateTimeEnd != new DateTime())
            {
                sql.Append(" and t.CreateDateTime between {" + i++ + "} and {" + i++ + "} ");
                paramList.Add(qnpj.CreateTimeBegin);
                paramList.Add(qnpj.CreateTimeEnd);
            }
            sql.Append(" order by CreateDateTime desc limit " + (page - 1) * rows + "," + rows);
            try
            {
                return mso.GetDataTable(sql.ToString(), paramList.ToArray());
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }

        /// <summary>
        /// 查询验证码行数
        /// </summary>
        /// <param name="qnpj"></param>
        /// <returns></returns>
        public int QueryCheckNoRows(QueryNoAndPj qnpj)
        {
            _log.Debug("方法Query接收参数：团购商id:" + qnpj.InternetGroupId + "-前缀码:" + qnpj.ProjectFrontNum
                + "验证状态:" + qnpj.CheckQuery + "生成日期区间:" + qnpj.CreateTimeBegin + qnpj.CreateTimeEnd);
            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select count(*) from t_CheckNo t where 1=1 ");
            var paramList = new List<object>() ;
            if (!string.IsNullOrEmpty(qnpj.CheckNo))
            {
                sql.Append(" and t.CheckID={" + i++ + "} ");
                paramList.Add(qnpj.CheckNo);
            }
            if (!string.IsNullOrEmpty(qnpj.InternetGroupId))
            {
                sql.Append(" and t.InternetGroupID={" + i++ + "} ");
                paramList.Add(qnpj.InternetGroupId);
            }
            if (!string.IsNullOrEmpty(qnpj.ProjectFrontNum))
            {
                sql.Append(" and t.CheckID_Front={" + i++ + "} ");
                paramList.Add(qnpj.ProjectFrontNum);
            }
            if (qnpj.CheckQuery == "未验证")
            {
                sql.Append(" and t.MachineCheck=1 and InternetCheck=1 ");
            }
            else if (qnpj.CheckQuery == "终端验证")
            {
                sql.Append(" and t.MachineCheck=2 ");
            }
            else if (qnpj.CheckQuery == "团购验证")
            {
                sql.Append(" and t.InternetCheck=2 ");
            }
            if (qnpj.CreateTimeBegin != new DateTime() && qnpj.CreateTimeEnd != new DateTime())
            {
                sql.Append(" and t.CreateDateTime between {" + i++ + "} and {" + i++ + "} ");
                paramList.Add(qnpj.CreateTimeBegin);
                paramList.Add(qnpj.CreateTimeEnd);
            }
            try
            {
                var sum = int.Parse(mso.GetScalar(sql.ToString(), paramList.ToArray()).ToString());
                return sum;
            }
            catch (Exception e)
            {
                _log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }
        /// <summary>
        /// 添加验证码
        /// </summary>
        /// <param name="bean"></param>
        public void AddCheckNo(CheckNoInfo bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var list = new List<object>();
            if (!string.IsNullOrEmpty(bean.CheckId))
            {
                sql1.Append(" CheckId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckId);
            }
            if (!string.IsNullOrEmpty(bean.CheckID_Front))
            {
                sql1.Append(" CheckID_Front,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckID_Front);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroupId))
            {
                sql1.Append(" InternetGroupId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.InternetGroupId);
            }
            if (!string.IsNullOrEmpty(bean.InternetGroup))
            {
                sql1.Append(" InternetGroup,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.InternetGroup);
            }
            if (bean.CheckIdBeginTime != new DateTime())
            {
                sql1.Append(" CheckIdBeginTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckIdBeginTime);
            }
            if (bean.CheckIdEndTime != new DateTime())
            {
                sql1.Append(" CheckIdEndTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckIdEndTime);
            }

            if (!string.IsNullOrEmpty(bean.CreatePeople))
            {
                sql1.Append(" CreatePeople,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CreatePeople);
            }
            sql1.Append(" MachineCheck,InternetCheck,InSumDate,");
            sql2.Append(" {" + i++ + "},{" + i++ + "},{" + i++ + "},");
            list.Add(bean.MachineCheck);
            list.Add(bean.InternetCheck);
            list.Add(bean.InSumDate);
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_CheckNo(" + sql1 + ",CreateDateTime) values(" + sql2 + ",now())";
            mso.Execute(sql, list.ToArray());
        }

        /// <summary>
        /// 解锁——更改验证码终端验证状态
        /// </summary>
        /// <param name="bean"></param>
        public void EditCheckNoUnlock(CheckNoInfo bean)
        {
            Log.Debug("Modify方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            var i = 0;
            var dList = new List<object>();
            sql.Append(" update t_CheckNo set MachineCheck={" + i++ + "},MachineCheckPeople={" + i++ + "} where CheckId={" + i++ + "} ");
            dList.Add(bean.MachineCheck);
            dList.Add(bean.MachineCheckPeople);
            dList.Add(bean.CheckId);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        /// <summary>
        /// 团购验证—修改团购验证状态,验证时间，验证人
        /// </summary>
        /// <param name="bean"></param>
        public void EditCheckNoIntnetCheck(CheckNoInfo bean)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sql = new StringBuilder();
            var i = 0;
            var dList = new List<object>();
            sql.Append(" update t_CheckNo set InternetCheck={" + i++ + "},InternetCheckDateTime=now(),InternetCheckPeople={" + i++ + "} where CheckId={" + i++ + "} ");
            dList.Add(bean.InternetCheck);
            dList.Add(bean.InternetCheckPeople);
            dList.Add(bean.CheckId);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override bool Exist(CheckNoInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(CheckNoInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(CheckNoInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(CheckNoInfo bean)
        {
            Log.Debug("Modify方法参数：" + bean.ToString());
            var sql = "update t_CheckNo set";
            var i = 0;
            var dList = new List<object>();
            if (bean.MachineCheck != 0)
            {
                sql += " MachineCheck={" + i++ + "}";
                dList.Add(bean.MachineCheck);
            }
            sql += " where CheckId={" + i++ + "}";
            dList.Add(bean.CheckId);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql, dList.ToArray());
        }

        public override System.Data.DataTable Query(CheckNoInfo bean)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(CheckNoInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(CheckNoInfo bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}