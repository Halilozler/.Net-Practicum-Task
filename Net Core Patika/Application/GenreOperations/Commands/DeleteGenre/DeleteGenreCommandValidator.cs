using System;
using FluentValidation;

namespace Net_Core_Patika.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
		public DeleteGenreCommandValidator()
		{
			RuleFor(command => command.GenreId).GreaterThan(0);
		}
	}
}

