using System;
using FluentValidation;
using Net_Core_Patika.Application.GenreOperations.Commands.DeleteGenre;

namespace Net_Core_Patika.Application.GenreOperations.Commands.UpdateGenre
{
	public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
		public UpdateGenreCommandValidator()
		{
			RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
        }
	}
}

