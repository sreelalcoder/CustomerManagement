using CustomerServiceWebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceWebApi.Infrastructure.Data
{
    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Pepa Pig", Email = "Pepa@gmail.com", PhoneNumber = "+440987654321" },
                new Customer { Id = 2, Name = "Gorge Pig", Email = "Gorge@yahoo.com", PhoneNumber = "+911234567890" }
            );
            
        }
    }
}
