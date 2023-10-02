using AutoMapper;
using Microsoft.AspNetCore.Builder;
using MinimalAPI_Book.Models;
using MinimalAPI_Book.Models.DTOs;
using MinimalAPI_Book.Repository.IRepository;
using System.Net;
using static Azure.Core.HttpHeader;


namespace MinimalAPI_Book.Endpoint
{
    public static class BookEndpoints
    {
        public static void ConfigureBookEndPoints(this WebApplication app)
        {
            app.MapGet("/api/books/", GetAllBook).WithName("GetBooks").Produces<APIResponse>(200);
            app.MapGet("/api/books/{id:guid}", GetBook).WithName("GetBook").Produces<APIResponse>(200);

            app.MapPost("/api/createbook/", CreateBook)
                .WithName("CreateBook")
                .Accepts<BookCreateDTO>("application/json")
                .Produces(201).Produces(400);

            app.MapPut("/api/updateBook/", UpdateBook)
                .WithName("UpdateBook")
                .Accepts<BookUpdateDTO>("application/json")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapDelete("/api/deleteBook/{id:guid}", DeleteBook).WithName("Delete").Produces<APIResponse>(200);

        }

        private async static Task<IResult> GetAllBook(IBookRepository _bookRepo)
        {
            APIResponse response = new();
            response.Result = await _bookRepo.GetAllAsync();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetBook(IBookRepository _bookRepo, Guid id)
        {
            APIResponse response = new();
            response.Result = await _bookRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }


        private async static Task<IResult> CreateBook(IBookRepository _bookRepo,
            IMapper _mapper,
            BookCreateDTO book_C_DTO)
        {
            APIResponse response = new() 
            { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            if (_bookRepo.GetAsync(book_C_DTO.Title).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Book name already exists");
                return Results.BadRequest(response);

            }

            // Set the Created date to the current date and time
            book_C_DTO.Created = DateTime.Now;
            Book book = _mapper.Map<Book>(book_C_DTO);
            await _bookRepo.CreateAsync(book);
            await _bookRepo.SaveAsync();
            BookDTO bookDTO = _mapper.Map<BookDTO>(book);

            response.Result = bookDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateBook(IBookRepository _bookRepo,
           IMapper _mapper,
           //Guid id,
          BookUpdateDTO Book_U_DTO)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };

            //var existingBook = await _bookRepo.GetAsync(id);
            //if (existingBook == null)
            //{
            //    response.ErrorMessages.Add("Book not found.");
            //    return Results.NotFound(response);
            //}

            //// Update the existing book with the data from Book_U_DTO
            //_mapper.Map(Book_U_DTO, existingBook);

            //// Save the changes
            //await _bookRepo.SaveAsync();

            //response.Result = _mapper.Map<BookDTO>(existingBook);
            //response.IsSuccess = true;
            //response.StatusCode = HttpStatusCode.OK;
            //return Results.Ok(response);


           
            await _bookRepo.UpdateAsync(_mapper.Map<Book>(Book_U_DTO));
            await _bookRepo.SaveAsync();
            response.Result = _mapper.Map<BookDTO>(await _bookRepo.GetAsync(Book_U_DTO.Id));
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteBook(IBookRepository _bookRepo, Guid id)
        {
            APIResponse response = new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest
            };
            Book BookFromDB = await _bookRepo.GetAsync(id);
            if (BookFromDB != null)
            {
                await _bookRepo.DeleteAsync(BookFromDB);
                await _bookRepo.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invaid ID");
                return Results.BadRequest(response);
            }


        }


    }
}
