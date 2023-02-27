using System;
using FluentValidation;
using Net_Core_Patika.BookOperations.GetBookById;

namespace Net_Core_Patika.BookOperations.CreateBook
{
    public class GetBookByIdCommandValidator : AbstractValidator<GetBookByIdCommand>
	{ 
		public GetBookByIdCommandValidator()
		{
            //string ifade sayı olmalı.
            RuleFor(command => command.Id).Must(x => int.TryParse(x, out var val) && val > 0).WithMessage("Invalid number");
        }
	}
}

