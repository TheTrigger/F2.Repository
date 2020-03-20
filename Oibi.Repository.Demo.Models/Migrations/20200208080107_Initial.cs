using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oibi.Repository.Demo.Models.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Isbn = table.Column<string>(maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthors_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e7dfaa14-0c5d-475a-b78b-6e9014d10a3e"), new DateTime(2020, 2, 8, 9, 1, 6, 773, DateTimeKind.Local).AddTicks(714), "William Shakespeare", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cec0fb8e-4935-4e85-a271-2a2814d632cd"), new DateTime(2020, 2, 8, 9, 1, 6, 775, DateTimeKind.Local).AddTicks(2681), "Oibi.dev", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedAt", "Isbn", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e399eca2-b840-4210-99c6-33becc6a7d4b"), new DateTime(2020, 2, 8, 9, 1, 6, 776, DateTimeKind.Local).AddTicks(3294), "1234567890123", "Hamlet", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("80d521c9-431a-4324-b078-8d25e9d2da4c"), new DateTime(2020, 2, 8, 9, 1, 6, 776, DateTimeKind.Local).AddTicks(4153), "0987654321045", "King Lear", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("642d6404-6957-4361-83e8-10098044ec6a"), new DateTime(2020, 2, 8, 9, 1, 6, 776, DateTimeKind.Local).AddTicks(4179), null, "Random Othello w/ no authors", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "AuthorId", "BookId", "CreatedAt", "UpdatedAt" },
                values: new object[] { new Guid("22b5e183-655a-fca7-92c1-3abc998e8ac3"), new Guid("e7dfaa14-0c5d-475a-b78b-6e9014d10a3e"), new Guid("e399eca2-b840-4210-99c6-33becc6a7d4b"), new DateTime(2020, 2, 8, 9, 1, 6, 776, DateTimeKind.Local).AddTicks(4806), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "AuthorId", "BookId", "CreatedAt", "UpdatedAt" },
                values: new object[] { new Guid("54c50511-81fe-c049-283b-f115af97335f"), new Guid("e7dfaa14-0c5d-475a-b78b-6e9014d10a3e"), new Guid("80d521c9-431a-4324-b078-8d25e9d2da4c"), new DateTime(2020, 2, 8, 9, 1, 6, 776, DateTimeKind.Local).AddTicks(5702), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "Id", "AuthorId", "BookId", "CreatedAt", "UpdatedAt" },
                values: new object[] { new Guid("9b9e0193-44b8-52c3-c6b8-c1f1d871fd4b"), new Guid("cec0fb8e-4935-4e85-a271-2a2814d632cd"), new Guid("80d521c9-431a-4324-b078-8d25e9d2da4c"), new DateTime(2020, 2, 8, 9, 1, 6, 776, DateTimeKind.Local).AddTicks(5732), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "BookAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookId",
                table: "BookAuthors",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
