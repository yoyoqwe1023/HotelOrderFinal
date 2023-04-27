using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class DiscountReference
    {
        public int DiscountReferenceId { get; set; }
        public int DiscountId { get; set; }
        public string? MemberId { get; set; }
        public int? Activaled { get; set; }

        public virtual Discount Discount { get; set; } = null!;
        public virtual RoomMember? Member { get; set; }
    }
}
