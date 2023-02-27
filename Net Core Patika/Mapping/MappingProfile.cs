using System;
using AutoMapper;
using Net_Core_Patika.BookOperations.GetBooks;
using Net_Core_Patika.Common;
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
			CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
		}
	}
}

