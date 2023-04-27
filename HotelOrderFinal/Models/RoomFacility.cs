using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomFacility
    {
        public RoomFacility()
        {
            MultipleRoomFacilities = new HashSet<MultipleRoomFacility>();
        }

        public int RoomFacilityId { get; set; }
        public string? RoomFacilityName { get; set; }
        public byte[]? RoomFacilityImage { get; set; }

        public virtual ICollection<MultipleRoomFacility> MultipleRoomFacilities { get; set; }
    }
}
