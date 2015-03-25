using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HotelCheckIn_BackSystem.DataService.Model;
using HotelCheckIn_InterfaceSystem.model;
using CommonLibrary;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class RoomQualityDal : BaseDal<RoomQualityInfo>
    {
        public override bool Exist(RoomQualityInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(RoomQualityInfo bean)
        {
            Log.Debug("Add方法参数：" + bean.ToString());
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
            if (!string.IsNullOrEmpty(bean.Name))
            {
                sql1.Append(" Name,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Name);
            }
            if (!string.IsNullOrEmpty(bean.TypeId))
            {
                sql1.Append(" TypeId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.TypeId);
            }
            if (!string.IsNullOrEmpty(bean.Note))
            {
                sql1.Append(" Note,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.Note);
            }
            if (!string.IsNullOrEmpty(bean.HotelId))
            {
                sql1.Append(" HotelId,");
                sql2.Append(" {" + i++ + "},");
                list.Add(bean.HotelId);
            }

            if (sql1.Length > 0)
            {
                sql1 = sql1.Remove(sql1.Length - 1, 1);
                sql2 = sql2.Remove(sql2.Length - 1, 1);
            }
            var sql = "insert into t_RoomQuality(" + sql1 + ") values(" + sql2 + ")";
            Log.Debug("SQL :" + sql + ",params:" + list.ToString());
            mso.Execute(sql, list.ToArray());
        }

        /// <summary>
        /// 更新房间信息，以及房间属性信息
        /// </summary>
        /// <param name="roomList"></param>
        /// <param name="hotelId"></param>
        public bool UpdateRoomAndQualityInfo(RoomList roomList, string hotelId)
        {
            if (roomList == null)
            {
                return false;
            }
            var sqls = new List<string>();
            var paraList = new List<object[]>();
            sqls.AddRange(new string[] { "delete from t_RoomQuality", "delete from t_RoomInfo" });
            paraList.Add(new object[] { });
            paraList.Add(new object[] { });
            if (roomList.room_Buildings_List != null && roomList.room_Buildings_List.Count > 0)
            {
                foreach (DM dm in roomList.room_Buildings_List)
                {
                    AddToQuality(hotelId, dm, sqls, paraList, 2);
                }
            }
            if (roomList.room_Directions_List != null && roomList.room_Directions_List.Count > 0)
            {
                foreach (DM dm in roomList.room_Directions_List)
                {
                    AddToQuality(hotelId, dm, sqls, paraList, 4);
                }
            }
            if (roomList.room_Floors_List != null && roomList.room_Floors_List.Count > 0)
            {
                foreach (DM dm in roomList.room_Floors_List)
                {
                    AddToQuality(hotelId, dm, sqls, paraList, 3);
                }
            }
            if (roomList.room_Types_List != null && roomList.room_Types_List.Count > 0)
            {
                foreach (DM dm in roomList.room_Types_List)
                {
                    AddToQuality(hotelId, dm, sqls, paraList, 1);
                }
            }
            if (roomList.room_List != null && roomList.room_List.Count > 0)
            {
                foreach (Room room in roomList.room_List)
                {
                    AddToRoomInfo(hotelId, room, sqls, paraList);
                }
            }
            Transaction transaction = new Transaction();
            return transaction.Execute(sqls, paraList);
        }

        /// <summary>
        /// 向房间属性表添加数据
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="dm"></param>
        /// <param name="sqls"></param>
        /// <param name="paraList"></param>
        private static void AddToQuality(string hotelId, DM dm, List<string> sqls, List<object[]> paraList, int typeId)
        {
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var para = new List<object>();
            if (!string.IsNullOrEmpty(dm.Id))
            {
                sql1.Append(" id,");
                sql2.Append(" {" + i++ + "},");
                para.Add(dm.Id);
            }
            if (!string.IsNullOrEmpty(dm.Name))
            {
                sql1.Append(" Name,");
                sql2.Append(" {" + i++ + "},");
                para.Add(dm.Name);
            }
            sql1.Append(" TypeId, HotelId");
            sql2.Append(" {" + i++ + "}, {" + i++ + "}");
            para.AddRange(new object[] { typeId, hotelId });
            sqls.Add("insert into t_RoomQuality(" + sql1 + ") values(" + sql2 + ")");
            paraList.Add(para.ToArray());
        }

        /// <summary>
        /// 向房间表添加数据
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="room"></param>
        /// <param name="sqls"></param>
        /// <param name="paraList"></param>
        private static void AddToRoomInfo(string hotelId, Room room, List<string> sqls, List<object[]> paraList)
        {
            var sql1 = new StringBuilder();
            var sql2 = new StringBuilder();
            var i = 0;
            var para = new List<object>();
            if (!string.IsNullOrEmpty(room.room_Id))
            {
                sql1.Append(" roomid,");
                sql2.Append(" {" + i++ + "},");
                para.Add(room.room_Id);
            }
            if (!string.IsNullOrEmpty(room.room_No))
            {
                sql1.Append(" roomnum,");
                sql2.Append(" {" + i++ + "},");
                para.Add(room.room_No);
            }
            if (!string.IsNullOrEmpty(room.room_Type_Id))
            {
                sql1.Append(" roomTypeId,");
                sql2.Append(" {" + i++ + "},");
                para.Add(room.room_Type_Id);
            }
            if (!string.IsNullOrEmpty(room.room_Building_Id))
            {
                sql1.Append(" BuildingId,");
                sql2.Append(" {" + i++ + "},");
                para.Add(room.room_Building_Id);
            }
            if (!string.IsNullOrEmpty(room.room_Floor_Id))
            {
                sql1.Append(" FloorId,");
                sql2.Append(" {" + i++ + "},");
                para.Add(room.room_Floor_Id);
            }
            if (!string.IsNullOrEmpty(room.room_Direction_Id))
            {
                sql1.Append(" towardsid,");
                sql2.Append(" {" + i++ + "},");
                para.Add(room.room_Direction_Id);
            }
            sql1.Append("  HotelId");
            sql2.Append(" {" + i++ + "}");
            para.Add(hotelId);
            sqls.Add("insert into t_RoomInfo(" + sql1 + ") values(" + sql2 + ")");
            paraList.Add(para.ToArray());
        }

        public void Del(int str)
        {
            var sql = "delete from t_RoomQuality where TypeId='" + str + "'";
            Log.Debug("SQL :" + sql);
            mso.Execute(sql); ;
        }

        public override void Del(RoomQualityInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(RoomQualityInfo bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(RoomQualityInfo bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(RoomQualityInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(RoomQualityInfo bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}