using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.CreateBook
{
	public class CreateBookCommand
	{
		public CreateBookModel Model { get; set; }

        private readonly IBookStoreDbContext context;

		//Mapperları oluşutrudk.
		private readonly IMapper mapper;

		public CreateBookCommand(IBookStoreDbContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}
		public void Handle()
		{
            var book = context.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
            {
				throw new InvalidOperationException("Kitap zaten mevcut");
			}

			//burada bunları tek tek tanımlamamıza gerek yok.
			//Book newBook = new Book { Title = Model.Title, PublishDate = Model.PublishDate, GenreId = Model.GenreId, PageCount = Model.PageCount };

			//Modeli Book objesine dönüştür dedik.
			var newBook = mapper.Map<Book>(Model);

            context.Books.Add(newBook);
			context.SaveChanges();
        }

		public class CreateBookModel
        {
			public string Title { get; set; }
			public int GenreId { get; set; }
			public int PageCount { get; set; }
			public DateTime PublishDate { get; set; }
		}
	}
}

