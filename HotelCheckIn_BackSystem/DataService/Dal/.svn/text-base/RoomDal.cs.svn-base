using System;
using System.Collections.Generic;
using System.Data;
using HotelCheckIn_BackSystem.DataService.Model;
using System.Text;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class RoomDal : BaseDal<RoomInfo>
    {
        public override bool Exist(RoomInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(RoomInfo bean)
        {
            throw new NotImplementedException();
        }

        public void Add(List<RoomInfo> roomInfos)
        {
            
            var i = 0;
            var list = new List<object>();
           
            foreach (var roomInfo in roomInfos)
            {
                var sql = "";
                var sql1 = new StringBuilder();
                var sql2 = new StringBuilder();
                if (!string.IsNullOrEmpty(roomInfo.RoomNum))
                {
                    sql1.Append(" RoomNum,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.RoomNum);
                }
                if (!string.IsNullOrEmpty(roomInfo.HotelId))
                {
                    sql1.Append(" HotelId,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.HotelId);
                }
                if (!string.IsNullOrEmpty(roomInfo.RoomId))
                {
                    sql1.Append(" RoomId,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.RoomId);
                }
                if (!string.IsNullOrEmpty(roomInfo.RoomTypeId))
                {
                    sql1.Append(" RoomTypeId,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.RoomTypeId);
                }
                if (!string.IsNullOrEmpty(roomInfo.BuildingId))
                {
                    sql1.Append(" BuildingId,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.BuildingId);
                }
                if (!string.IsNullOrEmpty(roomInfo.FloorId))
                {
                    sql1.Append(" FloorId,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.FloorId);
                }
                if (!string.IsNullOrEmpty(roomInfo.TowardsId))
                {
                    sql1.Append(" TowardsId,");
                    sql2.Append(" {" + i++ + "},");
                    list.Add(roomInfo.TowardsId);
                }
                if (sql1.Length > 0)
                {
                    sql1 = sql1.Remove(sql1.Length - 1, 1);
                    sql2 = sql2.Remove(sql2.Length - 1, 1);
                }
                sql = "insert into t_RoomInfo(" + sql1 + ") values(" + sql2 + ");";
                mso.Execute(sql, list.ToArray());
            }
        }

        public void Del()
        {
            var sql = "delete from t_RoomInfo";
            Log.Debug("SQL :" + sql);
            mso.Execute(sql);
        }

        public override void Del(RoomInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(RoomInfo bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(RoomInfo bean)
        {
            Log.Debug("Query方法参数：" + bean);
            var sql = new StringBuilder(@"SELECT ri.*,
(SELECT name FROM t_RoomQuality where id=ri.roomtypeid and typeid=1 and hotelid=ri.hotelid) roomtype, 
(SELECT name FROM t_RoomQuality where id=ri.buildingid and typeid=2 and hotelid=ri.hotelid) building,
(SELECT name FROM t_RoomQuality where id=ri.floorid and typeid=3 and hotelid=ri.hotelid) floor,
(SELECT name FROM t_RoomQuality where id=ri.towardsid and typeid=4 and hotelid=ri.hotelid) towards
FROM t_RoomInfo ri where 1=1 ");
            var list = new List<object>();
            var i = -1;
            if (!string.IsNullOrEmpty(bean.RoomNum))
            {
                sql.Append(" and ri.RoomNum={" + ++i + "}");
                list.Add(bean.RoomNum);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql.Append(" and ri.HotelId={" + ++i + "}");
                list.Add(bean.HotelId);
            }
            if (!string.IsNullOrEmpty(bean.RoomId))
            {
                sql.Append(" and ri.RoomId={" + ++i + "}");
                list.Add(bean.RoomId);
            }
            if (!string.IsNullOrEmpty(bean.RoomTypeId))
            {
                sql.Append(" and ri.RoomTypeId={" + ++i + "}");
                list.Add(bean.RoomTypeId);
            }
            if (!string.IsNullOrEmpty(bean.BuildingId))
            {
                sql.Append(" and ri.BuildingId={" + ++i + "}");
                list.Add(bean.BuildingId);
            }
            if (!string.IsNullOrEmpty(bean.FloorId))
            {
                sql.Append(" and ri.FloorId={" + ++i + "}");
                list.Add(bean.FloorId);
            }
            if (!string.IsNullOrEmpty(bean.TowardsId))
            {
                sql.Append(" and ri.TowardsId={" + ++i + "}");
                list.Add(bean.TowardsId);
            }
            Log.Debug("sql:" + sql + ",params:" + list);
            return mso.GetDataTable(sql.ToString(), list.ToArray());
        }

        public override DataTable QueryByPage(RoomInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(RoomInfo bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}