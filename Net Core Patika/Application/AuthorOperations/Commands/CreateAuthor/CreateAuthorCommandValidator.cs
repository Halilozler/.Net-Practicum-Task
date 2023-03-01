using System;
using FluentValidation;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
		public CreateAuthorCommandValidator()
		{
			RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(command => command.Model.Surname).NotEmpty().MinimumLength(2);
        }
	}
}

