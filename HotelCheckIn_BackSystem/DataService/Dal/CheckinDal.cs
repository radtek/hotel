using System;
using System.Collections.Generic;
using System.Data;
using HotelCheckIn_BackSystem.DataService.Common;
using log4net;
using MySql.Data.MySqlClient;
using System.Text;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Dal
{
    public class CheckinDal : BaseDal<CheckinInfo>
    {
        private static ILog log = log4net.LogManager.GetLogger("CheckinDal");
        public CheckinDal()
        { }

        /// <summary>
        /// 客户入住信息录入数据库
        /// </summary>
        /// <param name="checkin"></param>
        /// <param name="customList"></param>
        /// <returns></returns>
        public bool UploadCheckinOrder(CheckinInfo checkin, List<CustomerInfo> customList)
        {
            /*
             * 向checkin_order、custom中添加数据，如果已经存在则删除之前的数据，重新添加。
             *  1.删除t_CheckinOrder表中重复数据。
             *  2.添加t_CheckinOrder表中数据。
             *  3.删除t_Customer表中重复数据。
             *  4.添加t_Customer表中数据。
             * 
             * */
            List<string> sqlList = new List<string>();
            List<MySqlParameter[]> paramList = new List<MySqlParameter[]>();

            StringBuilder sql2Checkin = new StringBuilder(" values (");
            StringBuilder sql1Checkin = new StringBuilder("insert into t_CheckinOrder (");
            List<MySqlParameter> paramList1 = new List<MySqlParameter>();
            checkin.OrderId = string.IsNullOrEmpty(checkin.OrderId) ? Guid.NewGuid().ToString() : checkin.OrderId;
            if (!string.IsNullOrEmpty(checkin.OrderId))
            {
                //删除Orderid已经存在的t_CheckinOrder数据
                string sql_delete = "delete from t_CheckinOrder where Orderid=?orderid";
                MySqlParameter[] param = new MySqlParameter[] { new MySqlParameter("?orderid", checkin.OrderId) };
                sqlList.Add(sql_delete);
                paramList.Add(param);
                sql1Checkin.Append("id,");
                sql2Checkin.Append("?id,");
                string id = string.IsNullOrEmpty(checkin.Id) ? Guid.NewGuid().ToString() : checkin.Id;
                paramList1.Add(new MySqlParameter("?id", id));
                sql1Checkin.Append("Orderid,");
                sql2Checkin.Append("?Orderid,");
                paramList1.Add(new MySqlParameter("?Orderid", checkin.OrderId));
            }
            if (!string.IsNullOrEmpty(checkin.CheckinCode))
            {
                sql1Checkin.Append("CheckinCode,");
                sql2Checkin.Append("?CheckinCode,");
                paramList1.Add(new MySqlParameter("?CheckinCode", checkin.CheckinCode));
            }
            if (!string.IsNullOrEmpty(checkin.HotelId))
            {
                sql1Checkin.Append("HotelId,");
                sql2Checkin.Append("?HotelId,");
                paramList1.Add(new MySqlParameter("?HotelId", checkin.HotelId));
            }
            if (!string.IsNullOrEmpty(checkin.RoomNum))
            {
                sql1Checkin.Append("Roomnum,");
                sql2Checkin.Append("?Roomnum,");
                paramList1.Add(new MySqlParameter("?Roomnum", checkin.RoomNum));
            }
            if (!string.IsNullOrEmpty(checkin.RoomType))
            {
                sql1Checkin.Append("roomtype,");
                sql2Checkin.Append("?roomtype,");
                paramList1.Add(new MySqlParameter("?roomtype", checkin.RoomType));
            }
            if (!string.IsNullOrEmpty(checkin.Building))
            {
                sql1Checkin.Append("Building,");
                sql2Checkin.Append("?Building,");
                paramList1.Add(new MySqlParameter("?Building", checkin.Building));
            }
            if (!string.IsNullOrEmpty(checkin.RoomCode))
            {
                sql1Checkin.Append("Roomcode,");
                sql2Checkin.Append("?Roomcode,");
                paramList1.Add(new MySqlParameter("?Roomcode", checkin.RoomCode));
            }
            if (checkin.RoomRate != 0.0)
            {
                sql1Checkin.Append("Roomrate,");
                sql2Checkin.Append("?Roomrate,");
                paramList1.Add(new MySqlParameter("?Roomrate", checkin.RoomRate));
            }
            if (!string.IsNullOrEmpty(checkin.CheckinType))
            {
                sql1Checkin.Append("Checkintype,");
                sql2Checkin.Append("?Checkintype,");
                paramList1.Add(new MySqlParameter("?Checkintype", checkin.CheckinType));
            }
            if (!string.IsNullOrEmpty(checkin.ViPcardNum))
            {
                sql1Checkin.Append("VIPcardNum,");
                sql2Checkin.Append("?VIPcardNum,");
                paramList1.Add(new MySqlParameter("?VIPcardNum", checkin.ViPcardNum));
            }
            if (!string.IsNullOrEmpty(checkin.ViPcardType))
            {
                sql1Checkin.Append("VIPcardtype,");
                sql2Checkin.Append("?VIPcardtype,");
                paramList1.Add(new MySqlParameter("?VIPcardtype", checkin.ViPcardType));
            }
            if (checkin.PeopleNum != 0)
            {
                sql1Checkin.Append("PeopleNum,");
                sql2Checkin.Append("?PeopleNum,");
                paramList1.Add(new MySqlParameter("?PeopleNum", checkin.PeopleNum));
            }
            if (DateTime.MinValue != checkin.CheckinTime)
            {
                sql1Checkin.Append("Checkintime,");
                sql2Checkin.Append("?Checkintime,");
                paramList1.Add(new MySqlParameter("?Checkintime", checkin.CheckinTime));
            }
            if (checkin.Hours >= 0)
            {
                sql1Checkin.Append("hours,");
                sql2Checkin.Append("?hours,");
                paramList1.Add(new MySqlParameter("?hours", checkin.Hours));
            }
            if (checkin.AdvancePayment != 0.0)
            {
                sql1Checkin.Append("AdvancePayment,");
                sql2Checkin.Append("?AdvancePayment,");
                paramList1.Add(new MySqlParameter("?AdvancePayment", checkin.AdvancePayment));
            }
            if (!string.IsNullOrEmpty(checkin.AdvanceType))
            {
                sql1Checkin.Append("AdvanceType,");
                sql2Checkin.Append("?AdvanceType,");
                paramList1.Add(new MySqlParameter("?AdvanceType", checkin.AdvanceType));
            }
            if (!string.IsNullOrEmpty(checkin.MacId))
            {
                sql1Checkin.Append("Macid,");
                sql2Checkin.Append("?Macid,");
                paramList1.Add(new MySqlParameter("?Macid", checkin.MacId));
            }
            if (DateTime.MinValue != checkin.OrderTime)
            {
                sql1Checkin.Append("OrderTime,");
                sql2Checkin.Append("?OrderTime,");
                paramList1.Add(new MySqlParameter("?OrderTime", checkin.OrderTime));
            }
            if (!string.IsNullOrEmpty(checkin.CheckId))
            {
                sql1Checkin.Append("CheckId,");
                sql2Checkin.Append("?CheckId,");
                paramList1.Add(new MySqlParameter("?CheckId", checkin.CheckId));
            }
            if (!string.IsNullOrEmpty(checkin.InternetGroup))
            {
                sql1Checkin.Append("InternetGroup,");
                sql2Checkin.Append("?InternetGroup,");
                paramList1.Add(new MySqlParameter("?InternetGroup", checkin.InternetGroup));
            }
            if (!string.IsNullOrEmpty(checkin.PhoneNumber))
            {
                sql1Checkin.Append("PhoneNumber,");
                sql2Checkin.Append("?PhoneNumber,");
                paramList1.Add(new MySqlParameter("?PhoneNumber", checkin.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(checkin.PmsSign))
            {
                sql1Checkin.Append("PmsSign,");
                sql2Checkin.Append("?PmsSign,");
                paramList1.Add(new MySqlParameter("?PmsSign", checkin.PmsSign));
            }
            if (!string.IsNullOrEmpty(checkin.PayRecord))
            {
                sql1Checkin.Append("PayRecord,");
                sql2Checkin.Append("?PayRecord,");
                paramList1.Add(new MySqlParameter("?PayRecord", checkin.PayRecord));
            }
            if (!string.IsNullOrEmpty(checkin.Custom))
            {
                sql1Checkin.Append("Custom,");
                sql2Checkin.Append("?Custom,");
                paramList1.Add(new MySqlParameter("?Custom", checkin.Custom));
            }

            //if (!string.IsNullOrEmpty(checkin.CheckinImage))
            //{
            //    sql1Checkin.Append("Checkinimage");
            //    sql2Checkin.Append("?Checkinimage");
            //    paramList1.Add(new MySqlParameter("?Checkinimage", checkin.CheckinImage));
            //}
            if (sql1Checkin.Length > 0)
            {
                sql1Checkin.Remove(sql1Checkin.Length - 1, 1);
                sql2Checkin.Remove(sql2Checkin.Length - 1, 1);
            }
            sql1Checkin.Append(")");
            sql2Checkin.Append(")");
            string sqlCheckin = sql1Checkin.Append(sql2Checkin).ToString();
            sqlList.Add(sqlCheckin);
            paramList.Add(paramList1.ToArray());

            for (int i = 0; i < customList.Count; i++)
            {
                List<MySqlParameter> paramList2 = new List<MySqlParameter>();
                CustomerInfo customer = customList[i];
                StringBuilder sql1Custom = new StringBuilder("insert into t_Customer (");
                StringBuilder sql2Custom = new StringBuilder(" values (");
                sql1Custom.Append("id,");
                sql2Custom.Append("?id,");
                paramList2.Add(new MySqlParameter("?id", Guid.NewGuid()));
                if (!string.IsNullOrEmpty(checkin.OrderId))
                {
                    //删除t_Customer表中Orderid已经存在的Customer
                    string sql_delete2 = "delete from t_Customer where Orderid=?orderid and Identitycardnum=?idcard";
                    MySqlParameter[] param2 = new MySqlParameter[] { 
                        new MySqlParameter("?orderid", checkin.OrderId), 
                        new MySqlParameter("?idcard", customer.IdentityCardNum) 
                    };
                    sqlList.Add(sql_delete2);
                    paramList.Add(param2);
                    sql1Custom.Append("Orderid,");
                    sql2Custom.Append("?Orderid,");
                    paramList2.Add(new MySqlParameter("Orderid", checkin.OrderId));
                }
                if (!string.IsNullOrEmpty(customer.IdentityCardNum))
                {
                    sql1Custom.Append("Identitycardnum,");
                    sql2Custom.Append("?Identitycardnum,");
                    paramList2.Add(new MySqlParameter("Identitycardnum", customer.IdentityCardNum));
                }
                if (!string.IsNullOrEmpty(customer.Name))
                {
                    sql1Custom.Append("Name,");
                    sql2Custom.Append("?Name,");
                    paramList2.Add(new MySqlParameter("Name", customer.Name));
                }
                if (!string.IsNullOrEmpty(customer.Sex))
                {
                    sql1Custom.Append("sex,");
                    sql2Custom.Append("?sex,");
                    paramList2.Add(new MySqlParameter("sex", customer.Sex));
                }
                if (!string.IsNullOrEmpty(customer.IdentityCardPhoto))
                {
                    sql1Custom.Append("Identitycardphoto,");
                    sql2Custom.Append("?Identitycardphoto,");
                    paramList2.Add(new MySqlParameter("Identitycardphoto", customer.IdentityCardPhoto));
                }
                if (!string.IsNullOrEmpty(customer.CameraPhoto))
                {
                    sql1Custom.Append("Cameraphoto,");
                    sql2Custom.Append("?Cameraphoto,");
                    paramList2.Add(new MySqlParameter("Cameraphoto", customer.CameraPhoto));
                }
                if (customer.CheckIDcard != 0)
                {
                    sql1Custom.Append("CheckIDcard,");
                    sql2Custom.Append("?CheckIDcard,");
                    paramList2.Add(new MySqlParameter("CheckIDcard", customer.CheckIDcard));
                }

                if (sql1Custom.Length > 0)
                {
                    sql1Custom.Remove(sql1Custom.Length - 1, 1);
                    sql2Custom.Remove(sql2Custom.Length - 1, 1);
                }
                sql1Custom.Append(")");
                sql2Custom.Append(")");
                sqlList.Add(sql1Custom.Append(sql2Custom).ToString());
                paramList.Add(paramList2.ToArray());
            }
            try
            {
                MySQLOperater.ExecuteTrans(sqlList, paramList);
                return true;
            }
            catch (Exception e)
            {
                log.Error("sql_checkin:" + sqlCheckin + "#" + e.Message);
                throw new Exception("客户入住信息录入出错。");
            }
        }

        /// <summary>
        /// 查询符合条件的行数
        /// </summary>
        /// <param name="ckInfo">查询条件</param>
        /// <returns>行数</returns>
        public int GetCheckinRowsCount(CheckinQuery ckInfo)
        {
            if (ckInfo.CheckinTimeBegin > ckInfo.CheckinTimeEnd)
            {
                throw new Exception("查询条件出错。");
            }
            //查询条件：macid,checkintime,checkouttime,exportcount
            log.Debug("方法Query接收参数：MacID:" + ckInfo.MacId + "-CheckinTimeBegin:" + ckInfo.CheckinTimeBegin
                + "-CheckinTimeEnd:" + ckInfo.CheckinTimeEnd + "-ExportCount:" + ckInfo.ExportCount);
            StringBuilder sql = new StringBuilder();
            int i = 0;
            sql.Append("select count(*) from t_CheckinOrder t where t.checkintime "
                + " between {" + i++ + "} and {" + i++ + "} ");
            var paramList = new List<object>();
            paramList.Add(ckInfo.CheckinTimeBegin);
            paramList.Add(ckInfo.CheckinTimeEnd);
            if (!string.IsNullOrEmpty(ckInfo.MacId))
            {
                sql.Append(" and t.macid={" + i++ + "} ");
                paramList.Add(ckInfo.MacId);
            }
            if (ckInfo.ExportCount == 0)
            {
                sql.Append(" and t.exportcount=0 ");
            }
            try
            {
                var sum = int.Parse(mso.GetScalar(sql.ToString(), paramList.ToArray()).ToString());
                return sum;
            }
            catch (Exception e)
            {
                log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询行数出错！");
            }
        }

        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <param name="ckInfo"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public DataTable Query(CheckinQuery ckInfo, int page, int rows)
        {
            if (page < 0 || rows < 0 || rows > 10000 || ckInfo.CheckinTimeBegin > ckInfo.CheckinTimeEnd)
            {
                throw new Exception("参数值不正确。");
            }
            log.Debug("方法Query接收参数：MacID:" + ckInfo.MacId + "-CheckinTimeBegin:" + ckInfo.CheckinTimeBegin
                + "-CheckinTimeEnd:" + ckInfo.CheckinTimeEnd + "-ExportCount:" + ckInfo.ExportCount
                + "-page:" + page + "-rows:" + rows);

            var sql = new StringBuilder();
            var i = 0;
            sql.Append("select id,orderid,roomnum,roomtype,building,roomcode,roomrate,"
                + "checkintype,vipcardnum,vipcardtype,peoplenum,checkintime,hours,"
                + "advancepayment,advancetype,macid,(select name from t_Machine b where b.jqId=t.macid) macname,"
                + "ordertime,checkinimage,exportcount,internetgroup,checkid from t_CheckinOrder t where t.checkintime "
                + "between {" + i++ + "} and {" + i++ + "} ");
            var paramList = new List<object> { ckInfo.CheckinTimeBegin, ckInfo.CheckinTimeEnd };
            if (!string.IsNullOrEmpty(ckInfo.MacId))
            {
                sql.Append(" and t.macid={" + i++ + "} ");
                paramList.Add(ckInfo.MacId);
            }
            if (!string.IsNullOrEmpty(ckInfo.CheckinType))
            {
                sql.Append(" and t.CheckinType={" + i++ + "} ");
                paramList.Add(ckInfo.CheckinType);
            }
            if (ckInfo.ExportCount == 0)
            {
                sql.Append(" and t.exportcount=0 ");
            }
            sql.Append(" order by checkintime desc limit " + (page - 1) * rows + "," + rows);
            try
            {
                return mso.GetDataTable(sql.ToString(), paramList.ToArray());
            }
            catch (Exception e)
            {
                log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询数据出错！");
            }
        }


        /// <summary>
        /// 向入住订单表中添加下载次数
        /// </summary>
        /// <param name="orderid">数组</param>
        public void AddDownloadCount(string[] orderid)
        {
            if (orderid.Length <= 0)
            {
                throw new Exception("订单ID少于一个。");
            }
            var strSql = new StringBuilder();
            var parameters = new object[orderid.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = orderid[i];
            }
            strSql.Append(" update t_CheckinOrder set exportcount=exportcount+1 where orderid in ( ");
            for (int i = 0; i < orderid.Length; i++)
            {
                strSql.Append("{" + i + "},");
            }
            strSql.Remove(strSql.ToString().LastIndexOf(","), 1);
            strSql.Append(")");
            try
            {
                mso.Execute(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                log.Error("sql:" + strSql + e.Message);
                throw new Exception("向入住订单表中添加下载次数。");
            }
        }

        /// <summary>
        /// 根据入住订单ID查询入住订单对象
        /// </summary>
        /// <param name="ckinInfo"></param>
        /// <returns></returns>
        public CheckinInfo GetModule(CheckinInfo ckinInfo)
        {
            if (string.IsNullOrEmpty(ckinInfo.OrderId))
            {
                throw new Exception("Orderid为空。");
            }
            log.Debug("方法GetModule接手参数：" + ckinInfo.OrderId);
            var sql = new StringBuilder("select * from t_CheckinOrder t where 1=1 ");
            var paramList = new List<object>();
            var i = 0;
            if (!string.IsNullOrEmpty(ckinInfo.OrderId))
            {
                sql.Append(" and t.orderid={" + i++ + "}");
                paramList.Add(ckinInfo.OrderId);
            }
            DataTable dt = null;
            try
            {
                dt = mso.GetDataTable(sql.ToString(), paramList.ToArray());
            }
            catch (Exception e)
            {
                log.Error("sql:" + sql + "-" + e.Message);
                throw new Exception("查询数据出错。");
            }
            if (null != dt && dt.Rows.Count > 0)
            {
                ckinInfo.CheckinImage = dt.Rows[0]["checkinimage"].ToString();
            }
            return ckinInfo;
        }

        /// <summary>
        /// 查询终端
        /// </summary>
        /// <returns></returns>
        public DataTable Query(MachineInfo machineinfo)
        {
            var sql = new StringBuilder();
            try
            {
                sql.Append("select * from t_Machine t where 1=1 ");
                var paramList = new List<object>();
                var i = 0;
                if (!string.IsNullOrEmpty(machineinfo.JqId))
                {
                    sql.Append(" and t.jqid={" + i++ + "} ");
                    paramList.Add(machineinfo.JqId);
                }
                if (!string.IsNullOrEmpty(machineinfo.FaultId))
                {
                    sql.Append(" and t.faultid={" + i++ + "}");
                    paramList.Add(machineinfo.FaultId);
                }
                if (!string.IsNullOrEmpty(machineinfo.HotelId))
                {
                    sql.Append(" and t.hotelid={" + i++ + "} ");
                    paramList.Add(machineinfo.HotelId);
                }
                if (machineinfo.Isdisabled == 1 || machineinfo.Isdisabled == 2)
                {
                    sql.Append(" and t.isdisabled={" + i++ + "} ");
                    paramList.Add(machineinfo.Isdisabled);
                }
                if (!string.IsNullOrEmpty(machineinfo.IP))
                {
                    sql.Append(" and t.ip={" + i++ + "} ");
                    paramList.Add(machineinfo.IP);
                }
                if (machineinfo.IscheckIDcard == 1 || machineinfo.IscheckIDcard == 2)
                {
                    sql.Append(" and t.IscheckIDcard={" + i++ + "} ");
                    paramList.Add(machineinfo.IscheckIDcard);
                }
                if (!string.IsNullOrEmpty(machineinfo.Name))
                {
                    sql.Append(" and t.name={" + i++ + "} ");
                    paramList.Add(machineinfo.Name);
                }
                return mso.GetDataTable(sql.ToString(), paramList.ToArray());
            }
            catch (Exception e)
            {
                log.Error("sql:" + sql + "#" + e.Message);
                throw new Exception("查询终端出错！");
            }
        }

        /// <summary>
        /// 根据条件查找客人数据
        /// </summary>
        /// <param name="cInfo">查询条件</param>
        /// <returns></returns>
        public DataTable Query(CustomerInfo cInfo)
        {
            log.Debug("查询条件：Orderid-" + cInfo.Id);
            string sql = "select * from t_Customer t where 1=1 ";
            var paramList = new List<object>();
            var i = 0;
            if (!string.IsNullOrEmpty(cInfo.Id))
            {
                sql += " and t.id={" + i++ + "} ";
                paramList.Add(cInfo.Id);
            }
            if (!string.IsNullOrEmpty(cInfo.OrderId))
            {
                sql += " and t.orderid={" + i++ + "}";
                paramList.Add(cInfo.OrderId);
            }
            try
            {
                return mso.GetDataTable(sql, paramList.ToArray());
            }
            catch (Exception e)
            {
                log.Error("sql:" + sql + e.Message);
                throw new Exception("查询客人信息出错。");
            }
        }



        /// <summary>
        /// 根据checkin数据查询客人信息
        /// </summary>
        /// <param name="ckinInfo">checkin信息</param>
        /// <returns>客人信息DataSet</returns>
        public DataTable Query(CheckinQuery ckinInfo)
        {
            log.Debug("Query查询条件MacID：" + ckinInfo.MacId + "，CheckinTime：" + ckinInfo.CheckinTimeBegin
                + "，CheckoutTime：" + ckinInfo.CheckinTimeEnd + "，ExportCount：" + ckinInfo.ExportCount);

            const int rowsMax = 10000;
            var paramList = new List<object>();
            var i = 0;
            var sql = "select * from t_Customer where orderid in (select orderid from t_CheckinOrder t where t.checkintime " +
                      "between {" + i++ + "} and {" + i++ + "} ";

            paramList.Add(ckinInfo.CheckinTimeBegin);
            paramList.Add(ckinInfo.CheckinTimeEnd);

            if (!string.IsNullOrEmpty(ckinInfo.MacId))
            {
                sql += " and t.macid={" + i++ + "} ";
                paramList.Add(ckinInfo.MacId);
            }
            if (ckinInfo.ExportCount == 0)
            {
                sql += " and t.exportcount=0 ";
            }
            sql += ") limit 0," + rowsMax;
            try
            {
                return mso.GetDataTable(sql, paramList.ToArray());
            }
            catch (Exception e)
            {
                log.Error("sql:" + sql + e.Message);
                throw new Exception("根据checkin数据查询客人信息出错。");
            }
        }


        /// <summary>
        /// 向客户表中添加下载次数
        /// </summary>
        /// <param name="orderids">订单ID</param>
        /// <param name="orderid"></param>
        public void AddDoenloadCount(string[] orderid)
        {
            var strSql = new StringBuilder();
            var parameters = new object[orderid.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                parameters[i] = orderid[i];
            }
            strSql.Append(" update t_Customer set exportcount=exportcount+1 where orderid in ( ");
            for (int i = 0; i < orderid.Length; i++)
            {
                strSql.Append("{" + i + "},");
            }
            strSql.Remove(strSql.ToString().LastIndexOf(","), 1);
            strSql.Append(")");
            try
            {
                mso.Execute(strSql.ToString(), parameters);
            }
            catch (Exception e)
            {
                log.Error("sql:" + strSql + e.Message);
                throw new Exception("向客户表中添加下载次数。");
            }
        }


        public override bool Exist(CheckinInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Add(CheckinInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Del(CheckinInfo bean)
        {
            throw new NotImplementedException();
        }

        public override void Modify(CheckinInfo bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable Query(CheckinInfo bean)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(CheckinInfo bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(CheckinInfo bean, int page, int rows, ref int count)
        {
            throw new NotImplementedException();
        }
    }
}
