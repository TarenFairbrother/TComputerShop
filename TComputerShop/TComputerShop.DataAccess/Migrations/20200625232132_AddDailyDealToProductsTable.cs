using Microsoft.EntityFrameworkCore.Migrations;

namespace TComputerShop.Data.Migrations
{
    public partial class AddDailyDealToProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DailyDeal",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyDeal",
                table: "Product");
        }
    }
}
