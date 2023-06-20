using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment_api.Infra.Data.Migrations
{
    public partial class alterphonetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ORDER_ITEMS_TB_ORDER_ID_ORDER",
                table: "TB_ORDER_ITEMS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_PHONES_TB_SELLERS_SellerEntityId1",
                table: "TB_PHONES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALES_TB_ORDER_ID_SALE",
                table: "TB_SALES");

            migrationBuilder.DropIndex(
                name: "IX_TB_PHONES_SellerEntityId1",
                table: "TB_PHONES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_ORDER",
                table: "TB_ORDER");

            migrationBuilder.DropColumn(
                name: "SellerEntityId1",
                table: "TB_PHONES");

            migrationBuilder.RenameTable(
                name: "TB_ORDER",
                newName: "TB_ORDERS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_ORDERS",
                table: "TB_ORDERS",
                column: "ID_ORDER");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ORDER_ITEMS_TB_ORDERS_ID_ORDER",
                table: "TB_ORDER_ITEMS",
                column: "ID_ORDER",
                principalTable: "TB_ORDERS",
                principalColumn: "ID_ORDER",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PHONES_TB_SELLERS_ID_PHONE",
                table: "TB_PHONES",
                column: "ID_PHONE",
                principalTable: "TB_SELLERS",
                principalColumn: "ID_SELLER",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALES_TB_ORDERS_ID_SALE",
                table: "TB_SALES",
                column: "ID_SALE",
                principalTable: "TB_ORDERS",
                principalColumn: "ID_ORDER",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ORDER_ITEMS_TB_ORDERS_ID_ORDER",
                table: "TB_ORDER_ITEMS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_PHONES_TB_SELLERS_ID_PHONE",
                table: "TB_PHONES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_SALES_TB_ORDERS_ID_SALE",
                table: "TB_SALES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_ORDERS",
                table: "TB_ORDERS");

            migrationBuilder.RenameTable(
                name: "TB_ORDERS",
                newName: "TB_ORDER");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerEntityId1",
                table: "TB_PHONES",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_ORDER",
                table: "TB_ORDER",
                column: "ID_ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PHONES_SellerEntityId1",
                table: "TB_PHONES",
                column: "SellerEntityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ORDER_ITEMS_TB_ORDER_ID_ORDER",
                table: "TB_ORDER_ITEMS",
                column: "ID_ORDER",
                principalTable: "TB_ORDER",
                principalColumn: "ID_ORDER",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PHONES_TB_SELLERS_SellerEntityId1",
                table: "TB_PHONES",
                column: "SellerEntityId1",
                principalTable: "TB_SELLERS",
                principalColumn: "ID_SELLER");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_SALES_TB_ORDER_ID_SALE",
                table: "TB_SALES",
                column: "ID_SALE",
                principalTable: "TB_ORDER",
                principalColumn: "ID_ORDER",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
