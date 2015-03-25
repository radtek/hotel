using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelCheckIn_PlatformSystem.DataService.Model.Parameter
{
    public class GetUpgradeFile:UpgradeFile
    {

        /// <summary>
        /// 
        /// </summary>
        public string MachineId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsDownland { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IsFlag { get; set; }
    }
}