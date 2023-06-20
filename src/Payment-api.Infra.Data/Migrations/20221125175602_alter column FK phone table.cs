using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class altercolumnFKphonetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PHONES_TB_SELLERS_ID_PHONE",
                table: "TB_PHONES");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TB_PHONES_TB_SELLERS_ID_PHONE",
                table: "TB_PHONES",
                column: "ID_PHONE",
                principalTable: "TB_SELLERS",
                principalColumn: "ID_SELLER",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
