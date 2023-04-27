using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderSpecialRequests = new HashSet<OrderSpecialRequest>();
        }

        public string OrderDetailId { get; set; } = null!;
        public string? OrderId { get; set; }
        public DateTime? MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? PaymentId { get; set; }
        public decimal? PaymentPrice { get; set; }
        public int? OrderDetailStatusId { get; set; }
        public string? RoomId { get; set; }
        public string? OrderDetailRemark { get; set; }

        public virtual Order? Order { get; set; }
        public virtual OrderDetailStatus? OrderDetailStatus { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderSpecialRequest> OrderSpecialRequests { get; set; }
    }
}
