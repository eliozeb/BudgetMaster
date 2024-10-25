using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetMaster.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondUpdateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dfe1888-bbf8-42d9-82d3-0f76fe1f0500", "AQAAAAIAAYagAAAAEDjNYdgjh5YeOiiIxXaALso5Zvopz7v8LRVnPzi2b0ipgsaoJYZdl/2/jFnGWXIFbg==", "6491d593-6c12-40be-a824-53db06008f4a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8b0bf7c-da27-46db-b5f5-f86971d84ff7", "AQAAAAIAAYagAAAAEFluM7SIWF6+alEa0jSKhVMk/IGz9m6/jGWg6sNlwBaGlPrKY/k9lggeBKQAmUa/Jg==", "e15f0987-c41d-40e7-971a-62ad1bd0c307" });
        }
    }
}
