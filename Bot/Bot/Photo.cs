using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Photo
    {
        public int PhotoId { get; set; }
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? DeletedBy { get; set; }

        public virtual Place Place { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
