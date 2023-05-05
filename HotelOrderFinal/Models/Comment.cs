using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelOrderFinal.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        [DisplayName("日　期")]
        public DateTime? CommentDate { get; set; }
        [DisplayName("會員編號")]
        public string MemberId { get; set; }
        [DisplayName("評　點")]
        [Required(ErrorMessage = "評點是必填欄位")]
        public int? CommentPoint { get; set; }
        [DisplayName("評　價")]
        public string CommentDetail { get; set; }

        public virtual RoomMember? Member { get; set; }
        public virtual RoomClass? RoomClass { get; set; }
    }
}
