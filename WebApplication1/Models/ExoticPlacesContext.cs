using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class ExoticPlacesContext : DbContext
    {
        public ExoticPlacesContext()
        {
        }

        public ExoticPlacesContext(DbContextOptions<ExoticPlacesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<Recommendation> Recommendations { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.PhotoId)
                    .ValueGeneratedNever()
                    .HasColumnName("Photo_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .HasColumnName("Created_by");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("Date_created");

                entity.Property(e => e.DateDeleted)
                    .HasColumnType("date")
                    .HasColumnName("Date_deleted");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(10)
                    .HasColumnName("Deleted_by");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(100)
                    .HasColumnName("Photo_URL");

                entity.Property(e => e.PlaceId).HasColumnName("Place_ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Photo_Place");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Photo_Users");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.PlaceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Place_ID");

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .HasColumnName("Created_by");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("Date_created");

                entity.Property(e => e.DateDeleted)
                    .HasColumnType("date")
                    .HasColumnName("Date_deleted");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(10)
                    .HasColumnName("Deleted_by");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.PlaceName)
                    .HasMaxLength(100)
                    .HasColumnName("Place_name");
            });

            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.Property(e => e.RecommendationId)
                    .ValueGeneratedNever()
                    .HasColumnName("Recommendation_ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("Created_by");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("Date_created");

                entity.Property(e => e.DateDeleted)
                    .HasColumnType("date")
                    .HasColumnName("Date_deleted");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(100)
                    .HasColumnName("Deleted_by");

                entity.Property(e => e.PlaceId).HasColumnName("Place_ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendations_Place");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendations_Users");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedNever()
                    .HasColumnName("Review_ID");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasColumnName("Created_by");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("Date_created");

                entity.Property(e => e.DateDeleted)
                    .HasColumnType("date")
                    .HasColumnName("Date_deleted");

                entity.Property(e => e.DeletedBy)
                    .HasMaxLength(100)
                    .HasColumnName("Deleted_by");

                entity.Property(e => e.PlaceId).HasColumnName("Place_ID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Place");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("date")
                    .HasColumnName("Date_created");

                entity.Property(e => e.DateDeleted)
                    .HasColumnType("date")
                    .HasColumnName("Date_deleted");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Pass).HasMaxLength(100);

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
