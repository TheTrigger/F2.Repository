using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oibi.Repository.Demo.Models.Migrations
{
    public partial class DataSeedingTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("ac8fab65-6ba2-4a2d-9789-645d02e8b54b"), new DateTime(2021, 4, 16, 23, 53, 0, 579, DateTimeKind.Utc).AddTicks(7484), "From Seeding", new DateTime(2021, 4, 16, 23, 53, 0, 579, DateTimeKind.Utc).AddTicks(7876) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("ac8fab65-6ba2-4a2d-9789-645d02e8b54b"));
        }
    }
}
