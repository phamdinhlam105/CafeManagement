using CafeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Data
{
    public class CafeManagementDbContext : DbContext
    {
        public CafeManagementDbContext(DbContextOptions<CafeManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.HasKey(p => p.Id);
                e.HasMany(p => p.Details)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.ProductId);
            });

            modelBuilder.Entity<OrderDetail>(e =>
            {
                e.ToTable("OrderDetail");
                e.HasKey(od => od.Id);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(o => o.Id);
                e.Property(o => o.createdAt).HasDefaultValueSql("getutcdate()");
                e.HasDiscriminator<string>("OrderType")
                    .HasValue<OnlineOrder>("Online")
                    .HasValue<InStoreOrder>("InStore");
                e.HasMany(o => o.Details)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OderId);
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired();
                e.HasMany(c => c.Products)
                    .WithOne(p => p.Category) 
                    .HasForeignKey(p => p.CategoryId);
            });
            modelBuilder.Entity<Customer>(e =>
            {
                e.ToTable("Customer");
                e.HasKey(c => c.Id);
                e.HasMany(c => c.Orders)  
                    .WithOne() 
                    .HasForeignKey("CustomerId");
            });
        }
    }
}
