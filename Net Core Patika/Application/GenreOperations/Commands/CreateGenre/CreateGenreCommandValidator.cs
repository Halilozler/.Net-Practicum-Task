using System;
using FluentValidation;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail;

namespace Net_Core_Patika.Application.GenreOperations.Commands.CreateGenre
{
	public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
		public CreateGenreCommandValidator()
		{
			RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
		}
	}
}

