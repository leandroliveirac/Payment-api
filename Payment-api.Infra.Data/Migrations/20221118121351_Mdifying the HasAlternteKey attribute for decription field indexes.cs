using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class MdifyingtheHasAlternteKeyattributefordecriptionfieldindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES",
                column: "DESCRIPTION",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES",
                column: "DESCRIPTION");
        }
    }
}
