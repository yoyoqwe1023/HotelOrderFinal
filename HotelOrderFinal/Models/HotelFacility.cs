using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class HotelFacility
    {
        public HotelFacility()
        {
            MultipleHotelFacilities = new HashSet<MultipleHotelFacility>();
        }

        public int HotelFacilityId { get; set; }
        public string? HotelFacilityName { get; set; }
        public byte[]? HotelFacilityImage { get; set; }

        public virtual ICollection<MultipleHotelFacility> MultipleHotelFacilities { get; set; }
    }
}
