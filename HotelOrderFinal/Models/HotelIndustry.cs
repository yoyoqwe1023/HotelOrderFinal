using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class HotelIndustry
    {
        public HotelIndustry()
        {
            MultipleHotelFacilities = new HashSet<MultipleHotelFacility>();
            Rooms = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelPhone { get; set; }
        public int? HotelRegionId { get; set; }
        public string? HotelAddress { get; set; }
        public string? HotelImage { get; set; }
        public string? HotelImageDiscription { get; set; }

        public virtual HotelRegionName? HotelRegion { get; set; }
        public virtual ICollection<MultipleHotelFacility> MultipleHotelFacilities { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
