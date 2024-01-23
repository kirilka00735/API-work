namespace WebApplication1.Contracts.Photo
{
    public class UpdatePhotoRequest
    {
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
