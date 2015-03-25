using System;
using System.Collections.Generic;
using System.Data;
using CommonLibrary.exception;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;
using log4net;

namespace HotelCheckIn_BackSystem.DataService.Bll
{
    public class RoomLockBll
    {
        private static ILog _log = log4net.LogManager.GetLogger("RoomLockBll");
        readonly RoomLockDal _rkdal = new RoomLockDal();
        readonly CheckNoDal _ckdal = new CheckNoDal();
        public RoomLockBll()
        { }

        /// <summary>
        /// 根据条件查询房间锁定数据
        /// </summary>
        /// <param name="rkinfo">查询条件实体对象</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页行数</param>
        /// <returns></returns>
        public List<RoomLockInfo> QueryRoomLock(RoomLockInfo rkinfo, int page, int rows, ref int total)
        {
            rkinfo.EndTime = rkinfo.EndTime.AddDays(1);
            total = _rkdal.QueryRoomLock(rkinfo);
            var ckList = new List<RoomLockInfo>();

            DataTable dt = _rkdal.QueryRoomLock(rkinfo, page, rows);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var ckIn = new RoomLockInfo();
                    DataRow dr = dt.Rows[i];
                    ckIn.CheckId = dr["CheckID"].ToString();
                    ckIn.HotelId = dr["HotelId"].ToString();
                    ckIn.MacId = dr["MacId"].ToString();
                    ckIn.RoomNum = dr["RoomNum"].ToString();
                    if (dr["LockTime"].ToString() == "" || dr["LockTime"] == null)
                        ckIn.LockTime = new DateTime();
                    else
                        ckIn.LockTime = DateTime.Parse(dr["LockTime"].ToString());
                    ckList.Add(ckIn);
                }
            }
            return ckList;
        }

        /// <summary>
        /// 添加房间锁定信息
        /// </summary>
        /// <param name="bean"></param>
        public void Add(RoomLockInfo bean)
        {
            bool b = _rkdal.Exist(new RoomLockInfo()
            {
                RoomNum = bean.RoomNum,
            });
            if (b)
            {
                throw new BusinessException("房间已经被锁定。房间号：" + bean.RoomNum);
            }
            _rkdal.Add(bean);
        }


        /// <summary>
        /// 解锁房间
        /// </summary>
        /// <param name="checkid"></param>
        public bool Unlock(string checkid)
        {
            const bool result = true;
            var checkno = new CheckNoInfo();
            DataTable dt = _ckdal.QueryCheckId(checkid);
            if (dt.Rows.Count > 0)
            {
                if (int.Parse(dt.Rows[0]["InternetCheck"].ToString()) == 1)//判断验证码团购验证状态（1未验证，2已验证）
                {
                    checkno.CheckId = checkid;
                    checkno.MachineCheck = 1;
                    checkno.MachineCheckPeople = 0;
                    _ckdal.EditCheckNoUnlock(checkno);//解锁房间，更改验证码（终端）验证状态
                    _rkdal.DelRoomLock(checkid);//解锁房间，删除房间锁定信息
                }
                else if (int.Parse(dt.Rows[0]["InternetCheck"].ToString()) == 2)
                {
                    return false;
                }
            }
            return result;
        }

        /// <summary>
        /// 取消验证码或手机对房间的锁定状态
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public bool Dellock(RoomLockInfo bean)
        {
            try
            {
                _ckdal.EditCheckNoUnlock(new CheckNoInfo()
                       {
                           CheckId = bean.CheckId,
                           MachineCheck = 1,
                           MachineCheckPeople = 0,
                       });//解锁房间，更改验证码（终端）验证状态
                if (!string.IsNullOrEmpty(bean.CheckId))
                {
                    _rkdal.Del(new RoomLockInfo() { CheckId = bean.CheckId });
                }
                else if (!string.IsNullOrEmpty(bean.PhoneNumber))
                {
                    _rkdal.Del(new RoomLockInfo() { PhoneNumber = bean.PhoneNumber });
                }
                return true;
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }
        }

    }
}