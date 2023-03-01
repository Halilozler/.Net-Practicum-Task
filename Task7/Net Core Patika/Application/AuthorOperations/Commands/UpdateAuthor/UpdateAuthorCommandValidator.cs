using System;
using FluentValidation;
using Net_Core_Patika.Application.GenreOperations.Commands.DeleteGenre;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
		public UpdateAuthorCommandValidator()
		{
			RuleFor(command => command.Model.Name).MinimumLength(2).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(2).When(x => x.Model.Surname != string.Empty);
        }
	}
}

