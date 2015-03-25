
namespace HotelCheckIn_InterfaceSystem.model
{
    public class VoidReturn
    {
        private string request_id;
        private ReturnInfo return_info;

        public string request_Id
        {
            get { return this.request_id; }
            set { this.request_id = value; }
        }

        public ReturnInfo return_Info
        {
            get { return this.return_info; }
            set { this.return_info = value; }
        }
    }
}
