using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomAdmin
    {
        public RoomAdmin()
        {
            RoomMembers = new HashSet<RoomMember>();
            Rooms = new HashSet<Room>();
        }

        public string AdminId { get; set; } = null!;
        public string? AdminName { get; set; }
        public string? AdminPassword { get; set; }

        public virtual ICollection<RoomMember> RoomMembers { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
