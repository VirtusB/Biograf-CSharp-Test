using System;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class BioContext : DbContext
    {
        public BioContext(DbContextOptions<BioContext> options) : base (options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Favorite> Favorites { get; set; }




        protected override void OnModelCreating(ModelBuilder builder) {
            #region Users
            builder.Entity<User>()
                .Property(e => e.Enabled)
                .HasDefaultValue(true);

            builder.Entity<User>()
                .Property(e => e.LifetimeSavedAmount)
                .HasDefaultValue(0);
            #endregion
                
            #region Reservations
            builder.Entity<Reservation>()
                .Property(s => s.BookingState)
                .HasDefaultValue(0);
            #endregion

            #region Favorites
            builder.Entity<Favorite>()
                .HasKey(k => new {k.LikerId, k.LikeeId}); 

            builder.Entity<Favorite>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Favorite>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}