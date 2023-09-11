using FluentValidation;
using MinimalAPI_Book.Models.DTOs;

namespace MinimalAPI_Book.Validations
{
    public class BookUpdateValidation : AbstractValidator<BookUpdateDTO>
    {
        public BookUpdateValidation()
        {
            //Validation varified so the incomedata meets the standard
            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Author).NotEmpty();
            RuleFor(model => model.Genre).NotEmpty();
            RuleFor(model => model.Description).NotEmpty();
            RuleFor(model => model.IsAvalible).NotEmpty();
        }

    }
}


