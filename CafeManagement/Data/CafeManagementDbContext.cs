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
        public DbSet<OnlineOrder> OnlineOrders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
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
                e.Property(e => e.No).IsRequired();
                e.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                e.Property(e => e.Quantity).IsRequired();
                e.Property(e => e.createdAt).IsRequired();
                e.Property(e => e.Note).HasMaxLength(500);
                e.Property(e => e.OrderStatus).IsRequired();
                e.Property(e => e.OrderType).IsRequired();

                e.HasMany(od => od.Details)
                    .WithOne(o => o.Order)
                    .HasForeignKey(od => od.OderId);

                e.HasMany(sh => sh.StatusHistories)
                    .WithOne(o => o.Order)
                    .HasForeignKey(o => o.OrderId);
            });

            modelBuilder.Entity<OnlineOrder>(e =>
            {
                e.ToTable("OnlineOrders"); 
                e.Property(e => e.DeliveryTime).IsRequired();
                e.Property(e => e.ShippingCost).IsRequired().HasColumnType("decimal(18,2)"); ;
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
                    .WithOne(o=>o.Customer) 
                    .HasForeignKey(o=>o.CustomerId);
            });

            modelBuilder.Entity<OrderStatusHistory>(e =>
            {
                e.ToTable("OrderStatusHistories");
                e.Property(sh => sh.NewStatus).IsRequired();
                e.Property(sh => sh.Description).IsRequired();
            });
        }
    }
}
