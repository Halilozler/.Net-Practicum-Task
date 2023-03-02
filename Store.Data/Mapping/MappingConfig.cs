using System;
using AutoMapper;
using Movie_Store.Entity;
using Store.Entity.Dtos;

namespace Movie_Store.Mapper
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<GenreDto, Genre>().ReverseMap();
		}
	}
}

