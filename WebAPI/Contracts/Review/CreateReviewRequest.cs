namespace WebApplication1.Contracts.Review
{
    public class CreateReviewRequest
    {
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
