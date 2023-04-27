using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomClass
    {
        public RoomClass()
        {
            Comments = new HashSet<Comment>();
            RoomTimePrices = new HashSet<RoomTimePrice>();
            Rooms = new HashSet<Room>();
        }

        public string RoomClassId { get; set; } = null!;
        public string? RoomClassName { get; set; }
        public string? RoomClassDetail { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<RoomTimePrice> RoomTimePrices { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
