﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomAdmin
    {
        public RoomAdmin()
        {
            Room = new HashSet<Room>();
            RoomMember = new HashSet<RoomMember>();
        }

        public string AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminPassword { get; set; }

        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<RoomMember> RoomMember { get; set; }
    }
}