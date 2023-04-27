using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomTimePrice
    {
        public int TimePriceId { get; set; }
        public string? RoomClassId { get; set; }
        public decimal? WeekdayPrice { get; set; }
        public decimal? HolidayPrice { get; set; }

        public virtual RoomClass? RoomClass { get; set; }
    }
}
