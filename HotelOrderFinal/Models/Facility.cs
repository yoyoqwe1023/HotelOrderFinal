using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Facility
    {
        public Facility()
        {
            MultipleFacilities = new HashSet<MultipleFacility>();
        }

        public int FacilityId { get; set; }
        public string? FacilityName { get; set; }
        public byte[]? FacilityImage { get; set; }

        public virtual ICollection<MultipleFacility> MultipleFacilities { get; set; }
    }
}
