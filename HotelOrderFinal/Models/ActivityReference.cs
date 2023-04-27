using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class ActivityReference
    {
        public int ActivityReferenceId { get; set; }
        public int ActivityId { get; set; }
        public string? OrderId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}
