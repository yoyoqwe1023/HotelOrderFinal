﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class HotelRegionName
    {
        public HotelRegionName()
        {
            HotelIndustry = new HashSet<HotelIndustry>();
        }

        public int HotelRegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<HotelIndustry> HotelIndustry { get; set; }
    }
}