
namespace HotelCheckIn_BackSystem.DataService.Model
{
    public class RoomQualityInfo : BaseModel<RoomQualityInfo>
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 酒店ID
        /// </summary>
        public string HotelId { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}