namespace WebApplication1.Contracts.Place
{
    public class GetPlaceRequest
    {
        public string PlaceName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? DeletedBy { get; set; }
    }
}
