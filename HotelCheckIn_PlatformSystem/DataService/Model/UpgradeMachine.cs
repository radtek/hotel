using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_PlatformSystem.DataService.Model
{
    public class UpgradeMachine
    {
        /// <summary>
        /// 升级机器id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 升级文件id
        /// </summary>
        public string UpgradeFileId { get; set; }

        /// <summary>
        /// 机器id
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// 是否已经下载：0 已下载，1 未下载
        /// </summary>
        public int? IsFlag { get; set; }

        /// <summary>
        /// 要不要下载:0 要，1 不要
        /// </summary>
        public int? IsDownland { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatePerson { get; set; }
    }
}