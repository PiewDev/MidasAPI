using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Midas.Net.Database.Migrations
{
    /// <inheritdoc />
    public partial class nuevamigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DbProductTypeProductTypeId",
                table: "products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_DbProductTypeProductTypeId",
                table: "products",
                column: "DbProductTypeProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_product_types_DbProductTypeProductTypeId",
                table: "products",
                column: "DbProductTypeProductTypeId",
                principalTable: "product_types",
                principalColumn: "ProductTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_product_types_DbProductTypeProductTypeId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_DbProductTypeProductTypeId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "DbProductTypeProductTypeId",
                table: "products");
        }
    }
}
