using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityReferences = new HashSet<ActivityReference>();
        }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = null!;
        public string? ActivityImage { get; set; }
        public string? ActivityDirections { get; set; }
        public DateTime? ActivityTime { get; set; }
        public string? ActivityPlace { get; set; }
        public int? ActivityPeople { get; set; }
        public string? ActivityCost { get; set; }

        public virtual ICollection<ActivityReference> ActivityReferences { get; set; }
    }
}
