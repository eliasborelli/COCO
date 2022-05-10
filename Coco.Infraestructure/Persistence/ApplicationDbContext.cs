using Coco.Core.Entities;
using Coco.Infraestructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Coco.Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<WorkingDays> WorkingDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StockEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StoreEntityConfiguration());
            modelBuilder.ApplyConfiguration(new WorkingDaysEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
