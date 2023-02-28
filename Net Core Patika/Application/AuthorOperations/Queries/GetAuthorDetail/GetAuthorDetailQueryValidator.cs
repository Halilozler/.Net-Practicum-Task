using System;
using FluentValidation;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
	{
		public GetAuthorDetailQueryValidator()
		{
			RuleFor(x => x.AuthorId).GreaterThan(0);
		}
	}
}

