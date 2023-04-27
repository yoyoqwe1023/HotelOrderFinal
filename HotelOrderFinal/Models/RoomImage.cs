using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomImage
    {
        public int RoomImagesId { get; set; }
        public string? RoomImageName { get; set; }
        public string? RoomId { get; set; }
        public byte[]? RoomImage1 { get; set; }
        public string? RoomImageDetail { get; set; }

        public virtual Room? Room { get; set; }
    }
}
