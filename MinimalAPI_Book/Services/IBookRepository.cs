using MinimalAPI_Book.Models;

namespace MinimalAPI_Book.Services
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
