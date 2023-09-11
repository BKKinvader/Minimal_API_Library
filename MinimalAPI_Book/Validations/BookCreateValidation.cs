using FluentValidation;
using MinimalAPI_Book.Models.DTOs;

namespace MinimalAPI_Book.Validations
{
    public class BookCreateValidation : AbstractValidator<BookCreateDTO>
    {
        public BookCreateValidation()
        {
            //Validation varified so the incomedata meets the standard
            RuleFor(model => model.Title)
             .NotEmpty()
             .MaximumLength(100) // Example maximum length
             .WithMessage("Title is required and must not exceed 100 characters.");

            RuleFor(model => model.Author)
                .NotEmpty()
                .MaximumLength(50) 
                .WithMessage("Author is required and must not exceed 50 characters.");

            RuleFor(model => model.Genre)
                .NotEmpty()
                .WithMessage("Genre is required.");

            RuleFor(model => model.Description)
                .NotEmpty()
                .MaximumLength(500) 
                .WithMessage("Description is required and must not exceed 500 characters.");

            RuleFor(model => model.IsAvalible)
                .NotNull() // Ensure it's not null
                .WithMessage("IsAvalible must be provided.");

            
        }

    }
}
