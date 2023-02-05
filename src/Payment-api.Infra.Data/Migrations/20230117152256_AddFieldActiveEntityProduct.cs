using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class AddFieldActiveEntityProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "TB_ORDER_ITEMS");

            migrationBuilder.AddColumn<bool>(
                name: "ACTIVE",
                table: "TB_PRODUCTS",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACTIVE",
                table: "TB_PRODUCTS");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "TB_ORDER_ITEMS",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
