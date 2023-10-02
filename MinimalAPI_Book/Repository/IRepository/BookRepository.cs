using Microsoft.EntityFrameworkCore;
using MinimalAPI_Book.Data;
using MinimalAPI_Book.Models;

namespace MinimalAPI_Book.Repository.IRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _dbContext;
         
        public BookRepository(BookDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }


        public async Task<Book> GetAsync(Guid id)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Book> GetAsync(string title)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(n => n.Title.ToLower() == title.ToLower());
        }



        public async Task CreateAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);

        }

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Books.Update(book);

        }

        public async Task DeleteAsync(Book book)
        {
            _dbContext.Books.Remove(book);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
