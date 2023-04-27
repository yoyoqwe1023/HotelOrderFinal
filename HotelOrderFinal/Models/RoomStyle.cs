using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomStyle
    {
        public RoomStyle()
        {
            Rooms = new HashSet<Room>();
        }

        public string RoomStyleId { get; set; } = null!;
        public string? RoomStyleName { get; set; }
        public string? RoomStyleDetail { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
