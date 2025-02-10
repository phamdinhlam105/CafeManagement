using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagement.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No",
                table: "OrderDetail");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_PromotionId",
                table: "Order",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Promotion_PromotionId",
                table: "Order",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Promotion_PromotionId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropIndex(
                name: "IX_Order_PromotionId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
