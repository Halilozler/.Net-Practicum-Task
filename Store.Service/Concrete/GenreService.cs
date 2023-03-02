using System;
using AutoMapper;
using Movie_Store.Entity;
using Movie_Store.Repository.Abstract;
using Store.Data.UnitOfWork;
using Store.Entity.Dtos;
using Store.Service.Abstract;

namespace Store.Service.Concrete
{
	public class GenreService : BaseService<GenreDto, Genre>, IGenreService
	{
		private readonly IGenericRepository<Genre> genreRepository;

        public GenreService(IGenericRepository<Genre> genreRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genreRepository, mapper, unitOfWork)
        {
            this.genreRepository = genreRepository;
        }
    }
}

