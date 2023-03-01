﻿using System;
using AutoMapper;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail
{
	public class GetGenreDetailQuery
	{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int GenreId { get; set; }

        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);

            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    //Mappera ekledik.
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}


