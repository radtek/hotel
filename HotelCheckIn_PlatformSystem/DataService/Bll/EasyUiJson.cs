using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelCheckIn_PlatformSystem.DataService.BLL
{
    public class EasyUiJson<T>
    {
        // 数据总数
        public int? Total { get; set; }

        // 关键数据
        public List<T> Rows { get; set; }
    }
}
