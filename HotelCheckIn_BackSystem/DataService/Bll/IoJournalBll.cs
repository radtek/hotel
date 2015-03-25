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
    public class IoJournalBll
    {
        private static ILog _log = log4net.LogManager.GetLogger("IoJournalBll");
        readonly IoJournalDal _iodal = new IoJournalDal();
        readonly MachineDal _madal = new MachineDal();
        public IoJournalBll()
        { }

        /// <summary>
        /// 多条件查询流水账
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public DataTable QueryIoBean(IoJournal bean)
        {
            return _iodal.QueryIoBean(bean);
        }

        /// <summary>
        /// 添加流水账记录
        /// </summary>
        /// <param name="bean"></param>
        public void AddIoJournal(IoJournal bean)
        {
            _iodal.AddIoJournal(bean);
        }

        /// <summary>
        /// 删除方法——更改流水账作废状态
        /// </summary>
        /// <param name="bean"></param>
        public void DelIoJournal(IoJournal bean)
        {
            _iodal.DelIoJournal(bean);
        }

        /// <summary>
        /// 查询流水账
        /// </summary>
        /// <param name="iobean"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<IoJournal> QueryIojournal(IoJournal iobean, int page, int rows, ref int total)
        {
            if (iobean.QuerySign!=1)
            {
                iobean.EndTime = iobean.EndTime.AddDays(1);
            }
            total = _iodal.QueryIojournalRows(iobean);
            var ckList = new List<IoJournal>();

            DataTable dt = _iodal.QueryIojournal(iobean, page, rows);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var io = new IoJournal();
                    DataRow dr = dt.Rows[i];
                    io.IoId = dr["IoId"].ToString();
                    io.IoName = dr["IoName"].ToString();
                    if (dr["IoTime"].ToString() == "" || dr["IoTime"] == null)
                        io.IoTime = new DateTime();
                    else
                        io.IoTime = DateTime.Parse(dr["IoTime"].ToString());
                    io.IoMoney = dr["IoMoney"].ToString() == "" ? 0 : decimal.Parse(dr["IoMoney"].ToString());
                    io.IoTag = dr["IoTag"].ToString() == "" ? 0 : int.Parse(dr["IoTag"].ToString());
                    io.IsUse = dr["IsUse"].ToString() == "" ? 0 : int.Parse(dr["IsUse"].ToString());
                    io.IoFrom = dr["IoFrom"].ToString() == "" ? 0 : int.Parse(dr["IoFrom"].ToString());
                    io.OrderId = dr["OrderId"].ToString();
                    io.RoomNo = dr["RoomNo"].ToString();
                    io.InOrOutCard = dr["InOrOutCard"].ToString() == "" ? 0 : int.Parse(dr["InOrOutCard"].ToString());
                    ckList.Add(io);
                }
            }
            return ckList;
        }

        /// <summary>
        /// 根据机器id查询机器名称
        /// </summary>
        /// <param name="jqid"></param>
        /// <returns></returns>
        public DataTable QueryName(string jqid)
        {
            return _madal.QueryName(jqid);
        }

        /// <summary>
        /// 修改机器来源收入是否发卡状态
        /// </summary>
        /// <param name="bean"></param>
        public void EditIoIfCard(IoJournal bean)
        {
            _iodal.EditIoIfCard(bean);
        }
    }
}