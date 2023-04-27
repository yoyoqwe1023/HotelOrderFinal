using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class Action
    {
        public Action()
        {
            ActionDescribes = new HashSet<ActionDescribe>();
        }

        public int ActionId { get; set; }
        public string? ActionName { get; set; }
        public string? ActionDescript { get; set; }

        public virtual ICollection<ActionDescribe> ActionDescribes { get; set; }
    }
}
