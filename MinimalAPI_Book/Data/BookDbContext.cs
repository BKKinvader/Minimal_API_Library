using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using System;
using MinimalAPI_Book.Models;

namespace MinimalAPI_Book.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Love Story",
                Author = "Sara Johnson",
                Genre = "Romance",
                Description = "En romantisk berättelse om kärlek och relationer.",
                IsAvailable = false,
                Created = DateTime.UtcNow,
                DateOfBorrowed = DateTime.UtcNow.AddDays(-10), // Example borrowed 10 days ago
                ReturnedDate = DateTime.UtcNow.AddDays(-5) // Example returned 5 days ago
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Science Fiction World",
                Author = "Mikael Svensson",
                Genre = "Science Fiction",
                Description = "En science fiction-berättelse som utforskar framtiden.",
                IsAvailable = true,
                Created = DateTime.UtcNow,
                DateOfBorrowed = null, // Not borrowed
                ReturnedDate = null
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Mystery Book",
                Author = "Laura Smith",
                Genre = "Mystery",
                Description = "En spännande mysteriebok med gåtor att lösa.",
                IsAvailable = true,
                Created = DateTime.UtcNow,
                DateOfBorrowed = null, // Not borrowed
                ReturnedDate = null
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Historical Fiction Saga",
                Author = "John Adams",
                Genre = "Historical Fiction",
                Description = "En episk historisk roman som tar dig till en annan tid.",
                IsAvailable = false,
                Created = DateTime.UtcNow,
                DateOfBorrowed = DateTime.UtcNow.AddDays(-15), // Example borrowed 15 days ago
                ReturnedDate = DateTime.UtcNow.AddDays(-8) // Example returned 8 days ago
            }


            );
        }
    }
}
