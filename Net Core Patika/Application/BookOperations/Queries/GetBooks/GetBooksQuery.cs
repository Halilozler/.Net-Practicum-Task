using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.Common;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.GetBooks
{
	public class GetBooksQuery
	{
		private readonly IBookStoreDbContext context;

		//mapper implement ediyoruz.
		private readonly IMapper mapper;

		//db işlemleri yaptığımızdan dbcontexte erişmemiz lazım
		public GetBooksQuery(IBookStoreDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public List<BooksViewModel> Handle()
		{
			//genreyi biz bir tablo olarak yazdık burada ona karşılık gelen genreyide ekliyoruz.(Include ile)
			var bookList = context.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList<Book>();
			//mapper ile eklediğimizden dolayı otomatik ismi getirir.

			/*List<BooksViewModel> vm = new List<BooksViewModel>();
			foreach (var item in bookList)
			{
				
				vm.Add(new BooksViewModel()
				{
					Title = item.Title,
					Genre = ((GenreEnum)item.GenreId).ToString(),
					PublishDate = item.PublishDate.Date.ToString("dd/MM/yyyy"),	//Formtalamak için yaptık
					PageCount = item.PageCount
				});
				

				//Mapper yaptık:
				vm.Add(mapper.Map<BooksViewModel>(item));
			}*/

			//autoMapper ekledik artık:
			List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(bookList);
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
		public string Author { get; set; }
	}
}

