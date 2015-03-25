using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HotelCheckIn_PlatformSystem.DataService.Dal;
using HotelCheckIn_PlatformSystem.DataService.Model;

namespace HotelCheckIn_PlatformSystem.DataService.BLL
{
    public class MenuPowerBll : BaseBll<MenuPower>
    {
        readonly MenuPowerDal _menuPowerDal = new MenuPowerDal();
        public override bool Exist(MenuPower bean)
        {
            return _menuPowerDal.Exist(bean);
        }

        public override void Add(MenuPower bean)
        {
            _menuPowerDal.Add(bean);
        }

        public override void Del(MenuPower bean)
        {
            _menuPowerDal.Del(bean);
        }

        public override void Modify(MenuPower bean)
        {
            _menuPowerDal.Modify(bean);
        }

        public override DataTable Query(MenuPower bean)
        {
            return _menuPowerDal.Query(bean);
        }

        public override DataTable QueryByPage(MenuPower bean, int page, int rows)
        {
            throw new NotImplementedException();
        }

        public override DataTable QueryByPage(MenuPower bean, int page, int rows, ref int count)
        {
            return _menuPowerDal.QueryByPage(bean, page, rows, ref count);
        }
        public DataTable QueryMenuByRole(string role)
        {
            return _menuPowerDal.QueryMenuByRole(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public DataTable QuerySubMenu(string parentid)
        {
            return _menuPowerDal.QuerySubMenu(parentid);
        }

        public DataTable QueryMainMenu(string roleid)
        {
            return _menuPowerDal.QueryMainMenu(roleid);
        }

        public DataTable QueryMenuSub(string parentid)
        {
            return _menuPowerDal.QueryMenuSub(parentid);
        }
    }
}
