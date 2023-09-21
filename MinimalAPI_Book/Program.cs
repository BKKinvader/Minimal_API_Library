using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Book.Data;
using MinimalAPI_Book.Models;
using MinimalAPI_Book.Models.DTOs;
using MinimalAPI_Book.Services;
using MinimalAPI_Book.Validations;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient<IValidator<BookCreateDTO>, BookCreateValidation>();
builder.Services.AddTransient<IValidator<BookUpdateDTO>, BookUpdateValidation>();
builder.Services.AddScoped<IBookRepository, BookRepository>();


//Configure the DATABASE connection and register my BookDbContext
builder.Services.AddDbContext<BookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



//Get All Book
app.MapGet("/api/books/", async (BookDbContext dbcontext) =>
{
    try
    {
        //With APIResponse
        APIResponse response = new APIResponse();
        response.Result = await dbcontext.Books.ToListAsync();
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        // Log the exception
        Console.WriteLine($"Error retrieving books: {ex}");

        //Custom error response
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error retrieving books." }
        };

        // Return an error response we created
        return Results.BadRequest(response);
    }
}).WithName("GetBooks").Produces(200);

//Get Book by Id
app.MapGet("/api/books/{id:guid}", async (Guid id, BookDbContext dbcontext) =>
{
    try
    {
        //With APIResponse
        APIResponse response = new APIResponse();
        response.Result = await dbcontext.Books.FirstOrDefaultAsync(b => b.Id == id);
        response.IsSuccess = true;
        response.StatusCode = System.Net.HttpStatusCode.OK;
       
        if (response.Result == null)
        {
            return Results.NotFound($"Book with id:{id} was not found");
        }
        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving book by ID: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error retrieving book by ID." }
        };

        return Results.BadRequest(response);
    };
}).WithName("GetBook");



//Create
app.MapPost("/api/createbook/", async (IBookRepository bookRepository, [FromBody] BookCreateDTO bookCreateDTO,IValidator<BookCreateDTO> validator) =>
{


    try
    {
        // Validate the incoming data
        var validationResult = await validator.ValidateAsync(bookCreateDTO);
        if (!validationResult.IsValid)
        {
            
            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            };

            return Results.BadRequest(response);
        }

       
        var book = new Book
        {
            Title = bookCreateDTO.Title,
            Author = bookCreateDTO.Author,
            Genre = bookCreateDTO.Genre,
            Description = bookCreateDTO.Description,
            IsAvailable = true,
            Created = DateTime.UtcNow
        };

        await bookRepository.CreateAsync(book);
        return Results.Created($"/api/book/{book.Id}", book);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating book: {ex}");

        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error creating the book" }
        };

        return Results.BadRequest(response);
    }

}).WithName("CreateBook").Accepts<BookCreateDTO>("application/json").Produces<APIResponse>(201).Produces(400);




//Update
app.MapPost("/api/UpdateBook/{id}",async (Guid id, [FromBody] BookUpdateDTO bookUpdateDTO, IBookRepository bookRepository, IValidator <BookUpdateDTO> validator) =>
{
    try
    {
        //WithOut APIResponse
        // Validate the incoming data
        var validationResult = await validator.ValidateAsync(bookUpdateDTO);
        if (!validationResult.IsValid)
        {

            var response = new APIResponse
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                ErrorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
            };

            return Results.BadRequest(response);
        }


        var existingBook = await bookRepository.GetByIdAsync(id);
        if (existingBook == null)
        {
            return Results.NotFound($"Book with id:{id} was not found");
        }

        //Update properties
        existingBook.Title = bookUpdateDTO.Title;
        existingBook.Author = bookUpdateDTO.Author;
        existingBook.Genre = bookUpdateDTO.Genre;
        existingBook.Description = bookUpdateDTO.Description;
        existingBook.IsAvailable = bookUpdateDTO.IsAvailable;

        await bookRepository.UpdateAsync(existingBook);
        return Results.Ok("Book updated successfully");
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error updateing the book: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error creating the book" }
        };

        return Results.BadRequest(response);

    }


}).WithName("UpdateBook").Accepts<BookUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400);

//Delete
app.MapDelete("/api/DeleteBook/{id}", async (Guid id, IBookRepository bookRepository) =>
{
    try
    {
        //WithOut APIResponse
        var bookToDelete = await bookRepository.GetByIdAsync(id);

        if (bookToDelete == null)
        {
            return Results.NotFound($"Book with id: {id} was not found");
        }

        await bookRepository.DeleteAsync(bookToDelete);

        return Results.NoContent();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updateing the book: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error deleting the book" }
        };

        return Results.BadRequest(response);

    }
});



//Get Book by Title
app.MapGet("/api/books/title/{title}", async (string title, BookDbContext dbcontext) =>
{
    try
    {
       
        var matchingTitle = await dbcontext.Books
       .Where(t => t.Title.Contains(title)) 
       .ToListAsync();



        if (matchingTitle.Count == 0)
        {
            return Results.NotFound($"Book with title : {title} was not found");
        }
        var response = new APIResponse
        {
            IsSuccess = true,
            StatusCode = System.Net.HttpStatusCode.OK,
            Result = matchingTitle
        };

        return Results.Ok(response);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving book by Title: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error retrieving book by Title." }
        };

        return Results.BadRequest(response);
    }

});

//Get Book by Auther using contain 
app.MapGet("/api/books/author/{author}", async (string author, BookDbContext dbcontext) =>
{
    try
    {
       
         var matchingAuthors = await dbcontext.Books
        .Where(a => a.Author.Contains(author)) // "Contain" if not searching full name
        .ToListAsync();

        

        if (matchingAuthors.Count == 0)
        {
            return Results.NotFound($"Autor with name {author} was not found");
        }
        var response = new APIResponse
        {
            IsSuccess = true,
            StatusCode = System.Net.HttpStatusCode.OK,
            Result = matchingAuthors
        };

        return Results.Ok(response);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving authors: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error retrieving Author." }
        };

        return Results.BadRequest(response);
    }

});

//Get Book by Genre 
app.MapGet("/api/books/genre/{genre}", async (string genre, BookDbContext dbcontext) =>
{
    try
    {

        var matchingGenre = await dbcontext.Books
       .Where(g => g.Genre.Contains(genre)) 
       .ToListAsync();



        if (matchingGenre.Count == 0)
        {
            return Results.NotFound($"Genre: {genre} was not found");
        }
        var response = new APIResponse
        {
            IsSuccess = true,
            StatusCode = System.Net.HttpStatusCode.OK,
            Result = matchingGenre
        };

        return Results.Ok(response);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving Genre: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error retrieving Genre." }
        };

        return Results.BadRequest(response);
    }

});

//Get all Available books
app.MapGet("/api/books/available", async (BookDbContext dbcontext) =>
{
    try
    {
        var availableBooks = await dbcontext.Books
        .Where(b => b.IsAvailable)
        .ToListAsync();

        if(availableBooks.Count == 0)
        {
            return Results.NotFound("There is no available books");
        }
        var response = new APIResponse
        {
            IsSuccess = true,
            StatusCode = System.Net.HttpStatusCode.OK,
            Result = availableBooks
        };

        return Results.Ok(response);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving available books: {ex}");
        var response = new APIResponse
        {
            IsSuccess = false,
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            ErrorMessages = new List<string> { "Error retrieving available books." }
        };

        return Results.BadRequest(response);

    }
});


app.Run();