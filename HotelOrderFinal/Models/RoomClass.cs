﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelOrderFinal.Models
{
    public partial class RoomClass
    {
        public RoomClass()
        {
            MultipleRoomFacility = new HashSet<MultipleRoomFacility>();
            Room = new HashSet<Room>();
        }

        public string RoomClassId { get; set; }
        public string RoomClassName { get; set; }
        public int? RoomClassSize { get; set; }
        public int? RoomClassPeople { get; set; }
        public string RoomClassDetail { get; set; }
        public string RoomClassPhoto1 { get; set; }
        public string RoomClassPhoto2 { get; set; }
        public string RoomClassPhoto3 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal? WeekdayPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal? HolidayPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal? AddPrice { get; set; }

        public virtual ICollection<MultipleRoomFacility> MultipleRoomFacility { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }
}