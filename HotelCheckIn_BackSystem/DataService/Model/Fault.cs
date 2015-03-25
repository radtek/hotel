using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class Fault
    {
        /// <summary>
        /// 故障id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 机器id
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// 故障id(与码表关联)记录故障的问题
        /// </summary>
        public string FaultId { get; set; }

        /// <summary>
        /// 故障原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 解决人员
        /// </summary>
        public string SolvePerson { get; set; }

        /// <summary>
        /// 解决时间
        /// </summary>
        public DateTime? SolveDt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDt { get; set; }
    }
}