using System;
using FluentValidation;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthor>
    {
		public DeleteAuthorCommandValidator()
		{
			RuleFor(command => command.Id).NotEmpty().GreaterThan(0);
        }
	}
}

