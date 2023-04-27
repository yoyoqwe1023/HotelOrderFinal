using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class SpecialRequest
    {
        public SpecialRequest()
        {
            OrderSpecialRequests = new HashSet<OrderSpecialRequest>();
        }

        public int SpecialRequestId { get; set; }
        public string? SpecialRequestName { get; set; }

        public virtual ICollection<OrderSpecialRequest> OrderSpecialRequests { get; set; }
    }
}
