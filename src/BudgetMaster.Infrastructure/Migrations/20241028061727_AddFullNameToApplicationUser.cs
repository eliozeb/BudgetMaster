using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a75b239-36c0-4cec-bb90-27b1699dc6a8", "AQAAAAIAAYagAAAAELFpMywhUSrSb/Wj/thTaEA8TU7wO4LZzJDN3BgRM3pIblpp7P904NotN+OjR2bR2A==", "2132c588-9335-4d5a-b8bc-9edeabe1d517" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4a48f42-714e-4a22-b354-892196a390e3", "AQAAAAIAAYagAAAAEOx+Fc1XQVNE7E5ZxZh7kj/BDtX23DK8WUhWvI8VfvioDOGfSGjNZwgPsehlrJsvNQ==", "913c319e-0437-4185-a482-3d242bb80379" });
        }
    }
}
