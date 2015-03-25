using System.Collections.Generic;

namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class pageObject<T>
    {
        private int _total;
        private List<T> _rows;

        public int total
        {
            set { _total = value; }
            get { return _total; }
        }

        public List<T> rows
        {
            set { _rows = value; }
            get { return _rows; }
        }

        public pageObject()
        { }

        public pageObject(int total, List<T> rows)
        {
            _total = total;
            _rows = rows;
        }
    }
}
