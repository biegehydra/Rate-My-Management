using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateMyManagement.Migrations
{
    public partial class RolesAndAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "631418d7-ad65-4b34-9d28-14901dc0c897", "e54f557d-6105-4519-88b7-238b6ec438a9", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff58952b-d3ef-4997-b61a-80d81b97b009", "f6c4a454-78d6-4eaa-a999-80c3fcc4d31f", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "37875bc7-588f-44f0-8419-9cbfc362e79e", 0, "ff7c1ccb-8999-4b1a-af05-692128ec2f33", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEE9wbiDMioih+rgxXqiZVE/w5v5F4TjX3GcO9VVj4S1kxzay0TMtB58MrZC2KoIH1g==", null, false, "817de208-9709-4b2d-9a38-9f986d74d072", false, "Administrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "631418d7-ad65-4b34-9d28-14901dc0c897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff58952b-d3ef-4997-b61a-80d81b97b009");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37875bc7-588f-44f0-8419-9cbfc362e79e");
        }
    }
}
