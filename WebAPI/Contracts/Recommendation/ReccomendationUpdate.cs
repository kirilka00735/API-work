namespace WebApplication1.Contracts.Recommendation
{
    public class ReccomendationUpdate
    {
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
    }
}
