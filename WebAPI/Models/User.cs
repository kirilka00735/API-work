using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class User
    {
        public User()
        {
            Photos = new HashSet<Photo>();
            Recommendations = new HashSet<Recommendation>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
