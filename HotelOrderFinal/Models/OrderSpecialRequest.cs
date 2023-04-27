﻿using System;
using System.Collections.Generic;

namespace HotelOrderFinal.Models
{
    public partial class OrderSpecialRequest
    {
        public int OrderSpecialRequestId { get; set; }
        public string? OrderDetailId { get; set; }
        public int? SpecialRequestId { get; set; }

        public virtual OrderDetail? OrderDetail { get; set; }
        public virtual SpecialRequest? SpecialRequest { get; set; }
    }
}
