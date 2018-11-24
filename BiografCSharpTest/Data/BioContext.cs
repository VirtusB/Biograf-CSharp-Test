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




        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<User>()
                .Property(e => e.Enabled)
                .HasDefaultValue(true);

            builder.Entity<User>()
                .Property(e => e.LifetimeSavedAmount)
                .HasDefaultValue(0);
                

            builder.Entity<Reservation>()
                .Property(s => s.BookingState)
                .HasDefaultValue(0);
        }
    }
}