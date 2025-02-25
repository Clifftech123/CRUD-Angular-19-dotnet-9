using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().HasColumnName("Id").HasColumnType("uniqueidentifier").HasDefaultValueSql("NEWID()");
                entity.Property(e => e.Name).IsRequired().HasColumnName("Name").HasColumnType("nvarchar(255)");
                entity.Property(e => e.Price).IsRequired().HasColumnName("Price").HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description).IsRequired().HasMaxLength(255);

            });
        }
    }

}
