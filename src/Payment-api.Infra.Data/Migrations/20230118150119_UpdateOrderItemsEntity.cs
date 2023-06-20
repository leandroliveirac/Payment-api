using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class UpdateOrderItemsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DS_PRODUCT",
                table: "TB_ORDER_ITEMS",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PRODUCT_UNIT_PRICE",
                table: "TB_ORDER_ITEMS",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DS_PRODUCT",
                table: "TB_ORDER_ITEMS");

            migrationBuilder.DropColumn(
                name: "PRODUCT_UNIT_PRICE",
                table: "TB_ORDER_ITEMS");
        }
    }
}
