using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class ActionDescribe
    {
        public int ActionToRoomId { get; set; }
        public string? RoomId { get; set; }
        public int? ActioId { get; set; }
        public string? MemberId { get; set; }

        public virtual Action? Actio { get; set; }
        public virtual RoomMember? Member { get; set; }
    }
}
