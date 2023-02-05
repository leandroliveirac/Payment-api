using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class addnewtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CategoryId",
                table: "PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PRODUCTS",
                table: "PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CATEGORIES",
                table: "CATEGORIES");

            migrationBuilder.RenameTable(
                name: "PRODUCTS",
                newName: "TB_PRODUCTS");

            migrationBuilder.RenameTable(
                name: "CATEGORIES",
                newName: "TB_CATEGORIES");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCTS_CategoryId",
                table: "TB_PRODUCTS",
                newName: "IX_TB_PRODUCTS_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CATEGORIES_DESCRIPTION",
                table: "TB_CATEGORIES",
                newName: "IX_TB_CATEGORIES_DESCRIPTION");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_PRODUCTS",
                table: "TB_PRODUCTS",
                column: "ID_PRODUCT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_CATEGORIES",
                table: "TB_CATEGORIES",
                column: "ID_CATEGORY");

            migrationBuilder.CreateTable(
                name: "TB_ORDER",
                columns: table => new
                {
                    ID_ORDER = table.Column<Guid>(type: "TEXT", nullable: false),
                    DATE = table.Column<DateTime>(type: "TEXT", nullable: false),
                    STATUS = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDER", x => x.ID_ORDER);
                });

            migrationBuilder.CreateTable(
                name: "TB_SELLERS",
                columns: table => new
                {
                    ID_SELLER = table.Column<Guid>(type: "TEXT", nullable: false),
                    NAME = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "TEXT", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SELLERS", x => x.ID_SELLER);
                });

            migrationBuilder.CreateTable(
                name: "TB_ORDER_ITEMS",
                columns: table => new
                {
                    ID_ORDER_ITEM = table.Column<Guid>(type: "TEXT", nullable: false),
                    QUANTITY = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_PRODUCT = table.Column<Guid>(type: "TEXT", nullable: false),
                    ID_ORDER = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDER_ITEMS", x => x.ID_ORDER_ITEM);
                    table.ForeignKey(
                        name: "FK_TB_ORDER_ITEMS_TB_ORDER_ID_ORDER",
                        column: x => x.ID_ORDER,
                        principalTable: "TB_ORDER",
                        principalColumn: "ID_ORDER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ORDER_ITEMS_TB_PRODUCTS_ID_PRODUCT",
                        column: x => x.ID_PRODUCT,
                        principalTable: "TB_PRODUCTS",
                        principalColumn: "ID_PRODUCT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_PHONES",
                columns: table => new
                {
                    ID_PHONE = table.Column<Guid>(type: "TEXT", nullable: false),
                    DDD = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    NUMBER = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    TYPE = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_SELLER = table.Column<Guid>(type: "TEXT", nullable: false),
                    SellerEntityId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PHONES", x => x.ID_PHONE);
                    table.ForeignKey(
                        name: "FK_TB_PHONES_TB_SELLERS_ID_SELLER",
                        column: x => x.ID_SELLER,
                        principalTable: "TB_SELLERS",
                        principalColumn: "ID_SELLER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PHONES_TB_SELLERS_SellerEntityId1",
                        column: x => x.SellerEntityId1,
                        principalTable: "TB_SELLERS",
                        principalColumn: "ID_SELLER");
                });

            migrationBuilder.CreateTable(
                name: "TB_SALES",
                columns: table => new
                {
                    ID_SALE = table.Column<Guid>(type: "TEXT", nullable: false),
                    MOMENT = table.Column<DateTime>(type: "TEXT", nullable: false),
                    STATUS = table.Column<int>(type: "INTEGER", nullable: false),
                    ID_SELLER = table.Column<Guid>(type: "TEXT", nullable: false),
                    ID_ORDER = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SALES", x => x.ID_SALE);
                    table.ForeignKey(
                        name: "FK_TB_SALES_TB_ORDER_ID_SALE",
                        column: x => x.ID_SALE,
                        principalTable: "TB_ORDER",
                        principalColumn: "ID_ORDER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_SALES_TB_SELLERS_ID_SELLER",
                        column: x => x.ID_SELLER,
                        principalTable: "TB_SELLERS",
                        principalColumn: "ID_SELLER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ORDER_ITEMS_ID_ORDER",
                table: "TB_ORDER_ITEMS",
                column: "ID_ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ORDER_ITEMS_ID_PRODUCT",
                table: "TB_ORDER_ITEMS",
                column: "ID_PRODUCT");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PHONES_ID_SELLER",
                table: "TB_PHONES",
                column: "ID_SELLER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PHONES_SellerEntityId1",
                table: "TB_PHONES",
                column: "SellerEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALES_ID_SELLER",
                table: "TB_SALES",
                column: "ID_SELLER");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PRODUCTS_TB_CATEGORIES_CategoryId",
                table: "TB_PRODUCTS",
                column: "CategoryId",
                principalTable: "TB_CATEGORIES",
                principalColumn: "ID_CATEGORY",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRODUCTS_TB_CATEGORIES_CategoryId",
                table: "TB_PRODUCTS");

            migrationBuilder.DropTable(
                name: "TB_ORDER_ITEMS");

            migrationBuilder.DropTable(
                name: "TB_PHONES");

            migrationBuilder.DropTable(
                name: "TB_SALES");

            migrationBuilder.DropTable(
                name: "TB_ORDER");

            migrationBuilder.DropTable(
                name: "TB_SELLERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_PRODUCTS",
                table: "TB_PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_CATEGORIES",
                table: "TB_CATEGORIES");

            migrationBuilder.RenameTable(
                name: "TB_PRODUCTS",
                newName: "PRODUCTS");

            migrationBuilder.RenameTable(
                name: "TB_CATEGORIES",
                newName: "CATEGORIES");

            migrationBuilder.RenameIndex(
                name: "IX_TB_PRODUCTS_CategoryId",
                table: "PRODUCTS",
                newName: "IX_PRODUCTS_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_CATEGORIES_DESCRIPTION",
                table: "CATEGORIES",
                newName: "IX_CATEGORIES_DESCRIPTION");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRODUCTS",
                table: "PRODUCTS",
                column: "ID_PRODUCT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CATEGORIES",
                table: "CATEGORIES",
                column: "ID_CATEGORY");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CategoryId",
                table: "PRODUCTS",
                column: "CategoryId",
                principalTable: "CATEGORIES",
                principalColumn: "ID_CATEGORY",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
