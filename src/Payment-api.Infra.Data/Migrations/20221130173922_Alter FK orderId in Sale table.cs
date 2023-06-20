using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class AlterFKorderIdinSaletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALES_TB_ORDERS_ID_SALE",
                table: "TB_SALES");

            migrationBuilder.AlterColumn<string>(
                name: "TYPE",
                table: "TB_PHONES",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALES_ID_ORDER",
                table: "TB_SALES",
                column: "ID_ORDER");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALES_TB_ORDERS_ID_ORDER",
                table: "TB_SALES",
                column: "ID_ORDER",
                principalTable: "TB_ORDERS",
                principalColumn: "ID_ORDER",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALES_TB_ORDERS_ID_ORDER",
                table: "TB_SALES");

            migrationBuilder.DropIndex(
                name: "IX_TB_SALES_ID_ORDER",
                table: "TB_SALES");

            migrationBuilder.AlterColumn<int>(
                name: "TYPE",
                table: "TB_PHONES",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALES_TB_ORDERS_ID_SALE",
                table: "TB_SALES",
                column: "ID_SALE",
                principalTable: "TB_ORDERS",
                principalColumn: "ID_ORDER",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
