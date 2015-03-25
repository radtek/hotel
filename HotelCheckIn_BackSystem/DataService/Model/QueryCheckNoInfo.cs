using System;
namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class QueryCheckNoInfo
    {
        public string CheckNo { get; set; }

        public string GroupId { get; set; }

        public string MacId { get; set; }

        public string HotelId { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CheckinTime { get; set; }

        public DateTime CheckoutTime { get; set; }
    }
}