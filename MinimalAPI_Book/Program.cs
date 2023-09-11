using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI_Book.Data;
using MinimalAPI_Book.Models;
using MinimalAPI_Book.Models.DTOs;
using MinimalAPI_Book.Services;
using MinimalAPI_Book.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient<IValidator<BookCreateDTO>, BookCreateValidation>();
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
        var books = await dbcontext.Books.ToListAsync();
        return Results.Ok(books);
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
});

//Get Book by Id
app.MapGet("/api/books/{id:guid}", async (Guid id, BookDbContext dbcontext) =>
{
    try
    {
        var book = await dbcontext.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null)
        {
            return Results.NotFound($"Book with id:{id} was not found");
        }
        return Results.Ok(book);
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
});



//Create
app.MapPost("/api/createbook/", async (IBookRepository bookRepository, [FromBody] BookCreateDTO bookCreateDTO) =>
{
    try
    {
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

        // Return a success response
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

});




//Update
app.MapPost("/api/UpdateBook/{id}",async (Guid id, [FromBody] BookUpdateDTO bookUpdateDTO, IBookRepository bookRepository) =>
{
    try
    {
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
        existingBook.IsAvailable = bookUpdateDTO.IsAvalible;

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


});

//Delete
app.MapDelete("/api/DeleteBook/{id}", async (Guid id, IBookRepository bookRepository) =>
{
    try
    {
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

app.Run();

