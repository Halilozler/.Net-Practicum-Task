using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.CreateBook
{
	public class CreateBookCommand
	{
		public CreateBookModel Model { get; set; }

        private readonly BookStoreDbContext context;

		public CreateBookCommand(BookStoreDbContext context)
		{
			this.context = context;
		}
		public void Handle()
		{
            var book = context.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
            {
				throw new InvalidOperationException("Kitap zaten mevcut");
			}

			Book newBook = new Book { Title = Model.Title, PublishDate = Model.PublishDate, GenreId = Model.GenreId, PageCount = Model.PageCount };

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

