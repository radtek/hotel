using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class TradingInfoDal:BaseDal<TradingInfoModel>
    {
        public override bool Exist(TradingInfoModel bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(TradingInfoModel bean)
        {
            try
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
                if (!string.IsNullOrEmpty(bean.TradingType))
                {
                    sql1.Append(" TradingType,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.TradingType);
                }
                if (!string.IsNullOrEmpty(bean.RoomNumber))
                {
                    sql1.Append(" RoomNumber,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.RoomNumber);
                }
                if (!string.IsNullOrEmpty(bean.CheckOrderNumber))
                {
                    sql1.Append(" CheckOrderNumber,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.CheckOrderNumber);
                }
                if (!string.IsNullOrEmpty(bean.Temp1))
                {
                    sql1.Append(" Temp1,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.Temp1);
                }
                if (!string.IsNullOrEmpty(bean.Temp2))
                {
                    sql1.Append(" Temp2,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.Temp2);
                }
                if (!string.IsNullOrEmpty(bean.Temp3))
                {
                    sql1.Append(" Temp3,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.Temp3);
                }
                if (!string.IsNullOrEmpty(bean.ReturnCode))
                {
                    sql1.Append(" ReturnCode,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.ReturnCode);
                }
                if (!string.IsNullOrEmpty(bean.BankNumber))
                {
                    sql1.Append(" BankNumber,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.BankNumber);
                }
                if (!string.IsNullOrEmpty(bean.CardNumber))
                {
                    sql1.Append(" CardNumber,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.CardNumber);
                }
                if (!string.IsNullOrEmpty(bean.Valid))
                {
                    sql1.Append(" Valid,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.Valid);
                }
                if (!string.IsNullOrEmpty(bean.LotNo))
                {
                    sql1.Append(" LotNo,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.LotNo);
                }
                if (!string.IsNullOrEmpty(bean.CertificateNo))
                {
                    sql1.Append(" CertificateNo,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.CertificateNo);
                    Log.Debug(bean.CertificateNo);
                }
                if (!string.IsNullOrEmpty(bean.Money))
                {
                    sql1.Append(" Money,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.Money);
                    Log.Debug(bean.Money);
                }
                if (!string.IsNullOrEmpty(bean.Note))
                {
                    sql1.Append(" Note,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add("11");
                    Log.Debug(bean.Note);
                }
                if (!string.IsNullOrEmpty(bean.ContactNo))
                {
                    sql1.Append(" ContactNo,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.ContactNo);
                    Log.Debug(bean.ContactNo);
                }
                if (!string.IsNullOrEmpty(bean.TerminalNo))
                {
                    sql1.Append(" TerminalNo,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.TerminalNo);
                    Log.Debug(bean.TerminalNo);
                }
                if (!string.IsNullOrEmpty(bean.TransactionReferenceNumber))
                {
                    sql1.Append(" TransactionReferenceNumber,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.TransactionReferenceNumber);
                    Log.Debug(bean.TransactionReferenceNumber);
                }
                if (!string.IsNullOrEmpty(bean.TradingDate))
                {
                    sql1.Append(" TradingDate,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.TradingDate);
                    Log.Debug(bean.TradingDate);
                }
                if (!string.IsNullOrEmpty(bean.TradingTime))
                {
                    sql1.Append(" TradingTime,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.TradingTime);
                    Log.Debug(bean.TradingTime);
                }
                if (!string.IsNullOrEmpty(bean.AuthorizationNumber))
                {
                    sql1.Append(" AuthorizationNumber,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(bean.AuthorizationNumber);
                    Log.Debug(bean.AuthorizationNumber);
                }
                if (sql1.Length > 0)
                {
                    sql1 = sql1.Remove(sql1.Length - 1, 1);
                    sql2 = sql2.Remove(sql2.Length - 1, 1);
                }
                var sql = "insert into test_hotel_backsystem.t_TradingInfo(" + sql1 + ") values(" + sql2 + ")";
                Log.Debug("SQL :" + sql + ",params:" + list.ToString());
                mso.Execute(sql, list.ToArray());
            }
            catch (Exception e)
            {
                Log.Debug("插入出错信息:" + e.Message);
            }
        }

        public override void Del(TradingInfoModel bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(TradingInfoModel bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(TradingInfoModel bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(TradingInfoModel bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(TradingInfoModel bean, int page, int rows, ref int count)
        {
            Log.Debug("Modify方法参数：" + bean);
            var sqlCount = new StringBuilder();
            var sqlDt = new StringBuilder();
            sqlCount.Append("select count(*) count from test_hotel_backsystem.t_TradingInfo where 1=1 ");
            count = int.Parse(mso.GetScalar(sqlCount.ToString(), bean.Id).ToString());

            sqlDt.Append("SELECT * FROM test_hotel_backsystem.t_TradingInfo ");
            sqlDt.Append("limit " + (page - 1) * rows + "," + rows);
            return mso.GetDataTable(sqlDt.ToString(), bean.Id);
        }
    }
}