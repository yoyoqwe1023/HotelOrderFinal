using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Order
    {
        public Order()
        {
            ActivityReferences = new HashSet<ActivityReference>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string OrderId { get; set; } = null!;
        public string? MemberId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OrderTotalPrice { get; set; }
        public int? CheckInPeople { get; set; }
        public string? OrderRemark { get; set; }
        public string? OrderClosed { get; set; }

        public virtual RoomMember? Member { get; set; }
        public virtual ICollection<ActivityReference> ActivityReferences { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
