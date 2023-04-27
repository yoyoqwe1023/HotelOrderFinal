using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class MultipleHotelFacility
    {
        public int MultipleHotelFacilityId { get; set; }
        public int? HotelId { get; set; }
        public int? HotelFacilityId { get; set; }

        public virtual HotelIndustry? Hotel { get; set; }
        public virtual HotelFacility? HotelFacility { get; set; }
    }
}
