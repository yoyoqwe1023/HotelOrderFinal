using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Payment
    {
        public Payment()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int PaymentId { get; set; }
        public string? Payments { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
