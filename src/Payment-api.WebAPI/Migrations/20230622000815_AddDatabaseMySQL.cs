using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseMySQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_CATEGORIES",
                columns: table => new
                {
                    ID_CATEGORY = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DESCRIPTION = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CATEGORIES", x => x.ID_CATEGORY);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_ORDERS",
                columns: table => new
                {
                    ID_ORDER = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDERS", x => x.ID_ORDER);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_SELLERS",
                columns: table => new
                {
                    ID_SELLER = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NAME = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_EMAIL = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NR_CPF = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SELLERS", x => x.ID_SELLER);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_PRODUCTS",
                columns: table => new
                {
                    ID_PRODUCT = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DESCRIPTION = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRICE = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ACTIVE = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PRODUCTS", x => x.ID_PRODUCT);
                    table.ForeignKey(
                        name: "FK_TB_PRODUCTS_TB_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TB_CATEGORIES",
                        principalColumn: "ID_CATEGORY",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_PHONES",
                columns: table => new
                {
                    ID_PHONE = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DDD = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NUMBER = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TYPE = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ID_SELLER = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_SALES",
                columns: table => new
                {
                    ID_SALE = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    MOMENT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    STATUS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ID_SELLER = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ID_ORDER = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SALES", x => x.ID_SALE);
                    table.ForeignKey(
                        name: "FK_TB_SALES_TB_ORDERS_ID_ORDER",
                        column: x => x.ID_ORDER,
                        principalTable: "TB_ORDERS",
                        principalColumn: "ID_ORDER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_SALES_TB_SELLERS_ID_SELLER",
                        column: x => x.ID_SELLER,
                        principalTable: "TB_SELLERS",
                        principalColumn: "ID_SELLER",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TB_ORDER_ITEMS",
                columns: table => new
                {
                    ID_ORDER_ITEM = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ID_ORDER = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ID_PRODUCT = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DS_PRODUCT = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PRODUCT_UNIT_PRICE = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ORDER_ITEMS", x => x.ID_ORDER_ITEM);
                    table.ForeignKey(
                        name: "FK_TB_ORDER_ITEMS_TB_ORDERS_ID_ORDER",
                        column: x => x.ID_ORDER,
                        principalTable: "TB_ORDERS",
                        principalColumn: "ID_ORDER",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ORDER_ITEMS_TB_PRODUCTS_ID_PRODUCT",
                        column: x => x.ID_PRODUCT,
                        principalTable: "TB_PRODUCTS",
                        principalColumn: "ID_PRODUCT",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CATEGORIES_DESCRIPTION",
                table: "TB_CATEGORIES",
                column: "DESCRIPTION",
                unique: true);

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
                name: "IX_TB_PRODUCTS_CategoryId",
                table: "TB_PRODUCTS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALES_ID_ORDER",
                table: "TB_SALES",
                column: "ID_ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SALES_ID_SELLER",
                table: "TB_SALES",
                column: "ID_SELLER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_SELLERS_NR_CPF",
                table: "TB_SELLERS",
                column: "NR_CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ORDER_ITEMS");

            migrationBuilder.DropTable(
                name: "TB_PHONES");

            migrationBuilder.DropTable(
                name: "TB_SALES");

            migrationBuilder.DropTable(
                name: "TB_PRODUCTS");

            migrationBuilder.DropTable(
                name: "TB_ORDERS");

            migrationBuilder.DropTable(
                name: "TB_SELLERS");

            migrationBuilder.DropTable(
                name: "TB_CATEGORIES");
        }
    }
}
