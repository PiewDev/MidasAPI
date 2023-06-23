using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midas.Net.Database.Migrations
{
    /// <inheritdoc />
    public partial class NombredelaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Uri = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Header = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserOrigin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatusCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_types",
                columns: table => new
                {
                    ProductTypeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_types", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    SaleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_products_product_types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "product_types",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sale_details",
                columns: table => new
                {
                    SaleDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_details", x => x.SaleDetailId);
                    table.ForeignKey(
                        name: "FK_sale_details_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sale_details_sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_TypeId",
                table: "products",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_details_ProductId",
                table: "sale_details",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_sale_details_SaleId",
                table: "sale_details",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "sale_details");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "product_types");
        }
    }
}
