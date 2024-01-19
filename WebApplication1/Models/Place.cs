using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Place
    {
        public Place()
        {
            Photos = new HashSet<Photo>();
            Recommendations = new HashSet<Recommendation>();
            Reviews = new HashSet<Review>();
        }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? DeletedBy { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
