using System;
using FluentValidation;

namespace Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
	{
		public GetGenreDetailQueryValidator()
		{
			RuleFor(x => x.GenreId).GreaterThan(0);
		}
	}
}

