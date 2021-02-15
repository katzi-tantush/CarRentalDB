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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-ERBRHMC\\SQLEXPRESS;Initial Catalog=CarRentalDb;Integrated Security=True");
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("CarRentalsConStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            // FIXME: not sure if I should ommit this
            //modelBuilder.Entity<CarCategory>(entity =>
            //{
            //    entity.Property(e => e.Id)
            //        .ValueGeneratedNever()
            //        .HasColumnName("ID");
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
