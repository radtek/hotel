using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;
using System.Text;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class PayStyleDal : BaseDal<PayStyle>
    {
        public override bool Exist(PayStyle bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(PayStyle bean)
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
            if (!string.IsNullOrEmpty(bean.CheckinId))
            {
                sql1.Append(" CheckinId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckinId);
            }
            if (!string.IsNullOrEmpty(bean.CheckinPmsCode))
            {
                sql1.Append(" CheckinPmsCode,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckinPmsCode);
            }
            if (!string.IsNullOrEmpty(bean.PayType))
            {
                sql1.Append(" PayType,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.PayType);
            }
            if (!string.IsNullOrEmpty(bean.PayWay))
            {
                sql1.Append(" PayWay,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.PayWay);
            }
            if (bean.PayMoney != 0.0f)
            {
                sql1.Append(" PayMoney,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.PayMoney);
            }
            if (!string.IsNullOrEmpty(bean.HepRecordId))
            {
                sql1.Append(" HepRecordId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.HepRecordId);
            }
            if (!string.IsNullOrEmpty(bean.DealRecordId))
            {
                sql1.Append(" DealRecordId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.DealRecordId);
            }
            if (!string.IsNullOrEmpty(bean.WorkNumPmsCode))
            {
                sql1.Append(" WorkNumPmsCode,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.WorkNumPmsCode);
            }
            if (bean.DealTime != new DateTime())
            {
                sql1.Append(" DealTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.DealTime);
            }
            if (!string.IsNullOrEmpty(bean.DealType))
            {
                sql1.Append(" DealType,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.DealType);
            }
            if (!string.IsNullOrEmpty(bean.CardNum))
            {
                sql1.Append(" CardNum,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CardNum);
            }
            if (!string.IsNullOrEmpty(bean.ValidityTime))
            {
                sql1.Append(" ValidityTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.ValidityTime);
            }
            if (bean.CheckinTime != new DateTime())
            {
                sql1.Append(" CheckinTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckinTime);
            }
            if (bean.CheckoutTime != new DateTime())
            {
                sql1.Append(" CheckoutTime,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.CheckoutTime);
            }
            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_PayStyle(" + sql1 + ") values(" + sql2 + ")";
            mso.Execute(sql, list.ToArray());
        }

        public override void Del(PayStyle bean)
        {
            Log.Debug("Del方法参数：" + bean.ToString());
            var strSql = new StringBuilder();
            strSql.Append(" delete from t_PayStyle where Id={0} ");
            var parameters = new object[] {
                bean.Id
            };
            try
            {
                mso.Execute(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                Log.Error("删除团购项目失败！", e);
            }
        }

        public override void Modify(PayStyle bean)
        {
            Log.Debug("Modify方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            sql.Append(" update t_PayStyle set ");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.CheckinId))
            {
                sql.Append(" CheckinId={" + i++ + "},");
                dList.Add(bean.CheckinId);
            }
            if (!string.IsNullOrEmpty(bean.CheckinPmsCode))
            {
                sql.Append(" CheckinPmsCode={" + i++ + "},");
                dList.Add(bean.CheckinPmsCode);
            }
            if (!string.IsNullOrEmpty(bean.PayType))
            {
                sql.Append(" PayType={" + i++ + "},");
                dList.Add(bean.PayType);
            }
            if (!string.IsNullOrEmpty(bean.PayWay))
            {
                sql.Append(" PayWay={" + i++ + "},");
                dList.Add(bean.PayWay);
            }
            if (bean.PayMoney != 0.0f)
            {
                sql.Append(" PayMoney={" + i++ + "},");
                dList.Add(bean.PayMoney);
            }
            if (!string.IsNullOrEmpty(bean.HepRecordId))
            {
                sql.Append(" HepRecordId={" + i++ + "},");
                dList.Add(bean.HepRecordId);
            }
            if (!string.IsNullOrEmpty(bean.DealRecordId))
            {
                sql.Append(" DealRecordId={" + i++ + "},");
                dList.Add(bean.DealRecordId);
            }
            if (!string.IsNullOrEmpty(bean.WorkNumPmsCode))
            {
                sql.Append(" WorkNumPmsCode={" + i++ + "},");
                dList.Add(bean.WorkNumPmsCode);
            }
            if (bean.DealTime != new DateTime())
            {
                sql.Append(" DealTime={" + i++ + "},");
                dList.Add(bean.DealTime);
            }
            if (!string.IsNullOrEmpty(bean.DealType))
            {
                sql.Append(" DealType={" + i++ + "},");
                dList.Add(bean.DealType);
            }
            if (!string.IsNullOrEmpty(bean.CardNum))
            {
                sql.Append(" CardNum={" + i++ + "},");
                dList.Add(bean.CardNum);
            }
            if (!string.IsNullOrEmpty(bean.ValidityTime))
            {
                sql.Append(" ValidityTime={" + i++ + "},");
                dList.Add(bean.ValidityTime);
            }
            if (bean.CheckinTime != new DateTime())
            {
                sql.Append(" CheckinTime={" + i++ + "},");
                dList.Add(bean.CheckinTime);
            }
            if (bean.CheckoutTime != new DateTime())
            {
                sql.Append(" CheckoutTime={" + i++ + "},");
                dList.Add(bean.CheckoutTime);
            }
            if (sql.Length > 0)
            {
                sql = sql.Remove(sql.Length - 1, 1);
            }
            sql.Append(" where Id={" + i++ + "}");
            dList.Add(bean.Id);
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            mso.Execute(sql.ToString(), dList.ToArray());
        }

        public override System.Data.DataTable Query(PayStyle bean)
        {
            Log.Debug("Query方法参数：" + bean.ToString());
            var sql = new StringBuilder();
            sql.Append(" select * from t_PayStyle where 1=1 ");
            var i = 0;
            var dList = new List<object>();
            if (!string.IsNullOrEmpty(bean.Id))
            {
                sql.Append(" and Id={" + i++ + "},");
                dList.Add(bean.Id);
            }
            if (!string.IsNullOrEmpty(bean.CheckinId))
            {
                sql.Append(" and CheckinId={" + i++ + "},");
                dList.Add(bean.CheckinId);
            }
            if (!string.IsNullOrEmpty(bean.CheckinPmsCode))
            {
                sql.Append(" and CheckinPmsCode={" + i++ + "},");
                dList.Add(bean.CheckinPmsCode);
            }
            if (!string.IsNullOrEmpty(bean.PayType))
            {
                sql.Append(" and PayType={" + i++ + "},");
                dList.Add(bean.PayType);
            }
            if (!string.IsNullOrEmpty(bean.PayWay))
            {
                sql.Append(" and PayWay={" + i++ + "},");
                dList.Add(bean.PayWay);
            }
            if (bean.PayMoney != 0.0f)
            {
                sql.Append(" and PayMoney={" + i++ + "},");
                dList.Add(bean.PayMoney);
            }
            if (!string.IsNullOrEmpty(bean.HepRecordId))
            {
                sql.Append(" and HepRecordId={" + i++ + "},");
                dList.Add(bean.HepRecordId);
            }
            if (!string.IsNullOrEmpty(bean.DealRecordId))
            {
                sql.Append(" and DealRecordId={" + i++ + "},");
                dList.Add(bean.DealRecordId);
            }
            if (!string.IsNullOrEmpty(bean.WorkNumPmsCode))
            {
                sql.Append(" and WorkNumPmsCode={" + i++ + "},");
                dList.Add(bean.WorkNumPmsCode);
            }
            if (bean.DealTime != new DateTime())
            {
                sql.Append(" and DealTime={" + i++ + "},");
                dList.Add(bean.DealTime);
            }
            if (!string.IsNullOrEmpty(bean.DealType))
            {
                sql.Append(" and DealType={" + i++ + "},");
                dList.Add(bean.DealType);
            }
            if (!string.IsNullOrEmpty(bean.CardNum))
            {
                sql.Append(" and CardNum={" + i++ + "},");
                dList.Add(bean.CardNum);
            }
            if (!string.IsNullOrEmpty(bean.ValidityTime))
            {
                sql.Append(" and ValidityTime={" + i++ + "},");
                dList.Add(bean.ValidityTime);
            }
            if (bean.CheckinTime != new DateTime())
            {
                sql.Append(" and CheckinTime={" + i++ + "},");
                dList.Add(bean.CheckinTime);
            }
            if (bean.CheckoutTime != new DateTime())
            {
                sql.Append(" and CheckoutTime={" + i++ + "},");
                dList.Add(bean.CheckoutTime);
            }
            if (sql.Length > 0)
            {
                sql = sql.Remove(sql.Length - 1, 1);
            }
            Log.Debug("SQL :" + sql + ",params:" + dList.ToString());
            return mso.GetDataTable(sql.ToString(), dList.ToArray());
        }

        public override System.Data.DataTable QueryByPage(PayStyle bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable QueryByPage(PayStyle bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}