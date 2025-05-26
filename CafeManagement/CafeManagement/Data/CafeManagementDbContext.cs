using CafeManagement.Interfaces;
using CafeManagement.Models;
using CafeManagement.Models.Order;
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.ProductModel;
using CafeManagement.Models.PromotionModel;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CafeManagement.Data
{
    public class CafeManagementDbContext : IdentityDbContext<User>
    {
        public CafeManagementDbContext(DbContextOptions<CafeManagementDbContext> options)
            : base(options)
        {
        }
        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeDetail> RecipeDetails { get; set; }
        #endregion

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        #region Promotion
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionSchedule> PromotionSchedules {  get; set; }
        #endregion

        #region Stock
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DailyStock> DailyStocks {  get; set; }
        public DbSet<StockEntry> StockEntries { get; set; }
        public DbSet<StockEntryDetail> StockEntryDetails {  get; set; }
        public DbSet<StockUsageLog> StockUsageLogs {  get; set; }
        public DbSet<StockUsageDetail> StockUsageDetails { get; set; }
        public DbSet<StockAdjustment> StockAdjustments { get; set; }
        public DbSet<AdjustmentDetail> AdjustmentDetails { get; set; }
        #endregion

        #region Report
        public DbSet<OrderReport> OrderReports {  get; set; }
        public DbSet<StockReport> StockReports { get; set; }
        public DbSet<ProductReport> ProductReports { get; set; }
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<MonthlyReport> MonthlyReports { get; set; }
        public DbSet<QuarterlyReport> QuarterlyReports { get; set; }
        public DbSet<YearlyReport> YearlyReports { get; set; }
        #endregion

        #region User
        public DbSet<Profile> Profiles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);
                e.HasQueryFilter(p => !p.IsDeleted);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);
                e.HasMany(od => od.Details)
                    .WithOne(o => o.Order)
                    .HasForeignKey(od => od.OrderId);
                e.HasQueryFilter(p => !p.IsDeleted);
            });


            modelBuilder.Entity<Category>(e =>
            {
                e.Property(p => p.IsDeleted)
                 .HasDefaultValue(false);
                e.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId);
                e.HasQueryFilter(p => !p.IsDeleted);
            });
            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(p => p.IsDeleted)
                 .HasDefaultValue(false);
                e.HasMany(c => c.Orders)
                    .WithOne(o => o.Customer)
                    .HasForeignKey(o => o.CustomerId);
                e.HasQueryFilter(p => !p.IsDeleted);
            });

            modelBuilder.Entity<OrderStatusHistory>(e =>
            {
                e.Property(sh => sh.NewStatus).IsRequired();
                e.Property(sh => sh.Description).IsRequired();
            });

            modelBuilder.Entity<Promotion>(e =>
            {
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
                e.Property(p => p.Age);
                e.Property(p => p.BirthDay);
                e.Property(p => p.Email).IsRequired();
                e.Property(p => p.PhoneNumber);
                e.Property(p => p.PictureURL);
                e.Property(p => p.joinDate).IsRequired();
            });


            #region SoftDeleteFilter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType is ISoftDeletable)
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
            #endregion
        }
    }
}
