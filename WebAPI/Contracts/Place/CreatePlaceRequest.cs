namespace WebApplication1.Contracts.Place
{
    public class CreatePlaceRequest
    {
        public string PlaceName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
