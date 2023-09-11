using Microsoft.EntityFrameworkCore;
using MinimalAPI_Book.Data;
using MinimalAPI_Book.Models;

namespace MinimalAPI_Book.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;

        public BookRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task CreateAsync(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
           
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            
        }
    }
}
