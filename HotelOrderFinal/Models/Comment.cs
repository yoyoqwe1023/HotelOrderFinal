﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public DateTime? CommentDate { get; set; }
        public string MemberId { get; set; }
        public string RoomClassId { get; set; }
        public int? CommentPoint { get; set; }
        public string CommentDetail { get; set; }

        public virtual RoomMember Member { get; set; }
        public virtual RoomClass RoomClass { get; set; }
    }
}