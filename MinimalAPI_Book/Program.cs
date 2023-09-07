using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_Book.Data;
using MinimalAPI_Book.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Get All Book in BookShelf list
app.MapGet("/api/books/", () =>
{
    return Results.Ok(BookShelf.booklist);
});

//Get Book by Id in BookShelf list
app.MapGet("/api/books/{id:int}", (int id) =>
{
    return Results.Ok(BookShelf.booklist.FirstOrDefault(b => b.Id == id));
});



//CreateBook in BookShelf
app.MapPost("/api/book/", ([FromBody]Book book) =>
{
    if(book.Id !=0 || string.IsNullOrEmpty(book.Title))
    {
        return Results.BadRequest("Invalid ID or Book Title");        
    }

    if (BookShelf.booklist.FirstOrDefault(b => b.Title.ToLower() == book.Title.ToLower()) != null)
    {
        return Results.BadRequest("Title of the book already exist");
    }
    
    book.Id = BookShelf.booklist.OrderByDescending(b => b.Id).FirstOrDefault().Id + 1;
    BookShelf.booklist.Add(book);
    return Results.Created($"/api/book/{book.Id}", book);
}).Produces<Book>(201).Produces(400);


app.MapPost("/api/UpdateBook/",(Book book) => 
{ 
    book.Id = book.Id; 
});

app.MapDelete("/api/DeleteBook/{id:int}", (int id) =>
{

});

app.Run();

