using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinimalAPI_Book.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBorrowed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Created", "DateOfBorrowed", "Description", "Genre", "IsAvailable", "ReturnedDate", "Title" },
                values: new object[,]
                {
                    { new Guid("26bde574-b8d7-40d8-92f3-3d316d59bc66"), "John Adams", new DateTime(2023, 9, 7, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(244), new DateTime(2023, 8, 23, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(244), "En episk historisk roman som tar dig till en annan tid.", "Historical Fiction", false, new DateTime(2023, 8, 30, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(245), "Historical Fiction Saga" },
                    { new Guid("45063f3c-aa51-4fb8-a9e3-56f8eec57778"), "Mikael Svensson", new DateTime(2023, 9, 7, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(241), null, "En science fiction-berättelse som utforskar framtiden.", "Science Fiction", true, null, "Science Fiction World" },
                    { new Guid("68da34b5-c6e4-47c8-8f21-87a82b0f8245"), "Sara Johnson", new DateTime(2023, 9, 7, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(236), new DateTime(2023, 8, 28, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(237), "En romantisk berättelse om kärlek och relationer.", "Romance", false, new DateTime(2023, 9, 2, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(240), "Love Story" },
                    { new Guid("771e64e1-7a2e-4abc-85bc-a0f3facc05f0"), "Laura Smith", new DateTime(2023, 9, 7, 14, 0, 19, 252, DateTimeKind.Utc).AddTicks(243), null, "En spännande mysteriebok med gåtor att lösa.", "Mystery", true, null, "The Mystery Book" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
