using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.Model.Parameter
{
    public class GetMachine:Machine
    {
        public string Areaid { get; set; }
    }
}