using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMM.Data.Migrations
{
    public partial class RolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3496e2c0-5c86-4f63-bec3-eec830d11ee5", "7f6a871f-3aeb-4d1b-9f95-08c5b7fdc196", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "62a70624-bced-45b1-802c-f81c43ed36c5", "ae094729-4c52-4256-b955-a4cbe3b88c1b", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5455f66-6fe4-4c49-9f44-597cb8e5e017", "bfd356b5-2b82-4e21-b937-fc64ed4f3146", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3496e2c0-5c86-4f63-bec3-eec830d11ee5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62a70624-bced-45b1-802c-f81c43ed36c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5455f66-6fe4-4c49-9f44-597cb8e5e017");
        }
    }
}
