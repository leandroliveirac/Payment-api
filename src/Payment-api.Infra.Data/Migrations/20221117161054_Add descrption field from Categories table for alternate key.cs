using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class AdddescrptionfieldfromCategoriestableforalternatekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES",
                column: "DESCRIPTION");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES");
        }
    }
}
