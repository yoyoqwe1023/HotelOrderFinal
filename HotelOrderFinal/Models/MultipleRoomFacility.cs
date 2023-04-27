using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class MultipleRoomFacility
    {
        public int MultipleRoomFacilitiesId { get; set; }
        public string? RoomId { get; set; }
        public int? RoomFacilityId { get; set; }

        public virtual Room? Room { get; set; }
        public virtual RoomFacility? RoomFacility { get; set; }
    }
}
