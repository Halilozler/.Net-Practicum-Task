using System;
using FluentValidation;
using Net_Core_Patika.BookOperations.CreateBook;

namespace Net_Core_Patika.BookOperations.DeleteBook
{
	public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
		public DeleteBookCommandValidator()
		{
			RuleFor(command => command.Id).GreaterThan(0);
		}
	}
}

