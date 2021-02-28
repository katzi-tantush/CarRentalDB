using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CarRentalDB.Models
{
    public partial class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext()
        {
        }

        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CarCategory> CarCategories { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Location> Locations{ get; set; }
        public virtual DbSet<RentedCar> RentedCars { get; set; }
        public virtual DbSet<User> Users{ get; set; }
        public virtual DbSet<UserMessage> UserMessages{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("CarRentalsConStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
