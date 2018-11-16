using System;
using BiografCSharpTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BiografCSharpTest.Data
{
    public class BioContext : DbContext
    {
        //public BioContext(DbContextOptions<BioContext> options) : base (options) { }
        public BioContext() : base () { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = String.Format(@"Server={0};Database=BioBTest;Trusted_Connection=True;User Id=virtus;Password=password;", Environment.MachineName);
            optionsBuilder.UseSqlServer(connString);
        }



        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<User>()
                .Property(e => e.Enabled)
                .HasDefaultValue(true);
        }
    }
}