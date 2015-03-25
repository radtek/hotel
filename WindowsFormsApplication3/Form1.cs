using System;
using System.Data;
using System.Windows.Forms;
using HotelCheckIn_BackSystem.DataService.Dal;
using log4net;

namespace UnlockRoomService
{
    public partial class Form1 : Form
    {
        private readonly RoomLockDal Rkdal;
        private readonly ILog Log;
        public Form1()
        {
            InitializeComponent();
            Rkdal = new RoomLockDal();
            Log =log4net.LogManager.GetLogger("Form1");
            timer1.Interval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["timer"]);
            timer1.Enabled = true;
        }

        /// <summary>
        /// 解锁方法
        /// </summary>
        private void UnlockRoom()
        {
            var dt = new DataTable();
            try
            {
                dt = Rkdal.QueryAllRoomLock();
            }
            catch (Exception e1)
            {
                Log.Error("查询房间锁定信息失败:" + e1.Message); ;
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var lockTime = DateTime.Parse(dt.Rows[i]["LockTime"].ToString());
                    var nowTime = DateTime.Now;
                    var differTime = DateTime.Compare(nowTime,lockTime.AddMinutes(10));
                    if (differTime > 0)
                    {
                        //pms解锁房间。。。。
                        //删除房间锁定信息
                        var checkId = dt.Rows[i]["CheckId"].ToString();
                        try
                        {
                            Rkdal.DelRoomLock(checkId);
                        }
                        catch (Exception e2)
                        {
                            Log.Error("删除间锁定信息失败:" + e2.Message); ;
                        }
                        
                    }
                }
            }
        }

        /// <summary>
        /// 定时触发解锁服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            UnlockRoom();
        }
    }
}
