namespace HotelOrderFinal.ViewModels
{
    public class CActivityPhoto
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = null!;
        public byte[]? ActivityImage { get; set; }
        public string? ActivityDirections { get; set; }
        public DateTime? ActivityTime { get; set; }
        public string? ActivityPlace { get; set; }
        public int? ActivityPeople { get; set; }
        public string? ActivityCost { get; set; }
    }
}
