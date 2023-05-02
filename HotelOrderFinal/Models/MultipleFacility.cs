using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class MultipleFacility
    {
        public int MultipleFacilitiesId { get; set; }
        public string? RoomId { get; set; }
        public int? FacilityId { get; set; }

        public virtual Facility? Facility { get; set; }
        public virtual Room? Room { get; set; }
    }
}
