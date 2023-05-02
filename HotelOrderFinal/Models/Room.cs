using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Room
    {
        public Room()
        {
            MultipleFacilities = new HashSet<MultipleFacility>();
            RoomImages = new HashSet<RoomImage>();
        }

        public string RoomId { get; set; } = null!;
        public string? RoomClassId { get; set; }
        public int? RoomSize { get; set; }
        public int? RoomPeople { get; set; }
        public string? RoomStyleId { get; set; }
        public int? HotelId { get; set; }
        public string? RoomStatus { get; set; }
        public string? AdminId { get; set; }
        public int? Favorite { get; set; }

        public virtual RoomAdmin? Admin { get; set; }
        public virtual HotelIndustry? Hotel { get; set; }
        public virtual RoomClass? RoomClass { get; set; }
        public virtual RoomStyle? RoomStyle { get; set; }
        public virtual ICollection<MultipleFacility> MultipleFacilities { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; }
    }
}
