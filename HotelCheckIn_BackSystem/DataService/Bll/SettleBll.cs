using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Bll
{
    public class SettleBll
    {
        private static ILog _log = log4net.LogManager.GetLogger("SettleBll");
        private readonly SettleDal _stdal = new SettleDal();
        public SettleBll()
        {
        }

        /// <summary>
        /// 查询结算记录列表
        /// </summary>
        /// <param name="settle"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<Settle> QuerySettleList(Settle settle, int page, int rows, ref int total)
        {
            settle.QEndTime = settle.QEndTime.AddDays(1);
            total = _stdal.QuerySettleRows(settle);
            var seList = new List<Settle>();

            DataTable dt = _stdal.QuerySettle(settle, page, rows);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var se = new Settle();
                    DataRow dr = dt.Rows[i];
                    if (dr["BeginTime"].ToString() == "" || dr["BeginTime"] == null)
                        se.BeginTime = new DateTime();
                    else
                        se.BeginTime = DateTime.Parse(dr["BeginTime"].ToString());
                    if (dr["EndTime"].ToString() == "" || dr["EndTime"] == null)
                        se.EndTime = new DateTime();
                    else
                        se.EndTime = DateTime.Parse(dr["EndTime"].ToString());
                    se.InMoney = dr["InMoney"].ToString() == "" ? 0 : decimal.Parse(dr["InMoney"].ToString());
                    se.OutMoney = dr["OutMoney"].ToString() == "" ? 0 : decimal.Parse(dr["OutMoney"].ToString());
                    se.SumMoney = dr["SumMoney"].ToString() == "" ? 0 : decimal.Parse(dr["SumMoney"].ToString());
                    if (dr["SettleDateTime"].ToString() == "" || dr["SettleDateTime"] == null)
                        se.SettleDateTime = new DateTime();
                    else
                        se.SettleDateTime = DateTime.Parse(dr["SettleDateTime"].ToString());
                    se.OptId = dr["OptId"].ToString();
                    se.OptName = dr["OptName"].ToString();
                    seList.Add(se);
                }
            }
            return seList;
        }

        /// <summary>
        /// 查询最近的结算时间
        /// </summary>
        /// <returns></returns>
        public DataTable QueryNewSettleTime()
        {
            return _stdal.QueryNewSettleTime();
        }

        /// <summary>
        /// 查询流水账
        /// </summary>
        /// <param name="iobean"></param>
        /// <returns></returns>
        public DataTable QueryIojournal(IoJournal iobean)
        {
            return _stdal.QueryIojournal(iobean);
        }

        /// <summary>
        /// 添加结算记录
        /// </summary>
        /// <param name="bean"></param>
        public void AddSettle(Settle bean)
        {
            _stdal.AddSettle(bean);
        }

    }

}