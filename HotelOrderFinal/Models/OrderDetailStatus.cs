using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class OrderDetailStatus
    {
        public OrderDetailStatus()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderDetailStatusId { get; set; }
        public string? OrderDetaiStatusContent { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
