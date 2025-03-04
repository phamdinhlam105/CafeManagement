using CafeManagement.Models;
using CafeManagement.Models.Order;
using CafeManagement.Models.PromotionModel;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Data
{
    public class CafeManagementDbContext : IdentityDbContext<User>
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
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionSchedule> PromotionSchedules {  get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DailyStock> DailyStocks {  get; set; }
        public DbSet<DailyStockDetail> DailyStockDetails {  get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<StockEntryDetail> StockEntryDetails {  get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<MonthlyReport> MonthlyReports { get; set; }
        public DbSet<QuarterlyReport> QuarterlyReports { get; set; }
        public DbSet<YearlyReport> YearlyReports { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
                e.HasMany(p => p.Details)
                    .WithOne(od => od.Product)
                    .HasForeignKey(od => od.ProductId);
            });



            modelBuilder.Entity<Order>(e =>
            {
                e.Property(e => e.No).IsRequired();
                e.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                e.Property(e => e.Quantity).IsRequired();
                e.Property(e => e.createdAt).IsRequired();
                e.Property(e => e.Note).HasMaxLength(500);
                e.Property(e => e.OrderStatus).IsRequired();

                e.HasMany(od => od.Details)
                    .WithOne(o => o.Order)
                    .HasForeignKey(od => od.OderId);
            });


            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired();
                e.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId);
            });
            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(c => c.Id);
                e.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerId);
            });

            modelBuilder.Entity<OrderStatusHistory>(e =>
            {
                e.Property(sh => sh.NewStatus).IsRequired();
                e.Property(sh => sh.Description).IsRequired();
            });


            modelBuilder.Entity<Promotion>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Description).IsRequired();
                e.HasMany(p => p.ApplyOrders)
                    .WithOne(od => od.Promotion)
                    .HasForeignKey(od => od.PromotionId);
            });
            modelBuilder.Entity<User>(e =>
            {
                e.HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Profile>(e =>
            {
                e.ToTable("Profiles");
                e.HasKey(p => p.Id);
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Age).IsRequired();
                e.Property(p => p.BirthDay);
                e.Property(p => p.Email).IsRequired();
                e.Property(p => p.PhoneNumber).IsRequired();
                e.Property(p => p.PictureURL);
                e.Property(p => p.joinDate).IsRequired();
            });

            modelBuilder.Entity<MonthlyReport>(entity =>
            {
                entity.HasOne(r => r.TopSelling)
                      .WithMany()
                      .HasForeignKey(r => r.TopSellingId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(r => r.LeastSelling)
                      .WithMany()
                      .HasForeignKey(r => r.LeastSellingId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<QuarterlyReport>(entity =>
            {
                entity.HasOne(r => r.TopSelling)
                      .WithMany()
                      .HasForeignKey(r => r.TopSellingId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(r => r.LeastSelling)
                      .WithMany()
                      .HasForeignKey(r => r.LeastSellingId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<DailyReport>(entity =>
            {
                entity.HasOne(r => r.TopSelling)
                      .WithMany()
                      .HasForeignKey(r => r.TopSellingId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(r => r.LeastSelling)
                      .WithMany()
                      .HasForeignKey(r => r.LeastSellingId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
