using System.ComponentModel;

namespace HotelOrderFinal.ViewModels
{
    public class ShowOderData
    {
        [DisplayName("會員ID")]
        public string MemberId { get; set; }
        [DisplayName("訂單ID")]
        public string OrderId { get; set; }
        [DisplayName("訂單日期")]
        public string OrderDate { get; set; }
        [DisplayName("訂單金額")]
        public int OrderTotalPrice { get; set; }
        [DisplayName("訂單數量")]
        public int? CheckInPeople { get; set; }
        [DisplayName("備註")]
        public string OrderRemark { get; set; }
    }
}
