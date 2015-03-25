using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_PlatformSystem.DataService.Model
{
    public class UpgradeFile
    {
        /// <summary>
        /// 升级文件id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

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