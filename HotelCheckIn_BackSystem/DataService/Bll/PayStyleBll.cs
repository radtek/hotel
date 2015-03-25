using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using HotelCheckIn_BackSystem.DataService.Dal;
using HotelCheckIn_BackSystem.DataService.Model;

namespace HotelCheckIn_BackSystem.DataService.Bll
{
    public class PayStyleBll
    {
        readonly PayStyleDal _psdal = new PayStyleDal();
        public PayStyleBll()
        { }

        public  void Add(PayStyle bean)
        {
            _psdal.Add(bean);
        }

        public  void Del(PayStyle bean)
        {
            _psdal.Del(bean);
        }

        public  void Modify(PayStyle bean)
        {
            _psdal.Modify(bean);
        }

        public  System.Data.DataTable Query(PayStyle bean)
        {
            return _psdal.Query(bean);
        }
    }
}