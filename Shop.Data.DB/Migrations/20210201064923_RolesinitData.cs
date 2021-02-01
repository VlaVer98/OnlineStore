using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.DB.Migrations
{
    public partial class RolesinitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("515f47dc-c4f1-4041-a385-bc6b0991f517"), "515f47dc-c4f1-4041-a385-bc6b0991f517", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1153afda-1fbd-446a-b37d-6b62ffbe3204"), "1153afda-1fbd-446a-b37d-6b62ffbe3204", "Buyer", "BUYER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1153afda-1fbd-446a-b37d-6b62ffbe3204"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("515f47dc-c4f1-4041-a385-bc6b0991f517"));
        }
    }
}
