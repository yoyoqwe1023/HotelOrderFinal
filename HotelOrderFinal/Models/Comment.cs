using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public DateTime? CommentDate { get; set; }
        public string? MemberId { get; set; }
        public string? RoomClassId { get; set; }
        public decimal? CommentPoint { get; set; }
        public string? CommentDetail { get; set; }

        public virtual RoomMember? Member { get; set; }
        public virtual RoomClass? RoomClass { get; set; }
    }
}
