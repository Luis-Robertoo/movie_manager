using Microsoft.EntityFrameworkCore;
using MovieManager.BLL.Entities;

namespace MovieManager.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public readonly int SuccededResultNumber = 1;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rental>()
                .HasOne(customer => customer.Customer)
                .WithMany(rental => rental.Rental);

            builder.Entity<Rental>()
              .HasOne(customer => customer.Movie);
        }
    }
}