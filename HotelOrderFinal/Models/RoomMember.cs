using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class RoomMember
    {
        public RoomMember()
        {
            ActionDescribes = new HashSet<ActionDescribe>();
            Comments = new HashSet<Comment>();
            DiscountReferences = new HashSet<DiscountReference>();
            Orders = new HashSet<Order>();
        }

        public string AdminId { get; set; } = null!;
        public string MemberId { get; set; } = null!;
        public string? MemberName { get; set; }
        public DateTime? MemberBirthday { get; set; }
        public string? MemberGender { get; set; }
        public string? MemberIdentity { get; set; }
        public string? MemberPhone { get; set; }
        public string? MemberEmail { get; set; }
        public byte[]? MemberPhoto { get; set; }
        public string? MemberPassword { get; set; }

        public virtual RoomAdmin Admin { get; set; } = null!;
        public virtual ICollection<ActionDescribe> ActionDescribes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<DiscountReference> DiscountReferences { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
