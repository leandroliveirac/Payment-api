using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class addedfieldCPFtableseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NR_CPF",
                table: "TB_SELLERS",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SELLERS_NR_CPF",
                table: "TB_SELLERS",
                column: "NR_CPF",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_SELLERS_NR_CPF",
                table: "TB_SELLERS");

            migrationBuilder.DropColumn(
                name: "NR_CPF",
                table: "TB_SELLERS");
        }
    }
}
