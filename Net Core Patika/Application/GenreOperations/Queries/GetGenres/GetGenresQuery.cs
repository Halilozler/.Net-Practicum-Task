using System;
using AutoMapper;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.GenreOperations.Queries.GetGenres
{
	//bütün genreleri getirecek.
	public class GetGenresQuery
	{
		private readonly IBookStoreDbContext _context;
		private readonly IMapper _mapper;

		public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<GenresViewModel> Handle()
		{
			var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
			List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
			return returnObj;
		}
	}

	//Mappera ekledik.
	public class GenresViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

