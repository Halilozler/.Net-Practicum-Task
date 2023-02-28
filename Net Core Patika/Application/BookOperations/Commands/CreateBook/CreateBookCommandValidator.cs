using System;
using FluentValidation;

namespace Net_Core_Patika.BookOperations.CreateBook
{
    //AbstractValidator<CreateBookCommand> = CreateBookCommand validatorğ bu dedik.
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
	{ 
		public CreateBookCommandValidator()
		{
            //GenreId > 0
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
			RuleFor(command => command.Model.PageCount).GreaterThan(0);

			//Not null and less than now
			RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);

            //Not null and length > 4
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
        }
	}
}

