using MinimalAPI_Book.Models.DTOs;

namespace Web_Book.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BookService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; 
        }

        public async Task<T> CreateBookAsync<T>(BookCreateDTO bookCreateDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = bookCreateDTO,
                Url = StaticDetails.LibraryApiBase + "/api/createbook/",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteBookAsync<T>(Guid id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.LibraryApiBase + "/api/deleteBook/" + id,
                AccessToken = ""
            });
        }

        public Task<T> GetAllBooks<T>()
        {
            return this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.LibraryApiBase + "/api/books/",
                AccessToken = ""
            });
        }

        public async Task<T> GetBookById<T>(Guid id)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.LibraryApiBase + "/api/books/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateBookAsync<T>(BookUpdateDTO bookUpdateDTO)
        {
            return await this.SendAsync<T>(new Models.ApiRequest()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = bookUpdateDTO,
                Url = StaticDetails.LibraryApiBase + "/api/updateBook/",
                AccessToken = ""
            });
        }

       
    }
}
