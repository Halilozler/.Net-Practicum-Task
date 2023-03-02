using System;
using Movie_Store.DbOperations;
using Movie_Store.Entity;
using Movie_Store.Repository.Concrete;
using Store.Data.Repository.Abstract;

namespace Store.Data.Repository.Concrete
{
	public class GenreRepository : GenericRepository<Genre>, IGenreRepository
	{
		public GenreRepository(MovieDbContext context) : base(context)
		{
		}
	}
}

