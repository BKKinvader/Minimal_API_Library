using MinimalAPI_Book.Models.DTOs;

namespace Web_Book.Services
{
    public interface IBookService
    {
        Task<T> GetAllBooks<T>();
        Task<T> GetBookById<T>(Guid id);
        Task<T> CreateBookAsync<T>(BookCreateDTO bookCreateDTO);
        Task<T> UpdateBookAsync<T>(BookUpdateDTO bookUpdateDTO);
        Task<T> DeleteBookAsync<T>(Guid id); 
    }
}
