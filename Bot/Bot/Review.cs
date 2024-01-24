using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? DeletedBy { get; set; }

        public virtual Place Place { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
