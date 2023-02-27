using System;
using AutoMapper;
using Net_Core_Patika.Common;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.GetBooks
{
	public class GetBooksQuery
	{
		private readonly BookStoreDbContext context;

		//mapper implement ediyoruz.
		private readonly IMapper mapper;

		//db işlemleri yaptığımızdan dbcontexte erişmemiz lazım
		public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public List<BooksViewModel> Handle()
		{
			var bookList = context.Books.OrderBy(x => x.Id).ToList<Book>();
			List<BooksViewModel> vm = new List<BooksViewModel>();
			foreach (var item in bookList)
			{
				/*
				vm.Add(new BooksViewModel()
				{
					Title = item.Title,
					Genre = ((GenreEnum)item.GenreId).ToString(),
					PublishDate = item.PublishDate.Date.ToString("dd/MM/yyyy"),	//Formtalamak için yaptık
					PageCount = item.PageCount
				});
				*/

				//Mapper yaptık:
				vm.Add(mapper.Map<BooksViewModel>(item));
			}
			return vm;
		}
	}

	//bizim DTO muz.
	public class BooksViewModel
	{
		public string Title { get; set; }
		public int PageCount { get; set; }
		public string PublishDate { get; set; }
		public string Genre { get; set; }
	}
}

