using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class HotelRegionName
    {
        public HotelRegionName()
        {
            HotelIndustries = new HashSet<HotelIndustry>();
        }

        public int HotelRegionId { get; set; }
        public string? RegionName { get; set; }

        public virtual ICollection<HotelIndustry> HotelIndustries { get; set; }
    }
}
