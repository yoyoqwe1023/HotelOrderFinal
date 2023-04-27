using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Discount
    {
        public Discount()
        {
            DiscountReferences = new HashSet<DiscountReference>();
        }

        public int DiscountId { get; set; }
        public string DiscountName { get; set; } = null!;
        public byte[]? DiscountImage { get; set; }
        public string? DiscountDirections { get; set; }
        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }
        public decimal? DiscountDiscount { get; set; }

        public virtual ICollection<DiscountReference> DiscountReferences { get; set; }
    }
}
