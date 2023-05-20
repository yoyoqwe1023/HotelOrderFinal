using HotelOrderFinal.Models;

namespace HotelOrderFinal.ViewModels
{
    public class CindexActivityViewModels
    {
        public int ActivityID;
        public List<string> ActivityImage;

        public List<HotelIndustry> hotelIndustry { get; set; }
    }
}
