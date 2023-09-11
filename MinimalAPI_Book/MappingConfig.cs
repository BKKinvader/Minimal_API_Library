using AutoMapper;
using MinimalAPI_Book.Models;
using MinimalAPI_Book.Models.DTOs;

namespace MinimalAPI_Book
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Book, BookCreateDTO>().ReverseMap();
            //Same as
            //CreateMap<Book, BookCreateDTO>();
            //CreateMap<BookCreateDTO, Book>();

            CreateMap<Book, BookDTO>().ReverseMap();

        }
    }
}
