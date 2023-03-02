using System;
using Movie_Store.Entity;
using Store.Entity.Dtos;

namespace Store.Service.Abstract
{
	public interface IGenreService : IBaseService<GenreDto, Genre>
	{
	}
}

