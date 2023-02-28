using System;
using AutoMapper;
using Net_Core_Patika.Application.AuthorOperations.Commands;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenres;
using Net_Core_Patika.BookOperations.GetBooks;
using Net_Core_Patika.Common;
using Net_Core_Patika.Entity;
using static Net_Core_Patika.BookOperations.CreateBook.CreateBookCommand;

namespace Net_Core_Patika.Mapping
{
    //Profile türetilir.
    public class MappingProfile : Profile
	{
		//ne neye dönüşecek bunları configini vericez.
		public MappingProfile()
		{
			CreateMap<CreateBookModel, Book>();

            //eğer her bir satırı nasıl değişeceğini config yapmak istersek.
            //mesela Biz Book kısmındaki Genre id(int) kısmı (GenreEnum) dönüştürdük işte 1 = PersonalGrowth karşılık geliyor oraya "PersonalGrowth" yazıyor 1 yazacağına.
            //CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));

            //genre oluşturduğumuz için artık bunun karşılığı o diye belirtik.
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            //Genre için:
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            //Author için:
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
        }
	}
}

