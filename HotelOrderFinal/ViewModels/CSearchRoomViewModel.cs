using HotelOrderFinal.Models;

namespace HotelOrderFinal.ViewModels
{
    public class CSearchRoomViewModel
    {
        public string RoomId { get; set; }
        public string RoomClassId { get; set; }
        public int? RoomSize { get; set; }
        public int? RoomPeople { get; set; }
        //public int? HotelId { get; set; }
        //public string AdminId { get; set; }

        public string RoomClassName { get; set; }
        public string RoomClassDetail { get; set; }
        public string RoomClassPhoto1 { get; set; }
        public string RoomClassPhoto2 { get; set; }
        public string RoomClassPhoto3 { get; set; }
        public decimal? WeekdayPrice { get; set; }
        public decimal? HolidayPrice { get; set; }
        public decimal? AddPrice { get; set; }

    }
}
