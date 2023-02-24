using System;
using _1.Odev.Dtos;
using _1.Odev.Model;
using AutoMapper;

namespace _1.Odev.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<StudentDto, Student>().ReverseMap();
        }
    }
}

