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
                    { new Guid("5d6a2441-33d3-4288-a853-0ceec4a10d1f"), "John Adams", new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(28), new DateTime(2023, 9, 4, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(28), "En episk historisk roman som tar dig till en annan tid.", "Historical Fiction", false, new DateTime(2023, 9, 11, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(29), "Historical Fiction Saga" },
                    { new Guid("69988e8f-896b-4ae7-9452-01770fbe1f3b"), "Laura Smith", new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(27), null, "En spännande mysteriebok med gåtor att lösa.", "Mystery", true, null, "The Mystery Book" },
                    { new Guid("cc2a6b71-6149-4e23-9f70-87e990b8c4cd"), "Mikael Svensson", new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(25), null, "En science fiction-berättelse som utforskar framtiden.", "Science Fiction", true, null, "Science Fiction World" },
                    { new Guid("f37fcf9d-4ffd-4302-9893-528c70818633"), "Sara Johnson", new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(20), new DateTime(2023, 9, 9, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(21), "En romantisk berättelse om kärlek och relationer.", "Romance", false, new DateTime(2023, 9, 14, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(24), "Love Story" }
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
