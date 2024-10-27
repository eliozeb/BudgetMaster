using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4a48f42-714e-4a22-b354-892196a390e3", "AQAAAAIAAYagAAAAEOx+Fc1XQVNE7E5ZxZh7kj/BDtX23DK8WUhWvI8VfvioDOGfSGjNZwgPsehlrJsvNQ==", "913c319e-0437-4185-a482-3d242bb80379" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dfe1888-bbf8-42d9-82d3-0f76fe1f0500", "AQAAAAIAAYagAAAAEDjNYdgjh5YeOiiIxXaALso5Zvopz7v8LRVnPzi2b0ipgsaoJYZdl/2/jFnGWXIFbg==", "6491d593-6c12-40be-a824-53db06008f4a" });
        }
    }
}
