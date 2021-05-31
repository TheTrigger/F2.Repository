using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oibi.Repository.Demo.Models.Migrations
{
    public partial class AllConfigurationsTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ac8fab65-6ba2-4a2d-9789-645d02e8b54b"));

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("52fcbd69-7ea4-4654-a64a-f1ffb1c16202"), new DateTime(2021, 5, 31, 17, 47, 59, 519, DateTimeKind.Utc).AddTicks(7871), "From Seeding", new DateTime(2021, 5, 31, 17, 47, 59, 519, DateTimeKind.Utc).AddTicks(8117) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("52fcbd69-7ea4-4654-a64a-f1ffb1c16202"));

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("ac8fab65-6ba2-4a2d-9789-645d02e8b54b"), new DateTime(2021, 4, 16, 23, 53, 0, 579, DateTimeKind.Utc).AddTicks(7484), "From Seeding", new DateTime(2021, 4, 16, 23, 53, 0, 579, DateTimeKind.Utc).AddTicks(7876) });
        }
    }
}
