﻿// <auto-generated />
using System;
using CafeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeManagement.Migrations
{
    [DbContext(typeof(CafeManagementDbContext))]
    [Migration("20250304025909_UploadDatabase")]
    partial class UploadDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CafeManagement.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CafeManagement.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("YearlyReportId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("YearlyReportId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<int>("No")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("PromotionId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PromotionId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("OderId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.OrderStatusHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NewStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderStatusHistory");
                });

            modelBuilder.Entity("CafeManagement.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Img")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CafeManagement.Models.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int?>("Age")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateOnly?>("BirthDay")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PictureURL")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("joinDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("CafeManagement.Models.PromotionModel.Promotion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("isActive")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("CafeManagement.Models.PromotionModel.PromotionSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PromotionId")
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("endDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("startDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("PromotionId");

                    b.ToTable("PromotionSchedules");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.DailyReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LeastSellingId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("MonthlyReportId")
                        .HasColumnType("char(36)");

                    b.Property<int>("NumberOfCancelledOrders")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfFinishedOrders")
                        .HasColumnType("int");

                    b.Property<string>("PeakHours")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("ReportDate")
                        .HasColumnType("date");

                    b.Property<Guid>("TopSellingId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalExpenditure")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TotalProductsSold")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("LeastSellingId");

                    b.HasIndex("MonthlyReportId");

                    b.HasIndex("TopSellingId");

                    b.ToTable("DailyReports");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.MonthlyReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<Guid>("LeastSellingId")
                        .HasColumnType("char(36)");

                    b.Property<int>("NumberOfCancelledOrders")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfFinishedOrders")
                        .HasColumnType("int");

                    b.Property<Guid?>("QuarterlyReportId")
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid>("TopSellingId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalExpenditure")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TotalProductsSold")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("LeastSellingId");

                    b.HasIndex("QuarterlyReportId");

                    b.HasIndex("TopSellingId");

                    b.ToTable("MonthlyReports");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.QuarterlyReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<Guid>("LeastSellingId")
                        .HasColumnType("char(36)");

                    b.Property<int>("NumberOfCancelledOrders")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfFinishedOrders")
                        .HasColumnType("int");

                    b.Property<int>("Quarter")
                        .HasColumnType("int");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<Guid>("TopSellingId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalExpenditure")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TotalProductsSold")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid?>("YearlyReportId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("LeastSellingId");

                    b.HasIndex("TopSellingId");

                    b.HasIndex("YearlyReportId");

                    b.ToTable("QuarterlyReports");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.YearlyReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("LeastSellingId")
                        .HasColumnType("char(36)");

                    b.Property<int>("NumberOfCancelledOrders")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfFinishedOrders")
                        .HasColumnType("int");

                    b.Property<Guid>("TopSellingId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("TotalExpenditure")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TotalProductsSold")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalRevenue")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("LeastSellingId");

                    b.HasIndex("TopSellingId");

                    b.ToTable("YearlyReports");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.DailyStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateOnly>("createDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("DailyStocks");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.DailyStockDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DailyStockId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("char(36)");

                    b.Property<float>("StockAtStartOfDay")
                        .HasColumnType("float");

                    b.Property<float>("StockImport")
                        .HasColumnType("float");

                    b.Property<float>("StockRemaining")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DailyStockId");

                    b.HasIndex("IngredientId");

                    b.ToTable("DailyStockDetails");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("MeasurementUnit")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PictureURL")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.StockEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("StockEntries");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.StockEntryDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<float>("Quantity")
                        .HasColumnType("float");

                    b.Property<Guid>("StockEntryId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("StockEntryId");

                    b.ToTable("StockEntryDetails");
                });

            modelBuilder.Entity("CafeManagement.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CafeManagement.Models.Customer", b =>
                {
                    b.HasOne("CafeManagement.Models.Report.YearlyReport", null)
                        .WithMany("TopLoyalCustomers")
                        .HasForeignKey("YearlyReportId");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.Order", b =>
                {
                    b.HasOne("CafeManagement.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("CafeManagement.Models.PromotionModel.Promotion", "Promotion")
                        .WithMany("ApplyOrders")
                        .HasForeignKey("PromotionId");

                    b.Navigation("Customer");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.OrderDetail", b =>
                {
                    b.HasOne("CafeManagement.Models.Order.Order", "Order")
                        .WithMany("Details")
                        .HasForeignKey("OderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Product", "Product")
                        .WithMany("Details")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.OrderStatusHistory", b =>
                {
                    b.HasOne("CafeManagement.Models.Order.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("CafeManagement.Models.Product", b =>
                {
                    b.HasOne("CafeManagement.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CafeManagement.Models.Profile", b =>
                {
                    b.HasOne("CafeManagement.Models.User", null)
                        .WithOne("Profile")
                        .HasForeignKey("CafeManagement.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CafeManagement.Models.PromotionModel.PromotionSchedule", b =>
                {
                    b.HasOne("CafeManagement.Models.PromotionModel.Promotion", "Promotion")
                        .WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.DailyReport", b =>
                {
                    b.HasOne("CafeManagement.Models.Product", "LeastSelling")
                        .WithMany()
                        .HasForeignKey("LeastSellingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Report.MonthlyReport", null)
                        .WithMany("DailyReports")
                        .HasForeignKey("MonthlyReportId");

                    b.HasOne("CafeManagement.Models.Product", "TopSelling")
                        .WithMany()
                        .HasForeignKey("TopSellingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("LeastSelling");

                    b.Navigation("TopSelling");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.MonthlyReport", b =>
                {
                    b.HasOne("CafeManagement.Models.Product", "LeastSelling")
                        .WithMany()
                        .HasForeignKey("LeastSellingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Report.QuarterlyReport", null)
                        .WithMany("MonthlyReports")
                        .HasForeignKey("QuarterlyReportId");

                    b.HasOne("CafeManagement.Models.Product", "TopSelling")
                        .WithMany()
                        .HasForeignKey("TopSellingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("LeastSelling");

                    b.Navigation("TopSelling");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.QuarterlyReport", b =>
                {
                    b.HasOne("CafeManagement.Models.Product", "LeastSelling")
                        .WithMany()
                        .HasForeignKey("LeastSellingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Product", "TopSelling")
                        .WithMany()
                        .HasForeignKey("TopSellingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Report.YearlyReport", null)
                        .WithMany("QuarterlyReports")
                        .HasForeignKey("YearlyReportId");

                    b.Navigation("LeastSelling");

                    b.Navigation("TopSelling");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.YearlyReport", b =>
                {
                    b.HasOne("CafeManagement.Models.Product", "LeastSelling")
                        .WithMany()
                        .HasForeignKey("LeastSellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Product", "TopSelling")
                        .WithMany()
                        .HasForeignKey("TopSellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeastSelling");

                    b.Navigation("TopSelling");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.DailyStockDetail", b =>
                {
                    b.HasOne("CafeManagement.Models.Stock.DailyStock", "DailyStock")
                        .WithMany("DailyStockDetails")
                        .HasForeignKey("DailyStockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Stock.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DailyStock");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.StockEntryDetail", b =>
                {
                    b.HasOne("CafeManagement.Models.Stock.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.Stock.StockEntry", "StockEntry")
                        .WithMany("StockEntryDetails")
                        .HasForeignKey("StockEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("StockEntry");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CafeManagement.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CafeManagement.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeManagement.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CafeManagement.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CafeManagement.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CafeManagement.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CafeManagement.Models.Order.Order", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("CafeManagement.Models.Product", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("CafeManagement.Models.PromotionModel.Promotion", b =>
                {
                    b.Navigation("ApplyOrders");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.MonthlyReport", b =>
                {
                    b.Navigation("DailyReports");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.QuarterlyReport", b =>
                {
                    b.Navigation("MonthlyReports");
                });

            modelBuilder.Entity("CafeManagement.Models.Report.YearlyReport", b =>
                {
                    b.Navigation("QuarterlyReports");

                    b.Navigation("TopLoyalCustomers");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.DailyStock", b =>
                {
                    b.Navigation("DailyStockDetails");
                });

            modelBuilder.Entity("CafeManagement.Models.Stock.StockEntry", b =>
                {
                    b.Navigation("StockEntryDetails");
                });

            modelBuilder.Entity("CafeManagement.Models.User", b =>
                {
                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
