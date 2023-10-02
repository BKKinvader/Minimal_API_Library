using MinimalAPI_Book.Models;

namespace MinimalAPI_Book.Repository.IRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetAsync(Guid id);
        Task<Book> GetAsync(string title);

        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task SaveAsync();
    }
}
