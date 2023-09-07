using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;
using System;
using MinimalAPI_Book.Models;

namespace MinimalAPI_Book.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
            new Book 
            { 
                Id = 1, 
                Title = "Sagan om Tim", 
                Author = "Tim Nilsson", Genre = "Mystery", 
                Description = "En spännande mysterieberättelse.", 
                IsAvalible = true 
            },
            new Book 
            { 
                Id = 2, 
                Title = "The Adventure", 
                Author = "Karl Andersson", 
                Genre = "Adventure", 
                Description = "En äventyrsbok med en spännande resa.", 
                IsAvalible = true 
            },
            new Book 
            { 
                Id = 3, 
                Title = "Love Story", 
                Author = "Sara Johnson", 
                Genre = "Romance", 
                Description = "En romantisk berättelse om kärlek och relationer.", 
                IsAvalible = false 
            },
            new Book 
            { 
                Id = 4, 
                Title = "Science Fiction World", 
                Author = "Mikael Svensson", 
                Genre = "Science Fiction", 
                Description = "En science fiction-berättelse som utforskar framtiden.", 
                IsAvalible = true 
            }


            );
        }
    }
}
