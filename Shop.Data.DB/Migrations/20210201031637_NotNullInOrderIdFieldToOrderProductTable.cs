using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.DB.Migrations
{
    public partial class NotNullInOrderIdFieldToOrderProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderProducts_Orders_OrderId",
                table: "orderProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "orderProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_orderProducts_Orders_OrderId",
                table: "orderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderProducts_Orders_OrderId",
                table: "orderProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "orderProducts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_orderProducts_Orders_OrderId",
                table: "orderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
