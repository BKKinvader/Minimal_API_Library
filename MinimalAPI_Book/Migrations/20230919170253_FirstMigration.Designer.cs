﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinimalAPI_Book.Data;

#nullable disable

namespace MinimalAPI_Book.Migrations
{
    [DbContext(typeof(BookDbContext))]
    [Migration("20230919170253_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MinimalAPI_Book.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBorrowed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ReturnedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f37fcf9d-4ffd-4302-9893-528c70818633"),
                            Author = "Sara Johnson",
                            Created = new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(20),
                            DateOfBorrowed = new DateTime(2023, 9, 9, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(21),
                            Description = "En romantisk berättelse om kärlek och relationer.",
                            Genre = "Romance",
                            IsAvailable = false,
                            ReturnedDate = new DateTime(2023, 9, 14, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(24),
                            Title = "Love Story"
                        },
                        new
                        {
                            Id = new Guid("cc2a6b71-6149-4e23-9f70-87e990b8c4cd"),
                            Author = "Mikael Svensson",
                            Created = new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(25),
                            Description = "En science fiction-berättelse som utforskar framtiden.",
                            Genre = "Science Fiction",
                            IsAvailable = true,
                            Title = "Science Fiction World"
                        },
                        new
                        {
                            Id = new Guid("69988e8f-896b-4ae7-9452-01770fbe1f3b"),
                            Author = "Laura Smith",
                            Created = new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(27),
                            Description = "En spännande mysteriebok med gåtor att lösa.",
                            Genre = "Mystery",
                            IsAvailable = true,
                            Title = "The Mystery Book"
                        },
                        new
                        {
                            Id = new Guid("5d6a2441-33d3-4288-a853-0ceec4a10d1f"),
                            Author = "John Adams",
                            Created = new DateTime(2023, 9, 19, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(28),
                            DateOfBorrowed = new DateTime(2023, 9, 4, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(28),
                            Description = "En episk historisk roman som tar dig till en annan tid.",
                            Genre = "Historical Fiction",
                            IsAvailable = false,
                            ReturnedDate = new DateTime(2023, 9, 11, 17, 2, 53, 306, DateTimeKind.Utc).AddTicks(29),
                            Title = "Historical Fiction Saga"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
