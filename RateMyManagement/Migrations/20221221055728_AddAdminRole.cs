using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyManagement.Migrations
{
    public partial class AddAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "631418d7-ad65-4b34-9d28-14901dc0c897", "37875bc7-588f-44f0-8419-9cbfc362e79e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "631418d7-ad65-4b34-9d28-14901dc0c897", "37875bc7-588f-44f0-8419-9cbfc362e79e" });
        }
    }
}
